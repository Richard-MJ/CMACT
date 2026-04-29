using System.Text.Json;
using StackExchange.Redis;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;

namespace Takana.Transferencias.CCE.Api.Datos.Repositorios
{
    /// <summary>
    /// Contexto base para operaciones con Redis.
    /// Centraliza la conexión, manejo de claves y control de errores
    /// utilizando Bitácora institucional.
    /// </summary>
    public class RepositorioRedis : IRepositorioRedis
    {
        /// <summary>
        /// Instancia de la base lógica de Redis.
        /// </summary>
        private readonly IDatabase _baseDeDatos;

        /// <summary>
        /// Configuración de la conexión Redis.
        /// </summary>
        private readonly IConfiguracionBaseDatosRedis _configuracion;

        /// <summary>
        /// Instancia de bitácora para el contexto Redis.
        /// </summary>
        private readonly IBitacora<RepositorioRedis> _bitacora;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="RepositorioRedis"/>.
        /// </summary>
        /// <param name="multiplexer">Multiplexor de conexión Redis.</param>
        /// <param name="configuracion">Configuración base de Redis.</param>
        /// <param name="bitacora">Bitácora para auditoría y errores.</param>
        public RepositorioRedis(
            IConnectionMultiplexer multiplexer,
            IConfiguracionBaseDatosRedis configuracion,
            IBitacora<RepositorioRedis> bitacora)
        {
            _bitacora = bitacora;
            _configuracion = configuracion;
            _baseDeDatos = multiplexer.GetDatabase(_configuracion.BaseLogica);
        }

        /// <summary>
        /// Construye la clave final aplicando el prefijo configurado.
        /// </summary>
        /// <param name="key">Clave base.</param>
        /// <returns>Clave con prefijo.</returns>
        private string Key(string key) => $"{key}";

        /// <summary>
        /// Guarda un valor en Redis serializado en formato JSON.
        /// </summary>
        /// <typeparam name="T">Tipo del objeto a guardar.</typeparam>
        /// <param name="key">Clave Redis.</param>
        /// <param name="obj">Objeto a almacenar.</param>
        /// <param name="expiry">Tiempo de expiración (opcional).</param>
        public async Task SetAsync<T>(string key, T obj, TimeSpan? expiry = null)
        {
            try
            {
                var json = JsonSerializer.Serialize(obj);
                await _baseDeDatos.StringSetAsync(Key(key), json, expiry);
            }
            catch (Exception ex)
            {
                _bitacora.Error(
                    "Error al guardar valor en Redis. Clave: {key}. Mensaje: {mensaje}",
                    key, ex.Message);

                throw new Exception($"Error al guardar valor en Redis. Clave: {key}", ex);
            }
        }

        /// <summary>
        /// Obtiene un valor almacenado en Redis y lo deserializa.
        /// </summary>
        /// <typeparam name="T">Tipo esperado.</typeparam>
        /// <param name="key">Clave Redis.</param>
        /// <returns>Objeto deserializado o null si no existe.</returns>
        public async Task<T?> GetAsync<T>(string key)
        {
            try
            {
                var val = await _baseDeDatos.StringGetAsync(Key(key));
                return val.HasValue ? JsonSerializer.Deserialize<T>(val!) : default;
            }
            catch (Exception ex)
            {
                _bitacora.Error(
                    "Error al obtener valor de Redis. Clave: {key}. Mensaje: {mensaje}",
                    key, ex.Message);

                throw new Exception($"Error al obtener valor de Redis. Clave: {key}", ex);
            }
        }

        /// <summary>
        /// Incrementa en uno el valor de una clave (contador).
        /// Ideal para control de reintentos.
        /// </summary>
        /// <param name="key">Clave del contador.</param>
        /// <param name="expiry">Tiempo de expiración (opcional).</param>
        /// <returns>Valor actualizado del contador.</returns>
        public async Task<long> IncrementarAsync(string key, TimeSpan? expiry = null)
        {
            try
            {
                var redisKey = Key(key);
                var valor = await _baseDeDatos.StringIncrementAsync(redisKey);

                if (expiry.HasValue)
                {
                    await _baseDeDatos.KeyExpireAsync(redisKey, expiry);
                }

                return valor;
            }
            catch (Exception ex)
            {
                _bitacora.Error(
                    "Error al incrementar contador en Redis. Clave: {key}. Mensaje: {mensaje}",
                    key, ex.Message);

                throw new Exception($"Error al incrementar contador Redis. Clave: {key}", ex);
            }
        }

        /// <summary>
        /// Elimina una clave de Redis.
        /// </summary>
        /// <param name="key">Clave a eliminar.</param>
        public async Task EliminarAsync(string key)
        {
            try
            {
                await _baseDeDatos.KeyDeleteAsync(Key(key));
            }
            catch (Exception ex)
            {
                _bitacora.Error(
                    "Error al eliminar clave Redis. Clave: {key}. Mensaje: {mensaje}",
                    key, ex.Message);

                throw new Exception($"Error al eliminar clave Redis. Clave: {key}", ex);
            }
        }

        /// <summary>
        /// Obtiene el tiempo de vida restante (TTL) de una clave en Redis.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<TimeSpan?> ObtenerTiempoVidaAsync(string key)
        {
            try
            {
                return await _baseDeDatos.KeyTimeToLiveAsync(Key(key));
            }
            catch (Exception ex)
            {
                _bitacora.Error(
                    "Error al obtener TTL de Redis. Clave: {key}. Mensaje: {mensaje}",
                    key, ex.Message);
                throw;
            }
        }
    }
}