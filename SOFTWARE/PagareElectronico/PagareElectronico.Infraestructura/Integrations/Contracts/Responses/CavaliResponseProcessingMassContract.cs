using System.Text.Json.Serialization;

namespace PagareElectronico.Infrastructure.Integrations.Cavali.Contracts.Responses
{
    /// <summary>
    /// Representa la respuesta externa de CAVALI para operaciones masivas.
    /// </summary>
    public sealed class CavaliResponseProcessingMassContract
    {
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

        /// <summary>
        /// Obtiene o establece la cantidad de registros procesados exitosamente.
        /// </summary>
        [JsonPropertyName("successful")]
        public int Successful { get; set; }

        /// <summary>
        /// Obtiene o establece la lista de registros fallidos.
        /// </summary>
        [JsonPropertyName("failed")]
        public List<CavaliRecordFailedContract>? Failed { get; set; }
    }

    /// <summary>
    /// Representa un registro fallido dentro de la respuesta masiva de CAVALI.
    /// </summary>
    public sealed class CavaliRecordFailedContract
    {
        /// <summary>
        /// Obtiene o establece la llave del pagaré fallido.
        /// </summary>
        [JsonPropertyName("promissoryNoteKey")]
        public CavaliKeyPagareFailedContract? PromissoryNoteKey { get; set; }

        /// <summary>
        /// Obtiene o establece el código de resultado del registro fallido.
        /// </summary>
        [JsonPropertyName("resultCode")]
        public int? ResultCode { get; set; }

        /// <summary>
        /// Obtiene o establece el mensaje descriptivo del registro fallido.
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }

    /// <summary>
    /// Representa la llave del pagaré fallido dentro de una respuesta masiva de CAVALI.
    /// </summary>
    public sealed class CavaliKeyPagareFailedContract
    {
        /// <summary>
        /// Obtiene o establece el código de banca.
        /// </summary>
        [JsonPropertyName("banking")]
        public int Banking { get; set; }

        /// <summary>
        /// Obtiene o establece el código de producto.
        /// </summary>
        [JsonPropertyName("product")]
        public int Product { get; set; }

        /// <summary>
        /// Obtiene o establece el número de crédito.
        /// </summary>
        [JsonPropertyName("creditNumber")]
        public string? CreditNumber { get; set; }

        /// <summary>
        /// Obtiene o establece el código único.
        /// </summary>
        [JsonPropertyName("uniqueCode")]
        public string? UniqueCode { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha de cancelación, cuando exista.
        /// </summary>
        [JsonPropertyName("cancellationDate")]
        public string? CancellationDate { get; set; }
    }
}