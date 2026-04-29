namespace Takana.Transferencias.CCE.Api.Common.DTOs.Reporte
{
    public class ReporteMantenimientoDTO : ReporteGeneralDTO
    {
        #region Propiedades
        /// <summary>
        /// Identificador único del objeto.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de Tipo de Mantenimiento Ejecutado.
        /// </summary>
        public int IdTipoMantenimiento { get; set; }
        /// <summary>
        /// Tipo de Mantenimiento Ejecutado.
        /// </summary>
        public string TipoMantenimiento { get; set; }
        /// <summary>
        /// Duración del mantenimiento en formato de texto.
        /// </summary>
        public string Duracion { get; set; }
        /// <summary>
        /// Tiempo asociado al mantenimiento, posiblemente un estimado o medición específica.
        /// </summary>
        public string Tiempo { get; set; }
        /// <summary>
        /// Descripción del motivo por el cual se realizó el mantenimiento.
        /// </summary>
        public string DescripcionMotivo { get; set; }
        /// <summary>
        /// Descripción del impacto que causó el mantenimiento en el sistema o los usuarios.
        /// </summary>
        public string DescripcionImpacto { get; set; }
        /// <summary>
        /// Descripción de los problemas encontrados durante el proceso de mantenimiento.
        /// </summary>
        public string DescripcionProblemas { get; set; }
        /// <summary>
        /// Comentarios adicionales sobre el mantenimiento, como notas o recomendaciones.
        /// </summary>
        public string DescripcionComentarios { get; set; }
        /// <summary>
        /// Duracion Total de mantenimientos Programados
        /// </summary>
        public int DuracionTotalMantenimientoProgramados { get; set; }
        /// <summary>
        /// Cantidad Total de mantenimiento Programado
        /// </summary>
        public int CantidadTotalMantenimientoProgramados { get; set; }
        /// <summary>
        /// Duracion total de mantenimientos correctivos
        /// </summary>
        public int DuracionTotalMantenimientoCorrectivos { get; set; }
        /// <summary>
        /// Cantidad Total de mantenimietos correctivos
        /// </summary>
        public int CantidadTotalMantenimientoCorrectivos { get; set; }
        /// <summary>
        /// Duracion total de mantenimientos de emergencia
        /// </summary>
        public int DuracionTotalMantenimientoEmergencia { get; set; }
        /// <summary>
        /// Cantidad total de mantenimientos de emergencia
        /// </summary>
        public int CantidadTotalMantenimientoEmergencia { get; set; }
        #endregion
    }
}
