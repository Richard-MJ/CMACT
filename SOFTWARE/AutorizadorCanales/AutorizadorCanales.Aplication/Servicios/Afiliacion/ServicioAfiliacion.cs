using AutorizadorCanales.Aplication.Common.ServicioPinOperacionesApi;
using AutorizadorCanales.Aplication.Servicios;
using AutorizadorCanales.Aplication.Servicios.Afiliacion;
using AutorizadorCanales.Domain.Entidades;
using AutorizadorCanales.Domain.Entidades.CL;
using AutorizadorCanales.Domain.Entidades.TJ;
using AutorizadorCanales.Domain.Repositorios;
using AutorizadorCanales.Domain.Validaciones;
using AutorizadorCanales.Excepciones;
using AutorizadorCanales.Logging.Interfaz;
using System.Transactions;

namespace AutorizadorCanales.Infrastructure.Servicios;
public class ServicioAfiliacion : ServicioBase<ServicioAfiliacion>, IServicioAfiliacion
{
    private readonly IServicioPinOperaciones _servicioPinOperaciones;
    private readonly IRepositorioEscritura _repositorioEscritura;

    public ServicioAfiliacion(
        IServicioPinOperaciones servicioPinOperaciones,
        IRepositorioEscritura repositorioEscritura,
        IContexto contexto,
        IBitacora<ServicioAfiliacion> bitacora) : base(contexto, bitacora)
    {
        _servicioPinOperaciones = servicioPinOperaciones;
        _repositorioEscritura = repositorioEscritura;
    }

    public async Task AfiliarCanalesElectronicos(string idAudiencia, string passwordCajero, string passwordInternet, int numeroDiasVencimiento, Tarjeta tarjeta)
    {
        tarjeta.ValidarTarjetaAfiliacion();

        var clientesApi = await _repositorioEscritura.ObtenerPorExpresionConLimiteAsync<ClienteApi>
            (c => c.IdSistemaCliente == idAudiencia && c.CodigoCliente == tarjeta.Duenio!.CodigoCliente
            && (c.IndicadorEstado == ClienteApi.AFILIADO || c.IndicadorEstado == ClienteApi.BLOQUEADO));

        clientesApi.ForEach(cliente =>
        {
            cliente.Desafiliar();
        });

        if (Contexto.EsCanalEmpresarial())
        {
            var tarjetaEmpresarial = await _repositorioEscritura.ObtenerPorCodigoAsync<TarjetaHomebankingEmpresarial>
                (EntidadEmpresa.EMPRESA, tarjeta.NumeroTarjeta);

            tarjeta.PrepararHomeBankingEmpresarial(Contexto.FechaSistema);
            tarjetaEmpresarial.ActivarAfiliacion(Contexto.CodigoUsuario, Contexto.FechaSistema);
        }
        else
            tarjeta.HabilitarAfiliacionHomeBankingPersonal(Contexto.FechaSistema);

        var pinblock = await _servicioPinOperaciones
           .TrasladaPINBlock(tarjeta.NumeroTarjetaString, passwordCajero.Trim());
        var (pinBlock1, pinBlock2) = await _servicioPinOperaciones
            .ObtenerPinBlock(passwordInternet.Trim(), tarjeta.NumeroTarjetaString);

        if (!await _servicioPinOperaciones.ValidarClave(tarjeta.NumeroTarjetaString, pinblock, tarjeta.NumeroPvv2!))
            throw ExcepcionAUsuario.ExcepcionAfiliacionInicioSesion();

        var pvv1 = await _servicioPinOperaciones.GenerarPvv(tarjeta.NumeroTarjetaString, pinBlock1);
        var pvv2 = await _servicioPinOperaciones.GenerarPvv(tarjeta.NumeroTarjetaString, pinBlock2);
        tarjeta.RealizarActualizacionPvvHomeBanking(pvv1, pvv2);

        var nuevoClienteApi = ClienteApi.Crear(idAudiencia, tarjeta);

        using (var transaccion = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            if (tarjeta.AfiliadaServicioSms)
            {
                var afiliacionCanalElectronico = await _repositorioEscritura.ObtenerPorCodigoAsync<AfiliacionCanalElectronico>
                    (tarjeta.AfiliacionCanalElectronico.IdAfiliacionCanalElectronico);
                afiliacionCanalElectronico.DesactivarPrimerFactorAutenticacion
                    (Contexto.CodigoUsuario, Contexto.IdTerminal);
            }

            var nuevaAfiliacionCanalElectronico = AfiliacionCanalElectronico.CrearPrimerFactorAutenticacion(
                nuevoClienteApi,
                Contexto.CodigoUsuario,
                Contexto.IdTerminal,
                Contexto.FechaSistema.AddDays(numeroDiasVencimiento),
                Contexto.ModeloDispositivo,
                Contexto.DireccionIp,
                Contexto.Navegador,
                Contexto.SistemaOperativo);

            await _repositorioEscritura.AdicionarAsync(nuevoClienteApi);
            await _repositorioEscritura.AdicionarAsync(nuevaAfiliacionCanalElectronico);
            await _repositorioEscritura.GuardarCambiosAsync();
            transaccion.Complete();
        }
    }
}
