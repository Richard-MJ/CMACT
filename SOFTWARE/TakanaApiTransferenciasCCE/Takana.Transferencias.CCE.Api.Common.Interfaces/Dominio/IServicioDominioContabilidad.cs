
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;
using Takana.Transferencias.CCE.Api.Common.DTOs.CG;

namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    /// <summary>
    /// Interfaz que maqueta las tramas de respuesta para la CCE
    /// </summary>
    public interface IServicioDominioContabilidad : IServicioBase
    {
        /// <summary>
        /// Implementación del método GenerarAsientoContableCompletoPendiente a nivel de dominio
        /// </summary>
        /// <param name="numeroAsiento"></param>
        /// <param name="tipoCambioBase"></param>
        /// <param name="tipoCambioCuenta"></param>
        /// <param name="movimientos"></param>
        /// <returns>Instancia el asiento contable</returns>
        AsientoContable GenerarAsientoContableCompletoPendiente(
            int numeroAsiento,
            DateTime fechaSistema,
            decimal tipoCambioBase,
            decimal tipoCambioCuenta, 
            params IMovimientoContable[] movimientos);

        /// <summary>
        /// Método de Generar Detalle Asiento Contable de Transferencias Interbancarias Entrantes de Cuentas Puente
        /// </summary>
        /// <param name="subTipoTransaccionTransferencia"></param>
        /// <param name="asiento"></param>
        /// <param name="movimiento"></param>
        /// <param name="moneda"></param>
        /// <param name="MontoComision"></param>
        /// <param name="tasaCambio"></param>
        void GenerarDetalleAsientoDeCuentaPuenteTransferenciaEntrante(
            SubTipoTransaccion subTipoTransaccionTransferencia,
            AsientoContable asiento, IMovimientoContable? movimiento,
            List<CuentaContableInfoDTO> cuentasPuente, decimal MontoComision, 
            ITasaCambio tasaCambio, string descripcion, DateTime fechaSistema);

        /// <summary>
        /// Método que genera el asiento contable de transferencias interbancaria inmediata entrante, solo comisión
        /// </summary>
        /// <param name="cuentasPuente"></param>
        /// <param name="montoComision"></param>
        /// <param name="subTipoTransaccionTransferencia"></param>
        /// <param name="usuario"></param>
        /// <param name="numeroAsiento"></param>
        /// <param name="tipoCambioContabilidad"></param>
        /// <returns></returns>
        AsientoContable GenerarAsientoContableCompletoPendienteTransferenciaComisionEntrante(
            List<CuentaContableInfoDTO> cuentasPuente, decimal montoComision, SubTipoTransaccion subTipoTransaccionTransferencia, 
            DateTime fechaSistema, Usuario usuario, int numeroAsiento, ITasaCambio tipoCambioContabilidad);

        /// <summary>
        /// Método que genera el movimiento auxiliar contable Transaferencias entrante o saliente
        /// </summary>
        /// <param name="movimientosAContabilizar"></param>
        /// <param name="listaTransaccionContabilidad"></param>
        void GenerarMovimientoAuxiliarContableTransferencia(
            List<MovimientoDiario> movimientosAContabilizar,
            List<ITransaccion> listaTransaccionContabilidad,
            string indicadorTransferencia,
            string cuentaContableDestino);

        /// <summary>
        /// Implementación del método AnularAsientoContablePendiente a nivel de dominio
        /// </summary>
        /// <param name="asientoContable">Instancia de la clase AsientoContable</param>
        /// <returns>La misma instancia de la clase AsientoContable</returns>
        AsientoContable AnularAsientoContablePendiente(AsientoContable asientoContable);

        /// <summary>
        /// Implementación del método GenerarAsientoContableCompletoPendiente a nivel de dominio
        /// </summary>
        /// <param name="numeroAsiento"></param>
        /// <param name="fechaSistema"></param>
        /// <param name="tipoCambioBase"></param>
        /// <param name="tipoCambioCuenta"></param>
        /// <param name="movimientos"></param>
        /// <returns>Instancia el asiento contable</returns>
        AsientoContable GenerarAsientoContablePorComision(
            int numeroAsiento,
            DateTime fechaSistema,
            decimal tipoCambioBase,
            decimal tipoCambioCuenta,
            params IMovimientoContable[] movimientos);

        /// <summary>
        /// Genera un ajuste contable asociado a un asiento contable existente,
        /// utilizando la cuenta contable indicada para realizar dicho ajuste.
        /// </summary>
        /// <param name="asientoContable">Asiento contable sobre el cual se generará el ajuste.</param>
        /// <param name="cuentaAjuste">Información de la cuenta contable utilizada para el ajuste.</param>
        /// <returns>El detalle del asiento contable generado como ajuste.</returns>
        AsientoContableDetalle GenerarAjuste(AsientoContable asientoContable, CuentaContableInfoDTO cuentaAjuste);

        /// <summary>
        /// Cierra un asiento contable, dejándolo en estado definitivo
        /// e impidiendo modificaciones posteriores.
        /// </summary>
        /// <param name="asientoContable">Asiento contable que se desea cerrar.</param>
        /// <returns>El asiento contable cerrado.</returns>
        AsientoContable CerrarAsientoContable(AsientoContable asientoContable);
    }
}
