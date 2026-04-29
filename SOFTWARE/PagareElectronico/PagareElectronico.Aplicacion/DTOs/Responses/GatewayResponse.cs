namespace PagareElectronico.Application.DTOs.Responses;

/// <summary>
/// Representa una respuesta estándar devuelta por la API gateway luego de invocar un proveedor externo.
/// </summary>
public record class GatewayResponse<T>(
    bool Success, 
    int StatusCode, 
    string Message, 
    T? ProviderResponse);
