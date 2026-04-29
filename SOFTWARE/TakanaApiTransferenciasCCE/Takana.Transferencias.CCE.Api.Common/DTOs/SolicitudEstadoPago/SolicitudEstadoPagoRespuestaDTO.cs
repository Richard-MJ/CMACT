using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using Takana.Transferencias.CCE.Api.Common.Utilidades;

namespace Takana.Transferencias.CCE.Api.Common.SolicitudEstadoPago
{
    public record SolicitudEstadoPagoRespuestaDTO : GeneralSolicitudEstadoPagoDTO
    {
        /// <summary>
        /// Codigo de participante originante
        /// </summary>
        [Required]
        [SwaggerSchema("Codigo de participante originante")]
        public string debtorParticipantCode { get; set; }
        /// <summary>
        /// Fecha de respuesta 
        /// </summary>
        [Required]
        [SwaggerSchema("Fecha de respuesta ")]
        public string responseDate { get; set; }
        /// <summary>
        /// Hora de respuesta
        /// </summary>
        [Required]
        [SwaggerSchema("Hora de respuesta")]
        public string responseTime { get; set; }
        /// <summary>
        /// Identificador de terminal
        /// </summary>
        [Required]
        [SwaggerSchema("Identificador de terminal")]
        public string terminalId { get; set; }
        /// <summary>
        /// Numero de referencia
        /// </summary>
        [Required]
        [SwaggerSchema("Numero de referencia")]
        public string retrievalReferenteNumber { get; set; }
        /// <summary>
        /// Codigo trace
        /// </summary>
        [Required]
        [SwaggerSchema("Codigo trace")]
        public string trace { get; set; }
        /// <summary>
        /// Monto de transferencia
        /// </summary>
        [Required]
        [JsonPropertyName("amount")]
        [JsonConverter(typeof(ConvertirdorJsonAtipo<decimal>))]
        [SwaggerSchema("Monto de transferencia")]
        public decimal amount { get; set; }
        /// <summary>
        /// transaccion de referencia
        /// </summary>
        [SwaggerSchema("transaccion de referencia")]
        public string? transactionReference { get; set; }
        /// <summary>
        /// Codigo de respuesta
        /// </summary>
        [SwaggerSchema("Codigo de respuesta")]
        public string responseCode { get; set; }
        /// <summary>
        /// razon de respuesta
        /// </summary>
        [SwaggerSchema("razon de respuesta")]
        public string? reasonCode { get; set; }
        /// <summary>
        /// Monto de comision
        /// </summary>
        [JsonPropertyName("feeAmount")]
        [JsonConverter(typeof(ConvertirdorJsonAtipo<decimal>))]
        [SwaggerSchema("Monto de comision")]
        public decimal feeAmount { get; set; }
        /// <summary>
        /// Fecha de liquidacion
        /// </summary>
        [SwaggerSchema("Fecha de liquidacion")]
        public string? settlementDate { get; set; }
        /// <summary>
        /// tipo de transaccion
        /// </summary>
        [SwaggerSchema("tipo de transaccion")]
        public string? transactionType { get; set; }
        /// <summary>
        /// Codigo de cuenta interbancaria de originante
        /// </summary>
        [SwaggerSchema("Codigo de cuenta interbancaria de originante")]
        public string? debtorCCI { get; set; }
        /// <summary>
        /// Codigo de cuenta interbancaria de receptor
        /// </summary>
        [SwaggerSchema("Codigo de cuenta interbancaria de receptor")]
        public string? creditorCCI { get; set; }
        /// <summary>
        /// Numero de cuenta interbancaria
        /// </summary>
        [SwaggerSchema("Numero de cuenta interbancaria")]
        public string? creditorCreditCard { get; set; }
        /// <summary>
        /// Codigo de mismo titular 
        /// </summary>
        [SwaggerSchema("Codigo de mismo titular ")]
        public string? sameCustomerFlag { get; set; }
        /// <summary>
        /// Monto de liquidacion
        /// </summary>
        [JsonPropertyName("interbankSettlementAmount")]
        [JsonConverter(typeof(ConvertirdorJsonAtipo<decimal>))]
        [SwaggerSchema("Monto de liquidacion")]
        public decimal interbankSettlementAmount { get; set; }
    }
}




