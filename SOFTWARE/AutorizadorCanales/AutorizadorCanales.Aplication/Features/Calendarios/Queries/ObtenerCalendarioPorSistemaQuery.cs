using AutorizadorCanales.Core.DTO;
using MediatR;

namespace AutorizadorCanales.Aplication.Features.Calendarios.Queries;

/// <summary>
/// Datos para obtener calendario por sistema
/// </summary>
/// <param name="CodigoAgencia">Código de agencia</param>
/// <param name="CodigoSistema">Código de sistema</param>
public record ObtenerCalendarioPorSistemaQuery
(
    string CodigoAgencia,
    string CodigoSistema
) : IRequest<CalendarioDto>;
