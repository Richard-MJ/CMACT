namespace PagareElectronico.Infrastructure.Authentication;

/// <summary>
/// Define el contrato para obtener tokens OAuth de CAVALI.
/// </summary>
public interface ICavaliTokenProvider
{
    /// <summary>
    /// Obtiene un token válido para invocar los servicios protegidos de CAVALI.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Token de acceso OAuth.</returns>
    Task<string> GetTokenAsync(CancellationToken cancellationToken);
}