using Takana.Transferencias.CCE.Api.Common.DTOs.Reporte;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad;

namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    public interface IServicioAplicacionReporte : IServicioBase
    {
        /// <summary>
        /// Metodo que invoca al servicio de generar Reporte de Interoperabilidad: Valida y Generar Archivo
        /// </summary>
        /// <param name="datos"></param>
        /// <param name="esReporteIndividual"></param>
        /// <param name="configSFTP"></param>
        /// <param name="configCanalElectronicoWorkstation"></param>
        /// <returns></returns>
        Task<List<int>> GenerarArchivoReporteManual(
            GenerarReporteDTO datos,
            bool esReporteIndividual,
            IConfiguracionSFTP configSFTP,
            IConfiguracionSFTP configCanalElectronicoWorkstation);

        /// <summary>
        /// Metodo que invoca al servicio de generar Reporte de Interoperabilidad: Generar Archivo
        /// </summary>
        /// <param name="datos"></param>
        /// <param name="esReporteIndividual"></param>
        /// <param name="configSFTP"></param>
        /// <param name="configCanalElectronicoWorkstation"></param>
        /// <returns></returns>
        Task<int> GenerarArchivoReporte(
            List<GenerarReporteDTO> datos, 
            bool esReporteIndividual,
            DateTime fechaReporte,
            IConfiguracionSFTP configSFTP, 
            IConfiguracionSFTP configCanalElectronicoWorkstation);
        /// <summary>
        /// Método que genera el archivo de directorio de clientes seleccionados
        /// </summary>
        /// <param name="datosArchivo"></param>
        /// <param name="configSFTP"></param>
        /// <returns></returns>
        Task<int> GenerarArchivoDirectorioClienteSeleccionado(
            ArchivoDirectorioClienteDTO datosArchivo, IConfiguracionSFTP configSFTP);
        /// <summary>
        /// Método que verifica la respuesta del Directorio de la CCE
        /// </summary>
        /// <param name="id"></param>
        /// <param name="configSFTP"></param>
        /// <returns></returns>
        /// <exception cref="ValidacionException"></exception>
        Task<bool> VerificarRespuestaDirectorioCCE(int id, IConfiguracionSFTP configSFTP);
        /// <summary>
        /// Método que sube el archivo al servicio SFTP de la CCE
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enviarCorreo"></param>
        Task<bool> SubirArchivoSFTP(int id, bool enviarCorreo, IConfiguracionSFTP configSFTP);
        /// <summary>
        /// Método que envia un grupo de reportes al correo electronico
        /// </summary>
        /// <param name="fechaSistema"></param>
        /// <param name="tiposReportes"></param>
        /// <param name="frecuencia"></param>
        /// <param name="configSFTP"></param>
        /// <param name="configCanalElectronicoWorkstation"></param>
        /// <returns></returns>
        Task SubirArchivosYEnviarCorreosPorGrupo(
            DateTime fechaSistema, 
            List<int> tiposReportes, 
            string frecuencia,
            IConfiguracionSFTP configSFTP,
            IConfiguracionSFTP configCanalElectronicoWorkstation);

        /// <summary>
        /// Método que verifica si es el primer dia habil del Mes
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns>Retorna True si es Habil del Mes</returns>
        bool EsPrimerDiaHabilDelMes(DateTime fecha);
        /// <summary>
        /// Método que verifica si ya se envio el reporte mensual
        /// </summary>
        /// <param name="fechaSistema"></param>
        /// <returns>Retorna True si Existe</returns>
        bool SeEnvioReporteMensual(DateTime fechaSistema);
        /// <summary>
        /// Método que obtiene la fechas para generar reportes diarios
        /// </summary>
        /// <param name="fechaSistema"></param>
        /// <returns>Retorna True si Existe</returns>
        List<DateTime> ObtenerFechasGenerarReporteDiario(DateTime fechaSistema);
    }
}
