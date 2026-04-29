
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Takana.Transferencias.CCE.Api.Common.Interfaz;

namespace Takana.Transferencias.CCE.Api.Infraestructura.Contenedor
{
    public static class ConfigurarCargarContexto
    {
        /// <summary>
        /// Método que agrega la configuracion de carga de LOS certificados
        /// </summary>
        /// <param name="servicios"></param>
        /// <returns>Retorna la collecion del servicio</returns>
        public static IServiceCollection AddConfiguracionValidarCertificadoCCE(this IServiceCollection servicios, IConfiguration configuracion)
        {
            var serviceProvider = servicios.BuildServiceProvider();
            var servicioSeguridad = serviceProvider.GetService<IServicioAplicacionSeguridad>();

            var nombreCertificadoCMAC = configuracion["NOMBRE_CERTIFICADO_FIRMA_CCE"]!;
            var nombreCertificadoCCE = configuracion["NOMBRE_CERTIFICADO_FIRMA_CMACT"]!;
            servicioSeguridad!.ValidarCertificadoDigital(nombreCertificadoCMAC);
            servicioSeguridad!.ValidarCertificadoDigital(nombreCertificadoCCE);

            return servicios;
        }
    }
}