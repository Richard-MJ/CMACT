namespace AutorizadorCanales.Domain.Entidades.CF;

public class ParametroCanalElectronico
{
    #region Propiedades
    /// <summary>
    /// Codigo de parametro
    /// </summary>
    public string CodigoParametro { get; private set; } = null!;
    /// <summary>
    /// Codigo de moneda
    /// </summary>
    public string CodigoMoneda { get; private set; } = null!;
    /// <summary>
    /// Codigo de canal
    /// </summary>
    public string CodigoCanal { get; private set; } = null!;
    /// <summary>
    /// Codigo de subcanal
    /// </summary>
    public byte CodigoSubCanal { get; private set; }
    /// <summary>
    /// Valor de parametro
    /// </summary>
    public decimal ValorParametro { get; private set; }
    #endregion

    #region Constantes
    public const string TIEMPO_INACTIVIDAD = "TIEMPO_INACTIV";
    public const string TIEMPO_BLOQUEO = "TIEMPO_BLOQUEO";
    public const string TIEMPO_INGRESO_CLAVE = "TIEMPO_INGR_CLA";
    public const string NUMERO_INTENTOS_CLAVE = "NRO_INTENT_CLA";
    public const string PARAMETRO_TOKEN_REFRESCO = "TIEMPO_TOK_REF";
    #endregion
}
