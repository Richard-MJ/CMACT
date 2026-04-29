using AutorizadorCanales.Contracts.SG.Response;
using MediatR;

namespace AutorizadorCanales.Aplication.Features.Audiencias.Queries;

public record ObtenerAudienciaPorIdQuery
(
    string IdAudiencia
) : IRequest<AudienciaResponse>;
