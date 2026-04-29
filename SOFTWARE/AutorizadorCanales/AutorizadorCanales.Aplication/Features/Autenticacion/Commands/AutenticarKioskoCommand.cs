using AutorizadorCanales.Contracts.SG.Response;
using MediatR;
using System.Text.Json.Nodes;

namespace AutorizadorCanales.Aplication.Features.Autenticacion.Commands;

public record AutenticarKioskoCommand
(
    string NumeroTarjeta,
    string Password,
    int IdTrama,
    string Terminal,
    string Usuario,
    AudienciaResponse SistemaCliente
) : IRequest<JsonObject>;
