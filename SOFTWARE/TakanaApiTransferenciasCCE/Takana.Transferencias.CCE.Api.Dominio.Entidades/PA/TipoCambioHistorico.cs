namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.PA;

/// <summary>
/// Clase que representa a la entidad de dominio de un tipo de cambio historico
/// </summary>
public class TipoCambioHistorico : Empresa, ITasaCambio
{

    /// <summary>
    /// Fecha de tipo de cambio
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
    /// Codigo de moneda
    /// </summary>
    public string CodigoMoneda { get; private set; }
    /// <summary>
    /// Codigo de tipo de cambio
    /// </summary>
    public string CodigoTipoCambio { get; private set; }
}