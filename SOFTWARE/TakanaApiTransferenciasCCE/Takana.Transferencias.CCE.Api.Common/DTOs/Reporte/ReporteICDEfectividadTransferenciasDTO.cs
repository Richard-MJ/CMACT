namespace Takana.Transferencias.CCE.Api.Common.DTOs.Reporte
{
    public class ReporteICDEfectividadTransferenciasDTO
    {
        #region Propiedades

        /// <summary>
        /// Cantidad de intentos totales de transferencias en el periodo reportado.
        /// </summary>
        public int CantidadIntentosTotalesTransferencia { get; set; }
        /// <summary>
        /// Cantidad de fallas en la entidad ordenante durante el periodo reportado.
        /// </summary>
        public int CantidadFallasCMACT { get; set; }
        /// <summary>
        /// Cantidad de fallas en el Procesador de Pago Niubiz durante el periodo reportado.
        /// </summary>
        public int CantidadFallasNiubiz => 0;
        /// <summary>
        /// Cantidad de fallas en el Procesador de Pago CCE durante el periodo reportado.
        /// </summary>
        public int CantidadFallasCCE { get; set; }
        /// <summary>
        /// Cantidad de fallas en el Procesador de Pago Izipay durante el periodo reportado.
        /// </summary>
        public int CantidadFallasIzipay => 0;
        /// <summary>
        /// Resultado del ICD calculado (([11] + [12] + [13] + [14]) / [10]) * 100, redondeado a 3 enteros y 2 decimales.
        /// </summary>
        public decimal ICDResultado =>
            CantidadIntentosTotalesTransferencia > 0
                ? Math.Round(((decimal)(CantidadFallasCMACT + CantidadFallasNiubiz + CantidadFallasCCE + CantidadFallasIzipay) / CantidadIntentosTotalesTransferencia) * 100, 2)
                : 0M;
        /// <summary>
        /// Comentarios adicionales que la entidad desea transmitir.
        /// </summary>
        public string Comentario { get; set; }

        #endregion
    }
}
