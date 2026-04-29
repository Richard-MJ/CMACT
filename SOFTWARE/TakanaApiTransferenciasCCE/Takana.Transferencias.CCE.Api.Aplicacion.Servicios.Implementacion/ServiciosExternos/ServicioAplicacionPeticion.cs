using Polly;
using Polly.Wrap;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Common.DTOs.Monitoreo;
using Takana.Transferencias.CCE.Api.Common.SolicitudEstadoPago;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using static Takana.Transferencias.CCE.Api.Common.Interoperatibilidad.EstructuraJsonInteroperatibilidad;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.ServiciosExternos
{
    /// <summary>
    /// Servicio general para las operaciones a servicios APIs
    /// </summary>
    public class ServicioAplicacionPeticion : IServicioAplicacionPeticion
    {
        #region Declaracion de variables
        private readonly IConfiguration _configuration;
        private readonly IBitacora<ServicioAplicacionPeticion> _bitacora;
        private readonly static SemaphoreSlim _semaforo = new SemaphoreSlim(2);
        private readonly IServicioAplicacionPeticionBase _peticionServicioBase;
        private readonly IServicioAplicacionSeguridad _servicioAplicacionSeguridad;
        private readonly IServicioAplicacionParametroGeneral _parametroGeneralServicio;
        /// <summary>
        /// Encabezado de las transacciones
        /// </summary>
        private Dictionary<string, string> _header = new Dictionary<string, string>();
        /// <summary>
        /// URL servicio CCE
        /// </summary>
        private string _urlServicio = string.Empty;
        /// <summary>
        /// URL recurso del servicio
        /// </summary>
        private string _recurso = string.Empty;
        /// <summary>
        /// Tiempo de espera por peticion
        /// </summary>
        private int _timeoutServicio;
        /// <summary>
        /// Indicador si esta habilitado el entorno de encryptacion
        /// </summary>
        private bool _entornoEncriptacion;
        /// <summary>
        /// Indicador si esta habilitado el entorno de encryptacion
        /// </summary>
        private string codigoErrorFirmaMensaje = RazonRespuesta.codigoERRFIRMA;
        /// <summary>
        /// Referencia a la politica de reintentos
        /// </summary>
        private AsyncPolicyWrap _asyncPolicyWrap;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        public ServicioAplicacionPeticion(
            IConfiguration configuration,
            IBitacora<ServicioAplicacionPeticion> bitacora,
            IServicioAplicacionPeticionBase peticionServicioBase,
            IServicioAplicacionSeguridad servicioAplicacionSeguridad,
            IServicioAplicacionParametroGeneral parametrosGeneralesServicio)
        {
            _bitacora = bitacora;
            _configuration = configuration;
            _peticionServicioBase = peticionServicioBase;
            _parametroGeneralServicio = parametrosGeneralesServicio;
            _servicioAplicacionSeguridad = servicioAplicacionSeguridad;
            _entornoEncriptacion = _configuration.GetValue<bool>("TAK_USAR_ENTORNO_SEGURIDAD_CCE");
            _timeoutServicio = Convert.ToInt32(_parametroGeneralServicio
                .obtenerParametrosConfiguracion(ParametroGeneralTransferencia.TiempoReintento));
            var cantidadReintentos = Convert.ToInt32(_parametroGeneralServicio
                .obtenerParametrosConfiguracion(ParametroGeneralTransferencia.MaximoReintento));
            _asyncPolicyWrap = _servicioAplicacionSeguridad.PoliticasReintento(cantidadReintentos);
        }
        #endregion

        #region Servicio Entradas
        /// <summary>
        /// Metodo que se encargado de procesar la transferencia entrante
        /// </summary>
        /// <param name="identificadorInstruccion">Numero de identificacion de la transferencia</param>
        /// <returns>True si se proceso correctamente</returns>
        public async Task EnviarParaProcesarTransferenciaEntrante(
            string identificadorInstruccion)
        {
            try
            {
                _urlServicio = _configuration["URL_SERVICIO_API_INMEDIATA"]!;
                _recurso = _configuration["END_POINT_PROCESAR_TRANSFERENCIA_ENTRANTE"]!;

                await _peticionServicioBase.EjecutarRequestAsync<bool>(
                    _urlServicio, _recurso, HttpMethod.Post, _header, _timeoutServicio, identificadorInstruccion);
            }
            catch (Exception excepcion)
            {
                Console.Write(excepcion);
            }
        }
        /// <summary>
        /// Metodo que procesa el rechazo de transferencia entrante
        /// </summary>
        /// <param name="identificadorInstruccion">Numero de identificacion de la transferencia</param>
        /// <returns>True si se proceso correctamente</returns>
        public async Task EnviarParaProcesarRechazoTransferenciaEntrante(
            string identificadorInstruccion)
        {
            try
            {
                _urlServicio = _configuration["URL_SERVICIO_API_INMEDIATA"]!;
                _recurso = _configuration["END_POINT_PROCESAR_RECHAZO_TRANSFERENCIA_ENTRANTE"]!;

                await _peticionServicioBase.EjecutarRequestAsync<bool>(
                    _urlServicio, _recurso, HttpMethod.Post, _header, _timeoutServicio, identificadorInstruccion);
            }
            catch (Exception excepcion)
            {
                Console.Write(excepcion);
            }
        }

        /// <summary>
        /// Metodo que envia el procesa automatico que solicitud estado de Pago al mismo Api Transferencia
        /// </summary>
        /// <param name="datos">dato de Identificador de Instruccion</param>
        public async Task EnviarEstadoSolicitudPagoApiTransferencia(
           string datos)
        {
            try
            {
                _urlServicio = _configuration["URL_SERVICIO_API_INMEDIATA"]!;
                _recurso = _configuration["END_POINT_SOLICITUD_ESTADO_PAGO"]!;

                await _peticionServicioBase.EjecutarRequestAsync<SolicitudEstadoPagoRespuestaDTO>(
                    _urlServicio, _recurso, HttpMethod.Post, _header, _timeoutServicio, datos);
            }
            catch (Exception excepcion)
            {
                Console.Write(excepcion);
            }
        }

        /// <summary>
        /// Metodo que envia la solicitud estado de pago a la CCE.
        /// </summary>
        /// <param name="datos">datos de solicitud de estado de pago</param>
        /// <returns>Retorna la respuesta de la solicitud de estado de pago(PSR2)</returns>
        public async Task<EstructuraContenidoPSR2> EnviarSolicitudEstadoPagoCCE(
            EstructuraContenidoPSR1 datos, string numeroSeguimiento){
            try{

                _header.Add("request-id", EntidadFinancieraInmediata.SoloCodigoCajaTacna + numeroSeguimiento);

                _urlServicio = _configuration["URL_SERVICIO_OPERADORA_CCE"]!;
                _recurso = _configuration["END_POINT_SOLICITUD_ESTADO_PAGO"]!;

                if (_entornoEncriptacion)
                    return await EnviarDatosEncriptadosCCE<EstructuraContenidoPSR2>(datos);

                return await _peticionServicioBase.EjecutarRequestAsync<EstructuraContenidoPSR2>(
                    _urlServicio, _recurso, HttpMethod.Post, _header, _timeoutServicio, datos, true);
            }
            catch (Exception excepcion)
            {
                Console.Write(excepcion);
                return new EstructuraContenidoPSR2();
            }
        }

        #endregion

        #region Servicio Salidas
        /// <summary>
        /// Envia la peticion de consulta cuenta a la CCE
        /// </summary>
        /// <param name="datos">Datos a enviar</param>
        /// <param name="cantReintentos">Cantidad de reintentos</param>
        /// <returns>Resultado de Consulta cuenta</returns>
        public async Task<EsctruturaAV4> ObtenerDatosCuentaCCE(EsctruturaAV1 datos, int cantReintentos)
        {
            try
            {
                if (_header.ContainsKey("request-id"))
                    _header["request-id"] = EntidadFinancieraInmediata.SoloCodigoCajaTacna + datos.AV1.trace;
                else
                    _header.Add("request-id", EntidadFinancieraInmediata.SoloCodigoCajaTacna + datos.AV1.trace);

                _urlServicio =_configuration["URL_SERVICIO_OPERADORA_CCE"]!;
                _recurso = _configuration["END_POINT_CONSULTA_CUENTA_OPERADORA" ]!;

                if (_entornoEncriptacion)
                    return await EnviarDatosEncriptadosCCE<EsctruturaAV4>(datos);
                else
                {
                    return await _peticionServicioBase.EjecutarRequestAsync<EsctruturaAV4>(
                        _urlServicio, _recurso, HttpMethod.Post, _header, _timeoutServicio, datos, true);
                }
            }
            catch (Exception)
            {
                throw new ValidacionException(RazonRespuesta.codigoERRCONSUL);
            }
           
        }
        /// <summary>
        /// Envia la orden de transferencia a la CCE
        /// </summary>
        /// <param name="cuerpo">Datos de la consulta</param>
        /// <param name="cantidadReintento">Cantidad de reintentos</param>
        /// <returns>Resultado de Consulta cuenta</returns>
        public async Task<EsctruturaCT4> EnviarOrdenTransferencia(EsctruturaCT1 datos, int cantidadReintento)
        {
            try {
                if (_header.ContainsKey("request-id"))
                    _header["request-id"] = EntidadFinancieraInmediata.SoloCodigoCajaTacna + datos.CT1.trace;
                else
                    _header.Add("request-id", EntidadFinancieraInmediata.SoloCodigoCajaTacna + datos.CT1.trace);

                _urlServicio = _configuration["URL_SERVICIO_OPERADORA_CCE"]!;
                _recurso =_configuration["END_POINT_ORDEN_TRANSFERENCIA_CUENTA_OPERADORA"]!;

                var pollyContext = new Context();
                return await _asyncPolicyWrap.ExecuteAsync(async (context) =>
                {

                    var intento = context.ContainsKey("RetryAttempt") ? (int)context["RetryAttempt"] : 0;
                    if (intento == 1) datos.CT1.messageTypeId = DatosGenerales.Reintento;

                    if (_entornoEncriptacion)
                        return await EnviarDatosEncriptadosCCE<EsctruturaCT4>(datos);
                    else
                    {
                        return await _peticionServicioBase.EjecutarRequestAsync<EsctruturaCT4>(
                            _urlServicio, _recurso, HttpMethod.Post, _header, _timeoutServicio, datos, true);
                    }
                }, pollyContext);
            }
            catch (Exception)
            {
                throw new Exception(DatosGenerales.IndicadorIntentosRealizados);
            }
        }

        /// <summary>
        /// Envia el sign on a la CCE
        /// </summary>
        /// <param name="datosEnviar">Datos a enviar</param>
        /// <returns>Resultado de la peticion de SignOn</returns>
        public async Task<EstructuraSignOn2> EnviarSignOn(EstructuraSignOn1 datos)
        {
            _header.Add("request-id", EntidadFinancieraInmediata.SoloCodigoCajaTacna + datos.SignOn1.trace);

            _urlServicio = _configuration["URL_SERVICIO_OPERADORA_CCE"]!;
            _recurso = _configuration["END_POINT_SIGN_ON"]!;

            var resultado = new EstructuraSignOn2();
            await _asyncPolicyWrap.ExecuteAsync(async () =>
            {
                if (_entornoEncriptacion)
                    resultado = await EnviarDatosEncriptadosCCE<EstructuraSignOn2>(datos);
                else
                {
                    resultado = await _peticionServicioBase.EjecutarRequestAsync<EstructuraSignOn2>(
                        _urlServicio, _recurso, HttpMethod.Post, _header, _timeoutServicio, datos, true);
                }
            });
            return resultado;
        }

        /// <summary>
        /// Envia el sign off a la CCE
        /// </summary>
        /// <param name="datos">Datos a enviar</param>
        /// <returns>Resultado de la peticion de SignOff</returns>
        public async Task<EstructuraSignOff2> EnviarSignOff(EstructuraSignOff1 datos)
        {
            _header.Add("request-id", EntidadFinancieraInmediata.SoloCodigoCajaTacna + datos.SignOff1.trace);

            _urlServicio = _configuration["URL_SERVICIO_OPERADORA_CCE"]!;
            _recurso = _configuration["END_POINT_SIGN_OFF"]!;

            var resultado = new EstructuraSignOff2();
            await _asyncPolicyWrap.ExecuteAsync(async () =>
            {
                if (_entornoEncriptacion)
                    resultado = await EnviarDatosEncriptadosCCE<EstructuraSignOff2>(datos);
                else
                {
                    resultado = await _peticionServicioBase.EjecutarRequestAsync<EstructuraSignOff2>(
                        _urlServicio, _recurso, HttpMethod.Post, _header, _timeoutServicio, datos, true);
                }
            });
            return resultado;    
        }

        /// <summary>
        /// Envia Echo test a la CCE
        /// </summary>
        /// <param name="datos">Datos a enviar</param>
        /// <param name="numeroSeguimiento">Datos a enviar</param>
        /// <returns>Retorna respuesta de conexion</returns>
        public async Task<EsctruturaET2> EnviarEchoTest(EsctruturaET1 datos, string numeroSeguimiento)
        {
            _header.Add("request-id", EntidadFinancieraInmediata.SoloCodigoCajaTacna + numeroSeguimiento);

            _urlServicio = _configuration["URL_SERVICIO_OPERADORA_CCE"]!;
            _recurso = _configuration["END_POINT_ECHO_TEST"]!;

            if (_entornoEncriptacion)
                return await EnviarDatosEncriptadosCCE<EsctruturaET2>(datos);

            return await _peticionServicioBase.EjecutarRequestAsync<EsctruturaET2>(
                _urlServicio, _recurso, HttpMethod.Post, _header, _timeoutServicio, datos, true);
        }

        #endregion

        #region Servicio Interoperabilidad
        /// <summary>
        /// Metodo encargado de enviar el barrido de contactos a la CCE
        /// </summary>
        /// <param name="datos">Datos para el barrido de contactos</param>
        /// <param name="tiempoEspera">Tiempo de espera</param>
        /// <returns>Retorna los datos del barrido de contactos</returns>
        public async Task<EstructuraBarridoContacto> EnviarBarridoContacto(
            EstructuraBarridoContacto datos)
        {
            try
            {
                _header.Add("Connection", "Keep-Alive");
                _header.Add("Message", "/NoYamlValidation");

                _urlServicio = _configuration["URL_SERVICIO_OPERADORA_CCE_INTEROPERABILIDAD_BARRIDO"]!;
                _recurso = _configuration["END_POINT_INTEROPERABILIDAD_BARRIDO"]!;

                if (_entornoEncriptacion)
                    return await EnviarDatosEncriptadosCCE<EstructuraBarridoContacto>(datos);

                return await _peticionServicioBase.EjecutarRequestAsync<EstructuraBarridoContacto>(
                    _urlServicio, _recurso, HttpMethod.Post, _header, _timeoutServicio, datos, true);
            }
            catch (Exception)
            {
                throw new Exception("Ocurrio un incoveniente al enviar el barrido de contactos");
            }
            
        }
        /// <summary>
        /// Metodo que envia el registro de directorio a la CCE
        /// </summary>
        /// <param name="datos">Datos para el barrido de contactos</param>
        /// <returns>Retorna los datos del Registro de contactos</returns>
        public async Task<EstructuraRegistroDirectorio> EnviarAfiliacionDirectorio(
            EstructuraRegistroDirectorio datos)
        {
            try
            {
                if (!_header.ContainsKey("Connection"))
                    _header.Add("Connection", "Keep-Alive");

                if (!_header.ContainsKey("Message"))
                    _header.Add("Message", "/ProxyRegistrationV01");

                _recurso = _configuration["END_POINT_INTEROPERABILIDAD_REGISTRO_DIRECTORIO"]!;
                _urlServicio = _configuration["URL_SERVICIO_OPERADORA_CCE_INTEROPERABILIDAD"]!;

               if (_entornoEncriptacion)
                    return await EnviarDatosEncriptadosCCE<EstructuraRegistroDirectorio>(datos);

                return await _peticionServicioBase.EjecutarRequestAsync<EstructuraRegistroDirectorio>(
                    _urlServicio, _recurso, HttpMethod.Post, _header, _timeoutServicio, datos, true);
            }
            catch (Exception)
            {
                throw new ValidacionException("Ocurrio un incoveniente al enviar el registro al Directorio CCE");
            }
            
        }
        /// <summary>
        /// Metodo que envia la peticion de generacion de token para interoperabilidad
        /// </summary>
        /// <param name="datos">Estructura de datos enviar</param>
        /// <returns>Retorna los datos del Token</returns>
        public async Task<EstructuraObtenerTokenRespuesta> EnviarGenerarToken(
            EstructuraObtenerToken datos)
        {
            try
            {
                var usuario = _configuration["USUARIO_INTEROPERABILIDAD_QR"]!;
                var clave = _configuration["CLAVE_INTEROPERABILIDAD_QR"]!;
                _header.Add("user", usuario);
                _header.Add("password", clave);

                _urlServicio = _configuration["URL_SERVICIO_OPERADORA_CCE_INTEROPERABILIDAD_QR"]!;
                _recurso = _configuration["END_POINT_INTEROPERABILIDAD_AUTORIZACION_QR"]!;

                return await _peticionServicioBase.EjecutarRequestAsync<EstructuraObtenerTokenRespuesta>(_urlServicio, _recurso,
                HttpMethod.Post, _header, _timeoutServicio, datos, false);
            }
            catch (Exception)
            {
                throw new Exception("Ocurrio un inconveniente al generar el Token de la CCE");
            }
        }
        /// <summary>
        /// Metodo que envia la peticion de generacion de QR para interoperabilidad
        /// </summary>
        /// <param name="datos">Estructura de datos enviar</param>
        /// <returns>Retorna los datos del QR</returns>
        public async Task<EstructuraGenerarQRRespuesta> EnviarGenerarQr(
            EstructuraGenerarQR datos, string token)
        {
            try
            {
                _header.Add("Authorization", "Token " + token);

                _urlServicio = _configuration["URL_SERVICIO_OPERADORA_CCE_INTEROPERABILIDAD_QR"]!;
                _recurso = _configuration["END_POINT_INTEROPERABILIDAD_GENERAR_QR"]!;

                return await _peticionServicioBase.EjecutarRequestAsync<EstructuraGenerarQRRespuesta>(
                    _urlServicio, _recurso, HttpMethod.Post, _header, _timeoutServicio, datos, false);
            }
            catch (Exception)
            {
                throw new Exception("Ocurrio un inconveniente al generar un QR");
            }
            
        }
        /// <summary>
        /// Metodo que envia la peticion de generacion de lectura QR para interoperabilidad
        /// </summary>
        /// <param name="datos">Estructura de datos enviar</param>
        /// <param name="token">Estructura de datos enviar</param>
        /// <returns>Retorna los datos de la lectura de QR</returns>
        public async Task<EstructuraConsultaDatosQR> EnviarLecturaQr(
            EstructuraLeerQR datos, string token)
        {
            try
            {
                _header.Add("Authorization", "Token " + token);

                _urlServicio = _configuration["URL_SERVICIO_OPERADORA_CCE_INTEROPERABILIDAD_QR"]!;
                _recurso = _configuration["END_POINT_INTEROPERABILIDAD_LEER_QR"]!;

                return await _peticionServicioBase.EjecutarRequestAsync<EstructuraConsultaDatosQR>(
                    _urlServicio, _recurso, HttpMethod.Post, _header, _timeoutServicio, datos, false);
            }
            catch (Exception)
            {
                throw new Exception("Ocurrio un inconveniente al intentar realizar la Lectura de QR");
            }
           
        }
        /// <summary>
        /// Metodo que envia la peticion de generacion de consulta de dato de QR para interoperabilidad
        /// </summary>
        /// <param name="datos">Estructura de datos enviar</param>
        /// <returns>Retorna los datos de consulta de QR</returns>
        public async Task<EstructuraConsultaDatosQR> ProcesarPeticionObtenerDatosQr(
            EstructuraObtenerDatosQR datos, string token)
        {
            try
            {
                _header.Add("Authorization", "Token " + token);

                _urlServicio = _configuration["URL_SERVICIO_OPERADORA_CCE_INTEROPERABILIDAD_QR"]!;
                _recurso = _configuration["END_POINT_INTEROPERABILIDAD_OBTENER_DATOS_QR"]!;

                return await _peticionServicioBase.EjecutarRequestAsync<EstructuraConsultaDatosQR>(
                    _urlServicio, _recurso, HttpMethod.Post, _header, _timeoutServicio, datos, false);
            }
            catch (Exception)
            {
                throw new Exception("Error al enviar la Obtencion de datos de QR");
            }
            
        }
        
       
        #endregion Interoperabilidad

        #region Servicio Monitoreo
        /// <summary>
        /// Metodo que se encargado de solicitar un resumen de disponibilidad
        /// </summary>
        /// <param name="datos">datos del resumen</param>
        public async Task<List<MonitoreoEventosReporteDTO>> ObtenerRegistrosDisponibilidad(MonitoreoDTO datos)
        {
            try
            {
                var servicioTinInmediata = _configuration["COD_SERVICIO_TIN_INMEDIATA_MONITOREO"]!;

                _urlServicio = _configuration["URL_SERVICIO_MONITOREO"]!;
                _recurso = _configuration["END_POINT_REGISTROS_MONITOREO"]!
                    + $"?startDate={datos.startDate}&endDate={datos.endDate}&services={servicioTinInmediata}";

                return await _peticionServicioBase.EjecutarRequestAsync<List<MonitoreoEventosReporteDTO>>(
                    _urlServicio, _recurso, HttpMethod.Get, _header, _timeoutServicio, null, false);
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }
        #endregion

        #region Servicio HSM

        /// <summary>
        /// Enviar datos de encriptación a la CCE
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datos"></param>
        /// <returns></returns>
        public async Task<T> EnviarDatosEncriptadosCCE<T>(
            object datos)
        {
            try
            {
                bool usarHSM = _configuration.GetValue<bool>("TAK_USAR_MODULO_SEGURIDAD_HARDWARE")!;
                var datosEncriptados = await EncriptarMensajeCCE(datos);

                var mensaje = _header.ContainsKey("request-id")
                    ? $"Se envió un mensaje a la CCE con su resquest-id {_header["request-id"]} : {datosEncriptados}"
                    : $"Se envió un mensaje a la CCE: {datosEncriptados}";
                _bitacora.Trace(mensaje);

                var respuesta = await _peticionServicioBase.EjecutarRequestAsync<EstructuraSeguridadCCE>(
                    _urlServicio, _recurso, HttpMethod.Post, _header, _timeoutServicio, datosEncriptados, true);

                var datosDesencriptados = usarHSM
                    ? await DesencriptarMensajeCCE<T>(respuesta)
                    : await _servicioAplicacionSeguridad.DesencriptarMensajeCCE<T>(respuesta);
                return datosDesencriptados;
            }
            catch (Exception excepcion)
            {
                throw new Exception($"Ocurrio un error inesperado al enviar a la CCE: {excepcion.Message}");
            }
        }

        /// <summary>
        /// Envia la Encriptación del Mensaje de la CCE al HSM
        /// </summary>
        /// <param name="datos"></param>
        /// <returns>Retorna Mensaje de respuesta del HSM</returns>
        /// <exception cref="Exception"></exception>
        public async Task<object> EncriptarMensajeCCE(
            object datos)
        {
            await _semaforo.WaitAsync();
            try
            {
                if (!_header.ContainsKey("Connection"))
                    _header.Add("Connection", "Keep-Alive");

                var urlServicio = _configuration["URL_SERVICIO_API_PIN_OPERACIONES"]!;
                var recurso = _configuration["END_POINT_ENCRIPTAR_HSM"]!;

                var payload = new EstructuraContenidoCCE
                {
                    Header = _servicioAplicacionSeguridad.ObtenerHeaderHuellaDigitalCertificadoCCE(),
                    Contenido = ShortGuid.Base64UrlEncode(JsonConvert.SerializeObject(datos))
                };

                var resultado = await _peticionServicioBase.EjecutarRequestAsync<RespuestaHSMDTO>(
                    urlServicio, recurso, HttpMethod.Post, _header, _timeoutServicio, payload);

                await _servicioAplicacionSeguridad.ValidarFirmaDigitalCMAC(resultado.Datos, payload.Contenido);

                var estructuraCCE = JsonConvert.DeserializeObject<EstructuraSeguridadCCE>(resultado.Datos.Mensaje);

                var contenido = new
                {
                    payload = estructuraCCE!.Cuerpo,
                    @protected = estructuraCCE.Proteccion,
                    signature = estructuraCCE.Firma
                };

                return contenido;
            }
            catch (Exception)
            {
                throw new Exception(codigoErrorFirmaMensaje);
            }
            finally
            {
                _semaforo.Release();
            }
        }

        /// <summary>
        /// Envia la Desencriptacion del Mensaje de la CCE al HSM
        /// </summary>
        /// <param name="datos"></param>
        /// <returns>Retorna Mensaje de respuesta del HSM</returns>
        /// <exception cref="Exception"></exception>
        public async Task<T> DesencriptarMensajeCCE<T>(
            EstructuraSeguridadCCE datos)
        {
            await _semaforo.WaitAsync();
            try
            {
                if (!_header.ContainsKey("Connection"))
                    _header.Add("Connection", "Keep-Alive");

                var urlServicio = _configuration["URL_SERVICIO_API_PIN_OPERACIONES"]!;
                var recurso = _configuration["END_POINT_DESENCRIPTAR_HSM"]!;

                var resultado = await _peticionServicioBase.EjecutarRequestAsync<RespuestaHSMDTO>(
                    urlServicio, recurso, HttpMethod.Post, _header, _timeoutServicio, datos);

                if (resultado.Datos.Resultado == RespuestaHSMDTO.CodigoErrorHSM 
                    && !resultado.Datos.Mensaje.Contains(' ')) 
                    codigoErrorFirmaMensaje = resultado.Datos.Mensaje;

                return JsonConvert.DeserializeObject<T>(resultado.Datos.Mensaje)!;
            }
            catch (Exception)
            {
                throw new Exception(codigoErrorFirmaMensaje);
            }
            finally
            {
                _semaforo.Release();
            }
        }

        #endregion
    }
}