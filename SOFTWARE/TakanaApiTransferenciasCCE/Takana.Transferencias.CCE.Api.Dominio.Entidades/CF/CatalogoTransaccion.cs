namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

/// <summary>
/// Clase de dominio que reprsenta un tipo de transacción
/// </summary>
public class CatalogoTransaccion
{
    #region Propiedades
    /// <summary>
    /// Propiedad que representa el Código del sistema
    /// </summary>
    public string CodigoSistema { get; private set; }
    /// <summary>
    /// Propiedad que representa el Código de tipo de transacción
    /// </summary>
    public string TipoTransaccion { get; private set; }
    /// <summary>
    /// Propiedad que representa la descripción de tipo de transacción
    /// </summary>
    public string DescripcionTransaccion { get; private set; }
    /// <summary>
    /// Propiedad que representa el indicador del tipo de operación
    /// </summary>
    public string IndicadorMovimiento { get; private set; }
    /// <summary>
    /// Propiedad que representa el Código de operación de lavado
    /// </summary>
    public string? CodigoLavado { get; private set; }
    /// <summary>
    /// Propiedad que representa una indicación de Retiro Deposito CTS
    /// </summary>
    public string IndicadorRetiroDepositoCTS { get; private set; }
    /// <summary>
    /// Propiedad que representa una identificación de asiento sunat
    /// </summary>
    public string CodigoIdentificacionAsientoSunat { get; private set; }

    #endregion
}

