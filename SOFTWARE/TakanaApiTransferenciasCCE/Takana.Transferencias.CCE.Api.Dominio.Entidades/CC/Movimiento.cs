using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

/// <summary>
/// Clase abstracta que representa a una entidad de dominio de los movimientos de cuentas de ahorro
/// </summary>
public abstract class Movimiento : Empresa
{
    #region Constantes
    /// <summary>
    /// Indicador de movimiento como ingreso
    /// </summary>
    public const string Ingreso = "I";
    /// <summary>
    /// Indicador de movimiento como egreso
    /// </summary>
    public const string Egreso = "E";
    /// <summary>
    /// Codigo de serie para movimienot diario en CC
    /// </summary>
    public const string CodigoSerieMovimientoDiarioEnCC = "CC_MOV_DIA";
    #endregion

    #region Propiedades
    /// <summary>
    /// Numero de movimiento
    /// </summary>
    public decimal NumeroMovimiento { get; protected set; }
    /// <summary>
    /// Numero de movimiento fuente
    /// </summary>
    public decimal NumeroMovimientoFuente { get; protected set; }
    /// <summary>
    /// Numero de cuenta
    /// </summary>
    public string NumeroCuenta { get; protected set; }
    /// <summary>
    /// Fecha de movimiento
    /// </summary>
    public DateTime FechaMovimiento { get; protected set; }
    /// <summary>
    /// Estado de movimiento
    /// </summary>
    public string EstadoMovimiento { get; protected set; }
    /// <summary>
    /// Monto de movimiento
    /// </summary>
    public decimal MontoMovimiento { get; protected set; }
    /// <summary>
    /// Codigo de sistema
    /// </summary>
    public string CodigoSistema { get; protected set; }
    /// <summary>
    /// Codigo de tipo transaccion
    /// </summary>
    public string CodigoTipoTransaccion { get; protected set; }
    /// <summary>
    /// Codigo de subtipo de transaccion
    /// </summary>
    public string CodigoSubTipoTransaccion { get; protected set; }
    /// <summary>
    /// Codigo de usuario
    /// </summary>
    public string CodigoUsuario { get; protected set; }
    /// <summary>
    /// Numero de asiento contable
    /// </summary>
    public decimal NumeroAsiento { get; protected set; }
    /// <summary>
    /// Codigo de producto
    /// </summary>
    public string CodigoProducto { get; protected set; }
    /// <summary>
    /// Numero de documento
    /// </summary>
    public decimal NumeroDocumento { get; protected set; }
    /// <summary>
    /// Indicador de aplica cargo
    /// </summary>
    public string IndAplicaCargo { get; protected set; }
    /// <summary>
    /// Sistema puente
    /// </summary>
    public string SistemaFuente { get; protected set; }
    /// <summary>
    /// Descripcion de movimiento
    /// </summary>
    public string DescripcionMovimiento { get; protected set; }
    /// <summary>
    /// Codigo de agencia
    /// </summary>
    public string CodigoAgencia { get; protected set; }
    /// <summary>
    /// Indicador de consolidado
    /// </summary>
    public string IndConsolidado { get; protected set; }
    /// <summary>
    /// Monto disponible
    /// </summary>
    public decimal MontoDisponible { get; protected set; }
    /// <summary>
    /// Monto intangible
    /// </summary>
    public decimal MontoIntangible { get; protected set; }
    /// <summary>
    /// Indicador de afecta ITF
    /// </summary>
    public string IndAfectaITF { get; protected set; }
    /// <summary>
    /// Indicador de automatico
    /// </summary>
    public string IndDepAutomatico { get; protected set; }
    /// <summary>
    /// Indicador de origen destino
    /// </summary>
    public string IndOrigenDestino { get; protected set; }
    /// <summary>
    /// Feha de servidor
    /// </summary>
    public DateTime FechaServidor { get; protected set; }
    /// <summary>
    /// Indicador de movimiento remunerativo
    /// </summary>
    public string? IndicadorRemunerativo { get; protected set; }
    /// <summary>
    /// Datos de cuenta 
    /// </summary>
    public virtual CuentaEfectivo Cuenta { get; protected set; }
    /// <summary>
    /// Datos de transaccion
    /// </summary>
    public virtual CatalogoTransaccion Transaccion { get; protected set; }
    /// <summary>
    /// Datos de dusb tipo de transaccion
    /// </summary>
    public virtual SubTipoTransaccion SubTipoTransaccionMovimiento { get; protected set; }
    /// <summary>
    /// Datos de agencia de operacion
    /// </summary>
    public virtual Agencia AgenciaOperacion { get; protected set; }
    #endregion

    #region Propiedades Calculadas

    /// <summary>
    /// Indica si la transacción es de ITF
    /// </summary>
    public bool EsTransaccionITF => CodigoTipoTransaccion == ((int)CatalogoTransaccionEnum.CodigoTransaccionCargoITF).ToString();

    /// <summary>
    /// Determina si el movimiento es movimiento principal, a partir de la cual se originan los
    /// demas movimientos.
    /// </summary>
    public bool EsMovimientoPrincipal => EsMovimientoOrigen && NumeroMovimientoFuente == 0;

    /// <summary>
    /// Determina si el movimiento es un movimiento de origen de una transacción.
    /// </summary>
    public bool EsMovimientoOrigen => (IndOrigenDestino == MovimientoDiario.Desconocido || IndOrigenDestino == MovimientoDiario.Origen);

    /// <summary>
    /// Valida si es movimiento destino
    /// </summary>
    public bool EsMovimientoDestino => IndOrigenDestino == MovimientoDiario.Destino && NumeroMovimientoFuente > 0;

    /// <summary>
    /// Tipo del monto de movimiento, remunerativo o no remunerativo.
    /// </summary>
    public TipoMontoCuentaEfectivo TipoMontoMovimiento
    {
        get
        {
            if (IndicadorRemunerativo == null)
            {
                return Cuenta.EsCuentaSueldo
                    ? TipoMontoCuentaEfectivo.Remunerativo
                    : TipoMontoCuentaEfectivo.NoRemunerativo;
            }
            return IndicadorRemunerativo == MovimientoDiario.Remunerativo
                ? TipoMontoCuentaEfectivo.Remunerativo
                : TipoMontoCuentaEfectivo.NoRemunerativo;
        }
    }

    #endregion Propiedades Calculadas
}

