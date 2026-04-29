using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using Takana.Transferencias.CCE.Api.Atributos;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using Takana.Transferencias.CCE.Api.Common.DTOs.Reporte;
using static Takana.Transferencias.CCE.Api.Common.SwaggerExamplesDTO;

namespace Takana.Transferencias.CCE.Api.Controllers.Reporte
{
    /// <summary>
    /// Clase que redirecciona los datos recibidoS del canal hacia los servicios requeridos
    /// </summary>
    [ApiController]
    [GenerarSesion]
    [Impersonalizacion]
    [ApiVersion("1.0")]
    [Authorize(Policy = "ModuloTesoreria")]
    [Route("api/reportes-cce/v{version:apiVersion}")]
    public class ReportesController : BaseController<ReportesController>
    {
        private readonly IConfiguracionReporteSFTP _configReporteSFTP;
        private readonly IServicioAplicacionReporte _servicioAplicacionReporte;
        private readonly IConfiguracionCanalElectronicoWorkstation _configCanalElectronicoWorkstation;

        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="configReporteSFTP"></param>
        /// <param name="servicioAplicacionReporte"></param>
        /// <param name="configCanalElectronicoSFTP"></param>
        public ReportesController(
            IBitacora<ReportesController> bitacora,
            IConfiguracionReporteSFTP configReporteSFTP,
            IServicioAplicacionReporte servicioAplicacionReporte,
            IConfiguracionCanalElectronicoWorkstation configCanalElectronicoSFTP) : base(bitacora)
        {
            _configReporteSFTP = configReporteSFTP;
            _servicioAplicacionReporte = servicioAplicacionReporte;
            _configCanalElectronicoWorkstation = configCanalElectronicoSFTP;
        }

        /// <summary>
        /// Metodo que invoca al servicio de generar Reporte de Interoperabilidad: Generar Archivo
        /// </summary>
        /// <returns>El Id si se genero correctamente el reporte</returns>
        [HttpPost]
        [Route("generar-archivo")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Reportes de Interoperabilidad Circular-009-BCRP" },
            Summary = "Reporte de Interoperabilidad: Generar Archivo",
            OperationId = "ReporteArchivo")]
        [SwaggerRequestExample(typeof(GenerarReporteDTO), typeof(GenerarReporteDTOExample))]
        public async Task<ActionResult<List<int>>> GenerarArchivo([FromBody] GenerarReporteDTO datos)
        {
            _bitacora.Trace("Iniciando proceso de Generación Reporte de Interoperabilidad: Generar Archivo - CCE.");
            return await InvocarOperacionDesdeServicios(async () => await 
                _servicioAplicacionReporte.GenerarArchivoReporteManual(datos, true, _configReporteSFTP, _configCanalElectronicoWorkstation));
        }

        /// <summary>
        /// Metodo que invoca al servicio de Subir Archivo SFTP
        /// </summary>
        [HttpPost]
        [Route("subir-archivo-sftp")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Reportes de Interoperabilidad Circular-009-BCRP" },
            Summary = "Subir Archivo SFTP",
            OperationId = "ReporteArchivo")]
        public async Task<ActionResult<bool>> SubirArchivoSFTP([FromBody] int id)
        {
            _bitacora.Trace("Iniciando proceso de Subir Archivo al Servicio de Reporte SFTP - CCE.");
            return await InvocarOperacionDesdeServicios(async () => await 
                _servicioAplicacionReporte.SubirArchivoSFTP(id, true, _configReporteSFTP));
        }
    }
}
