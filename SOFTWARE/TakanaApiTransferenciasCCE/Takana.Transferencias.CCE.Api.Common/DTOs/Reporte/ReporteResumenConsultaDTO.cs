namespace Takana.Transferencias.CCE.Api.Common.DTOs.Reporte
{
    public class ReporteResumenConsultaDTO
    {
        #region Propiedades
        /// <summary>
        /// Cantidad de consultas totales del periodo.
        /// </summary>
        public int CantidadConsultasTotales { get; set; }
        /// <summary>
        /// Cantidad de consultas totales del periodo que fueron exitosas.
        /// </summary>
        public int CantidadConsultasExitosas { get; set; }
        /// <summary>
        /// Cantidad de consultas totales del periodo que no fueron exitosas.
        /// </summary>
        public int CantidadConsultasErradas { get; set; }
        /// <summary>
        /// Cantidad de consultas totales del periodo que se originaron por medio de códigos QR.
        /// </summary>
        public int CantidadConsultasQR { get; set; }
        #endregion
    }
}
