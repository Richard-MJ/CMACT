namespace Takana.Transferencias.CCE.Api.Common.DTOs.Reporte
{
    public class ReporteObjetivoTiempoRecuperacionDTO
    {
        #region Propiedades
        /// <summary>
        /// Tipo de ICD para Incidentes
        /// </summary>
        public string Tipo { get; set; }
        /// <summary>
        /// Cantidad de incidencias asociadas a un tipo
        /// </summary>
        public int CantidadIncidencias { get; set; }
        /// <summary>
        /// Tiempo del RTO
        /// </summary>
        public int TiempoRTO { get; set; }
        /// <summary>
        /// Cantidad de incidencias que superaron el tiempo del RTO
        /// </summary>
        public int CantidadIncidenciasRTO { get; set; }
        /// <summary>
        /// Resultado del ICD 
        /// </summary>
        public decimal ICDResultado
        {
            get
            {
                if (CantidadIncidencias <= 0) return 0M;

                return Math.Round(((decimal)(CantidadIncidencias - CantidadIncidenciasRTO)/ CantidadIncidencias) * 100, 2);
            }
        }
        #endregion
    }
}
