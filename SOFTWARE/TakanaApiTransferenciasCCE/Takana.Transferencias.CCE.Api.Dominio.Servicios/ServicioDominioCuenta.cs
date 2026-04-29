using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Dominio;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;
using Takana.Transferencias.CCE.Api.Dominio.Servicios.Logica;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios
{
    public class ServicioDominioCuenta : IServicioDominioCuenta
    {
        /// <summary>
        /// Metodo que genera el movimiento diario de comision
        /// </summary>
        /// <param name="movimientoPrincipal">Movimiento principal</param>
        /// <param name="comisionAhorros">Comision de ahorros</param>
        /// <param name="usuario">Usuarios</param>
        /// <param name="numeroMovimiento">Numero de movimientos</param>
        /// <returns>Retorna el movimiento diario</returns>
        public MovimientoDiario GenerarMovimientoDiarioDeComision(
            MovimientoDiario movimientoPrincipal,
            ComisionAhorrosAuxiliar comisionAhorros,
            Usuario usuario,
            int numeroMovimiento,
            bool indicadorCuentaSueldo)
        {
            var movimientoComision = MovimientoDiario.Crear(
                movimientoPrincipal.Cuenta,
                numeroMovimiento,
                comisionAhorros.ConfiguracionComision.SubTipoTransaccion,
                comisionAhorros.ConfiguracionComision.SubTipoTransaccion.DescripcionSubTransaccion,
                comisionAhorros.MontoComision.Redondear(AsientoContableDetalle.DecimalesPorDefecto),
                usuario,
                movimientoPrincipal.FechaMovimiento,
                movimientoPrincipal.CodigoSistema,
                TipoMontoCuentaEfectivo.NoRemunerativo);

            var logicaCuenta = LogicaCuentaEfectivo.ObtenerLogica(
                movimientoComision.Cuenta);

            logicaCuenta.Retirar(movimientoComision.MontoMovimiento, indicadorCuentaSueldo);
            movimientoComision.EstablecerOperacionOrigen(movimientoPrincipal);
            return movimientoComision;
        }
        /// <summary>
        /// Metodo que genera el movimiento contable de comision
        /// </summary>
        /// <param name="movimientoComision">Movimiento de la comision</param>
        /// <param name="cuentaContableComision">Cuenta contable de la comision</param>
        /// <returns>Retorna del movimiento auxiliar logico de la comision</returns>
        public MovimientoAuxiliarLogico GenerarMovimientoContableComision(
           MovimientoDiario movimientoComision,
           string cuentaContableComision)
        {
            var movimientoContable = new MovimientoAuxiliarLogico(
                movimientoComision,
                cuentaContableComision,
                movimientoComision.SubTipoTransaccionMovimiento,
                movimientoComision.DescripcionMovimiento,
                AsientoContableDetalle.CodigoCredito,
                false);
            return movimientoContable;
        }
        /// <summary>
        /// Reversa el monto de operacion de la cuenta
        /// </summary>
        /// <param name="transferencia">transferencia</param>
        /// <param name="movimientoRelacionados">movimiento</param>
        /// <param name="indicadorCuentaSueldo">movimiento</param>
        /// <param name="IndicadorReversarComision">movimiento</param>
        /// <returns>True si la reversion es exitosa</returns>
        public void ReversarTransferenciaInmediata(
            Transferencia transferencia,
            List<MovimientoDiario> movimientoRelacionados,
            string indicadorCuentaSueldo,
            CodigoRespuesta codigoRespuesta,
            bool IndicadorReversarComision)
        {
            if (movimientoRelacionados.Count <= 0)
                throw new Exception("No es cuenta de Movimiento Relaciandos ala Operación.");
            if (transferencia == null)
                throw new Exception("No se pudo Obtener transferencia.");

            var movimientoItf = movimientoRelacionados.Where(x => x.EsTransaccionITF).FirstOrDefault();
            if (movimientoItf != null)
            {
                movimientoItf.Anular();
                movimientoItf.Cuenta.DepositarReversion(movimientoItf.MontoMovimiento);
            }

            foreach (var movimiento in movimientoRelacionados.Where(g => !g.EsTransaccionITF))
            {
                if (movimiento.SubTipoTransaccionMovimiento.EsComisionCCE && !IndicadorReversarComision)
                    return;

                movimiento.Anular();
                movimiento.ActualizarDescripcionEstadoCuenta(codigoRespuesta.DescripcionEstadoCuenta);
                var logicaCuenta = LogicaCuentaEfectivo.ObtenerLogica(movimiento.Cuenta);
                logicaCuenta.Depositar(movimiento.MontoMovimiento, movimiento.IndicadorRemunerativo);
            }

            transferencia.InvalidarTransferencia();
        }
    }

}
