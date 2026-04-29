using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.Notificaciones
{
    public record Notificacion999DTO : GeneralNotificacionDTO
    {
        /// <summary>
        /// Fecha anterior Ventana Asentamiento
        /// </summary>
        [Required]
        [SwaggerSchema("Fecha anterior Ventana Asentamiento")]
        public string previousSettlementWindowDate { get; set; }
        /// <summary>
        /// Anterior Estado del ciclo de liquidación
        /// </summary>
        [Required]
        [SwaggerSchema("Anterior Estado del ciclo de liquidación")]
        public string previousSettlementCycleStatus { get; set; }
        /// <summary>
        /// Reconciliacion
        /// </summary>
        [Required]
        [SwaggerSchema("Reconciliacion")]
        public string reconciliationCheckpointCutOffDateAndTime { get; set; }
        /// <summary>
        /// Fecha nuevo Asentamiento 
        /// </summary>
        [Required]
        [SwaggerSchema("Fecha nuevo Asentamiento ")]
        public string newSettlementWindowDate { get; set; }
        /// <summary>
        /// Nuevo estado de la ventana de liquidación
        /// </summary>
        [Required]
        [SwaggerSchema("Nuevo estado de la ventana de liquidación")]
        public string newSettlementWindowStatus { get; set; }
        /// <summary>
        /// Codigo de moneda ISO
        /// </summary>
        [Required]
        [SwaggerSchema("Codigo de moneda ISO")]
        public string currency { get; set; }
        /// <summary>
        /// Miembros de identificacion
        /// </summary>
        [Required]
        [SwaggerSchema("Miembros de identificacion")]
        public string memberIdentification { get; set; }
        /// <summary>
        /// Nombre de banco
        /// </summary>
        [Required]
        [SwaggerSchema("Nombre de banco")]
        public string bankName { get; set; }
        /// <summary>
        /// Balance anterior
        /// </summary>
        [Required]
        [SwaggerSchema("Balance anterior")]
        public decimal previousOpeningPrefundedBalance { get; set; }
        /// <summary>
        /// Balance nuevo
        /// </summary>
        [Required]
        [SwaggerSchema("Balance nuevo")]
        public decimal newOpeningPrefundedBalance { get; set; }
        /// <summary>
        /// Numero credito recibidas aceptadas
        /// </summary>
        [Required]
        [SwaggerSchema("Numero credito recibidas aceptadas")]
        public string numberOfCreditTransferReceivedAndAccepted { get; set; }
        /// <summary>
        /// Total creditos recibidos aceptados
        /// </summary>
        [Required]
        [SwaggerSchema("Total creditos recibidos aceptados")]
        public decimal valueOfCreditTransferReceivedAndAccepted { get; set; }
        /// <summary>
        /// Numero de creditos recibidos rechazados
        /// </summary>
        [Required]
        [SwaggerSchema("Numero de creditos recibidos rechazados")]
        public string numberOfCreditTransferReceivedAndRejected { get; set; }
        /// <summary>
        /// Total de creditos recibidos rechazados
        /// </summary>
        [Required]
        [SwaggerSchema("Total de creditos recibidos rechazados")]
        public decimal valueOfCreditTransferReceivedAndRejected { get; set; }
        /// <summary>
        /// Numero de creditos enviados aceptados
        /// </summary>
        [Required]
        [SwaggerSchema("Numero de creditos enviados aceptados")]
        public string numberOfCreditTransferSentAndAccepted { get; set; }
        /// <summary>
        /// Total de creditos enviados aceptados
        /// </summary>
        [Required]
        [SwaggerSchema("Total de creditos enviados aceptados")]
        public decimal valueOfCreditTransferSentAndAccepted { get; set; }
        /// <summary>
        /// Numero de creditos enviados rechazados
        /// </summary>
        [Required]
        [SwaggerSchema("Numero de creditos enviados rechazados")]
        public string numberCreditTransferSentAndRejected { get; set; }
        /// <summary>
        /// Total de creditos enviados rechazados
        /// </summary>
        [Required]
        [SwaggerSchema("Total de creditos enviados rechazados")]
        public decimal valueOfCreditTransferSentAndRejected { get; set; }
        /// <summary>
        /// posición neta
        /// </summary>
        [Required]
        [SwaggerSchema("posición neta")]
        public decimal netPosition { get; set; }
        /// <summary>
        /// recuento de financiación suplementaria
        /// </summary>
        [Required]
        [SwaggerSchema("recuento de financiación suplementaria")]
        public string countOfSupplementalFunding { get; set; }
        /// <summary>
        /// valor bruto de financiación suplementaria
        /// </summary>
        [Required]
        [SwaggerSchema("valor bruto de financiación suplementaria")]
        public decimal grossValueOfSupplementalFunding { get; set; }
        /// <summary>
        /// recuento de reducciones
        /// </summary>
        [Required]
        [SwaggerSchema("recuento de reducciones")]
        public string countOfDrawdowns { get; set; }
        /// <summary>
        /// valor bruto de reducciones
        /// </summary>
        [Required]
        [SwaggerSchema("valor bruto de reducciones")]
        public decimal grossValueOfDrawdowns { get; set; }
    }
}
