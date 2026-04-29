namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    /// <summary>
    /// Interfaz del contexto del API
    /// </summary>
    public interface IContextoAplicacion
    {
        /// <summary>
        /// ID de la sesión
        /// </summary>
        string IdSesion { get; }
        /// <summary>
        /// ID del login
        /// </summary>
        string IdLogin { get; }
        /// <summary>
        /// ID del terminal de origen
        /// </summary>
        string IdTerminalOrigen { get; }
        /// <summary>
        /// Identificador del usuario autenticado
        /// </summary>
        string IdUsuarioAutenticado { get; }
        /// <summary>
        /// Dominio de la aplicacion
        /// </summary>
        string Dominio { get; }
        /// <summary>
        /// Indicador del Canal
        /// </summary>
        string IndicadorCanal { get; }
        /// <summary>
        /// Indicador de Sub Canal
        /// </summary>
        byte IndicadorSubCanal { get; }
        /// <summary>
        /// Codigo usuario de la session
        /// </summary>
        string CodigoUsuario { get; }
        /// <summary>
        /// Codigo de la agencia de la session
        /// </summary>
        string CodigoAgencia { get; }
        /// <summary>
        /// Identificador del terminal del login
        /// </summary>
        public string IdTerminalLogin { get; }
        /// <summary>
        /// Identificador del Canal de Origen
        /// </summary>
        public string IdCanalOrigen { get; }
        /// <summary>
        /// Modelo del dispositivo que se esta utilizando
        /// </summary>
        public string ModeloDispositivo { get; }
        /// <summary>
        /// Direccion Ip
        /// </summary>
        public string IpAddress { get; }
        /// <summary>
        /// Información del Navegador
        /// </summary>
        public string Navegador { get; }
        /// <summary>
        /// Información del Sistema Operativo
        /// </summary>
        public string SistemaOperativo { get; }
        /// <summary>
        /// Contraseña encriptada del usuario
        /// </summary>
        public string ContrasenaEncriptada { get; }
        /// <summary>
        /// Identificador Visual
        /// </summary>
        public string IdVisual { get; }

        /// <summary>
        /// Token de sesión
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// Actualiza los datos del contexto.
        /// </summary>
        /// <param name="idLogin">Identificador del login</param>
        /// <param name="idSesion">Identificador de la sesion.</param>
        /// <param name="codigoUsuario">Identificador del usuario.</param>
        /// <param name="codigoAgencia">Identificador de la agencia.</param>
        /// <param name="indicadorCanal">Indicador del canal.</param>
        /// <param name="idTerminalOrigen">Identificador del terminal de origen.</param>
        void Actualizar(string idLogin, string idSesion, string codigoUsuario, string codigoAgencia,
            string indicadorCanal, string idTerminalOrigen);

        void Actualizar(string idSesion, string idLogin, string idAudiencia, string idUsuarioAutenticado,
            string idTerminalUsuario, string idCanalOrigen, string codigoUsuario, string codigoAgencia,
            string modeloDispositivo, string ipAddress, string navegador, string sistemaOperativo, string idVisual,
            string claveEncriptada, string token);
    }
}
