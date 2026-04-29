using StackExchange.Redis;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Takana.Transferencia.CCE.Api.Loggin.Nlog;
using Takana.Transferencias.CCE.Api.Datos.Contexto;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using Takana.Transferencias.CCE.Api.Dominio.Servicios;
using Takana.Transferencias.CCE.Api.Datos.Repositorios;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Dominio;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Servicios;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.ServiciosExternos;

namespace Takana.Transferencias.CCE.Api.Infraestructura.Contenedor
{
    public static class ConfigurarIoC
    {
        /// <summary>
        /// Método que configura las agregaciones de infraestructura
        /// </summary>
        /// <param name="servicios"></param>
        /// <returns>Retorna la collecion de servicios</returns>
        public static IServiceCollection AddConfiguracionInfraestructura(this IServiceCollection servicios)
        {
            #region Contexto, bitacora y Repositorios
            servicios.AddScoped<ContextoSistema>();
            servicios.AddDbContext<ContextoGeneral>();
            servicios.AddDbContext<ContextoEscritura>();
            servicios.AddDbContext<ContextoOperacion>();
            servicios.AddScoped<IRepositorioGeneral, RepositorioGeneral>();
            servicios.AddScoped<IRepositorioEscritura, RepositorioEscritura>();
            servicios.AddScoped(typeof(IBitacora<>), typeof(NLogProxy<>));
            servicios.AddScoped<IRepositorioOperacion, RepositorioOperacion>();
            servicios.AddScoped<IContextoBitacora>(x => x.GetRequiredService<ContextoSistema>());
            servicios.AddScoped<IContextoAplicacion>(x => x.GetRequiredService<ContextoSistema>());
            servicios.AddScoped<IConfiguracionColas>(x => x.GetRequiredService<IOptions<ConfiguracionColas>>().Value);
            servicios.AddScoped<IConfiguracionBaseDatosSAF>(x => x.GetRequiredService<IOptions<ConfiguracionBaseDatosSAF>>().Value);
            servicios.AddScoped<IConfiguracionBaseDatosRedis>(x => x.GetRequiredService<IOptions<ConfiguracionBaseDatosRedis>>().Value);
            servicios.AddScoped<IConfiguracionReporteSFTP>(x => x.GetRequiredService<IOptions<ConfiguracionReporteSFTP>>().Value);
            servicios.AddScoped<IConfiguracionDirectorioSFTP>(x => x.GetRequiredService<IOptions<ConfiguracionDirectorioSFTP>>().Value);
            servicios.AddScoped<IConfiguracionPollyWrapOptions>(x => x.GetRequiredService<IOptions<ConfiguracionPollyWrapOptions>>().Value);            
            servicios.AddScoped<IConfiguracionCanalElectronicoWorkstation>(x => x.GetRequiredService<IOptions<ConfiguracionCanalElectronicoWorkstation>>().Value);
            servicios.AddSingleton<IConnectionMultiplexer>(config =>
            {
                var configuracion = config.GetRequiredService<IConfiguracionBaseDatosRedis>();
                var options = ConfigurationOptions.Parse(configuracion.CadenaConexion);
                options.AbortOnConnectFail = false;
                return ConnectionMultiplexer.Connect(options);
            });
            servicios.AddSingleton<IRepositorioRedis, RepositorioRedis>();
            #endregion

            #region Generales
            servicios.AddScoped<IServicioAplicacionTransaccionOperacion, ServicioAplicacionTransaccionOperacion>();
            servicios.AddScoped<IServicioAplicacionTransferenciaSalida, ServicioAplicacionTransferenciaSalida>();
            servicios.AddScoped<IServicioAplicacionTransferenciaEntrada, ServicioAplicacionTransferenciaEntrada>();
            servicios.AddScoped<IServicioDominioContabilidad, ServicioDominioContabilidad>();
            servicios.AddScoped<IServicioDominioProducto, ServicioDominioProducto>();          
            servicios.AddScoped<IServicioDominioLavado, ServicioDominioLavado>();
            servicios.AddScoped<IServicioAplicacionPeticionBase, ServicioAplicacionPeticionBase>();
            servicios.AddScoped<IServicioAplicacionSeguridad, ServicioAplicacionSeguridad>();
            servicios.AddTransient<IServicioAplicacionPeticion, ServicioAplicacionPeticion>();

            servicios.AddSingleton<IRabbitMqConnectionManager, RabbitMqConnectionManager>();
            servicios.AddScoped<IServicioAplicacionColas, ServicioAplicacionColas>();

            servicios.AddSingleton<IServicioAplicacionSFTP, ServicioAplicacionSFTP>();
            #endregion

            #region Transferencias Inmediata
            servicios.AddScoped<IServicioAplicacionTransferenciaSalida, ServicioAplicacionTransferenciaSalida>();
            servicios.AddScoped<IServicioAplicacionParametroGeneral, ServicioAplicacionParametroGeneral>();
            servicios.AddScoped<IServicioDominioCuenta, ServicioDominioCuenta>();
            servicios.AddTransient<IServicioAplicacionBitacora, ServicioAplicacionBitacora>();
            servicios.AddScoped<IServicioAplicacionCliente, ServicioAplicacionCliente>();
            servicios.AddScoped<IServicioAplicacionTransferenciaEntrada, ServicioAplicacionTransferenciaEntrada>();
            servicios.AddScoped<IServicioDominioTransaccionOperacion, ServicioDominioTransaccionOperacion>();
            servicios.AddScoped<IServicioAplicacionValidacion, ServicioAplicacionValidacion>();
            servicios.AddScoped<IServicioAplicacionCajero, ServicioAplicacionCajero>();
            servicios.AddScoped<IServicioAplicacionProducto, ServicioAplicacionProducto>();
            #endregion

            #region Interoperabilidad
            servicios.AddScoped<IServicioAplicacionInteroperabilidad, ServicioAplicacionInteroperabilidad>();
            servicios.AddScoped<IServicioAplicacionLavado, ServicioAplicacionLavado>(); 
            servicios.AddScoped<IServicioAplicacionCliente, ServicioAplicacionCliente>();
            servicios.AddScoped<IServicioDominioAfiliacion, ServicioDominioAfiliacion>(); 
            servicios.AddScoped<IServicioAplicacionAfiliacion, ServicioAplicacionAfiliacion>();
            servicios.AddScoped<IServicioAplicacionNotificaciones, ServicioAplicacionNotificaciones>();
            #endregion

            #region Reportes
            servicios.AddScoped<IServicioAplicacionReporte, ServicioAplicacionReporte>();
            #endregion

            return servicios;
        }
    }
}
