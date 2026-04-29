namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    public class ComisionesExoneradasVista : Empresa
    {
        /// <summary>
        /// Id de la comiisón exonerada
        /// </summary>
        public long CodigoComisionExonerada { get; private set; }
        /// <summary>
        /// Número del movimiento (diario o mensual) origen
        /// </summary>
        public decimal NumeroMovimientoOrigen { get; private set; }
        /// <summary>
        /// Código de la comisión exonerada
        /// </summary>
        public string CodigoComision { get; private set; }
        /// <summary>
        /// Numero de la cuenta efectivo
        /// </summary>
        public string NumeroCuenta { get; private set; }
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
        /// Fecha del movimiento de la comisión
        /// </summary>
        public DateTime FechaMovimiento { get; private set; }
        /// <summary>
        /// Código del estado del movimiento
        /// </summary>
        public string CodigoEstadoMovimiento { get; private set; }
        /// <summary>
        /// Propiedad virtual que define la cuenta efectivo
        /// </summary>
        public virtual CuentaEfectivo CuentaEfectivo { get; private set; }
    }
}
