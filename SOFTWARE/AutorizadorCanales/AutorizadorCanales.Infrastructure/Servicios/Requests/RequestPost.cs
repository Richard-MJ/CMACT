using AutorizadorCanales.ServiciosExternos.Interfaz.Cliente;
using System.Text;
using System.Text.Json;

namespace AutorizadorCanales.Infrastructure.Servicios.Requests;

public class RequestPost : IRequest
{
    public string UrlBase { get; }
    private string EndPoint { get; }
    private HttpContent Contenido { get; }
    public bool ActivarOauth { get; }
    public string Token { get; set; }

    public RequestPost(string urlBase, bool activarOAuth,
        string endPoint, object datos, string token)
    {
        UrlBase = urlBase;
        EndPoint = endPoint;
        ActivarOauth = activarOAuth;
        Contenido = new StringContent(JsonSerializer.Serialize(datos), Encoding.UTF8, "application/json");
        Token = token;
    }

    public HttpRequestMessage GenerarSolicitud()
    {
        return new HttpRequestMessage(HttpMethod.Post, EndPoint) { Content = Contenido };
    }
}
