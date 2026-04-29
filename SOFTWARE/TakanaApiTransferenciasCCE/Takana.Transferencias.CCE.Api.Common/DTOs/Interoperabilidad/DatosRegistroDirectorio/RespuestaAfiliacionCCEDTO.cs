using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad.DatosRegistroDirectorio
{
    /// <summary>
    /// Clase de respuesta de la afiliacion
    /// </summary>
    public record RespuestaAfiliacionCCEDTO
    {
        /// <summary>
        /// Fecah de operacion de la afiliacion
        /// </summary>
        [SwaggerSchema("Fecah de operacion de la afiliacion")]
        public DateTime FechaOperacion {  get; set; }
        /// <summary>
        /// Cadeba has de QR
        /// </summary>
        [SwaggerSchema("Cadeba has de QR")]
        public string CadenaHash { get; set; }
    }
}
