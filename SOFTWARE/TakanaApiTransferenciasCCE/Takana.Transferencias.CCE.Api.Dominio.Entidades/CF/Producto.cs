
namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF
{
    public class Producto : Empresa
    {
        #region Propiedades

        /// <summary>
        /// Código de sistema del producto
        /// </summary>
        public string CodigoSistema { get; private set; }

        /// <summary>
        /// Código del productoi
        /// </summary>
        public string CodigoProducto { get; private set; }

        /// <summary>
        /// Nombre del producto
        /// </summary>
        public string NombreProducto { get; private set; }
        /// <summary>
        /// Codigo de tipo de producto reporte
        /// </summary>
        public string CodigoTipoProductoReporte { get; private set; }

        /// <summary>
        /// Código de la monedad del producto
        /// </summary>
        public string CodigoMoneda { get; private set; }
        /// <summary>
        /// Codigo de producto asociado
        /// </summary>
        public string CodigoProductoAsociado { get; private set; }

        /// <summary>
        /// Código del estado de producto
        /// </summary>
        public string CodigoEstado { get; private set; }

        /// <summary>
        /// Descripción del producto
        /// </summary>
        public string DescripcionProducto { get; private set; }
        /// <summary>
        /// Nombre comercial del producto
        /// </summary>
        public string NombreComercial { get; private set; }

        /// <summary>
        /// Propiedad virtual que define la moneda del producto
        /// </summary>
        public virtual Moneda Moneda { get; private set; }
        #endregion
    }
}
