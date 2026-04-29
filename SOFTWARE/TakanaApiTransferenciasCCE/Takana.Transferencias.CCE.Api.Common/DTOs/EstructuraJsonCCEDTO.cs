using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;
using Takana.Transferencias.CCE.Api.Common.EchoTest;
using Takana.Transferencias.CCE.Api.Common.Rechazos;
using Takana.Transferencias.CCE.Api.Common.SignOnOff;
using Takana.Transferencias.CCE.Api.Common.Cancelaciones;
using Takana.Transferencias.CCE.Api.Common.Notificaciones;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.SolicitudEstadoPago;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;
using Takana.Transferencias.CCE.Api.Common.Constantes;

namespace Takana.Transferencias.CCE.Api.Common
{
    #region Estructura Cabecera
    /// <summary>
    /// Estructura del encabezado general
    /// </summary>
    public class EstructuraEncabezado
    {
        /// <summary>
        /// Contiene el URL del dominio
        /// </summary>
        [FromHeader(Name = "Host")]
        public string Host { get; set; }
        /// <summary>
        ///  Contiene la informacion del Accept del encabezado
        /// </summary>
        [FromHeader(Name = "Accept")]
        public string Aceptar { get; set; }
        /// <summary>
        ///  Contiene la informacion  de codificacion del Accept del encabezado
        /// </summary>

        [FromHeader(Name = "Accept-Encoding")]
        public string CodificacionAceptar { get; set; }
        /// <summary>
        /// Contiene la informacion del identificador de la solicitud
        /// </summary>

        [FromHeader(Name = "request-id")]
        public string IdentificadorSolicitud { get; set; }
        /// <summary>
        /// Contiene el tipo de contenido
        /// </summary>
        [FromHeader(Name = "Content-Type")]
        public string TipoContenido { get; set; }
        /// <summary>
        /// Longitud del contenido
        /// </summary>

        [FromHeader(Name = "Content-Length")]
        public string LongitudContenido { get; set; }
    }
    #endregion

    #region Estructura Seguridad CCE

    /// <summary>
    ///  Estructura de Contenido cuando esta Habilitado el entorno de Encriptacion de la CCE
    /// </summary>
    public record EstructuraContenidoCCE
    {
        /// <summary>
        /// Encabezado de la estructura de la CCE
        /// </summary>
        public string Header { get; set; }
        /// <summary>
        /// Contenido de la estructura de la CCE
        /// </summary>
        public string Contenido { get; set; }
    }

    /// <summary>
    ///  Estructura de Seguridad cuando esta Habilitado el entorno de Encriptacion de la CCE
    /// </summary>
    public record EstructuraSeguridadCCE
    {
        /// <summary>
        ///  Mensaje con estructura JSON serializado
        /// </summary>
        [JsonProperty("payload")]
        [JsonPropertyName("payload")]
        public string Cuerpo { get; set; }
        /// <summary>
        ///   Contiene la información relevante al algoritmo y certificados utilizados para la generación de la firma digital(JWS).
        /// </summary>
        [JsonProperty("protected")]
        [JsonPropertyName("protected")]
        public string Proteccion { get; set; }
        /// <summary>
        ///  Contiene el resultado de aplicar la firma (JWS) sobre el Payload y Protected, conteniendo el mensaje JSON
        /// </summary>
        [JsonProperty("signature")]
        [JsonPropertyName("signature")]
        public string Firma { get; set; }
    }

    /// <summary>
    ///  Estructura de Seguridad de Protected cuando esta Habilitado el entorno de Encriptacion de la CCE
    /// </summary>
    public record EstructuraProtectedCCE
    {
        /// <summary>
        /// Contiene la información del algoritmo de encriptacion
        /// </summary>
        public string alg { get; set; }
        /// <summary>
        /// Contiene la huella digital del certificado Codificada en BASE 64
        /// </summary>
        public string x5t { get; set; }
    }

    #endregion

