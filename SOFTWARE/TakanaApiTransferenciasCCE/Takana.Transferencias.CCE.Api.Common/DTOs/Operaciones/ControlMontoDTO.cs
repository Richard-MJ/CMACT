using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones
{
    public record ControlMontoDTO
    {
        /// <summary>
        /// Codigo de la moneda
        /// </summary>
        [SwaggerSchema("Codigo de la moneda")]
        public string CodigoMoneda { get; set; }
        /// <summary>
        /// Codigo de la moneda origen
        /// </summary>
        [SwaggerSchema("Codigo de la moneda origen")]
        public string? CodigoMonedaOrigen { get; set; }
        /// <summary>
        /// Monto de la operacion
        /// </summary>
        [SwaggerSchema("onto de la operacion")]
        public decimal Monto { get; set; }
        /// <summary>
        /// Monto de la comision
        /// </summary>
        [SwaggerSchema("Monto de la comision")]
        public decimal MontoComisionEntidad { get; set; }
        /// <summary>
        /// Monto de la comision
        /// </summary>
        [SwaggerSchema("Monto de la comision")]
        public decimal MontoComisionCce { get; set; }
        /// <summary>
        /// Monto ITF
        /// </summary>
        [SwaggerSchema("Monto ITF")]
        public decimal Itf { get; set; } = 0.0m;
        /// <summary>
        /// Comision Total
        /// </summary>
        [SwaggerSchema("Comision Total")]
        public decimal TotalComision { get; set; }
        /// <summary>
        /// Monto Total
        /// </summary>
        [SwaggerSchema("Monto Total")]
        public decimal Total { get; set; }
    }
}
