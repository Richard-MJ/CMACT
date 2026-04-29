using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.Notificaciones
{
    public record Notificacion993DTO : GeneralNotificacionDTO
    {
        /// <summary>
        /// Nombre del banco
        /// </summary>
        [Required]
        [SwaggerSchema("Nombre del banco")]
        public string bankName { get; set; }
        /// <summary>
        /// Codigo de moneda
        /// </summary>
        [Required]
        [SwaggerSchema("Codigo de moneda")]
        public string currency { get; set; }
        /// <summary>
        /// Estado de balance
        /// </summary>
        [Required]
        [SwaggerSchema("Estado de balance")]
        public string availablePrefundBalanceStatus { get; set; }
        /// <summary>
        /// Performance del balance
        /// </summary>
        [Required]
        [SwaggerSchema("Performance del balance")]
        public decimal openingPrefundedBalance { get; set; }
        /// <summary>
        /// valor bajo de marca de agua
        /// </summary>
        [Required]
        [SwaggerSchema("valor bajo de marca de agua")]
        public decimal lowWatermarkValue { get; set; }
        /// <summary>
        /// valor alto de marca de agua
        /// </summary>
        [Required]
        [SwaggerSchema("valor alto de marca de agua")]
        public decimal highWatermarkValue { get; set; }
        /// <summary>
        /// Prefinanciado Saldo
        /// </summary>
        [Required]
        [SwaggerSchema("Prefinanciado Saldo")]
        public decimal availablePrefundedBalance { get; set; }
    }
}
