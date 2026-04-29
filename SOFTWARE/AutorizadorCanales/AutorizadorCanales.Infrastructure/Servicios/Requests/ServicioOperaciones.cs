using AutorizadorCanales.Excepciones;
using AutorizadorCanales.Logging.Interfaz;
using AutorizadorCanales.ServiciosExternos.Interfaz.Cliente;

namespace AutorizadorCanales.Infrastructure.Servicios.Requests;

public class ServicioOperaciones<T> where T : class
{
    private readonly IBitacora<T> _bitacora;

    public ServicioOperaciones(IBitacora<T> bitacora)
    {
        _bitacora = bitacora;
    }

    public async Task<TDatos> InvocarOperacionAsync<TDatos>(IRequest request, Dictionary<string, string>? encabezado = null)
    {
        try
        {
            var respuesta = await ServicioClienteApi.InvocarBackendAsync(request, encabezado);

            if (!respuesta.RespuestaValida())
            {
                if (respuesta.EsMensajePersonalizado())
                {
                    var mensajeRespuesta = respuesta.ContenidoDeRespuesta<ExcepcionUsuario>();
                    throw new ExcepcionAUsuario(mensajeRespuesta.CodigoExcepcion ?? mensajeRespuesta.Codigo, mensajeRespuesta.Razon ?? mensajeRespuesta.Mensaje);
                }
                if (respuesta.EstaNoAutorizado())
                {
                    var mensajeRespuesta = respuesta.ContenidoDeRespuesta<ExcepcionUsuario>();
                    throw new ExcepcionAUsuario(mensajeRespuesta.CodigoExcepcion ?? mensajeRespuesta.Codigo, mensajeRespuesta.Razon ?? mensajeRespuesta.Mensaje);
                }

                throw new Exception($"Respuesta de servicio {nameof(T)} no válida.");
            }
            else
            {
                return respuesta.ContenidoDeRespuesta<TDatos>();
            }
        }
        catch (ExcepcionAUsuario ex)
        {
            _bitacora.Fatal($"Error en la comunicación con {typeof(T).Name}: {ex.Message}");
            throw new ExcepcionAUsuario(ex.CodigoError, ex.Message);
        }
        catch (ObjectDisposedException ex)
        {
            _bitacora.Fatal(
                $"Error en disposed: {ex.Message} con objeto {ex.ObjectName}<|>{ex.StackTrace}");
            throw new Exception(ex.Message);
        }
        catch (Exception ex)
        {
            _bitacora.Fatal($"Error en invocación del servicio {typeof(T).Name}: {ex.Message}");
            throw new Exception(ex.Message);
        }
    }
}
