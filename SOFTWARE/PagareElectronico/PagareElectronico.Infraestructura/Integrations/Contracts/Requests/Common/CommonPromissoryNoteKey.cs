namespace PagareElectronico.Infrastructure.Integrations.Cavali.Contracts.Requests.Common
{
    /// <summary>
    /// Representa la llave común de un pagaré para las operaciones de CAVALI.
    /// </summary>
    public sealed class CommonPromissoryNoteKey
    {
        /// <summary>
        /// Obtiene o establece el código de banca.
        /// </summary>
        public int Banking { get; set; }

        /// <summary>
        /// Obtiene o establece el código único.
        /// </summary>
        public string UniqueCode { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el código de producto.
        /// </summary>
        public int Product { get; set; }

        /// <summary>
        /// Obtiene o establece el número de crédito.
        /// </summary>
        public string CreditNumber { get; set; } = string.Empty;
    }
}