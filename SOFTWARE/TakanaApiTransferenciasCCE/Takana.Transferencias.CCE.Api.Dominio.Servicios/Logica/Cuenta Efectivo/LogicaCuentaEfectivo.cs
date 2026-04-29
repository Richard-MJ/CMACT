using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;
using Takana.Transferencias.CCE.Api.Dominio.Servicios.Logica.Cuenta_Efectivo;
using static Takana.Transferencias.CCE.Api.Dominio.Entidades.CC.LimitesOperacionesCuenta;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Logica
{
    /// <summary>
    /// Logica de la cuenta efecivo
    /// </summary>
    public class LogicaCuentaEfectivo
    {
        protected CuentaEfectivo CuentaEfectivo { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cuentaEfectivo"></param>
        protected LogicaCuentaEfectivo(CuentaEfectivo cuentaEfectivo)
        {
            CuentaEfectivo = cuentaEfectivo;
        }

        /// <summary>
        /// Obtiene la logica de la cuenta efectivo por el producto correspondiente
        /// </summary>
        /// <param name="cuentaEfectivo">Cuenta efectivo</param>
        /// <param name=""></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static LogicaCuentaEfectivo ObtenerLogica(CuentaEfectivo cuentaEfectivo)
        {
            if (cuentaEfectivo.Producto == null)
            {
                throw new Exception($"La cuenta {cuentaEfectivo.NumeroCuenta} no tiene un producto válido.");
            }

            switch (cuentaEfectivo.Producto.CodigoProducto)
            {
                case "CC013":
                case "CC014":
                    return new LogicaCuentaEfectivoPatrono(cuentaEfectivo );

                case "CC063":
                case "CC064":
                    return new LogicaCuentaEfectivoSueldo(cuentaEfectivo);

                case "CC001":
                case "CC002":
                case "CC007":
                case "CC008":
                    return new LogicaCuentaEfectivoNormal(cuentaEfectivo);

                case "CC049":
                case "CC056":
                case "CC057":
                    return new LogicaCuentaEfectivoDevoluciones(cuentaEfectivo);

                case "CC050":
                    return new LogicaCuentaEfectivoDemandados(cuentaEfectivo);

                case "CC065":
                case "CC066":
                    return new LogicaCuentaEfectivoFuturo(cuentaEfectivo);

                case "CC003":
                case "CC004":
                case "CC005":
                case "CC006":
                case "CC009":
                case "CC010":
                case "CC011":
                case "CC012":
                case "CC058":
                case "CC059":
                case "CC060":
                case "CC061":
                    return new LogicaCuentaEfectivoJuridica(cuentaEfectivo);

                case "CC079":
                    return new LogicaCuentaEfectivoAfpIntangible(cuentaEfectivo);

                default:
                    return new LogicaCuentaEfectivoNormal(cuentaEfectivo);
            }
        }

        /// <summary>
        /// Retira un monto de la cuenta efectivo
        /// </summary>
        /// <param name="monto">Monto a retirar de la cuenta</param>
        /// <returns>Monto retirado</returns>
        public virtual MontoCuentaEfectivo Retirar(decimal monto, bool indicadorCuentaSueldo)
        {
            if (!CuentaEfectivo.EsCuentaActiva)
                throw new ValidacionException("Cuenta de ahorros no activa.");
            if (!CuentaEfectivo.PuedeSerDebitada(monto))
                throw new ValidacionException("Saldo insuficiente.");
            CuentaEfectivo.RetirarMonto(monto);

            return MontoCuentaEfectivo.Crear(monto, 0);
        }

        /// <summary>
        /// Genera movimientos para la cuenta efectivo
        /// </summary>
        /// <param name="subTransaccion">Subtransaccion</param>
        /// <param name="descripcionMovimiento">Descripcion de movimientos</param>
        /// <param name="monto">Monto del movimiento</param>
        /// <param name="usuarioActual">Usuario aCTUAL</param>
        /// <param name="codigoSistemaFuente">Codigo del sistema fuente</param>
        /// <param name="fechaActual">fecha del sistema</param>
        /// <returns>Retorna los datos del movimiento generado</returns>
        public virtual IList<MovimientoDiario> GenerarMovimientos(
            SubTipoTransaccion subTransaccion,
            string descripcionMovimiento, 
            MontoCuentaEfectivo monto, 
            Usuario usuarioActual,
            string codigoSistemaFuente, 
            DateTime fechaActual, 
            int numeroMovimiento)
        {
            var movimiento = MovimientoDiario.Crear(CuentaEfectivo, numeroMovimiento, subTransaccion,
                descripcionMovimiento, monto.NoRemunerativo, usuarioActual, fechaActual, codigoSistemaFuente,
                TipoMontoCuentaEfectivo.NoRemunerativo);

            CuentaEfectivo.MovimientosDiarios.Add(movimiento);

            if (monto.NoRemunerativo > 0)
                CuentaEfectivo.ActualizarFechaUltimoMovimiento(fechaActual);

            return new List<MovimientoDiario> { movimiento };
        }

        /// <summary>
        /// Método que desosita a una cuenta efectivo
        /// </summary>
        /// <param name="monto"></param>
        /// <param name="indicadorRemunerativo"></param>
        /// <returns></returns>
        /// <exception cref="ValidacionException"></exception>
        public virtual LogicaCuentaEfectivo Depositar(decimal monto, string? indicadorRemunerativo)
        {
            if (!CuentaEfectivo.SuEstadoEsAdecuadoParaUnDeposito())
                throw new ValidacionException("Estado de cuenta no válida para depósitos.");
            CuentaEfectivo.DepositarReversion(monto);
            return this;
        }

        /// <summary>
        /// Métdo de debitar por operacion ITF
        /// </summary>
        /// <param name="monto"></param>
        /// <returns></returns>
        public virtual LogicaCuentaEfectivo DebitarPorOperacionITF(decimal monto, bool indicadorCuentaSueldo) 
        {
            CuentaEfectivo.DebitarPorOperacionITF(monto);

            return this;
        }

        /// <summary>
        /// Validar limites de montos por cuenta efectivo
        /// </summary>
        public virtual void ValidarLimitesPorCuenta(
            decimal montoTransferencia, 
            int cantidadMovimientos, 
            IContextoAplicacion contexto)
        {
            if (CuentaEfectivo.LimitesOperacionesCuenta.Count() <= 0)
                return;

            var numeroLimiteOperaciones = CuentaEfectivo.LimitesOperacionesCuenta
                .Where(x =>
                    x.IndicadorCanal == contexto.IdCanalOrigen && 
                    x.IndicadorEstado == General.Activo && 
                    x.IdTipoLimite == (int)TipoOperacionLimite.LimiteTransacciones)
                .FirstOrDefault()?.ValorLimite;

            if (numeroLimiteOperaciones != null && cantidadMovimientos >= numeroLimiteOperaciones)
                throw new ValidacionException("Se ha superado la cantidad de transferencias por dia");

            var montoLimiteOperaciones = CuentaEfectivo.LimitesOperacionesCuenta
                .Where(x =>
                    x.IndicadorCanal == contexto.IdCanalOrigen && 
                    x.IndicadorEstado == General.Activo && 
                    x.IdTipoLimite == (int)TipoOperacionLimite.MontoLimite)
                .FirstOrDefault()?.ValorLimite;

            if (montoLimiteOperaciones != null && montoLimiteOperaciones < montoTransferencia)
                throw new ValidacionException("El monto de transferencia es mayor al configurado en tu cuenta");
        }

    }
}
