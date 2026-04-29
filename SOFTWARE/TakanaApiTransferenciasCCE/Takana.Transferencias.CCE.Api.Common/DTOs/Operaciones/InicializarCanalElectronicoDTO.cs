using Swashbuckle.AspNetCore.Annotations;
using Takana.Transferencias.CCE.Api.Common.DTOs.BA;
using Takana.Transferencias.CCE.Api.Common.DTOs.CF;
using Takana.Transferencias.CCE.Api.Common.DTOs.CL;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones
{
    public class InicializarCanalElectronicoDTO
    {
        /// <summary>
        /// Lista de Productos de debitos CCE
        /// </summary>
        [SwaggerSchema("Tipos de transferencia CCE")]
        public List<CuentaEfectivoDTO> ProductosDebito { get; set; }
        /// <summary>
        /// Lista de tipos de documentos CCE
        /// </summary>
        [SwaggerSchema("Entidades financieras CCE")]
        public List<TipoDocumentoTinDTO> TiposDocumentos { get; set; }
        /// <summary>
        /// Entidades financieras CCE
        /// </summary>
        [SwaggerSchema("Entidades financieras CCE")]
        public List<EntidadFinancieraTinDTO> EntidadesFinancieras { get; set; }
        /// <summary>
        /// Entidades financieras CCE
        /// </summary>
        [SwaggerSchema("Entidades financieras CCE")]
        public List<LimiteTransferenciaDTO> LimitesTransferencias { get; set; }
    }
}
