namespace PagareElectronico.Application.Exceptions
{
    /// <summary>
    /// Representa un error cuando no se encuentra un recurso solicitado.
    /// </summary>
    public sealed class NotFoundException : Exception
    {
        /// <summary>
        /// Código funcional o técnico del error.
        /// </summary>
        public string Code { get; }

        public NotFoundException(string code, string message)
            : base(message)
        {
            Code = code;
        }
    }
}