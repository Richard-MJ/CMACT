namespace Takana.Transferencias.CCE.Api.Common.DTOs.Reporte
{
    public class ReporteIncidenteDTO : ReporteGeneralDTO
    {
        #region Propiedades
        /// <summary>
        /// Identificador de incidente.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Tipo de incidente.
        /// </summary>
        public string Tipo { get; set; }
        /// <summary>
        /// Escenario del incidente.
        /// </summary>
        public string Escenario { get; set; }
        /// <summary>
        /// Indicador de si el incidente se solucionó (SI/NO).
        /// </summary>
        public string Solucionado { get; set; }
        /// <summary>
        /// Indicador de si el incidente superó el tiempo máximo de RTO (SI/NO).
        /// </summary>
        public string SuperoRTO { get; set; }
        /// <summary>
        /// Servicios impactados por el incidente (separados por comas).
        /// </summary>
        public string ServiciosImpactados { get; set; }
        /// <summary>
        /// Entidades o roles impactados por el incidente (separados por comas).
        /// </summary>
        public string EntidadesImpactadas { get; set; }
        /// <summary>
        /// Descripción del incidente.
        /// </summary>
        public string DescripcionIncidente { get; set; }
        /// <summary>
        /// Acciones ejecutadas para solucionar el incidente.
        /// </summary>
        public string AccionesEjecutadas { get; set; }
        /// <summary>
        /// Tiempo de indisponibilidad generado por el incidente (en minutos).
        /// </summary>
        public string TiempoIndisp { get; set; }
        /// <summary>
        /// Cantidad de pagos afectados por el incidente.
        /// </summary>
        public string CantidadPagosAfectados { get; set; }
        /// <summary>
        /// Monto del valor monetario afectado por el incidente.
        /// </summary>
        public decimal ValorPagosAfectados { get; set; }
        /// <summary>
        /// Lecciones aprendidas, propuestas de mejoras, entre otros.
        /// </summary>
        public string Recomendaciones { get; set; }
        /// <summary>
        /// Comentarios adicionales sobre el incidente.
        /// </summary>
        public string Comentarios { get; set; }

        #endregion
    }
}
