
using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.CL
{
    public class TipoDocumentoTinDTO
    {
        /// <summary>
        /// Codigo del tipo de documento
        /// </summary>
        [SwaggerSchema("Codigo del tipo de documento")]
        public byte CodigoTipoDocumento { get; set; }
        /// <summary>
        /// Codigo del tipo de documento de camara de compensacion
        /// </summary>
        [SwaggerSchema("Codigo del tipo de documento de camara de compensacion")]
        public byte CodigoTipoDocumentoCCE { get; set; }
        /// <summary>
        /// Descripcion del tipo de documento
        /// </summary>
        [SwaggerSchema("Descripcion del tipo de documento")]
        public string? DescripcionTipoDocumento { get; set; }
        /// <summary>
        /// Es tipo de persona juridica
        /// </summary>
        [SwaggerSchema("Es tipo de persona juridica")]
        public bool EsTipoPersonaJuridica { get; set; }
        /// <summary>
        /// Codigo del tipo de documento de camara de compensacion para transferencias inmediatas
        /// </summary>
        [SwaggerSchema("Codigo del tipo de documento de camara de compensacion para transferencias inmediatas")]
        public string? CodigoTipoDocumentoCceTransferenciasInmediatas { get; set; }
        /// <summary>
        /// Especifica la longitud del documento
        /// </summary>
        [SwaggerSchema("especifica la longitud del documento")]
        public int LongitudDocumentoCCE { get; set; }
    }
}
