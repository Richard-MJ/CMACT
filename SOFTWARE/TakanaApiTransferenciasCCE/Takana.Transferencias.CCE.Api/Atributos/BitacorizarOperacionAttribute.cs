using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;

namespace Takana.Transferencias.CCE.Api.Atributos
{
    public class BitacorizarOperacionAttribute : TypeFilterAttribute
    {
        public BitacorizarOperacionAttribute() : base(typeof(BitacorizarOperacionFilter))
        {
        }

        private class BitacorizarOperacionFilter : IActionFilter
        {
            private readonly IBitacora<BitacorizarOperacionFilter> _bitacora;

            /// <summary>
            /// Clase de bitacorizacion Operacion
            /// </summary>
            /// <param name="bitacora"></param>
            public BitacorizarOperacionFilter(IBitacora<BitacorizarOperacionFilter> bitacora)
            {
                _bitacora = bitacora;
            }

            /// <summary>
            /// Método que bitacoriza la ejecucion de la operacion
            /// </summary>
            /// <param name="context"></param>
            public void OnActionExecuting(ActionExecutingContext context)
            {
                _bitacora.Trace("Operación: {metodo}<|>{ruta}<|>{nombreAction}",
                    context.HttpContext.Request.Method, context.HttpContext.Request.Path.Value,
                    context.ActionDescriptor.DisplayName);
            }

            /// <summary>
            /// Método que bitacoriza la respuesta de la operacion
            /// </summary>
            /// <param name="context"></param>
            public void OnActionExecuted(ActionExecutedContext context)
            {
                if (EsRespuestaOk(context.HttpContext.Response))
                {
                    _bitacora.Trace("Respuesta: {nombreAction}<|>{codigoRespuesta}",
                        context.ActionDescriptor.DisplayName, context.HttpContext.Response.StatusCode);
                }
                else
                {
                    if (EsRespuestaFatal(context.HttpContext.Response))
                    {
                        _bitacora.Fatal("Respuesta: {nombreAction}<|>{codigoRespuesta}",
                            context.ActionDescriptor.DisplayName, context.HttpContext.Response.StatusCode);
                    }
                    else
                    {
                        _bitacora.Error("Respuesta: {nombreAction}<|>{codigoRespuesta}",
                            context.ActionDescriptor.DisplayName, context.HttpContext.Response.StatusCode);
                    }
                }
            }

            /// <summary>
            /// Método que retorna respuesta positiva
            /// </summary>
            /// <param name="response"></param>
            /// <returns>Retorna estado exitoso</returns>
            private bool EsRespuestaOk(HttpResponse response)
            {
                return response.StatusCode >= 200 && response.StatusCode <= 299;
            }

            /// <summary>
            /// Método que retorna respuesta negativa
            /// </summary>
            /// <param name="response"></param>
            /// <returns>Retorna estado negativo</returns>
            private bool EsRespuestaFatal(HttpResponse response)
            {
                return response.StatusCode >= 500 && response.StatusCode <= 599;
            }
        }

    }
}
