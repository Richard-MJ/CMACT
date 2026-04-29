namespace PagareElectronico.Application.DTOs.Requests
{
    /// <summary>
    /// Representa la solicitud interna para retirar uno o varios pagarés.
    /// </summary>
    public sealed class DtoSolicitudEliminarPagare
    {
        /// <summary>
        /// Obtiene o establece la lista de pagarés a retirar.
        /// </summary>
        public List<DtoItemEliminaPagareSolicitud> Pagares { get; set; } = new();
    }

    /// <summary>
    /// Representa un pagaré a retirar.
    /// </summary>
    public sealed class DtoItemEliminaPagareSolicitud : DtoPagareIdentificadorSolicitud
    {
    }
}