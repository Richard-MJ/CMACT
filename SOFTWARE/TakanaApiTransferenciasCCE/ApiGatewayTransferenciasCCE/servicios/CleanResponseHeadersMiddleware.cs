namespace ApiGatewayTransferenciasCCE.Servicios
{
    public class CleanResponseHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public CleanResponseHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                var headersToRemove = new[]
                {
                    "X-Rate-Limit-Limit",
                    "X-Rate-Limit-Remaining",
                    "X-Rate-Limit-Reset",
                    "api-supported-versions",
                    "Strict-Transport-Security",
                    "Transfer-Encoding",
                    "X-Powered-By",
                    "Server",
                    "Date",
                };

                foreach (var header in headersToRemove)
                {
                    context.Response.Headers.Remove(header);
                }

                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
