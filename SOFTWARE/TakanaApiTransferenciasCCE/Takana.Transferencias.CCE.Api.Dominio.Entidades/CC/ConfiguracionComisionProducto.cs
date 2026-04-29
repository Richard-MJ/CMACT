using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    public class ConfiguracionComisionProducto : Empresa
    {
        /// <summary>
        /// Codigo de configuración
        /// </summary>
        public long CodigoConfiguracionProducto { get; private set; }
        /// <summary>
        /// Codigo de configuración
        /// </summary>
        public long CodigoConfiguracion { get; private set; }
        /// <summary>
        /// Código del producto
        /// </summary>
        public string CodigoProducto { get; private set; }
        /// <summary>
        /// Código del sistema de la transacción
        /// </summary>
        public string CodigoSistema { get; private set; }
        /// <summary>
        /// Cantidad de operaciones libres de comisiones al mes
        /// </summary>
        public int NumeroOperacionesLibresSinComision { get; private set; }
        /// <summary>
        /// Codigo del estado
        /// </summary>
        public bool IndicadorEstado { get; private set; }
        /// <summary>
        /// fecha del registro
        /// </summary>
        public DateTime FechaRegistro { get; private set; }
        /// <summary>
        /// usuario que registro
        /// </summary>
        public string CodigoUsuarioRegistro { get; private set; }
        /// <summary>
        /// fecha de la ultima modificación
        /// </summary>
        public DateTime? FechaModificacion { get; private set; }
        /// <summary>
        /// ultimo usuario en modificar
        /// </summary>
        public string? CodigoUsuarioModificacion { get; private set; }
        /// <summary>
        /// Propiedad virtual que define el producto
        /// </summary>
        public virtual Producto Producto { get; private set; }
        /// <summary>
        /// Propiedad virtual que define las colecciones de detalle de agencias
        /// </summary>
        public virtual ICollection<ConfiguracionComisionAgencia> ConfiguracionAgencias { get; private set; }
        /// <summary>
        /// Propiedad virtual que define la configuracion Transacción
        /// </summary>
        public virtual ConfiguracionComision ConfiguracionTransaccion { get; private set; }
    }
}
