using AutorizadorCanales.Aplication.Features.Autenticacion.Commands;
using AutorizadorCanales.Aplication.Servicios.Autenticacion.DTOs;
using AutorizadorCanales.Aplication.Servicios.Autenticacion;
using AutorizadorCanales.Aplication.Common.JwtGenerator;
using AutorizadorCanales.Aplication.Features.Common;
using AutorizadorCanales.Aplication.Servicios;
using AutorizadorCanales.Contracts.SG.Response;
using AutorizadorCanales.Domain.Entidades.CF;
using AutorizadorCanales.Domain.Entidades.SG;
using AutorizadorCanales.Domain.Repositorios;
using AutorizadorCanales.Core.Utilitarios;
using AutorizadorCanales.Domain.Entidades;
using Microsoft.AspNetCore.Authentication;
using AutorizadorCanales.Logging.Interfaz;
using AutorizadorCanales.Core.Constantes;
using AutorizadorCanales.Excepciones;
using AutorizadorCanales.Core.DTO;
using System.Security.Claims;
using System.Transactions;

namespace AutorizadorCanales.Infrastructure.Servicios;

public class ServicioGeneradorToken : ServicioBase<ServicioGeneradorToken>, IServicioGeneradorToken
{
    #region Propiedades
    private readonly IRepositorioEscritura _repositorioEscritura;
    private readonly IRepositorioLectura _repositorioLectura;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private string _codigoUsuario
    {
        get =>

            Contexto.IndicadorCanal switch
            {
                CanalElectronicoConstante.APP_CMACT => UsuarioConstante.APP_MOVIL,
                CanalElectronicoConstante.HOME_BANKING => UsuarioConstante.HOME_BANKING,
                CanalElectronicoConstante.BANKING_EMPRESARIAL => UsuarioConstante.HOME_BANKING,
                _ => Contexto.CodigoUsuario
            };
    }
    #endregion

