using Microsoft.AspNetCore.Http;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

/// <summary>
/// Clase de dominio que representa a las Cuentas de Ahorro Corriente
/// </summary>
public class CuentaEfectivoSueldo
{
    #region Constantes
    public const string IndicadorCuentaSueldo = "IND_CUE_SUELDO";
    #endregion

    #region Propiedades
    /// <summary>
    /// Numero de cuenta
    /// </summary>
    public string NumeroCuenta { get; private set; }
    /// <summary>
    /// Codigo de empresa
    /// </summary>
    public string CodigoEmpresa { get; private set; }
    /// <summary>
    /// Saldo disponible remunerativo
    /// </summary>
    public decimal SaldoDisponibleRemu { get; private set; }
    /// <summary>
    /// Saldo disponible no remunerativo
    /// </summary>
    public decimal SaldoDisponibleNoRemu { get; private set; }
    /// <summary>
    /// Saldo transito remunerativo
    /// </summary>
    public decimal SaldoTransitoRemu { get; private set; }
    /// <summary>
    /// Saldo transito no remunerativo
    /// </summary>
    public decimal SaldoTransitoNoRemu { get; private set; }
    /// <summary>
    /// Saldo congelado remunerativo
    /// </summary>
    public decimal SaldoCongeladoRemu { get; private set; }
    /// <summary>
    /// Saldo congelado no remunerativo
    /// </summary>
    public decimal SaldoCongeladoNoRemu { get; private set; }
    /// <summary>
    /// Cuenta efectivo asociada
    /// </summary>
    public virtual CuentaEfectivo CuentaEfectivo { get; private set; }
    #endregion

    #region Métodos
    /// <summary>
    /// Método para crear la entidad
    /// </summary>
    /// <param name="codigoEmpresa">Codigo de empresa</param>
    /// <param name="numeroCuenta">Numero de cuenta</param>
    /// <param name="saldoDisponibleRemu">Saldo disponible remunerativo</param>
    /// <param name="saldoDisponibleNoRemu">Saldo disponible no remunerativo</param>
    /// <param name="saldoTransitoRemu">Saldo transito remunerativo</param>
    /// <param name="saldoTransitoNoRemu">Saldo transito no remunerativo</param>
    /// <param name="saldoCongeladoRemu">Saldo congelado remunerativo</param>
    /// <param name="saldoCongeladoNoRemu">Saldo congelado no remunerativo</param>
    /// <returns>Entidad cuenta efectivo sueldo</returns>
    public static CuentaEfectivoSueldo Crear(string codigoEmpresa, string numeroCuenta
        , decimal saldoDisponibleRemu, decimal saldoDisponibleNoRemu
        , decimal? saldoTransitoRemu, decimal saldoTransitoNoRemu
        , decimal? saldoCongeladoRemu, decimal saldoCongeladoNoRemu)
    {
        return new CuentaEfectivoSueldo()
        {
            CodigoEmpresa = codigoEmpresa,
            NumeroCuenta = numeroCuenta,
            SaldoDisponibleRemu = saldoDisponibleRemu,
            SaldoDisponibleNoRemu = saldoDisponibleNoRemu,
            SaldoTransitoRemu = (decimal)saldoTransitoRemu!,
            SaldoTransitoNoRemu = saldoTransitoNoRemu,
            SaldoCongeladoRemu = (decimal)saldoCongeladoRemu!,
            SaldoCongeladoNoRemu = saldoCongeladoNoRemu
        };
    }

    /// <summary>
    /// Metodo para actualizar los saldos
    /// </summary>
    /// <param name="saldoDisponibleRemu">Saldo disponible remunerativo</param>
    /// <param name="saldoDisponibleNoRemu">Saldo disponible no remunerativo</param>
    /// <param name="saldoTransitoRemu">Saldo transito remunerativo</param>
    /// <param name="saldoTransitoNoRemu">Saldo transito no remunerativo</param>
    /// <param name="saldoCongeladoRemu">Saldo congelado remunerativo</param>
    /// <param name="saldoCongeladoNoRemu">Saldo congelado no remunerativo</param>
    public void ActualizarSaldos(
        decimal saldoDisponibleRemu, 
        decimal saldoDisponibleNoRemu, 
        decimal saldoTransitoRemu, 
        decimal saldoTransitoNoRemu, 
        decimal saldoCongeladoRemu, 
        decimal saldoCongeladoNoRemu)
    {
        SaldoDisponibleRemu += saldoDisponibleRemu;
        SaldoDisponibleNoRemu += saldoDisponibleNoRemu;
        SaldoTransitoRemu += saldoTransitoRemu;
        SaldoTransitoNoRemu += saldoTransitoNoRemu;
        SaldoCongeladoRemu += saldoCongeladoRemu;
        SaldoCongeladoNoRemu += saldoCongeladoNoRemu;
    }
    #endregion
}