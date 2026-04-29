using AutorizadorCanales.ServiciosExternos.Interfaz.Cliente;
using System.Net;
using System.Net.Http.Headers;

namespace AutorizadorCanales.Infrastructure.Servicios.Requests;

public static class ServicioClienteApi
{
    public static async Task<RespuestaRequest> InvocarBackendAsync(IRequest request, Dictionary<string, string>? encabezado = null)
    {
        string contenidoRespuesta;
        HttpStatusCode codigoRespuesta;

        using (var handler = new HttpClientHandler())
        {
            handler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => true;

            using (var cliente = new HttpClient(handler))
            {
                cliente.BaseAddress = new Uri(request.UrlBase);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (encabezado != null)
                {
                    foreach (KeyValuePair<string, string> loEncabezado in encabezado)
                    {
                        cliente.DefaultRequestHeaders.Add(loEncabezado.Key, loEncabezado.Value);
                    }
                }
                if (request.ActivarOauth)
                {
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Token);
                }
                var solicitud = request.GenerarSolicitud();
                HttpResponseMessage respuesta;
                try
                {
                    respuesta = await cliente.SendAsync(solicitud);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al invocar servicio API " + request.UrlBase + ".", ex);
                }

                codigoRespuesta = respuesta.StatusCode;
                contenidoRespuesta = await respuesta.Content.ReadAsStringAsync();
            }
        }

        var response = new RespuestaRequest(codigoRespuesta, contenidoRespuesta);

        return response;
    }
}
