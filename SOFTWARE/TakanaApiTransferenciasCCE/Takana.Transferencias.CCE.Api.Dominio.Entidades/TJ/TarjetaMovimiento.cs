using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.TJ;

public class TarjetaMovimiento
{
    public decimal MontoTransaccion => 0;

    /// <summary>
    /// Código de la agencia en el que se hizo el movimeinto
    /// </summary>
    public string CodigoAgencia { get; private set; }

    /// <summary>
    /// Número de movimiento, obtenido de una serie (SEC_MOVIMT)
    /// </summary>
    public decimal IdMovimento { get; private set; }

    /// <summary>
    /// Código del usuario que realizo el movimiento.
    /// </summary>
    public string CodigoUsuario { get; private set; }

    /// <summary>
    /// Código del cliente al que pertenece la tarjeta.
    /// </summary>
    public string CodigoCliente { get; private set; }

    /// <summary>
    /// Número de Tarjeta.
    /// </summary>
    public decimal NumeroTarjeta { get; private set; }

    /// <summary>
    /// Tipo de transacción (en relación a catal_transacciones)
    /// </summary>
    public string CodigoTipoTransaccion { get; private set; }

    /// <summary>
    /// Subtipo de Transaccion (en relacion  a la tabla subtip_transac)
    /// </summary>
    public string CodigoSubTipoTransaccion { get; private set; }

    /// <summary>
    /// Fecha en el que se realizoel movimiento.
    /// </summary>
    public DateTime FechaMovimiento { get; private set; }

    /// <summary>
    /// Indicador de estado del movimiento (A Activo,N Nulo)
    /// </summary>
    public string IndicadorEstado { get; private set; }

    /// <summary>
    /// Observación del movimiento
    /// </summary>
    public string Observaciones { get; private set; }

    /// <summary>
    /// Indicador de Canal.
    /// </summary>
    public string? IndicadorCanal { get; private set; }

    /// <summary>
    /// Indicador de Canal.
    /// </summary>
    public string? DescripcionObservacion { get; set; }

    /// <summary>
    /// Número de movimiento fuente
    /// </summary>
    public decimal NumeroMovimientoFuente { get; private set; }
    /// <summary>
    /// Tarjeta
    /// </summary>
    public virtual Tarjeta Tarjeta { get; set; }
    /// c<summary>
    /// Codigo de sistema
    /// </summary>
    #region ITransaccion
    public string CodigoSistema { get; set; } = "TJ";
    /// <summary>
    /// Numero de operacion
    /// </summary>

    public int NumeroOperacion => (int)IdMovimento;
    #endregion


    public const string CODIGO_SERIE = "SEC_MOVIMT";

    public static TarjetaMovimiento Crear(
        decimal numeroSerie,
        Tarjeta tarjeta,
        string codigoUsuario,
        string codigoAgencia,
        SubTipoTransaccion subTipoTransaccion,
        DateTime fechaMovimiento,
        string indicadorEstado,
        string observaciones,
        string indicadorCanal)
    {
        return new TarjetaMovimiento()
        {
            IdMovimento = numeroSerie,
            NumeroTarjeta = tarjeta.NumeroTarjeta,
            CodigoUsuario = codigoUsuario,
            CodigoAgencia = codigoAgencia,
            CodigoCliente = tarjeta.CodigoCliente!,
            CodigoTipoTransaccion = subTipoTransaccion.CodigoTipoTransaccion,
            CodigoSubTipoTransaccion = subTipoTransaccion.CodigoSubTipoTransaccion,
            FechaMovimiento = fechaMovimiento,
            IndicadorEstado = indicadorEstado,
            Observaciones = observaciones,
            IndicadorCanal = indicadorCanal
        };
    }

    public void AsignarMovimientoFuente(decimal numeroMovimiento, string? descripcion = null)
    {
        NumeroMovimientoFuente = numeroMovimiento;
        DescripcionObservacion = descripcion ?? $"Movimiento de origen es {numeroMovimiento}";
    }
}
