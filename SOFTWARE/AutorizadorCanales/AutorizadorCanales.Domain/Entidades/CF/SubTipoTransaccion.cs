namespace AutorizadorCanales.Domain.Entidades.CF;

public class SubTipoTransaccion : EntidadEmpresa
{
    #region Propiedades
    /// <summary>
    /// Propiedad que representa el código del sistema
    /// </summary>
    public string CodigoSistema { get; private set; } = null!;
    /// <summary>
    /// Propiedad que representa el Código de tipo de transacción
    /// </summary>
    public string CodigoTipoTransaccion { get; private set; } = null!;
    /// <summary>
    /// Propiedad que representa el Código de tipo de sub transacción
    /// </summary>
    public string CodigoSubTipoTransaccion { get; private set; } = null!;
    /// <summary>
    /// Propiedad que representa el detalle de la sub transacción
    /// </summary>
    public string DescripcionSubTransaccion { get; private set; } = null!;
    #endregion

    #region Constantes
    public const string TJ_TIP_AFILIACION_CANALES_ELECTRONICOS = "1";
    public const string TJ_SUB_TIP_INICIO_SESION = "4";
    public const string TJ_SUB_TIP_INICIO_SESION_FALLIDO = "5";
    #endregion
}
