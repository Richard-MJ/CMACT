namespace PagareElectronico.Application.DTOs.Requests
{
    /// <summary>
    /// Representa la información base de un representante legal.
    /// </summary>
    public class DtoRepresentanteLegalBaseSolicitud
    {
        /// <summary>
        /// Obtiene o establece el nombre completo del representante legal.
        /// </summary>
        public string NombreRepresentanteLegal { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el número de documento del representante legal.
        /// </summary>
        public string NumeroDocumento { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el correo electrónico del representante legal.
        /// </summary>
        public string CorreoRepresentanteLegal { get; set; } = string.Empty;
    }
}