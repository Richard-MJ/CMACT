using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.CF
{
    public class ConceptoCobroDTO
    {
        /// <summary>
        /// Identificador de conceptp
        /// </summary>
        [SwaggerSchema("Identificador de conceptp")]
        public int IdConcepto { get; set; }
        /// <summary>
        /// Codigo del concepto
        /// </summary>
        [SwaggerSchema("Codigo del concepto")]
        public string Codigo { get; set; }
        /// <summary>
        /// Descripcion del concepto
        /// </summary>
        [SwaggerSchema("Descripcion del concepto")]
        public string Descripcion { get; set; }
        /// <summary>
        /// Indicador de habilitado o no habilitado
        /// </summary>
        [SwaggerSchema("Indicador de habilitado o no habilitado")]
        public bool IndicadorHabilitado { get; set; }
    }
}