    #region Estructuras de Entradas
    /// <summary>
    /// Representa la Estructura Recibida de Consulta de Cuenta
    /// Hace referencia al Tramo 2 (AV2) 7.2.3.2.2
    /// </summary>
    [SwaggerSchema("Representa la Estructura Recibida de Consulta de Cuenta")]
    public class EstructuraContenidoAV2
    {
        /// <summary>
        /// Datos de la Consulta Cuenta del Tramo 2 (AV2)
        /// </summary>
        public ConsultaCuentaRecepcionEntradaDTO AV2 { get; set; }
        /// <summary>
        /// Propiedad para validación de firma
        /// </summary>
        public string codigoValidacionFirma { get; set; } = RazonRespuesta.codigo0000;
    }

    /// <summary>
    /// Representa la Estructura Respondida de Consulta de Cuenta
    /// Hace referencia al Tramo 3 (AV3) 7.2.3.2.3
    /// </summary>
    [SwaggerSchema("Representa la Estructura Respondida de Consulta de Cuenta")]
    public record EstructuraContenidoAV3
    {
        /// <summary>
        /// Datos de la Consulta Cuenta del Tramo 3 (AV3)
        /// </summary>
        public ConsultaCuentaRespuestaEntradaDTO AV3 { get; set; }
    }

    /// <summary>
    /// Representa la Estructura Recibida de Orden de Transferencia
    /// Hace referencia al Tramo 2 (CT2) 7.2.3.3.2
    /// </summary>
    [SwaggerSchema("Representa la Estructura Recibida de Orden de Transferencia")]
    public record EstructuraContenidoCT2
    {
        /// <summary>
        /// Datos de la Orden de Transferencia del Tramo 2 (CT2)
        /// </summary>
        public OrdenTransferenciaRecepcionEntradaDTO CT2 { get; set; }
        /// <summary>
        /// Propiedad para validación de firma
        /// </summary>
        public string codigoValidacionFirma { get; set; } = RazonRespuesta.codigo0000;
    }

    /// <summary>
    /// Representa la Estructura Respondida de Orden de Transferencia
    /// Hace referencia al Tramo 3 (CT3) 7.2.3.3.3
    /// </summary>
    [SwaggerSchema("Representa la Estructura Respondida de Orden de Transferencia")]
    public record EstructuraContenidoCT3
    {
        /// <summary>
        /// Datos de la Orden de Transferencia del Tramo 3 (CT3)
        /// </summary>
        public OrdenTransferenciaRespuestaEntradaDTO CT3 { get; set; }
    }
    /// <summary>
    /// Representa la Estructura Recibida de la Confirmacion de Orden de Transferencia
    /// Hace referencia al Tramo 5 (CT5) 7.2.3.3.5
    /// </summary>
    [SwaggerSchema("Representa la Estructura Recibida de la Confirmacion de Orden de Transferencia")]
    public record EstructuraContenidoCT5
    {
        /// <summary>
        /// Datos de la Confirmacion Orden de Transferencia del Tramo 5 (CT5)
        /// </summary>
        public OrdenTransferenciaConfirmacionEntradaDTO CT5 { get; set; }
        /// <summary>
        /// Propiedad para validación de firma
        /// </summary>
        public string codigoValidacionFirma { get; set; } = RazonRespuesta.codigo0000;
    }

    /// <summary>
    /// Representa la Estructura Recibida de la Cancelación de Orden de Transferencia
    /// Hace referencia al Tramo 1 (CTC1) 7.2.3.4.1
    /// </summary>
    [SwaggerSchema("Representa la Estructura Recibida de la Cancelación de Orden de Transferencia")]
    public record EstructuraContenidoCTC1
    {
        /// <summary>
        /// Datos de la Cancelación Orden de Transferencia del Tramo 1 (CTC1)
        /// </summary>
        public CancelacionRecepcionDTO CTC1 { get; set; }
        /// <summary>
        /// Propiedad para validación de firma
        /// </summary>
        public string codigoValidacionFirma { get; set; } = RazonRespuesta.codigo0000;
    }

    /// <summary>
    /// Representa la Estructura Respondida de la Cancelación de Orden de Transferencia
    /// Hace referencia al Tramo 2 (CTC2) 7.2.3.4.2
    /// </summary>
    [SwaggerSchema("Representa la Estructura Respondida de la Cancelación de Orden de Transferencia")]
    public record EstructuraContenidoCTC2
    {
        /// <summary>
        /// Datos de la Cancelación Orden de Transferencia del Tramo 2 (CTC2)
        /// </summary>
        public CancelacionRespuestaDTO CTC2 { get; set; }
    }

