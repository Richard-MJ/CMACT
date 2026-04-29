namespace Takana.Transferencias.CCE.Api.Common.DTOs.Monitoreo
{
    /// <summary>
    /// Clase que representa los datos de la audiencia.
    /// </summary>
    public record class MonitoreoEventosReporteDTO
    {
        /// <summary>
        /// Número correlativo del evento.
        /// </summary>
        public int Nro { get; set; }
        /// <summary>
        /// Nombre del servicio monitoreado.
        /// </summary>
        public string Servicio { get; set; } = null!;
        /// <summary>
        /// Descripción del servicio o del evento monitoreado.
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Tipo de evento registrado (por ejemplo: Error, Advertencia, Información).
        /// </summary>
        public string TipoEvento { get; set; } = null!;
        /// <summary>
        /// Detalle específico del evento ocurrido.
        /// </summary>
        public string DetalleEvento { get; set; } = null!;
        /// <summary>
        /// Código de estado devuelto por el servicio (puede ser nulo).
        /// </summary>
        public string? CodigoEstado { get; set; }
        /// <summary>
        /// Tiempo que tomó el servicio en responder.
        /// </summary>
        public string TiempoRespuesta { get; set; } = null!;
        /// <summary>
        /// Fecha y hora en que inició el evento o la operación.
        /// </summary>
        public DateTime FechaHoraInicio { get; set; }
        /// <summary>
        /// Fecha y hora en que finalizó el evento o la operación.
        /// </summary>
        public DateTime FechaHoraFin { get; set; }
    }
}
