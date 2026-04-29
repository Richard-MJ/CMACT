using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc.Filters;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;

namespace Takana.Transferencias.CCE.Api.Atributos
{
    public class GenerarSesionAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GenerarSesionAttribute() : base(typeof(GenerarSesionFilter))
        {
        }
        /// <summary>
        /// Clase que representa la generacion de filtros de la session
        /// </summary>
        private class GenerarSesionFilter : IAsyncActionFilter
        {
            /// <summary>
            /// Instancia de IContextoApi
            /// </summary>
            private readonly IContextoAplicacion _contextoApi;
            /// <summary>
            /// Instancia de IBitacora
            /// </summary>
            private readonly IBitacora<GenerarSesionFilter> _bitacora;

            /// <summary>
            /// Constructor
            /// </summary>
            public GenerarSesionFilter(
                IContextoAplicacion contextoApi,
                IBitacora<GenerarSesionFilter> bitacora)
            {
                _bitacora = bitacora;
                _contextoApi = contextoApi;
            }
            /// <summary>
            /// Procesa las respuesta y agrega el encabezado
            /// </summary>
            /// <param name="context"></param>
            /// <param name="next"></param>
            /// <returns></returns>
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                try
                {
                    var Autorizador = context.HttpContext.Request.Headers["Authorization"];
                    var manejador = new JwtSecurityTokenHandler();
                    var token = manejador.ReadToken(Autorizador.First()?.Substring(7)) as JwtSecurityToken;
                    if (token == null) throw new Exception("No se recupero datos del token de seguridad.");
                    var idLogin = GetClaimValue(token, "x:idSesion", "");
                    var idAudiencia = GetClaimValue(token, "aud", "");
                    var idUsuarioLogin = GetClaimValue(token, "sub", "");
                    var claveEncriptada = GetClaimValue(token, "claveEncriptada", "");
                    var idTerminalLogin = GetClaimValue(token, "x:idTerminal", "");
                    var idCanalOrigen = GetClaimValue(token, "x:canal", "");
                    var codigoUsuario = GetClaimValue(token, "x:codigoUsuario", "");
                    var codigoAgencia = GetClaimValue(token, "x:codigoAgencia", "");
                    var modeloDispositivo = GetClaimValue(token, "modelo_dispositivo", "");
                    var ipAddress = GetClaimValue(token, "ip_address", "");
                    var navegador = GetClaimValue(token, "navegador", "");
                    var sistemaOperativo = GetClaimValue(token, "sistema_operativo", "");
                    var idVisual = GetClaimValue(token, "x:idVisual", "");

                    _contextoApi.Actualizar(_contextoApi.IdSesion, idLogin, idAudiencia,
                        idUsuarioLogin, idTerminalLogin, idCanalOrigen, codigoUsuario, codigoAgencia,
                        modeloDispositivo, ipAddress, navegador, sistemaOperativo, idVisual, claveEncriptada, Autorizador.First()?.Substring(7) ?? string.Empty);
                }
                catch (Exception excepcion)
                {
                    _bitacora.Error("Error al recuperar los datos de sesión del token: " + excepcion.Message);
                    throw new Exception("Error al recuperar los datos de sesión del token: " + excepcion.Message);
                }

                await next();
            }

            /// <summary>
            /// Valida si existen los claims en el token, caso contrario los rellena vacio
            /// </summary>
            /// <param name="token">Token</param>
            /// <param name="claimType">Nombre del claim</param>
            /// <param name="defaultValue">Valor por defecto</param>
            /// <returns></returns>
            public string GetClaimValue(JwtSecurityToken token, string claimType, string defaultValue = "")
            {
                var claim = token.Claims.SingleOrDefault(c => c.Type == claimType);
                return claim != null ? claim.Value : defaultValue;
            }
        }
    }
}
