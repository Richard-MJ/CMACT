namespace Takana.Transferencias.CCE.Api.Common.DTOs.Reporte
{
    public class ReporteGeneralDTO
    {
        #region Propiedades
        /// <summary>
        /// Fecha de inicio.
        /// </summary>
        public string FechaInicio { get; set; }
        /// <summary>
        /// Hora de inicio.
        /// </summary>
        public string HoraInicio { get; set; }
        /// <summary>
        /// Fecha de finalización.
        /// </summary>
        public string FechaFin { get; set; }
        /// <summary>
        /// Hora de finalización.
        /// </summary>
        public string HoraFin { get; set; }
        /// <summary>
        /// Indicador de la indisponibilidad del sistema durante el mantenimiento.
        /// </summary>
        public string Indisponibilidad { get; set; }
        /// <summary>
        /// Indicador que refleja si el mantenimiento fue exitoso y si el sistema ha pasado a producción.
        /// </summary>
        public string PaseProduccion { get; set; }
        #endregion
    }
}
