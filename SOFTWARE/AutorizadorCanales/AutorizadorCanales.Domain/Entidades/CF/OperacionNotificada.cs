namespace AutorizadorCanales.Domain.Entidades.CF;

public class OperacionNotificada
{
    /// <summary>
    /// Identificador
    /// </summary>
    public int IdOperacionNotificada { get; private set; }
    /// <summary>
    /// NUmero de movimiento
    /// </summary>
    public string NumeroMovimiento { get; private set; } = null!;
    /// <summary>
    /// Codigo tipo transaccion
    /// </summary>
    public string CodigoTipoTransaccion { get; private set; } = null!;
    /// <summary>
    /// Codigo subtipo transaccion
    /// </summary>
    public string CodigoSubtipoTransaccion { get; private set; } = null!;
    /// <summary>
    /// Codigo sistema
    /// </summary>
    public string CodigoSistema { get; private set; } = null!;
    /// <summary>
    /// Codigo canal
    /// </summary>
    public string CodigoCanal { get; private set; } = null!;
    /// <summary>
    /// Codigo subcanal
    /// </summary>
    public string CodigoSubCanal { get; private set; } = null!;
    /// <summary>
    /// Fecha registro
    /// </summary>
    public DateTime FechaRegistro { get; private set; }
    /// <summary>
    /// Indicador Estado
    /// </summary>
    public string IndicadorEstado { get; private set; } = null!;
    /// <summary>
    /// Fecha ultima actualizacion
    /// </summary>
    public DateTime? FechaUltimaActualizacion { get; private set; }

    public static OperacionNotificada Crear(
        string numeroMovimiento,
        string codigoTipoTransaccion,
        string codigoSubtipoTransaccion,
        string codigoSistema,
        string codigoCanal,
        string codigoSubcanal,
        DateTime fechaRegistro)
    {
        return new OperacionNotificada()
        {
            NumeroMovimiento = numeroMovimiento,
            CodigoTipoTransaccion = codigoTipoTransaccion,
            CodigoSubtipoTransaccion = codigoSubtipoTransaccion,
            CodigoSistema = codigoSistema,
            CodigoCanal = codigoCanal,
            CodigoSubCanal = codigoSubcanal,
            FechaRegistro = fechaRegistro,
            IndicadorEstado = EstadoEntidad.ACTIVO
        };
    }
}
