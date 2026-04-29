using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.EchoTest
{
    public record EchoTestRespuestaDTO : GeneralEchoTestDTO
    {
        /// <summary>
        /// Fecha de respuesta
        /// </summary>
        [Required]
        [SwaggerSchema("Fecha de respuesta")]
        public string responseDate { get; set; }
        /// <summary>
        /// Hora de respuesta
        /// </summary>
        [Required]
        [SwaggerSchema("Hora de respuesta")]
        public string responseTime { get; set; }
        /// <summary>
        /// Codigo de respuesta
        /// </summary>
        [Required]
        [SwaggerSchema("Codigo de respuesta")]
        public string status { get; set; }
        /// <summary>
        /// Descripcion de respuesta
        /// </summary>
        [SwaggerSchema("Descripcion de respuesta")]
        public string? reasonCode { get; set; }
    }

}
