using Takana.Transferencias.CCE.Api.Common.Interfaz;

namespace Takana.Transferencias.CCE.Api.Infraestructura.Contenedor
{   
    /// <summary>
    /// Configuraciones del servicio SFTP para REPORTE
    /// </summary>
    public class ConfiguracionReporteSFTP : IConfiguracionReporteSFTP
    {
        /// <summary>
        /// Ip del servicio SFTP
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// Usuario del servicio SFTP
        /// </summary>
        public string Usuario { get; set; }
        /// <summary>
        /// Password del servicio SFTP
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Password del servicio SFTP
        /// </summary>
        public string RutaDestino { get; set; }
        /// <summary>
        /// Puerto del servicio SFTP
        /// </summary>
        public int Puerto { get; set; }
        /// <summary>
        /// llave del servicio SFTP
        /// </summary>
        public string Llave { get; set; }
    }
}
