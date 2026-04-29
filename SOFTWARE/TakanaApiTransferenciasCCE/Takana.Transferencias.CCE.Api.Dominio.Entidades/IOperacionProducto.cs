using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades
{
    /// <summary>
    /// Interfaz de dominio de operacion de un producto
    /// </summary>
    public interface IOperacionProducto
    {
        #region Propiedades
        /// <summary>
        /// Codigo del sistema
        /// </summary>
        string CodigoSistema { get; }
        /// <summary>
        /// Numero de operacion
        /// </summary>
        int NumeroOperacion { get; }
        /// <summary>
        /// Indicador de origen
        /// </summary>
        bool EsOrigen { get; }
        /// <summary>
        /// Indicador de desttino
        /// </summary>
        bool EsDestino { get; }
        /// <summary>
        /// Datos de la subtransaccion de movimiento
        /// </summary>
        SubTipoTransaccion SubTipoTransaccionMovimiento { get; }
        /// <summary>
        /// Datos del producto
        /// </summary>
        IEntidadProducto Producto { get; }
        /// <summary>
        /// Fecha de movimiento
        /// </summary>
        DateTime FechaMovimiento { get; }
        #endregion Propiedades
        /// <summary>
        /// Metodo que establece la operacion origen
        /// </summary>
        /// <param name="operacionOrigen"></param>
        void EstablecerOperacionOrigen(IOperacionProducto operacionOrigen);
    }
}
