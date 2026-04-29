using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones
{
    /// <summary>
    /// Clase de consulta cuenta de Receptor
    /// </summary>
    public record ConsultaCuentaReceptorDTO
    {
        /// <summary>
        /// Código de tipo de transferencia inmediata
        /// </summary>
        [SwaggerSchema("Código de tipo de transferencia inmediata")]
        public string CodigoTipoTransferencia { get; set; }
        /// <summary>
        /// Codigo de canal segun la CCE
        /// </summary>
        [SwaggerSchema("Codigo de canal segun la CCE")]
        public string CodigoCanalCCE { get; set; }
        /// <summary>
        /// Numero de cuenta del cliente originante
        /// </summary>
        [SwaggerSchema("Numero de cuenta del cliente originante")]
        public string NumeroCuentaOriginante { get; set; }
        /// <summary>
        /// Codigo de Cuenta Interbancaria del Originante 
        /// </summary>
        [SwaggerSchema("Codigo de Cuenta Interbancaria del Originante")]
        public string NumeroCuentaReceptor { get; set; }
        /// <summary>
        /// Codigo de la entidad Receptora 
        /// </summary>
        [SwaggerSchema("Codigo de la entidad Receptora")]
        public string CodigoEntidadReceptora { get; set; }
    }
}

