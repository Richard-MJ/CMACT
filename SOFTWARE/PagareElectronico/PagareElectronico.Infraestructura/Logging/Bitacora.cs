using Microsoft.Extensions.Logging;

namespace PagareElectronico.Infraestructura.Logging
{
    /// <summary>
    /// Implementación de bitácora basada en ILogger.
    /// </summary>
    /// <typeparam name="T">Tipo asociado al origen del log.</typeparam>
    public sealed class Bitacora<T> : IBitacora<T> where T : class
    {
        private readonly ILogger<T> _logger;

        public Bitacora(ILogger<T> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Debug(string mensaje, params object[] argumentos)
        {
            _logger.LogDebug(mensaje, argumentos);
        }

        public void Info(string mensaje, params object[] argumentos)
        {
            _logger.LogInformation(mensaje, argumentos);
        }
        public void Trace(string mensaje, params object[] argumentos)
        {
            _logger.LogTrace(mensaje, argumentos);
        }

        public void Warn(string mensaje, params object[] argumentos)
        {
            _logger.LogWarning(mensaje, argumentos);
        }

        public void Error(string mensaje, params object[] argumentos)
        {
            _logger.LogError(mensaje, argumentos);
        }

        public void Error(Exception excepcion, string mensaje, params object[] argumentos)
        {
            _logger.LogError(excepcion, mensaje, argumentos);
        }

        public void Fatal(string mensaje, params object[] argumentos)
        {
            _logger.LogCritical(mensaje, argumentos);
        }

        public void Fatal(Exception excepcion, string mensaje, params object[] argumentos)
        {
            _logger.LogCritical(excepcion, mensaje, argumentos);
        }
    }
}