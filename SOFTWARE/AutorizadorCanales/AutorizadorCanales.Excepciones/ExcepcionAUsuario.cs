using AutorizadorCanales.Excepciones.Constantes;
using System.Runtime.Serialization;

namespace AutorizadorCanales.Excepciones;

[Serializable]
public class ExcepcionAUsuario : Exception
{
    public string CodigoError { get; private set; }

    public ExcepcionAUsuario(string codigo) : base()
    {
        CodigoError = codigo;
    }

    public ExcepcionAUsuario(string codigo, string message) : base(message)
    {
        CodigoError = codigo;
    }

    public ExcepcionAUsuario(string codigo, string message, Exception innerException) : base(message, innerException)
    {
        CodigoError = codigo;
    }

    protected ExcepcionAUsuario(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public static ExcepcionAUsuario ExcepcionAfiliacionInicioSesion(string codigoError = ConstMensajeError.CodigoErrorAfiliacionInicioSesion,
        string mensaje = ConstMensajeError.MensajeErrorAfiliacionInicioSesion)
    {
        return new ExcepcionAUsuario(
           codigoError,
           mensaje);
    }
}
