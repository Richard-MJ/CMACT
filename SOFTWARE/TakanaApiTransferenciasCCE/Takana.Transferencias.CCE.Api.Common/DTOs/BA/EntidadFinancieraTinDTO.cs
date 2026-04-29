using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.BA
{
    /// <summary>
    /// DTO de EntidadFinanciera 
    /// </summary>
    public class EntidadFinancieraTinDTO
    {
        /// <summary>
        /// Identificador de la entidad
        /// </summary>
        [SwaggerSchema("Identificador de la entidad")]
        public int IdEntidad { get; set; }
        /// <summary>
        /// Código del Estado Sign
        /// </summary>
        [SwaggerSchema("Código del Estado Sign")]
        public string? CodigoEstadoSign { get; set; }

        /// <summary>
        /// Código que identifica a la Entidad Financiera según CCE
        /// </summary>
        [SwaggerSchema("Código que identifica a la Entidad Financiera según CCE")]
        public string CodigoEntidad { get; set; }

        /// <summary>
        /// Descripción de la Entidad Financiera
        /// </summary>
        [SwaggerSchema("Descripción de la Entidad Financiera")]
        public string NombreEntidad { get; set; }

        /// <summary>
        /// Codigo Oficina
        /// </summary>
        [SwaggerSchema("Codigo Oficina")]
        public string? OficinaPagoTarjeta { get; set; }
    }
}
