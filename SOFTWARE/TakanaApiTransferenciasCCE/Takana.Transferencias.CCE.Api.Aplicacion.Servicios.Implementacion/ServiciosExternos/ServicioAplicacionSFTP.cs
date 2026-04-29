using Renci.SshNet;
using Renci.SshNet.Common;
using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.ServiciosExternos
{
    /// <summary>
    /// Servicio general para las operaciones a servicios APIs
    /// </summary>
    public class ServicioAplicacionSFTP : IServicioAplicacionSFTP
    {
        #region Declaracion de variables
        private readonly IBitacora<ServicioAplicacionSFTP> _bitacora;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        public ServicioAplicacionSFTP(
            IBitacora<ServicioAplicacionSFTP> bitacora)
        {
            _bitacora = bitacora;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método que sube el archivo al servicio SFTP de la CCE
        /// </summary>
        /// <param name="reportes"></param>
        /// <param name="configSFTP"></param>
        public async Task SubirArchivoSFTP(List<ArchivoAdjuntoDTO> archivos, IConfiguracionSFTP configSFTP)
        {
            try
            {
                using (var clienteSFTP = CrearClienteSFTP(configSFTP))
                {
                    clienteSFTP.Connect();

                    if (!clienteSFTP.IsConnected)
                        throw new InvalidOperationException($"No se pudo conectar al servidor SFTP {configSFTP.Ip}");

                    foreach (var archivo in archivos)
                    {
                        try
                        {
                            var rutaArchivo = $"{configSFTP.RutaDestino}/{archivo.NombreArchivo}";

                            if (!await clienteSFTP.ExistsAsync(configSFTP.RutaDestino))
                                await clienteSFTP.CreateDirectoryAsync(configSFTP.RutaDestino);

                            using (var archivoStream = new MemoryStream(archivo.Archivo))
                            {
                                archivoStream.Position = 0;
                                clienteSFTP.UploadFile(archivoStream, rutaArchivo, true);
                            }

                            _bitacora.Info($"Archivo subido exitosamente a '{rutaArchivo}'");
                        }
                        catch (Exception ex)
                        {
                            _bitacora.Error($"[ERROR] Archivo '{archivo.NombreArchivo}' no pudo subirse: {ex.Message}", ex);
                        }
                    }

                    clienteSFTP.Disconnect();

                }
            }
            catch (SshConnectionException excepcion)
            {
                _bitacora.Error($"[SSH] Error de conexión con {configSFTP.Ip}: {excepcion.Message}");
                throw new SshConnectionException($"[SSH] Error de conexión con {configSFTP.Ip}: {excepcion.Message}");
            }
            catch (SftpPathNotFoundException excepcion)
            {
                _bitacora.Error($"[SFTP] Ruta no encontrada '{configSFTP.RutaDestino}': {excepcion.Message}");
                throw new SftpPathNotFoundException($"[SFTP] Ruta no encontrada '{configSFTP.RutaDestino}': {excepcion.Message}");
            }
            catch (IOException excepcion)
            {
                _bitacora.Error($"[IO] Error de E/S al subir archivo a SFTP: {excepcion.Message}");
                throw new IOException($"[IO] Error de E/S al subir archivo a SFTP: {excepcion.Message}");
            }
            catch (Exception excepcion)
            {
                _bitacora.Error($"Error inesperado al subir archivo a {configSFTP.Ip} SFTP: {excepcion.Message}");
                throw new Exception($"Error inesperado al subir archivo a {configSFTP.Ip} SFTP: {excepcion.Message}");
            }
        }

        /// <summary>
        /// Método que obtiene un archivo desde el servicio SFTP de la CCE y lo devuelve como byte[]
        /// </summary>
        /// <param name="nombreArchivo">Nombre del archivo a descargar (incluyendo extensión)</param>
        /// <param name="configSFTP">Configuración del servidor SFTP</param>
        /// <returns>Contenido del archivo en formato byte[]</returns>
        public async Task<byte[]> ObtenerArchivoSFTP(string nombreArchivo, IConfiguracionSFTP configSFTP)
        {
            try
            {
                using (var clienteSFTP = CrearClienteSFTP(configSFTP))
                {
                    clienteSFTP.Connect();

                    if (!clienteSFTP.IsConnected)
                    {
                        _bitacora.Error($"No se pudo conectar al servidor {configSFTP.Ip} SFTP.");
                        throw new Exception($"No se pudo conectar al servidor {configSFTP.Ip} SFTP.");
                    }

                    var rutaArchivo = configSFTP.RutaDestino + "/respuesta/" + nombreArchivo;

                    if (!await clienteSFTP.ExistsAsync(rutaArchivo))
                    {
                        _bitacora.Error($"El archivo '{rutaArchivo}' no existe en el servidor SFTP.");
                        throw new FileNotFoundException($"El archivo '{rutaArchivo}' no existe en el servidor SFTP.");
                    }

                    byte[] contenido;

                    using (var archivoStream = new MemoryStream())
                    {
                        clienteSFTP.DownloadFile(rutaArchivo, archivoStream);
                        contenido = archivoStream.ToArray();
                    }

                    clienteSFTP.Disconnect();

                    return contenido;
                }
            }
            catch (Exception excepcion)
            {
                _bitacora.Error($"Ocurrió un error al obtener el archivo del servidor {configSFTP.Ip} SFTP: {excepcion.Message}");
                throw new Exception($"Ocurrió un error al obtener el archivo del servidor {configSFTP.Ip} SFTP: {excepcion.Message}");
            }
        }

        /// <summary>
        /// Metodo que crea el cliente SFTP
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private SftpClient CrearClienteSFTP(IConfiguracionSFTP config)
        {
            SftpClient cliente;

            if (!string.IsNullOrWhiteSpace(config.Llave))
            {
                var privateKey = new PrivateKeyFile(config.Llave);
                var connectionInfo = new ConnectionInfo(
                    config.Ip,
                    config.Puerto,
                    config.Usuario,
                    new PrivateKeyAuthenticationMethod(config.Usuario, privateKey)
                );

                cliente = new SftpClient(connectionInfo);
            }
            else
            {
                cliente = new SftpClient(config.Ip, config.Puerto, config.Usuario, config.Password);
            }

            cliente.ConnectionInfo.Timeout = TimeSpan.FromSeconds(30);
            cliente.KeepAliveInterval = TimeSpan.FromSeconds(15);
            cliente.OperationTimeout = TimeSpan.FromSeconds(60);

            return cliente;
        }

        /// <summary>
        /// Método para copiar archivo en Carpeta Compartida
        /// </summary>
        /// <param name="archivos"></param>
        /// <param name="configSFTP"></param>
        /// <returns></returns>
        public async Task CopiarArchivoCarpetaCompartida(List<ArchivoAdjuntoDTO> archivos, IConfiguracionSFTP configSFTP)
        {
            string rutaCompartida = $"\\\\{configSFTP.Ip}{configSFTP.RutaDestino}";

            foreach (var archivo in archivos)
            {
                try
                {
                    string rutaCompleta = Path.Combine(rutaCompartida, archivo.NombreArchivo);

                    using var stream = new FileStream(rutaCompleta, FileMode.Create, FileAccess.Write);
                    await stream.WriteAsync(archivo.Archivo, 0, archivo.Archivo.Length);

                    _bitacora.Info($"Archivo {archivo.NombreArchivo} copiado exitosamente a {rutaCompleta}");
                }
                catch (Exception excepcion)
                {
                    _bitacora.Error($"Error al copiar el archivo {archivo.NombreArchivo}: {excepcion.Message}");
                }
            }
        }

        #endregion
    }
}