using AutorizadorCanales.Contracts.SG.Response;
using MediatR;
using System.Text.Json.Nodes;

namespace AutorizadorCanales.Aplication.Features.Autenticacion.Commands;

/// <summary>
/// Comando para refrescar token
/// </summary>
/// <param name="RefreshToken">Token de refresco</param>
/// <param name="Audiencia">Entidad audiencia</param>
public record RefrescarTokenCommand
(
    string RefreshToken,
    AudienciaResponse Audiencia
) : IRequest<JsonObject>;
