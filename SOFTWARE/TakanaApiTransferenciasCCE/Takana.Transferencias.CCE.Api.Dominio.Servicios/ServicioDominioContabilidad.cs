using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;
using Takana.Transferencias.CCE.Api.Common.DTOs.CG;
using Takana.Transferencias.CCE.Api.Common.Constantes;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios
{
    /// <summary>
    /// Servicio de maquetacion de las Entradas
    /// </summary>
    public class ServicioDominioContabilidad : IServicioDominioContabilidad
    {
        #region Métodos
        /// <summary>
        /// Método que genera el movimiento auxiliar contable Transaferencias entrante o saliente
        /// </summary>
        /// <param name="movimientosAContabilizar"></param>
        /// <param name="listaTransaccionContabilidad"></param>
        /// <param name="indicadorTransferencia"></param>
        /// <param name="cuentaContableDestino"></param>
        public void GenerarMovimientoAuxiliarContableTransferencia(
            List<MovimientoDiario> movimientosAContabilizar,
            List<ITransaccion> listaTransaccionContabilidad,
            string indicadorTransferencia,
            string cuentaContableDestino)
        {
            foreach (var movimientoDiario in movimientosAContabilizar.Where(m => m.AplicaAsiento))
            {
                var tipoMovimiento = indicadorTransferencia == AsientoContableDetalle.IndicadorEntrante
                    ? AsientoContableDetalle.CodigoDebito : AsientoContableDetalle.CodigoCredito;

                var movimientoAuxiliar = new MovimientoAuxiliarLogico(
                    movimientoDiario,
                    cuentaContableDestino,
                    movimientoDiario.SubTipoTransaccionMovimiento,
                    movimientoDiario.DescripcionAsientoMovimientoContable,
                    tipoMovimiento,
                    false);
                listaTransaccionContabilidad.Add(movimientoAuxiliar);
            }
        }

        /// <summary>
        /// Implementación del método GenerarAsientoContableCompletoPendiente a nivel de dominio
        /// </summary>
        /// <param name="numeroAsiento"></param>
        /// <param name="tipoCambioBase"></param>
        /// <param name="tipoCambioCuenta"></param>
        /// <param name="movimientos"></param>
        /// <returns>Instancia el asiento contable</returns>
        public AsientoContable GenerarAsientoContableCompletoPendiente(
            int numeroAsiento, 
            DateTime fechaSistema,
            decimal tipoCambioBase,
            decimal tipoCambioCuenta, 
            params IMovimientoContable[] movimientos)
        {
            bool considerarMovimientoPrincipal = AsientoContable.considerarMovimientoPrincipal;

            var asiento = GenerarAsientoContableCompletoPendienteNormal(
                numeroAsiento,
                tipoCambioBase,
                tipoCambioCuenta,
                considerarMovimientoPrincipal,
                movimientos
                );

            return asiento;
        }

        /// <summary>
        /// Implementación del método GenerarAsientoContablePorComision a nivel de dominio
        /// </summary>
        /// <param name="numeroAsiento"></param>
        /// <param name="tipoCambioBase"></param>
        /// <param name="tipoCambioCuenta"></param>
        /// <param name="movimientos"></param>
        /// <returns>Instancia el asiento contable</returns>
        public AsientoContable GenerarAsientoContablePorComision(
            int numeroAsiento,
            DateTime fechaSistema,
            decimal tipoCambioBase,
            decimal tipoCambioCuenta,
            params IMovimientoContable[] movimientos)
        {
            bool considerarMovimientoPrincipal = false;

            var asiento = GenerarAsientoContableCompletoPendienteNormal(
                numeroAsiento,
                tipoCambioBase,
                tipoCambioCuenta,
                considerarMovimientoPrincipal,
                movimientos
                );

            return asiento;
        }

        /// <summary>
        /// Genera un detalle de ajuste para un asiento contable en caso de diferencias entre
        /// débitos y créditos. Aplica reglas específicas según si el asiento involucra
        /// múltiples monedas o transacciones de depósito.
        /// </summary>
        /// <param name="asientoContable">Asiento contable sobre el cual se generará el ajuste.</param>
        /// <param name="cuentaAjuste">Información de la cuenta contable que se utilizará para el ajuste.</param>
        /// <returns>
        /// El <see cref="AsientoContableDetalle"/> generado como ajuste,
        /// o <c>null</c> si no existen diferencias entre débitos y créditos.
        /// </returns>
        public AsientoContableDetalle GenerarAjuste(AsientoContable asientoContable, CuentaContableInfoDTO cuentaAjuste)
        {
            var montoDiferencia = asientoContable.DiferenciaDebitosCreditos();
            var montoDiferenciaCuenta = asientoContable.DiferenciaDebitosCreditosCuenta();

            if (montoDiferencia == 0) return null;

            string descripcionLinea = "AJUSTE DIF CAMBIARIO";

            bool debeAgregarAjuste = false;

            if ((montoDiferenciaCuenta != 0 && asientoContable.EsDetalleEntreDiferentesMonedas()) ||
                (montoDiferenciaCuenta == 0 && Math.Abs(montoDiferencia) > 0 && asientoContable.esTransaccionDeposito()))
            {
                debeAgregarAjuste = true;
            }
            else if (!asientoContable.EsDetalleConMonedaDolares())
            {
                throw new Exception("El Asiento debe contener una cuenta en dólares.");
            }
            else
            {
                debeAgregarAjuste = true;
            }

            if (debeAgregarAjuste)
            {
                return asientoContable.AgregarDetalleAjuste(
                    cuentaAjuste.TipoAsiento,
                    cuentaAjuste.NumeroCuenta,
                    Math.Abs(montoDiferencia),
                    cuentaAjuste.CodigoUnidadEjecutora,
                    cuentaAjuste.TasaCambioLocal,
                    cuentaAjuste.TasaCambioCuenta,
                    descripcionLinea,
                    string.Empty
                );
            }

            return null;
        }

        /// <summary>
        /// Generar asiento contable de manera pendiente
        /// </summary>
        /// <param name="numeroAsiento"></param>
        /// <param name="tipoCambioBase"></param>
        /// <param name="tipoCambioCuenta"></param>
        /// <param name="movimientos"></param>
        /// <returns>Retorna el asiento contable</returns>
        /// <exception cref="ValidacionException">Verifica si existe operacion principal</exception>
        private AsientoContable? GenerarAsientoContableCompletoPendienteNormal(int numeroAsiento, decimal tipoCambioBase,
            decimal tipoCambioCuenta, bool considerarMovimientoPrincipal, params IMovimientoContable[] movimientos)
        {
            var tipoCambioBasePrincipal = 0m;
            if (movimientos == null || movimientos.Count() <= 0)
                throw new ValidacionException("No existen detalles para procesar el asiento contable.");
            IMovimientoContable? movimientoPrincipal;
            if (considerarMovimientoPrincipal)
                movimientoPrincipal = movimientos.FirstOrDefault(o => o.EsPrincipal);         
            else
                movimientoPrincipal = movimientos.FirstOrDefault();
            if (movimientoPrincipal == null) throw new ValidacionException("No existe operacion principal para procesar el asiento");
            if (!movimientoPrincipal.AplicaAsiento) return null;

            AsientoContable asiento = AsientoContable.Generar(numeroAsiento, Empresa.CodigoPrincipal, movimientoPrincipal.CodigoAgencia
                , movimientoPrincipal.CodigoSistema, movimientoPrincipal.CodigoTipoTransaccion, movimientoPrincipal.CodigoSubTipoTransaccion
                , movimientoPrincipal.FechaMovimiento, movimientoPrincipal.SubTipoTransaccionMovimiento.DescripcionSubTransaccion, "N"
                , movimientoPrincipal.CodigoUsuario);

            foreach (var movimiento in movimientos.Where(o => o.AplicaAsiento && o.MontoMovimientoContable > 0))
            {
                var tipoCambioPrincipal = (movimiento.CuentaContable.Substring(0, 1) == AsientoContable.cuentaContableValida && tipoCambioBasePrincipal > 0);
                var detalle = AsientoContableDetalle.Generar(asiento, movimiento.TipoCuentaContable, movimiento.CuentaContable
                    , movimiento.MontoMovimientoContable, movimiento.CodigoUnidadEjecutora, movimiento.DescripcionAsientoMovimientoContable
                    , movimiento.ReferenciaMovimientoContable.Length > 35 ? movimiento.ReferenciaMovimientoContable.Substring(0, 34)
                    : movimiento.ReferenciaMovimientoContable
                    , tipoCambioPrincipal ? tipoCambioBasePrincipal : movimiento.TasaCambioLocal == 0 ? tipoCambioBase : movimiento.TasaCambioLocal
                    , tipoCambioPrincipal ? tipoCambioBasePrincipal : movimiento.TasaCambioCuenta == 0 ? tipoCambioCuenta : movimiento.TasaCambioCuenta);
                asiento.AgregarDetalle(detalle);
                movimiento.EstablecerAsiento(asiento);
            }
            return asiento;
        }

        /// <summary>
        /// Metodo que verifica la consistencia del Asiento antes de pasarlo a estado Pendiente
        /// </summary>
        /// <param name="asientoContable">Instancia de la clase AsientoContable a verificar</param>
        /// <returns>Instancia de la clase AsientoContable a verificada</returns>
        public AsientoContable CerrarAsientoContable(AsientoContable asientoContable)
        {
            decimal diferencia = asientoContable.DiferenciaDebitosCreditos();
            if (diferencia != 0)
            {
                throw new Exception("Los débitos son diferentes a los créditos: " + diferencia.ToString());
            }
            asientoContable.CerrarAsiento(AsientoContable.EstadoPendiente);
            return asientoContable;
        }

        /// <summary>
        /// Método de Generar Detalle Asiento Contable de Transferencias Interbancarias Entrantes de Cuentas Puente
        /// </summary>
        /// <param name="asiento">Asiento Contable</param>
        /// <param name="movimiento">Movimiento Principal</param>
        /// <param name="montoComision">Monto de la Comisión</param>        
        public void GenerarDetalleAsientoDeCuentaPuenteTransferenciaEntrante(
            SubTipoTransaccion subTipoTransaccionTransferencia,
            AsientoContable asiento,
            IMovimientoContable? movimiento,
            List<CuentaContableInfoDTO> cuentasPuente,
            decimal montoComision,
            ITasaCambio tipoCambioContabilidad,
            string descripcion,
            DateTime fechaSistema)
        {
            foreach (var cuenta in cuentasPuente)
            {
                var referencia = movimiento!.ReferenciaMovimientoContable.Length > 35
                    ? movimiento.ReferenciaMovimientoContable.Substring(0, 34) : movimiento.ReferenciaMovimientoContable;

                var detalle = AsientoContableDetalle.Generar(asiento, cuenta.TipoAsiento, cuenta.NumeroCuenta
                    , montoComision, movimiento.CodigoUnidadEjecutora, descripcion, referencia,
                    cuenta.TasaCambioLocal, cuenta.TasaCambioCuenta);
                asiento.AgregarDetalle(detalle);
            }
        }

        /// <summary>
        /// Método de Generar Asiento y Detalle Contable de Transferencias Interbancarias Entrantes de Cuentas Puente de solo la Comisión
        /// </summary>
        /// <param name="cuentasPuente">Cuentas puentes</param>
        /// <param name="numeroAsiento">Numero de Asiento</param>
        /// <param name="montoComision">Monto de la Comisión</param>
        /// <param name="subTipoTransaccionTransferencia">Sub tipo de Transaccion</param>
        /// <param name="fechaSistema">Fecha del sistema</param>
        /// <param name="usuario">Usuario </param>
        /// <param name="numeroAsiento">Usuario </param>
        /// <returns>Instancia nueva de la clase AsientoContable</returns>   
        public AsientoContable GenerarAsientoContableCompletoPendienteTransferenciaComisionEntrante(
            List<CuentaContableInfoDTO> cuentasPuente
            , decimal montoComision
            , SubTipoTransaccion subTipoTransaccionTransferencia
            , DateTime fechaSistema
            , Usuario usuario
            , int numeroAsiento
            , ITasaCambio tipoCambioContabilidad)
        {
            AsientoContable asientoComisionEntrante = AsientoContable.Generar(numeroAsiento, Empresa.CodigoPrincipal, usuario.CodigoAgencia
                , subTipoTransaccionTransferencia.CodigoSistema, subTipoTransaccionTransferencia.CodigoTipoTransaccion,
                subTipoTransaccionTransferencia.CodigoSubTipoTransaccion, fechaSistema, subTipoTransaccionTransferencia.DescripcionSubTransaccion,
                AsientoContable.NoLiquidacion, usuario.CodigoUsuario);

            foreach (var cuenta in cuentasPuente)
            {               
                var detalle = AsientoContableDetalle.Generar(asientoComisionEntrante, cuenta.TipoAsiento, cuenta.NumeroCuenta
                    , montoComision, AsientoContable.CodigoUnidadEjecutora, cuenta.DetalleCuenta
                    , "", cuenta.TasaCambioLocal, cuenta.TasaCambioCuenta);
                asientoComisionEntrante.AgregarDetalle(detalle);
            }
        
            return asientoComisionEntrante;
        }

        /// <summary>
        /// Implementación del método AnularAsientoContablePendiente a nivel de dominio
        /// </summary>
        /// <param name="asientoContable">Instancia de la clase AsientoContable</param>
        /// <returns>La misma instancia de la clase AsientoContable</returns>
        public AsientoContable AnularAsientoContablePendiente(AsientoContable asientoContable)
        {
            if (!asientoContable.EstaPendienteAplicacion())
                throw new ApplicationException("El asiento contable no se encuentra en estado Pendiente");
            asientoContable.CerrarAsiento(AsientoContable.EstadoAnulado);
            return asientoContable;
        }

        #endregion
    }
}
