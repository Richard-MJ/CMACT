using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias
{
    /// <summary>
    /// Proceso de orden de transferencia
    /// Clase que hace referencia a CT5 (7.2.3.2.5.)
    /// </summary>
    public record OrdenTransferenciaConfirmacionEntradaDTO : GeneralOrdenTransferenciaDTO
    {
        /// <summary>
        /// Fecha de Respuesta de la transacción
        /// </summary>
        [Required]
        [SwaggerSchema("Fecha de Respuesta de la transacción")]
        public string responseDate { get; set; }

        /// <summary>
        /// Hora de Respuesta de la transacción.
        /// </summary>
        [Required]
        [SwaggerSchema("Hora de Respuesta de la transacción.")]
        public string responseTime { get; set; }

        /// <summary>
        /// Código de Respuesta.   00: Aceptada
        /// </summary>
        [Required]
        [SwaggerSchema("Código de Respuesta.   00: Aceptada")]
        public string responseCode { get; set; }

        /// <summary>
        /// Hace referencia a la fecha de liquidación. 
        /// </summary>
        [Required]
        [SwaggerSchema("Hace referencia a la fecha de liquidación. ")]
        public string settlementDate { get; set; }

        /// <summary>
        /// Autogenerado de identificación única de transacción. Proporcionado por IPS.
        /// </summary>
        [Required]
        [SwaggerSchema("Autogenerado de identificación única de transacción. Proporcionado por IPS.")]
        public string instructionId { get; set; }

        /// <summary>
        /// Fecha Local de la transacción. 
        /// </summary>
        [Required]
        [SwaggerSchema("Fecha Local de la transacción. ")]
        public string creationDate { get; set; }

        /// <summary>
        /// Hora Local de la transacción.
        /// </summary>
        [Required]
        [SwaggerSchema("Hora Local de la transacción.")]
        public string creationTime { get; set; }

        /// <summary>
        /// Código del canal que origina la transacción. Ver Anexo 3.
        /// </summary>
        [Required]
        [SwaggerSchema("Código del canal que origina la transacción. Ver Anexo 3.")]
        public string channel { get; set; }

        /// <summary>
        /// Hace referencia al monto de liquidación interbancaria – entiéndase, monto +/- comisión (los 2 últimos dígitos indican la parte decimal).
        /// </summary>
        [Required]
        [SwaggerSchema("Hace referencia al monto de liquidación interbancaria – entiéndase, monto +/- comisión (los 2 últimos dígitos indican la parte decimal).")]
        public decimal interbankSettlementAmount { get; set; }
    }
}