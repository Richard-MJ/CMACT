using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Servicio;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.SignOnOff;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Common.DTOs.SignOnOff;
using static Takana.Transferencias.CCE.Api.Common.SwaggerExamplesDTO;

namespace Takana.Transferencias.CCE.Api.Controllers.Salidas
{
    /// <summary>
    /// Clase que redirecciona los datos recibidor del canal hacia los servicios requeridos
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}")]
    public class ConsultasController : BaseController<ConsultasController>
    {
        private readonly IServicioAplicacionTransferenciaSalida _servicioAplicacionTransferenciaSalida;
        private readonly TareasProgramadasServices _servicioTareasProgramadas;

        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="aplicacionTransferenciaCCEServicio"></param>
        public ConsultasController(
            IBitacora<ConsultasController> bitacora,
            IServicioAplicacionTransferenciaSalida aplicacionTransferenciaCCEServicio,
            TareasProgramadasServices tareasProgramadasServices
            ) : base(bitacora)
        {
            _servicioAplicacionTransferenciaSalida = aplicacionTransferenciaCCEServicio;
            _servicioTareasProgramadas = tareasProgramadasServices;
        }
        /// <summary>
        /// Metodo que invoca al servicio signon
        /// </summary>
        /// <returns>Respuesta de SignOn</returns>
        [HttpGet]
        [Route("echotest")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones consultas salidas" },
            Summary = "Prueba si hay conexion o no con la CCE.",
            OperationId = "EchoTest")]
        [SwaggerResponseExample(200, typeof(EchoTestRespuestaExample))]
        public async Task<ActionResult<RespuestaSalidaDTO<bool>>> EchoTest()
        {
            _bitacora.Trace("Iniciando proceso de Echotest por Transferencias Interbancarias Inmediatas - CCE.");

            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionTransferenciaSalida.EchoTest());
        }
        /// <summary>
        /// Metodo que invoca al servicio signon
        /// </summary>
        /// <returns>Respuesta de SignOn</returns>
        [HttpPost]
        [Route("signOn")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones consultas salidas" },
            Summary = "Manda las señal para que tengamos un estado de Encendido.",
            OperationId = "SignOn")]
        [SwaggerResponseExample(200, typeof(SignOnOffDTOExample))]
        public async Task<ActionResult<RespuestaSalidaDTO<SignOnOffDTO>>> SignOn()
        {
            _bitacora.Trace("Iniciando proceso de SignOn  por Transferencias Interbancarias Inmediatas - CCE.");

            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionTransferenciaSalida.SignOn());
        }

        /// <summary>
        /// Metodo que invoca al servicio signOff
        /// </summary>
        /// <returns>Respuesta de SignOff</returns>
        [HttpPost]
        [Route("signOff")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones consultas salidas" },
            Summary = "Manda las señal para que tengamos un estado de Apagado.",
            OperationId = "SignOn")]
        [SwaggerResponseExample(200, typeof(SignOnOffDTOExample))]
        public async Task<ActionResult<RespuestaSalidaDTO<SignOnOffDTO>>> SignOff()
        {
            _bitacora.Trace("Iniciando proceso de SignOff por Transferencias Interbancarias Inmediatas - CCE.");

            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionTransferenciaSalida.SignOff());
        }

        /// <summary>
        /// Metodo que invoca al servicio signOff para la obtencion de la lista de progrmacion sing on / sing off
        /// </summary>
        /// <returns>Lista Objeto Dto SingOnOffProgramadoDTO</returns>
        [HttpGet]
        [Route("obtener-programacion-sing")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones consultas salidas" },
            Summary = "Obtiene las lista de sing on off programadas.",
            OperationId = "obtener-programacion-sing")]
        [SwaggerResponseExample(200, typeof(SignOnOffDTOExample))]
        public async Task<ActionResult<IList<SingOnOffProgramadoDTO>>> ObtenerPromacionSignOff()
        {
            _bitacora.Trace("Iniciando proceso para la obtencion de Promacion de SingOnOff.");

            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionTransferenciaSalida.ObtenerProgamacionSignOnOff());
        }

        /// <summary>
        /// Metodo que gestionar periodo sing on off
        /// </summary>
        /// <returns>Respuesta mensaje de confirmacion</returns>
        [HttpPost]
        [Route("gestionar-programacion-sing")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones gestion salidas" },
            Summary = "Gestionar las tareas programadas sing on off.",
            OperationId = "gestionar-programacion-sing")]
        [SwaggerResponseExample(200, typeof(SignOnOffDTOExample))]
        public async Task<EntidadFinancieroInmediataPeriodo> GestionarSingOnOffProgramada([FromBody] SingOnOffProgramadoDTO singProgamado)
        {
            _bitacora.Trace("Iniciando proceso para la obtencion de Promacion de SingOnOff.");

            var resultado = await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionTransferenciaSalida.GestionarProgamacionSignOnOff(singProgamado));

            var resultadoActionResult = resultado.Result as OkObjectResult;
            var resultadoGestion = (EntidadFinancieroInmediataPeriodo)resultadoActionResult.Value;

                _servicioTareasProgramadas.ActualizarTareaPeriodoAsync(resultadoGestion);

            return resultadoGestion;
        }

        /// <summary>
        /// Metodo que gestionar periodo sing on off
        /// </summary>
        /// <returns>Respuesta mensaje de confirmacion</returns>
        [HttpPut]
        [Route("cambiar-estado-programacion-sing/{numeroPeriodo}/{estado}")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones gestion salidas" },
            Summary = "Gestionar las tareas programadas sing on off.",
            OperationId = "cambiar-estado-programacion-sing")]
        [SwaggerResponseExample(200, typeof(SignOnOffDTOExample))]
        public async Task<ActionResult<EntidadFinancieroInmediataPeriodo>> CambiarSingOnOffProgramada(long numeroPeriodo, string estado)
        {
            _bitacora.Trace("Iniciando proceso para la obtencion de Promacion de SingOnOff.");
            var resultado = await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionTransferenciaSalida.ActualizarEstadoPeriodoSignCmact(estado, numeroPeriodo));

            var resultadoActionResult = resultado.Result as OkObjectResult;
            var resultadoGestion = (EntidadFinancieroInmediataPeriodo)resultadoActionResult.Value;

            _servicioTareasProgramadas.ActualizarTareaPeriodoAsync(resultadoGestion);

            return resultadoGestion;
        }
    }
}