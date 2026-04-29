using AutorizadorCanales.Contracts.SG.Response;
using System.Text.Json.Nodes;
using MediatR;

namespace AutorizadorCanales.Aplication.Features.Autenticacion.Commands;
public record AutenticarAperturaProductoCommand
(
    string NumeroDocumento,
    string Terminal,
    string IdTipoDocumento,
    int IdTipoOperacionCanalElectronico,
    string? Guids,
    string? ModeloDispositivo,
    AudienciaResponse SistemaCliente,
    int IdTrama) : IRequest<JsonObject>;