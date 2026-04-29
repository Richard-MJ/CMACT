using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones
{
    /// <summary>
    /// Clase de datos de validacion para para validar la operacion de transaccion de interoperabilidad
    /// </summary>
    public record ValidarTransaccionInmediataDTO
    {
        /// <summary>
        /// Código del tipo de transferencia CCE (Consulta de Cuenta Efectivo).
        /// </summary>
        [SwaggerSchema("ódigo del tipo de transferencia CCE (Consulta de Cuenta Efectivo).")]
        public string CodigoTipoTransferenciaCce { get; set; }

        /// <summary>
        /// Código de la moneda de la operación.
        /// </summary>
        [SwaggerSchema("Código de la moneda de la operación.")]
        public string CodigoMoneda { get; set; }

        /// <summary>
        /// Saldo actual después de la operación.
        /// </summary>
        [SwaggerSchema("aldo actual después de la operación.")]
        public decimal SaldoActual { get; set; }

        /// <summary>
        /// Monto de la operación realizada.
        /// </summary>
        [SwaggerSchema("Monto de la operación realizada.")]
        public decimal MontoOperacion { get; set; }
    }
}
