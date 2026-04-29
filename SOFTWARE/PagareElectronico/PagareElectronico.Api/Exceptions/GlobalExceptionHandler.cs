using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using PagareElectronico.Application.Exceptions;
using PagareElectronico.Infraestructura.Logging;

namespace PagareElectronico.Api.Exceptions
{
    /// <summary>
    /// Manejador global de excepciones de la API.
    /// </summary>
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IBitacora<GlobalExceptionHandler> _bitacora;

        public GlobalExceptionHandler(IBitacora<GlobalExceptionHandler> bitacora)
        {
            _bitacora = bitacora;
        }

        /// <summary>
        /// Metodo que hace el tryhandle
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="exception"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var traceId = httpContext.TraceIdentifier;
            var (statusCode, title, codigo) = ObtenerDatosError(exception);

            RegistrarEnBitacora(exception, traceId, statusCode);

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            };

            problemDetails.Extensions["codigo"] = codigo;
            problemDetails.Extensions["mensaje"] = exception.Message;
            problemDetails.Extensions["traceId"] = traceId;

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }


        /// <summary>
        /// Metodo que obtiene el error
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static (int StatusCode, string Title, string Codigo) ObtenerDatosError(Exception exception)
        {
            return exception switch
            {
                ValidationException ex => (
                    StatusCodes.Status400BadRequest,
                    "Error de validación.",
                    ex.Code),

                NotFoundException ex => (
                    StatusCodes.Status404NotFound,
                    "Recurso no encontrado.",
                    ex.Code),

                ConflictException ex => (
                    StatusCodes.Status409Conflict,
                    "Conflicto de negocio.",
                    ex.Code),

                _ => (
                    StatusCodes.Status500InternalServerError,
                    "Error interno del servidor.",
                    "50000")
            };
        }

        /// <summary>
        /// Metodo que registra en la bitacora
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="traceId"></param>
        /// <param name="statusCode"></param>
        private void RegistrarEnBitacora(Exception exception, string traceId, int statusCode)
        {
            switch (exception)
            {
                case ValidationException ex:
                    _bitacora.Warn(
                        "Excepción controlada de validación. Código: {Codigo}. EstadoHTTP: {EstadoHTTP}. TraceId: {TraceId}. Mensaje: {Mensaje}",
                        ex.Code,
                        statusCode,
                        traceId,
                        ex.Message);
                    break;

                case NotFoundException ex:
                    _bitacora.Warn(
                        "Recurso no encontrado. Código: {Codigo}. EstadoHTTP: {EstadoHTTP}. TraceId: {TraceId}. Mensaje: {Mensaje}",
                        ex.Code,
                        statusCode,
                        traceId,
                        ex.Message);
                    break;

                case ConflictException ex:
                    _bitacora.Warn(
                        "Conflicto de negocio. Código: {Codigo}. EstadoHTTP: {EstadoHTTP}. TraceId: {TraceId}. Mensaje: {Mensaje}",
                        ex.Code,
                        statusCode,
                        traceId,
                        ex.Message);
                    break;

                default:
                    _bitacora.Fatal(
                        exception,
                        "Excepción no controlada. EstadoHTTP: {EstadoHTTP}. TraceId: {TraceId}. Mensaje: {Mensaje}",
                        statusCode,
                        traceId,
                        exception.Message);
                    break;
            }
        }
    }
}