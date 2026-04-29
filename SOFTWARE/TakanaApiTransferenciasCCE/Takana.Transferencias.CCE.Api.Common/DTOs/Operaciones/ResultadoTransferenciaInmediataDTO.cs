namespace Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones
{
    /// <summary>
    /// Clase de datos para resultado para la transferencia inmediata
    /// </summary>
    public record ResultadoTransferenciaInmediataDTO
    {
        /// <summary>
        /// Número de la transacción.
        /// </summary>
        public int NumeroTransaccion { get; set; }
        /// <summary>
        /// Número de la operación.
        /// </summary>
        public int NumeroOperacion { get; set; }
        /// <summary>
        /// Número del asiento.
        /// </summary>
        public int NumeroAsiento { get; set; }
        /// <summary>
        /// Número de lavado asociado.
        /// </summary>
        public int NumeroLavado { get; set; }
        /// <summary>
        /// Resultado de la impresión asociada.
        /// </summary>
        public string ResultadoImpresion { get; set; }

    }
}
