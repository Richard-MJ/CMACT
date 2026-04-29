using AutorizadorCanales.Aplication.Features.TramasProcesadas.Commands;
using AutorizadorCanales.Domain.Entidades.CC;
using AutorizadorCanales.Domain.Repositorios;
using MediatR;
using System.Transactions;

namespace AutorizadorCanales.Aplication.Features.TramasProcesadas.Handlers;

public class RegistrarRespuestaOperacionCommandHandler : IRequestHandler<RegistrarRespuestaOperacionCommand, int>
{
    private readonly IRepositorioEscritura _repositorioEscritura;

    public RegistrarRespuestaOperacionCommandHandler(IRepositorioEscritura repositorioEscritura)
    {
        _repositorioEscritura = repositorioEscritura;
    }

    /// <summary>
    /// Maneja el comando de registrar repuesta de la operación de trama procesada
    /// </summary>
    /// <param name="command">datos del comando</param>
    /// <param name="cancellationToken">token de cancelación</param>
    /// <returns>Id del registro de la trama</returns>
    public async Task<int> Handle(RegistrarRespuestaOperacionCommand command, CancellationToken cancellationToken)
    {
        var tramaProcesada = await _repositorioEscritura.ObtenerPorCodigoAsync<TramaProcesada>(command.IdTrama);

        tramaProcesada.ProcesarTrama(command.IdMovimientoTts, command.CodigoRespuesta, command.NumeroAutorizacion, command.FechaModificacion);

        using (var transaccion = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _repositorioEscritura.GuardarCambiosAsync();
            transaccion.Complete();
        }

        return tramaProcesada.Id;
    }
}
