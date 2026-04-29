using AutorizadorCanales.Domain.Entidades.SG;

namespace AutorizadorCanales.Domain.Entidades.CF;

/// <summary>
/// Clase que representa la entidad de los tipos de canales electronicos
/// </summary>
public class CanalElectronico
{
    #region Propiedades
    /// <summary>
    /// Codigo del canal electronico
    /// </summary>
    public string CodigoCanal { get; private set; } = null!;
    /// <summary>
    /// Descripcion del canal
    /// </summary>
    public string? DescripcionCanal { get; private set; } = null!;
    /// <summary>
    /// Descripcion del TTS
    /// </summary>
    public string DescripcionTTS { get; private set; } = null!;
    /// <summary>
    /// Indicador del canal de operacion permitido para las validaciones
    /// </summary>
    public bool IndicadorOperacionCanalPermitido { get; private set; }
    /// <summary>
    /// Instancia de la entidad SubCanalElectronico
    /// </summary>
    public virtual Audiencia? SistemaCliente { get; private set; }
    #endregion
}