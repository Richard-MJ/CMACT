using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using PagareElectronico.Infrastructure.Configuration;

namespace PagareElectronico.Infrastructure.Authentication;

/// <summary>
/// Implementa la obtención y almacenamiento temporal del token OAuth para CAVALI.
/// </summary>
public sealed class CavaliTokenProvider : ICavaliTokenProvider
{
    /// <summary>
    /// Clave de caché utilizada para almacenar el token.
    /// </summary>
    private const string CacheKey = "cavali_token";

    /// <summary>
    /// Semáforo que evita solicitudes concurrentes innecesarias al endpoint de autenticación.
    /// </summary>
    private static readonly SemaphoreSlim Semaphore = new(1, 1);

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMemoryCache _cache;
    private readonly CavaliApiOptions _options;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="CavaliTokenProvider"/>.
    /// </summary>
    /// <param name="httpClientFactory">Fábrica de clientes HTTP.</param>
    /// <param name="cache">Caché en memoria para almacenar temporalmente el token.</param>
    /// <param name="options">Configuración del API de CAVALI.</param>
    public CavaliTokenProvider(
        IHttpClientFactory httpClientFactory,
        IMemoryCache cache,
        IOptions<CavaliApiOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _cache = cache;
        _options = options.Value;
    }

    /// <summary>
    /// Obtiene un token OAuth válido desde caché o desde el endpoint de autenticación.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Token de acceso OAuth.</returns>
    public async Task<string> GetTokenAsync(CancellationToken cancellationToken)
    {
        if (_cache.TryGetValue(CacheKey, out string? token) &&
            !string.IsNullOrWhiteSpace(token))
        {
            return token;
        }

        await Semaphore.WaitAsync(cancellationToken);

        try
        {
            if (_cache.TryGetValue(CacheKey, out token) &&
                !string.IsNullOrWhiteSpace(token))
            {
                return token;
            }

            var client = _httpClientFactory.CreateClient("CavaliAuth");

            using var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["client_id"] = _options.ClientId,
                ["client_secret"] = _options.ClientSecret,
                ["grant_type"] = "client_credentials"
            });

            using var response = await client.PostAsync(_options.AuthUrl, content, cancellationToken);
            var body = await response.Content.ReadAsStringAsync(cancellationToken);

            response.EnsureSuccessStatusCode();

            var tokenResponse = await response.Content.ReadFromJsonAsync<OAuthTokenResponse>(
                cancellationToken: cancellationToken);

            if (tokenResponse is null || string.IsNullOrWhiteSpace(tokenResponse.Access_Token))
                throw new InvalidOperationException("No se pudo obtener un token válido desde CAVALI.");

            var ttl = Math.Max(tokenResponse.Expires_In - 60, 60);

            _cache.Set(
                CacheKey,
                tokenResponse.Access_Token,
                TimeSpan.FromSeconds(ttl));

            return tokenResponse.Access_Token;
        }
        finally
        {
            Semaphore.Release();
        }
    }
}