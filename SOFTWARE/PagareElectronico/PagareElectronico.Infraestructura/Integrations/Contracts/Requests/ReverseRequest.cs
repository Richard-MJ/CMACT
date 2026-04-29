using PagareElectronico.Infrastructure.Integrations.Cavali.Contracts.Requests.Common;

namespace PagareElectronico.Infrastructure.Integrations.Cavali.Contracts.Requests
{
    /// <summary>
    /// Representa la solicitud que CAVALI espera para reverse.
    /// </summary>
    public sealed class ReverseRequest
    {
        /// <summary>
        /// Obtiene o establece el código de participante.
        /// </summary>
        public int ParticipantCode { get; set; }

        /// <summary>
        /// Obtiene o establece la lista de pagarés a revertir.
        /// </summary>
        public List<ReversePromissoryNoteDataItem> ReversePromissoryNoteData { get; set; } = new();
    }

    /// <summary>
    /// Representa un item de pagaré para reversión.
    /// </summary>
    public sealed class ReversePromissoryNoteDataItem
    {
        /// <summary>
        /// Obtiene o establece la llave del pagaré.
        /// </summary>
        public CommonPromissoryNoteKey PromissoryNoteKey { get; set; } = new();
    }
}