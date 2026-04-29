namespace Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad
{
    /// <summary>
    /// Clase de datos de la respuesta de la consulta de cuenta por QR
    /// </summary>
    public record RespuestaConsultaDatosQRDTO
    {
        /// <summary>
        /// Código de la entidad receptora asociada al código QR.
        /// </summary>
        public string CodigoEntidadReceptor { get; set; }
        /// <summary>
        /// Identificador único de la cuenta asociada al código QR.
        /// </summary>
        public string IdentificadorCuenta { get; set; }
        /// <summary>
        /// Identificador único del código QR.
        /// </summary>
        public string IdentificadorQR { get; set; }
        /// <summary>
        /// Código de la moneda utilizada en la transacción asociada al código QR.
        /// </summary>
        public string CodigoMoneda { get; set; }
        /// <summary>
        /// Fecha de registro del código QR.
        /// </summary>
        public string FechaRegistro { get; set; }
        /// <summary>
        /// Fecha de vencimiento del código QR.
        /// </summary>
        public string FechaVencimiento { get; set; }
        /// <summary>
        /// Tipo de código QR utilizado.
        /// </summary>
        public string TipoQR{ get; set; }
    }
}
