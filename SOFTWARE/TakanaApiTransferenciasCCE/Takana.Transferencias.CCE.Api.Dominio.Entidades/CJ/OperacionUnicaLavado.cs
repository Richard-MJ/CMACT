using Takana.Transferencias.CCE.Api.Common.DTOs.CL;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
using CatalogoTransaccionEnum = Takana.Transferencias.CCE.Api.Common.Constantes.CatalogoTransaccionEnum;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CJ;
/// <summary>
/// Clase de dominio encargada del Umbral de operaciones del lavado unicos
/// </summary>
public class OperacionUnicaLavado : Empresa, IRegistroLavado
{
    #region Constantes
    /// <summary>
    /// Operacion activa
    /// </summary>
    public const string OperacionActiva = "A";
    /// <summary>
    /// Operacion pendiente
    /// </summary>
    public const string EstadoPendiente = "P";
    /// <summary>
    /// No regularizado
    /// </summary>
    public const string NoRegularizar = "N";
    /// <summary>
    /// Codigo de operacion unica
    /// </summary>
    public const string CodigoOperacionUnica = "U";
    /// <summary>
    /// Codigo de serie lavado
    /// </summary>
    public const string CodigoSerieLavado = "LAVADO";
    /// <summary>
    /// Modalidad  de otro medios de pagos  no presenciales
    /// </summary>
    public const int ModalidadOtrosMediosNoPresenciales = 2;
    /// <summary>
    /// Tipo de transaccion no valida inmediata
    /// </summary>
    public const string TipoTransaccionNoValidaInmediata = "5";
    /// <summary>
    /// Subtipo de transaccion no valida inmediata
    /// </summary>
    public const string SubTipoTransaccionNoValidaInmediata = "1";
    /// <summary>
    /// Descripcion de transferencia Inmediata
    /// </summary>
    public const string DescripcionTransferenciaInmediata = "TRANSFERENCIA INTERBANCARIA INMEDIATA ENTRANTE";
    /// <summary>
    /// Descripcion de transferencia Inmediata
    /// </summary>
    public const string DescripcionInteroperabilidad = "INTEROPERABILIDAD CCE";
    #endregion

