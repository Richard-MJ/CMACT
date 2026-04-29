namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.BA
{
    /// <summary>
    /// Clase que representa a la entidad de dominio Entidad financiera Plaza CCE
    /// </summary>
    public class PlazaCCE
    {
        #region Propiedades
        /// <summary>
        /// Id de la plaza
        /// </summary>
        public int IDPlaza { get; private set; }
        /// <summary>
        /// Descripción de la plaza
        /// </summary>
        public string DescripcionPlaza { get; private set; }
        /// <summary>
        /// Código ubigeo de la plaza
        /// </summary>
        public string CodigoUbigeo { get; private set; }
        /// <summary>
        /// Indicador del estado de la plaza
        /// </summary>
        public string EstadoPlaza { get; private set; }
        /// <summary>
        /// propiedad que indica si es plaza exclusiva
        /// </summary>
        public virtual string EsPlazaExclusiva { get; private set; }
        /// <summary>
        /// Lista de oficinas CCE
        /// </summary>
        public virtual ICollection<OficinaCCE> Oficinas { get; private set; }

        #endregion
    }
}
