
namespace Takana.Transferencias.CCE.Api.Loggin.Interfaz
{
    public interface IConfiguracionBaseDatosSAF
    {
        /// <summary>
        /// Servidor de la base de datos
        /// </summary>
        public string Servidor { get;}
        /// <summary>
        /// Nombre de la base de datos
        /// </summary>
        public string Catalogo { get;}
        /// <summary>
        /// Usuario de la base de datos
        /// </summary>
        public string Usuario { get;}
        /// <summary>
        /// Password de la base de datos
        /// </summary>
        public string Password { get;}
        /// <summary>
        /// True o False si utilizada certificados de autentificacion
        /// </summary>
        public bool Certificado { get;}
        /// <summary>
        /// True o False si utilizara seguridad integrada
        /// </summary>
        public bool SeguridadIntegrada { get;}
    }
}