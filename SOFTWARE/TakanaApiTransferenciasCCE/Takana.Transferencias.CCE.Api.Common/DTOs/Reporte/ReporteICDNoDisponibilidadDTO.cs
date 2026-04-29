namespace Takana.Transferencias.CCE.Api.Common.DTOs.Reporte
{
    public class ReporteICDNoDisponibilidadDTO
    {
        #region Propiedades
        /// <summary>
        /// Minutos totales definidos para el periodo (5 enteros).
        /// </summary>
        public int MinutosTotales { get; set; }
        /// <summary>
        /// Minutos de mantenimiento realizados en el periodo reportado (5 enteros).
        /// </summary>
        public int MinutosTotalesMantenimiento { get; set; }
        /// <summary>
        /// Resultado del ICD calculado como ([MinutosTotalesIndisp] / [MinutosTotales]) * 100 (3 enteros, 2 decimales).
        /// </summary>
        public decimal ICDResultado { get; set; }
        /// <summary>
        /// Comentario adicional que desee transmitir la entidad (hasta 300 caracteres).
        /// </summary>
        public string Comentarios { get; set; }
        #endregion

        /// <summary>
        /// Actualiza el valor de ICD No Disponibilidad
        /// </summary>
        /// <param name="minutosNoDisponibles"></param>
        public void ActualizarICDNoDisponibilidad(double minutosNoDisponibles)
        {
            ICDResultado = MinutosTotales > 0 ? Math.Round((decimal)minutosNoDisponibles / MinutosTotales * 100, 2) : 0M;
        }
    }
}
