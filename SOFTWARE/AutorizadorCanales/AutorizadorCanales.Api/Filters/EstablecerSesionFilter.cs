using AutorizadorCanales.Infrastructure.Modelos;
using AutorizadorCanales.Logging.Interfaz;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using AutorizadorCanales.Api.Helpers;
using AutorizadorCanales.Excepciones;

namespace AutorizadorCanales.Api.Filters;

public class EstablecerSesionFilter : IActionFilter
{
    private readonly IContexto _contexto;
    private readonly IBitacora<EstablecerSesionFilter> _bitacora;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EstablecerSesionFilter(
        IContexto contexto, 
        IBitacora<EstablecerSesionFilter> bitacora, 
        IHttpContextAccessor httpContextAccessor)
    {
        _contexto = contexto;
        _bitacora = bitacora;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Se sobreescribe el método OnActionExecutionAsync, que refiere a ejecutarse antes de la acción
    /// </summary>
    /// <param name="context">Contexto de la acción</param>
    /// <param name="next">Siguiente acción</param>
    /// <returns></returns>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var sesion = new Sesion();
        var idTerminalLogin = IpHelper.ObtenerIp(context.HttpContext);

        if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out var valorHeader))
        {
            try
            {
                var manejador = new JwtSecurityTokenHandler();
                var token = manejador.ReadToken(valorHeader.FirstOrDefault()!.Substring(7)) as JwtSecurityToken;
                if (token == null)
                    throw new ExcepcionAUsuario("06", "No se recuperó token de segurida");
                var idLogin = GetClaimValue(token, "x:idLogin");
                var idSesion = GetClaimValue(token, "x:idSesion");
                var idCanalOrigen = token.Claims.Single(c => c.Type == "x:canal").Value;
                var idUsuarioAutenticado = token.Claims.Single(c => c.Type == "sub").Value;
                var codigoUsuario = token.Claims.Single(c => c.Type == "codigoUsuario").Value;
                var codigoAgencia = token.Claims.Single(c => c.Type == "codigoAgencia").Value;
                var audiencia = token.Claims.Single(c => c.Type == "aud").Value;

                var subCanalOrigen = GetClaimValue(token, "subCanal");
                var modeloDispositivo = GetClaimValue(token, "modelo_dispositivo");
                var direccionIp = GetClaimValue(token, "ip_address");
                var navegador = GetClaimValue(token, "navegador");
                var sistemaOperativo = GetClaimValue(token, "sistema_operativo");

                sesion = new Sesion(
                    idSesion,
                    idTerminalLogin,
                    idTerminalLogin,
                    modeloDispositivo,
                    direccionIp,
                    navegador,
                    sistemaOperativo,
                    valorHeader.FirstOrDefault()!.Substring(7),
                    idCanalOrigen,
                    subCanalOrigen,
                    audiencia,
                    idUsuarioAutenticado);
            }
            catch (Exception ex)
            {
                _bitacora.Error("Error al recuperar los datos de sesión del token. " + ex.Message);
            }
        }

        _contexto.ActualizarDatos(sesion.IdSesion,
            sesion.CodigoUsuario,
            sesion.CodigoAgencia,
            sesion.IndicadorCanal,
            sesion.IndicadorSubCanal,
            idTerminalLogin,
            sesion.ModeloDispositivo,
            sesion.DireccionIp,
            sesion.Navegador,
            sesion.SistemaOperativo,
            sesion.Token,
            sesion.IdAudiencia,
            sesion.IdUsuarioAutenticado,
            _httpContextAccessor.HttpContext!.User.Identity?.Name ?? "no-auth");
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    /// <summary>
    /// Valida si existen los claims en el token, caso contrario los rellena vacio
    /// </summary>
    /// <param name="token">Token</param>
    /// <param name="claimType">Nombre del claim</param>
    /// <param name="defaultValue">Valor por defecto</param>
    /// <returns></returns>
    private string GetClaimValue(JwtSecurityToken token, string claimType, string defaultValue = "")
    {
        var claim = token.Claims.SingleOrDefault(c => c.Type == claimType);
        return claim != null ? claim.Value : defaultValue;
    }
}
