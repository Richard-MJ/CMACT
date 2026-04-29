namespace Takana.Transferencias.CCE.Api.Common.DTOs.Email
{
    public class CorreoGeneralDTO
    {
        #region Constantes
        /// <summary>
        /// Descripcion del archivo enviado al SFTP
        /// </summary>
        public const string DescripcionArchivoEnviadoSFTP = "ARCHIVO SUBIDO AL SERVICIO SFTP DE LA CCE";
        /// <summary>
        /// Descripcion del archivo respuesta al SFTP
        /// </summary>
        public const string DescripcionArchivorRespuestaSFTP = "ARCHIVO DE RESPUESTA DEL SERVICIO SFTP DE LA CCE";
        /// <summary>
        /// Descripcion del Reporte de Archivo Tema del correo
        /// </summary>
        public const string DescripcionArchivoTemaMensaje = "ENVIÓ DE REPORTES CIRCULAR 0009-2024-BCRP";
        /// <summary>
        /// Descripcion del Directorio tema del correo
        /// </summary>
        public const string DescripcionDirectorioTemaMensaje = "ENVIÓ DE ARCHIVO MASIVO DEL DIRECTORIO DE LA CCE";
        /// <summary>
        /// Descripcion del tipo de mensaje
        /// </summary>
        public const string DescripcionTipoMensaje = "TipoMensaje";
        /// <summary>
        /// Descripcion de adjunto
        /// </summary>
        public const string DescripcionAdjunto = "Adjunto";
        /// <summary>
        /// Llave que generara el correo
        /// </summary>
        public const string LlaveGenerarCorreo = "generar_email";   
        /// <summary>
        /// Codigo de correo para reporte de interoperabilidad
        /// </summary>
        public const string CodigoReporteInteroperabilidad = "reporte_interoperabilidad_cce";
        /// <summary>
        /// Codigo de correo para reporte de interoperabilidad
        /// </summary>
        public const string CodigoNotificacionCCE = "mensaje_notificacion_cce";
        /// <summary>
        /// Codigo de correo para afiliacion de interoperabilidad
        /// </summary>
        public const string CodigoAfiliacionInteroperabilidad = "afiliacion_Interoperabilidad";
        /// <summary>
        /// Codigo de correo para transferencias de inmediatas
        /// </summary>
        public const string CodigoTransferenciaInmediata = "transferencia_inmediata_cce";
        /// <summary>
        /// Codigo de correo para Pago de tarjeta de credito
        /// </summary>
        public const string CodigoPagoTarjetaCredito = "pago_tarjeta_credito_inmediata_cce";
        /// <summary>
        /// Codigo de correo reporte un error de firma digital
        /// </summary>
        public const string CodigoErrorFirmaDigital = "error_firma_digital";
        #endregion

        #region Propiedades
        /// <summary>
        /// Descripción de operación
        /// </summary>
        public string DescripcionOperacion { get; set; }
        /// <summary>
        /// Correo electrónico del destinatario
        /// </summary>
        public string CorreoElectronicoDestinatario { get; set; }
        /// <summary>
        /// Correo electrónico del remitente
        /// </summary>
        public string CorreoElectronicoRemitente { get; set; }
        /// <summary>
        /// Fecha de la operación
        /// </summary>
        public DateTime FechaOperacion { get; set; }
        /// <summary>
        /// Tema del mensaje
        /// </summary>
        public string TemaMensaje { get; set; }
        /// <summary>
        /// NOmbre del Servicio
        /// </summary>
        public string Servicio { get; set; }
        /// <summary>
        /// Celular del cliente
        /// </summary>
        public string Celular { get; set; }
        /// <summary>
        /// Cuenta del cliente
        /// </summary>
        public string Cuenta { get; set; }
        /// <summary>
        /// Nombre del cliente
        /// </summary>
        public string NombreCliente { get; set; }
        /// <summary>
        /// Direccion IP
        /// </summary>
        public string DireccionIP { get; set; }
        /// <summary>
        /// Modelo del dispositivo
        /// </summary>
        public string Modelo { get; set; }
        /// <summary>
        /// Sistema Operativo
        /// </summary>
        public string SistemaOperativo { get; set; }
        /// <summary>
        /// Información del Navegador
        /// </summary>
        public string Navegador { get; set; }
        /// <summary>
        /// Número de movimiento
        /// </summary>
        public long NumeroOperacion { get; set; }
        /// <summary>
        /// Archivos adjuntos
        /// </summary>
        public IList<ArchivoAdjuntoDTO> ArchivosAdjuntos { get; set; }
        #endregion
    }
}
