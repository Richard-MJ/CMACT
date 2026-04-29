using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones
{
    public record ComisionDTO
    {
        /// <summary>
        /// Identificador único del registro.
        /// </summary>
        [SwaggerSchema("Identificador único del registro.")]
        public int Id { get; set; }
        /// <summary>
        /// Identificador del tipo de transferencia asociada a la tarifa.
        /// </summary>
        [SwaggerSchema("Identificador del tipo de transferencia asociada a la tarifa.")]
        public int IdTipoTransferencia { get; set; }
        /// <summary>
        /// Código de la comisión asociada a la tarifa.
        /// </summary>
        [SwaggerSchema("Código de la comisión asociada a la tarifa.")]
        public string CodigoComision { get; set; }
        /// <summary>
        /// Código de la moneda utilizada en la tarifa.
        /// </summary>
        [SwaggerSchema("Código de la moneda utilizada en la tarifa.")]
        public string CodigoMoneda { get; set; }
        /// <summary>
        /// Código de la aplicación de la tarifa.
        /// </summary>
        [SwaggerSchema("Código de la aplicación de la tarifa.")]
        public string CodigoAplicacionTarifa { get; set; }
        /// <summary>
        /// Porcentaje de la comisión aplicada (si aplica).
        /// </summary>
        [SwaggerSchema("Porcentaje de la comisión aplicada (si aplica).")]
        public decimal Porcentaje { get; set; }
        /// <summary>
        /// Monto mínimo de la comisión aplicable (si aplica).
        /// </summary>
        [SwaggerSchema("Monto mínimo de la comisión aplicable (si aplica).")]
        public decimal Minimo { get; set; }
        /// <summary>
        /// Monto máximo de la comisión aplicable (si aplica).
        /// </summary>
        [SwaggerSchema("Monto máximo de la comisión aplicable (si aplica).")]
        public decimal Maximo { get; set; }
        /// <summary>
        /// Indica si el porcentaje se aplica sobre el monto total o sobre la base imponible.
        /// </summary>
        [SwaggerSchema("Indica si el porcentaje se aplica sobre el monto total o sobre la base imponible.")]
        public string IndicadorPorcentaje { get; set; }
        /// <summary>
        /// Indica si el monto de la comisión es fijo.
        /// </summary>
        [SwaggerSchema("Indica si el monto de la comisión es fijo.")]
        public string IndicadorFijo { get; set; }
        /// <summary>
        /// Porcentaje de la comisión aplicada a la Cuenta Corriente Extranjera (CCE) (si aplica).
        /// </summary>
        [SwaggerSchema("Porcentaje de la comisión aplicada a la Cuenta Corriente Extranjera (CCE) (si aplica).")]
        public decimal PorcentajeCCE { get; set; }
        /// <summary>
        /// Monto mínimo de la comisión aplicada a la CCE (si aplica).
        /// </summary>
        [SwaggerSchema("Monto mínimo de la comisión aplicada a la CCE (si aplica).")]
        public decimal MinimoCCE { get; set; }
        /// <summary>
        /// Monto máximo de la comisión aplicada a la CCE (si aplica).
        /// </summary>
        [SwaggerSchema("Monto máximo de la comisión aplicada a la CCE (si aplica).")]
        public decimal MaximoCCE { get; set; }
    }
}
