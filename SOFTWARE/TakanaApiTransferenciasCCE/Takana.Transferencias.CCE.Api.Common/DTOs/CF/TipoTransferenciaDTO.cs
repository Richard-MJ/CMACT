using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.CF
{
    public class TipoTransferenciaDTO
    {
        /// <summary>
        /// Identificador de tipo transferencia
        /// </summary>
        [SwaggerSchema("Identificador de tipo transferencia")]
        public int IDTipoTransferencia { get;  set; }
        /// <summary>
        /// Codigo de tipo de transferencia
        /// </summary>
        [SwaggerSchema("Codigo de tipo de transferencia")]
        public string CodigoTipoTransferencia { get;  set; }
        /// <summary>
        /// Descripcion de tipo de transferencia
        /// </summary>
        [SwaggerSchema("escripcion de tipo de transferencia")]
        public string DescripcionTipoTransferencia { get;  set; }
        /// <summary>
        /// Estado de tipo de transferencia
        /// </summary>
        [SwaggerSchema("stado de tipo de transferencia")]
        public string EstadoTipoTransferencia { get;  set; }

    }
}
