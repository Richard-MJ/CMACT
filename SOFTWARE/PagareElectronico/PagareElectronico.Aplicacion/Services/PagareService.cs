using PagareElectronico.Aplicacion.Helper;
using PagareElectronico.Application.DTOs.Requests;
using PagareElectronico.Application.DTOs.Responses;
using PagareElectronico.Application.Abstractions.Services;
using PagareElectronico.Application.Abstractions.Integrations;

namespace PagareElectronico.Application.Services
{
    /// <summary>
    /// Implementa la lógica de aplicación para la gestión de pagarés.
    /// </summary>
    public sealed class PagareService : IPagareService
    {
        /// <summary>
        /// Gateway de integración hacia CAVALI.
        /// </summary>
        private readonly ICavaliPagareGateway _cavaliPagareGateway;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PagareService"/>.
        /// </summary>
        /// <param name="cavaliPagareGateway">Gateway de integración hacia CAVALI.</param>
        public PagareService(ICavaliPagareGateway cavaliPagareGateway)
        {
            _cavaliPagareGateway = cavaliPagareGateway;
        }

        /// <summary>
        /// Metodo que registra el pagare
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GatewayResponse<DtoRespuestaRegistroPagare>> RegistrarAnotacionAsync(
            DtoSolicitudRegistrarAnotacionPagare request,
            CancellationToken cancellationToken)
        {
            request.ValidarSolicitudRegistro();

            return await _cavaliPagareGateway.RegistrarAnotacionAsync(request, cancellationToken);
        }

        /// <summary>
        /// Metodo que cancela el pagare
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GatewayResponse<DtoRespuestaProcesamientoMasivo>> CancelarAsync(
            DtoSolicitudCancelarPagare request,
            CancellationToken cancellationToken)
        {
            request.ValidarSolicitudCancelacion();

            return await _cavaliPagareGateway.CancelarAsync(request, cancellationToken);
        }

        /// <summary>
        /// Metodo que retira el pagare
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GatewayResponse<DtoRespuestaProcesamientoMasivo>> EliminarAsync(
            DtoSolicitudEliminarPagare request,
            CancellationToken cancellationToken)
        {
            request.ValidarSolicitudRetiro();

            return await _cavaliPagareGateway.EliminarAsync(request, cancellationToken);
        }

        /// <summary>
        /// Metodo que revierte la cancelacion
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GatewayResponse<DtoRespuestaProcesamientoMasivo>> RevertirCancelacionAsync(
            DtoSolicitudRevertirCancelacionPagare request,
            CancellationToken cancellationToken)
        {
            request.ValidarSolicitudReversion();

            return await _cavaliPagareGateway.RevertirCancelacionAsync(request, cancellationToken);
        }
    }
}