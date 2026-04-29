using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.EchoTest
{
    public record GeneralEchoTestDTO
    {
        #region Constantes
        /// <summary>
        /// Descripción de la tarea manual echo test
        /// </summary>
        public const string TareaManualEchoTest = "tarea_manual_echo_test";
        #endregion

        #region Propiedades
        /// <summary>
        /// Codigo de entidad originante
        /// </summary>
        [Required]
        [SwaggerSchema("Codigo de entidad originante")]
        public string participantCode { get; set; }
        /// <summary>
        /// Numero de seguimiento
        /// </summary>
        [Required]
        [SwaggerSchema("Numero de seguimiento")]
        public string trace { get; set; }
        #endregion
    }
}
