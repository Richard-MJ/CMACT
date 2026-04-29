using PagareElectronico.Infrastructure.Integrations.Cavali.Contracts.Requests.Common;

namespace PagareElectronico.Infrastructure.Integrations.Cavali.Contracts.Requests
{
    /// <summary>
    /// Representa la solicitud que CAVALI espera para cancellation.
    /// </summary>
    public sealed class CancellationRequest
    {
        /// <summary>
        /// Obtiene o establece el código de participante.
        /// </summary>
        public int ParticipantCode { get; set; }

        /// <summary>
        /// Obtiene o establece la lista de pagarés a cancelar.
        /// </summary>
        public List<CancellationPromissoryNoteDataItem> CancellationPromissoryNoteData { get; set; } = new();
    }

    /// <summary>
    /// Representa un item de pagaré para cancelación.
    /// </summary>
    public sealed class CancellationPromissoryNoteDataItem
    {
        /// <summary>
        /// Obtiene o establece la llave del pagaré.
        /// </summary>
        public CommonPromissoryNoteKey PromissoryNoteKey { get; set; } = new();

        /// <summary>
        /// Obtiene o establece la fecha de cancelación en formato yyyy-MM-dd.
        /// </summary>
        public string CancellationDate { get; set; } = string.Empty;
    }
}