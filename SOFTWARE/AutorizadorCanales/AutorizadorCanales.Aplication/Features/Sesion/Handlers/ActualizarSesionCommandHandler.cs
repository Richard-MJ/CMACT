using AutorizadorCanales.Aplication.Features.Sesion.Commands;
using AutorizadorCanales.Domain.Entidades.SG;
using AutorizadorCanales.Domain.Repositorios;
using MediatR;
using System.Transactions;

namespace AutorizadorCanales.Aplication.Features.Sesion.Handlers;

public class ActualizarSesionCommandHandler : IRequestHandler<ActualizarSesionCommand, int>
{
    private readonly IRepositorioEscritura _repositorioEscritura;

    public ActualizarSesionCommandHandler(IRepositorioEscritura repositorioEscritura)
    {
        _repositorioEscritura = repositorioEscritura;
    }

    /// <summary>
    /// Handler del comando actualizar sesión
    /// </summary>
    /// <param name="command">Comando a ejecutar</param>
    /// <returns></returns>
    public async Task<int> Handle(ActualizarSesionCommand command, CancellationToken cancellationToken)
    {
        var sesionCanalElectronico = await _repositorioEscritura.ObtenerPrimeroPor<SesionCanalElectronico>
          (x => x.TokenGuid == command.TokenGuid);

        if (sesionCanalElectronico != null)
        {
            using (var transaccion = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                sesionCanalElectronico.ActualizarConexion(command.IdConexion);
                await _repositorioEscritura.GuardarCambiosAsync();

                transaccion.Complete();
            }
        }

        return 1;
    }
}
