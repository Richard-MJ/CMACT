using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.TJ;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Common.DTOs.BitacoraNotificaciones;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Servicios;

public class ServicioAplicacionNotificaciones : IServicioAplicacionNotificaciones
{
    private readonly IServicioAplicacionColas _servicioAplicacionColas;
    private readonly IRepositorioOperacion _repositorioOperacion;
    private readonly IRepositorioGeneral _repositorioGeneral;
    private readonly IContextoAplicacion _contexto;

    public ServicioAplicacionNotificaciones(
        IServicioAplicacionColas servicioAplicacionColas,
        IContextoAplicacion contexto,
        IRepositorioOperacion repositorioOperacion,
        IRepositorioGeneral repositorioGeneral)
    {
        _servicioAplicacionColas = servicioAplicacionColas;
        _contexto = contexto;
        _repositorioOperacion = repositorioOperacion;
        _repositorioGeneral = repositorioGeneral;
    }

    public Task EnviarNotificacionTarjeta(TarjetaMovimiento movimiento)
    {
        return _servicioAplicacionColas.EnviarNotificacionBitacora(new OperacionNotificacion
        {
            NumeroMovimiento = movimiento.IdMovimento.ToString(),
            CodigoTipoTransaccion = movimiento.CodigoTipoTransaccion,
            CodigoSubtipoTransaccion = movimiento.CodigoSubTipoTransaccion,
            CodigoSistema = Sistema.Tarjetas,
            CodigoAgenciaMovimiento = movimiento.CodigoAgencia,
            CodigoCanal = _contexto.IndicadorCanal,
        });
    }

    public Task EnviarNotificacionCuentaEfectivo(Movimiento movimiento)
    {
        return _servicioAplicacionColas.EnviarNotificacionBitacora(new OperacionNotificacion
        {
            NumeroMovimiento = movimiento.NumeroMovimiento.ToString(),
            CodigoTipoTransaccion = movimiento.CodigoTipoTransaccion,
            CodigoSubtipoTransaccion = movimiento.CodigoSubTipoTransaccion,
            CodigoSistema = Sistema.CuentaEfectivo,
            CodigoAgenciaMovimiento = movimiento.CodigoAgencia,
            CodigoCanal = _contexto.IndicadorCanal,
        });
    }

    public async Task GenerarOperacionNotificada(string numeroMovimiento, SubTipoTransaccion subTipoTransaccion, DateTime fechaSistema)
    {
        var notificada = OperacionNotificada.Crear(
          numeroMovimiento,
          subTipoTransaccion.CodigoTipoTransaccion,
          subTipoTransaccion.CodigoSubTipoTransaccion,
          subTipoTransaccion.CodigoSistema,
          _contexto.IndicadorCanal,
          _contexto.IndicadorSubCanal.ToString(),
          fechaSistema);

        await _repositorioOperacion.AdicionarAsync(notificada);
    }

    public async Task<TarjetaMovimiento> GenerarNotificacionTarjeta(
        Tarjeta tarjeta,
        string codigoTipo,
        string codigoSubTipo,
        DateTime fechaSistema,
        decimal numeroMovimientoFuente = 0,
        string? descripcionMovimiento = null)
    {
        int numeroMovimientoPrincipal = await _repositorioGeneral
            .ObtenerNumeroSerieNoBloqueanteAsync("%", Sistema.Tarjetas, TarjetaMovimiento.CODIGO_SERIE, 1);

        var subTipo = await _repositorioOperacion.ObtenerPorCodigoAsync<SubTipoTransaccion>
            (Empresa.CodigoPrincipal, Sistema.Tarjetas, codigoTipo, codigoSubTipo);

        var movimiento = TarjetaMovimiento.Crear(
            numeroMovimientoPrincipal,
            tarjeta,
            _contexto.CodigoUsuario,
            _contexto.CodigoAgencia,
            subTipo,
            fechaSistema,
            "A",
            subTipo.DescripcionSubTransaccion,
            _contexto.IndicadorCanal);

        if (numeroMovimientoFuente != 0)
            movimiento.AsignarMovimientoFuente(numeroMovimientoFuente, descripcionMovimiento);

        var notificada = OperacionNotificada.Crear(
           movimiento.IdMovimento.ToString(),
           movimiento.CodigoTipoTransaccion,
           movimiento.CodigoSubTipoTransaccion,
           Sistema.Tarjetas,
           _contexto.IndicadorCanal,
           _contexto.IndicadorSubCanal.ToString(),
           fechaSistema);

        await _repositorioOperacion.AdicionarAsync(movimiento);
        await _repositorioOperacion.AdicionarAsync(notificada);

        return movimiento;
    }
}
