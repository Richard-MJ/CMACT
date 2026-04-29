namespace Takana.Transferencias.CCE.Api.Dominio.Entidades
{
    /// <summary>
    /// Clase abstracta de la empresa
    /// </summary>
    public abstract class Empresa
    {
        #region Constantes
        /// <summary>
        /// Codigo de la empresa
        /// </summary>
        public const string CodigoPrincipal = "1";

        #endregion Constantes

        /// <summary>
        /// Codigo de la empresa
        /// </summary>
        private string? _codigoEmpresa;

        public string CodigoEmpresa
        {
            get { return _codigoEmpresa; }
            internal set { this._codigoEmpresa = value; }
        }
    }
}
