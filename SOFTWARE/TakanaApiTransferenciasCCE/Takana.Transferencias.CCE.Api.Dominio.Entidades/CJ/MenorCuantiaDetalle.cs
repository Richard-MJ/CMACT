using Takana.Transferencias.CCE.Api.Common.Constantes;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CJ;

/// <summary>
/// Clase de dominio que representa el detalle de la operación de menor cuantía.
/// </summary>
public class MenorCuantiaDetalle
{
    #region Constantes
    public const string FormaPagoCaja = "9";
    #endregion
    
    #region Propiedades

    /// <summary>
    /// Representa el identificador del detalle
    /// </summary>
    public decimal IdentificadorMenorCuantiaDetalle { get; private set; }
    /// <summary>
    /// Representa el número de operación de lavado
    /// </summary>
    public int NumeroOperacion { get; private set; }
    /// <summary>
    /// Representa el código de sistema de la operación
    /// </summary>
    public string CodigoSistema { get; private set; }
    /// <summary>
    /// Representa el número de movimiento de la operacion
    /// </summary>
    public decimal NumeroMovimiento { get; private set; }
    /// <summary>
    /// Representa el monto de movimiento de la operacion
    /// </summary>
    public decimal MontoMovimiento { get; private set; }
    /// <summary>
    /// Representa el código del tipo de la transaccion
    /// </summary>
    public string CodigoTipoTransaccion { get; private set; }
    /// <summary>
    /// Representa el código del tipo de la subtransaccion
    /// </summary>
    public string CodigoSubTipoTransaccion { get; private set; }
    /// <summary>
    /// Indica si el movimiento es de Ingreso o Egreso
    /// </summary>
    public string IndicadorMovimiento { get; private set; }
    /// <summary>
    /// Representa la fecha de la operación
    /// </summary>
    public DateTime FechaOperacion { get; private set; }
    /// <summary>
    /// Representa el número de cuenta de la operación
    /// </summary>
    public string NumeroCuenta { get; private set; }
    /// <summary>
    /// Representa el código de moneda de la operación
    /// </summary>
    public string CodigoMoneda { get; private set; }
    /// <summary>
    /// Representa el código de la forma de pago de la operación
    /// </summary>
    public string CodigoFormaPagoCJ { get; private set; }
    /// <summary>
    /// Representa el número de secuencia de la operación en cajas
    /// </summary>
    public decimal NumeroSecuenciaDocumento { get; private set; }
    /// <summary>
    /// Representa el número de asiento contable de la operación
    /// </summary>
    public int NumeroAsiento { get; private set; }
    /// <summary>
    /// Representa el estado del registro
    /// </summary>
    public string IndicadorEstado { get; private set; }
    /// <summary>
    /// Representa el número de movimiento de operación secundaria
    /// </summary>
    public decimal NumeroMovimiento2 { get; private set; }
    /// <summary>
    /// Representa el número de cuenta de la operación secundaria
    /// </summary>
    public string NumeroCuenta2 { get; private set; }
    /// <summary>
    /// Representa el código de la entidad externa
    /// </summary>
    public string CodigoEntidadSBS { get; private set; }
    /// <summary>
    /// Representa la instancia de Encabazado a la cual esta asociado
    /// </summary>
    public virtual MenorCuantiaEncabezado Encabezado { get; private set; }

    #endregion Propiedades

