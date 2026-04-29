using System.Text.Json.Serialization;

namespace AutorizadorCanales.Contracts.SG.Response;

public record AutenticacionResponse(
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("token_type")] string TokenType,
    [property: JsonPropertyName("expires_in")] int ExpiresIn,
    [property: JsonPropertyName("inactivity_in")] int InactivityIn,
    [property: JsonPropertyName("refresh_token")] string RefreshToken,
    [property: JsonPropertyName("as:client_id")] string AsClientId,
    [property: JsonPropertyName("autorizado")] string Autorizado,
    [property: JsonPropertyName("x:idClienteFinal")] string XIdClienteFinal,
    [property: JsonPropertyName("x:idVisual")] string XIdVisual,
    [property: JsonPropertyName("x:idSesion")] string XIdSesion,
    [property: JsonPropertyName(".issued")] string Issued,
    [property: JsonPropertyName(".expires")] string Expires);