using System.Text;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;

namespace Takana.Transferencias.CCE.Api.Servicio
{
    public class JsonLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IBitacora<JsonLoggerMiddleware> _bitacora;
        /// <summary>
        /// Clase de json loger middleware
        /// </summary>
        /// <param name="next"></param>
        /// <param name="bitacora"></param>
        public JsonLoggerMiddleware(
            RequestDelegate next,
            IConfiguration configuration,
            IBitacora<JsonLoggerMiddleware> bitacora)
        {
            _next = next;
            _bitacora = bitacora;
            _configuration = configuration;
        }

        /// <summary>
        /// Método que invoka un Json body request
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            bool esProduccion = _configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT") == "Production";

            if (context.Request.ContentType != null && context.Request.ContentType.Contains("application/json") && !esProduccion)
            {
                context.Request.EnableBuffering();

                using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
                var requestBody = await reader.ReadToEndAsync();

                _bitacora.Info($"JSON Request Body: {requestBody}");

                context.Request.Body.Position = 0;
            }

            await _next(context);
        }
    }

}
