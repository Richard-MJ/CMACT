using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CJ;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

/// <summary>
/// Clase que representa a una entidad de dominio de los movimientos diarios de cuentas de ahorro
/// </summary>
public class MovimientoDiario : Movimiento, ITransaccion, IMovimientoCuantia
{

    #region Constantes
    /// <summary>
    /// Codigo de unidad ejecutora
    /// </summary>
    public string CodigoUnidadEjecutora => Cuenta.CodigoAgencia + "999999999999999";
    /// <summary>
    /// Remunerativo
    /// </summary>
    public const string Remunerativo = "R";
    /// <summary>
    /// No remunerativo
    /// </summary>
    public const string NoRemunerativo = "N";
    /// <summary>
    /// Descripcion de movimiento inmediata entrante
    /// </summary>
    public const string DescripcionMovimientoTinInmediataEntrante = "DEPOSITO POR TIN INMEDIATAS CCE";
    /// <summary>
    /// Movimiento estado activo
    /// </summary>
    public const string EstadoActivo = "A";
    /// <summary>
    /// Estado movimiento anulado
    /// </summary>
    public const string EstadoAnulado= "N";
    /// <summary>
    /// Movimiento Origen
    /// </summary>
    public const string Origen = "O";
    /// <summary>
    /// Movimiento Destino
    /// </summary>
    public const string Destino = "D";
    /// <summary>
    /// Movimiento Desconocido
    /// </summary>
    public const string Desconocido = "F";
    /// <summary>
    /// Movimiento Contabilizado
    /// </summary>
    public const string Contabilizable = "C";
    #endregion constantes

    #region Implementación de IMovimientoContable

    /// <summary>
    /// Numero de asiento
    /// </summary>
    public int NumeroAsientoContable => (int)NumeroAsiento;
    /// <summary>
    /// Descripcion de asiento del movimiento contable
    /// </summary>
    public string DescripcionAsientoMovimientoContable => 
        (SubTipoTransaccionMovimiento.CodigoTipoTransaccion == 
            ((int)CatalogoTransaccionEnum.CodigoTransferenciaInmediataEntrante).ToString()
            && SubTipoTransaccionMovimiento.CodigoSubTipoTransaccion ==
                ((int)SubTipoTransaccionEnum.CodigoTransferenciaInmediataEntrante).ToString())
                ? $"{AsientoContable.DescripcionAsientoTinInmediataEntrante}-Cta {Cuenta.NumeroCuenta}"
                : $"{SubTipoTransaccionMovimiento.DescripcionSubTransaccion}-Cta {Cuenta.NumeroCuenta}";
    /// <summary>
    /// Valida si es movimiento principal
    /// </summary>
    public bool EsPrincipal => SubTipoTransaccionMovimiento.EsDetalleContablePrincipal;
    /// <summary>
    /// Tipo de cuenta contable
    /// </summary>
    public string TipoCuentaContable => SubTipoTransaccionMovimiento.Transaccion.IndicadorMovimiento;
    /// <summary>
    /// Codigo de cuenta contable
    /// </summary>
    public string CuentaContable => Cuenta.Caracteristicas.CodigoCuentaContable;
    /// <summary>
    /// Monto de movimiento contable
    /// </summary>
    public decimal MontoMovimientoContable => MontoMovimiento;
    /// <summary>
    /// Numero de movimiento
    /// </summary>
    public string ReferenciaMovimientoContable => NumeroMovimiento.ToString();
    
    /// <summary>
    /// Indicador de cuenta puente
    /// </summary>
    public int CodigoCuentaPuente { get; private set; } = 0;

    /// <summary>
    /// Tasa de cambio local
    /// </summary>
    public decimal TasaCambioLocal { get; private set; } = 0;

    /// <summary>
    /// Tasa de cambio local
    /// </summary>
    public decimal TasaCambioCuenta { get; private set; } = 0;

    /// <summary>
    /// Valida si es contabilizable
    /// </summary>
    public bool AplicaAsiento => SubTipoTransaccionMovimiento.EsContabilizable;
    /// <summary>
    /// Establece el numero se asiento y el estado del asiento
    /// </summary>
    /// <param name="asiento"></param>
    public void EstablecerAsiento(AsientoContable asiento)
    {
        NumeroAsiento = asiento.NumeroAsiento;
        EstadoMovimiento = Contabilizable;
    }

    #endregion Implementación de IMovimientoContable

