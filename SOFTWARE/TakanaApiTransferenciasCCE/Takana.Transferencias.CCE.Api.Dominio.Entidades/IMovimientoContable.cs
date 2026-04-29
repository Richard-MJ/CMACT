using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades;
/// <summary>
/// Interfaz que representa una operación que participa en la generación de un asiento contable
/// </summary>
public interface IMovimientoContable
{
    /// <summary>
    /// Representa el número de asiento contable de la operación
    /// </summary>
    int NumeroAsientoContable { get; }

    /// <summary>
    /// Representa el código del sistema de la operación
    /// </summary>
    string CodigoSistema { get; }

    /// <summary>
    /// Representa el código del tipo de transacción de la operación
    /// </summary>
    string CodigoTipoTransaccion { get; }

    /// <summary>
    /// Representa el código del sub tipo de transacción de la operación
    /// </summary>
    string CodigoSubTipoTransaccion { get; }

    /// <summary>
    /// Representa la descripción del asiento contable
    /// </summary>
    string DescripcionAsientoMovimientoContable { get; }

    /// <summary>
    /// Representa el código del usuario del movimiento contable
    /// </summary>
    string CodigoUsuario { get; }

    /// <summary>
    /// Representa el código del usuario del movimiento contable
    /// </summary>
    string CodigoAgencia { get; }

    /// <summary>
    /// Representa la fecha de movimiento contable
    /// </summary>
    DateTime FechaMovimiento { get; }

    /// <summary>
    /// Indica si el movimiento es el que se utilizara como principal en reprsentación del asiento
    /// </summary>
    bool EsPrincipal { get; }

    /// <summary>
    /// Representa el codigo de cuenta contable
    /// </summary>
    string CuentaContable { get; }

    /// <summary>
    /// Representa el tipo de cuenta contable
    /// </summary>
    string TipoCuentaContable { get; }

    /// <summary>
    /// Representa el monto del movimiento contable
    /// </summary>
    decimal MontoMovimientoContable { get; }

    /// <summary>
    /// Representa la referencia del movimiento contable
    /// </summary>
    string ReferenciaMovimientoContable { get; }

    /// <summary>
    /// Propiedad que devuelve el código de Unidad Ejecutora
    /// </summary>
    string CodigoUnidadEjecutora { get; }

    /// <summary>
    /// Propiedad que devuelve el tipo de SubTransacción
    /// </summary>
    SubTipoTransaccion SubTipoTransaccionMovimiento { get; }

    /// <summary>
    /// Propiedad que devuelve el còdigo de cuenta puente a utilizar
    /// </summary>
    int CodigoCuentaPuente { get; }

    /// <summary>
    /// Propiedad que devuelve la tasa de cambio local
    /// </summary>
    decimal TasaCambioLocal { get; }

    /// <summary>
    /// Propiedad que devuelve la tasa de cambio de cuenta
    /// </summary>
    decimal TasaCambioCuenta { get; }
    
    /// <summary>
    /// Propiedad que devuelve si la entidad genera asiento contable.
    /// </summary>
    bool AplicaAsiento { get; }

    /// <summary>
    /// Establece el asiento contable del movimiento
    /// </summary>
    /// <param name="aoAsiento">instancia de la clase Asiento Contable</param>
    void EstablecerAsiento(AsientoContable aoAsiento);

    /// <summary>
    /// Asigna la tasa de cambio local
    /// </summary>
    /// <param name="tasaCambio"></param>
    void AsignarTasaCambioLocal(decimal tasaCambio);

    /// <summary>
    /// Asigna la tasa de cambio de cuenta
    /// </summary>
    /// <param name="tasaCambio"></param>
    void AsignarTasaCambioCuenta(decimal tasaCambio);
}
