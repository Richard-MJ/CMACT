using Takana.Transferencias.CCE.Api.Common.Cancelaciones;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;
using Takana.Transferencias.CCE.Api.Common.Rechazos;
using Takana.Transferencias.CCE.Api.Common.SolicitudEstadoPago;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;

namespace Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion
{
    public interface IServicioAplicacionBitacora
    {
        /// <summary>
        /// Registra una bitácora para la operación de consulta de cuenta de recepción.
        /// </summary>
        /// <param name="datos">Datos de la consulta de cuenta de recepción.</param>
        /// <param name="fechaSistema">Fecha y hora del sistema.</param>
        /// <returns>Una instancia de <see cref="BitacoraTransferenciaInmediata"/> con la información registrada.</returns>
        BitacoraTransferenciaInmediata RegistrarBitacora(ConsultaCuentaRecepcionEntradaDTO datos, DateTime fechaSistema);

        /// <summary>
        /// Registra una bitácora para la operación de orden de transferencia de recepción.
        /// </summary>
        /// <param name="datos">Datos de la orden de transferencia de recepción.</param>
        /// <param name="fechaSistema">Fecha y hora del sistema.</param>
        /// <returns>Una instancia de <see cref="BitacoraTransferenciaInmediata"/> con la información registrada.</returns>
        BitacoraTransferenciaInmediata RegistrarBitacora(OrdenTransferenciaRecepcionEntradaDTO datos, DateTime fechaSistema);

        /// <summary>
        /// Registra una transacción para una orden de transferencia de recepción inmediata.
        /// </summary>
        /// <param name="datosTransaccion">Datos de la transacción de orden de transferencia.</param>
        /// <param name="datosRespuesta">Datos de la respuesta de la orden de transferencia.</param>
        /// <param name="clienteReceptor">Datos del cliente receptor de la transferencia.</param>
        /// <param name="fechaSistema">Fecha y hora del sistema.</param>
        /// <returns>Una instancia de <see cref="TransaccionOrdenTransferenciaInmediata"/> con la transacción registrada.</returns>
        TransaccionOrdenTransferenciaInmediata RegistrarTransaccion(
            OrdenTransferenciaRecepcionEntradaDTO datosTransaccion,
            OrdenTransferenciaRespuestaEntradaDTO datosRespuesta, 
            ClienteReceptorDTO clienteReceptor,
            DateTime fechaSistema);

        /// <summary>
        /// Registra una bitácora para la operación de confirmación de orden de transferencia.
        /// </summary>
        /// <param name="datos">Datos de la confirmación de la orden de transferencia.</param>
        /// <param name="fechaSistema">Fecha y hora del sistema.</param>
        /// <returns>Una instancia de <see cref="BitacoraTransferenciaInmediata"/> con la información registrada.</returns>
        BitacoraTransferenciaInmediata RegistrarBitacora(OrdenTransferenciaConfirmacionEntradaDTO datos, DateTime fechaSistema);

        /// <summary>
        /// Registra una bitácora para la operación de cancelación de recepción.
        /// </summary>
        /// <param name="datos">Datos de la cancelación de la recepción.</param>
        /// <param name="fechaSistema">Fecha y hora del sistema.</param>
        /// <returns>Una instancia de <see cref="BitacoraTransferenciaInmediata"/> con la información registrada.</returns>
        BitacoraTransferenciaInmediata RegistrarBitacora(CancelacionRecepcionDTO datos, DateTime fechaSistema);

        /// <summary>
        /// Registra una bitácora para la operación de rechazo de recepción.
        /// </summary>
        /// <param name="datos">Datos del rechazo de la recepción.</param>
        /// <param name="fechaSistema">Fecha y hora del sistema.</param>
        /// <returns>Una instancia de <see cref="BitacoraTransferenciaInmediata"/> con la información registrada.</returns>
        BitacoraTransferenciaInmediata RegistrarBitacora(RechazoRecepcionDTO datos, DateTime fechaSistema);

