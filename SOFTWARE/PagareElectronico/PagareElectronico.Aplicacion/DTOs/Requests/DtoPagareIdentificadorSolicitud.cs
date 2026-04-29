namespace PagareElectronico.Application.DTOs.Requests
{
    /// <summary>
    /// Representa los datos básicos de identificación de un pagaré.
    /// </summary>
    public class DtoPagareIdentificadorSolicitud
    {
        /// <summary>
        /// Obtiene o establece el código único del pagaré.
        /// </summary>
        public string CodigoUnico { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el número de crédito asociado al pagaré.
        /// </summary>
        public string NumeroCredito { get; set; } = string.Empty;
    }
}