    #region Implementación de IRecibo
    /// <summary>
    /// Numero de recibo
    /// </summary>
    public int NumeroRecibo { get { return (int)NumeroMovimiento; } }
    /// <summary>
    /// Fecha de recibo
    /// </summary>
    public DateTime FechaRecibo { get { return FechaMovimiento; } }
    /// <summary>
    /// Nombre de agencia
    /// </summary>
    public string NombreAgencia { get { return AgenciaOperacion.NombreAgencia; } }
    /// <summary>
    /// Descripciond de recibo
    /// </summary>
    public string DescripcionRecibo { get { return SubTipoTransaccionMovimiento.DescripcionSubTransaccion; } }
    /// <summary>
    /// Datos de producto
    /// </summary>
    public IEntidadProducto Producto { get { return (IEntidadProducto)Cuenta; } }
    /// <summary>
    /// Nombre de cliente
    /// </summary>
    public string NombreCliente { get { return Cuenta.Cliente.NombreCliente; } }
    /// <summary>
    /// Monto de recibo
    /// </summary>
    public decimal MontoRecibo { get { return MontoMovimiento; } }
    /// <summary>
    /// Mostrar monto de impuesto recibo
    /// </summary>
    public bool MostrarMontoImpuestoRecibo { get { return true; } }
    /// <summary>
    /// Monto de impuesto recibo
    /// </summary>
    public decimal MontoImpuestoRecibo { get; private set; }
    /// <summary>
    /// Muestra el monto de impuesto recibo
    /// </summary>
    public bool MostrarMontoComisionRecibo { get { return false; } }
    /// <summary>
    /// Monto de comision del recibo
    /// </summary>
    public decimal MontoComisionRecibo { get; private set; }
    /// <summary>
    /// Monto total del recibo
    /// </summary>
    public decimal MontoTotalRecibo { get { return MontoRecibo + MontoComisionRecibo; } }
    /// <summary>
    /// Monto de redonde de la operacion
    /// </summary>
    public decimal MontoRedondeoOperacion { get; private set; }
    /// <summary>
    /// Establecer impuesto de comisiones
    /// </summary>
    public void EstablecerImpuestosComisiones(decimal montoImpuesto, decimal montoComision)
    {
        MontoImpuestoRecibo = montoImpuesto;
        MontoComisionRecibo = montoComision;
    }

    #endregion Implementación de IRecibo

    #region Implementación de IOperacionLavado
    /// <summary>
    /// Monto de la operacion
    /// </summary>
    public decimal MontoOperacion => MontoMovimiento;
    /// <summary>
    /// Moneda de la operacion
    /// </summary>
    public string MonedaOperacion => Cuenta.CodigoMoneda;
    /// <summary>
    /// Fechade movimiento
    /// </summary>
    public DateTime FechaOperacion => FechaMovimiento;
    /// <summary>
    /// Valida si es operacion principal lavado
    /// </summary>
    public bool EsOperacionPrincipalLavado => EsOrigen;
    /// <summary>
    /// Valida si es registro origen
    /// </summary>
    public bool EsRegistroOrigen => EsOrigen;
    /// <summary>
    /// Forma de pago de lavado
    /// </summary>
    public string FormaDePagoLavado => SubTipoTransaccionMovimiento.CodigoFormaPagoLavado;
    /// <summary>
    /// Numero de asiento lavado
    /// </summary>
    public int NumeroAsientoLavado => (int)NumeroAsiento;
    /// <summary>
    /// Codigo de entidad SBS
    /// </summary>
    public string CodigoEntidadSbs => string.Empty;
    /// <summary>
    /// Tipo de interviniente
    /// </summary>
    public TipoInteviniente TipoInterviniente =>
        EsRegistroOrigen ? TipoInteviniente.Ordenante : TipoInteviniente.Beneficiario;
    /// <summary>
    /// Interviniente
    /// </summary>
    public IInterviniente Interviniente
        => (from f in Cuenta.Firmas.Where(p => p.EsTitular()) select f.Cliente)
            .ToList<IInterviniente>().FirstOrDefault();

    #endregion Implementación de IOperacionLavado

