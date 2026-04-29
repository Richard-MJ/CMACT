
namespace Takana.Transferencias.CCE.Api.Loggin.Interfaz
{
    /// <summary>
    /// Representa la configuración necesaria para conectarse al servidor Redis
    /// utilizado por Hangfire.
    /// </summary>
    public interface IConfiguracionBaseDatosRedis
    {
        /// <summary>
        /// Cadena de conexión del servidor Redis (host:puerto o host:puerto,password=xxx).
        /// </summary>
        string CadenaConexion { get; }
        /// <summary>
        /// Número de la base lógica de Redis (0–15) donde se almacenarán las keys de Hangfire.
        /// </summary>
        int BaseLogica { get; }
        /// <summary>
        /// Prefijo que se agregará a todas las keys almacenadas en Redis por Hangfire.
        /// </summary>
        string PrefixHangFire { get; }
    }

}