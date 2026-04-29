using System.Text.Json.Serialization;

namespace PagareElectronico.Infrastructure.Integrations.Cavali.Contracts.Responses
{
    /// <summary>
    /// Representa la respuesta externa de CAVALI para el registro de pagaré.
    /// </summary>
    public sealed class CavaliResponseRecordContract
    {
        /// <summary>
        /// Obtiene o establece el identificador del proceso.
        /// </summary>
        [JsonPropertyName("idProceso")]
        public long? IdProceso { get; set; }

        /// <summary>
        /// Obtiene o establece el código de resultado.
        /// </summary>
        [JsonPropertyName("resultCode")]
        public int? ResultCode { get; set; }

        /// <summary>
        /// Obtiene o establece el mensaje descriptivo.
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}