namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF
{
    public class EstadoInmediata 
    {
        #region Constantes
        /// <summary>
        /// Constante de código de entidad Caja Tacna
        /// </summary>
        public const string EstadoSignOn = "H";

        /// <summary>
        /// Constante de código de entidad Caja Tacna
        /// </summary>
        public const string EstadoSignOff = "I";

        /// <summary>
        /// Constante de código de entidad Caja Tacna
        /// </summary>
        public const string EstadoBloqueado = "B";

        /// <summary>
        /// Constante de código de entidad Caja Tacna
        /// </summary>
        public const string Normalizado = "N";

        #endregion Constantes

        #region Propiedades

        /// <summary>
        /// Identificador del Estado
        /// </summary>
        public string Codigo { get; private set; }
        
        /// <summary>
        /// Código que identifica a la Entidad Financiera según CCE
        /// </summary>
        public string Descripcion { get; private set; }

        #endregion Propiedades
    }
}