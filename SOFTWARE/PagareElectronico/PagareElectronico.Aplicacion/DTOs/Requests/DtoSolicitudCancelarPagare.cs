namespace PagareElectronico.Application.DTOs.Requests
{
    /// <summary>
    /// Representa la solicitud interna para cancelar uno o varios pagarés.
    /// </summary>
    public sealed class DtoSolicitudCancelarPagare
    {
        /// <summary>
        /// Obtiene o establece la lista de pagarés a cancelar.
        /// </summary>
        public List<DtoItemCancelacionPagareSolicitud> Pagares { get; set; } = new();
    }

    /// <summary>
    /// Representa un pagaré a cancelar.
    /// </summary>
    public sealed class DtoItemCancelacionPagareSolicitud : DtoPagareIdentificadorSolicitud
    {
        /// <summary>
        /// Obtiene o establece la fecha de cancelación del pagaré.
        /// </summary>
        public DateOnly FechaCancelacion { get; set; }
    }
}