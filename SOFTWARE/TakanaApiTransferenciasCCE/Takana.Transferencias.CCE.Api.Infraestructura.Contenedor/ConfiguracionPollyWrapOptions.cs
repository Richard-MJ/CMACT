using Takana.Transferencias.CCE.Api.Common.Interfaz;

namespace Takana.Transferencias.CCE.Api.Infraestructura.Contenedor
{
    public class ConfiguracionPollyWrapOptions : IConfiguracionPollyWrapOptions
    {
        /// <summary>
        /// Tiempo de espera en segundos
        /// </summary>
        public int TimeOut { get; set; }
        /// <summary>
        /// numero de reintentos
        /// </summary>
        public int NumberRetries { get; set; }
        /// <summary>
        /// Espera entre reintentos en segundos
        /// </summary>
        public decimal RetryWaitSeconds { get; set; }
        /// <summary>
        /// Circuit Breaker en segundos
        /// </summary>
        public int CircuitBreakSeconds { get; set; }
    }
}
