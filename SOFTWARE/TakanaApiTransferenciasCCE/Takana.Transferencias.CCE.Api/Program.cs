using NLog.Web;
using Microsoft.AspNetCore;
using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Helpers;

namespace Takana.Transferencias.CCE.Api
{
    public class Program
    {
        /// <summary>
        /// Método que inicia la configuracion del servicio web
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Retorna valor byte</returns>
        public static int Main(string[] args)
        {
            var logger = LoggingHelper.ConfigurarNLog();

            var configuration = GetConfiguration();
            try
            {
                logger.Info($"Configurando web host servicio: {ConfigApi.Nombre} {ConfigApi.Version}");
                var host = BuildWebHost(configuration, args);

                logger.Info($"Inicializando web host servicio: {ConfigApi.Nombre} {ConfigApi.Version}");
                host.Run();

                logger.Info($"Inicio del web host servicio: {ConfigApi.Nombre} {ConfigApi.Version}");
                return 0;
            }
            catch (Exception excepcion)
            {
                logger.Warn(excepcion, $"Aplicacion terminada inesperadamente: {ConfigApi.Nombre} {ConfigApi.Version}");
                return 1;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        /// <summary>
        /// Método que construye el servidor web
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="args"></param>
        /// <returns>Retorna el servicio construido</returns>
        private static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(false)
                .UseStartup<Startup>()
                .ConfigureLogging(logging => LoggingHelper.EstablecerNLog(logging)).UseNLog()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(configuration)
                .Build();

        /// <summary>
        /// Método que configura la localizacion el archivo de configuracion
        /// </summary>
        /// <returns></returns>
        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            return config;
        }
    }
}