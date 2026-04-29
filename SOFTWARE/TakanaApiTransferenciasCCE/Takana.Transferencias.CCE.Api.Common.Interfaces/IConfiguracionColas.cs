namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    /// <summary>
    /// Interfaz del configuracion de servicio SFTP
    /// </summary>
    public interface IConfiguracionColas
    {
        /// <summary>
        /// Ip del servicio Colas
        /// </summary>
        public string Ip { get; }
        /// <summary>
        /// Usuario del servicio Colas
        /// </summary>
        public string Usuario { get; }
        /// <summary>
        /// Password del servicio Colas
        /// </summary>
        public string Password { get; }
    }
}
