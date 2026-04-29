using PagareElectronico.Application.DTOs.Requests;
using PagareElectronico.Infrastructure.Integrations.Cavali.Contracts.Requests.Common;

namespace PagareElectronico.Infrastructure.Integrations.Cavali.Mappers
{
    /// <summary>
    /// Proporciona métodos auxiliares compartidos para mapear solicitudes hacia CAVALI.
    /// </summary>
    internal static class CavaliMapperHelper
    {
        /// <summary>
        /// Mapea un identificador interno de pagaré a la llave común de CAVALI.
        /// </summary>
        /// <param name="source">Identificador interno del pagaré.</param>
        /// <param name="productCode">Código de producto configurado.</param>
        /// <returns>Llave común del pagaré para CAVALI.</returns>
        public static CommonPromissoryNoteKey MapearLlaveComunPagare(
            DtoPagareIdentificadorSolicitud source,
            int bancaCode,
            int productCode)
        {
            return new CommonPromissoryNoteKey
            {
                Banking = bancaCode,
                UniqueCode = source.CodigoUnico,
                Product = productCode,
                CreditNumber = source.NumeroCredito
            };
        }
    }
}