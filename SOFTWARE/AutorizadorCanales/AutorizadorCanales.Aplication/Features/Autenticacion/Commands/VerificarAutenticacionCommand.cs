using AutorizadorCanales.Contracts.SG.Response;
using MediatR;
using System.Security.Claims;

namespace AutorizadorCanales.Aplication.Features.Autenticacion.Commands;

/// <summary>
/// Comando para verificar la autenticación
/// </summary>
/// <param name="IdVerificacion">Id de la verificación</param>
/// <param name="CodigoAutorizacion">Código de autorización</param>
/// <param name="IdVisual">Id visual</param>
/// <param name="NewGuid">Guid del dispositivo</param>
/// <param name="Claims">Claims de la sesión</param>
/// <param name="Audiencia">Audiencia</param>
public record VerificarAutenticacionCommand
(
    string IdVerificacion,
    string CodigoAutorizacion,
    string IdVisual,
    string NewGuid,
    List<Claim> Claims,
    AudienciaResponse Audiencia
) : IRequest<VerificacionAutenticacionResponse>;
