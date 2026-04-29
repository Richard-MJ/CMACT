using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.ServiciosExternos
{
    /// <summary>
    /// Clase que realiza la conexión al servicio API, y realiza
    /// la operación indicada
    /// </summary>
    public class PeticionBase
    {
        private readonly IConfiguration _configuration;

        public PeticionBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Método que invoca el procesamiento de las peticiones al servicio API
        /// </summary>
        /// <param name="peticion">Petición a procesar</param>
        /// <param name="esMensajeParaCCE">Petición a procesar</param>
        /// <returns>Respuesta de la petición procesada</returns>
        public async Task<RespuestaRequest> InvocarBackendAsync(Peticion peticion, bool esMensajeParaCCE)
        {
            using (var handler = new HttpClientHandler())
            {
                EstablecerParametrosDeSeguridad(handler, esMensajeParaCCE);
                var respuesta = await ProcesarPeticion(handler, peticion, esMensajeParaCCE);

                return new RespuestaRequest((int)respuesta.StatusCode,
                    respuesta.Content.ReadAsStringAsync().Result);
            }
        }

        #region Métodos privados

        /// <summary>
        /// Método que establece la configuración de seguridad para la petición
        /// </summary>
        /// <param name="handler">Manejador de la petición a modificar con la
        /// configuración de seguridad</param>
        /// <returns>Manejador de la petición con la configuración de seguridad
        /// establecida</returns>
        private HttpClientHandler EstablecerParametrosDeSeguridad(HttpClientHandler handler, bool esMensajeParaCCE)
        {
            ConfigurarCertificadoCliente(handler, esMensajeParaCCE);
            ConfigurarProtocolosDeSeguridad();
            ConfigurarValidacionCertificadoServidor(handler);
            handler.MaxConnectionsPerServer = 10;
            return handler;
        }
         
        /// <summary>
        /// Configurar y Agregar el Certificado del Cliente
        /// </summary>
        /// <param name="handler"></param>
        private void ConfigurarCertificadoCliente(HttpClientHandler handler, bool esMensajeParaCCE)
        {
            if (esMensajeParaCCE)
            {
                var certificadoCliente = ObtenerCertificadoDelAlmacen();
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.ClientCertificates.Add(certificadoCliente!);
            }
        }

        /// <summary>
        /// Obtiene el certificado del almacen de confianza del servidor
        /// </summary>
        /// <returns>Retorna certificado o null</returns>
        private X509Certificate2? ObtenerCertificadoDelAlmacen()
        {
            string nombreCertificado = _configuration["NOMBRE_CERTIFICADO_TLS_CMACT"]!;

            using var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certificados = store.Certificates.Find(X509FindType.FindBySubjectName, nombreCertificado, validOnly: true);
            store.Close();

            if (certificados.Count > 0) return certificados[0];

            throw new Exception("No se encontró el certificado TLS requerido.");
        }

        /// <summary>
        /// Configura los protocolos de Seguridad
        /// </summary>
        private void ConfigurarProtocolosDeSeguridad() => ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

        /// <summary>
        /// Configura la Validacion de Certificados mTLS mutua para el Servidor.
        /// </summary>
        /// <param name="handler"></param>
        /// <exception cref="Exception"></exception>
        private void ConfigurarValidacionCertificadoServidor(HttpClientHandler handler)
        {
            ServicePointManager.ServerCertificateValidationCallback =
                (message, cert, chain, errors) => true;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, certChain, sslPolicyErrors) => true;
        }

        /// <summary>
        /// Método que procesa la petición al servicio API
        /// </summary>
        /// <param name="handler">Manejador de la petición</param>
        /// <param name="peticion">Petición a ejecutar</param>
        /// <param name="esMensajeParaCCE">Petición a ejecutar</param>
        /// <returns>Respuesta de la petición ejecutada</returns>
        private async Task<HttpResponseMessage> ProcesarPeticion(
            HttpClientHandler handler, Peticion peticion, bool esMensajeParaCCE)
        {
            using (var cliente = new HttpClient(handler))
            {
                EstablacerEncabezado(cliente, peticion, esMensajeParaCCE);
                cliente.Timeout = TimeSpan.FromSeconds(peticion.Timeout);
                var solicitud = peticion.ObtenerSolicitudSegunMetodoHttp();
                var tokenCancelacion = new CancellationTokenSource();
                try
                {
                    return await cliente.SendAsync(solicitud, tokenCancelacion.Token);
                }
                catch (Exception excepcion)
                {
                    if (excepcion.InnerException?.GetType() == typeof(TaskCanceledException))
                    {
                        var excepcionToken = (TaskCanceledException)excepcion.InnerException;
                        if (excepcionToken.CancellationToken != tokenCancelacion.Token)
                            throw new Exception("Se agotó el tiempo de espera para el " +
                                "servicio API.", excepcion.InnerException ?? excepcion);
                    }
                    throw new Exception("Error al invocar servicio API.",
                        excepcion.InnerException ?? excepcion);
                }
            }
        }
        /// <summary>
        /// Método que establece el encabezado a la petición
        /// </summary>
        /// <param name="cliente">Cliente HTTP de la petición a modificar con los encabezados</param>
        /// <param name="peticion">Petición que contiene los encabezados a establecer</param>
        /// <param name="esMensajeParaCCE">Petición que contiene los encabezados a establecer</param>
        /// <returns>Cliente HTTP de la petición modificado con los encabezados establecidos</returns>
        private HttpClient EstablacerEncabezado(HttpClient cliente, Peticion peticion, bool esMensajeParaCCE)
        {
            cliente.BaseAddress = new Uri(peticion.UrlBase);
            cliente.DefaultRequestHeaders.Accept.Clear();
            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (esMensajeParaCCE)
            {
                cliente.DefaultRequestHeaders.Host = cliente.BaseAddress.Host;
                cliente.DefaultRequestHeaders.AcceptEncoding.Clear();
                cliente.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("identity"));
            }
            foreach (KeyValuePair<string, string> encabezado in peticion.Encabezado)
            {
                cliente.DefaultRequestHeaders.Add(encabezado.Key, encabezado.Value);
            }
            return cliente;
        }
        #endregion Métodos privados
    }
}