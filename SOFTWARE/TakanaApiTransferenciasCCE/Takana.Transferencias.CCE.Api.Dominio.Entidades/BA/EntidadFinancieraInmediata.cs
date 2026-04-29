using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.BA
{
    /// <summary>
    /// Clase que representa a la entidad de dominio Entidad financiera inmediata
    /// </summary>
    public class EntidadFinancieraInmediata 
    {
        
        #region Constantes
        /// <summary>
        /// Constante de código de entidad Caja Tacna
        /// </summary>
        public const string CodigoCajaTacna = "0813";
        /// <summary>
        /// Constante de codigo de entidad
        /// </summary>
        public const string SoloCodigoCajaTacna = "813";
        /// <summary>
        /// Constante de código de Gestor de Directorio
        /// </summary>
        public const string CodigoGestorDirectorio = "5400";

        #endregion Constantes

        #region Propiedades

        /// <summary>
        /// Identificador de la Entidad Financiera
        /// </summary>
        public int IdentificadorEntidad { get; private set; }
        
        /// <summary>
        /// Código que identifica a la Entidad Financiera según CCE
        /// </summary>
        public string CodigoEntidad { get; private set; }

        /// <summary>
        /// Descripción de la Entidad Financiera
        /// </summary>
        public string NombreEntidad { get; private set; }

        /// <summary>
        /// Identificador del Estado Sign
        /// </summary>
        public string CodigoEstadoCCE { get; private set; }

        /// <summary>
        /// Identificador del Estado Sign
        /// </summary>
        public string CodigoEstadoSign { get; private set; }
        /// <summary>
        /// Identificador del Estado Sign
        /// </summary>
        public string? CodigoEntidadSBS { get; private set; }
        /// <summary>
        /// Estdo sign de la entidad
        /// </summary>
        public virtual EstadoInmediata EstadoSign { get; private set; }
        /// <summary>
        /// Estado de CCE
        /// </summary>
        public virtual EstadoInmediata EstadoCCE { get; private set; }
        /// <summary>
        /// Oficina de pago de tarjeta
        /// </summary>
        public string? OficinaPagoTarjeta { get; private set; }

        #endregion Propiedades

        #region Métodos
        /// <summary>
        /// Actualizar Estado de la CCE
        /// </summary>
        /// <param name="estadoCCE"></param>
        public void ActualizarEstadoCCE(string estadoCCE)
        {
            CodigoEstadoCCE = estadoCCE;
        }
        /// <summary>
        /// Actualizar estado sign de la entidad
        /// </summary>
        /// <param name="estadoSign"></param>
        public void ActualizarEstadoSign(string estadoSign)
        {
            CodigoEstadoSign = estadoSign;
        }

        #endregion
    }
}