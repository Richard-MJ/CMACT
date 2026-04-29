namespace Takana.Transferencias.CCE.Api.Common.DTOs.Reporte
{
    public class ReporteICDEfectividadBusquedaAliasDTO
    {
        #region Propiedades
        /// <summary>
        /// Cantidad de intentos totales de consultas en el periodo reportado.
        /// </summary>
        public int CantidadIntentosTotalesConsulta { get; set; }
        /// <summary>
        /// Cantidad de fallas en la entidad ordenante durante el periodo reportado.
        /// </summary>
        public int CantidadFallasCMACT { get; set; }
        /// <summary>
        /// Cantidad de fallas en la entidad ordenante durante el periodo reportado.
        /// </summary>
        public int CantidadFallasYellowPepper => 0;
        /// <summary>
        /// Cantidad de fallas en el Gestor de Directorio CCE durante el periodo reportado.
        /// </summary>
        public int CantidadFallasCCE { get; set; }
        /// <summary>
        /// Resultado del ICD calculado  (3 enteros, 2 decimales).
        /// </summary>
        public decimal ICDResultado => 
            CantidadIntentosTotalesConsulta > 0
                ? Math.Round((decimal)(CantidadFallasCMACT + CantidadFallasYellowPepper + CantidadFallasCCE) / CantidadIntentosTotalesConsulta * 100, 2) 
                : 0M;
        /// <summary>
        /// Comentarios adicionales que la entidad desea transmitir.
        /// </summary>
        public string Comentario { get; set; }
        #endregion
    }
}
