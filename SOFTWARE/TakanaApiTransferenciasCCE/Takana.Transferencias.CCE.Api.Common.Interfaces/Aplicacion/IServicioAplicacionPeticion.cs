using Takana.Transferencias.CCE.Api.Common.DTOs.Monitoreo;
using static Takana.Transferencias.CCE.Api.Common.Interoperatibilidad.EstructuraJsonInteroperatibilidad;

namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    /// <summary>
    /// Interfaz de servicio de aplicacion de peticiones.
    /// </summary>
    public interface IServicioAplicacionPeticion : IServicioBase
    {
        #region Aplicacion de peticiones de Entradas             
        /// <summary>
        /// Interfaz que envia el procesa automatico que solicitud estado de Pago al mismo Api Transferencia
        /// </summary>
        /// <param name="datosPayload">dato de Identificador de Instruccion</param>
        Task EnviarEstadoSolicitudPagoApiTransferencia(
            string datosPayload);

        /// <summary>
        /// Interfaz que envia el proceso de transferencias Inmediatas CCE
        /// </summary>
        /// <param name="identificadorInstruccion"></param>
        Task EnviarParaProcesarTransferenciaEntrante(
            string? identificadorInstruccion);

        /// <summary>
        /// Interfaz que envia el proceso de transferencias Inmediatas CCE
        /// </summary>
        /// <param name="identificadorInstruccion"></param>
        Task EnviarParaProcesarRechazoTransferenciaEntrante(
            string? identificadorInstruccion);

        /// <summary>
        /// Interfaz que envia la solicitud estado de pago a la CCE.
        /// </summary>
        /// <param name="datosPayload">datos de solicitud de estado de pago</param>
        /// <param name="numeroSeguimiento">numero de Trace</param>
        /// <returns>Retorna la respuesta de la solicitud de estado de pago(PSR2)</returns>
        Task<EstructuraContenidoPSR2> EnviarSolicitudEstadoPagoCCE(
            EstructuraContenidoPSR1 datosPayload, string numeroSeguimiento);
        #endregion

        #region Aplicacion de peticiones de Salidas
        /// <summary>
        /// Envia la orden de transferencia a la CCE
        /// </summary>
        /// <param name="cuerpo">Datos de la consulta</param>
        /// <param name="cantReintentos">Cantidad de reintentos</param>
        /// <returns>Resultado de Consulta cuenta</returns>
        Task<EsctruturaCT4> EnviarOrdenTransferencia(
            EsctruturaCT1 cuerpo, int cantReintentos);
        /// <summary>
        /// Envia la peticion de consulta cuenta a la CCE
        /// </summary>
        /// <param name="datosConsultaEnviar">Datos a enviar</param>
        /// <param name="cantReintentos">Cantidad de reintentos</param>
        /// <returns>Resultado de Consulta cuenta</returns>
        Task<EsctruturaAV4> ObtenerDatosCuentaCCE(
            EsctruturaAV1 datosConsultaEnviar, int cantReintentos);
        /// <summary>
        /// Envia Echo test a la CCE
        /// </summary>
        /// <param name="datos">Datos a enviar</param>
        /// <param name="numeroSeguimiento">Datos a enviar</param>
        /// <returns>Retorna respuesta de conexion</returns>
        Task<EsctruturaET2> EnviarEchoTest(EsctruturaET1 datos, string numeroSeguimiento);
        /// <summary>
        /// Envia el sign on a la CCE
        /// </summary>
        /// <param name="datosEnviar">Datos a enviar</param>
        /// <returns>Resultado de la peticion de SignOn</returns>
        Task<EstructuraSignOn2> EnviarSignOn(
            EstructuraSignOn1 datosEnviar);
        /// <summary>
        /// Envia el sign off a la CCE
        /// </summary>
        /// <param name="datosEnviar">Datos a enviar</param>
        /// <returns>Resultado de la peticion de SignOff</returns>
        Task<EstructuraSignOff2> EnviarSignOff(
            EstructuraSignOff1 datosEnviar);
        #endregion

        #region Servicio Interoperabilidad
        /// <summary>
        /// Metodo encargado de enviar el barrido de contactos a la CCE
        /// </summary>
        /// <param name="datosBarrido">Datos para el barrido de contactos</param>
        /// <returns>Retorna los datos del barrido de contactos</returns>
        Task<EstructuraBarridoContacto> EnviarBarridoContacto(
            EstructuraBarridoContacto datosBarrido);
        /// <summary>
        /// Metodo que envia el registro de directorio a la CCE
        /// </summary>
        /// <param name="datosRegistro">Datos para el barrido de contactos</param>
        /// <returns>Retorna los datos del Registro de contactos</returns>
        Task<EstructuraRegistroDirectorio> EnviarAfiliacionDirectorio(
            EstructuraRegistroDirectorio datosRegistro);
        /// <summary>
        /// Metodo que envia la peticion de generacion de token para interoperabilidad
        /// </summary>
        /// <param name="datosEntrada">Estructura de datos enviar</param>
        /// <returns>Retorna los datos del Token</returns>
        Task<EstructuraObtenerTokenRespuesta> EnviarGenerarToken(
            EstructuraObtenerToken datosEntrada);

        /// <summary>
        /// Método que envia para generar un QR
        /// </summary>
        /// <param name="datosEntrada"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<EstructuraGenerarQRRespuesta> EnviarGenerarQr(
            EstructuraGenerarQR datosEntrada, string token);
        /// <summary>
        /// Metodo que envia la peticion de generacion de lectura QR para interoperabilidad
        /// </summary>
        /// <param name="datosEntrada">Estructura de datos enviar</param>
        /// <param name="token"></param>
        /// <returns>Retorna los datos de la lectura de QR</returns>
        Task<EstructuraConsultaDatosQR> EnviarLecturaQr(
            EstructuraLeerQR datosEntrada, string token);
        /// <summary>
        /// Metodo que envia la peticion de generacion de consulta de dato de QR para interoperabilidad
        /// </summary>
        /// <param name="datosEntrada">Estructura de datos enviar</param>
        /// <param name="token"></param>
        /// <returns>Retorna los datos de consulta de QR</returns>
        Task<EstructuraConsultaDatosQR> ProcesarPeticionObtenerDatosQr(
            EstructuraObtenerDatosQR datosEntrada, string token);

        #endregion Interoperabilidad

        #region Servicio de peticiones HSM
        /// <summary>
        /// Servicio de encriptacion de mensajes CCE
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        Task<object> EncriptarMensajeCCE(
            object datos);

        /// <summary>
        /// Servicio de Desencriptacion de mensajes CCE
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        Task<T> DesencriptarMensajeCCE<T>(
            EstructuraSeguridadCCE datos);

        #endregion

        #region Servicio de Monitoreo
        /// <summary>
        /// Metodo que se encargado de solicitar un resumen de disponibilidad
        /// </summary>
        /// <param name="datos">datos del resumen</param>
        Task<List<MonitoreoEventosReporteDTO>> ObtenerRegistrosDisponibilidad(MonitoreoDTO datos);
        #endregion
    }
}
