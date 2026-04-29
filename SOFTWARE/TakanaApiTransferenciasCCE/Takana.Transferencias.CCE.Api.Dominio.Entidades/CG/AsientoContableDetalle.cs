using static Takana.Transferencias.CCE.Api.Dominio.Entidades.CF.Moneda;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;

/// <summary>
/// Clase de asiento contable detalle
/// </summary>
public class AsientoContableDetalle : Empresa
{
    #region Constantes

    /// <summary>
    /// Codigo de debito
    /// </summary>
    public const string CodigoDebito = "D";
    /// <summary>
    /// Codigo de credito
    /// </summary>
    public const string CodigoCredito = "C";
    /// <summary>
    /// Indicador Entrante
    /// </summary>
    public const string IndicadorEntrante = "E";
    /// <summary>
    /// Indicador Saliente
    /// </summary>
    public const string IndicadorSaliente = "S";
    /// <summary>
    /// Decimales por defecto
    /// </summary>
    public const short DecimalesPorDefecto = 2;
    /// <summary>
    /// Monto cero
    /// </summary>
    public const int MontoCero = 0;
    /// <summary>
    /// Mismo titular version CCE
    /// </summary>
    public const short DecimalesPorDefectoEnAsientosContables = 2;
    /// <summary>
    /// Otro titular version CCE
    /// </summary>
    public const short DecimalesTipoCambioEnAsientosContables = 4;
    #endregion

    #region Propiedades
    /// <summary>
    /// Numero de asiento
    /// </summary>
    public int NumeroAsiento { get; private set; }
    /// <summary>
    /// Numero de linea
    /// </summary>
    public int NumeroLinea { get; private set; }
    /// <summary>
    /// Numero de cuenta contable
    /// </summary>
    public string NumeroCuentaContable { get; private set; }
    /// <summary>
    /// Monto debitado de la cuenta en la moneda original de la cuenta
    /// </summary>
    public decimal Debito_Cta { get; private set; }
    /// <summary>
    /// Monto crédito a la cuenta en la moneda original de la cuenta
    /// </summary>
    public decimal Credito_Cta { get; private set; }
    /// <summary>
    /// Monto debitado de la cuenta en la moneda base (soles)
    /// </summary>
    public decimal Debito { get; private set; }
    /// <summary>
    /// Monto crédito a la cuenta en la moneda base (soles)
    /// </summary>
    public decimal Credito { get; private set; }
    /// <summary>
    /// Codigo de unidad ejecutora
    /// </summary>
    public string CodigoUnidadEjecutora { get; private set; }
    /// <summary>
    /// Codigo de agencia
    /// </summary>
    public string CodigoAgencia { get; private set; }
    /// <summary>
    /// Fecha de movimiento
    /// </summary>
    public DateTime FechaMovimiento { get; private set; }
    /// <summary>
    /// Detalle de asiento
    /// </summary>
    public string DetalleAsiento { get; private set; }
    /// <summary>
    /// Tipo de cambio base
    /// </summary>
    public decimal TipoCambioBase { get; private set; }
    /// <summary>
    /// Tipo de cambio cuenta
    /// </summary>
    public decimal TipoCambioCuenta { get; private set; }
    /// <summary>
    /// Referencia
    /// </summary>
    public string Referencia { get; private set; }
    /// <summary>
    /// Entidad de asiento contable
    /// </summary>
    public virtual AsientoContable Asiento { get; private set; }

    /// <summary>
    /// Entidad de cuenta contable
    /// </summary>
    private CuentaContable _cuentaContable;
    /// <summary>
    /// Entidad de cuenta contable
    /// </summary>
    public virtual CuentaContable CuentaContable
    {
        get { return _cuentaContable; }
        set
        {
            _cuentaContable = value;
            NumeroCuentaContable = value.NumeroCuentaContable;
        }
    }

    #endregion

    #region Métodos
    /// <summary>
    /// Generar detalle de asiento contable
    /// </summary>
    /// <param name="asiento"></param>
    /// <param name="tipoMovimiento"></param>
    /// <param name="cuentaContable"></param>
    /// <param name="monto"></param>
    /// <param name="codigoUnidad"></param>
    /// <param name="detalle"></param>
    /// <param name="referencia"></param>
    /// <param name="tipoCambioBase"></param>
    /// <param name="tipoCambioCuenta"></param>
    /// <returns></returns>
    public static AsientoContableDetalle Generar(AsientoContable asiento,
        string tipoMovimiento, string cuentaContable, decimal monto, 
        string codigoUnidad, string detalle, string referencia,
        decimal tipoCambioBase, decimal tipoCambioCuenta
        )
    {
        string codigoMoneda = cuentaContable.Substring(2, 1);
        return new AsientoContableDetalle()
        {
            NumeroAsiento = asiento.NumeroAsiento,
            Debito = tipoMovimiento == CodigoDebito 
                ? (codigoMoneda == ((int)MonedaCodigo.Soles).ToString() ? monto 
                : Math.Round(monto * tipoCambioBase, DecimalesPorDefecto)) : MontoCero,
            Credito = tipoMovimiento == CodigoCredito 
                ? (codigoMoneda == ((int)MonedaCodigo.Soles).ToString() ? monto 
                : Math.Round(monto * tipoCambioBase, DecimalesPorDefecto)) : MontoCero,
            NumeroCuentaContable = cuentaContable,
            CodigoEmpresa = asiento.CodigoEmpresa,
            CodigoAgencia = asiento.CodigoAgencia,
            NumeroLinea = asiento.Detalles.Count + 1,
            FechaMovimiento = asiento.FechaMovimiento,
            Debito_Cta = tipoMovimiento == CodigoDebito ? monto : MontoCero,
            Credito_Cta = tipoMovimiento == CodigoCredito ? monto : MontoCero,
            DetalleAsiento = detalle,
            TipoCambioBase = tipoCambioBase,
            TipoCambioCuenta = codigoMoneda == ((int)MonedaCodigo.Soles).ToString() 
                ? tipoCambioCuenta : 1,
            Referencia = referencia,
            CodigoUnidadEjecutora = codigoUnidad
        };

    }
    public bool EsCredito
        => Credito > 0m;

    #endregion
}
