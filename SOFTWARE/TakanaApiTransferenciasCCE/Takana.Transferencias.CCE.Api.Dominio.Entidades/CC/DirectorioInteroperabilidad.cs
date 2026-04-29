namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    /// <summary>
    /// Clase que representar a la entidad de Directorios de interoperabilidad
    /// </summary>
    public class DirectorioInteroperabilidad
    {
        /// <summary>
        /// Código del directorio.
        /// </summary>
        public string CodigoDirectorio { get; private set; }

        /// <summary>
        /// Nombre del directorio.
        /// </summary>
        public string NombreDirectorio { get; private set; }

        /// <summary>
        /// Estado del directorio.
        /// </summary>
        public string Estado { get; private set; }

        /// <summary>
        /// Código del usuario que realizó el registro.
        /// </summary>
        public string CodigoUsuarioRegistro { get; private set; }

        /// <summary>
        /// Código del usuario que realizó la última modificación, nullable.
        /// </summary>
        public string? CodigoUsuarioModifico { get; private set; }

        /// <summary>
        /// Fecha de registro del directorio.
        /// </summary>
        public DateTime FechaRegistro { get; private set; }

        /// <summary>
        /// Fecha de la última modificación del directorio, nullable.
        /// </summary>
        public DateTime? FechaModifico { get; private set; }
    }
}
