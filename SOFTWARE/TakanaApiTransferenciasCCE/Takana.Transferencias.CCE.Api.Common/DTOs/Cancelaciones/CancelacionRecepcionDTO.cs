using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.Cancelaciones
{
    public record CancelacionRecepcionDTO : GeneralCancelacionDTO
    {
        /// <summary>
        /// Fecha de creacion
        /// </summary>
        [Required]
        [SwaggerSchema("Fecha de creacion")]
        public string creationDate { get; set; }

        /// <summary>
        /// Hora de creacion
        /// </summary>
        [Required]
        [SwaggerSchema("Hora de creacion")]
        public string creationTime { get; set; }

        /// <summary>
        /// Numero de referencia de la transaccion
        /// </summary>
        [SwaggerSchema("Numero de referencia de la transaccion")]
        public string? transactionReference { get; set; }

        /// <summary>
        /// Numero de instruccion de la transferencia
        /// </summary>
        [Required]
        [SwaggerSchema("Numero de instruccion de la transferencia")]
        public string referenceInstructionId { get; set; }
    }

}
