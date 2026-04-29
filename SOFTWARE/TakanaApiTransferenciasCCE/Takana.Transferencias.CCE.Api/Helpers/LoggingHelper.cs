using NLog;
using System.Text;
using System.Reflection;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;
using Takana.Transferencias.CCE.Api.Infraestructura.Contenedor;

namespace Takana.Transferencias.CCE.Api.Helpers
{
    public static class LoggingHelper
    {
        private const string ConfigFileName = "nlog.config";
        private const string TokenVariable = "TAK_BITACORA_SERVIDOR";

        /// <summary>
        /// Configura NLog e inyecta propiedades de contexto
        /// </summary>
        public static Logger ConfigurarNLog()
        {
            ReemplazarTokenEnArchivoNLog();

            string rutaConfig = ObtenerRutaConfig();

            var logger = LogManager.Setup().LoadConfigurationFromFile(rutaConfig).GetCurrentClassLogger();

            var contexto = new ContextoSistema();
            logger.WithProperty("idSesion", contexto.IdSesion)
                  .WithProperty("codigoUsuario", contexto.CodigoUsuario)
                  .WithProperty("codigoAgencia", contexto.CodigoAgencia)
                  .WithProperty("indicadorCanal", contexto.IndicadorCanal)
                  .WithProperty("indicadorSubCanal", contexto.IndicadorSubCanal)
                  .WithProperty("idTerminalCliente", contexto.IdTerminalOrigen)
                  .WithProperty("fechaEvento", DateTime.Now.ToString("O"))
                  .WithProperty("idServicio", contexto.IdServicio);

            return logger;
        }

        /// <summary>
        /// Establece NLog como proveedor de logging
        /// </summary>
        public static void EstablecerNLog(ILoggingBuilder logging)
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(LogLevel.Trace);
        }

        /// <summary>
        /// Métdo que remplaza el roken en el archivo Nlog
        /// </summary>
        /// <exception cref="FileNotFoundException"></exception>
        private static void ReemplazarTokenEnArchivoNLog()
        {
            try
            {
                var rutaConfig = ObtenerRutaConfig();

                if (!File.Exists(rutaConfig))
                    throw new FileNotFoundException($"No se encontró el archivo NLog: {rutaConfig}");

                string contenido = File.ReadAllText(rutaConfig, Encoding.UTF8);

                if (!contenido.Contains(TokenVariable)) return;

                var valor = Environment.GetEnvironmentVariable(TokenVariable);

                if (string.IsNullOrWhiteSpace(valor))
                {
                    Console.WriteLine($"[WARNING] La variable '{TokenVariable}' no tiene valor.");
                    return;
                }

                contenido = contenido.Replace(TokenVariable, valor);
                File.WriteAllText(rutaConfig, contenido, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Reemplazo de token en NLog falló: {ex.Message}");
            }
        }

        /// <summary>
        /// Método que obtiene la ruta del config
        /// </summary>
        /// <returns></returns>
        private static string ObtenerRutaConfig()
        {
            var basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? AppContext.BaseDirectory;
            return Path.Combine(basePath, ConfigFileName);
        }
    }
}
