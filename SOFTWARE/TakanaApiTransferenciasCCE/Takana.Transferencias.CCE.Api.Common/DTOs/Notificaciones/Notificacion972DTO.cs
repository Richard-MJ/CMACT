using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.Notificaciones
{
    public record Notificacion972DTO : GeneralNotificacionDTO
    {
        /// <summary>
        /// Codigo de moneda ISO
        /// </summary>
        [Required]
        [SwaggerSchema("Codigo de moneda ISO")]
        public string currency { get; set; }
        /// <summary>
        /// Miembro de identificacion
        /// </summary>
        [Required]
        [SwaggerSchema("Miembro de identificacion")]
        public string memberIdentification { get; set; }
        /// <summary>
        /// Nombre
        /// </summary>
        [Required]
        [SwaggerSchema("Nombre")]
        public string name { get; set; }
        /// <summary>
        /// Estados
        /// </summary>
        [Required]
        [SwaggerSchema("Estados")]
        public string status { get; set; }
    }
}
