namespace Takana.Transferencias.CCE.Api.Dominio.Entidades
{
    /// <summary>
    /// Interfaz de dominio de una transaccion
    /// </summary>
    public interface ITransaccion : IOperacionProducto, IMovimientoContable, IOperacionLavado
    {
    }
}
