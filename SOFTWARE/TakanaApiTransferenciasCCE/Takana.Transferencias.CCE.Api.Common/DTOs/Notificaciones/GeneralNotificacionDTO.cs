using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.Notificaciones
{
    public record GeneralNotificacionDTO
    {
        /// <summary>
        /// Codigo de evento
        /// </summary>
        [Required]
        [SwaggerSchema("Codigo de evento")]
        public string eventCode { get; set; }
        /// <summary>
        /// Fecha de evento
        /// </summary>
        [Required]
        [SwaggerSchema("Fecha de evento")]
        public string eventDate { get; set; }
        /// <summary>
        /// Hora de evento
        /// </summary>
        [Required]
        [SwaggerSchema("Hora de evento")]
        public string eventTime { get; set; }
        /// <summary>
        /// Identificador del mensaje
        /// </summary>
        [Required]
        [SwaggerSchema("Identificador del mensaje")]
        public string messageIdentification { get; set; }
        /// <summary>
        /// Tipo o Clase de mensaje
        /// </summary>
        [Required]
        [SwaggerSchema("Tipo o Clase de mensaje")]
        public string messageClass { get; set; }
    }
}
