using System.Text.Json.Serialization;

namespace AutorizadorCanales.Contracts.SG.Response;

public record VerificacionAutenticacionResponse(
    [property: JsonPropertyName("token_type")] string TokenType,
    [property: JsonPropertyName("refresh_token")] string RefreshToken,
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("expires_in")] string ExpiresIn,
    [property: JsonPropertyName("inactivity_in")] int InactivityIn,
    [property: JsonPropertyName(".issued")] string Issued,
    [property: JsonPropertyName(".expires")] string Expires,
    [property: JsonPropertyName("guid")] string Guid);