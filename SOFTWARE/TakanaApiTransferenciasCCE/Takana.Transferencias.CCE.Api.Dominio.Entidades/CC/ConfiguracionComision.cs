using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    public class ConfiguracionComision : Empresa
    {
        #region Constante
        /// <summary>
        /// Constante que define el código de comisión de transferencia interbancaria
        /// </summary>
        public const string CodigoTransferenciaInterbancaria = "TRIN";
        #endregion
        /// <summary>
        /// Codigo de configuración
        /// </summary>
        public long CodigoConfiguracion { get; private set; }
        /// <summary>
        /// Código de la comisión
        /// </summary>
        public string CodigoComision { get; private set; }
        /// <summary>
        /// Código del tipo de transacción
        /// </summary>
        public string CodigoTipoTransaccion { get; private set; }
        /// <summary>
        /// Código del sub tipo transacción
        /// </summary>
        public string CodigoSubTipoTransaccion { get; private set; }
        /// <summary>
        /// Código del sistema de la transacción
        /// </summary>
        public string CodigoSistema { get; private set; }
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
        /// Propiedad virtual que define la sub transacción
        /// </summary>
        public virtual SubTipoTransaccion SubTipoTransaccion { get; private set; }
        /// <summary>
        /// Propiedad virtual que define las colecciones de detalle de productos
        /// </summary>
        public virtual ICollection<ConfiguracionComisionProducto> ConfiguracionProductos { get; private set; }
        /// <summary>
        /// Propiedad virtual que define las colecciones de detalle de moneda
        /// </summary>
        public virtual ICollection<ConfiguracionComisionMoneda> ConfiguracionMonedas { get; private set; }

    }
}
