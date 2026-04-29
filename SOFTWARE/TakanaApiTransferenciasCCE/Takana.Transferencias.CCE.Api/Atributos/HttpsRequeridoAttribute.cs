using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Takana.Transferencias.CCE.Api.Atributos
{
    public class HttpsRequeridoAttribute : ActionFilterAttribute
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Clase de Http Requerido constructor
        /// </summary>
        /// <param name="configuration"></param>
        public HttpsRequeridoAttribute(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool usarHttps = _configuration.GetValue<bool>("TAK_USAR_VALIDACION_SSL");

            if (!String.Equals(context.HttpContext.Request.Scheme,
                "https", StringComparison.OrdinalIgnoreCase) && usarHttps)
            {
                var resultado = new ContentResult();
                resultado.StatusCode = 403;
                resultado.Content = "HTTPS Required";

                context.Result = resultado;
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
    }
}
