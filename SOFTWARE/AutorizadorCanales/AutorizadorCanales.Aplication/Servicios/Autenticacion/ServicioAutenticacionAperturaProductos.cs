using AutorizadorCanales.Aplication.Common.ServicioPinOperacionesApi;
using AutorizadorCanales.Aplication.Features.Autenticacion.Commands;
using AutorizadorCanales.Aplication.Features.Common;
using AutorizadorCanales.Infrastructure.Servicios;
using AutorizadorCanales.Contracts.SG.Response;
using AutorizadorCanales.Domain.Entidades.CF;
using AutorizadorCanales.Domain.Entidades.CL;
using AutorizadorCanales.Domain.Repositorios;
using AutorizadorCanales.Domain.Entidades;
using AutorizadorCanales.Logging.Interfaz;
using AutorizadorCanales.Core.Constantes;
using AutorizadorCanales.Excepciones;
using AutorizadorCanales.Core.DTO;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace AutorizadorCanales.Aplication.Servicios.Autenticacion;

public class ServicioAutenticacionAperturaProductos : ServicioBase<ServicioAutenticacion>, IServicioAutenticacionAperturaProductos
{
    private readonly IRepositorioLectura _repositorioLectura;
    private readonly IServicioGeneradorToken _servicioGeneradorToken;

    public ServicioAutenticacionAperturaProductos(
        IRepositorioLectura repositorioLectura,
        IServicioGeneradorToken servicioGeneradorToken,
        IServicioPinOperaciones servicioPinOperaciones,
        IContexto contexto,
        IBitacora<ServicioAutenticacion> bitacora)
        : base(contexto, bitacora)
    {
        _servicioGeneradorToken = servicioGeneradorToken;
        _repositorioLectura = repositorioLectura;
    }

    public async Task<JsonObject> AutenticarAperturaProducto(AutenticarAperturaProductoCommand datos)
    {
        try
        {
            var tipoDocumento = await _repositorioLectura.ObtenerPorCodigoAsync<TipoDocumento>
                (EntidadEmpresa.EMPRESA, datos.IdTipoDocumento);

            if (!tipoDocumento.IndicadorHomeBankingAppCanales)
                throw ExcepcionAUsuario.ExcepcionAfiliacionInicioSesion();

            if (tipoDocumento.EsTipoDocumentoIdentidad && tipoDocumento.Mascara.Length != datos.NumeroDocumento.Trim().Length)
                throw new ExcepcionAUsuario("06", $"El número de caracteres del {tipoDocumento.DescripcionTipoDocumento} es incorrecto.");

            var parametrosCanalElectronico =
               await _repositorioLectura.ObtenerPorExpresionConLimiteAsync<ParametroCanalElectronico>(p =>
                   p.CodigoSubCanal == Contexto.IndicadorSubCanal && p.CodigoCanal == Contexto.IndicadorCanal && p.CodigoMoneda == Moneda.SOLES);

            var numeroHorasBloqueo = (int)parametrosCanalElectronico.First
                (x => x.CodigoParametro == ParametroCanalElectronico.TIEMPO_BLOQUEO).ValorParametro;
            var segundosRangoIntentosFallidos = (int)parametrosCanalElectronico.First
                (p => p.CodigoParametro == ParametroCanalElectronico.TIEMPO_INGRESO_CLAVE).ValorParametro;
            var maximoIntentosFallidos = (int)parametrosCanalElectronico.First
                (p => p.CodigoParametro == ParametroCanalElectronico.NUMERO_INTENTOS_CLAVE).ValorParametro;

            var traceSeisUltimosDigitos = "000000" + datos.IdTrama;
            traceSeisUltimosDigitos = traceSeisUltimosDigitos.Substring(traceSeisUltimosDigitos.Length - 6);

            var usuarioLogueado = new UsuarioLogueadoDto() { NumeroTarjeta = datos.NumeroDocumento };

            var (tokenAcceso, tokenGuid) = _servicioGeneradorToken.GenerarTokenAperturaProducto(
                autenticarCommand: datos,
                usuario: usuarioLogueado,
                roles: Utilitarios.ObtenerRolesFinales(),
                idSesion: traceSeisUltimosDigitos,
                sistemaCliente: datos.SistemaCliente);

            Bitacora.Trace("Generando token refresco para usuario API " + datos.NumeroDocumento + " en audiencia "
                + datos.SistemaCliente.IdAudiencia + ".");

            var tiempoMaximoInactividad = (int)parametrosCanalElectronico.First
                (p => p.CodigoParametro == ParametroCanalElectronico.TIEMPO_INACTIVIDAD).ValorParametro;

            var autenticacion = new AutenticacionResponse(
                tokenAcceso,
                AutenticacionConstante.TIPO_BEARER,
                (int)TimeSpan.FromMinutes(_servicioGeneradorToken.ObtenerMinutosVidaTokenApertura()).TotalSeconds,
                (int)tiempoMaximoInactividad,
                string.Empty,
                datos.SistemaCliente.IdAudiencia,
                usuarioLogueado.DispositivoAutorizado.ToString().ToLower(),
                Contexto.CodigoUsuario,
                Contexto.CodigoUsuario,
                traceSeisUltimosDigitos,
                Contexto.FechaSistema.ToUniversalTime().ToString(),
                Contexto.FechaSistema.AddMinutes(_servicioGeneradorToken.ObtenerMinutosVidaTokenApertura()).ToUniversalTime().ToString());

            var autenticacionObject = JsonNode.Parse
                (JsonSerializer.Serialize(autenticacion))!.AsObject();

            return autenticacionObject;
        }
        catch (ExcepcionAUsuario ex)
        {
            throw;
        }
    }
}