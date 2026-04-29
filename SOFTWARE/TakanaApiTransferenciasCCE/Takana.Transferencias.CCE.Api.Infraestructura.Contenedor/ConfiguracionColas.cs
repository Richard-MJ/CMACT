using Takana.Transferencias.CCE.Api.Common.Interfaz;

namespace Takana.Transferencias.CCE.Api.Infraestructura.Contenedor
{
    /// <summary>
    /// Configuraciones del servicio Colas para Rabbit
    /// </summary>
    public class ConfiguracionColas : IConfiguracionColas
    {
        /// <summary>
        /// Ip del servicio Colas
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// Usuario del servicio Colas
        /// </summary>
        public string Usuario { get; set; }
        /// <summary>
        /// Password del servicio Colas
        /// </summary>
        public string Password { get; set; }
    }
}
