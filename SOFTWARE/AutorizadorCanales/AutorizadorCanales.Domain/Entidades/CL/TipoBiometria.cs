namespace AutorizadorCanales.Domain.Entidades.CL;

public class TipoBiometria
{
    /// <summary>
    /// Identificador del tipo de biometria
    /// </summary>
    public int NumeroTipoBiometria { get; private set; }
    /// <summary>
    /// Descripcion del tipo de biometria
    /// </summary>
    public string DescripcionAfiliacion { get; private set; } = null!;
    /// <summary>
    /// Indicador de estado de la afiliación
    /// </summary>
    public string IndicadorEstado { get; private set; } = null!;
    /// <summary>
    /// Fecha en la que se registro la afiliación
    /// </summary>
    public DateTime FechaRegistro { get; private set; }
    /// <summary>
    /// Codigo del usuario que agrego la afiliación
    /// </summary>
    public string CodigoUsuario { get; private set; } = null!;
}
