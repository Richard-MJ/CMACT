namespace Takana.Transferencias.CCE.Api.Common.Excepciones;

public class DomainException : Exception
{
    /// <summary>
    /// Excepcion de dominio
    /// </summary>
    public DomainException()
    {
    }
    /// <summary>
    /// Excepcion de dominio
    /// </summary>
    /// <param name="message"></param>
    public DomainException(string message)
        : base(message)
    {
    }
    /// <summary>
    /// Excepcion de dominio
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public DomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
