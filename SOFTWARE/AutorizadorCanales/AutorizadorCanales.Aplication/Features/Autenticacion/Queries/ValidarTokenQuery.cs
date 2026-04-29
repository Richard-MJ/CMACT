using MediatR;

namespace AutorizadorCanales.Aplication.Features.Autenticacion.Queries;

/// <summary>
/// Query que valida el token de sesión
/// </summary>
public record ValidarTokenQuery() : IRequest<int>;
