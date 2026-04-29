using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using static Takana.Transferencias.CCE.Api.Dominio.Entidades.CF.Moneda;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;

/// <summary>
/// Clase de dominio que representa a una Entidad de Asiento Contable
/// </summary>
public class AsientoContable : Empresa
{
    #region Constancia
    /// <summary>
    /// Constante de debe
    /// </summary>
    public const string Debe = "D";
    /// <summary>
    /// Constante de haber
    /// </summary>
    public const string Haber = "H";
    /// <summary>
    /// Estado pendiente
    /// </summary>
    public const string EstadoPendiente = "P";
    /// <summary>
    /// Estado incompleto
    /// </summary>
    public const string EstadoIncompleto = "I";
    /// <summary>
    /// Estado Aplicado
    /// </summary>
    public const string EstadoAplicado = "A";
    /// <summary>
    /// Estado Anulado
    /// </summary>
    public const string EstadoAnulado = "N";
    /// <summary>
    /// Estado Reservado
    /// </summary>
    public const string EstadoReversado = "R";
    /// <summary>
    /// No liquidacion
    /// </summary>
    public const string NoLiquidacion = "N";
    /// <summary>
    /// Si liquidacion
    /// </summary>
    public const string SiLiquidacion = "S";
    /// <summary>
    /// Serie de asiento en CC
    /// </summary>
    public const string CodigoSerieAsientoEnCC = "CONS_ASTO";
    /// <summary>
    /// Codigo de unidad ejecutora principal
    /// </summary>
    public const string CodigoUnidadEjecutora = "01999999999999999";
    /// <summary>
    /// Descripcion de asiento tin inmediata entrante
    /// </summary>
    public const string DescripcionAsientoTinInmediataEntrante = "TRANSFERENCIA INTERBANCARIA INMEDIATA CCE ENTRANTE";
    /// <summary>
    /// Descripcion de asiento tin inmediata entrante
    /// </summary>
    public const string DescripcionAsientoComisionTinInmediataEntrante = "COMISIÓN TRANSFERENCIA INTERBANCARIA INMEDIATA CCE ENTRANTE";    
    /// <summary>
    /// Cuenta contable valida
    /// </summary>
    public const string cuentaContableValida = "4";
    /// <summary>
    /// Cuenta contable valida
    /// </summary>
    public const bool considerarMovimientoPrincipal = true;
    #endregion

    #region Propiedades
    /// <summary>
    /// Codigo de agencia
    /// </summary>
    public string CodigoAgencia { get; private set; }
    /// <summary>
    /// Numero de asiento
    /// </summary>
    public int NumeroAsiento { get; private set; }
    /// <summary>
    /// Codigo de sistem
    /// </summary>
    public string CodigoSistema { get; private set; }
    /// <summary>
    /// Codigo de tipo de transaccion
    /// </summary>
    public string CodigoTipoTransaccion { get; private set; }
    /// <summary>
    /// Codigo de sub tipo de transaccion
    /// </summary>
    public string CodigoSubTipoTransaccion { get; private set; }
    /// <summary>
    /// Deatlle de asiento
    /// </summary>
    public string DetalleAsiento { get; private set; }
    /// <summary>
    /// Fecha asiento
    /// </summary>
    public DateTime FechaAsiento { get; private set; }
    /// <summary>
    /// Estado de asiento
    /// </summary>
    public string EstadoAsiento { get; private set; }
    /// <summary>
    /// Fecha de movimiento
    /// </summary>
    public DateTime FechaMovimiento { get; private set; }
    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime FechaRegistro { get; private set; }
    /// <summary>
    /// Indicador de liquidacion
    /// </summary>
    public string IndLiquidacion { get; private set; }
    /// <summary>
    /// Codigo de usuario
    /// </summary>
    public string CodigoUsuario { get; private set; }
    /// <summary>
    /// Detalles de asiento contable
    /// </summary>
    public virtual ICollection<AsientoContableDetalle> Detalles { get; private set; }
    #endregion

    #region Métodos
    /// <summary>
    /// Genera el asiento contable
    /// </summary>
    /// <param name="numeroAsiento"></param>
    /// <param name="codigoEmpresa"></param>
    /// <param name="codigoAgencia"></param>
    /// <param name="codigoSistema"></param>
    /// <param name="tipoTransaccion"></param>
    /// <param name="subTipoTransaccion"></param>
    /// <param name="fechaAsiento"></param>
    /// <param name="descripcion"></param>
    /// <param name="liquidacion"></param>
    /// <param name="usuario"></param>
    /// <returns></returns>
    public static AsientoContable Generar(int numeroAsiento, string codigoEmpresa,
    string codigoAgencia, string codigoSistema, string tipoTransaccion, string subTipoTransaccion,
    DateTime fechaAsiento, string descripcion, string liquidacion, string usuario)
    {
        return new AsientoContable()
        {
            NumeroAsiento = numeroAsiento,
            CodigoSistema = codigoSistema,
            CodigoTipoTransaccion = tipoTransaccion,
            CodigoSubTipoTransaccion = subTipoTransaccion,
            FechaAsiento = fechaAsiento,
            EstadoAsiento = EstadoIncompleto,
            CodigoUsuario = usuario,
            IndLiquidacion = liquidacion,
            DetalleAsiento = descripcion,
            CodigoAgencia = codigoAgencia,
            CodigoEmpresa = codigoEmpresa,
            FechaMovimiento = fechaAsiento,
            FechaRegistro = fechaAsiento,
            Detalles = new List<AsientoContableDetalle>()
        };
    }
    /// <summary>
    /// Cerar el asiento
    /// </summary>
    /// <param name="estado"></param>
    public void CerrarAsiento(string estado)
    {
        this.EstadoAsiento = estado;
    }

