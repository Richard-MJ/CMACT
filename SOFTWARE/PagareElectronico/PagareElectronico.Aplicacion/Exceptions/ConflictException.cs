namespace PagareElectronico.Application.Exceptions
{
    /// <summary>
    /// Representa un conflicto de negocio.
    /// </summary>
    public sealed class ConflictException : Exception
    {
        /// <summary>
        /// Código funcional o técnico del error.
        /// </summary>
        public string Code { get; }

        public ConflictException(string code, string message)
            : base(message)
        {
            Code = code;
        }
    }
}