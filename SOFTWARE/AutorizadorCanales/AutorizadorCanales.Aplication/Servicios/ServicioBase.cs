using AutorizadorCanales.Logging.Interfaz;

namespace AutorizadorCanales.Aplication.Servicios;

public class ServicioBase<T> where T : class
{
    public readonly IBitacora<T> Bitacora;
    public readonly IContexto Contexto;

    public ServicioBase(IContexto contexto, IBitacora<T> bitacora)
    {
        Contexto = contexto;
        Bitacora = bitacora;
    }
}