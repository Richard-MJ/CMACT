using Takana.Transferencias.CCE.Api.Loggin.Interfaz;

namespace Takana.Transferencias.CCE.Api.Infraestructura.Contenedor
{
    /// <summary>
    /// Implementación concreta de la configuración para conectarse a Redis,
    /// utilizada para inicializar Hangfire con RedisStorage.
    /// </summary>
    public class ConfiguracionBaseDatosRedis : IConfiguracionBaseDatosRedis
    {
        /// <summary>
        /// Cadena del host Redis, por ejemplo: "localhost:6379" o "localhost:6379,password=123".
        /// </summary>
        public string CadenaConexion { get; set; } = string.Empty;

        /// <summary>
        /// Número de base lógica de Redis (0–15).
        /// </summary>
        public int BaseLogica { get; set; }

        /// <summary>
        /// Prefijo para todas las keys creadas por Hangfire en Redis,
        /// por ejemplo: "hangfire:".
        /// </summary>
        public string PrefixHangFire { get; set; } = string.Empty;
    }
}