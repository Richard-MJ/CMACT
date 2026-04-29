using AutorizadorCanales.Aplication.Features.Autenticacion.Commands;
using AutorizadorCanales.Aplication.Servicios.Autenticacion;
using MediatR;
using System.Text.Json.Nodes;

namespace AutorizadorCanales.Aplication.Features.Autenticacion.Handlers;

public class RefrescarTokenCommandHandler : IRequestHandler<RefrescarTokenCommand, JsonObject>
{
    private readonly IServicioRefrescoAcceso _servicioRefrescoAcceso;

    public RefrescarTokenCommandHandler(IServicioRefrescoAcceso servicioRefrescoAcceso)
    {
        _servicioRefrescoAcceso = servicioRefrescoAcceso;
    }

    /// <summary>
    /// Maneja el comando de refrescar token
    /// </summary>
    /// <param name="command">Datos del comando</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Retorna el json con datos de acceso</returns>
    public async Task<JsonObject> Handle(RefrescarTokenCommand command, CancellationToken cancellationToken)
    {
        return await _servicioRefrescoAcceso.RefrescarTokenCliente(command);
    }
}
