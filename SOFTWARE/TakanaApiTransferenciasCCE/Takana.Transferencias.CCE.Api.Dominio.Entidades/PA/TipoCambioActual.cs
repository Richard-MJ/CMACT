namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.PA;

/// <summary>
/// Clase que representa a la entidad de dominio de un tipo de cambio
/// </summary>
public class TipoCambioActual : Empresa, ITasaCambio
{

    #region Constantes
    /// <summary>
    /// Valor para tipo de cambio Neutro
    /// </summary>
    public const int ValorNeutro = 1;
    /// <summary>
    /// Moneda Base
    /// </summary>
    public const string MonedaBase = "MONEDA_BASE";
    /// <summary>
    /// Codigo Contabilidad
    /// </summary>
    public const string Contabilidad = "CONTA";
    /// <summary>
    /// Codigo Lavado
    /// </summary>
    public const string Lavado = "LAVAD";
    /// <summary>
    /// Clase de la tasa de cambio - Venta
    /// </summary>
    public const string CLASE_TASA_CAMBIO_VENTA = "V";
    /// <summary>
    /// Tipo de cambio - asiento
    /// </summary>
    public const string TipoCambio = "C";
    /// <summary>
    /// Tipo de cambio fijo - asiento
    /// </summary>
    public const string TipoCambioFijo = "F";
    /// <summary>
    /// Tipo de cambio ITF - asiento
    /// </summary>
    public const string TipoCambioITF = "I";
    /// <summary>
    /// Tipo de cambio histórico - asiento
    /// </summary>
    public const string TipoCambioHistorico = "H";
    /// <summary>
    /// Código parámetro - tipo de cambio fijo
    /// </summary>
    public const string CodigoTipoCambioFijo = "TIP_CAM_FIJO";
    /// <summary>
    /// Código parámetro - tipo de cambio para caja
    /// </summary>
    public const string CodigoTipoCambioCaja = "TIPO_CAMBIO";
    /// <summary>
    /// Código parámetro - tipo de cambio para ITF
    /// </summary>
    public const string CodigoTipoCambioITF = "TIP_CAM_ITF";
    /// <summary>
    /// Código parámetro - tipo de cambio default
    /// </summary>
    public const string CodigoTipoCambioDefault = "CONTA";
    #endregion Constantes

    /// <summary>
    /// Codigo de la moneda
    /// </summary>
    public string CodigoMoneda { get; private set; }
    /// <summary>
    /// Codigo de tipo de cambio
    /// </summary>
    public string CodigoTipoCambio { get; private set; }
    /// <summary>
    /// Fecha de cambio
    /// </summary>
    public DateTime FechaTipoCambio { get; private set; }
    /// <summary>
    /// Valor de venta
    /// </summary>
    public decimal ValorVenta { get; private set; }
    /// <summary>
    /// Valor de compra
    /// </summary>
    public decimal ValorCompra { get; private set; }

    /// <summary>
    /// Método que crea el tiempo de cambio actual
    /// </summary>
    /// <param name="valorVenta"></param>
    /// <param name="valorCompra"></param>
    /// <returns></returns>
    public static TipoCambioActual Crear(decimal valorVenta, decimal valorCompra)
    {
        return new TipoCambioActual
        {
            ValorVenta = valorVenta,
            ValorCompra = valorCompra
        };
    }

}