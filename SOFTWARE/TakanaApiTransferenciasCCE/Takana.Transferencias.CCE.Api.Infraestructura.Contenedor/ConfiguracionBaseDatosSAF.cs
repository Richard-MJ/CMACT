using Takana.Transferencias.CCE.Api.Loggin.Interfaz;

namespace Takana.Transferencias.CCE.Api.Infraestructura.Contenedor
{
    /// <summary>
    /// Configuraciones de la base de datos 
    /// </summary>
    public class ConfiguracionBaseDatosSAF : IConfiguracionBaseDatosSAF
    {
        /// <summary>
        /// Servidor de la base de datos
        /// </summary>
        public string Servidor { get; set; }
        /// <summary>
        /// Nombre de la base de datos
        /// </summary>
        public string Catalogo { get; set; }
        /// <summary>
        /// Usuario de la base de datos
        /// </summary>
        public string Usuario { get; set; }
        /// <summary>
        /// Password de la base de datos
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// True o False si utilizada certificados de autentificacion
        /// </summary>
        public bool Certificado { get; set; }
        /// <summary>
        /// True o False si utilizara seguridad integrada
        /// </summary>
        public bool SeguridadIntegrada { get; set; }
    }
}