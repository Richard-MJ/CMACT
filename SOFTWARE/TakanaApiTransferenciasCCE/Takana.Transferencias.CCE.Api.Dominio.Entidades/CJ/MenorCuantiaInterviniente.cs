using Takana.Transferencias.CCE.Api.Common.DTOs.CL;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CJ;
/// <summary>
/// Clase de dominio que representa a los intervinientes en una operación de menor cuantia
/// </summary>
public class MenorCuantiaInterviniente : ILavadoInterviniente
{
    #region Constantes
    /// <summary>
    /// Menor cuantia activa
    /// </summary>
    public const string MenorCuantiaActiva = "A";
    /// <summary>
    /// Menor cuantia Pendiente
    /// </summary>
    public const string MenorCuantiaPendiente = "P";
    /// <summary>
    /// Tipo de interviniente externo
    /// </summary>
    public const string TipoIntervinienteExterno = "EX";
    #endregion
    #region Propiedades
    /// <summary>
    /// Representa el Identificador del Interviniente
    /// </summary>
    public decimal IdInterviniente { get; private set; }
    /// <summary>
    /// Rpresenta el número de la operación
    /// </summary>
    public int NumeroOperacion { get; private set; }
    /// <summary>
    /// Representa el tipo de Interviniente
    /// </summary>
    public int TipoInterviniente { get; private set; }
    /// <summary>
    /// Representa el Tipo de Documento del interviniente
    /// </summary>
    public string TipoDocumento { get; private set; }
    /// <summary>
    /// Representa el Numero de Documento del interviniente
    /// </summary>
    public string NumeroDocumento { get; private set; }
    /// <summary>
    /// Representa si es un cliente de la institución o no
    /// </summary>
    public string TipoCliente { get; private set; }
    /// <summary>
    /// Representa el Codigo del cliente del interviniente
    /// </summary>
    public string CodigoCliente { get; private set; }
    /// <summary>
    /// Representa el Apellido Paterno del interviniente
    /// </summary>
    public string ApellidoPaterno { get; private set; }
    /// <summary>
    /// Representa el Apellido Materno del interviniente
    /// </summary>
    public string ApellidoMaterno { get; private set; }
    /// <summary>
    /// Representa los Nombres del interviniente
    /// </summary>
    public string Nombres { get; private set; }
    /// <summary>
    /// Representa el estado del registro si se cuentra activo o no
    /// </summary>
    public string EstadoRegistro { get; private set; }
    /// <summary>
    /// Repreenta el encabezado de la operación del interviniente
    /// </summary>
    public virtual MenorCuantiaEncabezado Encabezado { get; private set; }
    /// <summary>
    /// Numero de movimiento lavado
    /// </summary>
    public decimal? NumeroMovimientoLavado => NumeroOperacion;
    #endregion Propiedades

    #region Metodos
    /// <summary>
    /// Método que crea un interviniente de menor cuantia
    /// </summary>
    /// <param name="numeroOperacionLavado"></param>
    /// <param name="tipoInterviniente"></param>
    /// <param name="codigoTipoInterviniente"></param>
    /// <returns>Retorna datos de menor cuantia interviniente</returns>
    /// <exception cref="ValidacionException">Excepcion que retorna que no tiene una interviniente</exception>
    public static MenorCuantiaInterviniente Crear(
        int numeroOperacionLavado,
        IInterviniente tipoInterviniente,
        int codigoTipoInterviniente
        )
    {
        if (tipoInterviniente == null)
        {
            throw new ValidacionException("No se puede registrar un interviniente de menor cuantia");
        }
        return new MenorCuantiaInterviniente()
        {
            NumeroOperacion = numeroOperacionLavado,
            TipoInterviniente = codigoTipoInterviniente,
            TipoDocumento = tipoInterviniente?.CodigoTipoDocumento,
            NumeroDocumento = tipoInterviniente.NumeroDocumento,
            TipoCliente = tipoInterviniente.EsCliente ? Sistema.Clientes : TipoIntervinienteExterno,
            CodigoCliente = tipoInterviniente.CodigoCliente,
            ApellidoPaterno = tipoInterviniente.ApellidoPaterno,
            ApellidoMaterno = tipoInterviniente.ApellidoMaterno,
            Nombres = tipoInterviniente.Nombres,
            EstadoRegistro = MenorCuantiaActiva
        };
    }
    /// <summary>
    /// Registrar un cliente externo
    /// </summary>
    /// <param name="encabezado"></param>
    /// <param name="tipoInteviniente"></param>
    /// <param name="Cliente"></param>
    /// <returns></returns>
    public static MenorCuantiaInterviniente RegistrarDesdeClienteExterno(
    MenorCuantiaEncabezado encabezado, TipoInteviniente tipoInteviniente, ClienteExternoDTO Cliente)
    {
        return new MenorCuantiaInterviniente()
        {
            Encabezado = encabezado,
            NumeroOperacion = encabezado.NumeroLavado,
            TipoInterviniente = (int)tipoInteviniente,
            TipoDocumento = Cliente.CodigoTipoDocumento,
            NumeroDocumento = Cliente.NumeroDocumento,
            TipoCliente = Cliente.TipoCliente,
            CodigoCliente = Cliente.CodigoCliente,
            ApellidoPaterno = Cliente.EsClienteExterno
                ? Cliente.Nombres
                : Cliente.ApellidoPaterno ?? string.Empty,

            ApellidoMaterno = Cliente.EsPersonaJuridica
                ? string.Empty
                : Cliente.ApellidoMaterno ?? string.Empty,

            Nombres = Cliente.EsPersonaJuridica || Cliente.EsClienteExterno
                ? string.Empty
                : Cliente.Nombres,
            EstadoRegistro = MenorCuantiaActiva
        };
    }
    /// <summary>
    /// Registra un interviniente de menor cuantía desde un cliente externo como beneficiario.
    /// </summary>
    /// <param name="encabezado">El encabezado del registro de menor cuantía.</param>
    /// <param name="tipoInteviniente">El tipo de interviniente a registrar.</param>
    /// <param name="Cliente">El cliente externo que actúa como beneficiario.</param>
    /// <returns>El interviniente registrado.</returns>
    public static MenorCuantiaInterviniente RegistrarDesdeClienteExternoBeneficiario(
    MenorCuantiaEncabezado encabezado, TipoInteviniente tipoInteviniente, ClienteExternoDTO Cliente)
    {
        return new MenorCuantiaInterviniente()
        {
            Encabezado = encabezado,
            NumeroOperacion = encabezado.NumeroLavado,
            TipoInterviniente = (int)tipoInteviniente,
            TipoDocumento = Cliente.CodigoTipoDocumento,
            NumeroDocumento = Cliente.NumeroDocumento,
            TipoCliente = Cliente.TipoCliente,
            ApellidoPaterno = Cliente.ApellidoPaterno,
            ApellidoMaterno = Cliente.ApellidoMaterno,
            Nombres = Cliente.Nombres,
            EstadoRegistro = MenorCuantiaPendiente
        };
    }
    #endregion Metodos
}

/// <summary>
/// Tipos de Intervinientes
/// </summary>
public enum TipoInteviniente
{
    Solicitante = 1, Ordenante = 2, Beneficiario = 3
}