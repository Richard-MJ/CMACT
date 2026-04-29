using MediatR;

namespace AutorizadorCanales.Aplication.Features.Sesion.Commands;

/// <summary>
/// Comando que actualiza la sesión
/// </summary>
/// <param name="TokenGuid">Identificador único del token de sesión</param>
/// <param name="IdConexion">Id de conexión SignalR</param>
public record ActualizarSesionCommand(
    string TokenGuid,
    string IdConexion) : IRequest<int>;
