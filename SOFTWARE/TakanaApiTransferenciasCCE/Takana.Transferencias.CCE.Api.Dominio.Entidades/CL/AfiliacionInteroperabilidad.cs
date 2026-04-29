namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

/// <summary>
/// Clase de dominio encargada del afilicoin a interoperabilidad cabecera
/// </summary>
public class AfiliacionInteroperabilidad : Empresa
{
    #region Propiedades
    /// <summary>
    /// Codigo de servicio
    /// </summary>
    public string CodigoServicio { get; private set; }
    /// <summary>
    /// Codigo de cliente
    /// </summary>
    public string CodigoCliente { get; private set; }
    /// <summary>
    /// Numero de cuenta
    /// </summary>
    public string NumeroCuenta { get; private set; }
    /// <summary>
    /// Codigo de cuenta interbancario
    /// </summary>
    public string CodigoCuentaInterbancario { get; private set; }
    /// <summary>
    /// Codigo de usuario registro
    /// </summary>
    public string CodigoUsuarioRegistro { get; private set; }
    /// <summary>
    /// Codigo de usuario modifico
    /// </summary>
    public string CodigoUsuarioModifico { get; private set; }
    /// <summary>
    /// Fecha de modifico
    /// </summary>
    public DateTime? FechaModifico { get; private set; }
    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime FechaRegistro { get; private set; }
    /// <summary>
    /// Entidad de detalles de afiliación
    /// </summary>
    public virtual List<AfiliacionInteroperabilidadDetalle> Detalles { get; private set; }
    #endregion

    #region Métodos
    /// <summary>
    /// Método que crea un afiliacio nde interoperabiidad
    /// </summary>
    /// <param name="codigoServicio"></param>
    /// <param name="codigoCliente"></param>
    /// <param name="numeroCuenta"></param>
    /// <param name="codigoCII"></param>
    /// <param name="usuaroRegistro"></param>
    /// <param name="fechaRegistro"></param>
    /// <returns>Retorna un afiliacion interoperabilidad</returns>
    public static AfiliacionInteroperabilidad Crear(
        string codigoServicio,
        string codigoCliente,
        string numeroCuenta,
        string codigoCII,
        string usuaroRegistro,
        DateTime fechaRegistro)
    {
        return new AfiliacionInteroperabilidad
        {
            CodigoServicio = codigoServicio,
            CodigoCliente = codigoCliente,
            NumeroCuenta = numeroCuenta,
            CodigoCuentaInterbancario = codigoCII,
            CodigoUsuarioRegistro = usuaroRegistro,
            CodigoUsuarioModifico = usuaroRegistro,
            FechaRegistro = fechaRegistro,
            FechaModifico = fechaRegistro,
            CodigoEmpresa = Empresa.CodigoPrincipal
        };
    }
    #endregion
}
