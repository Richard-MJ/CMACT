using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.SolicitudEstadoPago
{
    public record SolicitudEstadoPagoSalidaDTO : GeneralSolicitudEstadoPagoDTO
    {
        /// <summary>
        /// Fecha de creación
        /// </summary>
        [Required]
        [SwaggerSchema("Fecha de creación")]
        public string creationDate { get; set; }
        /// <summary>
        /// Hora de creacion
        /// </summary>
        [Required]
        [SwaggerSchema("Hora de creacion")]
        public string creationTime { get; set; }
        /// <summary>
        /// Fecha original de creacion
        /// </summary>
        [Required]
        [SwaggerSchema("Fecha original de creacion")]
        public string originalCreationDate { get; set; }
        /// <summary>
        /// Hora original de creacion
        /// </summary>
        [Required]
        [SwaggerSchema("Hora original de creacion")]
        public string originalCreationTime { get; set; }
    }
}


