namespace Takana.Transferencias.CCE.Api.Infraestructura.Contenedor
{
    /// <summary>
    /// Configuración del consul
    /// </summary>
    public class ConfiguracionConsul
    {
        /// <summary>
        /// IP del Servidor Consul para el descubrimiento
        /// </summary>
        public string Ip { get; set; }
        public ConfigurarServicio Servicio { get; set; }
    }

    /// <summary>
    /// Configuracion del servicio
    /// </summary>
    public class ConfigurarServicio
    {
        /// <summary>
        /// Nombre del Servicio
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Esquema del servidor (http o https)
        /// </summary>
        public string Esquema { get; set; }
        /// <summary>
        /// IP en el que se despliega la instancia del api
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// Puerto en el que se despliega la instancia del api
        /// </summary>
        public int Puerto { get; set; }
        /// <summary>
        /// Tiempo de intervalo para la verificación del estado del servicio
        /// </summary>
        public int TiempoIntervalo { get; set; }
    }
}