    /// <summary>
    /// Monto ITF
    /// </summary>
    public decimal MontoItf { get; private set; } = 0;
    /// <summary>
    /// Codigo de agencia
    /// </summary>
    public string CodigoAgenciaMovimiento => CodigoAgencia;
    /// <summary>
    /// Numero de movimiento
    /// </summary>
    public int NumeroOperacion => (int)NumeroMovimiento;
    /// <summary>
    /// Indicador de movimienot
    /// </summary>
    public string IndicadorMovimiento => SubTipoTransaccionMovimiento.Transaccion.IndicadorMovimiento;
    /// <summary>
    /// Indicador de comando principal
    /// </summary>
    public bool EsOrigen => (SubTipoTransaccionMovimiento.Transaccion.IndicadorMovimiento == AsientoContableDetalle.CodigoDebito
        && SubTipoTransaccionMovimiento.IndicadorComandoPrincipal == General.Si);
    /// <summary>
    /// Indicador de comando principal
    /// </summary>
    public bool EsDestino => (SubTipoTransaccionMovimiento.Transaccion.IndicadorMovimiento == AsientoContableDetalle.CodigoCredito
        && SubTipoTransaccionMovimiento.IndicadorComandoPrincipal == General.Si);
    /// <summary>
    /// Sistema fuente
    /// </summary>
    public string CodigoSistemaFuente => SistemaFuente;
    /// <summary>
    /// Cuenta Efectivo para la exoneración
    /// </summary>
    public CuentaEfectivo CuentaEfectivo => Cuenta;
    /// <summary>
    /// Codigo de agencia para la exoneracion
    /// </summary>
    public string CodigoAgenciaOperacion => CodigoAgencia;
    /// <summary>
    /// fecha de proceso para la exoneración
    /// </summary>
    public DateTime FechaProceso => FechaMovimiento;

    #region Metodos

    #region Creacionales

    /// <summary>
    /// Genera un nuevo movimiento en CC.
    /// </summary>
    /// <param name="cuentaOrigen">Cuenta efectivo para la cual se genera el movimiento.</param>
    /// <param name="numeroMovimiento">Número del movimiento.</param>
    /// <param name="subTipoTransaccion">Tipo de subtransacción del movimiento a generar.</param>
    /// <param name="descripcionMovimiento">Descripcion que tendra el movimiento.</param>
    /// <param name="montoMovimiento">Monto del movimiento.</param>
    /// <param name="usuario">Usuario que genera el movimiento.</param>
    /// <param name="fechaEnCc">Fecha de sistema en CC.</param>
    /// <param name="codigoSistemaFuente">Código del sistema SAF que genera el movimiento.</param>
    /// <param name="indicadorRemunerativo">S: Indica que es un movimiento remunerativo.
    /// N: Indica que es un movimiento no remunerativo.</param>
    /// <returns>Devuelve el movimiento generado.</returns>
    public static MovimientoDiario Crear(
        CuentaEfectivo cuentaOrigen,
        int numeroMovimiento,
        SubTipoTransaccion subTipoTransaccion,
        string descripcionMovimiento,
        decimal montoMovimiento,
        Usuario usuario,
        DateTime fechaEnCc,
        string codigoSistemaFuente,
        TipoMontoCuentaEfectivo indicadorRemunerativo)
    {
        return new MovimientoDiario
        {
            CodigoEmpresa = cuentaOrigen.CodigoEmpresa,
            Cuenta = cuentaOrigen,
            Transaccion = subTipoTransaccion.Transaccion,
            CodigoSistema = cuentaOrigen.CodigoSistema!,
            CodigoTipoTransaccion = subTipoTransaccion.Transaccion.TipoTransaccion,
            CodigoSubTipoTransaccion = subTipoTransaccion.CodigoSubTipoTransaccion,
            EstadoMovimiento = EstadoActivo,
            NumeroAsiento = 0,
            NumeroMovimientoFuente = 0,
            NumeroCuenta = cuentaOrigen.NumeroCuenta,
            MontoMovimiento = montoMovimiento,
            NumeroMovimiento = numeroMovimiento,
            FechaMovimiento = fechaEnCc,
            CodigoUsuario = usuario.CodigoUsuario,
            FechaServidor = fechaEnCc,
            CodigoProducto = cuentaOrigen.CodigoProducto,
            DescripcionMovimiento = descripcionMovimiento,
            CodigoAgencia = usuario.CodigoAgencia,
            IndAfectaITF = General.No,
            IndAplicaCargo = General.No,
            IndConsolidado = General.No,
            IndDepAutomatico = General.No,
            IndOrigenDestino = Origen,
            MontoDisponible = 0,
            MontoIntangible = 0,
            NumeroDocumento = 0,
            SistemaFuente = codigoSistemaFuente,
            IndicadorRemunerativo = indicadorRemunerativo == TipoMontoCuentaEfectivo.NoRemunerativo 
                ? NoRemunerativo : Remunerativo,
            SubTipoTransaccionMovimiento = subTipoTransaccion,
            AgenciaOperacion = usuario.Agencia,
        };
    }


