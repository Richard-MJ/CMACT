using Takana.Transferencias.CCE.Api.Common.DTOs.Reporte;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    public interface IServicioGeneracionArchivoEstrategia
    {
        /// <summary>
        /// Generar Archivo
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        Task<byte[]> GenerarArchivo(List<GenerarReporteDTO> datos, Periodo periodo);
        /// <summary>
        /// Obtener Nombre del Archivo
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        string ObtenerNombreArchivo(GenerarReporteDTO datos);
        
        /// <summary>
        /// Obtener descripción del Reporte
        /// </summary>
        /// <returns></returns>
        string ObtenerDescripcionReporte();

        /// <summary>
        /// Obtener descripción de Tema del Reporte para Correo Electronico
        /// </summary>
        /// <returns></returns>
        string ObtenerDescripcionTemaMensaje();
    }
}
