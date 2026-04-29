namespace Takana.Transferencias.CCE.Api.Common.DTOs.Monitoreo
{
    /// <summary>
    /// Clase que representa los datos de la audiencia.
    /// </summary>
    public record class MonitoreoDTO
    {
        #region Propiedades
        /// <summary>
        /// Fecha de inicio de la consulta
        /// </summary>
        public string startDate { get; set; }
        /// <summary>
        /// Fecha de fin de la consulta
        /// </summary>
        public string endDate { get; set; }
        #endregion
    }
}
