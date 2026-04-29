using Microsoft.Extensions.DependencyInjection;

namespace Takana.Transferencias.CCE.Api.Infraestructura.Contenedor
{
    public static class ConfigurarBitacora
    {
        /// <summary>
        /// Método que agrega la configuracion de bitacora
        /// </summary>
        /// <param name="services"></param>
        /// <param name="logger"></param>
        /// <returns>Retorna la coleccion del servicio</returns>
        public static IServiceCollection AddConfiguracionBitacora(
            this IServiceCollection services, NLog.Logger logger)
        {
            ContextoSistema contexto = new ContextoSistema();
            logger.WithProperty("idSesion", contexto.IdSesion)
                  .WithProperty("codigoUsuario", contexto.CodigoUsuario)
                  .WithProperty("codigoAgencia", contexto.CodigoAgencia)
                  .WithProperty("indicadorCanal", contexto.IndicadorCanal)
                  .WithProperty("indicadorSubCanal", contexto.IndicadorSubCanal)
                  .WithProperty("idTerminalCliente", contexto.IdTerminalOrigen)
                  .WithProperty("fechaEvento", DateTime.Now.ToString("O"))
                  .WithProperty("idServicio", contexto.IdServicio)                 
                  .Info($"Inicio del servicio: Servicio TransferenciasInmediatasCCE");

            return services;
        }

    }
}