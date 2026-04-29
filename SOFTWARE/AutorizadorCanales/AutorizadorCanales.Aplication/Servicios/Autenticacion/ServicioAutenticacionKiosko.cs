using AutoMapper;
using AutorizadorCanales.Aplication.Common.ServicioPinOperacionesApi;
using AutorizadorCanales.Aplication.Features.Common;
using AutorizadorCanales.Aplication.Servicios.Autenticacion.DTOs;
using AutorizadorCanales.Contracts.SG.Response;
using AutorizadorCanales.Core.Constantes;
using AutorizadorCanales.Core.DTO;
using AutorizadorCanales.Domain.Entidades;
using AutorizadorCanales.Domain.Entidades.CF;
using AutorizadorCanales.Domain.Entidades.CL;
using AutorizadorCanales.Domain.Entidades.SG;
using AutorizadorCanales.Domain.Entidades.TJ;
using AutorizadorCanales.Domain.Repositorios;
using AutorizadorCanales.Excepciones;
using AutorizadorCanales.Logging.Interfaz;
using System.Globalization;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Transactions;

namespace AutorizadorCanales.Aplication.Servicios.Autenticacion;

public class ServicioAutenticacionKiosko : ServicioBase<ServicioAutenticacionKiosko>, IServicioAutenticacionKiosko
{
    private readonly IRepositorioEscritura _repositorioEscritura;
    private readonly IRepositorioLectura _repositorioLectura;
    private readonly IServicioGeneradorToken _servicioGeneradorToken;
    private readonly IServicioPinOperaciones _servicioPinOperaciones;
    private readonly IContexto _contexto;
    private readonly IMapper _mapper;

    public ServicioAutenticacionKiosko(
        IRepositorioEscritura repositorioEscritura,
        IRepositorioLectura repositorioLectura,
        IServicioGeneradorToken servicioGeneradorToken,
        IContexto contexto,
        IBitacora<ServicioAutenticacionKiosko> bitacora,
        IMapper mapper,
        IServicioPinOperaciones servicioPinOperaciones) : base(contexto, bitacora)
    {
        _repositorioEscritura = repositorioEscritura;
        _repositorioLectura = repositorioLectura;
        _servicioGeneradorToken = servicioGeneradorToken;
        _contexto = contexto;
        _mapper = mapper;
        _servicioPinOperaciones = servicioPinOperaciones;
    }

    public async Task<JsonObject> AutenticarClienteKiosko(string idAudiencia, string numeroTarjeta, string password, int idTrama, string codigoUsuario, string terminal)
    {
        var tarjeta = await _repositorioEscritura.ObtenerPorCodigoAsync<Tarjeta>(decimal.Parse(numeroTarjeta));
        var clientesApi = await _repositorioEscritura.ObtenerPorExpresionConLimiteAsync<ClienteApi>
            (c => c.IdSistemaCliente == idAudiencia && c.CodigoCliente == tarjeta.CodigoCliente &&
                (c.IndicadorEstado == ClienteApi.AFILIADO || c.IndicadorEstado == ClienteApi.BLOQUEADO));

        if (clientesApi.Count > 1)
            throw new ExcepcionAUsuario("06", "Error al recuperar datos de cliente.");

        var clienteApi = await ObtenerClienteApi(clientesApi, idAudiencia, tarjeta);

        var datosUsuario = await _repositorioLectura.ObtenerPrimeroPorAsync<Usuario>
            (x => x.CodigoUsuario == codigoUsuario && x.IndicadorHabilitado == EstadoEntidad.ACTIVO);

        var pinblock = await _servicioPinOperaciones.TrasladaPINBlock(numeroTarjeta, password);

        await ValidarClienteKiosko(clienteApi, tarjeta, pinblock);

        using (var transaccion = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _repositorioEscritura.GuardarCambiosAsync();
            transaccion.Complete();
        }

        _contexto.ActualizarDatos(datosUsuario.CodigoAgencia, datosUsuario.CodigoUsuario, terminal);

        var clienteNatural = tarjeta.Duenio!.PersonaFisica;
        var idSesion = "000000" + idTrama;
        idSesion = idSesion.Substring(idSesion.Length - 6);

        var datos = new DtoSesion
        {
            IdVisual = clienteApi.IdVisual,
            PrimerNombreUsuario =
                   clienteNatural == null ? tarjeta.Duenio.NombreCliente : clienteNatural.PrimerNombre,
            SegundoNombreUsuario =
                   clienteNatural == null ? tarjeta.Duenio.NombreCliente : clienteNatural.SegundoNombre,
            ApellidoUsuario = clienteNatural == null ? "" : clienteNatural.PrimerApellido,
            NumeroTarjeta = tarjeta.NumeroTarjeta.ToString(CultureInfo.InvariantCulture)
        };

        var claims = ObtenerClaims(datos, datosUsuario.RolesAsignados.ToList());

        var (tokenAcceso, tokenGuid) = _servicioGeneradorToken.GenerarTokenKiosko
            (claims, datos, _mapper.Map<Audiencia, AudienciaResponse>(clienteApi.SistemaCliente), idSesion);

        Bitacora.Trace("Generando token refresco para usuario API " + datos.NumeroTarjeta + " en audiencia "
            + clienteApi.IdSistemaCliente + ".");

        var tokenRefresco = await _servicioGeneradorToken.GenerarTokenRefresco(new GenerarTokenRefrescoDto
            (clienteApi.IdSistemaCliente,
            clienteApi.Id,
            clienteApi.IdVisual,
            tokenAcceso,
            clienteApi.SistemaCliente.IndicadorCanal,
            TokenRefresco.TIPO_AUTENTICACION_PASSWORD));

        var autenticacion = new AutenticacionKioskoResponse(
            tokenAcceso,
            AutenticacionConstante.TIPO_BEARER,
             (int)TimeSpan.FromMinutes(_servicioGeneradorToken.ObtenerMinutosVidaToken()).TotalSeconds,
             tokenRefresco,
             clienteApi.IdSistemaCliente,
             clienteApi.IdVisual,
             clienteApi.IdVisual,
             idSesion,
             Contexto.FechaSistema.ToUniversalTime().ToString(),
             Contexto.FechaSistema.AddMinutes(_servicioGeneradorToken.ObtenerMinutosVidaToken()).ToUniversalTime().ToString());

        return JsonNode.Parse(JsonSerializer.Serialize(autenticacion))!.AsObject();
    }

