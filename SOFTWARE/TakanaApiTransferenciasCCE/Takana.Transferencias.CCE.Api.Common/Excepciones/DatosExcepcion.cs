namespace Takana.Transferencias.CCE.Api.Common.Excepciones
{
    /// <summary>
    /// Representa los datos de la excepcion de la aplicacion.
    /// </summary>
    public class DatosExcepcion
    {
        /// <summary>
        /// Codigo de la excepcion.
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>
        /// Motivo de la excepcion.
        /// </summary>
        public string Mensaje { get; set; } = string.Empty;
    }
}
