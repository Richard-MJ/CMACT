using System.Diagnostics;
using PagareElectronico.Aplicacion.Helper;
using PagareElectronico.Infraestructura.Logging;

namespace PagareElectronico.Infraestructura.Http
{
    public sealed class ThirdPartyLoggingHandler : DelegatingHandler
    {
        private readonly IBitacora<ThirdPartyLoggingHandler> _bitacora;
        private const int MaxBodyLength = 5000;

        public ThirdPartyLoggingHandler(IBitacora<ThirdPartyLoggingHandler> bitacora)
        {
            _bitacora = bitacora;
        }

        /// <summary>
        /// Metodo que bitacoriza el envió
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            var requestBody = request.Content is null
                ? string.Empty
                : await request.Content.ReadAsStringAsync(cancellationToken);

            var requestBodyFormateado = UtilsHelper.FormatearJsonSiAplica(requestBody);

            _bitacora.Trace("Solicitud al tercero. Metodo: {Metodo}. Url: {Url}. Body: {Body}",
                request.Method,
                request.RequestUri!,
                Truncar(requestBodyFormateado));

            var response = await base.SendAsync(request, cancellationToken);

            var responseBody = response.Content is null
                ? string.Empty
                : await response.Content.ReadAsStringAsync(cancellationToken);

            var responseBodyFormateado = UtilsHelper.FormatearJsonSiAplica(responseBody);

            stopwatch.Stop();

            _bitacora.Trace(
                "Respuesta del tercero. Metodo: {Metodo}. Url: {Url}. EstadoHTTP: {Estado}. DuracionMs: {Duracion}. Body: {Body}",
                request.Method,
                request.RequestUri!,
                (int)response.StatusCode,
                stopwatch.ElapsedMilliseconds,
                Truncar(responseBodyFormateado));

            return response;
        }

        /// <summary>
        /// Metodo que Trunca el contenido
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        private static string Truncar(string body)
        {
            if (string.IsNullOrWhiteSpace(body))
                return string.Empty;

            return body.Length <= MaxBodyLength
                ? body
                : body[..MaxBodyLength] + "...[TRUNCADO]";
        }
    }
}