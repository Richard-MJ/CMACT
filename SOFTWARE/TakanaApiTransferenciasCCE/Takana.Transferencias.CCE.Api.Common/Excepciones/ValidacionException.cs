namespace Takana.Transferencias.CCE.Api.Common.Excepciones
{
    public class ValidacionException : BaseException
    {
        /// <summary>
        /// Codigo del error.
        /// </summary>
        private const string Codigo = "06";

        /// <summary>
        /// Representa una excepcion cuando no se cumple con una validacion.
        /// </summary>
        /// <param name="mensaje">Descripcion del error.</param>
        public ValidacionException(string mensaje) : base(Codigo, mensaje)
        {
        }

        /// <summary>
        /// Representa una excepcion cuando no se cumple con una validacion.
        /// </summary>
        /// <param name="mensaje">Descripcion del error.</param>
        /// <param name="innerException">Excepcion interna.</param>
        public ValidacionException(string mensaje, Exception innerException)
            : base(Codigo, mensaje, innerException)
        {
        }
    }
}
