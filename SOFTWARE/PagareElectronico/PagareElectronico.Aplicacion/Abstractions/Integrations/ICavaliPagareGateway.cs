using PagareElectronico.Application.DTOs.Requests;
using PagareElectronico.Application.DTOs.Responses;

namespace PagareElectronico.Application.Abstractions.Integrations
{
    /// <summary>
    /// Define el contrato de integración hacia CAVALI para operaciones de pagarés.
    /// </summary>
    public interface ICavaliPagareGateway
    {
        /// <summary>
        /// Envía la solicitud de registro y anotación a CAVALI.
        /// </summary>
        /// <param name="request">Solicitud interna.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Respuesta del gateway.</returns>
        Task<GatewayResponse<DtoRespuestaRegistroPagare>> RegistrarAnotacionAsync(
            DtoSolicitudRegistrarAnotacionPagare request,
            CancellationToken cancellationToken);

        /// <summary>
        /// Envía la solicitud de cancelación a CAVALI.
        /// </summary>
        /// <param name="request">Solicitud interna.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Respuesta del gateway.</returns>
        Task<GatewayResponse<DtoRespuestaProcesamientoMasivo>> CancelarAsync(
            DtoSolicitudCancelarPagare request,
            CancellationToken cancellationToken);

        /// <summary>
        /// Envía la solicitud de retiro a CAVALI.
        /// </summary>
        /// <param name="request">Solicitud interna.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Respuesta del gateway.</returns>
        Task<GatewayResponse<DtoRespuestaProcesamientoMasivo>> EliminarAsync(
            DtoSolicitudEliminarPagare request,
            CancellationToken cancellationToken);

        /// <summary>
        /// Envía la solicitud de reversión de cancelación a CAVALI.
        /// </summary>
        /// <param name="request">Solicitud interna.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Respuesta del gateway.</returns>
        Task<GatewayResponse<DtoRespuestaProcesamientoMasivo>> RevertirCancelacionAsync(
            DtoSolicitudRevertirCancelacionPagare request,
            CancellationToken cancellationToken);
    }
}