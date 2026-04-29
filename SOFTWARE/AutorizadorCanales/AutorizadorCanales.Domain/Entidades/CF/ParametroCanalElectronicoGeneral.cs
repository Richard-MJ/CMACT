namespace AutorizadorCanales.Domain.Entidades.CF;

/// <summary>
/// Clase que representa la entidad de parametros de canales electronicos
/// </summary>
public class ParametroCanalElectronicoGeneral
{
    /// <summary>
    /// Id del parametro del canal electronico
    /// </summary>
    public int IdParametroCanalElectronico { get; private set; }
    /// <summary>
    /// Descripcion del parametro
    /// </summary>
    public string? DescripcionParametro { get; private set; } = null!;
    /// <summary>
    /// Valor del parametros
    /// </summary>
    public string? ValorParametro { get; private set; } = null!;
    /// <summary>
    /// Indicador del canal
    /// </summary>
    public string? IndicadorCanal { get; private set; } = null!;
    /// <summary>
    /// Numero del sub canal
    /// </summary>
    public byte? NumeroSubcanal { get; private set; }
    /// <summary>
    /// Id del parametro del canal electronico general equivalente
    /// </summary>
    public int IdParametroCanalElectronicoGeneralEquivalente { get; private set; }
    /// <summary>
    /// Indicador de estado
    /// </summary>
    public bool IndicadorEstado { get; private set; }
    /// <summary>
    /// Codigo de usuario de registro
    /// </summary>
    public string? CodigoUsuarioRegistro { get; private set; } = null!;
    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime FechaRegistro { get; private set; }
}

/// <summary>
/// Modelo de parametros de canales electronicos
/// </summary>
public enum ModeloParametroCanalElectronicoGeneral
{
    INDICADOR_VIGENCIA_CLAVE = 1,
    NUMERO_DIAS_VENCIMIENTO = 2,
    HABILITAR_LIMITE_OPERACIONES = 3,
    HABILITAR_VALIDACION_SMS_OPERACIONES = 12,
}
