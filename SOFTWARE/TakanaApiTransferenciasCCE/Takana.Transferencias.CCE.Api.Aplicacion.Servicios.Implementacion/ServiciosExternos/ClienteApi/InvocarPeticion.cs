using Microsoft.Extensions.Configuration;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.ServiciosExternos
{
    /// <summary>
    /// Clase para las peticiones a servicios APIs
    /// </summary>
    public class InvocarPeticion
    {
        /// <summary>
        /// Cliente API para las peticiones HTTP
        /// </summary>
        private readonly PeticionBase _PeticionBase;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public InvocarPeticion(IConfiguration configuration)
        {
            _PeticionBase = new PeticionBase(configuration);
        }

        /// <summary>
        /// Método que realiza la operación al servicio API indicado según parámetros
        /// </summary>
        /// <typeparam name="T">Tipo de dato de respuesta</typeparam>
        /// <param name="urlServicio">URL del servicio API a invocar</param>
        /// <param name="recurso">Recurso del servicio API</param>
        /// <param name="metodoHttp">Método http de la petición</param>
        /// <param name="encabezado">Encabezado de la peticion</param>
        /// <param name="timeout">Tiempo de espera máximo para el proceso
        /// de ejecución al servicio API</param>
        /// <param name="datos">Datos de la petición (body)</param>
        /// <returns>Respuesta de la operación ejecutada</returns>
        public async Task<T> RealizarRequestAsync<T>(string urlServicio, string recurso, HttpMethod metodoHttp,
            Dictionary<string, string> encabezado, int timeout, object? datos, bool esMensajeParaCCE)
        {
            RespuestaRequest resultado;
            try
            {
                var peticion = new Peticion(urlServicio, recurso, metodoHttp, encabezado, timeout, datos);

                resultado = await _PeticionBase.InvocarBackendAsync(peticion, esMensajeParaCCE);
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message, excepcion.InnerException);
            }

            if (!resultado.RespuestaValida())
            {
                if (resultado.EsMensajePersonalizado())
                {
                    var mensajePersonalizado = resultado.ContenidoDeRespuesta<RespuestaPersonalizada>();
                    throw new Exception($"{mensajePersonalizado.Codigo} - {mensajePersonalizado.Mensaje}");
                }
                else if (resultado.NoAutorizado()) 
                {
                    throw new UnauthorizedAccessException();
                } else
                throw new Exception("Error en el servidor API. Comuníquese con el área de " +
                    "TIC, indicando la hora en la que realizó la operación.");
            }

            if (string.IsNullOrEmpty(resultado.Contenido) || resultado.Contenido == "true")
                return (T)Convert.ChangeType(resultado.CodigoRespuestaHttp, typeof(T));

            return resultado.ContenidoDeRespuesta<T>();
        }
    }
}