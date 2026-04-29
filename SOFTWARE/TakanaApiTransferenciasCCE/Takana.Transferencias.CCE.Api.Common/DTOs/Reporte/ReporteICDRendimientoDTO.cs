namespace Takana.Transferencias.CCE.Api.Common.DTOs.Reporte
{
    public class ReporteICDRendimientoDTO
    {
        #region Propiedades

        /// <summary>
        /// Cantidad de consultas totales exitosas en el periodo reportado.
        /// </summary>
        public int CantidadTotales { get; set; }
        /// <summary>
        /// Cantidad de consultas que no superaron el valor de t segundos (solo para tipo PERCENTIL).
        /// </summary>
        public int? CantidadMenorIgualT { get; set; }
        /// <summary>
        /// Cantidad de consultas que superaron el valor de tmax segundos (solo para tipo TIEMPO MÁXIMO).
        /// </summary>
        public int? CantidadMayorIgualTMax { get; set; }
        /// <summary>
        /// Tipo del ICD: PERCENTIL o TIEMPO MAXIMO.
        /// </summary>
        public string TipoICD { get; set; }
        /// <summary>
        /// Resultado del ICD calculado según el tipo:
        /// - Si PERCENTIL: ([12] / [11]) * 100
        /// - Si TIEMPO MAXIMO: ([13] / [11]) * 100
        /// </summary>
        public decimal ICDResultado
        {
            get
            {
                if (CantidadTotales <= 0)
                    return 0M;

                if (TipoICD == "PERCENTIL" && CantidadMenorIgualT.HasValue)
                    return Math.Round(((decimal)CantidadMenorIgualT.Value / CantidadTotales) * 100, 2);

                if (TipoICD == "TIEMPO MAXIMO" && CantidadMayorIgualTMax.HasValue)
                    return Math.Round(((decimal)CantidadMayorIgualTMax.Value / CantidadTotales) * 100, 2);

                return 0M;
            }
        }

        /// <summary>
        /// Comentarios adicionales que la entidad desea transmitir.
        /// </summary>
        public string Comentario { get; set; }
        #endregion
    }
}
