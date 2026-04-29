using Microsoft.Extensions.Options;
using PagareElectronico.Application.DTOs.Requests;
using PagareElectronico.Infrastructure.Configuration;
using PagareElectronico.Infrastructure.Integrations.Cavali.Contracts.Requests;

namespace PagareElectronico.Infrastructure.Integrations.Cavali.Mappers
{
    /// <summary>
    /// Mapea la solicitud interna de retiro al contrato deletion de CAVALI.
    /// </summary>
    public sealed class DeletionRequestMapper
    {
        private readonly CavaliApiOptions _options;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DeletionRequestMapper"/>.
        /// </summary>
        /// <param name="options">Opciones de configuración de CAVALI.</param>
        public DeletionRequestMapper(IOptions<CavaliApiOptions> options)
        {
            _options = options.Value;
        }

        /// <summary>
        /// Mapea la solicitud interna al request externo de CAVALI.
        /// </summary>
        /// <param name="source">Solicitud interna.</param>
        /// <returns>Solicitud externa para CAVALI.</returns>
        public DeletionRequest Map(DtoSolicitudEliminarPagare source)
        {
            return new DeletionRequest
            {
                ParticipantCode = _options.ParticipantCode,
                PromissoryNoteKey = source.Pagares
                    .Select(x => CavaliMapperHelper
                        .MapearLlaveComunPagare(x, _options.BankCode, _options.ProductCode))
                    .ToList()
            };
        }
    }
}