    #region Propiedades
    /// <summary>
    /// Numero de lavado
    /// </summary>
    public decimal NumeroLavado { get; private set; }
    /// <summary>
    /// Codigo de agencia
    /// </summary>
    public string? CodigoAgencia { get; private set; }
    /// <summary>
    /// Codigo de sistema
    /// </summary>
    public string? CodigoSistema { get; private set; }
    /// <summary>
    /// Codigo de tipo de transaccion
    /// </summary>
    public string? CodigoTipoTransaccion { get; private set; }
    /// <summary>
    /// Numero de secuencia documento
    /// </summary>
    public decimal? NumeroSecuenciaDocumento { get; private set; }
    /// <summary>
    /// Codigo de moneda
    /// </summary>
    public string? CodigoMoneda { get; private set; }
    /// <summary>
    /// Numero cuenta
    /// </summary>
    public string? NumeroCuenta { get; private set; }
    /// <summary>
    /// Tipo de lavado
    /// </summary>
    public string? TipoLavado { get; private set; }
    /// <summary>
    /// Indicador de movimiento
    /// </summary>
    public string? IndicadorMovimiento { get; private set; }
    /// <summary>
    /// Monto de movimiento
    /// </summary>
    public decimal? MontoMovimiento { get; private set; }
    /// <summary>
    /// Fecha de operacion
    /// </summary>
    public DateTime FechaOperacion { get; private set; }
    /// <summary>
    /// Indicador de forma de pago
    /// </summary>
    public string? IndicadorFormaPago { get; private set; }
    /// <summary>
    /// Descripcion de fondo
    /// </summary>
    public string? DescripcionFondo { get; private set; }
    /// <summary>
    /// Numero de cuenta destino
    /// </summary>
    public string? NumeroCuentaDestino { get; private set; }
    /// <summary>
    /// Codigo de modalidad
    /// </summary>
    public int CodigoModalidad { get; private set; }
    /// <summary>
    /// Codigo sub modalidad
    /// </summary>
    public int CodigoSubModalidad { get; private set; }
    /// <summary>
    /// Numero de asiento
    /// </summary>
    public int NumeroAsiento { get; private set; }
    /// <summary>
    /// Codigo de forma de pago cj
    /// </summary>
    public string? CodigoFormaPagoCJ { get; private set; }
    /// <summary>
    /// Codigo de usuario
    /// </summary>
    public string? CodigoUsuario { get; private set; }
    /// <summary>
    /// Indicador de estado
    /// </summary>
    public string? IndicadorEstado { get; private set; }
    /// <summary>
    /// Fecha de servidor
    /// </summary>
    public DateTime FechaServidor { get; private set; }
    /// <summary>
    /// Numero de movimtoSistema
    /// </summary>
    public decimal? NumeroMovimtoSistema { get; private set; }
    /// <summary>
    /// Indicador de canal
    /// </summary>
    public string? IndicadorCanal { get; private set; }
    /// <summary>
    /// Codigo de sub tipo de transaccion
    /// </summary>
    public string? CodigoSubTipoTransaccion { get; private set; }
    /// <summary>
    /// Codigo de entidad SBS
    /// </summary>
    public string? CodigoEntidadSBS { get; private set; }
    /// <summary>
    /// Indicador por regularizar
    /// </summary>
    public string IndicadorPorRegularizar { get; private set; }
    /// <summary>
    /// Entidad detalle
    /// </summary>
    public virtual ICollection<OperacionUnicaDetalle> Detalle { get; private set; }
    public bool EsTransferenciaCCE => 
        (CodigoSistema == Sistema.CuentaEfectivo && CodigoTipoTransaccion == ((int)CatalogoTransaccionEnum.CodigoTransferenciaInmediataSaliente).ToString()
        && 
            CodigoSubTipoTransaccion == ((int)SubTipoTransaccionEnum.TransaccionInmediataOrdinariaSalida).ToString() 
        ||
           (CodigoSistema == Sistema.CuentaEfectivo && CodigoTipoTransaccion == ((int)CatalogoTransaccionEnum.CodigoTransferenciaInmediataSaliente).ToString()
        && 
            CodigoSubTipoTransaccion == ((int)SubTipoTransaccionEnum.TransaccionInmediataTarjetaSalida).ToString()));
    /// <summary>
    /// Clase de operacion unica lavado
    /// </summary>
    public OperacionUnicaLavado()
    {
        Detalle = new List<OperacionUnicaDetalle>();
    }
    #endregion

    #region Métodos
    /// <summary>
    /// Método de adicionar un interviniente
    /// </summary>
    /// <param name="interviniente"></param>
    /// <returns></returns>
    public IRegistroLavado AdicionarInterviniente(ILavadoInterviniente interviniente)
    {
        if (interviniente is OperacionUnicaDetalle)
            Detalle.Add(interviniente as OperacionUnicaDetalle);
        return this;
    }

