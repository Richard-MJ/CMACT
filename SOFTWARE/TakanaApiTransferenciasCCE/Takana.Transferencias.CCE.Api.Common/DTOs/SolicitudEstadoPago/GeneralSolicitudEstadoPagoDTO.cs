using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.SolicitudEstadoPago
{
    public record GeneralSolicitudEstadoPagoDTO
    {
        /// <summary>
        /// Codigo de participante del receptor
        /// </summary>
        [Required]
        [SwaggerSchema("Codigo de participante del receptor")]
        public string creditorParticipantCode { get; set; }
        /// <summary>
        /// Codigo de Moneda ISO
        /// </summary>
        [Required]
        [SwaggerSchema("Codigo de Moneda ISO")]
        public string currency { get; set; }
        /// <summary>
        /// Identificador de instrucción
        /// </summary>
        [Required]
        [SwaggerSchema("Identificador de instrucción")]
        public string instructionId { get; set; }
    }
}


