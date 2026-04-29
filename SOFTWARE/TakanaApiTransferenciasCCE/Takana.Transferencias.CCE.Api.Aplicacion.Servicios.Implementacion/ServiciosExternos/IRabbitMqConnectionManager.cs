using RabbitMQ.Client;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.ServiciosExternos;

public interface IRabbitMqConnectionManager
{
    Task<IConnection> GetConnectionAsync();
}
