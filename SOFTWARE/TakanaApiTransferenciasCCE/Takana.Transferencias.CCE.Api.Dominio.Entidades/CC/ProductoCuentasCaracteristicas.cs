using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
/// <summary>
/// Clase de dominio que representa a la entidad Producto cuentas caracteriscas
/// </summary>
public class ProductoCuentasCaracteristicas
{
    #region Constantes
    /// <summary>
    /// Contante que define el codigo del producto intangible Afp
    /// </summary>
    public const string CodigoProductoIntangible = "CC079";

    #endregion

    #region Propiedades
    /// <summary>
    /// Codigo de empresa
    /// </summary>
    public string CodigoEmpresa { get; private set; }
    /// <summary>
    /// Codigo de sistema
    /// </summary>
    public string CodigoSistema { get; private set; }
    /// <summary>
    /// Codigo de producto
    /// </summary>
    public string CodigoProducto { get; private set; }
    /// <summary>
    /// Monto minimo de saldo
    /// </summary>
    public decimal MontoMinimoSaldo { get; private set; }
    /// <summary>
    /// Codigo cuenta contable
    /// </summary>
    public string CodigoCuentaContable { get; private set; }
    /// <summary>
    /// Indicador de cuenta alterna
    /// </summary>
    public string IndCtaAlterna { get; private set; }
    /// <summary>
    /// Indicador de pagar intereses
    /// </summary>
    public string IndPagInteres { get; private set; }
    /// <summary>
    /// TIpo de asignacion de tasa
    /// </summary>
    public string TipoAsignacionTasa { get; private set; }
    /// <summary>
    /// Tipo de capitalizacion
    /// </summary>
    public string TipoCapitalizacion { get; private set; }
    /// <summary>
    /// Car de manejo
    /// </summary>
    public string CarManejo { get; private set; }
    /// <summary>
    /// Carg de sobregiro
    /// </summary>
    public string CarSobregiro { get; private set; }
    /// <summary>
    /// Car de reserva
    /// </summary>
    public string CarReserva { get; private set; }
    /// <summary>
    /// Limite de cheques girados
    /// </summary>
    public decimal LimCksGirados { get; private set; }
    /// <summary>
    /// Monto checke adicion
    /// </summary>
    public decimal MonCkAdicion { get; private set; }
    /// <summary>
    /// Tasa de sobre giro
    /// </summary>
    public string TasaSobregiro { get; private set; }
    /// <summary>
    /// Tasa de reserva
    /// </summary>
    public string TasaReserva { get; private set; }
    /// <summary>
    /// Codigo de producto asociado
    /// </summary>
    public string? CodigoProductoAsociado { get; private set; }
    /// <summary>
    /// Dia de base de reserva
    /// </summary>
    public decimal DiaBaseReserva { get; private set; }
    /// <summary>
    /// Dia de base de sobre giro
    /// </summary>
    public decimal DiaBaseSobregiro { get; private set; }
    /// <summary>
    /// Tasa de interes
    /// </summary>
    public string TasaInteres { get; private set; }
    /// <summary>
    /// Indicador chequera
    /// </summary>
    public string IndChequera { get; private set; }
    /// <summary>
    /// Indicador de modo de apertura
    /// </summary>
    public string IndModApertura { get; private set; }
    /// <summary>
    /// Indicador de reserva de prom
    /// </summary>
    public string IndReservaProm { get; private set; }
    /// <summary>
    /// Indicador de calculo de interes
    /// </summary>
    public string IndCalInteres { get; private set; }
    /// <summary>
    /// Dia de base de intereses
    /// </summary>
    public decimal DiaBaseInteres { get; private set; }
    /// <summary>
    /// Dia de base de calculo
    /// </summary>
    public decimal DiaBaseCalculo { get; private set; }
    /// <summary>
    /// Por disposicion CTS
    /// </summary>
    public decimal PorDispCTS { get; private set; }
    /// <summary>
    /// Indicador orden de pago
    /// </summary>
    public string IndOrdenPago { get; private set; }
    /// <summary>
    /// Ind Sorteo
    /// </summary>
    public string IndSorteo { get; private set; }
    /// <summary>
    /// Monto minimo de saldo apertura
    /// </summary>
    public decimal MontoMinimoSaldoApertura { get; private set; }
    /// <summary>
    /// Codigo de unidad
    /// </summary>
    public string CodigoUnidad { get; private set; }
    /// <summary>
    /// Indicador de cta de excendente
    /// </summary>
    public string IndCtaExcedente { get; private set; }
    /// <summary>
    /// Indicador de tipo de cuenta
    /// </summary>
    public string IndTipoCuenta { get; private set; }
    /// <summary>
    /// CAT del cliente
    /// </summary>
    public string CatCliente { get; private set; }
    /// <summary>
    /// Codigo del cliente
    /// </summary>
    public string? CodigoCliente { get; private set; }
    /// <summary>
    /// Cuenta interna CTS
    /// </summary>
    public string CtaIntCTS { get; private set; }
    /// <summary>
    /// Codigo de producto general
    /// </summary>
    public string CodigoProductoGeneral { get; private set; }
    /// <summary>
    /// Cuenta contable inactiva
    /// </summary>
    public string CuentaContableInactiva { get; private set; }
    /// <summary>
    /// Monto minimo de transaccion
    /// </summary>
    public decimal MontoMinimoTransaccion { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public decimal PorDispIntCTS { get; private set; }
    /// <summary>
    /// INdicador comision Exceso de retencion
    /// </summary>
    public string IndComiExcesoRet { get; private set; }
    /// <summary>
    /// PorDispExec
    /// </summary>
    public decimal PorDispExec { get; private set; }
    /// <summary>
    /// Monto remunerativo calclado
    /// </summary>
    public decimal NumRemuCal { get; private set; }
    /// <summary>
    /// Indidacro de terero
    /// </summary>
    public string IndTerceroML { get; private set; }
    /// <summary>
    /// Indicador afiliado a microseguro
    /// </summary>
    public string IndAfilMicroseguro { get; private set; }
    /// <summary>
    /// Monto maximo de saldo
    /// </summary>
    public decimal MontoMaximoSaldo { get; private set; }
    /// <summary>
    /// Monto maximo de saldo
    /// </summary>
    public decimal MontoMaximoTransaccionDia { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public decimal MontoMaximoTransaccionMes { get; private set; }
    /// <summary>
    /// Indicador de cheque otra plaza
    /// </summary>
    public string IndAplComChequeOtraPlaza { get; private set; }
    /// <summary>
    /// Indicador pago TCCCEHB
    /// </summary>
    public string IndPagoTCCCEHB { get; private set; }
    /// <summary>
    /// Indicador transferncia CCEHB
    /// </summary>
    public string IndTransfCCEHB { get; private set; }
    /// <summary>
    /// Indicador transferencia CCE TIN
    /// </summary>
    public string IndTransfCCETIN { get; private set; }
    ///
    /// <summary>
    /// Aplica
    /// </summary>
    public string AplicaParaDebitoAutomaticoCuentas { get; private set; }
    /// <summary>
    /// Indicador si el producto esta exonerado de comisiones
    /// </summary>
    public string IndicadorProductoExoneradoComision { get; private set; }
    /// <summary>
    /// Cantidad maxima de aperturas de cuentas vigentes del producto por cliente
    /// </summary>
    public byte NumeroMaximoAperturas { get; private set; }

    /// <summary>
    /// Productos que ofrece CMAC Tacna.
    /// </summary>
    public virtual Producto Producto { get; private set; }
    #endregion

    #region Calculadas
    /// <summary>
    /// Esta hahilitado para transferencias inmediatas
    /// </summary>
    public bool EsHabilitadoTIN { get { return IndTransfCCETIN == General.Si; } }
    #endregion
}


