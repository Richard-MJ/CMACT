namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
public class ParametroPorEmpresa : Empresa
{
    #region Constantes

    /// <summary>
    /// Codigo serie no cliente
    /// </summary>
    public const string CodigoSerieNoCliente = "NO_CLIENTE_1";
    /// <summary>
    /// Indicador cuenta sueldo
    /// </summary>
    public const string IndicadorCuentaSueldo = "IND_CUE_SUELDO";
    /// <summary>
    /// Codigo de cuenta contable entrada soles
    /// </summary>
    public const string CodigoCuentaContableEntradaSoles = "CTA_TIN_SOL_ENT";
    /// <summary>
    /// Codigo de cuenta contable entrada dolares
    /// </summary>
    public const string CodigoCuentaContableEntradaDolares = "CTA_TIN_DOL_ENT";
    /// <summary>
    /// Codigo de cuenta contable salida soles
    /// </summary>
    public const string CodigoCuentaContableSalidaSoles = "CTA_TIN_SOL_SAL";
    /// <summary>
    /// Codigo de cuenta contable dolares
    /// </summary>
    public const string CodigoCuentaContableSalidaDolares = "CTA_TIN_DOL_SAL";
    /// <summary>
    /// Codigo de cuenta contable de comision saleda comision
    /// </summary>
    public const string CodigoCuentaContableComisionSoles = "CTA_TIN_SOL_COM";
    /// <summary>
    /// Codigo de cuenta contable de comision en dolares
    /// </summary>
    public const string CodigoCuentaContableComisionDolares = "CTA_TIN_DOL_COM";
    /// <summary>
    /// Email de canales
    /// </summary>
    public const string EmailCanales = "EMAIL_CANALES";
    #endregion

    #region Propiedades
    /// <summary>
    /// Codigo de sistema
    /// </summary>
    public string CodigoSistema { get; private set; }
    /// <summary>
    /// Codigo de parametro
    /// </summary>
    public string CodigoParametro { get; private set; }
    /// <summary>
    /// Valor de parametro
    /// </summary>
    public string ValorParametro { get; private set; }
    /// <summary>
    /// Descripcoin de parametro
    /// </summary>
    public string DescripcionParametro { get; private set; }
    #endregion
}
