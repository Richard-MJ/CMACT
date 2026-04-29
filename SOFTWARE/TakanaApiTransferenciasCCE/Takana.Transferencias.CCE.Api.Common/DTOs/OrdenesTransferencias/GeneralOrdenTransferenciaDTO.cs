using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using Takana.Transferencias.CCE.Api.Common.Utilidades;

namespace Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias
{
    /// <summary>
    /// Proceso de Orden de transferencia  (7.2.2.1)/// 
    /// Clase que representa a los datos que se repiten en CT1,CT2,CT3,CT4 Y CT5
    /// </summary>
    public record GeneralOrdenTransferenciaDTO : GeneralTransferenciaDTO
    {
        /// <summary>
        /// Hace referencia al importe de la operación (los 2 últimos dígitos indican la parte decimal).
        /// </summary>
        [Required]
        [JsonPropertyName("amount")]
        [JsonConverter(typeof(ConvertirdorJsonAtipo<long>))]
        [SwaggerSchema("Hace referencia al importe de la operación (los 2 últimos dígitos indican la parte decimal).")]
        public long amount { get; set; }

        /// <summary>
        /// Código de Moneda de la Transacción. 
        /// </summary>
        [SwaggerSchema("Código de Moneda de la Transacción. ")]
        public string? transactionReference { get; set; }

        /// <summary>
        /// Hace referencia al Importe de Comisión(los 2 últimos dígitos indican la parte decimal).
        /// </summary>
        [Required]
        [JsonPropertyName("feeAmount")]
        [JsonConverter(typeof(ConvertirdorJsonAtipo<long>))]
        [SwaggerSchema("Hace referencia al Importe de Comisión(los 2 últimos dígitos indican la parte decimal).")]
        public long feeAmount { get; set; }

        /// <summary>
        /// Código de Cuenta Interbancario del Cliente Originante.
        /// </summary>
        [Required]
        [SwaggerSchema("Código de Cuenta Interbancario del Cliente Originante.")]
        public string debtorCCI { get; set; }

        /// <summary>
        /// Indicador de Mismo Cliente, usado para determinar afectación de ITF.
        /// </summary>
        [SwaggerSchema("Indicador de Mismo Cliente, usado para determinar afectación de ITF.")]
        public string? sameCustomerFlag { get; set; }
    }
}