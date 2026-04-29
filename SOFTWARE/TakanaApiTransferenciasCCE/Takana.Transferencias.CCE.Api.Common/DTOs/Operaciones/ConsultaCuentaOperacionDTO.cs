using Swashbuckle.AspNetCore.Annotations;
using Takana.Transferencias.CCE.Api.Common.DTOs.BA;
using Takana.Transferencias.CCE.Api.Common.DTOs.CL;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones
{
    public record ConsultaCuentaOperacionDTO
    {
        /// <summary>
        /// Cuenta efectivo del cliente originante
        /// </summary>
        [SwaggerSchema("Cuenta efectivo del cliente originante")]
        public CuentaEfectivoDTO CuentaEfectivoDTO { get; set; }
        /// <summary>
        /// Tipo de tansferencia
        /// </summary>
        [SwaggerSchema("Tipo de tansferencia")]
        public string TipoTransferencia { get; set; }
        /// <summary>
        /// Numero de tarjeta o CCi
        /// </summary>
        [SwaggerSchema("Numero de tarjeta o CCi")]
        public string NumeroCuentaOTarjeta { get; set; }
        /// <summary>
        /// Tipo de documentos 
        /// </summary>
        [SwaggerSchema("Tipo de documentos")]
        public List<TipoDocumentoTinDTO> TiposDocumentos { get; set; }
        /// <summary>
        /// Entidad receptora
        /// </summary>
        [SwaggerSchema("Entidad receptora")]
        public EntidadFinancieraTinDTO? EntidadReceptora { get; set; } = new EntidadFinancieraTinDTO();
        /// <summary>
        /// Entidades
        /// </summary>
        [SwaggerSchema("Entidades")]
        public List<EntidadFinancieraTinDTO> Entidades { get; set; }
        /// <summary>
        /// Usuario que realiza la operacion
        /// </summary>
        [SwaggerSchema("Usuario que realiza la operacion")]
        public SesionUsuarioDTO Usuario { get; set; }
        /// <summary>
        /// Canal de operacion
        /// </summary>
        [SwaggerSchema("Canal de operacion")]
        public string Canal { get; set; }
        /// <summary>
        /// Numero celular del receptor para interoperabilidad
        /// </summary>
        [SwaggerSchema("Numero celular del receptor para interoperabilidad")]
        public string? NumeroCelularReceptor { get; set; }
        /// <summary>
        /// Valor proxy para interoperabilidad
        /// </summary>
        [SwaggerSchema("Valor proxy para interoperabilidad")]
        public string? ValorProxy { get; set; }
        /// <summary>
        /// Tipo de proxy para interoperabilidad
        /// </summary>
        [SwaggerSchema("Tipo de proxy para interoperabilidad")]
        public string? TipoProxy { get; set; }
    }
}
