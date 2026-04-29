using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using static Takana.Transferencias.CCE.Api.Dominio.Entidades.CF.Moneda;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

/// <summary>
/// Clase de dominio que representa a las Cuentas de Ahorro Corriente
/// </summary>
public class CuentaEfectivo : Empresa
{
    #region Constantes
    /// <summary>
    /// Indicador individual
    /// </summary>
    public const string Individual = "I";
    /// <summary>
    /// Indicador de cuenta mancomunada
    /// </summary>
    public const string Mancomunada = "M";
    /// <summary>
    /// Indicador de cuenta CTS
    /// </summary>
    public const string IndicadorCuentaCts = "1";
    /// <summary>
    /// Indicador de tipo comercio
    /// </summary>
    public const string IndicadorTipoComercio = "1";
    /// <summary>
    /// Estado activo
    /// </summary>
    public const string EstadoActivo = "A";
    /// <summary>
    /// Tipo de cuenta segundaria
    /// </summary>
    public const string CuentaComercialSegundaria = "2";
    /// <summary>
    /// Cuenta de ahorro sueldo en soles
    /// </summary>
    public const string CuentaAhorroSueldoSoles = "CC063";
    /// <summary>
    /// Cuenta de ahorro sueldo de dolares
    /// </summary>
    public const string CuentaAhorroSueldoDolares = "CC064";

    #endregion Constantes

    #region Constante de Estados
    /// <summary>
    /// Estado activo general
    /// </summary>
    public const string Activo = "A";
    /// <summary>
    /// Estado de bloquedo parcial
    /// </summary>
    public const string BloqueoParcial = "B";
    /// <summary>
    /// Estado Anulado general
    /// </summary>
    public const string Anulado = "N";
    /// <summary>
    /// Estado Cancelado general
    /// </summary>
    public const string Cancelado = "C";
    /// <summary>
    /// Estado Inactiva general
    /// </summary>
    public const string Inactiva = "I";
    /// <summary>
    /// Estado BloqueoTotal general
    /// </summary>
    public const string BloqueoTotal = "T";
    /// <summary>
    /// Estado de cuenta registrada
    /// </summary>
    public const string Registrada = "R";
    /// <summary>
    /// Estado CuentaValida general
    /// </summary>
    public const string CuentaValida = "S";
    /// <summary>
    /// Estado CuentaNoValida general
    /// </summary>
    public const string CuentaNoValida = "N";
    #endregion

    #region Propiedades

