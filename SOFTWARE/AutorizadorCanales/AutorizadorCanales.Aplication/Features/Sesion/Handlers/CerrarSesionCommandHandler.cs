using AutorizadorCanales.Aplication.Features.Sesion.Commands;
using AutorizadorCanales.Domain.Entidades;
using AutorizadorCanales.Domain.Entidades.SG;
using AutorizadorCanales.Domain.Repositorios;
using AutorizadorCanales.Logging.Interfaz;
using MediatR;
using System.Transactions;

namespace AutorizadorCanales.Aplication.Features.Sesion.Handlers;

public class CerrarSesionCommandHandler : IRequestHandler<CerrarSesionCommand, int>
{
    private readonly IContexto _contexto;
    private readonly IRepositorioEscritura _repositorioEscritura;

    public CerrarSesionCommandHandler(IRepositorioEscritura repositorioEscritura, IContexto contexto)
    {
        _repositorioEscritura = repositorioEscritura;
        _contexto = contexto;
    }

    /// <summary>
    /// Handler del comando CerrarSesión
    /// </summary>
    /// <param name="command">Comando a ejecutar</param>
    /// <returns></returns>
    public async Task<int> Handle(CerrarSesionCommand command, CancellationToken cancellationToken)
    {
        var sesionCanalElectronico = await _repositorioEscritura.ObtenerPrimeroPor<SesionCanalElectronico>
            (x => x.TokenGuid == command.TokenGuid && x.IndicadorEstado == EstadoEntidad.ACTIVO);

        if (sesionCanalElectronico != null)
        {
            using (var transaccion = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                sesionCanalElectronico.CerrarSesion(_contexto.FechaSistema);
                await _repositorioEscritura.GuardarCambiosAsync();

                transaccion.Complete();
            }
        }

        return 1;
    }
}
