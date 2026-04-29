using AutorizadorCanales.Domain.Entidades.CF;

namespace AutorizadorCanales.Domain.Entidades.SG;

/// <summary>
/// Entidad SG.SG_API_AUDIENCIA
/// </summary>
public class Audiencia
{
    /// <summary>
    /// Identificador de sistema
    /// </summary>
    public string Id { get; private set; } = null!;
    /// <summary>
    /// Indicador de estado
    /// </summary>
    public string IndicadorEstado { get; private set; } = null!;
    /// <summary>
    /// Id secreto
    /// </summary>
    public string IdSecreto { get; private set; } = null!;
    /// <summary>
    /// Nombre
    /// </summary>
    public string Nombre { get; private set; } = null!;
    /// <summary>
    /// Maximo de intentos fallidos
    /// </summary>
    public int MaximoIntentosFallidos { get; private set; }
    /// <summary>
    /// Rango de minutos de intentos fallidos
    /// </summary>
    public int MinutosRangoIntentosFallidos { get; private set; }
    /// <summary>
    /// Indicador de canal
    /// </summary>
    public string IndicadorCanal { get; private set; } = null!;
    /// <summary>
    /// Horas bloqueadas por intentos fallidos
    /// </summary>
    public int HorasBloquedasPorIntentosFallidos { get; private set; }
    /// <summary>
    /// Indicador de tipo de aplicación
    /// </summary>
    public TiposAplicacion IndicadorTipoApp { get; private set; }
    /// <summary>
    /// Indicador de tipo de app de tipo int
    /// </summary>
    public short IndicadorTipoAppInt { get { return (short)IndicadorTipoApp; } set { IndicadorTipoApp = (TiposAplicacion)value; } }
    /// <summary>
    /// Tiempo de vida de token refresco en minutos
    /// </summary>
    public int MinutosTiempoVidaTokenRefresco { get; private set; }
    /// <summary>
    /// Dirección de origenes permitido
    /// </summary>
    public string DireccionOrigenPermitido { get; private set; } = null!;
    /// <summary>
    /// Indicador que aplica inactividad
    /// </summary>
    public string IndicadorAplicaInactividad { get; private set; } = null!;
    /// <summary>
    /// Indicador de bloqueo de canal electrónico
    /// </summary>
    public string? IndicadorBloqueoCanalElectronico { get; private set; } = null!;
    /// <summary>
    /// Canal electrónico
    /// </summary>
    public virtual CanalElectronico CanalElectronico { get; private set; } = null!;
}
