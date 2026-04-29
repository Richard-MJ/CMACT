using PagareElectronico.Application.DTOs.Requests;
using PagareElectronico.Application.DTOs.Responses;

namespace PagareElectronico.Application.Abstractions.Services
{
    /// <summary>
    /// Define las operaciones de aplicación para la gestión de pagarés.
    /// </summary>
    public interface IPagareService
    {
        /// <summary>
        /// Ejecuta el flujo de registro y anotación de un pagaré.
        /// </summary>
        /// <param name="request">Solicitud interna de registro y anotación.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Respuesta del gateway con el resultado del registro.</returns>
        Task<GatewayResponse<DtoRespuestaRegistroPagare>> RegistrarAnotacionAsync(
            DtoSolicitudRegistrarAnotacionPagare request,
            CancellationToken cancellationToken);

        /// <summary>
        /// Ejecuta el flujo de cancelación de uno o varios pagarés.
        /// </summary>
        /// <param name="request">Solicitud interna de cancelación.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Respuesta del gateway con el resultado del procesamiento masivo.</returns>
        Task<GatewayResponse<DtoRespuestaProcesamientoMasivo>> CancelarAsync(
            DtoSolicitudCancelarPagare request,
            CancellationToken cancellationToken);

        /// <summary>
        /// Ejecuta el flujo de retiro de uno o varios pagarés.
        /// </summary>
        /// <param name="request">Solicitud interna de retiro.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Respuesta del gateway con el resultado del procesamiento masivo.</returns>
        Task<GatewayResponse<DtoRespuestaProcesamientoMasivo>> EliminarAsync(
            DtoSolicitudEliminarPagare request,
            CancellationToken cancellationToken);

        /// <summary>
        /// Ejecuta el flujo de reversión de cancelación de uno o varios pagarés.
        /// </summary>
        /// <param name="request">Solicitud interna de reversión.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Respuesta del gateway con el resultado del procesamiento masivo.</returns>
        Task<GatewayResponse<DtoRespuestaProcesamientoMasivo>> RevertirCancelacionAsync(
            DtoSolicitudRevertirCancelacionPagare request,
            CancellationToken cancellationToken);
    }
}