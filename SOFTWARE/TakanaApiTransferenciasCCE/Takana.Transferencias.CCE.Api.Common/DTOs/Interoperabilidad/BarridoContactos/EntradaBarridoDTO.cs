using Swashbuckle.AspNetCore.Annotations;
using Takana.Transferencias.CCE.Api.Common.Interoperabilidad;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad
{
    /// <summary>
    /// Clase de datos de entrada para barrido de directorios
    /// </summary>
    public record EntradaBarridoDTO
    {
        /// <summary>
        /// Codigo de cuenta interbancario
        /// </summary>
        [SwaggerSchema("Codigo de cuenta interbancario")]
        public string CodigoCCI { get; set; }
        /// <summary>
        /// Numero de celular originante
        /// </summary>
        [SwaggerSchema("Numero de celular originante")]
        public string NumeroCelularOrigen {  get; set; }
        /// <summary>
        /// Contactos para barrido
        /// </summary>
        [SwaggerSchema("Contactos para barrido")]
        public List<ContactosBarrido> ContactosBarrido { get; set; }
    }
}
