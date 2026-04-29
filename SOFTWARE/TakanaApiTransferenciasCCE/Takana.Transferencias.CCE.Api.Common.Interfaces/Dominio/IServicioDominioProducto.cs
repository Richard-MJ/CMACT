
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;

namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    /// <summary>
    /// Interfaz del servicio de dominio de producto.
    /// </summary>
    public interface IServicioDominioProducto : IServicioBase
    {
        /// <summary>
        /// Metodo que genera los movimientos de Credito de Transferencias Interbancarias Entrantes
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="subTipoTransaccionTransferencia"></param>
        /// <param name="subTipoTransaccionITF"></param>
        /// <param name="fechaSistema"></param>
        /// <param name="cuentaEfectivo"></param>
        /// <param name="transaccion"></param>
        /// <param name="numeroMovimiento"></param>
        /// <returns>Retorna Lista de Movimientos Diarios</returns>
        List<MovimientoDiario> GenerarMovimientoTransferenciaEntrante(
              Usuario usuario,
              SubTipoTransaccion subTipoTransaccionTransferencia,
              DateTime fechaSistema,
              CuentaEfectivo cuentaEfectivo,
              TransaccionOrdenTransferenciaInmediata transaccion,
              int numeroMovimiento);

        /// <summary>
        /// Método que genera un movimiento diario crédito con datos de la subTransacción
        /// </summary>
        /// <param name="numeroMovimiento">número del movimiento</param>
        /// <param name="montoMovimiento">monto del movimiento</param>
        /// <param name="fechaMovimiento">fecha del movimiento</param>
        /// <param name="codigoSistemaFuente">codigo del sistem fuente del movimiento</param>
        /// <param name="cuentaEfectivo">la cuenta efectivo de origen del movimiento</param>
        /// <param name="usuario">datos de usuario que realiza la operación</param>
        /// <param name="subTipoTransaccion">el tipo de subtransacción</param>
        /// <returns>Movimiento Diario Generado</returns>
        MovimientoDiario GenerarMovimientoDiarioCredito(
            int numeroMovimiento,
            decimal montoMovimiento,
            DateTime fechaMovimiento,
            string codigoSistemaFuente,
            CuentaEfectivo cuentaEfectivo,
            Usuario usuario,
            SubTipoTransaccion subTipoTransaccion);

        /// <summary>
        /// Metodo que genera los movimientos de Credito de Transferencias Interbancarias Entrantes Devolucion
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="subTipoTransaccionTransferencia"></param>
        /// <param name="fechaSistema"></param>
        /// <param name="cuentaEfectivo"></param>
        /// <param name="transaccion"></param>
        /// <param name="numeroMovimiento"></param>
        /// <returns>Retorna Lista de Movimientos Diarios</returns>
        List<MovimientoDiario> GenerarMovimientoTransferenciaDevolucion(
              Usuario usuario,
              SubTipoTransaccion subTipoTransaccionTransferencia,
              DateTime fechaSistema,
              CuentaEfectivo cuentaEfectivo,
              TransaccionOrdenTransferenciaInmediata transaccion,
              int numeroMovimiento);

        /// <summary>
        /// Método que realiza el movimiento diario de ITF
        /// </summary>
        /// <param name="numeroMovimiento">Número de movimiento del ITF</param>
        /// <param name="montoMovimientoITF">monto de movimiento del ITF</param>
        /// <param name="cuentaEfectivo">cuenta efectivo de origen</param>
        /// <param name="usuario">Datos del usuario</param>
        /// <param name="subTipoTransaccion">tipo de subtransacción</param>
        /// <param name="movimientoOrigen">movimiento de origen que genera el ITF</param>
        /// <returns>Movimiento Diarios del ITF</returns>
        MovimientoDiario GenerarMovimientoITF(
           int numeroMovimiento,
           decimal montoMovimientoITF,
           CuentaEfectivo cuentaEfectivo,
           Usuario usuario,
           SubTipoTransaccion subTipoTransaccion,
           MovimientoDiario movimientoOrigen);

        /// <summary>
        /// Obtiene el movimiento principal de una lista de movimientos
        /// </summary>
        /// <param name="movimientos">Lista de movimientos</param>
        /// <returns>Retorna el movimiento principal</returns>
        MovimientoDiario ObtenerMovimientoPrincipal(IList<MovimientoDiario> movimientos);
    }
}
