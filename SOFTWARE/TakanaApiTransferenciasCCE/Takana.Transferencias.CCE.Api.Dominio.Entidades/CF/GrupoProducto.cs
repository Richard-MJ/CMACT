namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF
{
    public class GrupoProducto : Empresa
    {
        #region Constantes
        /// <summary>
        /// Constante que indica los productos permitidos para débio
        /// </summary>
        public const string IndicadorGrupoDebitoTinInmediata = "CC_TI";
        /// <summary>
        /// Constante que indica los productos permitidos para débio
        /// </summary>
        public const string IndicadorGrupoDebito = "PD_DB";
        #endregion

        #region Propiedades
        /// <summary>
        /// Clave primaria del grupo de producto
        /// </summary>
        public int IdProducto { get; private set; }
        /// <summary>
        /// Código del sistema
        /// </summary>
        public string CodigoSistema { get; private set; }
        /// <summary>
        /// Código del producto
        /// </summary>
        public string CodigoProducto { get; private set; }
        /// <summary>
        /// Indicador del grupo de producto
        /// </summary>
        public string IndicadorGrupo { get; private set; }
        /// <summary>
        /// Indicador de estado
        /// </summary>
        public string IndicadorEstado { get; private set; }
        #endregion
    }
}
