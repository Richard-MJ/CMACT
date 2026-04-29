using Polly.Wrap;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;

namespace Takana.Transferencias.CCE.Api.Atributos
{
    public class EncryptacionResponseBodyAttribute : TypeFilterAttribute
    {
        public EncryptacionResponseBodyAttribute() : base(typeof(EncryptacionResponseBody))
        {
        }

        private class EncryptacionResponseBody : IAsyncActionFilter
        {
            private readonly IConfiguration _configuration;
            private readonly IBitacora<EncryptacionResponseBody> _bitacora;
            private readonly IServicioAplicacionPeticion _servicioAplicacionPeticion;
            private readonly IServicioAplicacionSeguridad _servicioAplicacionSeguridad;
            /// <summary>
            /// Referencia a la politica de reintentos
            /// </summary>
            private AsyncPolicyWrap _asyncPolicyWrap;

            /// <summary>
            /// Clase de encriptacion constructor
            /// </summary>
            /// <param name="bitacora"></param>
            /// <param name="configuration"></param>
            /// <param name="servicioAplicacionPeticion"></param>
            /// <param name="servicioAplicacionSeguridad"></param>
            /// <param name="configuracionPollyWrapOptions"></param>
            public EncryptacionResponseBody(
                IConfiguration configuration,
                IBitacora<EncryptacionResponseBody> bitacora,
                IServicioAplicacionPeticion servicioAplicacionPeticion,
                IServicioAplicacionSeguridad servicioAplicacionSeguridad,
                IConfiguracionPollyWrapOptions configuracionPollyWrapOptions)
            {
                _bitacora = bitacora;
                _configuration = configuration;
                _servicioAplicacionPeticion = servicioAplicacionPeticion;
                _servicioAplicacionSeguridad = servicioAplicacionSeguridad;
                _asyncPolicyWrap = _servicioAplicacionSeguridad.PoliticasReintento(configuracionPollyWrapOptions.NumberRetries);
            }

            /// <summary>
            /// Procesa las respuesta y agrega el encabezado
            /// </summary>
            /// <param name="context"></param>
            /// <param name="next"></param>
            /// <returns></returns>
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                bool entornoEncriptacion = _configuration.GetValue<bool>("TAK_USAR_ENTORNO_SEGURIDAD_CCE");

                var resultContext = await next();
                
                if (entornoEncriptacion)
                {
                    if (context.HttpContext.Request.Headers.ContainsKey("request-id"))
                    {
                        var identificadorSolicitud = context.HttpContext.Request.Headers["request-id"].ToString();
                        context.HttpContext.Response.Headers["request-id"] = identificadorSolicitud;
                    }

                    if (resultContext.Result is ObjectResult objectResult && objectResult.Value != null)
                    {
                        var encryptedResponse = await _asyncPolicyWrap.ExecuteAsync(async () => 
                            await _servicioAplicacionPeticion.EncriptarMensajeCCE(objectResult.Value));

                        var encryptedBodyJson = JsonConvert.SerializeObject(encryptedResponse);
                        _bitacora.Trace($"JSON Response Con Encriptacion: {encryptedBodyJson}");

                        resultContext.Result = new ContentResult
                        {
                            Content = encryptedBodyJson,
                            ContentType = "application/json; charset=UTF-8",
                            StatusCode = 200
                        };
                    }
                }
            }
        }

    }
}
