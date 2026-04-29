namespace Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad
{
    /// <summary>
    /// Clase de datos de archivo de directorio DTO
    /// </summary>
    public record ArchivoDirectorioDTO
    {
        /// <summary>
        /// Codigo de Operacion
        /// </summary>
        public string TipoInstruccion { get; set; }
        /// <summary>
        /// Codigo de Cuenta Interbancario
        /// </summary>
        public string CodigoCuentaInterbancario { get; set; }
        /// <summary>
        /// Codigo de referencia
        /// </summary>
        public string CodigoReferencia { get; set; }
        /// <summary>
        /// Número de Celular
        /// </summary>
        public string NumeroCelular { get; set; }
    }
}
