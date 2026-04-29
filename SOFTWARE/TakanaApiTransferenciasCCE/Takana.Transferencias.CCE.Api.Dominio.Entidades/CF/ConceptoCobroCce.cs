namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF
{
    public class ConceptoCobroCCE
    {
        #region Constantes
        public const string CodigoConceptoOtro = "OTHR";
        #endregion
        /// <summary>
        /// Identificador de conceptp
        /// </summary>
        public int IdConcepto { get; private set; }
        /// <summary>
        /// Codigo del concepto
        /// </summary>
        public string Codigo { get; private set; }
        /// <summary>
        /// Descripcion del concepto
        /// </summary>
        public string Descripcion { get; private set; }
        /// <summary>
        /// Indicador de habilitado o no habilitado
        /// </summary>
        public bool IndicadorHabilitado { get; private set; }
    }

}
