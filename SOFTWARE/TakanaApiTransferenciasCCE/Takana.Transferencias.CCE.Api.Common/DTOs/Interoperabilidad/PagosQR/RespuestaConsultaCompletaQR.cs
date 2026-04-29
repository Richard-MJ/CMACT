using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad
{
    /// <summary>
    /// Respuesta de consulta
    /// </summary>
    public record RespuestaConsultaCompletaQR
    {
        /// <summary>
        /// Datos de consulta para la interoperabilidad de cuenta.
        /// </summary>
        [SwaggerSchema("Datos de consulta para la interoperabilidad de cuenta.")]
        public ResultadoConsultaCuentaInteroperabilidadDTO DatosConsulta { get; set; }
        /// <summary>
        /// Nombre de la entidad receptora del código QR.
        /// </summary>
        [SwaggerSchema("Nombre de la entidad receptora del código QR.")]
        public string NombreEntidadReceptora { get; set; }
        /// <summary>
        /// Identificador único asociado al código QR.
        /// </summary>
        [SwaggerSchema("Identificador único asociado al código QR.")]
        public string IdentificadorQR { get; set; }
        /// <summary>
        /// Límite máximo permitido para el monto de una transacción.
        /// </summary>
        [SwaggerSchema("Límite máximo permitido para el monto de una transacción.")]
        public decimal LimiteMontoMaximo { get; set; }
        /// <summary>
        /// Límite mínimo permitido para el monto de una transacción.
        /// </summary>
        [SwaggerSchema("Límite mínimo permitido para el monto de una transacción.")]
        public decimal LimiteMontoMinimo { get; set; }
        /// <summary>
        /// Monto máximo permitido por día para una operación.
        /// </summary>
        [SwaggerSchema("Monto máximo permitido por día para una operación.")]
        public decimal MontoMaximoDia { get; set; }
    }
}
