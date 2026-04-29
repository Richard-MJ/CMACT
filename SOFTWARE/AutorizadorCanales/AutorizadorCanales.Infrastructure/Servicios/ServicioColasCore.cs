using AutorizadorCanales.Aplication.Common.ServicioColas;
using AutorizadorCanales.Aplication.Common.ServicioColas.Configuracion;
using AutorizadorCanales.Logging.Interfaz;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace AutorizadorCanales.Infrastructure.ServicioColas.Implementacion;

public class ServicioColasCore : IServicioColasCore, IDisposable
{
    private readonly IBitacora<ServicioColasCore> _bitacora;
    private readonly IConnection? _conexion;
    private readonly IContextoApiColas _contextoApiColas;

    public ServicioColasCore(
       IContextoApiColas contextoApiColas,
       ConfiguracionServicioColas configuracionServicioColas,
       IBitacora<ServicioColasCore> bitacora)
    {
        _contextoApiColas = contextoApiColas;
        _bitacora = bitacora;

        var hostname = configuracionServicioColas.Ip;

        var factory = new ConnectionFactory
        {
            HostName = hostname,
            UserName = configuracionServicioColas.Usuario,
            Password = configuracionServicioColas.Clave,
            VirtualHost = "/",
            Port = AmqpTcpEndpoint.UseDefaultPort
        };

        try
        {
            _conexion = factory.CreateConnection();
            _bitacora.Trace("Conexión a servico de Colas {0} habilitado.", hostname);
        }
        catch (Exception ex)
        {
            _conexion = null;
            _bitacora.Error("Error al conectar a servicio de Colas: {0}", ex.Message);
        }
    }

    public void Dispose()
    {
        if(_conexion is not null)
            _conexion.Close();
    }

    private string FormatearFechaSinZonaHoraria(DateTime fecha)
    {
        return fecha.ToString("yyyy-MM-ddTHH:mm:ss.fff");
    }

    public void EnviarNotificacion<T>(T datos) where T : class
    {
        string NOMBRE_COLA = "notificaciones";
        string DL_EXCHANGE = "dl_exchange";
        string DLQ_COLA = "dlq_notificaciones";
        using (var channel = _conexion!.CreateModel())
        {
            channel.ExchangeDeclare(DL_EXCHANGE, "direct", true);
            var argumentos = new Dictionary<string, object>
                {
                    {"x-dead-letter-exchange", DL_EXCHANGE },
                    {"x-dead-letter-routing-key", DLQ_COLA }
                };
            channel.QueueDeclare(
                queue: NOMBRE_COLA,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: argumentos);
            channel.QueueDeclare(DLQ_COLA, true, false, false);

            var datosMensaje = JsonSerializer.SerializeToNode(datos) as JsonObject ?? new JsonObject();
            foreach (var propiedad in datosMensaje.ToList())
            {
                if (propiedad.Value is JsonValue jsonValue && jsonValue.TryGetValue<DateTime>(out DateTime fecha))
                {
                    string fechaFormateada = FormatearFechaSinZonaHoraria(fecha);
                    datosMensaje[propiedad.Key] = fechaFormateada;
                }
            }

            var body = Encoding.UTF8.GetBytes(datosMensaje.ToString());
            var propiedades = channel.CreateBasicProperties();
            propiedades.Persistent = true;

            propiedades.Headers = new Dictionary<string, object>
                {
                    { "idLogin", _contextoApiColas.IdLogin },
                    { "idSesion", _contextoApiColas.IdSesion },
                    { "codigoUsuario", _contextoApiColas.CodigoUsuario },
                    { "codigoAgencia", _contextoApiColas.CodigoAgencia },
                    { "indicadorCanal", _contextoApiColas.IdCanalOrigen },
                    { "indicadorSubCanal", _contextoApiColas.IndicadorSubCanal.ToString() },
                    { "idTerminalOrigen", _contextoApiColas.IdTerminalLogin },
                    { "token", _contextoApiColas.Token }
                };

            channel.BasicPublish(
                exchange: "",
                routingKey: NOMBRE_COLA,
                basicProperties: propiedades,
                body: body);
            _bitacora.Trace("Envio a colas de la notificacion: " + datosMensaje);
        }
    }
}
