using System.Net;
using Newtonsoft.Json;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.ServiciosExternos
{
    /// <summary>
    /// Clase encargada validar la respuesta segun el codigo y contenido
    /// </summary>
    public class RespuestaRequest 
    {
        /// <summary>
        /// Codigo de respuesta de las peticiones
        /// </summary>
        public int CodigoRespuesta { get; }

        /// <summary>
        /// Contenido de respuesta de las peticiones
        /// </summary>
        public string Contenido { get; }
        /// <summary>
        /// Codigo de respuesta Http de las peticiones
        /// </summary>
        public HttpStatusCode CodigoRespuestaHttp => (HttpStatusCode)CodigoRespuesta;

        /// <summary>
        /// Constructor de respuesta de request
        /// </summary>
        /// <param name="codigoRespuesta"></param>
        /// <param name="contenido"></param>
        public RespuestaRequest(int codigoRespuesta, string contenido)
        {
            CodigoRespuesta = codigoRespuesta;
            Contenido = contenido;
        }
        /// <summary>
        /// Metodo que valida la respuesta 
        /// </summary>
        /// <returns>Retorna true</returns>
        public bool RespuestaValida()
            => CodigoRespuestaHttp == HttpStatusCode.Accepted
                || CodigoRespuestaHttp == HttpStatusCode.NoContent
                || CodigoRespuestaHttp == HttpStatusCode.Created
                || CodigoRespuestaHttp == HttpStatusCode.OK;
        /// <summary>
        /// Obtiene el mensaje personalizado
        /// </summary>
        /// <returns></returns>
        public bool EsMensajePersonalizado()
            => CodigoRespuestaHttp == HttpStatusCode.BadRequest;

        /// <summary>
        /// Deserealiza el contenido de la respuesta
        /// </summary>
        /// <typeparam name="T"> Clases generica </typeparam>
        /// <returns>Devuelve el contenido deserealizado</returns>
        public T ContenidoDeRespuesta<T>()
        {
            if (TieneContenido())
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(Contenido)!;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al tratar de recuperar el contenido de la respuesta. {ex.Message}", ex);
                }
            }
            
            throw new Exception("Respuesta sin contenido.");
        }
        /// <summary>
        /// Metodo que valida si tiene contenido
        /// </summary>
        /// <returns>Retorna true si tiene contenido</returns>
        public bool TieneContenido()
        {
            if (CodigoRespuestaHttp == HttpStatusCode.NoContent)
                return false;

            return Contenido.Length > 0;
        }
        /// <summary>
        /// Metodo que valida si la peticion no fue autorizada
        /// </summary>
        /// <returns></returns>
        public bool NoAutorizado()
            => CodigoRespuestaHttp == HttpStatusCode.Unauthorized;
    }
}