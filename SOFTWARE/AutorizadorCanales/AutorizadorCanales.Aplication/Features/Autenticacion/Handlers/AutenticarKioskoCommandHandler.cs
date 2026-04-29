using AutorizadorCanales.Aplication.Features.Autenticacion.Commands;
using AutorizadorCanales.Aplication.Servicios.Autenticacion;
using MediatR;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace AutorizadorCanales.Aplication.Features.Autenticacion.Handlers;

public record AutenticarKioskoCommandHandler : IRequestHandler<AutenticarKioskoCommand, JsonObject>
{
    private IServicioAutenticacionKiosko _servicioAutenticacionKiosko;

    public AutenticarKioskoCommandHandler(IServicioAutenticacionKiosko servicioAutenticacionKiosko)
    {
        _servicioAutenticacionKiosko = servicioAutenticacionKiosko;
    }

    public async Task<JsonObject> Handle(AutenticarKioskoCommand command, CancellationToken cancellationToken)
    {
        var dtoSesion = await _servicioAutenticacionKiosko.AutenticarClienteKiosko(command.SistemaCliente.IdAudiencia, command.NumeroTarjeta, command.Password, command.IdTrama, command.Usuario, command.Terminal);

        return JsonNode.Parse(JsonSerializer.Serialize(dtoSesion))!.AsObject();
    }
}
