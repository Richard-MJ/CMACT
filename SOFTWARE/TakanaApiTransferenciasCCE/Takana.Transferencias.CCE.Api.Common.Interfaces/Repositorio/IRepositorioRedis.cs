namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    /// <summary>
    /// Contrato base para operaciones con Redis.
    /// </summary>
    public interface IRepositorioRedis
    {
        /// <summary>
        /// Guarda un valor en Redis serializado en JSON.
        /// </summary>
        Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);

        /// <summary>
        /// Obtiene un valor desde Redis y lo deserializa.
        /// </summary>
        Task<T?> GetAsync<T>(string key);

        /// <summary>
        /// Incrementa un contador en Redis.
        /// Usado para control de reintentos.
        /// </summary>
        Task<long> IncrementarAsync(string key, TimeSpan? expiry = null);

        /// <summary>
        /// Elimina una clave de Redis.
        /// </summary>
        Task EliminarAsync(string key);
        /// <summary>
        /// Obtiene el tiempo de vida restante (TTL) de una clave.
        /// </summary>
        Task<TimeSpan?> ObtenerTiempoVidaAsync(string key);
    }
}