    private async Task ValidarClienteKiosko(ClienteApi clienteApi, Tarjeta tarjeta, string pinblock)
    {
        try
        {
            if (tarjeta.EsAnulada)
                throw new ExcepcionAUsuario("06", "Tarjeta anulada.");

            if (tarjeta.TarjetaVencida(_contexto.FechaSistema))
                throw new ExcepcionAUsuario("06", "Tarjeta vencida.");

            await _servicioPinOperaciones.ValidarClave
                (tarjeta.NumeroTarjeta.ToString(CultureInfo.InvariantCulture), pinblock, tarjeta.NumeroPvv2);

            clienteApi
                .RegistrarIngresoClaveValida(_contexto.FechaSistema)
                .ValidarEstado();
        }
        catch (ExcepcionAUsuario ex)
        {
            if (ex.CodigoError == "55")
            {
                var parametrosCanalElectronico = await _repositorioLectura.ObtenerPorExpresionConLimiteAsync<ParametroCanalElectronico>
                    (p => p.CodigoSubCanal == Contexto.IndicadorSubCanal && p.CodigoCanal == Contexto.IndicadorCanal && p.CodigoMoneda == Moneda.SOLES);

                var numeroHorasBloqueo = (int)parametrosCanalElectronico.First
                    (x => x.CodigoParametro == ParametroCanalElectronico.TIEMPO_BLOQUEO).ValorParametro;
                var segundosRangoIntentosFallidos = (int)parametrosCanalElectronico.First
                    (p => p.CodigoParametro == ParametroCanalElectronico.TIEMPO_INGRESO_CLAVE).ValorParametro;
                var maximoIntentosFallidos = (int)parametrosCanalElectronico.First
                    (p => p.CodigoParametro == ParametroCanalElectronico.NUMERO_INTENTOS_CLAVE).ValorParametro;

                clienteApi
                    .RegistrarIngresoClaveInvalida
                        (_contexto.FechaSistema, segundosRangoIntentosFallidos, maximoIntentosFallidos, numeroHorasBloqueo, _contexto.IndicadorCanal)
                    .ActualizarMensaje(maximoIntentosFallidos);
            }
            throw;
        }
    }

    private async Task<ClienteApi> ObtenerClienteApi(List<ClienteApi> clientesApi, string idSistemaCliente, Tarjeta tarjeta)
    {
        if (clientesApi.Count != 0)
            return clientesApi.Single();

        var clienteApi = ClienteApi.Crear(idSistemaCliente, tarjeta);

        await _repositorioEscritura.AdicionarAsync(clienteApi);

        return clienteApi;
    }

    private List<Claim> ObtenerClaims(DtoSesion datos, IList<UsuarioRol> rolesUSuario)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, datos.PrimerNombreUsuario),
            new Claim(ClaimTypes.Surname, datos.SegundoNombreUsuario),
            new Claim("codigoUsuario", _contexto.CodigoUsuario),
            new Claim("codigoAgencia", _contexto.CodigoAgencia),
            new Claim("x:idVisual", datos.IdVisual),
        };

        var rolesFinales = Utilitarios.ObtenerRolesFinales();

        claims.AddRange(rolesFinales.Select(x => new Claim("role", x)));
        claims.AddRange(rolesUSuario.Select(x => new Claim("role", x.CodigoRol)));

        return claims;
    }
}
