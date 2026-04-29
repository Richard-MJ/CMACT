using Takana.Transferencias.CCE.Api.Common.Interfaz;

namespace Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion
{
    public interface IServicioAplicacionSFTP
    {
        /// <summary>
        /// Método que sube el archivo al servicio SFTP de la CCE
        /// </summary>
        /// <param name="archivos"></param>
        /// <param name="configSFTP"></param>
        Task SubirArchivoSFTP(List<ArchivoAdjuntoDTO> archivos, IConfiguracionSFTP configSFTP);
        /// <summary>
        /// Método que obtiene un archivo desde el servicio SFTP de la CCE y lo devuelve como byte[]
        /// </summary>
        /// <param name="nombreArchivo">Nombre del archivo a descargar (incluyendo extensión)</param>
        /// <param name="configSFTP">Configuración del servidor SFTP</param>
        /// <returns>Contenido del archivo en formato byte[]</returns>
        Task<byte[]> ObtenerArchivoSFTP(string nombreArchivo, IConfiguracionSFTP configSFTP);
        /// <summary>
        /// Método para copiar archivo en Carpeta Compartida
        /// </summary>
        /// <param name="archivos"></param>
        /// <param name="configSFTP"></param>
        /// <returns></returns>
        Task CopiarArchivoCarpetaCompartida(List<ArchivoAdjuntoDTO> archivos, IConfiguracionSFTP configSFTP);
    }
}
