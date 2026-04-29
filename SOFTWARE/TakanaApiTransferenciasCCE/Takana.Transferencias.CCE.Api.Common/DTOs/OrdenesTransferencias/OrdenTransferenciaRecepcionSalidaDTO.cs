using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Takana.Transferencias.CCE.Api.Common.Utilidades;

namespace Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;

/// <summary>
/// Proceso de orden de transferencia
/// Clase que hace referencia a CT4 (7.2.3.2.4.)
/// </summary>
public record OrdenTransferenciaRecepcionSalidaDTO : GeneralOrdenTransferenciaDTO
{
        /// <summary>
        /// Fecha Local de la transacción.
        /// </summary>
        [Required]
        public string responseDate { get; set;}

        /// <summary>
        /// Hora Local de la transacción. 
        /// </summary>
        [Required]
        public string responseTime { get; set;}

        /// <summary>
        /// Código de Respuesta.
        /// 00: Aceptada
        /// 05: Rechazada
        /// </summary>
        [Required]
        public string responseCode { get; set; }

        /// <summary>
        /// La razón del código de respuesta o Respuesta Aplicativa.
        /// </summary>
        public string? reasonCode { get; set; }

        /// <summary>
        /// Hace referencia a la fecha de liquidación.
        /// </summary>
        [Required]
        public string settlementDate { get; set; }

        /// <summary>
        /// Autogenerado de identificación única de transacción. Proporcionado por IPS.
        /// </summary>
        [Required]
        public string instructionId { get; set; }

        /// <summary>
        /// Fecha Local de la transacción.
        /// </summary>
        [Required]
        public string creationDate { get; set; }

        /// <summary>
        /// Hora Local de la transacción. 
        /// </summary>
        [Required]
        public string creationTime { get; set; }

        /// <summary>
        /// Código del canal que origina la transacción. Ver Anexo 3.
        /// </summary>
        [Required]
        public string channel { get; set; }

        /// <summary>
        /// Hace referencia al monto de liquidación interbancaria – entiéndase, monto +/- comisión (los 2 últimos dígitos indican la parte decimal).
        /// </summary>
        [JsonPropertyName("interbankSettlementAmount")]                      
        [JsonConverter(typeof(ConvertirdorJsonAtipo<decimal>))] 
        public decimal interbankSettlementAmount { get; set; }

}
