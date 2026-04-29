using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.Cancelaciones
{
    public record GeneralCancelacionDTO
    {
        /// <summary>
        /// Codigo Entidad Receptora
        /// </summary>
        [Required]
        [SwaggerSchema("Codigo Entidad Receptora")]
        public string creditorParticipantCode { get; set; }

        /// <summary>
        /// Codigo de moneda
        /// </summary>        
        [Required]
        [SwaggerSchema("Codigo de moneda")]
        public string currency { get; set; }

        /// <summary>
        /// Numero de instruccion de la cancelacion
        /// </summary>        
        [Required]
        [SwaggerSchema("Numero de instruccion de la cancelacion")]
        public string instructionId { get; set; }

        /// <summary>
        /// Rama 
        /// </summary>        
        [Required]
        [SwaggerSchema("Rama")]
        public string branchId { get; set; }
    }
}
