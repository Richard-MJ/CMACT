namespace Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad
{
    /// <summary>
    /// Clase de datos para validacion de la operacoin segun parametros
    /// </summary>
    public record ValidarOperacionDTO
    {
        /// <summary>
        /// Monto de la operacion
        /// </summary>
        public decimal MontoOperacion { get; set;}
        /// <summary>
        /// Codigo de cuenta interbancario del cliente originante
        /// </summary>
        public string CodigoCCIOriginante { get; set; }
    }
}
