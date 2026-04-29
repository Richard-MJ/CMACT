namespace Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad
{
    /// <summary>
    /// Clase de datos pde archivos de cliente DE dto
    /// </summary>
    public record ArchivoDirectorioClienteDTO
    {
        /// <summary>
        /// Nombre del archivo
        /// </summary>
        public string NombreArchivo { get; set; }
        /// <summary>
        /// Usuario que genera el archivo
        /// </summary>
        public string Usuario { get; set; }
        /// <summary>
        /// Lista de datos del archivo de directorio
        /// </summary>
        public List<EstructuraArchivoDirectorioDTO> DatosArchivoDirectorio { get; set; }
    }

    /// <summary>
    /// Clase de datos de la estructura del archivo de directorio de DTO
    /// </summary>
    public record EstructuraArchivoDirectorioDTO
    {
        /// <summary>
        /// Codigo de tipo de afiliacion
        /// </summary>
        public string CodigoAfiliacion { get; set;}
        /// <summary>
        /// Identificador de trama
        /// </summary>
        public string IdTrama { get; set; }
        /// <summary>
        /// Codigo de cuenta interbancario del cliente
        /// </summary>
        public string CodigoCuentaInterbancaria { get; set; }
        /// <summary>
        /// Numero de celulcar del cliente
        /// </summary>
        public string NumeroCelular { get; set; }
    }
}