    #region Métodos
    /// <summary>
    /// Método que crea un detalle de menor cuantia
    /// </summary>
    /// <param name="numeroOperacion"></param>
    /// <param name="codigoSistema"></param>
    /// <param name="numeroMovimiento"></param>
    /// <param name="montoMovimiento"></param>
    /// <param name="codigoTipoTransaccion"></param>
    /// <param name="codigoSubTipoTransaccion"></param>
    /// <param name="indicadorMovimiento"></param>
    /// <param name="fechaOperacion"></param>
    /// <param name="numeroCuenta"></param>
    /// <param name="codigoMoneda"></param>
    /// <param name="codigoFormaPago"></param>
    /// <param name="numeroSecuenciaDocumento"></param>
    /// <param name="numeroAsiento"></param>
    /// <param name="numeroMovimiento2"></param>
    /// <param name="codigoCuentaInterbancaria"></param>
    /// <returns></returns>
    public static MenorCuantiaDetalle Crear(
        int numeroOperacion
        , string codigoSistema
        , decimal numeroMovimiento
        , decimal montoMovimiento
        , string codigoTipoTransaccion
        , string codigoSubTipoTransaccion
        , string indicadorMovimiento
        , DateTime fechaOperacion
        , string numeroCuenta
        , string codigoMoneda
        , string codigoFormaPago
        , decimal numeroSecuenciaDocumento
        , int numeroAsiento
        , decimal numeroMovimiento2
        , string codigoCuentaInterbancaria
        )
    {
        return new MenorCuantiaDetalle()
        {
            NumeroOperacion = numeroOperacion,
            CodigoSistema = codigoSistema,
            NumeroMovimiento = numeroMovimiento,
            MontoMovimiento = montoMovimiento,
            CodigoTipoTransaccion = codigoTipoTransaccion,
            CodigoSubTipoTransaccion = codigoSubTipoTransaccion,
            IndicadorMovimiento = indicadorMovimiento,
            FechaOperacion = fechaOperacion,
            NumeroCuenta = codigoCuentaInterbancaria,
            CodigoMoneda = codigoMoneda,
            CodigoFormaPagoCJ = codigoFormaPago,
            NumeroSecuenciaDocumento = numeroSecuenciaDocumento,
            NumeroAsiento = numeroAsiento,
            IndicadorEstado = General.Activo,
            NumeroMovimiento2 = numeroMovimiento2,
            NumeroCuenta2 = numeroCuenta
        };
    }

    /// <summary>
    /// Establece el código de la entidad SBS
    /// </summary>
    /// <param name="codigoEntidadSBS">Código de la entidad SBS obtenida por Bancos -> EntidadFinancieraCCE.</param>
    /// <returns>El objeto modificado.</returns>
    public MenorCuantiaDetalle EstablecerCodigoEntidadSBS(string codigoEntidadSBS)
    {
        CodigoEntidadSBS = codigoEntidadSBS;
        return this;
    }

    /// <summary>
    /// Establece el código de la entidad SBS
    /// </summary>
    /// <param name="numeroOperacionLavado">Numero de Operacion de lavado.</param>
    /// <param name="operacionesOrigen">Interfaz para la operacion de origen.</param>
    /// <param name="operacionesDestino">Interfaz para la operacion de destino.</param>
    /// <returns>El objeto modificado.</returns>
    public static MenorCuantiaDetalle RegistarDetalle(int numeroOperacionLavado
        , IList<IOperacionLavado> operacionesOrigen, IOperacionLavado operacionesDestino)
    {
        IOperacionLavado operacionOrigen = operacionesOrigen.First();

        return new MenorCuantiaDetalle()
        {
            NumeroOperacion = numeroOperacionLavado,
            CodigoSistema = operacionOrigen.SubTipoTransaccionMovimiento.CodigoSistema,
            NumeroMovimiento = operacionOrigen.NumeroMovimiento,
            CodigoTipoTransaccion = operacionOrigen.SubTipoTransaccionMovimiento.CodigoTipoTransaccion,
            CodigoSubTipoTransaccion = operacionOrigen.SubTipoTransaccionMovimiento.CodigoSubTipoTransaccion,
            IndicadorMovimiento = string.IsNullOrEmpty(operacionesDestino.CodigoEntidadSbs) 
                ? operacionesDestino.SubTipoTransaccionMovimiento.IndicadorMovimientoLavando ?? string.Empty
                : operacionesDestino.IndicadorMovimiento,
            FechaOperacion = operacionOrigen.FechaOperacion,
            NumeroCuenta = operacionOrigen.NumeroCuenta,
            CodigoMoneda = operacionOrigen.MonedaOperacion,
            CodigoFormaPagoCJ = operacionesDestino.FormaDePagoLavado,
            NumeroAsiento = operacionOrigen.NumeroAsientoLavado,
            MontoMovimiento = operacionesOrigen.Sum(x => x.MontoOperacion),
            IndicadorEstado = General.Activo,
            NumeroMovimiento2 = 0,
            NumeroCuenta2 = operacionesDestino.NumeroCuenta ?? string.Empty,
            CodigoEntidadSBS = operacionesDestino.CodigoEntidadSbs ?? string.Empty,
            NumeroSecuenciaDocumento = operacionesDestino.NumeroMovimiento
        };
    }

    #endregion Métodos
}
