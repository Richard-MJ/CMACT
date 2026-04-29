using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Logica.Cuenta_Efectivo
{
    /// <summary>
    /// Clase encargada de la logica de la cuenta efectivo Sueldo
    /// </summary>
    public class LogicaCuentaEfectivoSueldo : LogicaCuentaEfectivo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cuentaEfectivo"></param>
        public LogicaCuentaEfectivoSueldo(CuentaEfectivo cuentaEfectivo)
           : base(cuentaEfectivo)
        {
        }

        /// <summary>
        /// Método de retirar en cuenta sueldo
        /// </summary>
        /// <param name="monto"></param>
        /// <param name="indicadorCuentaSueldo"></param>
        /// <returns></returns>
        public override MontoCuentaEfectivo Retirar(decimal monto, bool indicadorCuentaSueldo)
        {
            var cuentaSueldo = ObtenerCuentaEfectivoSueldo();

            base.Retirar(monto, indicadorCuentaSueldo);

            var montoDetallado = MontoADebitar(monto, indicadorCuentaSueldo);

            cuentaSueldo.ActualizarSaldos(montoDetallado.Remunerativo * -1,
                montoDetallado.NoRemunerativo * -1, 0, 0, 0, 0);

            return montoDetallado;
        }

        /// <summary>
        /// Método que generar movimientos de la cuenta Sueldo.
        /// </summary>
        /// <param name="subTransaccion"></param>
        /// <param name="descripcionMovimiento"></param>
        /// <param name="monto"></param>
        /// <param name="usuarioActual"></param>
        /// <param name="codigoSistemaFuente"></param>
        /// <param name="fechaActual"></param>
        /// <param name="numeroMovimiento"></param>
        /// <returns></returns>
        public override IList<MovimientoDiario> GenerarMovimientos(SubTipoTransaccion subTransaccion,
                string descripcionMovimiento, MontoCuentaEfectivo monto, Usuario usuarioActual,
                string codigoSistemaFuente, DateTime fechaActual, int numeroMovimiento)
        {
            var movimientos = new List<MovimientoDiario>();

            if (monto.Remunerativo > 0)
            {
                var movimiento = MovimientoDiario.Crear(CuentaEfectivo, numeroMovimiento,
                    subTransaccion, descripcionMovimiento, monto.Remunerativo, usuarioActual,
                    fechaActual, codigoSistemaFuente, TipoMontoCuentaEfectivo.Remunerativo);
                CuentaEfectivo.MovimientosDiarios.Add(movimiento);
                movimientos.Add(movimiento);
            }

            if (monto.NoRemunerativo > 0)
            {
                var movimiento = MovimientoDiario.Crear(CuentaEfectivo, numeroMovimiento + 1,
                    subTransaccion, descripcionMovimiento, monto.NoRemunerativo, usuarioActual,
                    fechaActual, codigoSistemaFuente, TipoMontoCuentaEfectivo.NoRemunerativo);
                CuentaEfectivo.MovimientosDiarios.Add(movimiento);
                movimientos.Add(movimiento);
            }

            if (movimientos.Count == 2)
                movimientos[1].AsignarMovimientoPrincipal(movimientos[0]);

            if (monto.Remunerativo > 0 || monto.NoRemunerativo > 0)
                CuentaEfectivo.ActualizarFechaUltimoMovimiento(fechaActual);

            return movimientos;
        }

        /// <summary>
        /// Se obtiene los datos de saldos diferenciados, si no existe se crean.
        /// </summary>
        /// <returns>Datos de saldo de cuenta sueldo.</returns>
        private CuentaEfectivoSueldo ObtenerCuentaEfectivoSueldo()
        {
            return CuentaEfectivo.CuentaEfectivoSueldo ??
                   (CuentaEfectivo.CuentaEfectivoSueldo = CuentaEfectivoSueldo.Crear(
                       CuentaEfectivo.CodigoEmpresa, CuentaEfectivo.NumeroCuenta,
                       CuentaEfectivo.SaldoDisponible, 0, CuentaEfectivo.SaldoTransito, 0,
                       CuentaEfectivo.SaldoIntangible, 0));
        }

        /// <summary>
        /// Método de monto a debitar de la cuenta sueldo
        /// </summary>
        /// <param name="monto"></param>
        /// <param name="indicadorCuentaSueldo"></param>
        /// <returns></returns>
        private MontoCuentaEfectivo MontoADebitar(decimal monto, bool indicadorCuentaSueldo)
        {
            if (indicadorCuentaSueldo)
            {
                var montoRemunerativo = ObtenerCuentaEfectivoSueldo().SaldoDisponibleRemu >= monto
                    ? monto : ObtenerCuentaEfectivoSueldo().SaldoDisponibleRemu;
                var montoNoRemunerativo = monto - montoRemunerativo;

                return MontoCuentaEfectivo.Crear(montoNoRemunerativo, montoRemunerativo);
            }

            return MontoCuentaEfectivo.Crear(0, monto);
        }

        /// <summary>
        /// Método de posito en cuenta sueldo
        /// </summary>
        /// <param name="monto"></param>
        /// <param name="indicadorRemunerativo"></param>
        /// <returns></returns>
        public override LogicaCuentaEfectivo Depositar(decimal monto, string indicadorRemunerativo)
        {
            base.Depositar(monto, indicadorRemunerativo);

            decimal montoRemunerativo = indicadorRemunerativo == MovimientoDiario.Remunerativo ? monto : 0m;
            decimal montoNoRemunerativo = indicadorRemunerativo == MovimientoDiario.Remunerativo ? 0m : monto;

            CuentaEfectivo.CuentaEfectivoSueldo.ActualizarSaldos(
                montoRemunerativo,
                montoNoRemunerativo,
                0, 0, 0, 0
            );

            return this;
        }

        /// <summary>
        /// Método de debitar por la operacion de ITF
        /// </summary>
        /// <param name="monto"></param>
        /// <param name="indicadorCuentaSueldo"></param>
        /// <returns></returns>
        public override LogicaCuentaEfectivo DebitarPorOperacionITF(decimal monto, bool indicadorCuentaSueldo)
        {
            base.DebitarPorOperacionITF(monto, indicadorCuentaSueldo);

            if (indicadorCuentaSueldo)
            {
                if (CuentaEfectivo.CuentaEfectivoSueldo == null)
                    CuentaEfectivo.CuentaEfectivoSueldo = CuentaEfectivoSueldo.Crear(CuentaEfectivo.CodigoEmpresa
                        , CuentaEfectivo.NumeroCuenta, CuentaEfectivo.SaldoDisponible, 0, CuentaEfectivo.SaldoTransito, 0
                        , CuentaEfectivo.SaldoIntangible, 0);
                else
                {
                    CuentaEfectivo.CuentaEfectivoSueldo.ActualizarSaldos(0, monto * -1, 0, 0, 0, 0);
                }
            }

            return this;
        }
    }
}
