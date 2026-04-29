using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios
{
    /// <summary>
    /// Servicio de maquetacion de las Entradas
    /// </summary>
    public class ServicioDominioProducto : IServicioDominioProducto
    {
        #region Métodos

        /// <summary>
        /// Metodo que genera los movimientos de Credito de Transferencias Interbancarias Entrantes
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="subTipoTransaccionTransferencia"></param>
        /// <param name="fechaSistema"></param>
        /// <param name="cuentaEfectivo"></param>
        /// <param name="transaccion"></param>
        /// <param name="numeroMovimiento"></param>
        /// <returns>Retorna Lista de Movimientos Diarios</returns>
        public List<MovimientoDiario> GenerarMovimientoTransferenciaEntrante(
              Usuario usuario,
              SubTipoTransaccion subTipoTransaccionTransferencia,
              DateTime fechaSistema,
              CuentaEfectivo cuentaEfectivo,
              TransaccionOrdenTransferenciaInmediata transaccion,
              int numeroMovimiento)
        {
            List<MovimientoDiario> movimientosDiariosCuentaEfectivo = new List<MovimientoDiario>();

            var movimientoEnCuentaEfectivo = GenerarMovimientoDiarioCredito(
                numeroMovimiento,
                transaccion.MontoTransferencia,
                fechaSistema,
                Sistema.CuentaEfectivo,
                cuentaEfectivo,
                usuario,
                subTipoTransaccionTransferencia);
            movimientosDiariosCuentaEfectivo.Add(movimientoEnCuentaEfectivo);

            cuentaEfectivo.ActualizarFechaUltimoMovimiento(fechaSistema);

            return movimientosDiariosCuentaEfectivo;
        }

        /// <summary>
        /// Metodo que Generar el Movimiento Diario de Credito
        /// </summary>
        /// <param name="numeroMovimiento"></param>
        /// <param name="montoMovimiento"></param>
        /// <param name="fechaMovimiento"></param>
        /// <param name="codigoSistemaFuente"></param>
        /// <param name="cuentaEfectivo"></param>
        /// <param name="usuario"></param>
        /// <param name="subTipoTransaccion"></param>
        /// <returns>Retorna el Movimiento Diario</returns>
        public MovimientoDiario GenerarMovimientoDiarioCredito(
            int numeroMovimiento,
            decimal montoMovimiento,
            DateTime fechaMovimiento,
            string codigoSistemaFuente,
            CuentaEfectivo cuentaEfectivo,
            Usuario usuario,
            SubTipoTransaccion subTipoTransaccion)
        {
            cuentaEfectivo.Depositar(montoMovimiento, fechaMovimiento);

            var movimiento = MovimientoDiario.Crear(
                cuentaEfectivo,
                numeroMovimiento,
                subTipoTransaccion,
                subTipoTransaccion.DescripcionSubTransaccion,
                montoMovimiento,
                usuario,
                fechaMovimiento,
                codigoSistemaFuente,
                TipoMontoCuentaEfectivo.NoRemunerativo);

            cuentaEfectivo.MovimientosDiarios.Add(movimiento);
            return movimiento;
        }

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
        public List<MovimientoDiario> GenerarMovimientoTransferenciaDevolucion(
              Usuario usuario,
              SubTipoTransaccion subTipoTransaccionTransferencia,
              DateTime fechaSistema,
              CuentaEfectivo cuentaEfectivo,
              TransaccionOrdenTransferenciaInmediata transaccion,
              int numeroMovimiento)
        {
            List<MovimientoDiario> movimientosDiariosCuentaEfectivo = new List<MovimientoDiario>();

            var movimientoEnCuentaEfectivo = GenerarMovimientoDiarioCredito(
                numeroMovimiento,
                transaccion.MontoTransferencia,
                fechaSistema,
                Sistema.CuentaEfectivo,
                cuentaEfectivo,
                usuario,
                subTipoTransaccionTransferencia);

            if (cuentaEfectivo.EsCuentaSueldo)
            {
                cuentaEfectivo.CuentaEfectivoSueldo
                    .ActualizarSaldos(0, transaccion.MontoTransferencia, 0, 0, 0, 0);
            }

            movimientosDiariosCuentaEfectivo.Add(movimientoEnCuentaEfectivo);

            return movimientosDiariosCuentaEfectivo;
        }

        /// <summary>
        /// Método que genera el Cargo del ITF
        /// </summary>
        /// <param name="numeroMovimiento"></param>
        /// <param name="montoMovimientoITF"></param>
        /// <param name="cuentaEfectivo"></param>
        /// <param name="usuario"></param>
        /// <param name="subTipoTransaccion"></param>
        /// <param name="movimientoOrigen"></param>
        /// <returns></returns>
        public MovimientoDiario GenerarMovimientoITF(
           int numeroMovimiento,
           decimal montoMovimientoITF,
           CuentaEfectivo cuentaEfectivo,
           Usuario usuario,
           SubTipoTransaccion subTipoTransaccion,
           MovimientoDiario movimientoOrigen)
        {
            var movimientoITF = MovimientoDiario.Crear(
                cuentaEfectivo,
                numeroMovimiento,
                subTipoTransaccion,
                subTipoTransaccion.DescripcionSubTransaccion,
                montoMovimientoITF,
                usuario,
                movimientoOrigen.FechaMovimiento,
                movimientoOrigen.CodigoSistemaFuente,
                TipoMontoCuentaEfectivo.NoRemunerativo);

            movimientoITF.AsignarMovimientoOrigen(movimientoOrigen);
            movimientoOrigen.AgregarMontoITF(montoMovimientoITF);

            movimientoOrigen.Cuenta.MovimientosDiarios.Add(movimientoITF);
            return movimientoITF;
        }

        /// <summary>
        /// Obtiene el movimiento principal de una lista de movimientos
        /// </summary>
        /// <param name="movimientos">Lista de movimientos</param>
        /// <returns>Retorna el movimiento principal</returns>
        public MovimientoDiario ObtenerMovimientoPrincipal(IList<MovimientoDiario> movimientos)
        {
            var movimientosPrincipal = movimientos.Where(m => m.EsMovimientoPrincipal);

            if (movimientosPrincipal.Count() > 1)
                throw new Exception("Existe mas de un movimiento principal en la lista.");

            return movimientosPrincipal.SingleOrDefault();
        }

        #endregion
    }
}
