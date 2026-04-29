namespace PagareElectronico.Application.DTOs.Responses
{
    /// <summary>
    /// Representa la respuesta interna de registro y anotación de un pagaré.
    /// </summary>
    public sealed class DtoRespuestaRegistroPagare
    {
        /// <summary>
        /// Obtiene o establece el identificador del proceso retornado por CAVALI.
        /// Solo se retorna normalmente en HTTP 200.
        /// </summary>
        public long? IdProceso { get; set; }

        /// <summary>
        /// Obtiene o establece el código de resultado devuelto por CAVALI.
        /// </summary>
        public int? CodigoResultado { get; set; }

        /// <summary>
        /// Obtiene o establece el mensaje descriptivo devuelto por CAVALI.
        /// </summary>
        public string Mensaje { get; set; } = string.Empty;
    }
}