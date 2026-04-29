using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;
using Takana.Transferencias.CCE.Api.Common.SolicitudEstadoPago;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.Cancelaciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Rechazos;


namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Servicios
{
    /// <summary>
    /// Clase de capa Aplicacion para el control de servicio de las bitacorizaciones
    /// </summary>
    public class ServicioAplicacionBitacora : IServicioAplicacionBitacora
    {

        private readonly IRepositorioGeneral _repositorioGeneral;
        private readonly IRepositorioOperacion _repositorioOperacion;
        private readonly IRepositorioEscritura _repositorioEscritura;

        public ServicioAplicacionBitacora(
            IRepositorioGeneral repositorioGeneral,
            IRepositorioOperacion repositorioOperacion,
            IRepositorioEscritura repositorioEscritura)
        {
            _repositorioGeneral = repositorioGeneral;
            _repositorioOperacion = repositorioOperacion;
            _repositorioEscritura = repositorioEscritura;
        }

        #region Registrar Bitacora
        /// <summary>
        /// Registra una bitácora para la operación de consulta de cuenta de recepción.
        /// </summary>
        /// <param name="datos">Datos de la consulta de cuenta de recepción.</param>
        /// <param name="fechaSistema">Fecha y hora del sistema.</param>
        /// <returns>Una instancia de <see cref="BitacoraTransferenciaInmediata"/> con la información registrada.</returns>
        public BitacoraTransferenciaInmediata RegistrarBitacora(ConsultaCuentaRecepcionEntradaDTO datos, DateTime fechaSistema)
        {
            var bitacora = BitacoraTransferenciaInmediata.RegistrarBitacora(datos, fechaSistema);
            _repositorioGeneral.Adicionar(bitacora);
            _repositorioGeneral.GuardarCambios();
            return bitacora;
        }

        /// <summary>
        /// Registra una bitácora para la operación de orden de transferencia de recepción.
        /// </summary>
        /// <param name="datos">Datos de la orden de transferencia de recepción.</param>
        /// <param name="fechaSistema">Fecha y hora del sistema.</param>
        /// <returns>Una instancia de <see cref="BitacoraTransferenciaInmediata"/> con la información registrada.</returns>
        public BitacoraTransferenciaInmediata RegistrarBitacora(OrdenTransferenciaRecepcionEntradaDTO datos, DateTime fechaSistema)
        {
            var bitacora = BitacoraTransferenciaInmediata.RegistrarBitacora(datos, fechaSistema);
            _repositorioOperacion.Adicionar(bitacora);
            _repositorioOperacion.GuardarCambios();
            return bitacora;
        }

        /// <summary>
        /// Registra una transacción para una orden de transferencia de recepción inmediata.
        /// </summary>
        /// <param name="datosTransaccion">Datos de la transacción de orden de transferencia.</param>
        /// <param name="datosRespuesta">Datos de la respuesta de la orden de transferencia.</param>
        /// <param name="clienteReceptor">Datos del cliente receptor de la transferencia.</param>
        /// <param name="fechaSistema">Fecha y hora del sistema.</param>
        /// <returns>Una instancia de <see cref="TransaccionOrdenTransferenciaInmediata"/> con la transacción registrada.</returns>
        public TransaccionOrdenTransferenciaInmediata RegistrarTransaccion(
            OrdenTransferenciaRecepcionEntradaDTO datosTransaccion,
            OrdenTransferenciaRespuestaEntradaDTO datosRespuesta, 
            ClienteReceptorDTO clienteReceptor,
            DateTime fechaSistema)
        {
            var transaccion = TransaccionOrdenTransferenciaInmediata
                .RegistrarTransaccion(datosTransaccion, datosRespuesta, clienteReceptor, fechaSistema);
            _repositorioOperacion.Adicionar(transaccion);
            return transaccion;
        }

        /// <summary>
        /// Registra una bitácora para la operación de confirmación de orden de transferencia.
        /// </summary>
        /// <param name="datos">Datos de la confirmación de la orden de transferencia.</param>
        /// <param name="fechaSistema">Fecha y hora del sistema.</param>
        /// <returns>Una instancia de <see cref="BitacoraTransferenciaInmediata"/> con la información registrada.</returns>
        public BitacoraTransferenciaInmediata RegistrarBitacora(OrdenTransferenciaConfirmacionEntradaDTO datos, DateTime fechaSistema)
        {
            var bitacora = BitacoraTransferenciaInmediata.RegistrarBitacora(datos, fechaSistema);
            _repositorioOperacion.Adicionar(bitacora);
            _repositorioOperacion.GuardarCambios();
            return bitacora;
        }

        /// <summary>
        /// Registra una bitácora para la operación de cancelación de recepción.
        /// </summary>
        /// <param name="datos">Datos de la cancelación de la recepción.</param>
        /// <param name="fechaSistema">Fecha y hora del sistema.</param>
        /// <returns>Una instancia de <see cref="BitacoraTransferenciaInmediata"/> con la información registrada.</returns>
        public BitacoraTransferenciaInmediata RegistrarBitacora(CancelacionRecepcionDTO datos, DateTime fechaSistema)
        {
            var bitacora = BitacoraTransferenciaInmediata.RegistrarBitacora(datos, fechaSistema);
            _repositorioGeneral.Adicionar(bitacora);
            _repositorioGeneral.GuardarCambios();
            return bitacora;
        }

        /// <summary>
        /// Registra una bitácora para la operación de rechazo de recepción.
        /// </summary>
        /// <param name="datos">Datos del rechazo de la recepción.</param>
        /// <param name="fechaSistema">Fecha y hora del sistema.</param>
        /// <returns>Una instancia de <see cref="BitacoraTransferenciaInmediata"/> con la información registrada.</returns>
        public BitacoraTransferenciaInmediata RegistrarBitacora(RechazoRecepcionDTO datos, DateTime fechaSistema)
        {
            var bitacora = BitacoraTransferenciaInmediata.RegistrarBitacora(datos, fechaSistema);
            _repositorioGeneral.Adicionar(bitacora);
            _repositorioGeneral.GuardarCambios();
            return bitacora;
        }

        /// <summary>
        /// Registra una bitácora para la solicitud de estado de pago.
        /// </summary>
        /// <param name="datos">Datos de la solicitud de estado de pago.</param>
        /// <param name="fechaSistema">Fecha y hora del sistema.</param>
        /// <returns>Una instancia de <see cref="BitacoraTransferenciaInmediata"/> con la información registrada.</returns>
        public BitacoraTransferenciaInmediata RegistrarBitacora(SolicitudEstadoPagoSalidaDTO datos, DateTime fechaSistema)
        {
            var bitacora = BitacoraTransferenciaInmediata.RegistrarBitacora(datos, fechaSistema);
            _repositorioOperacion.Adicionar(bitacora);
            _repositorioOperacion.GuardarCambios();
            return bitacora;
        }

        /// <summary>
        /// Registra una bitácora para la operación de consulta de cuenta de salida, con la posibilidad de consulta por QR.
        /// </summary>
        /// <param name="datos">Datos de la consulta de cuenta de salida.</param>
        /// <param name="fechaSistema">Fecha y hora del sistema.</param>
        /// <param name="consultaPorQR">Indica si la consulta es por código QR.</param>
        /// <returns>Una instancia de <see cref="BitacoraTransferenciaInmediata"/> con la información registrada.</returns>
        public BitacoraTransferenciaInmediata RegistrarBitacora(ConsultaCuentaSalidaDTO datos, DateTime fechaSistema, bool consultaPorQR)
        {
            var bitacora = BitacoraTransferenciaInmediata.RegistrarBitacora(datos, fechaSistema, consultaPorQR);
            _repositorioOperacion.Adicionar(bitacora);
            _repositorioOperacion.GuardarCambios();
            return bitacora;
        }

        /// <summary>
        /// Registra una bitácora para la operación de orden de transferencia de salida, incluyendo los datos del canal.
        /// </summary>
        /// <param name="datos">Datos de la orden de transferencia de salida.</param>
        /// <param name="datosCanal">Datos del canal desde el cual se realiza la transferencia.</param>
        /// <param name="fechaSistema">Fecha y hora del sistema.</param>
        /// <returns>Una instancia de <see cref="BitacoraTransferenciaInmediata"/> con la información registrada.</returns>
        public BitacoraTransferenciaInmediata RegistrarBitacora(
            OrdenTransferenciaSalidaDTO datos, 
            OrdenTransferenciaCanalDTO datosCanal, 
            DateTime fechaSistema)
        {
            var bitacora = BitacoraTransferenciaInmediata.RegistrarBitacora(datos, datosCanal, fechaSistema);
            _repositorioEscritura.Adicionar(bitacora);
            _repositorioEscritura.GuardarCambios();
            return bitacora;
        }

        /// <summary>
        ///  Actualiza la bitacora despues de la respuesta de la orden de transferencia
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="datosBitacora"></param>
        /// <param name="fechaSistema"></param>
        public void ActualizarBitacora(BitacoraTransferenciaInmediata bitacora, OrdenTransferenciaRecepcionSalidaDTO datosBitacora, DateTime fechaSistema)
        {
            bitacora.ActualizarBitacora(datosBitacora, fechaSistema);
            _repositorioEscritura.GuardarCambios();
        }

        /// <summary>
        /// Actualiza la bitácora con una razón específica.
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="razonBitacora"></param>
        /// <param name="fechaSistema"></param>
        public void ActualizarBitacora(BitacoraTransferenciaInmediata bitacora, string razonBitacora, DateTime fechaSistema)
        {
            bitacora.ActualizarBitacora(razonBitacora, fechaSistema);
            _repositorioEscritura.GuardarCambios();
        }

        /// <summary>
        /// Registra una transacción para una orden de transferencia de salida, incluyendo los datos del canal.
        /// </summary>
        /// <param name="datos">Datos de la orden de transferencia de salida.</param>
        /// <param name="datosCanal">Datos del canal desde el cual se realiza la transferencia.</param>
        /// <param name="fechaSistema">Fecha y hora del sistema.</param>
        /// <returns>Una instancia de <see cref="TransaccionOrdenTransferenciaInmediata"/> con la transacción registrada.</returns>
        public TransaccionOrdenTransferenciaInmediata RegistrarTransaccion(
            OrdenTransferenciaSalidaDTO datos, 
            OrdenTransferenciaCanalDTO datosCanal,
            AsientoContable asiento,
            Transferencia transferencia,
            DateTime fechaSistema)
        {
            var transaccion = TransaccionOrdenTransferenciaInmediata
                .RegistrarTransaccion(datos, datosCanal, asiento, transferencia, fechaSistema);
            _repositorioOperacion.Adicionar(transaccion);
            return transaccion;
        }

        /// <summary>
        /// Registra una trama procesada.
        /// </summary>
        /// <param name="datos"></param>
        /// <param name="contexto"></param>
        /// <param name="numeroAsiento"></param>
        /// <param name="numeroMovimiento"></param>
        /// <param name="fechaSistema"></param>
        /// <returns></returns>
        public TramaProcesada RegistrarTramaProcesada(
            OrdenTransferenciaSalidaDTO datos,
            IContextoAplicacion contexto,
            int numeroAsiento,
            int numeroMovimiento,
            DateTime fechaSistema)
        {
            var tramaARegistrar = TramaProcesada.RegistrarDatosTrama(
                contexto.IdUsuarioAutenticado, contexto.IdCanalOrigen,
                numeroMovimiento, numeroAsiento,
                TramaProcesada.TipoTramaRequest,
                TramaProcesada.TipoTramaTransferenciasCCE,
                datos.currency, contexto.IdTerminalOrigen,
                datos.amount, datos.trace,
                fechaSistema, datos.debtorCCI,
                datos.creditorCCI ?? datos.creditorCreditCard,
                fechaSistema, Sistema.CuentaEfectivo);

            _repositorioOperacion.Adicionar(tramaARegistrar);
            return tramaARegistrar;
        }

        #endregion
    }
}
