using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CJ;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
/// <summary>
/// Clase que representa la entidad Movimiento axiliar logico
/// </summary>
public class MovimientoAuxiliarLogico : ITransaccion
{
    /// <summary>
    /// Indica  una cuenta contable configurada
    /// </summary>
    public string CuentaContableConfigurada { get;  private set; }
    /// <summary>
    /// Indica si el transaccion es configurada
    /// </summary>
    public SubTipoTransaccion TransaccionConfigurada { get;  private set; }
    /// <summary>
    /// Indica si el transaccion es configurada
    /// </summary>
    public string DescripcionAsientoConfigurada { get;  private set; }
    /// <summary>
    /// Indica si el Tipo Movimiento Contable Configurada es configurada
    /// </summary>
    public string TipoMovimientoContableConfigurada { get;  private set; }
    /// <summary>
    /// Indica si el Principal es configurada
    /// </summary>
    public bool PrincipalConfigurada { get;  private set; }
    /// <summary>
    /// Tasa de cambio local
    /// </summary>
    public decimal TasaCambioLocal {  get; private set; }
    /// <summary>
    /// Tasa de cambio cuenta
    /// </summary>
    public decimal TasaCambioCuenta { get; private set; }

    /// <summary>
    /// Crea el movimiento auxiliar logico
    /// </summary>
    /// <param name="movimiento">Datos del movimiento</param>
    /// <param name="cuentaContable">numero de cuenta contable</param>
    /// <param name="subTransaccion">Datos de subtransaccion</param>
    /// <param name="descripcionAsientoConfigurada">descripcion de asiento</param>
    /// <param name="tipoMovimientoContableConfigurada">Tipo de movimiento contable</param>
    /// <param name="principalConfigurada">indicador de movimiento princiapl</param>
    public MovimientoAuxiliarLogico(MovimientoDiario movimiento, string cuentaContable
        , SubTipoTransaccion subTransaccion, string descripcionAsientoConfigurada
        , string tipoMovimientoContableConfigurada, bool principalConfigurada)
    {
        TransaccionConfigurada = subTransaccion;
        CuentaContableConfigurada = cuentaContable;
        DescripcionAsientoConfigurada = descripcionAsientoConfigurada;
        TipoMovimientoContableConfigurada = tipoMovimientoContableConfigurada;
        PrincipalConfigurada = principalConfigurada;
        NumeroOperacion = movimiento.NumeroOperacion;
        EsOrigen = movimiento.EsOrigen;
        EsDestino = movimiento.EsOrigen;
        NumeroAsientoContable = movimiento.NumeroOperacion;
        FechaMovimiento = movimiento.FechaMovimiento;
        ReferenciaMovimientoContable = movimiento.ReferenciaMovimientoContable;
        MontoMovimientoContable = movimiento.MontoMovimientoContable;
        CodigoUnidadEjecutora = movimiento.CodigoUnidadEjecutora;
        CodigoCuentaPuente = movimiento.CodigoCuentaPuente;
        AplicaAsiento = movimiento.AplicaAsiento;
        CodigoAgencia = movimiento.CodigoAgencia;
        CodigoUsuario = movimiento.CodigoUsuario;
        FechaOperacion = movimiento.FechaMovimiento;
        MonedaOperacion = movimiento.MonedaOperacion;
        MontoOperacion = movimiento.MontoOperacion;
        NumeroMovimiento = movimiento.NumeroOperacion;
        FormaDePagoLavado = movimiento.FormaDePagoLavado;
        NumeroAsientoLavado = movimiento.NumeroAsientoLavado;
        CodigoSistema = movimiento.CodigoSistema;
        CodigoEntidadSbs = movimiento.CodigoEntidadSbs;
        IndicadorMovimiento = movimiento.IndicadorMovimiento;
        CodigoSistemaFuente = movimiento.CodigoSistemaFuente;
    }

    #region Propiedades

    /// <summary>
    /// Indica numero de la operacion
    /// </summary>
    public int NumeroOperacion { get; }
    /// <summary>
    /// Indica el origen del movimiento
    /// </summary>
    public bool EsOrigen { get; }
    /// <summary>
    /// Indica si el destino de movimiento
    /// </summary>
    public bool EsDestino { get; }
    /// <summary>
    /// Indica entidad producto
    /// </summary>
    public IEntidadProducto Producto { get; }
    /// <summary>
    /// Indica establece operacion origen
    /// </summary>
    public void EstablecerOperacionOrigen(IOperacionProducto operacionOrigen)
    {
    }
    /// <summary>
    /// Representa el número de asiento contable de la operación
    /// </summary>
    public int NumeroAsientoContable { get; }

    /// <summary>
    /// Representa el código del tipo de transacción de la operación
    /// </summary>
    public string CodigoTipoTransaccion => TransaccionConfigurada.CodigoTipoTransaccion;