    #region Constructor
    public ServicioGeneradorToken(
        IRepositorioEscritura repositorioEscritura,
        IRepositorioLectura repositorioLectura,
        IContexto contexto,
        IBitacora<ServicioGeneradorToken> bitacora,
        IJwtTokenGenerator jwtTokenGenerator) : base(contexto, bitacora)
    {
        _repositorioEscritura = repositorioEscritura;
        _repositorioLectura = repositorioLectura;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    #endregion

    #region Implementación

    /// <summary>
    /// Método que genra el token de acceso
    /// </summary>
    /// <param name="autenticarCommand">Datos que contiene el comando</param>s
    /// <param name="usuario"></param>
    /// <param name="roles"></param>
    /// <param name="idSesion"></param>
    /// <param name="sistemaCliente"></param>
    /// <param name="claimsToken"></param>
    /// <returns></returns>
    public (string, string) GenerarToken(
            string[] roles,
            AudienciaResponse sistemaCliente,
            AutenticarClienteCommand? autenticarCommand = null,
            UsuarioLogueadoDto? usuario = null,
            string? idSesion = null,
            List<Claim>? claimsToken = null)
    {
        var claims = GenerarClaimsClienteAfiliado(autenticarCommand, usuario, roles, idSesion, claimsToken);

        return _jwtTokenGenerator.GenerateJwtToken(claims, sistemaCliente.IdSecreto, sistemaCliente.DireccionOrigenPermitido, sistemaCliente.IdAudiencia);
    }

    /// <summary>
    /// Método que genera el token de apertura producto
    /// </summary>
    /// <param name="roles"></param>
    /// <param name="sistemaCliente"></param>
    /// <param name="autenticarCommand"></param>
    /// <param name="usuario"></param>
    /// <param name="idSesion"></param>
    /// <param name="claimsToken"></param>
    /// <returns></returns>
    public (string, string) GenerarTokenAperturaProducto(
     string[] roles,
     AudienciaResponse sistemaCliente,
     AutenticarAperturaProductoCommand? autenticarCommand = null,
     UsuarioLogueadoDto? usuario = null,
     string? idSesion = null,
     List<Claim>? claimsToken = null)
    {
        var claims = GenerarClaimsAperturaProducto(autenticarCommand, usuario, roles, idSesion, claimsToken);

        return _jwtTokenGenerator.GenerateJwtToken(claims, sistemaCliente.IdSecreto, sistemaCliente.DireccionOrigenPermitido, sistemaCliente.IdAudiencia, true);
    }

    /// <summary>
    /// Método que genera el token de refresco
    /// </summary>
    /// <param name="datos">Datos necesarios para generar el token de refresco</param>
    /// <returns>Token de refresco</returns>
    public async Task<string> GenerarTokenRefresco(GenerarTokenRefrescoDto datos)
    {
        var parametroCanalElectronico = await _repositorioLectura.ObtenerPorCodigoAsync<ParametroCanalElectronico>
            (ParametroCanalElectronico.PARAMETRO_TOKEN_REFRESCO, MonedaConstante.SOLES, datos.IndicadorCanal, Contexto.IndicadorSubCanal);

        var tokenId = Guid.NewGuid().ToString("N");

        var tokenRefresco = TokenRefresco.Crear
        (
            tokenId.HashString(),
            datos.IdAudiencia,
            datos.IdClienteApi,
            EstadoEntidad.ACTIVO,
            Contexto.FechaSistema,
            Contexto.FechaSistema.AddMinutes((int)parametroCanalElectronico.ValorParametro),
            _jwtTokenGenerator.SerializarToken(datos.TokenAcceso),
            datos.TipoAutenticacion,
            Contexto.IdTerminal,
            datos.IdVisual
        );

        var tokensActivos = await _repositorioLectura.ObtenerPorExpresionConLimiteAsync<TokenRefresco>
            (t => t.IdClienteApi == datos.IdClienteApi && t.FechaExpiracion > Contexto.FechaSistema);

        if (tokensActivos.Any(x => tokenRefresco.IdTipoAutenticacion == 1 && x.IdDispositivoAutenticacion != tokenRefresco.IdDispositivoAutenticacion))
            Bitacora.Warning("Se esta generando un token de refresco con un dispositivo diferente al original.");

        using (var transaccion = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _repositorioEscritura.AdicionarAsync(tokenRefresco);
            await _repositorioEscritura.GuardarCambiosAsync();
            transaccion.Complete();
        }

        return tokenId;
    }

    /// <summary>
    /// Método que deserializa un token de acceso
    /// </summary>
    /// <param name="tokenSerializado">token serializado</param>
    /// <returns>token original</returns>
    public AuthenticationTicket DeserializarToken(string tokenSerializado)
    {
        try
        {
            return TicketSerializer.Default.Deserialize(Convert.FromBase64String(tokenSerializado))!;
        }
        catch (Exception ex)
        {
            throw new ExcepcionAUsuario("06", "Error al obtener el token de refresco", ex);
        }
    }

    /// <summary>
    /// Anular tokens de refresco
    /// </summary>
    /// <param name="idVisual">Id visual</param>
    /// <param name="idAudiencia">id de audiencia</param>
    /// <returns></returns>
    public async Task AnularTokensRefresco(string idVisual, string idAudiencia)
    {
        var tokensRefrescoActivos = await _repositorioEscritura.ObtenerPorExpresionConLimiteAsync<TokenRefresco>
            (t => t.IdSistemaCliente == idAudiencia && t.ClienteApi.IdVisual == idVisual && t.IndicadorEstado == EstadoEntidad.ACTIVO);

        tokensRefrescoActivos.ForEach(t =>
        {
            t.Desactivar();
        });

        using (var transaccion = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _repositorioEscritura.GuardarCambiosAsync();
            transaccion.Complete();
        }
    }

    public (string, string) GenerarTokenKiosko(List<Claim> claims, DtoSesion datos, AudienciaResponse sistemaCliente, string idSesion)
    {
        claims = AgregarClaimsDefecto(claims, datos.NumeroTarjeta, idSesion);
        return _jwtTokenGenerator.GenerateJwtToken(claims, sistemaCliente.IdSecreto, sistemaCliente.DireccionOrigenPermitido, sistemaCliente.IdAudiencia);
    }

    private List<Claim> AgregarClaimsDefecto(List<Claim> claims, string usuarioAutenticado, string idSesion, List<Claim> claimsToken = null)
    {
        claims.Add(new Claim("fechaEmision", Utilitarios.ConvertirATimeUnix(Contexto.FechaSistema.ToUniversalTime()).ToString()));
        claims.Add(new Claim("modelo_dispositivo", Contexto.ModeloDispositivo));
        claims.Add(new Claim("ip_address", Contexto.DireccionIp));
        claims.Add(new Claim("navegador", Contexto.Navegador));
        claims.Add(new Claim("sistema_operativo", Contexto.SistemaOperativo));
        claims.Add(new Claim("x:idSesion", idSesion));
        claims.Add(new Claim("sub", usuarioAutenticado));
        claims.Add(new Claim("x:idTerminal",
              claimsToken != null
            ? ObtenerValorClaim(claimsToken, "x:idTerminal")
            : Contexto.IdTerminal));
        claims.Add(new Claim("x:canal", Contexto.IndicadorCanal));
        claims.Add(new Claim("x:codigoAgencia",
              claimsToken != null
            ? ObtenerValorClaim(claimsToken, "x:codigoAgencia")
            : Contexto.CodigoAgencia));
        claims.Add(new Claim("x:codigoUsuario",
              claimsToken != null
            ? ObtenerValorClaim(claimsToken, "x:codigoUsuario")
            : _codigoUsuario));

        return claims;
    }

    #endregion

    #region Métodos privados

    private List<Claim> GenerarClaimsClienteAfiliado(AutenticarClienteCommand? autenticarCommand, UsuarioLogueadoDto? usuario, string[] roles, string? idSesion, List<Claim>? claimsToken)
    {
        idSesion = autenticarCommand != null
            ? idSesion!
            : ObtenerValorClaim(claimsToken!, "x:idSesion");
        var numeroTarjeta = autenticarCommand != null
            ? usuario!.NumeroTarjeta
            :
                string.IsNullOrEmpty(ObtenerValorClaim(claimsToken!, ClaimTypes.NameIdentifier))
                    ? ObtenerValorClaim(claimsToken!, "sub")
                    : ObtenerValorClaim(claimsToken!, ClaimTypes.NameIdentifier);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, autenticarCommand != null ? usuario!.PrimerNombreUsuario : ObtenerValorClaim(claimsToken!,ClaimTypes.Name)),
            new Claim(ClaimTypes.Surname, autenticarCommand != null ? usuario!.SegundoNombreUsuario : ObtenerValorClaim(claimsToken!,ClaimTypes.Surname)),
            new Claim("codigoAgencia",
                claimsToken != null
                    ? ObtenerValorClaim(claimsToken, "codigoAgencia")
                    : Contexto.CodigoAgencia),
            new Claim("codigoUsuario",
                claimsToken != null
                    ? ObtenerValorClaim(claimsToken, "codigoUsuario")
                    : _codigoUsuario),

            new Claim("x:idVisual", autenticarCommand != null ? usuario!.CodigoUsuario : ObtenerValorClaim(claimsToken!,"x:idVisual")),
        };

