namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
/// <summary>
/// Clase que representa a la entidad de dominio de una Producto exonerado
/// </summary>
public class ProductoExonerado : Empresa
{
    #region Constantes
    /// <summary>
    /// Estado alta
    /// </summary>
    public const string EstadoAlta = "A";
    /// <summary>
    /// Estado baja
    /// </summary>
    public const string EstadoBaja = "B";
    /// <summary>
    /// Tipo declaracion Exoneracion
    /// </summary>
    public const string TipoDeclaracionExoneracion = "1";
    #endregion

    #region Propiedades
    /// <summary>
    /// Codigo de agencia
    /// </summary>
    public string CodigoAgencia { get; private set; }
    /// <summary>
    /// Codigo de sistema
    /// </summary>
    public string CodigoSistema { get; private set; }
    /// <summary>
    /// Numero de producto
    /// </summary>
    public string NumeroProducto { get; private set; }
    /// <summary>
    /// Tipo de declaracion
    /// </summary>
    public string TipoDeclaracion { get; private set; }
    /// <summary>
    /// Codigo de operacion
    /// </summary>
    public string CodigoOperacion { get; private set; }
    /// <summary>
    /// Fecha de solicitud de alta
    /// </summary>
    public DateTime FechaSolicitudAlta { get; private set; }
    /// <summary>
    /// Contador de solicitudes
    /// </summary>
    public int ContadorSolicitudes { get; private set; }
    /// <summary>
    /// Indicador de estado
    /// </summary>
    public string IndicadorEstado { get; private set; }
    /// <summary>
    /// Indicador de remuneracion
    /// </summary>
    public string IndicadorRemuneracion { get; private set; }
    /// <summary>
    /// Numero de cuenta patrono
    /// </summary>
    public string NumeroCuentaPatrono { get; private set; }
    /// <summary>
    /// Codigo de agencia del usuario
    /// </summary>
    public string CodigoAgenciaUsuario { get; private set; }
    /// <summary>
    /// Codigo del usuario
    /// </summary>
    public string CodigoUsuario { get; private set; }
    /// <summary>
    /// Codigo de inciso
    /// </summary>
    public string CodigoInciso { get; private set; }
    /// <summary>
    /// Fecha de documento
    /// </summary>
    public DateTime FechaDocumento { get; private set; }
    /// <summary>
    /// Numero de documento
    /// </summary>
    public string NumeroDocumento { get; private set; }
    #endregion Propiedades
}