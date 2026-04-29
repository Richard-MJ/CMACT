using System.Net;
using System.Text.Json;

namespace AutorizadorCanales.Infrastructure.Servicios.Requests;

public class RespuestaRequest
{
    private HttpStatusCode CodigoRespuesta { get; }
    private string Contenido { get; }

    public RespuestaRequest(HttpStatusCode codigoRespuesta, string contenido)
    {
        CodigoRespuesta = codigoRespuesta;
        Contenido = contenido;
    }

    public bool TieneContenido()
    {
        if (CodigoRespuesta == HttpStatusCode.NoContent)
        {
            return false;
        }

        return Contenido.Length > 0;
    }

    public bool RespuestaValida()
    {
        if (CodigoRespuesta == HttpStatusCode.Accepted
            || CodigoRespuesta == HttpStatusCode.NoContent
            || CodigoRespuesta == HttpStatusCode.Created
            || CodigoRespuesta == HttpStatusCode.OK)
        {
            return true;
        }

        return false;
    }

    public bool EsMensajePersonalizado()
    {
        return CodigoRespuesta == HttpStatusCode.BadRequest;
    }

    public bool EstaNoAutorizado() =>
    CodigoRespuesta == HttpStatusCode.Unauthorized;

    public bool ErrorServidor() =>
        CodigoRespuesta == HttpStatusCode.InternalServerError;

    public T ContenidoDeRespuesta<T>()
    {
        if (TieneContenido())
        {
            try
            {
                return JsonSerializer.Deserialize<T>(Contenido)!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al tratar de recuperar el contenido de la respuesta. {ex.Message}", ex);
            }
        }

        throw new Exception("Respuesta sin contenido.");
    }
}
