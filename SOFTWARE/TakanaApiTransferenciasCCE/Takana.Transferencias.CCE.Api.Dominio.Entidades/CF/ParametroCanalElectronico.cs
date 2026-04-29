namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

public class ParametroCanalElectronico
{
    /// <summary>
    /// Constante de numero maximo de operaciones frecuentes
    /// </summary>
    public const string NumeroMaximoOperacionesFrecuentes = "NRO_MAX_FREC";
    /// <summary>
    /// Constante monto maximo por transferencia
    /// </summary>
    public const string MontoMaximoPorTransferencia = "MON_MAX_TRANS";
    /// <summary>
    /// Constante monto minimo por transferencia
    /// </summary>
    public const string MontoMinimoPorTransferencia = "MON_MIN_TRANS";
    /// <summary>
    /// Codigo de parametro
    /// </summary>
    public string CodigoParametro { get; private set; }
    /// <summary>
    /// Codigo de moneda
    /// </summary>
    public string CodigoMoneda { get; private set; }
    /// <summary>
    /// Codigo de canal
    /// </summary>
    public string CodigoCanal { get; private set; }
    /// <summary>
    /// Valor de parametro
    /// </summary>
    public decimal ValorParametro { get; private set; }
}
