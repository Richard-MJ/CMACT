
using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Infraestructura.Contenedor
{
    public class ContextoSistema : IContextoAplicacion, IContextoBitacora
    {
        #region Implementación de IContextoBitacora.
        /// <summary>
        /// Identificador del login
        /// </summary>
        public string IdLogin { get; private set; }
        /// <summary>
        /// Identificador de sesion
        /// </summary>
        public string IdSesion { get; private set; }
        /// <summary>
        /// Codigo del usuario
        /// </summary>
        public string CodigoUsuario { get; private set; }
        /// <summary>
        /// Codigo de agencia
        /// </summary>
        public string CodigoAgencia { get; private set; }
        /// <summary>
        /// Indicador de canal
        /// </summary>
        public string IndicadorCanal { get; private set; }
        /// <summary>
        /// Indicador de sub canal
        /// </summary>
        public byte IndicadorSubCanal { get; private set; }
        /// <summary>
        /// Identificador de terminal de origen
        /// </summary>
        public string IdTerminalOrigen { get; private set; }
        /// <summary>
        /// Identificador del servicio
        /// </summary>
        public string IdServicio => ConfigApi.Nombre;
        /// <summary>
        /// Identificador del servicio
        /// </summary>
        public string Dominio => ConfigApi.Dominio;
        /// <summary>
        /// Identificador de audiencia
        /// </summary>
        public string IdAudiencia { get; private set; }
        /// <summary>
        /// Identificador del terminal del login
        /// </summary>
        public string IdTerminalLogin { get; private set; }
        /// <summary>
        /// Identificador del Canal de Origen
        /// </summary>
        public string IdCanalOrigen { get; private set; }
        /// <summary>
        /// Modelo del dispositivo que se esta utilizando
        /// </summary>
        public string ModeloDispositivo { get; private set; }
        /// <summary>
        /// Direccion Ip
        /// </summary>
        public string IpAddress { get; private set; }
        /// <summary>
        /// Información del Navegador
        /// </summary>
        public string Navegador { get; private set; }
        /// <summary>
        /// Información del Sistema Operativo
        /// </summary>
        public string SistemaOperativo { get; private set; }
        /// <summary>
        /// Identificador Visual
        /// </summary>
        public string IdVisual { get; private set; }
        /// <summary>
        /// Contraseña encriptada del usuario
        /// </summary>
        public string ContrasenaEncriptada { get; private set; }

        /// <summary>
        /// Token de sesión
        /// </summary>
        public string Token { get;private set; }

        #endregion Implementación de IContextoBitacora.

        /// <summary>
        /// Constructor del contexto del sistema
        /// </summary>
        public ContextoSistema()
        {
            IdLogin = string.Empty;
            IdSesion = ShortGuid.NewGuid();
            CodigoUsuario = Environment.UserName;
            CodigoAgencia = Agencia.Principal;
            IndicadorCanal = General.CanalCCE;
            IndicadorSubCanal = (int)CanalCCE.CanalInmediataEnum.SubCanalTinInmediata;
            IdTerminalOrigen = Environment.MachineName;
        }

        #region Implementación IContextoApi
        /// <summary>
        /// Identificador de usuario de autenticado
        /// </summary>
        public string IdUsuarioAutenticado { get; private set; }
        /// <summary>
        /// Identificador de terminal
        /// </summary>
        public string IdTerminal { get; private set; }

        /// <summary>
        /// Método que actualiza el contexto
        /// </summary>
        /// <param name="idLogin"></param>
        /// <param name="idSesion"></param>
        /// <param name="codigoUsuario"></param>
        /// <param name="codigoAgencia"></param>
        /// <param name="indicadorCanal"></param>
        /// <param name="idTerminalOrigen"></param>
        public void Actualizar(string idLogin, string idSesion, string codigoUsuario, string codigoAgencia,
            string indicadorCanal, string idTerminalOrigen)
        {
            IdLogin = idLogin;
            IdSesion = string.IsNullOrWhiteSpace(idSesion) ? (string)ShortGuid.NewGuid() : idSesion;
            CodigoUsuario = codigoUsuario;
            CodigoAgencia = codigoAgencia;
            IndicadorCanal = indicadorCanal;
            IdTerminalOrigen = idTerminalOrigen;
        }

        /// <summary>
        /// Método que actualiza el contexto
        /// </summary>
        /// <param name="idSesion"></param>
        /// <param name="idLogin"></param>
        /// <param name="idAudiencia"></param>
        /// <param name="idUsuarioAutenticado"></param>
        /// <param name="idTerminalUsuario"></param>
        /// <param name="idCanalOrigen"></param>
        /// <param name="codigoUsuario"></param>
        /// <param name="codigoAgencia"></param>
        /// <param name="modeloDispositivo"></param>
        /// <param name="ipAddress"></param>
        /// <param name="navegador"></param>
        /// <param name="sistemaOperativo"></param>
        /// <param name="idVisual"></param>
        /// <param name="claveEncriptada"></param>
        public void Actualizar(string idSesion, string idLogin, string idAudiencia, string idUsuarioAutenticado,
            string idTerminalUsuario, string idCanalOrigen, string codigoUsuario, string codigoAgencia,
            string modeloDispositivo, string ipAddress, string navegador, string sistemaOperativo, string idVisual,
            string claveEncriptada, string token)
        {
            IdSesion = idSesion;
            IdLogin = idLogin;
            IdAudiencia = idAudiencia;
            IdUsuarioAutenticado = idUsuarioAutenticado;
            IdTerminalLogin = idTerminalUsuario;
            IdCanalOrigen = idCanalOrigen;
            IndicadorCanal = idCanalOrigen;
            CodigoUsuario = codigoUsuario;
            CodigoAgencia = codigoAgencia;
            ModeloDispositivo = modeloDispositivo;
            IpAddress = ipAddress;
            Navegador = navegador;
            SistemaOperativo = sistemaOperativo;
            ContrasenaEncriptada = claveEncriptada;
            IdVisual = idVisual;
            Token = token;
        }

        #endregion Implementación IContextoApi
    }
}