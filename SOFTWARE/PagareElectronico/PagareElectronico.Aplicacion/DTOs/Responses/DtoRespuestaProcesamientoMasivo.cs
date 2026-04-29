namespace PagareElectronico.Application.DTOs.Responses
{
    /// <summary>
    /// Representa la respuesta interna para operaciones masivas de CAVALI.
    /// </summary>
    public sealed class DtoRespuestaProcesamientoMasivo
    {
        /// <summary>
        /// Obtiene o establece el código de resultado devuelto por CAVALI.
        /// </summary>
        public int? CodigoResultado { get; set; }

        /// <summary>
        /// Obtiene o establece el mensaje descriptivo devuelto por CAVALI.
        /// </summary>
        public string Mensaje { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece la cantidad de registros procesados exitosamente.
        /// </summary>
        public int CantidadExitosa { get; set; }

        /// <summary>
        /// Obtiene o establece la lista de registros fallidos.
        /// </summary>
        public List<DtoRegistroFallidoProcesamientoMasivo> Fallidos { get; set; } = new();
    }

    /// <summary>
    /// Representa un registro fallido dentro de un procesamiento masivo.
    /// </summary>
    public sealed class DtoRegistroFallidoProcesamientoMasivo
    {
        /// <summary>
        /// Obtiene o establece la llave del pagaré fallido.
        /// </summary>
        public DtoLlavePagareFallido LlavePagare { get; set; } = new();

        /// <summary>
        /// Obtiene o establece el código de resultado específico del registro fallido.
        /// </summary>
        public int? CodigoResultado { get; set; }

        /// <summary>
        /// Obtiene o establece el mensaje descriptivo del error del registro fallido.
        /// </summary>
        public string Mensaje { get; set; } = string.Empty;
    }

    /// <summary>
    /// Representa la llave de un pagaré fallido en una respuesta masiva.
    /// </summary>
    public sealed class DtoLlavePagareFallido
    {
        /// <summary>
        /// Obtiene o establece el código de banca.
        /// </summary>
        public int Banca { get; set; }

        /// <summary>
        /// Obtiene o establece el código de producto.
        /// </summary>
        public int Producto { get; set; }

        /// <summary>
        /// Obtiene o establece el número de crédito.
        /// </summary>
        public string NumeroCredito { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el código único.
        /// </summary>
        public string CodigoUnico { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece la fecha de cancelación, cuando aplique.
        /// </summary>
        public DateOnly? FechaCancelacion { get; set; }
    }
}