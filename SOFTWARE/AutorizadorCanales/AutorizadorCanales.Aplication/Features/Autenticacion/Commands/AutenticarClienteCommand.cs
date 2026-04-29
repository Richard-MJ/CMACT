using AutorizadorCanales.Contracts.SG.Response;
using MediatR;
using System.Text.Json.Nodes;

namespace AutorizadorCanales.Aplication.Features.Autenticacion.Commands;

public record AutenticarClienteCommand
(
    string NumeroTarjeta,
    string Password,
    string? PasswordPrimario,
    string Terminal,
    string NumeroDocumento,
    string IdTipoDocumento,
    int IdTipoOperacionCanalElectronico,
    string? Guids,
    string? ModeloDispositivo,
    AudienciaResponse SistemaCliente,
    int IdTrama) : IRequest<JsonObject>;