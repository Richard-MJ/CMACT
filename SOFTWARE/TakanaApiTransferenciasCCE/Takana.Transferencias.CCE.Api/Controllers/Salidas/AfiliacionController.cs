using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using Takana.Transferencias.CCE.Api.Servicio;
using Takana.Transferencias.CCE.Api.Atributos;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad;
using static Takana.Transferencias.CCE.Api.Common.SwaggerExamplesDTO;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad.DatosRegistroDirectorio;

namespace Takana.Transferencias.CCE.Api.Controllers.Salidas
{
    /// <summary>
    /// Clase que redirecciona los datos recibidor del canal hacia los servicios requeridos
    /// </summary>    
    [ApiController]
    [GenerarSesion]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}.0")]
    public class AfiliacionController : BaseController<AfiliacionController>
    {
        private readonly TareasProgramadasServices _servicioTareasProgramadas;
        private readonly IConfiguracionDirectorioSFTP _configDirectorioSFTP;
        private readonly IServicioAplicacionReporte _servicioAplicacionReporte;
        private readonly IServicioAplicacionAfiliacion _servicioAplicacionAfiliacion;

        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="configDirectorioSFTP"></param>
        /// <param name="servicioTareasProgramadas"></param>
        /// <param name="servicioAplicacionReporte"></param>
        /// <param name="servicioAplicacionAfiliacion"></param>
        public AfiliacionController(
            IBitacora<AfiliacionController> bitacora,
            IConfiguracionDirectorioSFTP configDirectorioSFTP,
            TareasProgramadasServices servicioTareasProgramadas,
            IServicioAplicacionReporte servicioAplicacionReporte,
            IServicioAplicacionAfiliacion servicioAplicacionAfiliacion) : base(bitacora)
        {
            _configDirectorioSFTP = configDirectorioSFTP;
            _servicioAplicacionReporte = servicioAplicacionReporte;
            _servicioTareasProgramadas = servicioTareasProgramadas;
            _servicioAplicacionAfiliacion = servicioAplicacionAfiliacion;
        }

        /// <summary>
        /// End point para la afiliacion al directorio CCE
        /// </summary>
        /// <param name="datosRegistro">Datos necesarios para la afiliacion.</param>
        /// <returns>Retorna datos de rspuesta de la afiliacion</returns>
        [HttpPost]
        [Route("afiliar_directorio")]
        [Authorize(Policy = "ModuloAppMovil")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones Directorio CCE" },
            Summary = "Afilia al cliente originante al directorio CCE",
            OperationId = "AfiliacionDirectorioCCE")]
        [SwaggerRequestExample(typeof(EntradaAfiliacionDirectorioDTO), typeof(EntradaAfiliacionDirectorioDTOExample))]
        [SwaggerResponseExample(200, typeof(RespuestaAfiliacionCCEDTOExample))]
        public async Task<ActionResult<RespuestaAfiliacionCCEDTO>> AfiliacionDirectorioCCE(
            [FromBody] EntradaAfiliacionDirectorioDTO datosRegistro)
        {
            _bitacora.Trace("Iniciando proceso de afiliación de Directorio de Interoperabilidad - CCE.");
            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionAfiliacion.AfiliacionDirectorioCCE(datosRegistro));
        }

        /// <summary>
        /// End point para la Desafiliar al directorio CCE
        /// </summary>
        /// <param name="datosRegistro">Datos necesarios para la afiliacion.</param>
        /// <returns>Retorna datos de rspuesta de la desafiliacion</returns>
        [HttpPost]
        [Route("desafiliar_directorio")]
        [Authorize(Policy = "TodosModulos")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones Directorio CCE" },
            Summary = "Desafiliar al cliente originante del directorio CCE",
            OperationId = "DesafiliacionDirectorioCCE")]
        [SwaggerRequestExample(typeof(EntradaAfiliacionDirectorioDTO), typeof(EntradaAfiliacionDirectorioExample))]
        [SwaggerResponseExample(200, typeof(RespuestaAfiliacionCCEExample))]
        public async Task<ActionResult<RespuestaAfiliacionCCEDTO>> DesafiliacionDirectorioCCE(
            [FromBody] EntradaAfiliacionDirectorioDTO datosRegistro)
        {
            _bitacora.Trace("Iniciando proceso de desafiliacion de Directorio de Interoperabilidad - CCE");
            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionAfiliacion.DesafiliacionDirectorioCCE(datosRegistro));
        }

        /// <summary>
        /// Metodo que invoca al servicio de generar archivo de Directorio de Interoperabilidad: Generar Archivo
        /// </summary>
        /// <returns>El Id si se genero correctamente el reporte</returns>
        [HttpPost]
        [Impersonalizacion]
        [Route("directorio-cce/generar-archivo")]
        [Authorize(Policy = "ModuloTesoreria")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Reporte Directorio CCE" },
            Summary = "Generar Archivo de Afiliaciones y Desafiliaciones de Interoperabilidad",
            OperationId = "DirectorioArchivo")]
        public async Task<ActionResult<int>> GenerarArchivo([FromBody] ArchivoDirectorioClienteDTO datos)
        {
            _bitacora.Trace("Iniciando proceso de Generación de Archivo de Directorio de Interoperabilidad");
            return await InvocarOperacionDesdeServicios(async () =>
            {
                var idReporte = await _servicioAplicacionReporte.GenerarArchivoDirectorioClienteSeleccionado(datos, _configDirectorioSFTP);
                _servicioTareasProgramadas.ProgramarTareaVerificarRepuestaDirectorioCCE(idReporte);
                return idReporte;
            });
        }

        /// <summary>
        /// Metodo que invoca al servicio de Subir Archivo SFTP
        /// </summary>
        [HttpPost]
        [Impersonalizacion]
        [Route("directorio-cce/subir-archivo-sftp")]
        [Authorize(Policy = "ModuloTesoreria")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Reporte Directorio CCE" },
            Summary = "Subir Archivo SFTP",
            OperationId = "DirectorioArchivo")]
        public async Task<ActionResult<bool>> SubirArchivoSFTP([FromBody] int id)
        {
            _bitacora.Trace("Iniciando proceso de Subir Archivo al Servicio de Directorio SFTP - CCE.");
            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionReporte.SubirArchivoSFTP(id, true, _configDirectorioSFTP));
        }

    }
}