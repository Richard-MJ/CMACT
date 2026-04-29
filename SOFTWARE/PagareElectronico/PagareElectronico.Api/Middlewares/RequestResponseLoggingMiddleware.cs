using System.Text;
using System.Diagnostics;
using PagareElectronico.Aplicacion.Helper;
using PagareElectronico.Infraestructura.Logging;

namespace PagareElectronico.Api.Middlewares
{
    public sealed class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private const int MaxBodyLength = 5000;
        private readonly IBitacora<RequestResponseLoggingMiddleware> _bitacora;

        public RequestResponseLoggingMiddleware(
            RequestDelegate next,
            IBitacora<RequestResponseLoggingMiddleware> bitacora)
        {
            _next = next;
            _bitacora = bitacora;
        }

        /// <summary>
        /// Metodo que Invoka la trazamilidad del request
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var traceId = context.TraceIdentifier;
            var stopwatch = Stopwatch.StartNew();
            _bitacora.Trace($"======================================= Inicio de la petición con Trace: {traceId} =======================================");
            var requestBody = await LeerRequestAsync(context.Request);
            var originalResponseBody = context.Response.Body;

            await using var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            try
            {
                _bitacora.Trace(
                    "Solicitud entrante. Metodo: {Metodo}. Ruta: {Ruta}. Query: {Query}. Body: {Body}",
                    context.Request.Method,
                    context.Request.Path,
                    context.Request.QueryString.HasValue ? context.Request.QueryString.Value : string.Empty,
                    requestBody);

                await _next(context);

                var responseBody = await LeerResponseAsync(context.Response);

                stopwatch.Stop();

                _bitacora.Trace(
                    "Respuesta saliente. Metodo: {Metodo}. Ruta: {Ruta}. EstadoHTTP: {Estado}. DuracionMs: {Duracion}. Body: {Body}",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    stopwatch.ElapsedMilliseconds,
                    responseBody);

                responseBodyStream.Position = 0;
                await responseBodyStream.CopyToAsync(originalResponseBody);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                _bitacora.Error(
                    ex,
                    "Error durante el procesamiento HTTP. Metodo: {Metodo}. Ruta: {Ruta}. DuracionMs: {Duracion}",
                    context.Request.Method,
                    context.Request.Path,
                    stopwatch.ElapsedMilliseconds);

                throw;
            }
            finally
            {
                _bitacora.Trace($"======================================= Fin de la petición con Trace: {traceId} =======================================");
                context.Response.Body = originalResponseBody;
            }
        }

        /// <summary>
        /// Metodo que lee el resquest asincrono
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static async Task<string> LeerRequestAsync(HttpRequest request)
        {
            request.EnableBuffering();

            if (request.ContentLength is null or 0)
                return string.Empty;

            request.Body.Position = 0;

            using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();

            request.Body.Position = 0;

            return SanitizarBody(body);
        }

        /// <summary>
        /// Metodo que lee el response
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static async Task<string> LeerResponseAsync(HttpResponse response)
        {
            response.Body.Position = 0;

            using var reader = new StreamReader(response.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();

            response.Body.Position = 0;

            return SanitizarBody(body);
        }

        /// <summary>
        /// Metodo que saniliza el contenido y lo trunca si es necesario
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        private static string SanitizarBody(string body)
        {
            if (string.IsNullOrWhiteSpace(body))
                return string.Empty;

            var sanitized = body;

            if (sanitized.Length > MaxBodyLength)
                sanitized = sanitized[..MaxBodyLength] + "...[TRUNCADO]";

            sanitized = UtilsHelper.FormatearJsonSiAplica(sanitized);

            return sanitized;
        }
    }
}