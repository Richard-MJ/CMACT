using Swashbuckle.AspNetCore.Annotations;
using Takana.Transferencias.CCE.Api.Common.Interoperabilidad;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad
{
    /// <summary>
    /// Clase de datos par realizar la consulta de cuenta completa con QR
    /// </summary>
    public record ConsultaCuentaCompletaQRDTO : LeerQRDTO
    {
        /// <summary>
        /// Numero de cuenta Originante
        /// </summary>
        [SwaggerSchema("Numero de cuenta Originante")]
        public string NumeroCuentaOriginante { get; set; }
    }
}
