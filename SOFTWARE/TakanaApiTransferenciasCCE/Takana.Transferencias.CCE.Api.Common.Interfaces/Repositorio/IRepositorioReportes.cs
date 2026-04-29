using Takana.Transferencias.CCE.Api.Common.DTOs.Reporte;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    public interface IRepositorioReportes
    {
        /// <summary>
        /// Procedimiento almancenado que obtiene datos de mantenimiento del Reporte Anexo 14
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        List<ReporteMantenimientoDTO> ObtenerDatosReporteMantenimiento(string anio, string mes);
        /// <summary>
        /// Procedimiento almancenado que obtiene datos de Incidentes del Reporte Anexo 15
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        List<ReporteIncidenteDTO> ObtenerDatosReporteIncidente(string anio, string mes);
        /// <summary>
        /// Procedimiento almancenado que obtiene datos de Objeto de tiempo de Recuperación del Reporte Anexo 16
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        List<ReporteObjetivoTiempoRecuperacionDTO> ObtenerDatosReporteObjetoTiempoRecuperacion(string anio, string mes);
        /// <summary>
        /// Procedimiento almancenado que obtiene datos de Calidad de servicio de no disponibilidad Reporte Anexo 17
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <param name="dia"></param>
        /// <param name="periodo"></param>
        /// <param name="frecuencia"></param>
        /// <returns></returns>
        ReporteICDNoDisponibilidadDTO ObtenerDatosCalidadServicioNoDisponibilidad(string anio, string mes, string dia, Periodo periodo, string frecuencia);
        /// <summary>
        /// Procedimiento almancenado que obtiene datos de Calidad de servicio de efectividad de busqueda de alias Reporte Anexo 17
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <param name="dia"></param>
        /// <param name="periodo"></param>
        /// <param name="frecuencia"></param>
        /// <returns></returns>
        ReporteICDEfectividadBusquedaAliasDTO ObtenerDatosCalidadServicioEfectividadBusquedaAlias(string anio, string mes, string dia, Periodo periodo, string frecuencia);
        /// <summary>
        /// Procedimiento almancenado que obtiene datos de Calidad de servicio de efectividad de Transferencias Reporte Anexo 17
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <param name="dia"></param>
        /// <param name="periodo"></param>
        /// <param name="frecuencia"></param>
        /// <returns></returns>
        ReporteICDEfectividadTransferenciasDTO ObtenerDatosCalidadServicioEfectividadTransferencias(string anio, string mes, string dia, Periodo periodo, string frecuencia);
        /// <summary>
        /// Procedimiento almancenado que obtiene datos de Calidad de servicio de rendimiento de busqueda de alias Reporte Anexo 17
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <param name="dia"></param>
        /// <param name="periodo"></param>
        /// <param name="frecuencia"></param>
        /// <returns></returns>
        List<ReporteICDRendimientoDTO> ObtenerDatosCalidadServicioRendimientoBusquedaAlias(string anio, string mes, string dia, Periodo periodo, string frecuencia);
        /// <summary>
        /// Procedimiento almancenado que obtiene datos de Calidad de servicio de rendimiento de transferencias Reporte Anexo 17
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <param name="dia"></param>
        /// <param name="periodo"></param>
        /// <param name="frecuencia"></param>
        /// <returns></returns>
        List<ReporteICDRendimientoDTO> ObtenerDatosCalidadServicioRendimientoTransferencias(string anio, string mes, string dia, Periodo periodo, string frecuencia);
        /// <summary>
        /// Procedimiento almancenado que obtiene datos de Resumen de Consultas del Reporte Anexo 19
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <param name="dia"></param>
        /// <param name="periodo"></param>
        /// <returns></returns>
        ReporteResumenConsultaDTO ObtenerDatosResumenConsultas(string anio, string mes, string dia, Periodo periodo);
        /// <summary>
        /// Procedimiento almancenado que obtiene datos de Resumen de Consultas del Reporte Anexo 19
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <param name="dia"></param>
        /// <param name="periodo"></param>
        /// <returns></returns>
        ReporteResumenTransaccionDTO ObtenerDatosResumenTransacciones(string anio, string mes, string dia, Periodo periodo);
        /// <summary>
        /// Procedimiento almancenado que obtiene datos de Variación de usuarios y montos de transferencias del Reporte Anexo 20
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        ReporteVariacionUsuarioMontoTransferenciasDTO ObtenerDatosReporteVariacionUsuarioMontoTransferencias(string anio, string mes);

        /// <summary>
        /// Funcion que verifica si es un dia Habil
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        bool VerificarSiEsDiaHabil(DateTime fecha);
    }
}
