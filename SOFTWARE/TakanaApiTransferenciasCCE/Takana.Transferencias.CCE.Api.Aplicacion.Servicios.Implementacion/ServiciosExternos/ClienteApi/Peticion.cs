using System.Text;
using System.Text.Json;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.ServiciosExternos
{
    /// <summary>
    /// Clase que contiene los datos necesarios para una petición
    /// </summary>
    public class Peticion
    {
        /// <summary>
        /// URL base del servicio
        /// </summary>
        public string UrlBase { get; }
        /// <summary>
        /// Recurso de la petición
        /// </summary>
        public string Recurso { get; }
        /// <summary>
        /// Tiempo máximo de espera
        /// </summary>
        public int Timeout { get; }
        /// <summary>
        /// Método HTTP de la petición
        /// </summary>
        public HttpMethod MetodoHttp { get; }
        /// <summary>
        /// Encabezados de la petición
        /// </summary>
        public Dictionary<string, string> Encabezado { get; }

        /// <summary>
        /// Contenido de la petición
        /// </summary>
        public HttpContent Contenido { get; set; }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="urlBase">URL base del servicio</param>
        /// <param name="recurso">Recurso de la petición</param>
        /// <param name="metodoHttp">Método HTTP de la petición</param>
        /// <param name="encabezado">Encabezados de la petición</param>
        /// <param name="timeout">Tiempo máximo de espera para la petición</param>
        /// <param name="datos">Datos de la petición (cuerpo)</param>
        public Peticion(string urlBase, string recurso, HttpMethod metodoHttp,
            Dictionary<string, string> encabezado, int timeout, object? datos)
        {
            var headers = new Dictionary<string, string>();

            if (encabezado == null) encabezado = new Dictionary<string, string>();

            foreach (var elemento in encabezado)
            {
                headers.Add(elemento.Key, elemento.Value);
            }

            UrlBase = urlBase;
            Recurso = recurso;
            MetodoHttp = metodoHttp;
            Encabezado = headers;
            Timeout = timeout;

            if(datos != null)
            {
                var datosSerializados = JsonSerializer.Serialize(datos);
                Contenido = new StringContent(datosSerializados, Encoding.UTF8, "application/json");
                Contenido.Headers.ContentLength = datosSerializados.Length;
            }
        }

        /// <summary>
        /// Método que obtiene el tipo de solicitud para la petición según el método HTTP
        /// </summary>
        /// <returns>Solicitud a procesar</returns>
        public HttpRequestMessage ObtenerSolicitudSegunMetodoHttp()
        {
            switch (MetodoHttp.Method)
            {
                case "GET":
                    return new HttpRequestMessage(MetodoHttp, Recurso)
                    { Content = Contenido };
                case "POST":
                    return new HttpRequestMessage(MetodoHttp, Recurso)
                    { Content = Contenido };
                case "DELETE":
                    return new HttpRequestMessage(MetodoHttp, Recurso);
                default:
                    return new HttpRequestMessage();
            }
        }
    }
}