using Microsoft.Extensions.Configuration;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.ServiciosExternos
{
    /// <summary>
    /// Agente base para procesar las operaciones a servicios APIs
    /// </summary>
    public class ServicioAplicacionPeticionBase : IServicioAplicacionPeticionBase, IDisposable
    {
        /// <summary>
        /// Servicio general para las operaciones a servicios APIs
        /// </summary>
        protected InvocarPeticion _InvocarPeticionApi;
        /// <summary>
        /// Servicio de bitacora
        /// </summary>
        private readonly IBitacora<ServicioAplicacionPeticionBase> _bitacora;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public ServicioAplicacionPeticionBase(
            IConfiguration configuration,
            IBitacora<ServicioAplicacionPeticionBase> bitacora)
        {
            _bitacora = bitacora;
            _InvocarPeticionApi = new InvocarPeticion(configuration);
        }

        /// <summary>
        /// Método que invoca al servicio general para operaciones a servicios APIs
        /// </summary>
        /// <typeparam name="T">Tipo de dato de respuesta</typeparam>
        /// <param name="urlServicio">URL del servicio API a invocar</param>
        /// <param name="recurso">Recurso del servicio API</param>
        /// <param name="metodoHttp">Método http de la petición</param>
        /// <param name="encabezado">Encabezado de la peticion</param>
        /// <param name="timeout">Tiempo de espera máximo para el proceso de ejecución al servicio API</param>
        /// <param name="datos">Datos de la petición (body)</param>
        /// <param name="esMensajeParaCCE">Si es un mensaje para la CCE</param>
        /// <returns>Respuesta de la operación ejecutada</returns>
        public async Task<T> EjecutarRequestAsync<T>(string urlServicio, string recurso, HttpMethod metodoHttp,
            Dictionary<string, string> encabezado, int timeout, object? datos, bool esMensajeParaCCE)
        {
            try
            {
                return await _InvocarPeticionApi.RealizarRequestAsync<T>(urlServicio, recurso, metodoHttp, encabezado,
                    timeout, datos, esMensajeParaCCE);
            }
            catch (Exception excepcion)
            {
                _bitacora.Error($"Ocurrio un error en la Petición - Host: {urlServicio} : {excepcion.Message} {excepcion.InnerException}");
                throw new Exception("falto algo : " + urlServicio + excepcion.Message, excepcion.InnerException);
            }
        }

        /// <summary>
        /// Método que libera la instancia del servicio general de operaciones a servicios APIs
        /// </summary>
        public void Dispose() 
        {
            _InvocarPeticionApi = null;
        }   
    }
}