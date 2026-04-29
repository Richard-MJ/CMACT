namespace PagareElectronico.Application.Exceptions
{
    /// <summary>
    /// Representa un error de validación o de regla de negocio.
    /// </summary>
    public sealed class ValidationException : Exception
    {
        /// <summary>
        /// Código funcional o técnico del error.
        /// </summary>
        public string Code { get; }

        public ValidationException(string code, string message)
            : base(message)
        {
            Code = code;
        }
    }
}