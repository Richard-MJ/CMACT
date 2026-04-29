namespace AutorizadorCanales.Domain.Entidades.CF;

public class Calendario : EntidadEmpresa
{
    #region Propiedades
    /// <summary>
    /// Código de sistema
    /// </summary>
    public string CodigoSistema { get; private set; } = null!;
    /// <summary>
    /// Código de agencia
    /// </summary>
    public string CodigoAgencia { get; private set; } = null!;
    /// <summary>
    /// Fecha de sistema
    /// </summary>
    public DateTime? FechaSistema { get; private set; }
    #endregion

    #region Autegeneradas
    /// <summary>
    /// Fecha y hora del sistema
    /// </summary>
    public DateTime FechaHoraSistema =>
            (FechaSistema ?? DateTime.Now.Date) + DateTime.Now.TimeOfDay;
    #endregion
}
