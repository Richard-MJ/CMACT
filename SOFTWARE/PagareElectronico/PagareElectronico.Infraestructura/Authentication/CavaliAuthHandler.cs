using System.Net.Http.Headers;

namespace PagareElectronico.Infrastructure.Authentication;

/// <summary>
/// Handler HTTP encargado de adjuntar automáticamente el token Bearer en cada solicitud a CAVALI.
/// </summary>
public sealed class CavaliAuthHandler : DelegatingHandler
{
    private readonly ICavaliTokenProvider _tokenProvider;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="CavaliAuthHandler"/>.
    /// </summary>
    /// <param name="tokenProvider">Proveedor encargado de obtener el token OAuth.</param>
    public CavaliAuthHandler(ICavaliTokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }

    /// <summary>
    /// Intercepta la solicitud saliente y agrega el encabezado Authorization con el token Bearer.
    /// </summary>
    /// <param name="request">Solicitud HTTP saliente.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Respuesta HTTP del proveedor externo.</returns>
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await _tokenProvider.GetTokenAsync(cancellationToken);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }
}