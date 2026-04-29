using MediatR;

namespace AutorizadorCanales.Aplication.Features.TramasProcesadas.Commands;

/// <summary>
/// Comando que se usa para registrar la respuesta de operación
/// </summary>
/// <param name="IdTrama">Id de la trama</param>
/// <param name="CodigoRespuesta">Código de la respuesta</param>
/// <param name="IdMovimientoTts">Id del movimiento</param>
/// <param name="NumeroAutorizacion">Número de autorización</param>
/// <param name="FechaModificacion">Fecha de modificación</param>
public record RegistrarRespuestaOperacionCommand
(
    int IdTrama,
    string CodigoRespuesta,
    int IdMovimientoTts,
    string NumeroAutorizacion,
    DateTime FechaModificacion
) : IRequest<int>;
