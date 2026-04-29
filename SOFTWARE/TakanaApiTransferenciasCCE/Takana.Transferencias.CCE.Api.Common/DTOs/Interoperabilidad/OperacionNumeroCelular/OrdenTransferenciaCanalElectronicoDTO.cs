using Swashbuckle.AspNetCore.Annotations;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad
{
    /// <summary>
    /// Clase de datos para la realizacion de transferencia
    /// </summary>
    public record OrdenTransferenciaCanalElectronicoDTO
    {
        /// <summary>
        /// Datos de control de montos
        /// </summary>
        [SwaggerSchema("Datos de control de montos")]
        public ControlMontoDTO ControlMonto { get; set; }
        /// <summary>
        /// Detalle de datos de la consulta de cuenta
        /// </summary>
        [SwaggerSchema("Detalle de datos de la consulta de cuenta")]
        public ResultadoConsultaCuentaCCE ResultadoConsultaCuenta { get; set; }
        /// <summary>
        /// Numero de cuenta cliente originante
        /// </summary>
        [SwaggerSchema("Numero de cuenta cliente originante")]
        public string NumeroCuenta { get; set; }
        /// <summary>
        /// Identificador QR si es que existe
        /// </summary>
        [SwaggerSchema("Identificador QR si es que existe")]
        public string? IdentificadorQR { get; set;}
        /// <summary>
        /// Numero de tarjeta del cliente originante
        /// </summary>
        [SwaggerSchema("Numero de tarjeta del cliente originante")]
        public string NumeroTarjeta { get; set; }
        /// <summary>
        /// Entidad destino
        /// </summary>
        [SwaggerSchema("Entidad destino")]
        public string EntidadDestino { get; set; }
        /// <summary>
        /// Numero de celular del cliente originante
        /// </summary>
        [SwaggerSchema("Numero de celular del cliente originante")]
        public string NumeroCelularOriginante { get; set; }
        /// <summary>
        /// Numero de celular cliente recpetor
        /// </summary>
        [SwaggerSchema("Numero de celular cliente receptor")]
        public string? NumeroCelularReceptor { get; set; }
        /// <summary>
        /// Descripción del motivo de la transferencia
        /// </summary>
        [SwaggerSchema("Descripción del motivo de la transferencia")]
        public string? Motivo { get; set; }
        /// <summary>
        /// Documento en bytes de terminos de beneficios y Riesgos
        /// </summary>
        [SwaggerSchema("Documento en bytes de terminos de beneficios y Riesgos")]
        public byte[] DocumentoTerminos { get; set; }
        /// <summary>
        /// Nombre de documento de terminos
        /// </summary>
        [SwaggerSchema("Nombre de documento de terminos")]
        public string NombreDocumentoTerminos { get; set; }
    }
}
