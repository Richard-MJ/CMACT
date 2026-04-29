namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

/// <summary>
/// Clase de Dominio que representa a las Monedas
/// </summary>
public class Moneda
{
    #region Contantes
    /// <summary>
    /// Nombre moneda soles
    /// </summary>
    public const string Soles = "SOLES";
    /// <summary>
    /// Nombre moneda dolares
    /// </summary>
    public const string Dolares = "DÓLARES";
    #endregion

    #region Constantes Enum
    /// <summary>
    /// Codigos de moneda
    /// </summary>
    public enum MonedaCodigo
    {
        Soles = 1,
        Dolares = 2,
        SolesCCE = 604,
        DolaresCCE = 840,
    }
    #endregion Constantes

    #region Propiedades
    /// <summary>
    /// Codigo de moneda
    /// </summary>
    public string CodigoMoneda { get; private set; }
    /// <summary>
    /// Nombre de la moneda
    /// </summary>
    public string NombreMoneda { get; private set; }
    /// <summary>
    /// Abreviatura de moneda
    /// </summary>
    public string AbreviaturaMoneda { get; private set; }
    /// <summary>
    /// Codigo de sigla
    /// </summary>
    public string CodigoSigla { get; private set; }
    /// <summary>
    /// Moneda de ml
    /// </summary>
    public string Moneda_ML { get; private set; }
    /// <summary>
    /// Codigo de moneda ISO
    /// </summary>
    public string CodigoMonedaIso { get; private set; }
    /// <summary>
    /// Nombre simple
    /// </summary>
    public string NombreSimple { get; private set; }
    /// <summary>
    /// Codigo de retencion Sunat
    /// </summary>
    public string CodigoRetencionSunat { get; private set; }
    /// <summary>
    /// Codigo de moneda de pago efectivo
    /// </summary>
    public string CodigoMonedaPagoEfectivo { get; private set; }
    #endregion

}