using Swashbuckle.AspNetCore.Annotations;
using Takana.Transferencias.CCE.Api.Common.DTOs.BA;
using Takana.Transferencias.CCE.Api.Common.DTOs.CC;
using Takana.Transferencias.CCE.Api.Common.DTOs.CF;
using Takana.Transferencias.CCE.Api.Common.DTOs.CL;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones
{
    public class InicializarVentanillaDTO
    {
        /// <summary>
        /// Tipos de transferencia CCE
        /// </summary>
        [SwaggerSchema("Tipos de transferencia CCE")]
        public List<TipoTransferenciaDTO> TipoTransferenciaCCE { get; set; }
        /// <summary>
        /// Entidades financieras CCE
        /// </summary>
        [SwaggerSchema("Entidades financieras CCE")]
        public List<EntidadFinancieraTinDTO> EntidadFinancieraCCE { get; set; }
        /// <summary>
        /// Tipos de documentos CCE
        /// </summary>
        [SwaggerSchema("Tipos de documentos CCE")]
        public List<TipoDocumentoTinDTO> TipoDocumentoCCE { get; set; }
        /// <summary>
        /// Concepto de cobro
        /// </summary>
        [SwaggerSchema("oncepto de cobro")]
        public List<ConceptoCobroDTO> ConceptoCobro { get; set; }
        /// <summary>
        /// Vinculos y motivos
        /// </summary>
        [SwaggerSchema("Vinculos y motivos")]
        public VinculosMotivosDTO VinculoMotivo { get; set; }


    }
}
