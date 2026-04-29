using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.Notificaciones
{
    public record Notificacion971DTO : GeneralNotificacionDTO
    {
        /// <summary>
        /// Nuevo de servicios de estados
        /// </summary>
        [Required]
        [SwaggerSchema("Nuevo de servicios de estados")]
        public string newServiceStatus {get; set; }
    }
}