    #endregion Creacionales

    #region Asignación

    /// <summary>
    /// Define al movimiento como destino de una operación y le asigna su movimiento origen.
    /// </summary>
    /// <param name="movimientoOrigen">Movimiento de origen de la operación.</param>
    /// <returns>Movimiento destino de la operación.</returns>
    public MovimientoDiario AsignarMovimientoOrigen(MovimientoDiario movimientoOrigen)
    {
        NumeroMovimientoFuente = movimientoOrigen.NumeroMovimiento;
        IndOrigenDestino = Destino;
        movimientoOrigen.IndOrigenDestino = movimientoOrigen.IndOrigenDestino == Destino
            ? movimientoOrigen.IndOrigenDestino
            : Desconocido;

        return this;
    }

    /// <summary>
    /// Define al movimiento como secundario, asignandole su movimiento principal.
    /// </summary>
    /// <param name="movimientoPrincipal">Movimiento que será el principal.</param>
    /// <returns>Retorna el movimiento secundario, puede lanzar una excepción si el movimiento ya tiene
    /// un movimiento principal.</returns>
    public MovimientoDiario AsignarMovimientoPrincipal(MovimientoDiario movimientoPrincipal)
    {
        if (NumeroMovimientoFuente > 0)
            throw new ValidacionException("El movimiento ya tiene un movimiento principal asignado.");
        NumeroMovimientoFuente = movimientoPrincipal.NumeroMovimiento;
        return this;
    }
    /// <summary>
    /// Metodo que asignar el asiento contable al movimiento
    /// </summary>
    /// <param name="asientoContable"></param>
    /// <returns></returns>
    public MovimientoDiario AsignarAsientoContable(AsientoContable asientoContable)
    {
        NumeroAsiento = asientoContable.NumeroAsiento;
        return Contabilizar();
    }
    /// <summary>
    /// Contabilizar movimiento
    /// </summary>
    /// <returns></returns>
    public MovimientoDiario Contabilizar()
    {
        EstadoMovimiento = Contabilizable;
        return this;
    }
    /// <summary>
    /// Anular movimienot
    /// </summary>
    /// <returns></returns>
    public MovimientoDiario Anular()
    {
        EstadoMovimiento = EstadoAnulado;

        return this;
    }
    /// <summary>
    /// Establece la operacion de origen
    /// </summary>
    /// <param name="operacionOrigen"></param>
    public void EstablecerOperacionOrigen(IOperacionProducto operacionOrigen)
    {
        if (NumeroMovimientoFuente != 0)
            return;
        SistemaFuente = operacionOrigen.CodigoSistema;
        NumeroMovimientoFuente = operacionOrigen.NumeroOperacion;
        if (operacionOrigen.CodigoSistema == Sistema.Cajas)
        {
            NumeroMovimientoFuente = 0;
            operacionOrigen.EstablecerOperacionOrigen(this);
        }
    }
    /// <summary>
    /// Asigna la cuenta puente
    /// </summary>
    /// <param name="codigoCuentaPuente"></param>
    public void AsignarCuentaPuente(int codigoCuentaPuente)
    {
        CodigoCuentaPuente = codigoCuentaPuente;
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

    #endregion Asignación

    /// <summary>
    /// Marca el movimiento contabilizado sin monto ni asiento
    /// </summary>
    public void MarcarMovimientoContabilizadoSinMontoNiAsiento()
    {
        if (MontoMovimiento == 0 && NumeroAsiento == 0 && EstadoMovimiento == EstadoActivo)
            EstadoMovimiento = Contabilizable;
    }
    /// <summary>
    /// Establece que es afectado por ITF
    /// </summary>
    public void EstablecerAfectadoItf()
    {
        IndAfectaITF = General.Si;
    }
    /// <summary>
    /// Se agrega el monto ITF
    /// </summary>
    /// <param name="monto">Monto itf a agregar al movimiento</param>
    public void AgregarMontoITF(decimal monto)
    {
        MontoItf = monto;
    }

    /// <summary>
    /// Metodo de obtener la descripcion de estado de cuenta
    /// </summary>
    /// <returns></returns>
    public void ActualizarDescripcionEstadoCuenta(string descripcion)
    {
        DescripcionMovimiento = SubTipoTransaccionMovimiento.EsComisionCCE 
            ? $"RECHAZO {SubTipoTransaccionMovimiento.DescripcionSubTransaccion}" 
            : $"TRANS. INTERB. INMEDIATA CCE {descripcion}";
    }
    #endregion Metodos
}
