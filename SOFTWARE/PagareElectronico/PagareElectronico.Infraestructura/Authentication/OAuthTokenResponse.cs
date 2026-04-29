namespace PagareElectronico.Infrastructure.Authentication;

/// <summary>
/// Representa la respuesta devuelta por el endpoint OAuth 2.0 de CAVALI.
/// </summary>
public sealed class OAuthTokenResponse
{
    /// <summary>
    /// Token de acceso emitido por el servidor de autorización.
    /// </summary>
    public string Access_Token { get; set; } = string.Empty;

    /// <summary>
    /// Tiempo de vigencia del token en segundos.
    /// </summary>
    public int Expires_In { get; set; }

    /// <summary>
    /// Tipo de token devuelto, normalmente Bearer.
    /// </summary>
    public string Token_Type { get; set; } = string.Empty;
}