    /// <summary>
    /// Representa el código del sub tipo de transacción de la operación
    /// </summary>
    public string CodigoSubTipoTransaccion => TransaccionConfigurada.CodigoSubTipoTransaccion;

    /// <summary>
    /// Representa la descripción del asiento contable
    /// </summary>
    public string DescripcionAsientoMovimientoContable => DescripcionAsientoConfigurada;

    /// <summary>
    /// Representa la fecha de movimiento contable
    /// </summary>
    public DateTime FechaMovimiento { get; }

    /// <summary>
    /// Indica si el movimiento es el que se utilizara como principal en reprsentación del asiento
    /// </summary>
    public bool EsPrincipal => PrincipalConfigurada; 

    /// <summary>
    /// Representa el codigo de cuenta contable
    /// </summary>
    public string CuentaContable => CuentaContableConfigurada;

    /// <summary>
    /// Representa el tipo de cuenta contable
    /// </summary>
    public string TipoCuentaContable => TipoMovimientoContableConfigurada;

    /// <summary>
    /// Representa el monto del movimiento contable
    /// </summary>
    public decimal MontoMovimientoContable { get; }

    /// <summary>
    /// Representa la referencia del movimiento contable
    /// </summary>
    public string ReferenciaMovimientoContable { get; }

    /// <summary>
    /// Propiedad que devuelve el código de Unidad Ejecutora
    /// </summary>
    public string CodigoUnidadEjecutora { get; }

    /// <summary>
    /// Propiedad que devuelve el còdigo de cuenta puente a utilizar
    /// </summary>
    public int CodigoCuentaPuente { get; }

    /// <summary>
    /// Propiedad que devuelve si la entidad genera asiento contable.
    /// </summary>
    public bool AplicaAsiento { get; }

    /// <summary>
    /// Establece el asiento contable del movimiento
    /// </summary>
    /// <param name="aoAsiento">instancia de la clase Asiento Contable</param>
    public void EstablecerAsiento(AsientoContable aoAsiento)
    {
    }

    /// <summary>
    /// Asigna la tasa de cambio local
    /// </summary>
    /// <param name="tasaCambio"></param>
    public void AsignarTasaCambioLocal(decimal tasaCambio)
    {
        TasaCambioLocal = tasaCambio;
    }

    /// <summary>
    /// Asigna la tasa de cambio de cuenta
    /// </summary>
    /// <param name="tasaCambio"></param>
    public void AsignarTasaCambioCuenta(decimal tasaCambio)
    {
        TasaCambioCuenta = tasaCambio;
    }

    /// <summary>
    /// Valida si es operacion princiapl  de lavado
    /// </summary>
    public bool EsOperacionPrincipalLavado { get; }
    /// <summary>
    /// Valida si es movimiento origen
    /// </summary>
    public bool EsRegistroOrigen => EsOrigen;
    /// <summary>
    /// Codigo de agencia
    /// </summary>
    public string CodigoAgencia { get; }
    /// <summary>
    /// Codigo de usuario
    /// </summary>
    public string CodigoUsuario { get; }
    /// <summary>
    /// Fecha de operacion
    /// </summary>
    public DateTime FechaOperacion { get; }
    /// <summary>
    /// Datos de subtipo de movimiento
    /// </summary>
    public SubTipoTransaccion SubTipoTransaccionMovimiento => TransaccionConfigurada;
    /// <summary>
    /// Moneda de la operacion
    /// </summary>
    public string MonedaOperacion { get; }
    /// <summary>
    /// Monto de la operacion
    /// </summary>
    public decimal MontoOperacion { get; }
    /// <summary>
    /// Numero de cuenta
    /// </summary>
    public string NumeroCuenta { get; }
    /// <summary>
    /// Numero de movimiento
    /// </summary>
    public decimal NumeroMovimiento { get; }
    /// <summary>
    /// Forma de pago del lavado
    /// </summary>
    public string FormaDePagoLavado { get; }
    /// <summary>
    /// Numero de asiento de lavado
    /// </summary>
    public int NumeroAsientoLavado { get; }
    /// <summary>
    /// Codigo de sitema
    /// </summary>
    public string CodigoSistema { get; }
    /// <summary>
    /// Codigo de entidad SBS
    /// </summary>
    public string CodigoEntidadSbs { get; }
    /// <summary>
    /// Indicador de movimiento
    /// </summary>
    public string IndicadorMovimiento { get; }
    /// <summary>
    /// Codigo de sistema fuerte
    /// </summary>
    public string CodigoSistemaFuente { get; }
    /// <summary>
    /// Lista de intervinientes
    /// </summary>
    public IList<IInterviniente> Intervinientes { get; }
    /// <summary>
    /// Tipos de intervinientes
    /// </summary>
    public TipoInteviniente TipoInterviniente { get; }
    /// <summary>
    /// Interviniente
    /// </summary>
    public IInterviniente Interviniente { get; }
    #endregion Propiedades
}


