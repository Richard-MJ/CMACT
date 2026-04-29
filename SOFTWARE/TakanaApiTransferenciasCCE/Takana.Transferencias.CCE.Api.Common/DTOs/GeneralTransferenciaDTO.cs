using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common
{
    /// <summary>
    /// Clase Padre que representa los datos generales del proceso de Consulta Cuenta.
    /// Son datos redundantes de todos los tramos.
    /// </summary>
    public record GeneralTransferenciaDTO
    {
        /// <summary>
        /// Código de la Entidad Originante.
        /// </summary>
        [Required]
        [SwaggerSchema("Código de la Entidad Originante.")]
        public string debtorParticipantCode { get; set; }
        /// <summary>
        /// Código de la Entidad Receptora
        /// </summary>
        [Required]
        [SwaggerSchema("Código de la Entidad Receptora")]
        public string creditorParticipantCode { get; set; }
        /// <summary>
        /// Código de identificación de terminal.
        /// </summary>        
        [Required]
        [SwaggerSchema("Código de identificación de terminal.")]
        public string terminalId { get; set; }
        /// <summary>
        /// Número de referencia
        /// </summary>        
        [Required]
        [SwaggerSchema("Número de referencia")]
        public string retrievalReferenteNumber { get; set; }
        /// <summary>
        /// Número de seguimiento de la transacción.
        /// </summary>        
        [Required]
        [SwaggerSchema("Número de seguimiento de la transacción.")]
        public string trace { get; set; }
        /// <summary>
        /// Código de Moneda de la Transacción.
        /// </summary>
        [Required]
        [SwaggerSchema("Código de Moneda de la Transacción.")]
        public string currency { get; set; }
        /// <summary>
        /// Código de Transacción Extendida. Se utiliza para ampliar el código de operación de la transacción.
        /// </summary>                
        [Required]
        [SwaggerSchema("Código de Transacción Extendida. Se utiliza para ampliar el código de operación de la transacción.")]
        public string transactionType { get; set; }
        /// <summary>
        /// Código de Cuenta Interbancario del Cliente Receptor.
        /// </summary>        
        [SwaggerSchema("Código de Cuenta Interbancario del Cliente Receptor.")]
        public string? creditorCCI { get; set; }
        /// <summary>
        /// Código de Tarjeta de Crédito del Cliente Receptor.
        /// </summary>        
        [SwaggerSchema("Código de Tarjeta de Crédito del Cliente Receptor.")]
        public string? creditorCreditCard { get; set; }
    }
}


