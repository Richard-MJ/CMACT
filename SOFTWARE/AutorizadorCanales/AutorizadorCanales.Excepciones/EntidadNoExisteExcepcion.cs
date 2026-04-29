using System.Runtime.Serialization;

namespace AutorizadorCanales.Excepciones;

[Serializable]
public class EntidadNoExisteExcepcion : ExcepcionAUsuario
{
    private const string FORMATO = "No se pudo recuperar la entidad {0} con clave: {1}.";
    private readonly string _mensaje = string.Empty;

    public override string Message => _mensaje;

    public EntidadNoExisteExcepcion() : base("02") { }

    public EntidadNoExisteExcepcion(Type tipoEntidad, object[] propiedadesClave) : base("02")
    {
        _mensaje = string.Format(FORMATO, tipoEntidad.Name,
            propiedadesClave.Aggregate((a, b) => a + "," + b));
    }

    public EntidadNoExisteExcepcion(Type tipoEntidad, object[] propiedadesClave, Exception innerException)
        : base("02", "", innerException)
    {
        _mensaje = string.Format(FORMATO, tipoEntidad.Name,
            propiedadesClave.Aggregate((a, b) => a + "," + b));
    }

    public EntidadNoExisteExcepcion(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
