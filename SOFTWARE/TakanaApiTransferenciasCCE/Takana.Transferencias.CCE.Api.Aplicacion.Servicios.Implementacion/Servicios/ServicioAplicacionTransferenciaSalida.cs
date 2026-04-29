using System.Text.Json;
using System.Text.Json.Nodes;
using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.DTOs.SignOnOff;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;
using Takana.Transferencias.CCE.Api.Common.SignOnOff;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
using Takana.Transferencias.CCE.Api.Dominio.Servicios;
using Takana.Transferencias.CCE.Api.Dominio.Servicios.Extensiones;
using Takana.Transferencias.CCE.Api.Dominio.Servicios.Logica.Programacion_Sing_On_Off;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion
{
    /// <summary>
    /// Servicio encargado de recibir datos de los canales y armar el esquema para enviarselo a la Operadora
    /// </summary>
    public class ServicioAplicacionTransferenciaSalida : ServicioBase, IServicioAplicacionTransferenciaSalida
    {
        private readonly IRepositorioOperacion _repositorioOperacion;
        private readonly IServicioAplicacionPeticion _servicioAplicacionPeticion;
        private readonly IServicioAplicacionBitacora _servicioAplicacionBitacora;
        private readonly IServicioDominioTransaccionOperacion _dominioTransaccion;
        private readonly IServicioAplicacionValidacion _servicioAplicacionValidacion;
        private readonly IServicioAplicacionParametroGeneral _servicioAplicacionParametro;
        /// <summary>
        /// M�todo constructor
        /// </summary>
        /// <param name="contexto"></param>
        /// <param name="repositorioOperacion"></param>
        /// <param name="servicioAplicacionPeticion"></param>
        /// <param name="servicioAplicacionBitacora"></param>
        /// <param name="dominioTransaccion"></param>
        /// <param name="parametroGeneralServicio"></param>
        /// <param name="servicioAplicacionValidacion"></param>
        public ServicioAplicacionTransferenciaSalida(
            IContextoAplicacion contexto,
            IRepositorioOperacion repositorioOperacion,
            IServicioAplicacionPeticion servicioAplicacionPeticion,
            IServicioAplicacionBitacora servicioAplicacionBitacora,
            IServicioDominioTransaccionOperacion dominioTransaccion,
            IServicioAplicacionValidacion servicioAplicacionValidacion,
            IServicioAplicacionParametroGeneral parametroGeneralServicio
            ) : base(contexto)
        {
            _dominioTransaccion = dominioTransaccion;
            _repositorioOperacion = repositorioOperacion;
            _servicioAplicacionParametro = parametroGeneralServicio;
            _servicioAplicacionBitacora = servicioAplicacionBitacora;
            _servicioAplicacionPeticion = servicioAplicacionPeticion;
            _servicioAplicacionValidacion = servicioAplicacionValidacion;
        }

        #region Consulta de Cuenta CCE
        /// <summary>
        /// Metodo encargado de comunicarse con la Operadora para obtener datos de la cuenta receptora
        /// </summary>
        /// <param name="datosConsulta">Los datos enviados por el canal</param>
        /// <param name="consultaPorQr">consultaPorQr</param>
        /// <returns>Retorna la cuenta del cliente Receptor</returns>
        public async Task<RespuestaSalidaDTO<ConsultaCuentaRespuestaTraducidoDTO>> ObtenerDatosCuentaCCE(
            ConsultaCanalDTO datosConsulta, Calendario fechaSistema, bool consultaPorQr)
        {
            var esquema = new ConsultaCuentaSalidaDTO();
            var bitacora = new BitacoraTransferenciaInmediata();
            var respuesta = new RespuestasExtensiones<ConsultaCuentaRespuestaTraducidoDTO>();

            try
            {
                var conexion = await EchoTest();
                if (!conexion.Datos) throw new Exception("Lo sentimos, ocurrio un error en las comunicaciones con la CCE");

                _servicioAplicacionValidacion.ValidarReglasIPS(datosConsulta);

                esquema.trace = _servicioAplicacionParametro.ObtenerNumeroSeguimiento(DatosGenerales.CodigoSeguimiento);

                if (!datosConsulta.IndicadorCuentaMancomunada)
                    esquema.debtorIdCode = _servicioAplicacionParametro
                        .ObtenertipoDocumentoCCE(datosConsulta.TipoDocumentoDeudor.ToString());

                var cuerpoConsulta = datosConsulta.MaquetarDatos(esquema, fechaSistema);

                var cantReintentos = int.Parse(_servicioAplicacionParametro
                    .obtenerParametrosConfiguracion(ParametroGeneralTransferencia.MaximoReintento));

                bitacora = _servicioAplicacionBitacora.RegistrarBitacora(cuerpoConsulta, fechaSistema.FechaHoraSistema, consultaPorQr);

                var formatoDatos = cuerpoConsulta.DarFormatoSalida();

                string jsonLegible = JsonSerializer.Serialize(formatoDatos, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                Console.Write(jsonLegible);

                var resultado = await _servicioAplicacionPeticion.ObtenerDatosCuentaCCE(formatoDatos, cantReintentos);

                string resultadoLegible = JsonSerializer.Serialize(resultado, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                Console.Write(resultadoLegible);

                bitacora.ActualizarBitacora(resultado.AV4, fechaSistema.FechaHoraSistema);

                _repositorioOperacion.GuardarCambios();

                if (resultado.AV4.responseCode == CodigoRespuesta.Aceptada)
                    return respuesta.ConstruirRespuestaDatos(resultado.AV4.responseCode, resultado.AV4.responseCode, "Consulta Aceptada",
                        resultado.AV4.TraducirCampos());

                var error = _servicioAplicacionParametro.ObtenerErrorLocal(resultado.AV4.reasonCode);
                var descripcion = datosConsulta.EsMensajeCliente ? error.MensajeCliente : error.Nombre;
                return respuesta.ConstruirRespuestaDatos(resultado.AV4.responseCode, error.Nombre, descripcion, resultado.AV4.TraducirCampos());
            }
            catch (DomainException ex)
            {
                var mensaje = datosConsulta.EsMensajeCliente
                    ? BaseException.DescripcionExcepcionCCI
                    : ex.Message;

                return respuesta.ConstruirRespuestaGeneral(mensaje, CodigoRespuesta.Rechazada);
            }
            catch (Exception excepcion)
            {
                string mensaje;
                if (excepcion.Message.Equals(RazonRespuesta.codigoERRCONSUL))
                {
                    var error = _servicioAplicacionParametro.ObtenerErrorLocal(excepcion.Message);
                    mensaje = datosConsulta.EsMensajeCliente
                        ? error.MensajeCliente!
                        : "Ocurrio un inconveniente al enviar la consulta de cuenta a la CCE";
                    bitacora.ActualizarBitacora(RazonRespuesta.codigoERRCONSUL, fechaSistema.FechaHoraSistema);
                    _repositorioOperacion.GuardarCambios();
                }
                else mensaje = datosConsulta.EsMensajeCliente ? BaseException.DescripcionExcepcionGeneral : excepcion.Message;

                return respuesta.ConstruirRespuestaGeneral(mensaje, CodigoRespuesta.Rechazada);
            }
        }
        #endregion

        #region Orden de Transferencias CCE
        /// <summary>
        /// Metodo encargado de recibir la confirmacion del cliente originante para enviar la transferencia 
        /// Envia la orden de transferencia, espera que la entidad receptora confirme para luego realizar la operacion en esta entidad.
        /// </summary>
        /// <param name="ordenTransferencia">Los datos enviados por el canal para enviar la transferencia</param>
        /// <returns>Retorna la confirmacion de la orden de transferencia enviada</returns>
        public async Task<RespuestaSalidaDTO<OrdenTransferenciaRespuestaTraducidoDTO>> EnviarOrdenTransferencia(
            OrdenTransferenciaCanalDTO ordenTransferencia, 
            Calendario fechaSistema,
            AsientoContable asiento,
            Transferencia transferencia)
        {
            var bitacora = new BitacoraTransferenciaInmediata();
            var respuesta = new RespuestasExtensiones<OrdenTransferenciaRespuestaTraducidoDTO>();

            try
            {
                _servicioAplicacionValidacion.VerificarEstadoEntidades(ordenTransferencia.CodigoEntidadOriginante,
                    ordenTransferencia.CodigoEntidadReceptora);

                var numeroSeguimiento = _servicioAplicacionParametro.ObtenerNumeroSeguimiento(DatosGenerales.CodigoSeguimiento);

                ordenTransferencia = _servicioAplicacionParametro.ConvertirTipoDocumentos(ordenTransferencia);
                var cuerpoOrden = ordenTransferencia.MaquetarDatos(fechaSistema, numeroSeguimiento);

                var cantidadReintentos = int.Parse(_servicioAplicacionParametro.obtenerParametrosConfiguracion(
                    ParametroGeneralTransferencia.MaximoReintento));

                bitacora = _servicioAplicacionBitacora.RegistrarBitacora(cuerpoOrden, ordenTransferencia, fechaSistema.FechaHoraSistema);

                var transaccion = _servicioAplicacionBitacora.RegistrarTransaccion(
                    cuerpoOrden, ordenTransferencia, asiento, transferencia, fechaSistema.FechaHoraSistema);

                var tramaProcesada = _servicioAplicacionBitacora.RegistrarTramaProcesada(
                    cuerpoOrden, _contextoAplicacion, asiento.NumeroAsiento, transferencia.NumeroMovimiento, fechaSistema.FechaHoraSistema);

                var formato = cuerpoOrden.DarFormatoSalida();

                var resultado = await _servicioAplicacionPeticion.EnviarOrdenTransferencia(formato, cantidadReintentos);

                _servicioAplicacionBitacora.ActualizarBitacora(bitacora, resultado.CT4, fechaSistema.FechaHoraSistema);

                _dominioTransaccion.ActualizarEstadoTransaccion(transaccion, tramaProcesada, resultado.CT4, fechaSistema.FechaHoraSistema);

                _repositorioOperacion.GuardarCambios();

                if (resultado.CT4.responseCode == CodigoRespuesta.Aceptada)
                    return respuesta.ConstruirRespuestaDatos(resultado.CT4.responseCode, resultado.CT4.responseCode,
                        "Respuesta de Orden", resultado.CT4.TraducirCampos());

                var error = _servicioAplicacionParametro.ObtenerErrorLocal(resultado.CT4.reasonCode);
                return respuesta.ConstruirRespuestaDatos(resultado.CT4.responseCode, error.Nombre,
                    error.MensajeCliente, resultado.CT4.TraducirCampos(), error.TipoCodigoRespuesta);
            }
            catch (Exception excepcion)
            {
                string mensaje;
                if (excepcion.Message.Equals(DatosGenerales.IndicadorIntentosRealizados))
                {
                    var error = _servicioAplicacionParametro.ObtenerErrorLocal(RazonRespuesta.codigoERRCMACT);
                    mensaje = ordenTransferencia.EsMensajeCliente
                        ? error.MensajeCliente!
                        : "No se pudo confirmar la orden debido a un posible inconveniente de firma o comunicación. "
                          + "Para mayor información, revise la bitácora de Estados de Órdenes de Transferencia.";
                    _servicioAplicacionBitacora.ActualizarBitacora(bitacora, RazonRespuesta.codigoERRCMACT, fechaSistema.FechaHoraSistema);
                }
                else mensaje = ordenTransferencia.EsMensajeCliente ? BaseException.DescripcionExcepcionGeneral : excepcion.Message;

                throw new ValidacionException(mensaje);
            }
        }
        #endregion

        #region Operaciones de SignOn, SignOff y Echo Test
        /// <summary>
        /// Metodo que enviar el estado de ON en la operadora
        /// </summary>
        /// <returns>Retorna la confirmacion de cambio de estado ON</returns>
        public async Task<RespuestaSalidaDTO<SignOnOffDTO>> SignOn()
        {
            var respuesta = new RespuestasExtensiones<SignOnOffDTO>();
            try
            {
                var numeroSeguimiento = _servicioAplicacionParametro.ObtenerNumeroSeguimiento(DatosGenerales.CodigoSeguimiento);

                var fechaSistema = _repositorioOperacion.ObtenerCalendarioCuentaEfectivo();

                var cuerpoSign = ServicioDominioMaquetacion.MaquetarDatosSignOnOff(fechaSistema, numeroSeguimiento);

                var formato = ServicioDominioMaquetacion.DarFormatoSalidaOn(cuerpoSign);

                var resultado = await _servicioAplicacionPeticion.EnviarSignOn(formato);

                if (resultado.SignOn2.status == CodigoRespuesta.Aceptada)
                {
                    ActualizarEstadoSignCmact(General.SignOn);

                    return respuesta.ConstruirRespuestaDatos(CodigoRespuesta.Aceptada,
                        resultado.SignOn2.reasonCode, "SignOnOff", resultado.SignOn2);
                }

                var error = _servicioAplicacionParametro.ObtenerErrorLocal(resultado.SignOn2.reasonCode);
                return respuesta.ConstruirRespuestaDatos(resultado.SignOn2.status, error.Nombre,
                    error.Descripcion, resultado.SignOn2);
            }
            catch (Exception excepcion)
            {
                return respuesta.ConstruirRespuestaGeneral(excepcion.Message);
            }
        }

        /// <summary>
        /// Metodo que enviar el estado de OFF en la operadora
        /// </summary>
        /// <returns>Retorna la confirmacion de cambio de estado OFF</returns>
        public async Task<RespuestaSalidaDTO<SignOnOffDTO>> SignOff()
        {
            var respuesta = new RespuestasExtensiones<SignOnOffDTO>();

            try
            {
                var numeroSeguimiento = _servicioAplicacionParametro.ObtenerNumeroSeguimiento(DatosGenerales.CodigoSeguimiento);

                var fechaSistema = _repositorioOperacion.ObtenerCalendarioCuentaEfectivo();

                var cuerpoSign = ServicioDominioMaquetacion.MaquetarDatosSignOnOff(fechaSistema, numeroSeguimiento);

                var formato = ServicioDominioMaquetacion.DarFormatoSalidaOff(cuerpoSign);

                var resultado = await _servicioAplicacionPeticion.EnviarSignOff(formato);

                if (resultado.SignOff2.status == CodigoRespuesta.Aceptada)
                {
                    ActualizarEstadoSignCmact(General.SignOff);

                    return respuesta.ConstruirRespuestaDatos(CodigoRespuesta.Aceptada, resultado.SignOff2.reasonCode,
                        "SignOnOff", resultado.SignOff2);
                }

                var error = _servicioAplicacionParametro.ObtenerErrorLocal(resultado.SignOff2.reasonCode);
                return respuesta.ConstruirRespuestaDatos(resultado.SignOff2.status, error.Nombre,
                    error.Descripcion, resultado.SignOff2);
            }
            catch (Exception excepcion)
            {
                return respuesta.ConstruirRespuestaGeneral(excepcion.Message);
            }
        }
        /// <summary>
        /// Verifica conexion entre la CCE y el API Transferencias
        /// Tambien verifica la conexion antes de una operacion de Transferencia de takana
        /// </summary>
        /// <returns>True si hay conexion</returns>
        public async Task<RespuestaSalidaDTO<bool>> EchoTest()
        {
            var respuesta = new RespuestasExtensiones<bool>();

            try
            {
                var numeroSeguimiento = _servicioAplicacionParametro.ObtenerNumeroSeguimiento(DatosGenerales.CodigoSeguimientoEcho);

                var fechaSistema = _repositorioOperacion.ObtenerCalendarioCuentaEfectivo();

                var cuerpo = ServicioDominioMaquetacion.CreacionEchoTest(fechaSistema,
                    ParametroGeneralTransferencia.CodigoEntidadOriginante, numeroSeguimiento);

                var resultado = await _servicioAplicacionPeticion.EnviarEchoTest(cuerpo, numeroSeguimiento);

                if (resultado.ET2.status == CodigoRespuesta.Rechazada)
                {
                    var error = _servicioAplicacionParametro.ObtenerErrorLocal(resultado.ET2.reasonCode);
                    return respuesta.ConstruirRespuestaDatos(CodigoRespuesta.Aceptada, CodigoRespuesta.Aceptada,
                        "No hay conexion con la CCE", false);
                }

                return respuesta.ConstruirRespuestaDatos(CodigoRespuesta.Aceptada, CodigoRespuesta.Aceptada,
                        "Hay conexion con la CCE", true);
            }
            catch (Exception e)
            {
                return respuesta.ConstruirRespuestaGeneral(e.Message);
            }
        }
        /// <summary>
        /// Metodo que solo obtiene una lista de las promacion
        /// solo filtra para la caja tacna
        /// </summary>
        /// <returns>DtoPromacionSingOnOff</returns>
        public async Task<IList<SingOnOffProgramadoDTO>> ObtenerProgamacionSignOnOff()
        {
            var periodoEntidad = _repositorioOperacion.ObtenerPorExpresionConLimite<EntidadFinancieroInmediataPeriodo>
                (x => x.CodigoEntidad == General.CodigoEntidad);

           return ServicioDominioMaquetacion.MaquetarDatosPromamacionSignOnOff(periodoEntidad);
        }
        /// <summary>
        /// Gestiona la programaci�n de activaci�n o desactivaci�n (Sign On / Sign Off) de un per�odo
        /// financiero inmediato, validando la informaci�n recibida, aplicando la l�gica de negocio
        /// correspondiente y persistiendo los cambios en el repositorio.
        /// </summary>
        /// <param name="singProgamado">
        /// DTO que contiene los datos de la programaci�n Sign On / Sign Off del per�odo a gestionar.
        /// </param>
        /// <returns>
        /// La entidad <see cref="EntidadFinancieroInmediataPeriodo"/> gestionada o actualizada;
        /// en caso de error, retorna <c>null</c>.
        /// </returns>
        public async Task<EntidadFinancieroInmediataPeriodo> GestionarProgamacionSignOnOff(SingOnOffProgramadoDTO singProgamado)
        {
            try
            {
                var fecheActual = _repositorioOperacion.ObtenerPorCodigo<Calendario>(
               Empresa.CodigoPrincipal,
               _contextoAplicacion.CodigoAgencia,
               Sistema.Clientes);

                var periodoEntidad = _repositorioOperacion.ObtenerUnoONulo<EntidadFinancieroInmediataPeriodo>
                   (x => x.NumeroPeriodo == singProgamado.NumeroPeriodo);

                var entidadGestionada = LogicaGestionarProgramacionSing.GestionarPeriodoSing(
                    periodoEntidad,
                    singProgamado,
                    _contextoAplicacion.CodigoUsuario,
                    fecheActual.FechaHoraSistema
                    );

                _repositorioOperacion.Adicionar(entidadGestionada);
                _servicioAplicacionBitacora.ValidarEntidadTexto("Inicio del proceso para guardar cambios de Periodo Sing.");
                _repositorioOperacion.GuardarCambios();
                _servicioAplicacionBitacora.ValidarEntidadTexto("Finalizando del proceso para guardar cambios de Periodo Sing.");

                return entidadGestionada ?? periodoEntidad;
            }
            catch (Exception)
            {
                _servicioAplicacionBitacora.ValidarEntidadTexto("El proceso de gestion de Periodo Sing a Fallado.");
                return null;
            }
        }
        /// <summary>
        /// Actualiza el estado de programaci�n Sign On / Sign Off de un per�odo financiero inmediato,
        /// registrando la modificaci�n con el usuario y la fecha del sistema, y persistiendo
        /// los cambios en el repositorio correspondiente.
        /// </summary>
        /// <param name="estado">
        /// Nuevo estado que se asignar� al per�odo Sign On / Sign Off.
        /// </param>
        /// <param name="numeroPeriodo">
        /// Identificador del per�odo financiero inmediato a actualizar.
        /// </param>
        /// <returns>
        /// La entidad <see cref="EntidadFinancieroInmediataPeriodo"/> con el estado actualizado;
        /// en caso de error, retorna <c>null</c>.
        /// </returns>
        public async Task<EntidadFinancieroInmediataPeriodo> ActualizarEstadoPeriodoSignCmact(string estado, long numeroPeriodo)
        {
            try
            {

                var entidadFinanciera = _repositorioOperacion
                .ObtenerPorCodigo<EntidadFinancieroInmediataPeriodo>(numeroPeriodo);

                var fecheActual = _repositorioOperacion.ObtenerPorCodigo<Calendario>(
                  Empresa.CodigoPrincipal,
                  _contextoAplicacion.CodigoAgencia,
                  Sistema.Clientes);

                entidadFinanciera.ModificarEstadoProgamadoSing(
                    estado,
                    _contextoAplicacion.CodigoUsuario,
                    fecheActual.FechaHoraSistema
                    );
                _servicioAplicacionBitacora.ValidarEntidadTexto("Inicio del proceso para guardar estado para el Periodo Sing.");
                _repositorioOperacion.GuardarCambios();
                _servicioAplicacionBitacora.ValidarEntidadTexto("Finalizando del proceso para guardar estado para el Periodo Sing.");

                return entidadFinanciera;
            }
            catch (Exception)
            {

                _servicioAplicacionBitacora.ValidarEntidadTexto("El proceso del cambio estado de Periodo Sing a Fallado.");
                return null;
            }
        }
        #endregion

        #region Entidad Financiera
        /// <summary>
        /// M�todo para actualizar el estado de sign on o off
        /// </summary>
        /// <param name="estado"></param>
        private void ActualizarEstadoSignCmact(string estado)
        {
            var entidadFinanciera = _repositorioOperacion
                .ObtenerPorCodigo<EntidadFinancieraInmediata>(EntidadFinancieraInmediata.CodigoCajaTacna);

            entidadFinanciera.ActualizarEstadoSign(estado);

            _repositorioOperacion.GuardarCambios();
        }
        #endregion
    }
}