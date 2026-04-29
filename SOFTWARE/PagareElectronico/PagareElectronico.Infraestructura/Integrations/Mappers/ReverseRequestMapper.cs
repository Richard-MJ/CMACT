using Microsoft.Extensions.Options;
using PagareElectronico.Application.DTOs.Requests;
using PagareElectronico.Infrastructure.Configuration;
using PagareElectronico.Infrastructure.Integrations.Cavali.Contracts.Requests;

namespace PagareElectronico.Infrastructure.Integrations.Cavali.Mappers
{
    /// <summary>
    /// Mapea la solicitud interna de reversión al contrato reverse de CAVALI.
    /// </summary>
    public sealed class ReverseRequestMapper
    {
        private readonly CavaliApiOptions _options;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ReverseRequestMapper"/>.
        /// </summary>
        /// <param name="options">Opciones de configuración de CAVALI.</param>
        public ReverseRequestMapper(IOptions<CavaliApiOptions> options)
        {
            _options = options.Value;
        }

        /// <summary>
        /// Mapea la solicitud interna al request externo de CAVALI.
        /// </summary>
        /// <param name="source">Solicitud interna.</param>
        /// <returns>Solicitud externa para CAVALI.</returns>
        public ReverseRequest Map(DtoSolicitudRevertirCancelacionPagare source)
        {
            return new ReverseRequest
            {
                ParticipantCode = _options.ParticipantCode,
                ReversePromissoryNoteData = source.Pagares
                    .Select(x => new ReversePromissoryNoteDataItem
                    {
                        PromissoryNoteKey = CavaliMapperHelper
                            .MapearLlaveComunPagare(x, _options.BankCode, _options.ProductCode)
                    })
                    .ToList()
            };
        }
    }
}