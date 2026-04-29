using Consul;
using Microsoft.Extensions.Options;
using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Infraestructura.Contenedor;

namespace Takana.Transferencias.CCE.Api.Extensions
{  
    public static class ConsulServiceExtension
    {
        /// <summary>
        /// Agrega la configuración de cliente Consul al contenedor de servicios
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IServiceCollection AddConsul(this IServiceCollection services)
        {
            services.AddSingleton<IConsulClient>(provider =>
            {
                var config = provider.GetRequiredService<IOptions<ConfiguracionConsul>>().Value;

                if (string.IsNullOrWhiteSpace(config.Ip))
                    throw new ArgumentException("La IP de Consul no está configurada.");

                return new ConsulClient(c => c.Address = new Uri(config.Ip));
            });

            return services;
        }

        /// <summary>
        /// Registra el servicio en Consul y lo elimina al apagar la aplicación
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
        {
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var config = app.ApplicationServices.GetRequiredService<IOptions<ConfiguracionConsul>>()?.Value;

            if (config?.Servicio == null || string.IsNullOrWhiteSpace(config.Ip))
                return app;

            var servicio = config.Servicio;

            var serviceId = $"{servicio.Nombre}@{servicio.Ip}:{servicio.Puerto}";
            var healthCheckUrl = $"{servicio.Esquema}://{servicio.Ip}:{servicio.Puerto}{ConfigApi.RutaHealthy}";

            var registro = new AgentServiceRegistration
            {
                ID = serviceId,
                Name = servicio.Nombre,
                Address = servicio.Ip,
                Port = servicio.Puerto,
                Check = new AgentCheckRegistration
                {
                    HTTP = healthCheckUrl,
                    Notes = $"Servicio {servicio.Nombre} registrado correctamente.",
                    Interval = TimeSpan.FromSeconds(servicio.TiempoIntervalo),
                    TLSSkipVerify = true
                }
            };

            lifetime.ApplicationStarted.Register(async () =>
            {
                try
                {
                    await consulClient.Agent.ServiceDeregister(registro.ID).ConfigureAwait(false);
                    await consulClient.Agent.ServiceRegister(registro).ConfigureAwait(false);
                }
                catch (Exception excepcion)
                {
                    Console.WriteLine($"Error registrando servicio en Consul: {excepcion.Message}");
                }
            });

            lifetime.ApplicationStopping.Register(async () =>
            {
                try
                {
                    await consulClient.Agent.ServiceDeregister(registro.ID).ConfigureAwait(false);
                }
                catch (Exception excepcion)
                {
                    Console.WriteLine($"Error al deregistrar servicio de Consul: {excepcion.Message}");
                }
            });

            return app;
        }
    }
}
