namespace Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad
{
    /// <summary>
    /// Clase de datos para la respuesta de Generacion de QR
    /// </summary>
    public record RespuestaGenerarQR
    {
        /// <summary>
        /// Indentificador QR
        /// </summary>
        public string IdentificadorQR {  get; set; }
        /// <summary>
        /// Cadena hash del QR
        /// </summary>
        public string CadenaQR { get; set; }    
    }
}
