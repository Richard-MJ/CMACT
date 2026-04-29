using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.Cancelaciones
{
    public record CancelacionRespuestaDTO : GeneralCancelacionDTO
    {
        /// <summary>
        /// Fecha de creacion
        /// </summary>
        [Required]
        [SwaggerSchema("Fecha de creacion")]
        public string responseDate { get; set; }

        /// <summary>
        /// Hora de creacion
        /// </summary>
        [Required]
        [SwaggerSchema("Hora de creacion")]
        public string responseTime { get; set; }

        /// <summary>
        /// Codigo de respuesta
        /// </summary>
        [Required]
        [SwaggerSchema("Codigo de respuesta")]
        public string responseCode { get; set; }

        /// <summary>
        /// razon de respuesta
        /// </summary>
        [SwaggerSchema("razon de respuesta")]
        public string? reasonCode { get; set; }
    }

}
