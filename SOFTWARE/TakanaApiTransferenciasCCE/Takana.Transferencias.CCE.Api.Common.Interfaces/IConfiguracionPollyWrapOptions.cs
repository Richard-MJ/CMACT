namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    /// <summary>
    /// Interfaz del configuracion de servicio SFTP
    /// </summary>
    public interface IConfiguracionPollyWrapOptions
    {
        /// <summary>
        /// Tiempo de espera en segundos
        /// </summary>
        public int TimeOut { get; }
        /// <summary>
        /// numero de reintentos
        /// </summary>
        public int NumberRetries { get; }
        /// <summary>
        /// Espera entre reintentos en segundos
        /// </summary>
        public decimal RetryWaitSeconds { get; }
        /// <summary>
        /// Circuit Breaker en segundos
        /// </summary>
        public int CircuitBreakSeconds { get; }
    }
}
