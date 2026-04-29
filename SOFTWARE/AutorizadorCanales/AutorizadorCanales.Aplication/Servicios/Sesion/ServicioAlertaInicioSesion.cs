using AutorizadorCanales.Aplication.Common.ServicioColas;
using AutorizadorCanales.Aplication.Common.ServicioColas.DTOs;
using AutorizadorCanales.Aplication.Servicios;
using AutorizadorCanales.Aplication.Servicios.Sesion;
using AutorizadorCanales.Core.Constantes;
using AutorizadorCanales.Domain.Entidades;
using AutorizadorCanales.Domain.Entidades.CF;
using AutorizadorCanales.Domain.Entidades.CL;
using AutorizadorCanales.Domain.Entidades.SG;
using AutorizadorCanales.Domain.Entidades.TJ;
using AutorizadorCanales.Domain.Repositorios;
using AutorizadorCanales.Logging.Interfaz;

namespace AutorizadorCanales.Infrastructure.Servicios;

public class ServicioAlertaInicioSesion : ServicioBase<ServicioAlertaInicioSesion>, IServicioAlertaInicioSesion
{
    private readonly IRepositorioLectura _repositorioLectura;
    private readonly IRepositorioEscritura _repositorioEscritura;
    private readonly IServicioColasCore _servicioColasCore;

    public ServicioAlertaInicioSesion(
        IRepositorioLectura lectura,
        IRepositorioEscritura escritura,
        IServicioColasCore colas,
        IContexto contexto,
        IBitacora<ServicioAlertaInicioSesion> bitacora) : base(contexto, bitacora)
    {
        _repositorioLectura = lectura;
        _repositorioEscritura = escritura;
        _servicioColasCore = colas;
    }

    public async Task<DispositivoCanalElectronico?> RegistrarAlertaInicioSesion(
        string estado,
        string numeroTarjeta,
        string? idRegistroNuevoDispositivo = null,
        bool adjuntarDocumento = false,
        decimal numeroMovimiento = 0)
    {
        var tarjeta = await _repositorioLectura.ObtenerPorCodigoAsync<Tarjeta>(decimal.Parse(numeroTarjeta));

        var dispositivo = tarjeta.DispositivosCanalElectronico
            .FirstOrDefault(x => x.DispositivoId == idRegistroNuevoDispositivo);

        var movimiento = numeroMovimiento == 0
            ? await CrearNuevoMovimiento(tarjeta, estado)
            : await _repositorioLectura.ObtenerPorCodigoAsync<TarjetaMovimiento>(numeroMovimiento);

        var alerta = CrearAlerta(tarjeta, estado, dispositivo, movimiento.NumeroMovimiento);

        await _repositorioEscritura.AdicionarAsync(alerta);
        await _repositorioEscritura.GuardarCambiosAsync();

        Notificar(movimiento);

        return dispositivo;
    }

    private async Task<TarjetaMovimiento> CrearNuevoMovimiento(Tarjeta tarjeta, string estado)
    {
        var numeroMovimiento = await _repositorioLectura.ObtenerNumeroSerieNoBloqueante("%", "TJ", "SEC_MOVIMT", 1);

        var subTipo = await _repositorioLectura.ObtenerPorCodigoAsync<SubTipoTransaccion>(
            EntidadEmpresa.EMPRESA,
        Sistema.TARJETA,
            SubTipoTransaccion.TJ_TIP_AFILIACION_CANALES_ELECTRONICOS,
            estado == EstadoEntidad.SI
                ? SubTipoTransaccion.TJ_SUB_TIP_INICIO_SESION
                : SubTipoTransaccion.TJ_SUB_TIP_INICIO_SESION_FALLIDO);

        var movimiento = TarjetaMovimiento.Crear(
        numeroMovimiento,
            tarjeta,
            Contexto.CodigoUsuario,
            Contexto.CodigoAgencia,
            subTipo,
            Contexto.FechaSistema,
            EstadoEntidad.APLICADO,
            subTipo.DescripcionSubTransaccion,
            Contexto.IndicadorCanal);

        var notificada = OperacionNotificada.Crear(
            movimiento.NumeroMovimiento.ToString(),
            movimiento.CodigoTipoTransaccion,
            movimiento.CodigoSubTipoTransaccion,
            Sistema.TARJETA,
            Contexto.IndicadorCanal,
            Contexto.IndicadorSubCanal.ToString(),
            Contexto.FechaSistema);

        await _repositorioEscritura.AdicionarAsync(notificada);
        await _repositorioEscritura.AdicionarAsync(movimiento);

        return movimiento;
    }

    private AlertaInicioSesion CrearAlerta(
        Tarjeta tarjeta,
        string estado,
        DispositivoCanalElectronico? dispositivo,
        decimal numeroMovimiento)
    {
        return AlertaInicioSesion.Crear(
            tarjeta.NumeroTarjeta,
            estado,
            Contexto.DireccionIp,
            Contexto.SistemaOperativo,
            Contexto.Navegador,
            Contexto.IndicadorCanal,
            dispositivo?.DispositivoId ?? "No registrado",
            Contexto.FechaSistema,
            Contexto.ModeloDispositivo,
            numeroMovimiento);
    }

    private void Notificar(TarjetaMovimiento movimiento)
    {
        _servicioColasCore.EnviarNotificacion(new DtoOperacionNotificacionCola
        {
            NumeroMovimiento = movimiento.NumeroMovimiento.ToString(),
            CodigoAgenciaMovimiento = movimiento.CodigoAgencia,
            CodigoTipoTransaccion = movimiento.CodigoTipoTransaccion,
            CodigoSubtipoTransaccion = movimiento.CodigoSubTipoTransaccion,
            CodigoSistema = Sistema.TARJETA
        });
    }
}
