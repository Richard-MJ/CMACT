using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.Rechazos
{
    public record RechazoRecepcionDTO
    {
        /// <summary>
        /// Mensaje original
        /// </summary>
        [Required]
        [SwaggerSchema("Mensaje original")]
        public string originalMessage { set; get; }
        /// <summary>
        /// Fecha de respuesta
        /// </summary>
        [Required]
        [SwaggerSchema("Fecha de respuesta")]
        public string responseDate { set; get; }
        /// <summary>
        /// Hora de respuesta
        /// </summary>
        [Required]
        [SwaggerSchema("Hora de respuesta")]
        public string responseTime { set; get; }
        /// <summary>
        /// Codigo de estado
        /// </summary>
        [Required]
        [SwaggerSchema("Codigo de estado")]
        public string status { set; get; }
        /// <summary>
        /// Codigo de razon
        /// </summary>
        [Required]
        [SwaggerSchema("Codigo de razon")]
        public string reasonCode { set; get; }
        /// <summary>
        /// Identificador de instruccion
        /// </summary>
        [SwaggerSchema("Identificador de instruccion")]
        public string? instructionId { set; get; }
        /// <summary>
        /// Trace
        /// </summary>
        [SwaggerSchema("Trace")]
        public string? trace { set; get; }
        /// <summary>
        /// Referencia de transaccion
        /// </summary>
        [SwaggerSchema("Referencia de transaccion")]
        public string? transactionReference { set; get; }
        /// <summary>
        /// Fecha de creacion
        /// </summary>
        [SwaggerSchema("Fecha de creacion")]
        public string? creationDate { set; get; }
        /// <summary>
        /// Hora de creacion
        /// </summary>
        [SwaggerSchema("Hora de creacion")]
        public string? creationTime { set; get; }
        /// <summary>
        /// Codigo de participante origen
        /// </summary>
        [SwaggerSchema("Codigo de participante origen")]
        public string? debtorParticipantCode { set; get; }
        /// <summary>
        /// Mensaje de definicion original
        /// </summary>
        [SwaggerSchema("Mensaje de definicion original")]
        public string? originalMessageDefinitionIdentifier { set; get; }
        /// <summary>
        /// Identificador de terminal
        /// </summary>
        [SwaggerSchema("Identificador de terminal")]
        public string? terminalId { set; get; }
    }
}
