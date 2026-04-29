using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.Notificaciones
{
    public record Notificacion998DTO : GeneralNotificacionDTO
    {
        /// <summary>
        /// Miembros de identificaion
        /// </summary>
        [Required]
        [SwaggerSchema("Miembros de identificaion")]
        public string memberIdentification { get; set; }
        /// <summary>
        /// Nombre del banco
        /// </summary>
        [Required]
        [SwaggerSchema("Nombre del banco")]
        public string bankName { get; set; }
        /// <summary>
        /// Codigo de moneda ISO
        /// </summary>
        [Required]
        [SwaggerSchema("Codigo de moneda ISO")]
        public string currency { get; set; }
        /// <summary>
        /// Estado de cambio de balance
        /// </summary>
        [Required]
        [SwaggerSchema("Estado de cambio de balance")]
        public string prefundedBalanceChangeStatus { get; set; }
        /// <summary>
        /// Nuevo balance
        /// </summary>
        [Required]
        [SwaggerSchema("Nuevo balance")]
        public decimal newPrefundedBalance { get; set; }
        /// <summary>
        /// Anterior balance
        /// </summary>
        [Required]
        [SwaggerSchema("Anterior balance")]
        public decimal previousPrefundedBalance { get; set; }
    }
}
