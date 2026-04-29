using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades
{
    /// <summary>
    /// Interfaz de dominio de un movimiento de menor cuantia
    /// </summary>
    public interface IMovimientoCuantia
    {

        /// <summary>
        /// Codigo de tipo de transaccion
        /// </summary>
        string CodigoTipoTransaccion { get; }
        /// <summary>
        /// Codigo de agencia de movimiento
        /// </summary>
        string CodigoAgenciaMovimiento { get; }
        /// <summary>
        /// Fecha de movimiento
        /// </summary>
        DateTime FechaMovimiento { get; }
        /// <summary>
        /// Codigo de usuario
        /// </summary>
        string CodigoUsuario { get; }
        /// <summary>
        /// Codigo de usuario
        /// </summary>
        string CodigoSistema { get; }
        /// <summary>
        /// Numero de movimiento
        /// </summary>
        decimal NumeroMovimiento { get; }
        /// <summary>
        /// Monto de movimiento
        /// </summary>
        decimal MontoMovimiento { get; }
        /// <summary>
        /// Codigo de tipo de transaccion
        /// </summary>
        string CodigoSubTipoTransaccion { get; }
        /// <summary>
        /// Numero de cuenta
        /// </summary>
        string NumeroCuenta { get; }
        /// <summary>
        /// Numero de asiento
        /// </summary>
        decimal NumeroAsiento { get; }
        /// <summary>
        /// Entidad Sub tipo de transaccion del movimiento
        /// </summary>
        SubTipoTransaccion SubTipoTransaccionMovimiento { get; }
    }
}
