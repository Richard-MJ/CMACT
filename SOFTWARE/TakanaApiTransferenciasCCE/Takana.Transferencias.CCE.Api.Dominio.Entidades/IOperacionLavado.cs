using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CJ;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades
{
    /// <summary>
    /// Interfaz de dominio de una operacion de lavado
    /// </summary>
    public interface IOperacionLavado
    {
        /// <summary>
        /// Valida si es una operacion principal de lavado
        /// </summary>
        bool EsOperacionPrincipalLavado { get; }
        /// <summary>
        /// Indica si el registro de lavado es el origen del dinero.
        /// </summary>
        bool EsRegistroOrigen { get; }
        /// <summary>
        /// Codigo de agencia
        /// </summary>
        string CodigoAgencia { get; }
        /// <summary>
        /// Codigo de usuario
        /// </summary>
        string CodigoUsuario { get; }
        /// <summary>
        /// Fecha de operacion
        /// </summary>
        DateTime FechaOperacion { get; }
        /// <summary>
        /// Subtipo de transaccion de movimiento
        /// </summary>
        SubTipoTransaccion SubTipoTransaccionMovimiento { get; }
        /// <summary>
        /// Moneda de la operacion
        /// </summary>
        string MonedaOperacion { get; }
        /// <summary>
        /// Monto de la operacion
        /// </summary>
        decimal MontoOperacion { get; }
        /// <summary>
        /// Numero de la cuenta
        /// </summary>
        string NumeroCuenta { get; }
        /// <summary>
        /// Numero del movimiento
        /// </summary>
        decimal NumeroMovimiento { get; }
        /// <summary>
        /// Forma de pago del lavado
        /// </summary>
        string FormaDePagoLavado { get; }
        /// <summary>
        /// Numero de asiento del lavado
        /// </summary>
        int NumeroAsientoLavado { get; }
        /// <summary>
        /// Codigo de sistema
        /// </summary>
        string CodigoSistema { get; }
        /// <summary>
        /// Codigo de la entidad SBS
        /// </summary>
        string CodigoEntidadSbs { get; }
        /// <summary>
        /// Importante para el proceso de lavado de una operación contra CCE con valor E.
        /// </summary>
        string IndicadorMovimiento { get; }
        /// <summary>
        /// Codigo de sistema fuente
        /// </summary>
        string CodigoSistemaFuente { get; }
        /// <summary>
        /// Tipo de interviniente
        /// </summary>
        TipoInteviniente TipoInterviniente { get; }
        /// <summary>
        /// Datos del interviniente
        /// </summary>
        IInterviniente Interviniente { get; }
    }
}
