using Swashbuckle.AspNetCore.Annotations;
using Takana.Transferencias.CCE.Api.Common.DTOs.CL;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad
{
    public class RespuetaConsultaCuentaDTO
    {
        /// <summary>
        /// Datos de la cuenta efectivo
        /// </summary>
        [SwaggerSchema("Datos de la cuenta efectivo")]
        public CuentaEfectivoDTO CuentaEfectivo { get; set; }
    }
}
