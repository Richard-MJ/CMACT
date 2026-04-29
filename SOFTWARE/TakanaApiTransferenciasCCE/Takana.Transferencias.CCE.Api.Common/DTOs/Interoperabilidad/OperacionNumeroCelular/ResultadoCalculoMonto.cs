using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad
{
    /// <summary>
    /// Clase de datos de calculo de montos totaltes
    /// </summary>
    public record ResultadoCalculoMonto
    {
        /// <summary>
        /// Control monto totales
        /// </summary>
        public ControlMontoDTO ControlMonto {  get; set; }
    }
}
