using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Common.Interoperabilidad;

public record LeerQRDTO
{
    /// <summary>
    /// Hash de QR leido
    /// </summary>
    [SwaggerSchema("Hash de QR leido")]
    public string CadenaHash {get; set;}
}
