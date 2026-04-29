using MediatR;

namespace AutorizadorCanales.Aplication.Features.Sesion.Commands;

/// <summary>
/// Comando encargado de cerrar la sesión
/// </summary>
/// <param name="TokenGuid">Identificador único del token de sesión</param>
public record CerrarSesionCommand
    (string TokenGuid) : IRequest<int>;
