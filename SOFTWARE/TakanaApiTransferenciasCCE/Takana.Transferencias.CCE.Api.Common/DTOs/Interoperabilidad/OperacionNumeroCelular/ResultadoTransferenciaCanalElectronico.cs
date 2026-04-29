using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad
{
    /// <summary>
    /// Clase de datos para resultado de la transferencia
    /// </summary>
    public record ResultadoTransferenciaCanalElectronico
    {
        /// <summary>
        /// Numero de operacion de la transferencia
        /// </summary>
        [SwaggerSchema("Numero de operacion de la transferencia")]
        public int NumeroOperacion { get; set; }
        /// <summary>
        /// Fecha de operacion
        /// </summary>
        [SwaggerSchema("Numero de operacion de la transferencia")]
        public DateTime FechaOperacion { get; set; }
    }
}