        /// <summary>
        /// Registra una bitácora para la solicitud de estado de pago.
        /// </summary>
        /// <param name="datos">Datos de la solicitud de estado de pago.</param>
        /// <param name="fechaSistema">Fecha y hora del sistema.</param>
        /// <returns>Una instancia de <see cref="BitacoraTransferenciaInmediata"/> con la información registrada.</returns>
        BitacoraTransferenciaInmediata RegistrarBitacora(SolicitudEstadoPagoSalidaDTO datos, DateTime fechaSistema);

        /// <summary>
        /// Registra una bitácora para la operación de consulta de cuenta de salida, con la posibilidad de consulta por QR.
        /// </summary>
        /// <param name="datos">Datos de la consulta de cuenta de salida.</param>
        /// <param name="fechaSistema">Fecha y hora del sistema.</param>
        /// <param name="consultaPorQR">Indica si la consulta es por código QR.</param>
        /// <returns>Una instancia de <see cref="BitacoraTransferenciaInmediata"/> con la información registrada.</returns>
        BitacoraTransferenciaInmediata RegistrarBitacora(ConsultaCuentaSalidaDTO datos, DateTime fechaSistema, bool consultaPorQR);

        /// <summary>
        /// Registra una bitácora para la operación de orden de transferencia de salida, incluyendo los datos del canal.
        /// </summary>
        /// <param name="datosBitacora">Datos de la orden de transferencia de salida.</param>
        /// <param name="datosCanal">Datos del canal desde el cual se realiza la transferencia.</param>
        /// <param name="fechaSistema">Fecha y hora del sistema.</param>
        /// <returns>Una instancia de <see cref="BitacoraTransferenciaInmediata"/> con la información registrada.</returns>
        BitacoraTransferenciaInmediata RegistrarBitacora(OrdenTransferenciaSalidaDTO datosBitacora,
            OrdenTransferenciaCanalDTO datosCanal, DateTime fechaSistema);

        /// <summary>
        ///  Actualiza la bitacora despues de la respuesta de la orden de transferencia
        /// </summary>
        /// <param name="datosBitacora">Bitacora ya registrada antes del envio</param>
        /// <param name="datosBitacora">Datos de orden de transferencia</param>
        void ActualizarBitacora(BitacoraTransferenciaInmediata bitacora,OrdenTransferenciaRecepcionSalidaDTO datosBitacora, DateTime fechaSistema);

        /// <summary>
        /// Actualiza la bitácora con una razón específica.
        /// </summary>
        /// <param name="razonBitacora"></param>
        /// <param name="fechaSistema"></param>
        void ActualizarBitacora(BitacoraTransferenciaInmediata bitacora,string razonBitacora, DateTime fechaSistema);

        /// <summary>
        /// Registra una transacción para una orden de transferencia de salida, incluyendo los datos del canal.
        /// </summary>
        /// <param name="datos">Datos de la orden de transferencia de salida.</param>
        /// <param name="datosCanal">Datos del canal desde el cual se realiza la transferencia.</param>
        /// <param name="fechaSistema">Fecha y hora del sistema.</param>
        /// <returns>Una instancia de <see cref="TransaccionOrdenTransferenciaInmediata"/> con la transacción registrada.</returns>
        TransaccionOrdenTransferenciaInmediata RegistrarTransaccion(
            OrdenTransferenciaSalidaDTO datos, 
            OrdenTransferenciaCanalDTO datosCanal,
            AsientoContable asiento,
            Transferencia transferencia,
            DateTime fechaSistema);

        /// <summary>
        /// Registra una trama procesada.
        /// </summary>
        /// <param name="datos"></param>
        /// <param name="contexto"></param>
        /// <param name="numeroAsiento"></param>
        /// <param name="numeroMovimiento"></param>
        /// <param name="fechaSistema"></param>
        /// <returns></returns>
        TramaProcesada RegistrarTramaProcesada(
            OrdenTransferenciaSalidaDTO datos,
            IContextoAplicacion contexto,
            int numeroAsiento,
            int numeroMovimiento,
            DateTime fechaSistema);

    }
}