    /// <summary>
    /// Representa la Estructura Enviada de Solicitud Estado de Pago
    /// Hace referencia al Tramo 1 (PSR1) 7.2.3.5.1
    /// </summary>
    [SwaggerSchema("Representa la Estructura Enviada de Solicitud Estado de Pago")]
    public record EstructuraContenidoPSR1
    {
        /// <summary>
        /// Datos de Solicitud Estado de Pago del Tramo 1 (PSR1)
        /// </summary>
        public SolicitudEstadoPagoSalidaDTO PSR1 { get; set; }
    }

    /// <summary>
    /// Representa la Estructura Respondida de Solicitud Estado de Pago
    /// Hace referencia al Tramo 2 (PSR2) 7.2.3.5.2
    /// </summary>
    [SwaggerSchema("Representa la Estructura Respondida de Solicitud Estado de Pago")]
    public record EstructuraContenidoPSR2
    {
        /// <summary>
        /// Datos de Solicitud Estado de Pago del Tramo 2 (PSR2)
        /// </summary>        
        public SolicitudEstadoPagoRespuestaDTO PSR2 { get; set; }
    }

    /// <summary>
    /// Representa la Estructura Recibida de Rechazo por estructura
    /// Hace referencia al Tramo 2 (PSR2) 7.2.3.5.2
    /// </summary>
    [SwaggerSchema("Representa la Estructura Recibida de Rechazo por estructura")]
    public record EstructuraContenidoReject
    {
        /// <summary>
        /// Datos de Solicitud Estado de Pago del Tramo 2 (PSR2)
        /// </summary>        
        public RechazoRecepcionDTO Reject { get; set; }
    }

    /// <summary>
    /// Representa la Estructura Recibida de Entrada del Echo Test
    /// Hace referencia al Tramo 1 (ET1) 7.2.3.10.1
    /// </summary>
    [SwaggerSchema("Representa la Estructura Recibida de Entrada del Echo Test")]
    public record EstructuraContenidoET1
    {
        /// <summary>
        /// Datos de Entrada del Echo Test del Tramo 1 (ET1)
        /// </summary>
        public EchoTestDTO ET1 { get; set; }
        /// <summary>
        /// Propiedad para validación de firma
        /// </summary>
        public string codigoValidacionFirma { get; set; } = RazonRespuesta.codigo0000;
    }

    /// <summary>
    /// Representa la Estructura Respondida de Entrada del Echo Test
    /// Hace referencia al Tramo 2 (ET2) 7.2.3.10.2
    /// </summary>
    [SwaggerSchema("Representa la Estructura Respondida de Entrada del Echo Test")]
    public record EstructuraContenidoET2
    {
        /// <summary>
        /// Datos de Echo Test del Tramo 2 (ET2)
        /// </summary>
        public EchoTestRespuestaDTO ET2 { get; set; }
    }

    /// <summary>
    /// Representa la Estructura Recibida de entrada del Mensaje SNM971
    /// Notificación de Cambio de Estado del Sistema
    /// </summary>
    [SwaggerSchema("Notificación de Cambio de Estado del Sistema")]
    public record EstructuraContenidoSNM971
    {
        /// <summary>
        /// Datos de Mensaje de Notificacion 971 (SNM971)
        /// </summary>
        public Notificacion971DTO SNM971 { get; set; }
    }

    /// <summary>
    /// Representa la Estructura Recibida de entrada del Mensaje SNM972
    /// Notificación de Cambio de estado de Bloqueo de la Entidad
    /// </summary>
    [SwaggerSchema("Notificación de Cambio de estado de Bloqueo de la Entidad")]
    public record EstructuraContenidoSNM972
    {
        /// <summary>
        /// Datos de Mensaje de Notificacion 972 (SNM972)
        /// </summary>
        public Notificacion972DTO SNM972 { get; set; }
    }

    /// <summary>
    /// Representa la Estructura Recibida de entrada del Mensaje SNM981
    /// Notificación de Libre Formato
    /// </summary>
    [SwaggerSchema("Notificación de Libre Formato")]
    public record EstructuraContenidoSNM981
    {
        /// <summary>
        /// Datos de Mensaje de Notificacion 981 (SNM981)
        /// </summary>
        public Notificacion981DTO SNM981 { get; set; }
    }

