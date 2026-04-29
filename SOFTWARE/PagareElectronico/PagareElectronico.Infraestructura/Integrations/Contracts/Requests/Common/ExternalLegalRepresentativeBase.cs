namespace PagareElectronico.Infrastructure.Integrations.Cavali.Contracts.Requests.Common
{
    /// <summary>
    /// Representa la información común de un representante legal en contratos de CAVALI.
    /// </summary>
    public class ExternalLegalRepresentativeBase
    {
        /// <summary>
        /// Obtiene o establece el nombre del representante legal.
        /// </summary>
        public string LegalRepresentativeName { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el número de documento del representante legal.
        /// </summary>
        public string DocumentNumber { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el correo electrónico del representante legal.
        /// </summary>
        public string EmailLegalRepresentative { get; set; } = string.Empty;
    }
}