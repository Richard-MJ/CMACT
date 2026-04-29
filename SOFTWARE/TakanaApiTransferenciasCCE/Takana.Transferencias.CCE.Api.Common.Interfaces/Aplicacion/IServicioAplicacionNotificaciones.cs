using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.TJ;

namespace Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;

public interface IServicioAplicacionNotificaciones
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="tarjeta"></param>
    /// <param name="codigoTipo"></param>
    /// <param name="codigoSubTipo"></param>
    /// <param name="fechaSistema"></param>
    /// <param name="numeroMovimientoFuente"></param>
    /// <param name="descripcionMovimiento"></param>
    /// <returns></returns>
    Task<TarjetaMovimiento> GenerarNotificacionTarjeta(
        Tarjeta tarjeta,
        string codigoTipo,
        string codigoSubTipo,
        DateTime fechaSistema,
        decimal numeroMovimientoFuente = 0,
        string? descripcionMovimiento = null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="movimiento"></param>
    /// <returns></returns>
    Task EnviarNotificacionTarjeta(
        TarjetaMovimiento movimiento);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="movimiento"></param>
    /// <returns></returns>
    Task EnviarNotificacionCuentaEfectivo(
        Movimiento movimiento);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="numeroMovimiento"></param>
    /// <param name="subTipoTransaccion"></param>
    /// <param name="fechaSistema"></param>
    /// <returns></returns>
    Task GenerarOperacionNotificada(
        string numeroMovimiento, 
        SubTipoTransaccion subTipoTransaccion, 
        DateTime fechaSistema);
}