    /// <summary>
    /// Metodo que indica si se encuentra el asiento en estado pendiente para mejor legibilidad del código
    /// </summary>
    /// <returns>Verdadero o Falso</returns>
    public bool EstaPendienteAplicacion()
    {
        return EstadoAsiento == EstadoPendiente;
    }

    /// <summary>
    /// Devuelve la diferencia de la suma de montos de Debitos menos Créditos
    /// </summary>
    /// <returns>Monto de diferencia</returns>
    public decimal DiferenciaDebitosCreditos()
    {
        return Detalles.Sum(p => p.Debito) - Detalles.Sum(p => p.Credito);
    }

    /// <summary>
    /// Diferencia entre el debe y el haber de los montos en la moneda de las cuentas contables.
    /// </summary>
    /// <returns></returns>
    public decimal DiferenciaDebitosCreditosCuenta()
    {
        return Detalles.Sum(i => i.Debito_Cta) - Detalles.Sum(i => i.Credito_Cta);
    }

    /// <summary>
    /// Método que detalla entre diferentes monedas
    /// </summary>
    /// <returns></returns>
    public bool EsDetalleEntreDiferentesMonedas()
    {
        var cuentas = Detalles.Select(c => c.CuentaContable);

        return cuentas.GroupBy(e => e.CodigoMoneda).Count() > 1;
    }

    /// <summary>
    /// Indicador si es transaccion de deposito
    /// </summary>
    /// <returns></returns>
    public bool esTransaccionDeposito()
    {
        if (CodigoSistema == Sistema.Cajas && CodigoTipoTransaccion == ((int)CatalogoTransaccionEnum.CodigoDepositoCuentaEfectivo).ToString() 
            && CodigoSubTipoTransaccion == ((int)SubTipoTransaccionEnum.CodigoDepositoGeneralCuentaEfectivo).ToString())
            return true;
        return false;
    }

    /// <summary>
    /// Identifica si el detalle del asiento trabaja con dólares
    /// </summary>
    /// <returns></returns>
    public bool EsDetalleConMonedaDolares()
    {
        return Detalles
            .Select(d => d.NumeroCuentaContable)
            .Any(codigo =>
                !string.IsNullOrEmpty(codigo) &&
                codigo.Length > 2 &&
                codigo.Substring(2, 1) == ((int)MonedaCodigo.Dolares).ToString());
    }

    /// <summary>
    /// Método que adiciona un detalle al asiento contable
    /// </summary>
    /// <param name="detalle">instancia de la clase AsientoContableDetalle</param>
    public void AgregarDetalle(AsientoContableDetalle detalle) { Detalles.Add(detalle); }

    /// <summary>
    /// Agrega un detalle de ajuste a un asiento contable existente.
    /// Calcula el monto a contabilizar considerando los tipos de cambio base y de la cuenta,
    /// y valida que los montos sean mayores a cero.
    /// </summary>
    /// <param name="tipoDetalle">Tipo de movimiento del detalle (por ejemplo, débito o crédito).</param>
    /// <param name="numeroCuentaContable">Número de la cuenta contable asociada al detalle.</param>
    /// <param name="montoContabilizarCuenta">Monto que se desea contabilizar en la cuenta.</param>
    /// <param name="codigoUnidadEjecutora">Código de la unidad ejecutora asociada al detalle.</param>
    /// <param name="montoTipoCambioBase">Tipo de cambio de la moneda base para el asiento.</param>
    /// <param name="montoTipoCambioCuenta">Tipo de cambio aplicado a la cuenta contable.</param>
    /// <param name="descripcion">Descripción o detalle del asiento contable.</param>
    /// <param name="referencia">Referencia adicional para el detalle del asiento.</param>
    /// <returns>El objeto <see cref="AsientoContableDetalle"/> generado y agregado al asiento.</returns>
    public AsientoContableDetalle AgregarDetalleAjuste(string tipoDetalle, string numeroCuentaContable,
            decimal montoContabilizarCuenta, string codigoUnidadEjecutora, decimal montoTipoCambioBase,
            decimal montoTipoCambioCuenta, string descripcion, string referencia)
    {
        montoTipoCambioBase = montoTipoCambioBase.Redondear(AsientoContableDetalle.DecimalesTipoCambioEnAsientosContables);
        montoTipoCambioCuenta = montoTipoCambioCuenta.Redondear(AsientoContableDetalle.DecimalesTipoCambioEnAsientosContables);
        if (montoTipoCambioCuenta == 0)
        {
            throw new Exception("El tipo de cambio de la cuenta no puede ser cero.");
        }
        montoContabilizarCuenta = montoContabilizarCuenta.Redondear(AsientoContableDetalle.DecimalesPorDefectoEnAsientosContables);
        decimal montoContabilizar = (montoContabilizarCuenta * montoTipoCambioBase / montoTipoCambioCuenta).Redondear(AsientoContableDetalle.DecimalesPorDefectoEnAsientosContables);

        if (montoContabilizarCuenta <= 0 || montoContabilizar <= 0)
        {
            throw new Exception("No se puede contabilizar un monto de cero o menor a cero.");
        }

        var detalleDeAsientoContable = AsientoContableDetalle.Generar(
            asiento: this,
            tipoMovimiento: tipoDetalle,
            cuentaContable: numeroCuentaContable,
            monto: montoContabilizarCuenta,
            codigoUnidad: codigoUnidadEjecutora,
            detalle: descripcion,
            referencia: referencia,
            tipoCambioBase: montoTipoCambioBase,
            tipoCambioCuenta: montoTipoCambioCuenta
        );

        Detalles.Add(detalleDeAsientoContable);

        return detalleDeAsientoContable;
    }
    #endregion
}
