namespace AutorizadorCanales.Core.DTO;

/// <summary>
/// Dto de calendario
/// </summary>
public class CalendarioDto
{
    /// <summary>
    /// Código de sistema
    /// </summary>
    public string CodigoSistema { get; set; } = null!;
    /// <summary>
    /// Código de agencia
    /// </summary>
    public string CodigoAgencia { get; set; } = null!;
    /// <summary>
    /// Fecha de sistema
    /// </summary>
    public DateTime FechaSistema { get; set; }
    /// <summary>
    /// Fecha y hora del sistema
    /// </summary>
    public DateTime FechaHoraSistema =>
        FechaSistema + DateTime.Now.TimeOfDay;
}