        if (usuario != null && usuario.DispositivoAutorizado)
            claims.Add(new Claim("confirmacion_sms", "true"));

        if (claimsToken != null && !string.IsNullOrEmpty(ObtenerValorClaim(claimsToken, "confirmacion_sms")))
            claims.Add(new Claim("confirmacion_sms", "true"));

        foreach (var role in roles)
        {
            claims.Add(new Claim("role", role));
        };

        claims = AgregarClaimsDefecto(claims, numeroTarjeta, idSesion, claimsToken);

        return claims;
    }

    private string ObtenerValorClaim(List<Claim> claims, string nombreClaim)
    {
        return claims.FirstOrDefault(x => x.Type == nombreClaim)?.Value ?? "";
    }

    /// <summary>
    /// Metodo obtener minutos de vida Token
    /// </summary>
    /// <returns></returns>
    public int ObtenerMinutosVidaToken()
    {
        return _jwtTokenGenerator.GetLifeTimeToken();
    }

    /// <summary>
    /// Metodo obtener minutos de vida Token
    /// </summary>
    /// <returns></returns>
    public int ObtenerMinutosVidaTokenApertura()
    {
        return _jwtTokenGenerator.GetLifeTimeTokenApertura();
    }

    /// <summary>
    /// Método que genera los claims para el token de apertura de producto
    /// </summary>
    /// <param name="autenticarCommand"></param>
    /// <param name="usuario"></param>
    /// <param name="roles"></param>
    /// <param name="idSesion"></param>
    /// <param name="claimsToken"></param>
    /// <returns></returns>
    private List<Claim> GenerarClaimsAperturaProducto(AutenticarAperturaProductoCommand? autenticarCommand, UsuarioLogueadoDto? usuario, string[] roles, string? idSesion, List<Claim>? claimsToken)
    {
        idSesion = autenticarCommand != null
            ? idSesion!
            : ObtenerValorClaim(claimsToken!, "x:idSesion");

        var claims = new List<Claim>
        {
            new Claim("codigoAgencia","01"),
            new Claim("codigoUsuario","APP_MOVIL"),
            new Claim("role", "WEBAP"),
            new Claim("subCanal", "2"),
        };

        claims = AgregarClaimsDefecto(claims, usuario.NumeroTarjeta, idSesion, claimsToken);

        return claims;
    }
    #endregion
}
