namespace Takana.Transferencias.CCE.Api.Loggin.Interfaz
{
    public interface IContextoBitacora
    {
        /// <summary>
        /// ID para identificar al evento como parte de un login de usuario/cliente.
        /// Si es 0 el evento no es generado a partir del login de un usuario/cliente.
        /// </summary>
        public string IdLogin { get; }
        /// <summary>
        /// ID para identificar al evento como parte de una transacción, se debe
        /// generar cuando la operación inicia en el backend.
        /// </summary>
        public string IdSesion { get; }
        /// <summary>
        /// Código de Usuario que genera el evento.
        /// </summary>
        public string CodigoUsuario { get; }
        /// <summary>
        /// Código de la agencia desde donde se genera el evento, por lo general donde
        /// se encuentra activo el usuario.
        /// </summary>
        public string CodigoAgencia { get; }
        /// <summary>
        /// Código del canal en el cual se esta ejecutando la operación que genera
        /// el evento.
        /// </summary>
        public string IndicadorCanal { get; }
        /// <summary>
        /// Código del sub canal en el cual se esta ejecutando la operación que
        /// genera el evento.
        /// </summary>
        public byte IndicadorSubCanal { get; }
        /// <summary>
        /// ID del terminal (nombre o IP) de donde llega la operación.
        /// </summary>
        public string IdTerminalOrigen { get; }
        /// <summary>
        /// ID del servicio que genera el evento.
        /// </summary>
        public string IdServicio { get; }
    }
}