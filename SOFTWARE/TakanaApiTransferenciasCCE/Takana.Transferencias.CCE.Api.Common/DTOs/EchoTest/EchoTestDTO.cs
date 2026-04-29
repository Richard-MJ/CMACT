using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.EchoTest
{
    public record EchoTestDTO : GeneralEchoTestDTO
    {
        #region Propiedades
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
        #endregion
    }
}
