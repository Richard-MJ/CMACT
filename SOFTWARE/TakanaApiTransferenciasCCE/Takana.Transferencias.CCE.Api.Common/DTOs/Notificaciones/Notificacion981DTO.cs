using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.Notificaciones
{
    public record Notificacion981DTO : GeneralNotificacionDTO
    {
        /// <summary>
        /// Descripcion de eventos
        /// </summary>
        [Required]
        [SwaggerSchema("Descripcion de eventos")]
        public string eventDescription { get; set; }
    }
}
