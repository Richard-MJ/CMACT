using MediatR;

namespace AutorizadorCanales.Aplication.Features.TramasProcesadas.Commands;

/// <summary>
/// Command para registrar datos de inicio de sesión
/// </summary>
/// <param name="IndicadorCanal">Indicador de canal</param>
/// <param name="IdClienteFinal">Id del cliente final (número de tarjeta)</param>
/// <param name="IdTerminal">Id de la terminal</param>
/// <param name="CodigoMensajeUno">Código de mensaje para registrar en la entidad</param>
/// <param name="FechaOperacion">Fecha de la operación</param>
public record RegistrarDatosInicioSesionCommand
(
    string IndicadorCanal,
    decimal IdClienteFinal,
    string IdTerminal,
    string CodigoMensajeUno,
    DateTime FechaOperacion
) : IRequest<int>;