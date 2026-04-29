namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    /// <summary>
    /// Clase de dominio de tipo de cuenta grupo
    /// </summary>
    public class TipoCuentaGrupo
    {
        /// <summary>
        /// Indica el tipo de cuenta efectivo
        /// </summary>
        public string IndicadorTipoCuenta { get; private set; }
        /// <summary>
        /// Descripcion del tipo
        /// </summary>
        public string Descripcion { get; private set; }
    }
}
