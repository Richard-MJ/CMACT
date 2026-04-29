using System.Transactions;
using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Common.EchoTest;
using Takana.Transferencias.CCE.Api.Common.Rechazos;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Dominio.Servicios;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Common.Cancelaciones;
using Takana.Transferencias.CCE.Api.Common.Notificaciones;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.DTOs.Notificaciones;
using Takana.Transferencias.CCE.Api.Common.ReprocesarTransaccion;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;
using static Takana.Transferencias.CCE.Api.Dominio.Entidades.CF.Moneda;
using Takana.Transferencias.CCE.Api.Common.Constantes.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Extensiones;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion
{
    /// <summary>
    /// Servicio base de Aplicacion de transferencias de entradas.
    /// </summary>
    public class ServicioAplicacionTransferenciaEntrada : ServicioBase, IServicioAplicacionTransferenciaEntrada
    {
        #region Declaraciones
        private readonly IRepositorioGeneral _repositorioGeneral;
        private readonly IRepositorioOperacion _repositorioOperacion;
        private readonly IServicioAplicacionColas _servicioAplicacionColas;
        private readonly IServicioAplicacionCliente _servicioAplicacionCliente;
        private readonly IServicioAplicacionPeticion _servicioaplicacionPeticion;
        private readonly IServicioAplicacionBitacora _servicioAplicacionBitacora;
        private readonly IServicioAplicacionParametroGeneral _servicioAplicacionParametro;
        private readonly IServicioAplicacionTransaccionOperacion _servicioAplicacionTransaccionOperacion;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor de Clase
        /// </summary>
        public ServicioAplicacionTransferenciaEntrada(
            IRepositorioGeneral repositoGeneral,
            IContextoAplicacion contextoAplicacion,
            IRepositorioOperacion repositorioOperacion,
            IServicioAplicacionColas servicioAplicacionColas,
            IServicioAplicacionCliente servicioAplicacionCliente,
            IServicioAplicacionPeticion aplicacionPeticionServicio,
            IServicioAplicacionBitacora servicioAplicacionBitacora,
            IServicioAplicacionParametroGeneral servicioAplicacionParametro,
            IServicioAplicacionTransaccionOperacion servicioAplicacionTransaccionOperacion) : base(contextoAplicacion)
        {
            _repositorioGeneral = repositoGeneral;
            _repositorioOperacion = repositorioOperacion;
            _servicioAplicacionColas = servicioAplicacionColas;
            _servicioAplicacionCliente = servicioAplicacionCliente;
            _servicioaplicacionPeticion = aplicacionPeticionServicio;
            _servicioAplicacionBitacora = servicioAplicacionBitacora;
            _servicioAplicacionParametro = servicioAplicacionParametro;
            _servicioAplicacionTransaccionOperacion = servicioAplicacionTransaccionOperacion;
        }

        #endregion

        #region Metodos

        #region Consulta de Cuenta
        /// <summary>
        /// Procesa la operacion consulta cuenta de la Camara de Compensación Eléctronica.
        /// </summary>
        /// <param name="datosConsultaCuenta">datos recepcionados de la CCE</param>
        /// <returns>Retorna la respuesta maquetada (AV3)</returns>
        public async Task<EstructuraContenidoAV3> ConsultaCuentaDeCCE(
            ConsultaCuentaRecepcionEntradaDTO datosConsultaCuenta,
            string codigoValidacionFirma)
        {
            var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

            var bitacoraOperacion = _repositorioGeneral
                .ObtenerPorExpresionConLimite<BitacoraTransferenciaInmediata>(x =>
                    x.IdentificadorInstruccion == datosConsultaCuenta.instructionId &&
                    x.IndicadorBitacora == General.Receptor)
                .FirstOrDefault();

            var entidadesFinancieras = _repositorioGeneral
                .ObtenerPorExpresionConLimite<EntidadFinancieraInmediata>(x =>
                    x.CodigoEntidad == EntidadFinancieraInmediata.CodigoCajaTacna ||
                    x.CodigoEntidad == datosConsultaCuenta.debtorParticipantCode)
                .ToList();

            var bitacora = _servicioAplicacionBitacora
                .RegistrarBitacora(datosConsultaCuenta, fechaSistema.FechaHoraSistema);

            var afiliacionInteroperabilidad = _repositorioGeneral
                .ObtenerPorExpresionConLimite<AfiliacionInteroperabilidadDetalle>(x =>
                    x.CodigoCuentaInterbancario == datosConsultaCuenta.creditorCCI &&
                    x.IndicadorEstadoAfiliado == AfiliacionInteroperabilidadDetalle.Afiliado)
                .FirstOrDefault();

            var canal = _repositorioGeneral
                .ObtenerPorExpresionConLimite<CanalCCE>(x =>
                    x.CodigoCanalCCE == datosConsultaCuenta.channel &&
                    x.IndicadorEstado == General.Activo)
                .FirstOrDefault();

            var clienteReceptor = _servicioAplicacionCliente
                .ObtenerDatosPorCodigoCuentaInterbancaria(datosConsultaCuenta.creditorCCI);

            var codigoValidacion = datosConsultaCuenta
                .ValidarReglas(clienteReceptor, bitacoraOperacion,
                    entidadesFinancieras, afiliacionInteroperabilidad,
                    canal, codigoValidacionFirma);

            var respuesta = datosConsultaCuenta.MaquetarDatos(
                codigoValidacion, clienteReceptor);

            bitacora.ActualizarBitacora(respuesta.AV3, clienteReceptor, fechaSistema, codigoValidacionFirma);

            _repositorioGeneral.GuardarCambios();

            return respuesta;
        }
        #endregion

        #region Orden de Transferencia
        /// <summary>
        /// Procesa la operacion orden de transferencia de la Camara de Compensación Eléctronica.
        /// </summary>
        /// <param name="datosOrdenTransferencia">datos recepcionados de la CCE</param>
        /// <returns>Retorna la respuesta maquetada (CT3)</returns>
        public async Task<EstructuraContenidoCT3> AutorizaOrdenTransferenciaDeCCE(
            OrdenTransferenciaRecepcionEntradaDTO datosOrdenTransferencia,
            string codigoValidacionFirma)
        {
            var fechaSistema = _repositorioOperacion.ObtenerCalendarioCuentaEfectivo();

            var tramaOperacion = _repositorioOperacion
                .ObtenerPorExpresionConLimite<TransaccionOrdenTransferenciaInmediata>(x =>
                    x.IdentificadorInstruccion == datosOrdenTransferencia.instructionId &&
                    x.IndicadorTransaccion == General.Receptor)
                .FirstOrDefault();

            var entidadesFinancieras = _repositorioOperacion
                .ObtenerPorExpresionConLimite<EntidadFinancieraInmediata>(x =>
                    x.CodigoEntidad == EntidadFinancieraInmediata.CodigoCajaTacna ||
                    x.CodigoEntidad == datosOrdenTransferencia.debtorParticipantCode)
                .ToList();

            var codigoMoneda = datosOrdenTransferencia.currency == ((int)MonedaCodigo.SolesCCE).ToString()
                ? ((int)MonedaCodigo.Soles).ToString() : ((int)MonedaCodigo.Dolares).ToString();

            var limiteTransferencia = _repositorioOperacion
                .ObtenerPorExpresionConLimite<LimiteTransferenciaInmediata>(x =>
                    x.CodigoCanal == General.CanalCCE &&
                    x.TipoTransferencia.Codigo == datosOrdenTransferencia.transactionType &&
                    x.CodigoMoneda == codigoMoneda &&
                    x.EstadoLimite == General.Activo)
                .FirstOrDefault();

            var bitacora = _servicioAplicacionBitacora
                .RegistrarBitacora(datosOrdenTransferencia, fechaSistema.FechaHoraSistema);

            var canal = _repositorioOperacion
                .ObtenerPorExpresionConLimite<CanalCCE>(x =>
                    x.CodigoCanalCCE == datosOrdenTransferencia.channel &&
                    x.IndicadorEstado == General.Activo)
                .FirstOrDefault();

            var clienteReceptor = _servicioAplicacionCliente
                .ObtenerDatosPorCodigoCuentaInterbancaria(datosOrdenTransferencia.creditorCCI);

            var codigoValidacion = datosOrdenTransferencia
                .ValidarReglas(clienteReceptor, tramaOperacion,
                    entidadesFinancieras, limiteTransferencia,
                    canal, codigoValidacionFirma);

            codigoValidacion = ValidarLimiteTransferenciaCanalInteroperabilidad(
                codigoValidacion, fechaSistema.FechaHoraSistema, datosOrdenTransferencia);

            var respuesta = datosOrdenTransferencia.MaquetarDatos(
                codigoValidacion, clienteReceptor, fechaSistema);

            datosOrdenTransferencia.debtorIdCode = _servicioAplicacionParametro
                .ObtenertipoDocumentoTakana(datosOrdenTransferencia.debtorIdCode!);

            bitacora.ActualizarBitacora(respuesta.CT3, fechaSistema.FechaHoraSistema, codigoValidacionFirma);

            var transaccion = _servicioAplicacionBitacora
                .RegistrarTransaccion(datosOrdenTransferencia, respuesta.CT3, clienteReceptor, fechaSistema.FechaHoraSistema);

            _ = Task.Run(async () => await _servicioaplicacionPeticion
                .EnviarEstadoSolicitudPagoApiTransferencia(respuesta.CT3.instructionId));

            if (respuesta.CT3.responseCode == CodigoRespuesta.Rechazada)
            {
                transaccion.ActualizarFechaRespuesta(fechaSistema.FechaHoraSistema);
                _ = Task.Run(async () => await _servicioaplicacionPeticion
                    .EnviarParaProcesarRechazoTransferenciaEntrante(datosOrdenTransferencia.instructionId));
            }

            _repositorioOperacion.GuardarCambios();

            return respuesta;
        }

        /// <summary>
        /// Valida los limites de transferencia para el canal de interoperabilidad
        /// </summary>
        /// <param name="codigoValidacion"></param>
        /// <param name="datosOrdenTransferencia"></param>
        /// <returns></returns>
        private string ValidarLimiteTransferenciaCanalInteroperabilidad(
            string codigoValidacion,
            DateTime fechaSistema,
            OrdenTransferenciaRecepcionEntradaDTO datosOrdenTransferencia)
        {
            if(datosOrdenTransferencia.channel != DatosGeneralesInteroperabilidad.CanalInteroperabilidad)
                return codigoValidacion;

            if (codigoValidacion != RazonRespuesta.codigo0000)
                return codigoValidacion;

            var parametros = _repositorioOperacion
                .ObtenerPorExpresionConLimite<ParametroGeneralTransferencia>(x =>
                    x.CodigoParametro == ParametroGeneralTransferencia.MaximoPorTransferenciaEntranteSoles ||
                    x.CodigoParametro == ParametroGeneralTransferencia.MaximoPorDiaTransferenciaEntranteSoles)
                .ToList();

            var montoTransferenciaCCE = datosOrdenTransferencia.amount.ObtenerImporteOperacion();

            var limiteMontoPorTransferencias = parametros.First(x =>
                x.CodigoParametro == ParametroGeneralTransferencia.MaximoPorTransferenciaEntranteSoles).ValorParametro;

            if (montoTransferenciaCCE > decimal.Parse(limiteMontoPorTransferencias))
                return RazonRespuesta.codigoFF07;

            var montoTotalTransferenciaPorDia = _repositorioOperacion
                .ObtenerPorExpresionConLimite<TransaccionOrdenTransferenciaInmediata>(x =>
                    x.CodigoCanal == DatosGeneralesInteroperabilidad.CanalInteroperabilidad &&
                    x.CodigoCuentaInterbancariaReceptor == datosOrdenTransferencia.creditorCCI &&
                    x.FechaRegistro.Date == fechaSistema.Date &&
                    x.IndicadorEstadoOperacion == General.Finalizado)
                .Sum(x => x.MontoTransferencia);

            var limiteMontoPorDia = parametros.First(x =>
                x.CodigoParametro == ParametroGeneralTransferencia.MaximoPorDiaTransferenciaEntranteSoles).ValorParametro;

            var montoTotal = montoTransferenciaCCE + montoTotalTransferenciaPorDia;

            if (montoTotal > decimal.Parse(limiteMontoPorDia))
                return RazonRespuesta.codigoFF07;

            return codigoValidacion;
        }

        /// <summary>
        /// Procesa confirmacion de abono de la orden de transferencia de la Camara de Compensación Eléctronica.
        /// </summary>
        /// <param name="datosConfirmacion">datos recepcionados de la CCE</param>
        /// <returns>Retorna mensaje de confirmacion boleano true</returns>
        public async Task ConfirmaOrdenTransferenciaDeCCE(
            OrdenTransferenciaConfirmacionEntradaDTO datosConfirmacion,
            string codigoValidacionFirma)
        {
            var fechaSistema = _repositorioOperacion.ObtenerCalendarioCuentaEfectivo();

            var bitacora = _servicioAplicacionBitacora.RegistrarBitacora(datosConfirmacion, fechaSistema.FechaHoraSistema);

            var transaccion = _repositorioOperacion
                .ObtenerPorExpresionConLimite<TransaccionOrdenTransferenciaInmediata>(x =>
                    x.IdentificadorInstruccion == datosConfirmacion.instructionId &&
                    x.IndicadorTransaccion == General.Receptor)
                .FirstOrDefault();

            var codigoValidacion = datosConfirmacion.ValidarReglas(transaccion, codigoValidacionFirma);

            if (codigoValidacion == RazonRespuesta.codigo0000)
            {
                bitacora.ActualizarBitacora(transaccion, fechaSistema.FechaHoraSistema, codigoValidacionFirma);

                transaccion.ActualizarEstadoTransaccion(General.Confirmado, fechaSistema.FechaHoraSistema);

                _ = Task.Run(async () => await _servicioaplicacionPeticion
                    .EnviarParaProcesarTransferenciaEntrante(transaccion?.IdentificadorInstruccion));
            }

            _repositorioOperacion.GuardarCambios();
        }

        /// <summary>
        /// Método que procesa la transferencia Entrante de la CCE
        /// </summary>
        /// <param name="identificadorInstruccion"></param>
        /// <returns>Retorna true si se procesa correctamente la transferencia</returns>
        public async Task<bool> ProcesarTransferenciaEntranteCCE(string identificadorInstruccion)
        {
            try
            {
                var (numeroMovimiento, enviarNotificacion) = await _servicioAplicacionTransaccionOperacion
                    .RealizarTransferenciaEntranteCCE(identificadorInstruccion);
                
                await _servicioAplicacionCliente.EnviarNotificacionAntifraudeUNIBANCA(numeroMovimiento, NotificacionUnibancaDTO.NotificacionTransferenciaCanalElectronico);

                if(enviarNotificacion)
                    await _servicioAplicacionCliente.EnviarCorreoClienteTransferenciaInmediata(numeroMovimiento);
                
                return true;
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message, excepcion.InnerException);
            }
        }

        /// <summary>
        /// Método que procesa el rechazo de la transferencia Entrante de la CCE
        /// </summary>
        /// <param name="identificadorInstruccion"></param>
        /// <returns>Retorna true si se procesa correctamente el rechazo</returns>
        public async Task<bool> ProcesarRechazoTransferenciaEntranteCCE(string identificadorInstruccion)
        {
            try
            {
                await _servicioAplicacionTransaccionOperacion.RealizarRechazoTransferenciaEntranteCCE(identificadorInstruccion);
                return true;
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message, excepcion.InnerException);
            }
        }

        /// <summary>
        /// Procesa cancelacion de la orden de transferencia de la Camara de Compensación Eléctronica.
        /// </summary>
        /// <param name="datosCancelacion">datos recepcionados de la CCE</param>
        /// <returns>Retorna la respuesta maquetada (CTC2)</returns>
        public async Task<EstructuraContenidoCTC2> CancelacionOrdenTransferenciaDeCCE(
            CancelacionRecepcionDTO datosCancelacion,
            string codigoValidacionFirma)
        {
            var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

            var bitacora = _servicioAplicacionBitacora.RegistrarBitacora(datosCancelacion, fechaSistema.FechaHoraSistema);

            var codigoValidacion = datosCancelacion.ValidarReglas(codigoValidacionFirma);

            var respuesta = datosCancelacion.MaquetarDatos(codigoValidacion, fechaSistema);

            bitacora.ActualizarBitacora(respuesta.CTC2, fechaSistema.FechaHoraSistema, codigoValidacionFirma);

            if (codigoValidacion == RazonRespuesta.codigo0000)
            {
                var transaccion = _repositorioGeneral
                .ObtenerPorExpresionConLimite<TransaccionOrdenTransferenciaInmediata>(x =>
                    x.IdentificadorInstruccion == datosCancelacion.referenceInstructionId &&
                    x.IndicadorTransaccion == General.Receptor)
                .First();

                transaccion.RestablecerComision();

                transaccion.ActualizarEstadoTransaccion(General.Rechazo, fechaSistema.FechaHoraSistema);
            }

            _repositorioGeneral.GuardarCambios();

            return respuesta;
        }
        #endregion

        #region Rechazo de CCE
        /// <summary>
        /// Registra en la Bitacora el Rechazo(Reject) de la Camara de Compensación Eléctronica.
        /// </summary>
        /// <param name="datosRechazo">datos recepcionados de la CCE</param>
        /// <returns>Retorna la respuesta boleano true</returns>
        public async Task RechazoDeCCE(RechazoRecepcionDTO datosRechazo)
        {
            var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

            _servicioAplicacionBitacora.RegistrarBitacora(datosRechazo, fechaSistema.FechaHoraSistema);
        }
        #endregion

        #region Echo Test
        /// <summary>
        /// Procesa la operacion de Echo Test de la Camara de Compensación Eléctronica.
        /// </summary>
        /// <param name="datosEchoTest">datos recepcionados de la CCE</param>
        /// <returns>Retorna la respuesta maquetada (ET2)</returns>
        public async Task<EstructuraContenidoET2> EchoTestDeCCE(
            EchoTestDTO datosEchoTest,
            string codigoValidacionFirma)
        {
            var codigoValidacion = datosEchoTest.ValidarReglas(codigoValidacionFirma);

            var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

            var respuesta = datosEchoTest.MaquetarDatos(codigoValidacion, fechaSistema);

            return respuesta;
        }
        #endregion

        #region Mensajes de Notificación
        /// <summary>
        /// Procesa la operacion de mensaje de notificacion 971 de la Camara de Compensación Eléctronica.
        /// Notificación de Cambio de Estado del Sistema
        /// </summary>
        /// <param name="datosNotificacion">datos recepcionados de la CCE</param>
        /// <returns>Retorna la respuesta boleano true</returns>
        public async Task Notificacion971DeCCE(Notificacion971DTO datosNotificacion)
        {
            var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

            var parametroGeneral = _repositorioGeneral
                .ObtenerPorExpresionConLimite<ParametroGeneralTransferencia>(
                    x => x.CodigoParametro == ParametroGeneralTransferencia.CodigoSistemaTINCCE)
                .FirstOrDefault()
                .ValidarEntidad();

            var valor = datosNotificacion.newServiceStatus == General.DescripcionNormal
                ? General.Disponible : General.Suspendido;

            parametroGeneral.ActualizarValorParametro(valor);

            var mensajeNotificacion = MensajeNotificacionTransferenciaInmediata
                .RegistrarMensajeNotificacion(datosNotificacion, fechaSistema.FechaHoraSistema);
            _repositorioGeneral.Adicionar(mensajeNotificacion);

            await EnviarCorreoPorRabitAsync(fechaSistema.FechaHoraSistema, (int)TipoTramaEnum.SNM971, mensajeNotificacion);

            _repositorioGeneral.GuardarCambios();
        }

        /// <summary>
        /// Procesa la operacion de mensaje de notificacion 972 de la Camara de Compensación Eléctronica.
        /// Notificación de Cambio de estado de Bloqueo de la Entidad
        /// </summary>
        /// <param name="datosNotificacion">datos recepcionados de la CCE</param>
        /// <returns>Retorna la respuesta boleano true</returns>
        public async Task Notificacion972DeCCE(Notificacion972DTO datosNotificacion)
        {
            var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

            var entidadFinanciera = _repositorioGeneral
                .ObtenerPorExpresionConLimite<EntidadFinancieraInmediata>(x =>
                    x.CodigoEntidad == datosNotificacion.memberIdentification)
                .FirstOrDefault()
                .ValidarEntidad();

            var valorEstado = datosNotificacion.status == General.DescripcionNormal
                ? General.Normalizado : General.Bloqueado;

            entidadFinanciera.ActualizarEstadoCCE(valorEstado);

            var mensajeNotificacion = MensajeNotificacionTransferenciaInmediata
                .RegistrarMensajeNotificacion(datosNotificacion, fechaSistema.FechaHoraSistema);
            _repositorioGeneral.Adicionar(mensajeNotificacion);

            await EnviarCorreoPorRabitAsync(fechaSistema.FechaHoraSistema, (int)TipoTramaEnum.SNM972, mensajeNotificacion);

            _repositorioGeneral.GuardarCambios();
        }

        /// <summary>
        /// Procesa la operacion de mensaje de notificacion 981 de la Camara de Compensación Eléctronica.
        /// Notificación de Libre Formato
        /// </summary>
        /// <param name="datosNotificacion">datos recepcionados de la CCE</param>
        /// <returns>Retorna la respuesta boleano true</returns>
        public async Task Notificacion981DeCCE(Notificacion981DTO datosNotificacion)
        {
            var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

            var mensajeNotificacion = MensajeNotificacionTransferenciaInmediata
                .RegistrarMensajeNotificacion(datosNotificacion, fechaSistema.FechaHoraSistema);
            _repositorioGeneral.Adicionar(mensajeNotificacion);

            await EnviarCorreoPorRabitAsync(fechaSistema.FechaHoraSistema, (int)TipoTramaEnum.SNM981, mensajeNotificacion);

            _repositorioGeneral.GuardarCambios();
        }

        /// <summary>
        /// Procesa la operacion de mensaje de notificacion 982 de la Camara de Compensación Eléctronica.
        /// Notificación de Cambio de Estado de Logueo de la Entidad
        /// </summary>
        /// <param name="datosNotificacion">datos recepcionados de la CCE</param>
        /// <returns>Retorna la respuesta boleano true</returns>
        public async Task Notificacion982DeCCE(Notificacion982DTO datosNotificacion)
        {
            var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

            var entidadFinanciera = _repositorioGeneral
                .ObtenerPorExpresionConLimite<EntidadFinancieraInmediata>(x =>
                    x.CodigoEntidad == datosNotificacion.participantIdentification)
                .FirstOrDefault()
                .ValidarEntidad();

            var valorEstado = datosNotificacion.participantStatus == General.DescripcionSignOn
                ? General.SignOn : General.SignOff;

            entidadFinanciera.ActualizarEstadoSign(valorEstado);

            var mensajeNotificacion = MensajeNotificacionTransferenciaInmediata
                .RegistrarMensajeNotificacion(datosNotificacion, fechaSistema.FechaHoraSistema);
            _repositorioGeneral.Adicionar(mensajeNotificacion);

            await EnviarCorreoPorRabitAsync(fechaSistema.FechaHoraSistema, (int)TipoTramaEnum.SNM982, mensajeNotificacion);

            _repositorioGeneral.GuardarCambios();
        }

        /// <summary>
        /// Procesa la operacion de mensaje de notificacion 993 de la Camara de Compensación Eléctronica.
        /// Notificación de Ampliación de Garantías
        /// </summary>
        /// <param name="datosNotificacion">datos recepcionados de la CCE</param>
        /// <returns>Retorna la respuesta boleano true</returns>
        public async Task Notificacion993DeCCE(Notificacion993DTO datosNotificacion)
        {
            var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

            var mensajeNotificacion = MensajeNotificacionTransferenciaInmediata
                .RegistrarMensajeNotificacion(datosNotificacion, fechaSistema.FechaHoraSistema);
            _repositorioGeneral.Adicionar(mensajeNotificacion);

            await EnviarCorreoPorRabitAsync(fechaSistema.FechaHoraSistema, (int)TipoTramaEnum.SNM993, mensajeNotificacion);

            _repositorioGeneral.GuardarCambios();
        }

        /// <summary>
        /// Procesa la operacion de mensaje de notificacion 998 de la Camara de Compensación Eléctronica.
        /// Notificación de Cambio en el Saldo Operativo
        /// </summary>
        /// <param name="datosNotificacion">datos recepcionados de la CCE</param>
        /// <returns>Retorna la respuesta boleano true</returns>
        public async Task Notificacion998DeCCE(Notificacion998DTO datosNotificacion)
        {
            var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

            var mensajeNotificacion = MensajeNotificacionTransferenciaInmediata
                .RegistrarMensajeNotificacion(datosNotificacion, fechaSistema.FechaHoraSistema);
            _repositorioGeneral.Adicionar(mensajeNotificacion);

            await EnviarCorreoPorRabitAsync(fechaSistema.FechaHoraSistema, (int)TipoTramaEnum.SNM998, mensajeNotificacion);

            _repositorioGeneral.GuardarCambios();
        }

        /// <summary>
        /// Procesa la operacion de mensaje de notificacion 999 de la Camara de Compensación Eléctronica.
        /// Notificación de Resumen de Ciclo Operativo
        /// </summary>
        /// <param name="datosNotificacion">datos recepcionados de la CCE</param>
        /// <returns>Retorna la respuesta boleano true</returns>
        public async Task Notificacion999DeCCE(Notificacion999DTO datosNotificacion)
        {

            var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

            var mensajeNotificacion = MensajeNotificacionTransferenciaInmediata
                .RegistrarMensajeNotificacion(datosNotificacion, fechaSistema.FechaHoraSistema);
            _repositorioGeneral.Adicionar(mensajeNotificacion);

            await EnviarCorreoPorRabitAsync(fechaSistema.FechaHoraSistema, (int)TipoTramaEnum.SNM999, mensajeNotificacion);

            _repositorioGeneral.GuardarCambios();
        }

        /// <summary>
        /// Método que envia el correo electronico y el archivo
        /// </summary>
        /// <param name="fechaSistema"></param>
        /// <param name="idMensajeNotificacion"></param>
        /// <param name="mensajeNotificacion"></param>
        /// <returns></returns>
        private async Task EnviarCorreoPorRabitAsync(
            DateTime fechaSistema,
            int idMensajeNotificacion,
            MensajeNotificacionTransferenciaInmediata mensajeNotificacion)
        {
            var tipoMensaje = _repositorioGeneral.ObtenerPorCodigo<TipoTrama>(idMensajeNotificacion);

            var correoRemitente = _repositorioGeneral
                .ObtenerPorExpresionConLimite<ParametroGeneralTransferencia>(x =>
                    x.CodigoParametro == ParametroGeneralTransferencia.CorreoElectronicoAdministrador)
                .First().ValorParametro;

            var correosDestinatarios = _repositorioGeneral
                .ObtenerPorExpresionConLimite<ParametroGeneralTransferencia>(x =>
                    x.CodigoParametro == ParametroGeneralTransferencia.CorreoElectronicoDestinatariosNotificacion)
                .First().ValorParametro;

            var correosElectronicosDestinatarios = correosDestinatarios.Split(";");

            foreach (var correoDestino in correosElectronicosDestinatarios)
            {
                var formatoCorreo = _contextoAplicacion.AdtoFormatoNotificacionCorreo(fechaSistema, correoRemitente,
                    correoDestino, tipoMensaje.Descripcion, mensajeNotificacion);

                await _servicioAplicacionColas.EnviarCorreoAsync(CorreoGeneralDTO.CodigoNotificacionCCE, formatoCorreo);
            }
        }
        #endregion

        #region Método de Solicitud Estado de Pago
        /// <summary>
        /// Operación que maqueta y envia para procesar el requerimiento de Solicitud de Estado de Pago
        /// </summary>
        /// <param name="identificadorInstruccion">Identificador de Instruccion</param>
        /// <param name="modalidad">Modalidad Manual o Automatica</param>
        /// <returns>Retorna la respuesta boleano true</returns>
        public async Task<bool> SolicitudEstadoPagoParaCCE(
            string identificadorInstruccion,
            bool modalidad)
        {
            bool codigoRepuesta = true;
            try
            {
                var fechaSistema = _repositorioOperacion.ObtenerCalendarioCuentaEfectivo();

                var parametro = _repositorioOperacion
                    .ObtenerPorExpresionConLimite<ParametroGeneralTransferencia>(x =>
                        x.CodigoParametro == ParametroGeneralTransferencia.TiempoReintento)
                    .FirstOrDefault();

                if (modalidad)
                    await Task.Delay(Convert.ToInt32(parametro?.ValorParametro) * General.ValorConvetirEnSegundos);

                var transaccion = _repositorioOperacion.
                    ObtenerPorExpresionConLimite<TransaccionOrdenTransferenciaInmediata>(x =>
                        x.IdentificadorInstruccion == identificadorInstruccion)
                    .FirstOrDefault()
                    .ValidarOrdenTransferencia();

                var respuesta = transaccion.MaquetarDatos(fechaSistema.FechaHoraSistema);

                codigoRepuesta = await ProcesarSolicitudEstadoPagoParaCCE(
                    respuesta, transaccion, fechaSistema, modalidad);

                return codigoRepuesta;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Operación que procesa un requerimiento automático para una Solicitud de Estado de Pago.
        /// </summary>
        /// <param name="datosSolicitudEstadoPago"></param>
        /// <param name="modalidad">Modalidad Manual o Automatica</param>
        /// <returns>Retorna la respuesta booleana true</returns>
        private async Task<bool> ProcesarSolicitudEstadoPagoParaCCE(
            EstructuraContenidoPSR1 datosSolicitudEstadoPago,
            TransaccionOrdenTransferenciaInmediata transaccion,
            Calendario fechaSistema,
            bool modalidad)
        {
            try
            {
                var parametro = _repositorioOperacion
                    .ObtenerPorExpresionConLimite<ParametroGeneralTransferencia>(x =>
                        x.CodigoParametro == ParametroGeneralTransferencia.MaximoReintento)
                    .FirstOrDefault()!;

                if (transaccion.IndicadorTransaccion == General.Receptor && transaccion.IndicadorEstadoOperacion == General.Confirmado)
                    await ProcesarTransferenciaEntranteCCE(transaccion.IdentificadorInstruccion);
                else
                    await ProcesarSolicitudEstadoPago(datosSolicitudEstadoPago, transaccion, fechaSistema.FechaHoraSistema);

                if (modalidad) await ManejarReintentoSolicitud(transaccion, parametro);

                _repositorioOperacion.GuardarCambios();

                return true;
            }
            catch (Exception excepcion)
            {
                await EnviarEstadoSolicitudPagoParaReintento(transaccion);
                throw new Exception(excepcion.Message);
            }
        }

        /// <summary>
        /// Procesa la solicitud de estado de pago para una transacción no confirmada.
        /// </summary>
        /// <param name="datosSolicitudEstadoPago"></param>
        /// <param name="transaccion"></param>
        /// <param name="fechaSistema"></param>
        /// <returns></returns>
        private async Task ProcesarSolicitudEstadoPago(
            EstructuraContenidoPSR1 datosSolicitudEstadoPago,
            TransaccionOrdenTransferenciaInmediata transaccion,
            DateTime fechaSistema)
        {
            var bitacora = _servicioAplicacionBitacora.RegistrarBitacora(datosSolicitudEstadoPago.PSR1, fechaSistema);
            var numeroSeguimiento = _servicioAplicacionParametro.ObtenerNumeroSeguimiento(DatosGenerales.CodigoSeguimiento);

            var respuesta = await _servicioaplicacionPeticion
                .EnviarSolicitudEstadoPagoCCE(datosSolicitudEstadoPago, numeroSeguimiento);

            if (respuesta.PSR2 == null || respuesta.PSR2.responseCode == CodigoRespuesta.NoEncontrada)
                return;

            if (respuesta.PSR2.responseCode == CodigoRespuesta.Aceptada)
            {
                await ConfirmarTransaccion(transaccion, fechaSistema);

                await ProcesarTransferenciaEntranteCCE(transaccion.IdentificadorInstruccion);
            }
            else
                await RechazarTransaccion(transaccion, respuesta, fechaSistema);

            bitacora.ActualizarBitacora(transaccion, respuesta.PSR2, fechaSistema);
        }

        /// <summary>
        /// Confirma la transacción y procesa la transferencia entrante.
        /// </summary>
        /// <param name="transaccion"></param>
        /// <param name="fechaSistema"></param>
        /// <returns></returns>
        private async Task ConfirmarTransaccion(TransaccionOrdenTransferenciaInmediata transaccion, DateTime fechaSistema)
        {
            transaccion.ActualizarEstadoTransaccion(General.Confirmado, fechaSistema);
            _repositorioOperacion.GuardarCambios();;
        }

        /// <summary>
        /// Rechaza la transacción y maneja el rechazo según el respuesta.
        /// </summary>
        /// <param name="transaccion"></param>
        /// <param name="respuesta"></param>
        /// <param name="fechaSistema"></param>
        /// <returns></returns>
        private async Task RechazarTransaccion(TransaccionOrdenTransferenciaInmediata transaccion, EstructuraContenidoPSR2 respuesta, DateTime fechaSistema)
        {
            transaccion.ActualizarEstadoTransaccion(General.Rechazo, fechaSistema);

            if (respuesta.PSR2.reasonCode == CodigoRespuesta.TiempoEspera)
                transaccion.RestablecerComision();
            else
            {
                _= Task.Run(async() => await _servicioaplicacionPeticion
                    .EnviarParaProcesarRechazoTransferenciaEntrante(transaccion.IdentificadorInstruccion));
            }
        }

        /// <summary>
        /// Maneja el reintento de solicitud según el parámetro de reintentos.
        /// </summary>
        /// <param name="transaccion"></param>
        /// <param name="parametro"></param>
        /// <returns></returns>
        private async Task ManejarReintentoSolicitud(TransaccionOrdenTransferenciaInmediata transaccion, ParametroGeneralTransferencia parametro)
        {
            transaccion.IncrementarNumeroReintentoSolicitud();

            if (transaccion.NumeroReintentoSolicitud < Convert.ToInt32(parametro?.ValorParametro))
                await EnviarEstadoSolicitudPagoParaReintento(transaccion);
        }

        /// <summary>
        /// Envía el estado de solicitud de pago para reintento en caso de error.
        /// </summary>
        /// <param name="transaccion"></param>
        /// <returns></returns>
        private async Task EnviarEstadoSolicitudPagoParaReintento(TransaccionOrdenTransferenciaInmediata transaccion)
        {
            _= Task.Run(async() => await _servicioaplicacionPeticion
                .EnviarEstadoSolicitudPagoApiTransferencia(transaccion.IdentificadorInstruccion));
        }

        #endregion

        #region Método de reprocesar
        /// <summary>
        /// Metodo que reprocesar las transacciones en proceso de transferencias inmediatas.
        /// </summary>
        /// <param name="transacciones"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task ReprocesarTransaccion(List<ReprocesarTransaccionDTO> transacciones)
        {
            var fechaSistema = _repositorioOperacion.ObtenerCalendarioCuentaEfectivo();

            using (var transaction = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    foreach (var item in transacciones)
                    {
                        var transaccion = _repositorioOperacion
                            .ObtenerPorCodigo<TransaccionOrdenTransferenciaInmediata>(item.IdTransaccion)
                            .ValidarOrdenTransferencia();

                        if (item.IdArchivoMovimiento != 0)                        
                            await ProcesarArchivoMovimiento(item, transaccion, fechaSistema);
                        else
                            await ProcesarTransaccionSinMovimiento(transaccion, fechaSistema, item);
                    }
                    transaction.Complete();
                }
                catch (Exception excepcion)
                {
                    throw new Exception(excepcion.Message, excepcion.InnerException);
                }
            }
        }

        /// <summary>
        /// Procesa el archivo de movimiento asociado a la transacción.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="transaccion"></param>
        /// <param name="fechaSistema"></param>
        /// <returns></returns>
        private async Task ProcesarArchivoMovimiento(ReprocesarTransaccionDTO item, TransaccionOrdenTransferenciaInmediata transaccion, Calendario fechaSistema)
        {
            if (transaccion.IndicadorEstadoOperacion == General.Receptor)
                await SolicitudEstadoPagoParaCCE(transaccion.IdentificadorInstruccion, false);
            else
            {
                var movimientoConciliacion = _repositorioOperacion
                    .ObtenerPorCodigo<ArchivoMovimientoConciliacion>(item.IdArchivoMovimiento);

                var bitacora = ObtenerBitacoraTransaccion(transaccion);
                ActualizarBitacora(movimientoConciliacion, bitacora, fechaSistema, item.Usuario);

                if (movimientoConciliacion.Estado == General.Aceptado)
                    FinalizarTransaccionSaliente(movimientoConciliacion, transaccion, fechaSistema, item.Usuario);
                else
                {
                    await _servicioAplicacionTransaccionOperacion.RealizarTransferenciaDevolucionCCE(
                            transaccion, movimientoConciliacion.IdentificadorTransaccion,
                            fechaSistema, item.Usuario);
                }
            }
        }

        /// <summary>
        /// Procesa la transacción sin movimiento asociado.
        /// </summary>
        /// <param name="transaccion"></param>
        /// <param name="fechaSistema"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private async Task ProcesarTransaccionSinMovimiento(TransaccionOrdenTransferenciaInmediata transaccion, Calendario fechaSistema, ReprocesarTransaccionDTO item)
        {
            var bitacora = ObtenerBitacoraTransaccion(transaccion);
            ActualizarBitacoraSinMovimiento(bitacora, transaccion, fechaSistema, item.Usuario);

            await _servicioAplicacionTransaccionOperacion
                .RealizarTransferenciaDevolucionCCE(
                    transaccion, transaccion.IdentificadorInstruccion,
                    fechaSistema, item.Usuario);
        }

        /// <summary>
        /// Obtiene la bitácora de una transacción.
        /// </summary>
        /// <param name="transaccion"></param>
        /// <returns>BitacoraTransferenciaInmediata</returns>
        private BitacoraTransferenciaInmediata ObtenerBitacoraTransaccion(TransaccionOrdenTransferenciaInmediata transaccion)
        {
            return _repositorioOperacion
                .ObtenerPorExpresionConLimite<BitacoraTransferenciaInmediata>(x =>
                    x.NumeroTrace == transaccion.CodigoTrace &&
                    x.MontoImporte == transaccion.MontoTransferencia &&
                    x.IndicadorBitacora == General.Originante)
                .First();
        }

        /// <summary>
        /// Actualiza la bitácora con la información del movimiento de conciliación.
        /// </summary>
        /// <param name="movimientoConciliacion"></param>
        /// <param name="bitacora"></param>
        /// <param name="fechaSistema"></param>
        /// <param name="usuario"></param>
        private void ActualizarBitacora(ArchivoMovimientoConciliacion movimientoConciliacion, BitacoraTransferenciaInmediata bitacora, Calendario fechaSistema, string usuario)
        {
            bitacora.ActualizarBitacora(movimientoConciliacion.IdentificadorTransaccion,
                movimientoConciliacion.Estado, fechaSistema, usuario);
        }

        /// <summary>
        /// Actualiza la bitácora cuando no hay movimiento asociado.
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="transaccion"></param>
        /// <param name="fechaSistema"></param>
        /// <param name="usuario"></param>
        private void ActualizarBitacoraSinMovimiento(BitacoraTransferenciaInmediata bitacora, TransaccionOrdenTransferenciaInmediata transaccion, Calendario fechaSistema, string usuario)
        {
            bitacora.ActualizarBitacora(transaccion.IdentificadorInstruccion,
                General.Rechazo, fechaSistema, usuario);
        }

        /// <summary>
        /// Finaliza la transacción saliente.
        /// </summary>
        /// <param name="movimientoConciliacion"></param>
        /// <param name="transaccion"></param>
        /// <param name="fechaSistema"></param>
        /// <param name="usuario"></param>
        private void FinalizarTransaccionSaliente(ArchivoMovimientoConciliacion movimientoConciliacion, TransaccionOrdenTransferenciaInmediata transaccion, Calendario fechaSistema, string usuario)
        {
            transaccion.ActualizarTransaccionSaliente(
                General.Finalizado, usuario,
                movimientoConciliacion.IdentificadorTransaccion,
                fechaSistema.FechaHoraSistema);
            _repositorioOperacion.GuardarCambios();
        }

        #endregion

        #endregion
    }
}