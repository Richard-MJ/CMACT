using Takana.Transferencias.CCE.Api.Common.Constantes;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
public class CuentaContable : Empresa
{
    /// <summary>
    /// Numero de cuenta contable
    /// </summary>
    public string NumeroCuentaContable { get; private set; }
    /// <summary>
    /// Codigo de moneda
    /// </summary>
    public string CodigoMoneda { get; private set; }
    /// <summary>
    /// Codigo de tipo de cambio
    /// </summary>
    public string? CodigoTipoCambio { get; private set; }
    /// <summary>
    /// Codigo de clase de tipo de cambio
    /// </summary>
    public string CodigoClaseTipoCambio { get; private set; }
    /// <summary>
    /// Valor de tasa de cambio
    /// </summary>
    public decimal? ValorTasaCambio { get; private set; }
    /// <summary>
    /// Indicador de tipo de cambio
    /// </summary>
    public string IndicadorTipoCambio { get; private set; }
    /// <summary>
    /// Indicador de estado
    /// </summary>
    public string IndicadorEstado { get; private set; }
    /// <summary>
    /// Codigo de grupo
    /// </summary>
    public string? CodigoGrupo { get; private set; }
    /// <summary>
    /// Descripcion de cuenta
    /// </summary>
    public string? DescripcionCuenta { get; private set; }
    /// <summary>
    /// Categoria de cuenta
    /// </summary>
    public string? CategoriaCuenta { get; private set; }
    /// <summary>
    /// Indicador de acceso
    /// </summary>
    public string? IndicadorAcceso { get; private set; }
    /// <summary>
    /// Identificador de cuenta
    /// </summary>
    public decimal? IdCuenta { get; private set; }
    /// <summary>
    /// Codigo de tipo de cuenta
    /// </summary>
    public string CodigoTipoCuenta { get; private set; }

    #region Métodos
    /// <summary>
    /// Valida que la cuenta contable cumpla con las reglas de negocio
    /// necesarias para su uso en operaciones contables.
    /// </summary>
    public void Validar()
    {
        var estadoActivo = General.Activo;

        if (IndicadorEstado != estadoActivo)
        {
            throw new Exception("Cuenta Contable está cerrada: " + NumeroCuentaContable);
        }

        if (CodigoTipoCuenta != estadoActivo)
        {
            throw new Exception("Cuenta Contable no es de tipo Auxiliar: " + NumeroCuentaContable);
        }
    }
    #endregion
}
