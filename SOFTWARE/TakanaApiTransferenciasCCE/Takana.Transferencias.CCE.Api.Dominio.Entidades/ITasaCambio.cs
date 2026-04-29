namespace Takana.Transferencias.CCE.Api.Dominio.Entidades
{
    /// <summary>
    /// Interfaz de dominio de tasa de cambio
    /// </summary>
    public interface ITasaCambio
    {
        /// <summary>
        /// Valor de la venta
        /// </summary>
        decimal ValorVenta { get; }
        /// <summary>
        /// Valor de  la compra
        /// </summary>
        decimal ValorCompra { get; }
    }
}
