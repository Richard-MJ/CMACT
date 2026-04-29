namespace PagareElectronico.Application.DTOs.Requests
{
    /// <summary>
    /// Representa la solicitud interna para revertir la cancelación de uno o varios pagarés.
    /// </summary>
    public sealed class DtoSolicitudRevertirCancelacionPagare
    {
        /// <summary>
        /// Obtiene o establece la lista de pagarés cuya cancelación será revertida.
        /// </summary>
        public List<DtoItemReversionCancelacionPagareSolicitud> Pagares { get; set; } = new();
    }

    /// <summary>
    /// Representa un pagaré cuya cancelación será revertida.
    /// </summary>
    public sealed class DtoItemReversionCancelacionPagareSolicitud : DtoPagareIdentificadorSolicitud
    {
    }
}