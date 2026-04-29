using PagareElectronico.Infrastructure.Integrations.Cavali.Contracts.Requests.Common;

namespace PagareElectronico.Infrastructure.Integrations.Cavali.Contracts.Requests
{
    /// <summary>
    /// Representa la solicitud que CAVALI espera para deletion.
    /// </summary>
    public sealed class DeletionRequest
    {
        /// <summary>
        /// Obtiene o establece el código de participante.
        /// </summary>
        public int ParticipantCode { get; set; }

        /// <summary>
        /// Obtiene o establece la lista de llaves de pagarés a retirar.
        /// </summary>
        public List<CommonPromissoryNoteKey> PromissoryNoteKey { get; set; } = new();
    }
}