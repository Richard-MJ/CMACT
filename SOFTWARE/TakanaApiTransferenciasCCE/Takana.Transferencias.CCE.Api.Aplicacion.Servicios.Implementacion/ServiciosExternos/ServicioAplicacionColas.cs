using RabbitMQ.Client;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Takana.Transferencias.CCE.Api.Common.DTOs.Sms;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Common.DTOs.Notificaciones;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.ServiciosExternos;

/// <summary>
/// Clase de servicio de colas
/// </summary>
public class ServicioAplicacionColas : ServicioBase, IServicioAplicacionColas
{
    private readonly IRabbitMqConnectionManager _connectionManager;
    private readonly IBitacora<ServicioAplicacionColas> _bitacora;

    private const string DefaultExchange = "";
    private const string DefaultDlExchange = "dl_exchange";
    private const string NOMBRE_COLA_NOTIFICACIONES = "notificaciones";
    private const string NOMBRE_COLA_NOTIFICA_UNIBANCA = "monitoreo_antifraude_unibanca";
    private const string DL_EXCHANGE_NOTIFICACIONES = "dl_exchange";
    private const string DLQ_COLA_NOTIFICACIONES = "dlq_notificaciones";

    public ServicioAplicacionColas(
        IRabbitMqConnectionManager connectionManager,
        IBitacora<ServicioAplicacionColas> bitacora,
        IContextoAplicacion contexto) : base(contexto)
    {
        _bitacora = bitacora;
        _connectionManager = connectionManager;
    }

    /// <summary>
    /// Método que envia un sms por colas en rabbit
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tipoMensaje"></param>
    /// <param name="datos"></param>
    /// <returns></returns>
    public async Task EnviarSmsGeneralAsync<T>(string tipoMensaje, T datos) where T : class
    {
        await EnviarMensajeAsync(SmsGeneralDTO.LlaveGenerarSMS, datos, tipoMensaje, incluirAdjunto: true);
    }

    /// <summary>
    /// Método 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tipoMensaje"></param>
    /// <param name="datos"></param>
    /// <returns></returns>
    public async Task EnviarCorreoAsync<T>(string tipoMensaje, T datos) where T : class
    {
        await EnviarMensajeAsync(CorreoGeneralDTO.LlaveGenerarCorreo, datos, tipoMensaje);
    }

    /// <summary>
    /// Metodo que envia notificación de antifraude
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tipoMensaje"></param>
    /// <param name="datos"></param>
    /// <returns></returns>
    public async Task EnviarNotificacionAntifraude<T>(string tipoMensaje, T datos) where T : class
    {
        await EnviarMensajeAsync(NOMBRE_COLA_NOTIFICA_UNIBANCA, datos, tipoMensaje);
    }

    /// <summary>
    /// Enviar notificaciones
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="datos"></param>
    /// <returns></returns>
    public Task EnviarNotificacionBitacora<T>(T datos) where T : class
    {
        return EnviarMensajeAsync(
            NOMBRE_COLA_NOTIFICACIONES,
            datos,
            dlqConfig: (DL_EXCHANGE_NOTIFICACIONES, DLQ_COLA_NOTIFICACIONES, DLQ_COLA_NOTIFICACIONES));
    }

    /// <summary>
    /// Enviar mensaje asincrono
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="queue"></param>
    /// <param name="datos"></param>
    /// <param name="tipoMensaje"></param>
    /// <param name="exchange"></param>
    /// <param name="dlqConfig"></param>
    /// <returns></returns>
    public async Task EnviarMensajeAsync<T>(
        string queue,
        T datos,
        string? tipoMensaje = null,
        string exchange = DefaultExchange,
        (string dlExchange, string dlRoutingKey, string dlQueue)? dlqConfig = null,
        bool incluirAdjunto = false) where T : class
    {
        var connection = await _connectionManager.GetConnectionAsync();

        await using var channel = await connection.CreateChannelAsync();

        try
        {
            Dictionary<string, object?> queueArgs = null;

            if (dlqConfig.HasValue)
            {
                var (dlEx, dlRk, dlQ) = dlqConfig.Value;
                var dlExchangeName = dlEx ?? DefaultDlExchange;

                await channel.ExchangeDeclareAsync(dlExchangeName, ExchangeType.Direct, durable: true);
                await channel.QueueDeclareAsync(dlQ, durable: true, exclusive: false, autoDelete: false);
                await channel.QueueBindAsync(dlQ, dlExchangeName, dlRk);

                queueArgs = new Dictionary<string, object?>
                {
                    ["x-dead-letter-exchange"] = dlExchangeName,
                    ["x-dead-letter-routing-key"] = dlRk
                };
            }

            await channel.QueueDeclareAsync(
                queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: queueArgs);

            object payload = datos;

            if (!string.IsNullOrEmpty(tipoMensaje))
            {
                var dict = JObject.FromObject(datos).ToObject<Dictionary<string, object>>();
                if (dict != null)
                {
                    dict[CorreoGeneralDTO.DescripcionTipoMensaje] = tipoMensaje;
                    dict[NotificacionUnibancaDTO.DescripcionTipoOperacion] = tipoMensaje;
                    payload = dict;
                }
            }

            byte[] body = JsonSerializer.SerializeToUtf8Bytes(payload);

            var props = new BasicProperties
            {
                Persistent = true,
                Headers = new Dictionary<string, object?>
                {
                    { "idLogin", _contextoAplicacion.IdLogin },
                    { "idSesion", _contextoAplicacion.IdSesion },
                    { "codigoUsuario", _contextoAplicacion.CodigoUsuario },
                    { "codigoAgencia", _contextoAplicacion.CodigoAgencia },
                    { "indicadorCanal", _contextoAplicacion.IndicadorCanal },
                    { "indicadorSubCanal", _contextoAplicacion.IndicadorSubCanal.ToString() },
                    { "idTerminalOrigen", _contextoAplicacion.IdTerminalOrigen },
                    { "token", _contextoAplicacion.Token }
                }
            };

            await channel.BasicPublishAsync(
                exchange,
                queue,
                mandatory: false,
                props,
                body.AsMemory());

            var logMsg = string.IsNullOrEmpty(tipoMensaje)
                ? $"Enviado mensaje a queue {queue}."
                : $"Enviado {tipoMensaje} a queue {queue}.";

            _bitacora.Trace(logMsg);
        }
        catch (Exception ex)
        {
            _bitacora.Error($"Error envío a {queue}: {ex.Message}");
            throw;
        }
    }
}