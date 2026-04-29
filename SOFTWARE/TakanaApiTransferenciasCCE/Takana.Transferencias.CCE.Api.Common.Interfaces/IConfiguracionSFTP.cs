namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    /// <summary>
    /// Interfaz del configuracion de servicio SFTP
    /// </summary>
    public interface IConfiguracionSFTP
    {
        /// <summary>
        /// Host del servicio
        /// </summary>
        public string Ip { get; }
        /// <summary>
        /// Usuario
        /// </summary>
        public string Usuario { get; }
        /// <summary>
        /// Password del servicio SFTP
        /// </summary>
        public string Password { get; }
        /// <summary>
        /// Password
        /// </summary>
        public string RutaDestino { get; }
        /// <summary>
        /// Puerto del servicio SFTP
        /// </summary>
        public int Puerto { get; }
        /// <summary>
        /// llave del servicio SFTP
        /// </summary>
        public string Llave { get; }
    }
}
