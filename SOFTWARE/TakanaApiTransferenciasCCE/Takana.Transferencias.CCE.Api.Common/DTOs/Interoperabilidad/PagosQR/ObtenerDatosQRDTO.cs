namespace Takana.Transferencias.CCE.Api.Common.Interoperabilidad;

/// <summary>
/// Clase de datos para obtener datos del QR
/// </summary>
public record ObtenerDatosQRDTO
{
    /// <summary>
    /// Codigo cuenta interbancario
    /// </summary>
    public string IdentificadorCuenta {get; set;}
    /// <summary>
    /// Identificador unico QR
    /// </summary>
    public string? IdentificadorQR {get; set;}
}
