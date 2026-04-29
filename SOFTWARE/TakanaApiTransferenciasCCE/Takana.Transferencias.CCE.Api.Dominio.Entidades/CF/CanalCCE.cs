namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

public class CanalCCE
{
    #region Constantes
    public enum CanalInmediataEnum
    {
        SubCanalTinDiferidas = 0,
        SubCanalTinInmediata = 1,
        SubCanalInteroperabilidad = 2,
    }
    #endregion

    #region Propiedades
    /// <summary>
    /// Identificador de canal
    /// </summary>
    public int IdCanal { get; private set; }
    /// <summary>
    /// Codigo del canal CCE
    /// </summary>
    public string CodigoCanalCCE { get; private set; }
    /// <summary>
    /// Indicador de estado
    /// </summary>
    public string IndicadorEstado { get; private set; }

    /// <summary>
    /// Descripcion de canal
    /// </summary>
    public string DescripcionCanal { get; private set; }
    /// <summary>
    /// Entidad de canal por SubTransaccionCCE
    /// </summary>
    public virtual ICollection<CanalPorSubTransaccionCCE> CanalesPorSubTransaciones { get; private set; }
    #endregion
}
