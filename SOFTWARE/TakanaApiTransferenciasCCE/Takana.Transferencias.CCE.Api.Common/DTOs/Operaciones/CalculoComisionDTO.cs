using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones
{
    public record CalculoComisionDTO
    {
        /// <summary>
        /// Numero cuenta del cliente originante
        /// </summary>
        [SwaggerSchema("NumeroCuenta")]
        public string NumeroCuenta { get; set; }
        /// <summary>
        /// Comision de operacion
        /// </summary>
        [SwaggerSchema("Comision de operacion")]
        public ComisionDTO Comision { get; set; }
        /// <summary>
        /// Control de monto
        /// </summary>
        [SwaggerSchema("Control de monto")]
        public ControlMontoDTO? ControlMonto { get; set; } = new ControlMontoDTO();
        /// <summary>
        /// Indicaor de mismo titular u otro titular
        /// </summary>
        [SwaggerSchema("Indicador de mismo titular u otro titular")]
        public string MismoTitular { get; set; }
        /// <summary>
        /// Saldo actual
        /// </summary>
        [SwaggerSchema("Saldo actual")]
        public decimal SaldoActual { get; set; }
        /// <summary>
        /// Monto de la operacion
        /// </summary>
        [SwaggerSchema("Monto de la operacion")]
        public decimal MontoOperacion { get; set; }
        /// <summary>
        /// Monto minimo de la cuenta
        /// </summary>
        [SwaggerSchema("Monto minimo de la cuenta")]
        public decimal MontoMinimoCuenta { get; set; }
        /// <summary>
        /// Indicador de cuenta exonerada de ITF
        /// </summary>
        [SwaggerSchema("Indicador de cuenta exonerada de ITF")]
        public bool EsExoneradaITF { get; set; }
        /// <summary>
        /// Indicador de cuenta sueldo
        /// </summary>
        [SwaggerSchema("Indicador de cuenta sueldo")]
        public bool EsCuentaSueldo { get; set; }
        /// <summary>
        /// Indicador si es exonerada de comision.
        /// </summary>
        [SwaggerSchema("Indicador si es exonerada de comision")]
        public bool EsExoneradoComision { get; set; }
    }
}
