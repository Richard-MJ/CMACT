using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CJ;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Logica
{
    public class MovimientoLavadoAuxiliarLogico : IOperacionLavado
    {
        #region Propiedades
        /// <summary>
        /// Indicador si realiza operación lavado
        /// </summary>
        public bool EsOperacionPrincipalLavado { get; }
        /// <summary>
        /// Indicador si es registro de origemn
        /// </summary>
        public bool EsRegistroOrigen { get; }
        /// <summary>
        /// Código de la agencia de operación
        /// </summary>
        public string CodigoAgencia { get; }
        /// <summary>
        /// Código del usuario
        /// </summary>
        public string CodigoUsuario { get; }
        /// <summary>
        /// Fecha de la operación
        /// </summary>
        public DateTime FechaOperacion { get; }
        /// <summary>
        /// Entidad de la sub transacción
        /// </summary>
        public SubTipoTransaccion SubTipoTransaccionMovimiento { get; }
        /// <summary>
        /// Moneda de operación
        /// </summary>
        public string MonedaOperacion { get; }
        /// <summary>
        /// Monto de la operación
        /// </summary>
        public decimal MontoOperacion { get; }
        /// <summary>
        /// Número de la cuenta de origen
        /// </summary>
        public string NumeroCuenta { get; }
        /// <summary>
        /// Número del movimiento
        /// </summary>
        public decimal NumeroMovimiento { get; }
        /// <summary>
        /// Código de la forma de pago del lavado
        /// </summary>
        public string FormaDePagoLavado { get; }
        /// <summary>
        /// Número del asiento del lavado
        /// </summary>
        public int NumeroAsientoLavado { get; }
        /// <summary>
        /// Código del sistema
        /// </summary>
        public string CodigoSistema { get; }
        /// <summary>
        /// ´Código de la entidad SBS
        /// </summary>
        public string CodigoEntidadSbs { get; }
        /// <summary>
        /// Indicadotr del movimiento
        /// </summary>
        public string IndicadorMovimiento { get; }
        /// <summary>
        /// código del sistmea fuente
        /// </summary>
        public string CodigoSistemaFuente { get; }
        /// <summary>
        /// Interfaz del tipo de intervinente
        /// </summary>
        public TipoInteviniente TipoInterviniente { get; }
        /// <summary>
        /// Interfaz del intervinente
        /// </summary>
        public IInterviniente Interviniente { get; }
        #endregion Propiedades

        /// <summary>
        /// Comnstrusctor del movimiento auxiliar
        /// </summary>
        /// <param name="movimientoOrigen">movimiento diario de origen</param>
        /// <param name="transaccionDetalle">detalle de la transacción</param>
        public MovimientoLavadoAuxiliarLogico(
            MovimientoDiario movimientoOrigen,
            TransferenciaDetalleSalienteCCE transaccionDetalle)
        {
            SubTipoTransaccionMovimiento = movimientoOrigen.SubTipoTransaccionMovimiento;
            EsOperacionPrincipalLavado = false;
            EsRegistroOrigen = false;
            MontoOperacion = transaccionDetalle.MontoOperacion;
            NumeroCuenta = transaccionDetalle.CodigoCuentaInterbancario;
            CodigoEntidadSbs = transaccionDetalle.EntidadDestino.CodigoEntidadSbs;
            IndicadorMovimiento = movimientoOrigen.SubTipoTransaccionMovimiento.IndicadorMovimientoLavando;
            CodigoSistemaFuente = movimientoOrigen.CodigoSistemaFuente;
            CodigoAgencia = movimientoOrigen.CodigoAgencia;
            CodigoUsuario = movimientoOrigen.CodigoUsuario;
            FechaOperacion = movimientoOrigen.FechaMovimiento;
            MonedaOperacion = movimientoOrigen.MonedaOperacion;
            NumeroMovimiento = movimientoOrigen.NumeroOperacion;
            FormaDePagoLavado = movimientoOrigen.FormaDePagoLavado;
            NumeroAsientoLavado = Convert.ToInt32(movimientoOrigen.NumeroAsiento);
            CodigoSistema = movimientoOrigen.CodigoSistema;
            Interviniente = IntervinenteLavadoAuxiliarLogico.Crear(transaccionDetalle);
        }

        /// <summary>
        /// Clase que define al intervinente de lavado auxliar
        /// </summary>
        public class IntervinenteLavadoAuxiliarLogico : IInterviniente
        {
            /// <summary>
            /// Código del tipo de intervinente
            /// </summary>
            public int CodigoTipoInterviniente { get; set; }
            /// <summary>
            /// Código del tipo de documento
            /// </summary>
            public string CodigoTipoDocumento { get; set; }
            /// <summary>
            /// número de documento del intervinente
            /// </summary>
            public string NumeroDocumento { get; set; }
            /// <summary>
            /// Indicador si es cliente
            /// </summary>
            public bool EsCliente { get; set; }
            /// <summary>
            /// Código del cliente
            /// </summary>
            public string CodigoCliente { get; set; }
            /// <summary>
            /// Apellido paterni del intervinente
            /// </summary>
            public string ApellidoPaterno { get; set; }
            /// <summary>
            /// Apellido materno del intervinente
            /// </summary>
            public string ApellidoMaterno { get; set; }
            /// <summary>
            /// Nombre del intervinnete
            /// </summary>
            public string Nombres { get; set; }
            /// <summary>
            /// Método estático para crear el intervinente auxiliar
            /// </summary>
            /// <param name="transaccionDetalle">datos del detalle de la transferencia CCE</param>
            /// <returns>intervinente auxliar creado</returns>
            public static IntervinenteLavadoAuxiliarLogico Crear(
                TransferenciaDetalleSalienteCCE transaccionDetalle)
            {
                return new IntervinenteLavadoAuxiliarLogico()
                {
                    CodigoTipoInterviniente = 1,
                    CodigoTipoDocumento = transaccionDetalle.CodigoTipoDocumento,
                    NumeroDocumento = transaccionDetalle.NumeroDocumento,
                    EsCliente = transaccionDetalle.MismoTitular != Cliente.OtroTitular,
                    CodigoCliente = string.Empty,
                    ApellidoPaterno = transaccionDetalle.ApellidoPaterno,
                    ApellidoMaterno = transaccionDetalle.ApellidoMaterno,
                    Nombres = transaccionDetalle.Nombres
                };
            }
        }

    }
}