    /// <summary>
    /// Representa la Estructura Recibida de entrada del Mensaje SNM982
    /// Notificación de Cambio de Estado de Logueo de la Entidad
    /// </summary>
    [SwaggerSchema("Notificación de Cambio de Estado de Logueo de la Entidad")]
    public record EstructuraContenidoSNM982
    {
        /// <summary>
        /// Datos de Mensaje de Notificacion 982 (SNM982)
        /// </summary>
        public Notificacion982DTO SNM982 { get; set; }
    }

    /// <summary>
    /// Representa la Estructura Recibida de entrada del Mensaje SNM993
    /// Notificación de Ampliación de Garantías
    /// </summary>
    [SwaggerSchema("Notificación de Ampliación de Garantías")]
    public record EstructuraContenidoSNM993
    {
        /// <summary>
        /// Datos de Mensaje de Notificacion 993 (SNM993)
        /// </summary>
        public Notificacion993DTO SNM993 { get; set; }
    }

    /// <summary>
    /// Representa la Estructura Recibida de entrada del Mensaje SNM993
    /// Notificación de Cambio en el Saldo Operativo
    /// </summary>
    [SwaggerSchema("Notificación de Cambio en el Saldo Operativo")]
    public record EstructuraContenidoSNM998
    {
        /// <summary>
        /// Datos de Mensaje de Notificacion 998 (SNM998)
        /// </summary>
        public Notificacion998DTO SNM998 { get; set; }
    }

    /// <summary>
    /// Representa la Estructura Recibida de entrada del Mensaje SNM993
    /// Notificación de Resumen de Ciclo Operativo
    /// </summary>
    [SwaggerSchema("Notificación de Resumen de Ciclo Operativo")]
    public record EstructuraContenidoSNM999
    {
        /// <summary>
        /// Datos de Mensaje de Notificacion 999 (SNM999)
        /// </summary>
        public Notificacion999DTO SNM999 { get; set; }
    }
    #endregion

    #region Estructuras de Salidas
    public  record EsctruturaAV1{
        /// <summary>
        /// Cuerpo de datos de consulta cuenta a enviar
        /// </summary>
        public  ConsultaCuentaSalidaDTO AV1 {get; set;}
    }
    
    public record EsctruturaAV4{
        /// <summary>
        /// Cuerpo de dato de consulta cuenta a recibir
        /// </summary>
        public ConsultaCuentaRecepcionSalidaDTO AV4 {get; set;}
    }
    public record EsctruturaCT1{
        /// <summary>
        /// Cuerpo de datos de orden de transferencia a enviar
        /// </summary>
        public OrdenTransferenciaSalidaDTO CT1 {get; set;}
    }
    public record EsctruturaCT4{
        /// <summary>
        /// Cuerpo de datos de orden de transfernecia a recibir
        /// </summary>
        public OrdenTransferenciaRecepcionSalidaDTO CT4 {get; set;}
    }

    public record EsctruturaET1{
        /// <summary>
        /// Cuerpo de datos de echo test a enviar
        /// </summary>
        public EchoTestDTO ET1 {get; set;}
    }

    public record EsctruturaET2{
        /// <summary>
        /// Cuerpo de datos de echo test a recibir
        /// </summary>
        public EchoTestRespuestaDTO ET2 {get; set;}
    }
    public record EstructuraSignOn1{
        /// <summary>
        /// Cuerpo de datos de signOn a enviar
        /// </summary>
        public SignOnOffDTO SignOn1 {get; set;}
    }
    public record EstructuraSignOn2{
        /// <summary>
        /// Cuerpo de datos de signOn a recibir
        /// </summary>
        public SignOnOffDTO SignOn2 {get; set;}
    }

    public record EstructuraSignOff1{
        /// <summary>
        /// Cuerpo de datos de signOff a enviar
        /// </summary>
        public SignOnOffDTO SignOff1 {get; set;}
    }
    public record EstructuraSignOff2{
        /// <summary>
        /// Cuerpo de datos de signOff a recibir
        /// </summary>
        public SignOnOffDTO SignOff2 {get; set;}
    }
        
    #endregion SALIDAS

}