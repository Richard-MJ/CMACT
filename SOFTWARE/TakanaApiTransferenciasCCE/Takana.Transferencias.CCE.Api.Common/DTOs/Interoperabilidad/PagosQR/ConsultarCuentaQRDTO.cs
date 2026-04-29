using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad
{
    /// <summary>
    /// Clase de datos para la consuslta de cuenta QR
    /// </summary>
    public record ConsultarCuentaQRDTO
    {
        /// <summary>
        /// Numero de cuenta originante
        /// </summary>
        public string NumeroCuentaOriginante { get; set; }
        /// <summary>
        /// Identificador de QR
        /// </summary>
        public string IdentificadorQR { get; set; }
        /// <summary>
        /// Codigo de entidad receptora
        /// </summary>
        [StringLength(3, ErrorMessage = "El campo Codigo Entidad Receptora no puede tener más de 3 caracteres.")]
        public string CodigoEntidadReceptora { get; set; }
        /// <summary>
        /// Identificador de cuenta
        /// </summary>
        public string? IdentificadorCuenta { get; set; }
        /// <summary>
        /// Codigo de moneda de la transaccion
        /// </summary>
        public string CodigoMoneda { get; set; }
    }
}
