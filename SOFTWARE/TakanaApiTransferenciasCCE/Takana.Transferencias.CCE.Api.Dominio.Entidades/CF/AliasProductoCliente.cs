
namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF
{
    public class AliasProductoCliente
    {
        #region Propiedades
        /// <summary>
        /// Identificador del Alias
        /// </summary>
        public int NumeroAlias { get; private set; }
        /// <summary>
        /// Alias que tomara el producto
        /// </summary>
        public string NombreAlias { get; private set; }
        /// <summary>
        /// Número de producto
        /// </summary>
        public string NumeroProducto { get; private set; }
        /// <summary>
        /// Codigo del sistema que identifica al producto
        /// </summary>
        public string CodigoSistema { get; private set; }
        /// <summary>
        /// Indicador de estado del alias
        /// </summary>
        public string IndicadorEstado { get; private set; }
        /// <summary>
        /// Fecha en la que se registro el alias
        /// </summary>
        public DateTime FechaRegistro { get; private set; }
        /// <summary>
        /// Codigo del usuario que agrego el alias
        /// </summary>
        public string CodigoUsuario { get; private set; }
        #endregion
    }
}
