using Swashbuckle.AspNetCore.Annotations;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad
{
    /// <summary>
    /// Clase de datos para consulta de interoperabilidad
    /// </summary>
    public record ResultadoConsultaCuentaInteroperabilidadDTO
    {
        /// <summary>
        /// Resultado de la consulta cuenta
        /// </summary>
        [SwaggerSchema("Resultado de la consulta cuenta")]
        public ResultadoConsultaCuentaCCE ResultadoConsultaCuenta { get; set; }
        
    }
}
