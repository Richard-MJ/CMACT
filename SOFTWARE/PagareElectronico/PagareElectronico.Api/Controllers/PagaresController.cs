using Microsoft.AspNetCore.Mvc;
using PagareElectronico.Application.DTOs.Requests;
using PagareElectronico.Application.DTOs.Responses;
using PagareElectronico.Application.Abstractions.Services;

namespace PagareElectronico.Api.Controllers
{
    /// <summary>
    /// Expone los endpoints HTTP para la gestión de pagarés.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public sealed class PagaresController : ControllerBase
    {
        /// <summary>
        /// Servicio de aplicación encargado de la orquestación de pagarés.
        /// </summary>
        private readonly IPagareService _servicePagare;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PagaresController"/>.
        /// </summary>
        /// <param name="servicePagare">Servicio de aplicación.</param>
        public PagaresController(IPagareService servicePagare)
        {
            _servicePagare = servicePagare;
        }

        /// <summary>
        /// Ejecuta el flujo de registro y anotación del pagaré hacia CAVALI.
        /// </summary>
        /// <param name="request">Modelo interno recibido desde el consumidor.</param>
        /// <param name="cancellationToken">Token de cancelación de la operación.</param>
        /// <returns>Resultado HTTP del registro y anotación.</returns>
        [HttpPost("registro-anotacion")]
        [ProducesResponseType(typeof(GatewayResponse<DtoRespuestaRegistroPagare>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegistrarAnotacion(
            [FromBody] DtoSolicitudRegistrarAnotacionPagare request,
            CancellationToken cancellationToken)
        {
            var resultado = await _servicePagare.RegistrarAnotacionAsync(request, cancellationToken);
            return Ok(resultado);
        }

        /// <summary>
        /// Ejecuta el flujo de cancelación de pagarés hacia CAVALI.
        /// </summary>
        /// <param name="request">Modelo interno recibido desde el consumidor.</param>
        /// <param name="cancellationToken">Token de cancelación de la operación.</param>
        /// <returns>Resultado HTTP de la cancelación.</returns>
        [HttpPost("cancelacion")]
        [ProducesResponseType(typeof(GatewayResponse<DtoRespuestaProcesamientoMasivo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Cancelar(
            [FromBody] DtoSolicitudCancelarPagare request,
            CancellationToken cancellationToken)
        {
            var resultado = await _servicePagare.CancelarAsync(request, cancellationToken);
            return Ok(resultado);
        }

        /// <summary>
        /// Ejecuta el flujo de retiro de pagarés hacia CAVALI.
        /// </summary>
        /// <param name="request">Modelo interno recibido desde el consumidor.</param>
        /// <param name="cancellationToken">Token de cancelación de la operación.</param>
        /// <returns>Resultado HTTP del retiro.</returns>
        [HttpPost("eliminar")]
        [ProducesResponseType(typeof(GatewayResponse<DtoRespuestaProcesamientoMasivo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Eliminar(
            [FromBody] DtoSolicitudEliminarPagare request,
            CancellationToken cancellationToken)
        {
            var resultado = await _servicePagare.EliminarAsync(request, cancellationToken);
            return Ok(resultado);
        }

        /// <summary>
        /// Ejecuta el flujo de reversión de cancelación de pagarés hacia CAVALI.
        /// </summary>
        /// <param name="request">Modelo interno recibido desde el consumidor.</param>
        /// <param name="cancellationToken">Token de cancelación de la operación.</param>
        /// <returns>Resultado HTTP de la reversión.</returns>
        [HttpPost("reversion-cancelacion")]
        [ProducesResponseType(typeof(GatewayResponse<DtoRespuestaProcesamientoMasivo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RevertirCancelacion(
            [FromBody] DtoSolicitudRevertirCancelacionPagare request,
            CancellationToken cancellationToken)
        {
            var resultado = await _servicePagare.RevertirCancelacionAsync(request, cancellationToken);
            return Ok(resultado);
        }
    }
}