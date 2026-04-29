using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.Notificaciones
{
    public record Notificacion982DTO : GeneralNotificacionDTO
    {
        /// <summary>
        /// Identificaion de participante
        /// </summary>
        [Required]
        [SwaggerSchema("Identificaion de participante")]
        public string participantIdentification { get; set; }
        /// <summary>
        /// Nombre del participante
        /// </summary>
        [Required]
        [SwaggerSchema("Nombre del participante")]
        public string participantName { get; set; }
        /// <summary>
        /// Estado del participante
        /// </summary>
        [Required]
        [SwaggerSchema("Estado del participante")]
        public string participantStatus { get; set; }
    }
}