    #region Internas
    /// <summary>
    /// Codigo agencia
    /// </summary>
    public string CodigoAgencia { get; private set; }
    /// <summary>
    /// Numero de cuenta
    /// </summary>
    public string NumeroCuenta { get; private set; }
    /// <summary>
    /// Codigo de producto
    /// </summary>
    public string CodigoProducto { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public string CodigoCliente { get; private set; }
    /// <summary>
    /// Saldo disponible
    /// </summary>
    public decimal SaldoDisponible { get; private set; }
    /// <summary>
    /// Saldo intangible
    /// </summary>
    public decimal? SaldoIntangible { get; private set; }
    /// <summary>
    /// Saldo transito
    /// </summary>
    public decimal? SaldoTransito { get; private set; }
    /// <summary>
    /// Saldo reserva
    /// </summary>
    public decimal? SaldoReserva { get; private set; }
    /// <summary>
    /// Interes disponible
    /// </summary>
    public decimal? InteresDisponible { get; private set; }
    /// <summary>
    /// Interes intangible
    /// </summary>
    public decimal? InteresIntangible { get; private set; }
    /// <summary>
    /// Codigo modalidad
    /// </summary>
    public int CodigoModalidad { get; private set; }
    /// <summary>
    /// Codigo estado
    /// </summary>
    public string? CodigoEstado { get; private set; }
    /// <summary>
    /// Fecha estado
    /// </summary>
    public DateTime FechaEstado { get; private set; }
    /// <summary>
    /// Fecha apertura
    /// </summary>
    public DateTime? FechaApertura { get; private set; }
    /// <summary>
    /// Fecha vencimiento
    /// </summary>
    public DateTime? FechaVencimiento { get; private set; }
    /// <summary>
    /// Fecha inicial de sobre giro
    /// </summary>
    public DateTime? FechaInicialSobregiro { get; private set; }
    /// <summary>
    /// Fecha ultima de actualizacion de interes
    /// </summary>
    public DateTime? FechaUltimaActualizacionIntereses { get; private set; }
    /// <summary>
    /// Fecha de ultima capitaizacion de interes
    /// </summary>
    public DateTime? FechaUltimaCapitalizacionIntereses { get; private set; }
    /// <summary>
    /// Indicador de tipo comercial
    /// </summary>
    public string? IndicadorTipoComercial { get; private set; }
    /// <summary>
    /// Indicador de tipo asociacion
    /// </summary>
    public string? IndicadorTipoAsociacion { get; private set; }
    /// <summary>
    /// Codigo de la moneda
    /// </summary>
    public string CodigoMoneda { get; private set; }
    /// <summary>
    /// Codigo de sistema
    /// </summary>
    public string? CodigoSistema { get; private set; }
    /// <summary>
    /// Codigo de cuenta cuenta interbancario
    /// </summary>
    public string? CodigoCuentaInterbancario { get; private set; }
    /// <summary>
    /// Codigo cliente de patrono
    /// </summary>
    public string? CodigoClientePatrono { get; private set; }
    /// <summary>
    /// Codigo de usuario de apertura
    /// </summary>
    public string? CodigoUsuarioApertura { get; private set; }
    /// <summary>
    /// Nombre de chequera
    /// </summary>
    public string? NombreChequera { get; private set; }
    /// <summary>
    /// Indicador de tipo de cargos
    /// </summary>
    public string? IndicadorTipoCargos { get; private set; }
    /// <summary>
    /// Indicador de cuenta alterna
    /// </summary>
    public string? IndicadorCuentaAlterna { get; private set; }
    /// <summary>
    /// Indicador de paga de interes
    /// </summary>
    public string? IndicadorPagaIntereses { get; private set; }
    /// <summary>
    /// Saldo consultado
    /// </summary>
    public decimal? SaldoConsultado { get; private set; }
    /// <summary>
    /// Saldo de promedio
    /// </summary>
    public decimal? SaldoPromedio { get; private set; }
    /// <summary>
    /// Saldo de ultimo corte
    /// </summary>
    public decimal? SaldoUltimoCorte { get; private set; }
    /// <summary>
    /// Monto de saldo
    /// </summary>
    public decimal? MontoSaldoEnReservaUtilizado { get; private set; }
    /// <summary>
    /// Monto sobre precio pactado
    /// </summary>
    public decimal? MontoSobreGiroPrePactado { get; private set; }
    /// <summary>
    /// Monto sobre giro temporal
    /// </summary>
    public decimal? MontoSobreGiroTemporal { get; private set; }
    /// <summary>
    /// Monto sobre giro disponible
    /// </summary>
    public decimal? MontoSobreGiroDisponible { get; private set; }
    /// <summary>
    /// Monto total de cargos
    /// </summary>
    public decimal? MontoTotalCargos { get; private set; }
    /// <summary>
    /// Interes por capital congelado
    /// </summary>
    public decimal? InteresPorCapitalCongelado { get; private set; }
    /// <summary>
    /// Interes por capital en reserva
    /// </summary>
    public decimal? InteresPorCapitalEnReserva { get; private set; }
    /// <summary>
    /// Interes por sobre giro pre practo
    /// </summary>
    public decimal? InteresPorSobreGiroPrePactado { get; private set; }
    /// <summary>
    /// Interes por reserva utilizada
    /// </summary>
    public decimal? InteresPorReservaUtilizada { get; private set; }
    /// <summary>
    /// Indicador de sobre giro
    /// </summary>
    public string? IndicadorSobreGiro { get; private set; }
    /// <summary>
    /// Numero de cuenta relacionada
    /// </summary>
    public string? NumeroCuentaRelacionada { get; private set; }
    /// <summary>
    /// Ineres ganado por mes actual
    /// </summary>
    public decimal? InteresGanadoMesActual { get; private set; }
    /// <summary>
    /// Indicador tipo de correspondencia
    /// </summary>
    public string? IndicadorTipoCorrespondencia { get; private set; }
    /// <summary>
    /// fecha de ultimo de movimiento
    /// </summary>
    public DateTime FechaUltimoMovimiento { get; private set; }
    /// <summary>
    /// Observacion de estado cuenta
    /// </summary>
    public string? ObservacionEstadoCuenta { get; private set; }
    /// <summary>
    /// Indicador tipo de cliente
    /// </summary>
    public string? IndicadorTipoCliente { get; private set; }
    /// <summary>
    /// Numero de cuenta completo
    /// </summary>
    public string? NumeroCuentaCompleto { get; private set; }
    /// <summary>
    /// Indicador cuenta contable intermediacion
    /// </summary>
    public string? IndicadorCuentaContableIntermediacion { get; private set; }
    /// <summary>
    /// Indicador para envio de email
    /// </summary>
    public string? IndicadorEnvioEmail { get; private set; }
    /// <summary>
    /// Codigo de direccion
    /// </summary>
    public string? CodigoDireccion { get; private set; }
    /// <summary>
    /// Codigo de categoria
    /// </summary>
    public string? CodigoCategoria { get; private set; }
    /// <summary>
    /// Codigo de estado de cuenta
    /// </summary>
    public string? CodigoEstadoCuenta { get; private set; }
    /// <summary>
    /// Monto de tasa preferencial
    /// </summary>
    public decimal? MontoTasaPreferencial { get; private set; }
    /// <summary>
    /// Codigo motivo
    /// </summary>
    public decimal? CodigoMotivoBloqueo { get; private set; }
    /// <summary>
    /// Numero de movimiento
    /// </summary>
    public decimal? NumeroMovimiento { get; private set; }
    /// <summary>
    /// Indicador deposito plan ahorro
    /// </summary>
    public string? IndicadorPlanAhorro { get; private set; }
    /// <summary>
    /// Moneda 
    /// </summary>
    private Moneda _moneda;
    /// <summary>
    /// Moneda
    /// </summary>
    virtual public Moneda Moneda
    {
        get { return _moneda; }
        set
        {
            _moneda = value;
            CodigoMoneda = value.CodigoMoneda;
        }
    }

    /// <summary>
    /// Propiedad virtual que define registro en productos exonerados
    /// </summary>
    
    public virtual Producto Producto { get; private set; }    
    /// <summary>
    /// Cuenta Exonerada
    /// </summary>
    public virtual ProductoExonerado CuentaExonerada { get; private set; }
    /// <summary>
    /// Listado de comisiones exoneradas (no considera movimientos anulados según definición de la vista)
    /// </summary>
    public virtual ICollection<ComisionesExoneradasVista> ComisionesExoneradas { get; set; }
    /// <summary>
    /// Caracteristicas del producto
    /// </summary>
    public virtual ProductoCuentasCaracteristicas Caracteristicas { get; private set; }
    
    /// <summary>
    /// Firmas del cliente
    /// </summary>
    public virtual ICollection<FirmaCliente> Firmas { get; private set; }
    
    /// <summary>
    /// Movimiento diario
    /// </summary>
    public virtual ICollection<MovimientoDiario> MovimientosDiarios { get; private set; }
    /// <summary>
    /// Entidad LimitesOperacionesCuenta que guarda los 
    /// </summary>
    public virtual ICollection<LimitesOperacionesCuenta> LimitesOperacionesCuenta { get; private set; }
    /// <summary>
    /// Cuenta efectivo Sueldo
    /// </summary>
    public virtual CuentaEfectivoSueldo CuentaEfectivoSueldo { get; set; }
    /// <summary>
    /// Cliente de cuenta efectivo
    /// </summary>
    public virtual Cliente Cliente { get; private set; }    
    /// <summary>
    /// Agencia de la cuenta
    /// </summary>
    public virtual Agencia Agencia { get; private set; }
    /// <summary>
    /// Estado de la cuenta
    /// </summary>
    public virtual EstadoCuenta EstadoCuenta { get; set; }
    /// <summary>
    /// Direccion del cliente
    /// </summary>
    public virtual DireccionCliente DireccionCliente { get; private set; }
    /// <summary>
    /// Tipo de grupo
    /// </summary>
    public virtual TipoCuentaGrupo TipoCuentaGrupo { get; private set; }
    /// <summary>
    /// Entidad AliasProductoCliente correspondiente al producto
    /// </summary>
    public virtual AliasProductoCliente AliasProductoCliente { get; set; }
    #endregion Internas

    #region Calculadas
    /// <summary>
    /// Descripcion del monto
    /// </summary>
    public string? DescripcionMonto { get { return IndicadorTipoComercial == IndicadorTipoComercio ? "saldo contable" : "saldo disponible"; } }
    /// <summary>
    /// Monto disponible de la cuenta
    /// </summary>
    public decimal? MontoDisponible { get { return SaldoDisponible; } }
    /// <summary>
    /// Monto contable
    /// </summary>
    public decimal? MontoContable { get { return SaldoDisponible + SaldoReserva + SaldoTransito + SaldoIntangible; } }
    /// <summary>
    /// Identificador del CCI
    /// </summary>
    public string? IdentificadorCci { get { return CodigoCuentaInterbancario; } }
    /// <summary>
    /// Numero de producto
    /// </summary>
    public string? NumeroProducto { get { return NumeroCuenta; } }
    /// <summary>
    /// Codigo de sistema
    /// </summary>
    public string? CodigoTipo { get { return CodigoSistema; } }
    /// <summary>
    /// Simbolo de la moneda
    /// </summary>
    public string? SimboloMoneda { get { return Moneda.Moneda_ML; } }
    /// <summary>
    /// Es cuenta activa o no
    /// </summary>
    public bool EsCuentaActiva { get { return CodigoEstado == EstadoActivo; } }
    /// <summary>
    /// Es cuanta de sueldo
    /// </summary>
    public bool EsCuentaSueldo { get { return (CodigoProducto == CuentaAhorroSueldoSoles || CodigoProducto == CuentaAhorroSueldoDolares); } }
    /// <summary>
    /// Es cuenta cts
    /// </summary>
    public bool EsCuentaCTS{ get { return IndicadorTipoComercial == IndicadorCuentaCts; } }
    /// <summary>
    /// Tiene asignado CCI
    /// </summary>
    public bool TieneAsignadoCCI { get { return !string.IsNullOrEmpty(CodigoCuentaInterbancario); } }
    /// <summary>
    /// Indica si la cuenta esta exonerada de ITF
    /// </summary>
    public bool EsExoneradaITF
        => IndicadorTipoComercial == IndicadorTipoComercio
        || (
            (CuentaExonerada?.IndicadorEstado ?? "") == General.Activo
        );

    /// <summary>
    /// Método que me indica si es dueño
    /// </summary>
    /// <param name="codigoCliente"></param>
    /// <returns></returns>
    public bool EsDuenio(string codigoCliente)
    {
        var duenio = false;

        foreach (var firma in Firmas)
        {
            if (firma.Cliente.CodigoCliente == codigoCliente && firma.EsPropietario())
                duenio = true;
        }

        return duenio;
    }

    /// <summary>
    /// Obtener codigo de cuenta contable
    /// </summary>
    /// <param name="indicador">indicador</param>
    /// <returns>Retorna el codigo</returns>
    public string ObtenerCodigoCuentaContable(string indicador)
    {
        var codigoCuentaContable = string.Empty;
        if (indicador == AsientoContableDetalle.IndicadorEntrante)
            codigoCuentaContable = CodigoMoneda == ((int)MonedaCodigo.Soles).ToString()
                ? ParametroPorEmpresa.CodigoCuentaContableEntradaSoles
                : ParametroPorEmpresa.CodigoCuentaContableEntradaDolares;
        else
            codigoCuentaContable = CodigoMoneda == ((int)MonedaCodigo.Soles).ToString()
                ? ParametroPorEmpresa.CodigoCuentaContableSalidaSoles
                : ParametroPorEmpresa.CodigoCuentaContableSalidaDolares;
        return codigoCuentaContable;
    }
    /// <summary>
    /// Puede ser debitada sin monto minimo
    /// </summary>
    /// <param name="monto"></param>
    /// <returns>True si puede ser debitada</returns>
    public bool PuedeSerDebitadaSinMontoMinimo(decimal monto)
    {
        return (SaldoDisponible >= monto);
    }
    /// <summary>
    /// Estado adecuado para un desposito
    /// </summary>
    /// <returns></returns>
    public bool SuEstadoEsAdecuadoParaUnDeposito()
    {
        return CodigoEstado == EstadoActivo || CodigoEstado == BloqueoParcial || CodigoEstado == Registrada;
    }
    /// <summary>
    /// Deposita el monto
    /// </summary>
    /// <param name="monto">Monto de la operacion</param>
    /// <param name="fechaSistema">Fecha de sistema</param>
    public void Depositar(decimal monto, DateTime fechaSistema)
    {
        if (!SuEstadoEsAdecuadoParaUnDeposito())
            throw new ValidacionException("Estado de cuenta no válida para depósitos.");

        if (CodigoEstado == Registrada)
        {
            CodigoMotivoBloqueo = 3;
            CodigoEstado = BloqueoParcial;
            FechaEstado = fechaSistema;
            FechaUltimoMovimiento = fechaSistema;
        }

        SaldoDisponible += monto;
    }
    /// <summary>
    /// Debita por operacion de ITF
    /// </summary>
    /// <param name="monto">Monto de la operacion ITF</param>
    public void DebitarPorOperacionITF(decimal monto)
    {
        if (!SuEstadoEsAdecuadoParaUnDeposito())
            throw new ValidacionException("Estado de cuenta no válida para depósitos.");

        if (!PuedeSerDebitadaSinMontoMinimo(monto))
            throw new ValidacionException("Saldo insuficiente.");

        SaldoDisponible -= monto;
    }
    /// <summary>
    /// Indicador exonerada de comisiones o no
    /// </summary>
    public bool EsExoneradaComisiones
    {
        get
        {
            return (IndicadorTipoComercial == IndicadorTipoComercio
                || Caracteristicas.IndicadorProductoExoneradoComision == General.Si);
        }
    }
    /// <summary>
    /// Indicador si es exonerada de impuestos
    /// </summary>
    public bool EsExoneradaImpuestos
    {
        get { return (IndicadorTipoComercial == IndicadorTipoComercio 
                || IndicadorTipoComercial == CuentaComercialSegundaria); }
    }
    /// <summary>
    /// Indicador puede ser debitada o no
    /// </summary>
    /// <param name="monto">Monto a debitar</param>
    public bool PuedeSerDebitada(decimal monto)
    {
        return ((SaldoDisponible - Caracteristicas.MontoMinimoSaldo) >= monto);
    }
    /// <summary>
    /// Valida que tenga el monto minimo del tipo de producto
    /// </summary>
    /// <returns>true si el saldo disponible es mayor al monto minimo del producto</returns>
    public bool TieneSaldoMinimoSuficiente()
    {
        return SaldoDisponible >= Caracteristicas.MontoMinimoTransaccion;
    }
    /// <summary>
    /// Método para actualizar la fecha del último movimiento de la cuenta
    /// </summary>
    /// <param name="fecha">Fecha del movimiento</param>
    public void ActualizarFechaUltimoMovimiento(DateTime fecha)
    {
        FechaUltimoMovimiento = fecha;
    }
    /// <summary>
    /// Realiza credito por transferencia
    /// </summary>
    /// <param name="monto"></param>
    public void DepositarReversion(decimal monto)
    {
        SaldoDisponible += monto;
    }
    /// <summary>
    /// Retira el monto 
    /// </summary>
    /// <param name="monto">Monto a retirar</param>
    public void RetirarMonto(decimal monto)
    {
        SaldoDisponible -= monto;
    }
    #endregion Calculadas

    #endregion Propiedades
}