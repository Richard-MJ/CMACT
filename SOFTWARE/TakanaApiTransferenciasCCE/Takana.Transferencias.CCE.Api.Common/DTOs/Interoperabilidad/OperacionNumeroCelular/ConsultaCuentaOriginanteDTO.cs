using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad
{
    public class ConsultaCuentaOriginanteDTO
    {
        /// <summary>
        /// Numero de cuenta originante
        /// </summary>
        [SwaggerSchema("Numero de cuenta originante")]
        public string NumeroCuenta {  get; set; }
    }
}
