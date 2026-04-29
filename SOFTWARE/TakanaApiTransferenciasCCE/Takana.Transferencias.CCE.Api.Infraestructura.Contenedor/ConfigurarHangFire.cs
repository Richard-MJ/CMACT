using Hangfire;
using Hangfire.Redis.StackExchange;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Takana.Transferencias.CCE.Api.Infraestructura.Contenedor
{
    public static class ConfigurarHangFire
    {
        private const int ReintentoCortoSegundos = 5;
        private const int ReintentoMedioSegundos = 15;
        private const int ReintentoLargoSegundos = 30;
        private static readonly TimeSpan TiempoInvisibilidadTrabajo = TimeSpan.FromMinutes(5);
        private static readonly TimeSpan TiempoEsperaObtencionTrabajo = TimeSpan.FromSeconds(60);
        private static readonly TimeSpan IntervaloRevisionTrabajosExpirados = TimeSpan.FromSeconds(60);
        private const int MaximoTrabajosExitosos = 5000;
        private const int MaximoTrabajosEliminados = 5000;

        /// <summary>
        /// Método que agrega la configuración de Hangfire usando Redis.
        /// </summary>
        public static IServiceCollection AddConfiguracinHangFire(
            this IServiceCollection servicios, IConfiguration configuracion)
        {
            var redisConfig = configuracion.GetSection("BaseDatosConexionRedis")
                                           .Get<ConfiguracionBaseDatosRedis>()!;

            var opcionesRedis = new RedisStorageOptions
            {
                Prefix = redisConfig.PrefixHangFire,
                Db = redisConfig.BaseLogica,
                InvisibilityTimeout = TiempoInvisibilidadTrabajo,
                FetchTimeout = TiempoEsperaObtencionTrabajo,
                ExpiryCheckInterval = IntervaloRevisionTrabajosExpirados,
                SucceededListSize = MaximoTrabajosExitosos,
                DeletedListSize = MaximoTrabajosEliminados
            };

            servicios.AddHangfire(config =>
            {
                config.UseRedisStorage(redisConfig.CadenaConexion, opcionesRedis);
                config.UseRecommendedSerializerSettings();
                config.UseSimpleAssemblyNameTypeSerializer();
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);

                config.UseFilter(new AutomaticRetryAttribute
                {
                    Attempts = 3,
                    DelaysInSeconds = new[]
                    {
                        ReintentoCortoSegundos,
                        ReintentoMedioSegundos,
                        ReintentoLargoSegundos
                    }
                });
            });

            servicios.AddHangfireServer(options =>
            {
                options.Queues = new[] { "default" };
                options.WorkerCount = Environment.ProcessorCount * 5;
            });

            return servicios;
        }
    }
}