    #region Implementación de IRegistroLavado
    /// <summary>
    /// Método de inaplicar lavado
    /// </summary>
    public void InaplicarLavado()
    {
        IndicadorEstado = General.No;
    }
    /// <summary>
    /// Método de completar datos de detalle
    /// </summary>
    /// <param name="origenes"></param>
    /// <param name="destino"></param>
    /// <param name="codigoCanal"></param>
    public void CompletarDatosDetalle(IList<IOperacionLavado> origenes, IOperacionLavado destino, string? codigoCanal)
    {
        var origen = origenes.FirstOrDefault();

        if (destino.SubTipoTransaccionMovimiento.CodigoSistema == Sistema.Cajas
            && destino.SubTipoTransaccionMovimiento.CodigoTipoTransaccion == TipoTransaccionNoValidaInmediata
            && destino.SubTipoTransaccionMovimiento.CodigoSubTipoTransaccion == SubTipoTransaccionNoValidaInmediata)
        {
            IndicadorMovimiento = destino.SubTipoTransaccionMovimiento.IndicadorMovimientoLavando;
            IndicadorEstado = OperacionActiva;
            NumeroSecuenciaDocumento = 0;
            NumeroCuenta = origen.NumeroCuenta;
            NumeroAsiento = origen.NumeroAsientoLavado;
            CodigoSistema = origen.SubTipoTransaccionMovimiento.CodigoSistema;
            CodigoTipoTransaccion = origen.SubTipoTransaccionMovimiento.CodigoTipoTransaccion;
            CodigoSubTipoTransaccion = origen.SubTipoTransaccionMovimiento.CodigoSubTipoTransaccion;
            NumeroMovimtoSistema = 0;
            CodigoFormaPagoCJ = destino.SubTipoTransaccionMovimiento.IndicadorMovimientoLavando;
            NumeroSecuenciaDocumento = destino.NumeroMovimiento;
            IndicadorCanal = General.CanalCCE;
        }
        else
        {
            IndicadorEstado = General.Activo;
            NumeroSecuenciaDocumento = 0;
            NumeroCuenta = origen.NumeroCuenta;
            NumeroAsiento = origen.NumeroAsientoLavado;
            CodigoSistema = origen.SubTipoTransaccionMovimiento.CodigoSistema;
            CodigoTipoTransaccion = origen.SubTipoTransaccionMovimiento.CodigoTipoTransaccion;
            CodigoSubTipoTransaccion = origen.SubTipoTransaccionMovimiento.CodigoSubTipoTransaccion;
            IndicadorMovimiento = EsTransferenciaCCE ? destino.SubTipoTransaccionMovimiento.IndicadorMovimientoLavando :
                                   origen.SubTipoTransaccionMovimiento.Transaccion.IndicadorMovimiento;
            NumeroMovimtoSistema = origen.NumeroMovimiento;
            NumeroCuentaDestino = destino?.NumeroCuenta ?? string.Empty;
            CodigoFormaPagoCJ = destino.FormaDePagoLavado;
            CodigoEntidadSBS = destino?.CodigoEntidadSbs ?? destino.CodigoEntidadSbs;
            IndicadorCanal = General.CanalCCE;
        }
    }

    /// <summary>
    /// Método que crea una operacion unica de lavado para entrante
    /// </summary>
    /// <param name="transferencia"></param>
    /// <param name="asiento"></param>
    /// <param name="clienteOriginante"></param>
    /// <param name="fechaSistema"></param>
    /// <param name="banco"></param>
    /// <param name="numeroLavado"></param>
    /// <returns>Retorna la operacion unica del lavado</returns>
    public static OperacionUnicaLavado Crear(
        Transferencia transferencia,
        AsientoContable asiento,
        ClienteExternoDTO clienteOriginante,
        DateTime fechaSistema,
        EntidadFinancieraInmediata banco,
        decimal numeroLavado,
        string descripcionTransferencia,
        string codigoTipoTransaferencia,
        string codigoSubTipoTransaferencia
    )
    {
        return new OperacionUnicaLavado()
        {
            CodigoAgencia = transferencia.CodigoAgencia,
            CodigoEmpresa = transferencia.CodigoEmpresa,
            CodigoModalidad = General.ModalidadOtrosMediosNoPresenciales,
            CodigoSubModalidad = General.SubModalidadCajeroCCE,
            CodigoMoneda = transferencia.CodigoMoneda,
            CodigoSistema = Sistema.CuentaEfectivo,
            CodigoTipoTransaccion = codigoTipoTransaferencia,
            CodigoUsuario = transferencia.CodigoUsuario,
            DescripcionFondo = descripcionTransferencia,
            FechaOperacion = transferencia.FechaTransferencia,
            FechaServidor = fechaSistema,
            IndicadorCanal = General.CanalCCE,
            IndicadorMovimiento = Movimiento.Ingreso,
            IndicadorPorRegularizar = OperacionUnicaLavado.NoRegularizar,
            MontoMovimiento = transferencia.MontoTransferencia,
            NumeroAsiento = asiento.NumeroAsiento,
            NumeroCuenta = clienteOriginante.CodigoCuentaInterbancaria,
            NumeroCuentaDestino = transferencia.NumeroCuenta,
            NumeroMovimtoSistema = transferencia.NumeroMovimiento,
            NumeroSecuenciaDocumento = 0,
            TipoLavado = OperacionUnicaLavado.CodigoOperacionUnica,
            NumeroLavado = numeroLavado,
            CodigoSubTipoTransaccion = codigoSubTipoTransaferencia,
            IndicadorEstado = OperacionActiva,
            IndicadorFormaPago = General.FormaPagoCuentaCorriente,
            CodigoFormaPagoCJ = General.FormaPagoCuentaCorriente,
            CodigoEntidadSBS = banco.CodigoEntidadSBS
        };
    }

