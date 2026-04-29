namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

public class Sistema
{
    #region Constantes
    /// <summary>
    /// Esquema de bancos
    /// </summary>
    public const string Bancos = "BA";
    /// <summary>
    /// Esquema de Cuenta Efectivo
    /// </summary>
    public const string CuentaEfectivo = "CC";
    /// <summary>
    /// Esquema de Cajas
    /// </summary>
    public const string Cajas = "CJ";
    /// <summary>
    /// Esquema de Contabilidad
    /// </summary>
    public const string Contabilidad = "CG";
    /// <summary>
    /// Esquema de parametros
    /// </summary>
    public const string Parametros = "PA";
    /// <summary>
    /// Esquema de depositos
    /// </summary>
    public const string Deposito = "DP";
    /// <summary>
    /// Esquema de Creditos
    /// </summary>
    public const string Creditos = "PR";
    /// <summary>
    /// Esquema de Tarjetas
    /// </summary>
    public const string Tarjetas = "TJ";
    /// <summary>
    /// Esquema Configuracion
    /// </summary>
    public const string Configuracion = "CF";
    /// <summary>
    /// Esquema de clientes
    /// </summary>
    public const string Clientes = "CL";
    #endregion

    #region Propiedades
    /// <summary>
    /// Codigo de sistema
    /// </summary>
    public string CodigoSistema { get; private set; }
    /// <summary>
    /// Descripcion del sistem
    /// </summary>
    public string DescripcionSistema { get; private set; }
    /// <summary>
    /// Indicador de estado
    /// </summary>
    public string IndicadorEstado { get; private set; }

    #endregion Propiedades

}