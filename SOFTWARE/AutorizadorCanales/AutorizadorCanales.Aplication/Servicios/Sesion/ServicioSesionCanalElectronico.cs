using AutorizadorCanales.Aplication.Servicios;
using AutorizadorCanales.Aplication.Servicios.Sesion;
using AutorizadorCanales.Domain.Entidades;
using AutorizadorCanales.Domain.Entidades.CL;
using AutorizadorCanales.Domain.Entidades.SG;
using AutorizadorCanales.Domain.Repositorios;
using AutorizadorCanales.Logging.Interfaz;
using System.Transactions;

namespace AutorizadorCanales.Infrastructure.Servicios;

public class ServicioSesionCanalElectronico : ServicioBase<ServicioSesionCanalElectronico>, IServicioSesionCanalElectronico
{
    private readonly IRepositorioEscritura _repositorioEscritura;

    public ServicioSesionCanalElectronico(
        IRepositorioEscritura repositorioEscritura,
        IContexto contexto,
        IBitacora<ServicioSesionCanalElectronico> bitacora) : base(contexto, bitacora)
    {
        _repositorioEscritura = repositorioEscritura;
    }

    public async Task CrearSesionCanalElectronico(bool esDispositivoNuevo, DispositivoCanalElectronico? dispositivoCanalElectronico, string tokenGuid)
    {
        if (dispositivoCanalElectronico == null || esDispositivoNuevo)
            return;

        var sesionesActivas = await _repositorioEscritura.ObtenerPorExpresionConLimiteAsync<SesionCanalElectronico>
            (x => x.NumeroTarjeta == dispositivoCanalElectronico.NumeroTarjeta
                && x.IndicadorCanal == Contexto.IndicadorCanal && x.IndicadorEstado == EstadoEntidad.ACTIVO);

        sesionesActivas.ForEach(x => x.CerrarSesion(Contexto.FechaSistema));

        using (var transaccion = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var nuevaSesion = SesionCanalElectronico.Crear(
                dispositivoCanalElectronico,
                Contexto.DireccionIp,
                Contexto.SistemaOperativo,
                Contexto.Navegador,
                Contexto.ModeloDispositivo,
                tokenGuid,
                Contexto.IndicadorCanal,
                Contexto.FechaSistema);

            await _repositorioEscritura.AdicionarAsync(nuevaSesion);
            await _repositorioEscritura.GuardarCambiosAsync();

            transaccion.Complete();
        }
    }
}