    /// <summary>
    /// Método que crea una operacion unica de lavado para saliente 
    /// </summary>
    /// <param name="codigoMoneda"></param>
    /// <param name="codigoCCI"></param>
    /// <param name="montoOperacion"></param>
    /// <param name="fechaSistema"></param>
    /// <param name="numeroLavado"></param>
    /// <param name="codigoTipoTransaccion"></param>
    /// <param name="codigoSubTipoTransaccion"></param>
    /// <returns>Retorna la operacion unica del lavado</returns>
    public static OperacionUnicaLavado CrearLavado(
        string codigoMoneda,
        string codigoCCI,
        decimal montoOperacion,
        DateTime fechaSistema,
        int numeroLavado,
        string codigoTipoTransaccion,
        string codigoSubTipoTransaccion
    )
    {
        return new OperacionUnicaLavado()
        {
            CodigoEmpresa = Empresa.CodigoPrincipal,
            NumeroLavado = numeroLavado,
            CodigoAgencia = Agencia.Principal,
            CodigoSistema = Sistema.CuentaEfectivo,
            CodigoTipoTransaccion = codigoTipoTransaccion,
            CodigoSubTipoTransaccion = codigoSubTipoTransaccion,
            NumeroSecuenciaDocumento = 0,
            CodigoMoneda = codigoMoneda,
            NumeroCuenta = codigoCCI,
            TipoLavado = CodigoOperacionUnica,
            MontoMovimiento = montoOperacion,
            IndicadorFormaPago = General.FormaPagoCuentaCorriente,
            DescripcionFondo = DescripcionInteroperabilidad,
            CodigoModalidad = General.ModalidadOtrosMediosNoPresenciales,
            CodigoSubModalidad = General.SubModalidadAppMovil,
            NumeroAsiento = 0,
            CodigoFormaPagoCJ = General.FormaPagoCuentaCorriente,
            CodigoUsuario = General.UsuarioPorDefectoInteroperabilidad,
            IndicadorEstado = EstadoPendiente,
            FechaServidor = fechaSistema,
            NumeroMovimtoSistema = 0,
            IndicadorCanal = General.CanalCCE,
            IndicadorPorRegularizar = NoRegularizar,
            FechaOperacion = fechaSistema,
        };
    }


    /// <summary>
    /// Indicador de tipo de lavado
    /// </summary>
    public string? IndicadorTipoLavado => IRegistroLavado.Unico;
    /// <summary>
    /// Número de operacion lavado
    /// </summary>
    public int NumeroOperacionLavado => Convert.ToInt32(NumeroLavado);

    #endregion Implementación de IRegistroLavado

    #endregion
}
