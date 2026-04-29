namespace PagareElectronico.Infrastructure.Configuration;

/// <summary>
/// Representa la configuración necesaria para consumir los servicios REST de CAVALI.
/// </summary>
public sealed class CavaliApiOptions
{
    /// <summary>
    /// Nombre de la sección de configuración utilizada en appsettings.
    /// </summary>
    public const string SectionName = "CavaliApi";

    /// <summary>
    /// URL del endpoint de autenticación OAuth 2.0.
    /// </summary>
    public string AuthUrl { get; set; } = string.Empty;

    /// <summary>
    /// URL base de los servicios REST de CAVALI.
    /// </summary>
    public string BaseUrl { get; set; } = string.Empty;

    /// <summary>
    /// Código de participante asignado por CAVALI a la entidad.
    /// </summary>
    public int ParticipantCode { get; set; }

    /// <summary>
    /// Código del banco asociado a pagarés.
    /// </summary>
    public int BankCode { get; set; }

    /// <summary>
    /// Código del producto asociado a pagarés.
    /// </summary>
    public int ProductCode { get; set; }

    /// <summary>
    /// Identificador del cliente OAuth entregado por CAVALI.
    /// </summary>
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// Secreto del cliente OAuth entregado por CAVALI.
    /// </summary>
    public string ClientSecret { get; set; } = string.Empty;

    /// <summary>
    /// API Key requerida por CAVALI para invocar recursos protegidos.
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// Tiempo máximo de espera en segundos para las llamadas HTTP.
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;
}