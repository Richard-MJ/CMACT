using System.Globalization;
using Microsoft.Extensions.Options;
using PagareElectronico.Application.DTOs.Requests;
using PagareElectronico.Infrastructure.Configuration;
using PagareElectronico.Infrastructure.Integrations.Cavali.Contracts.Requests;

namespace PagareElectronico.Infrastructure.Integrations.Cavali.Mappers
{
    /// <summary>
    /// Mapea la solicitud interna de cancelación al contrato cancellation de CAVALI.
    /// </summary>
    public sealed class CancellationRequestMapper
    {
        private readonly CavaliApiOptions _options;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CancellationRequestMapper"/>.
        /// </summary>
        /// <param name="options">Opciones de configuración de CAVALI.</param>
        public CancellationRequestMapper(IOptions<CavaliApiOptions> options)
        {
            _options = options.Value;
        }

        /// <summary>
        /// Mapea la solicitud interna al request externo de CAVALI.
        /// </summary>
        /// <param name="source">Solicitud interna.</param>
        /// <returns>Solicitud externa para CAVALI.</returns>
        public CancellationRequest Map(DtoSolicitudCancelarPagare source)
        {
            return new CancellationRequest
            {
                ParticipantCode = _options.ParticipantCode,
                CancellationPromissoryNoteData = source.Pagares
                    .Select(x => new CancellationPromissoryNoteDataItem
                    {
                        PromissoryNoteKey = CavaliMapperHelper.MapearLlaveComunPagare(x, _options.BankCode, _options.ProductCode),
                        CancellationDate = x.FechaCancelacion.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                    })
                    .ToList()
            };
        }
    }
}