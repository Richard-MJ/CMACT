using RabbitMQ.Client;
using Takana.Transferencias.CCE.Api.Common.Interfaz;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.ServiciosExternos;

public class RabbitMqConnectionManager : IRabbitMqConnectionManager, IAsyncDisposable
{
    private readonly IConfiguracionColas _config;
    private readonly ConnectionFactory _factory;
    private IConnection? _connection;

    private readonly SemaphoreSlim _connectionLock = new SemaphoreSlim(1, 1);
    private bool _disposed;

    public RabbitMqConnectionManager(IConfiguracionColas config)
    {
        _config = config;
        _factory = new ConnectionFactory
        {
            HostName = _config.Ip,
            UserName = _config.Usuario,
            Password = _config.Password,
            VirtualHost = "/",
            Port = AmqpTcpEndpoint.UseDefaultPort,
            AutomaticRecoveryEnabled = true,
        };
    }

    public async Task<IConnection> GetConnectionAsync()
    {
        if (_disposed) throw new ObjectDisposedException(nameof(RabbitMqConnectionManager));

        if (_connection is { IsOpen: true })
            return _connection;

        await _connectionLock.WaitAsync();

        try
        {
            if (_connection is { IsOpen: true })
                return _connection;

            _connection = await _factory.CreateConnectionAsync();
            return _connection;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error conectando a RabbitMQ: {ex.Message}");
            throw;
        }
        finally
        {
            _connectionLock.Release();
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;
        _disposed = true;

        if (_connection != null)
        {
            try
            {
                await _connection.CloseAsync();
                await _connection.DisposeAsync();
            }
            catch { }
        }

        _connectionLock.Dispose();
    }
}
