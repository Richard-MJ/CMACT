namespace AutorizadorCanales.ServiciosExternos.Interfaz.Cliente;

/// <summary>
/// Interfaz para los datos necesarios para un request.
/// </summary>
public interface IRequest
{
    /// <summary>
    /// Ruta del endpoint a invocar en el servidor.
    /// </summary>
    string UrlBase { get; }
    /// <summary>
    /// Indica si se debe activar el envio de un token de autenticación.
    /// </summary>
    bool ActivarOauth { get; }
    /// <summary>
    /// Token de autenticación.
    /// </summary>
    string Token { get; }

    /// <summary>
    /// Se genera un objeto HttpRequestMessage para su uso en el request al servicio.
    /// </summary>
    /// <returns>Objeto con la información para realizar el request.</returns>
    HttpRequestMessage GenerarSolicitud();
}
