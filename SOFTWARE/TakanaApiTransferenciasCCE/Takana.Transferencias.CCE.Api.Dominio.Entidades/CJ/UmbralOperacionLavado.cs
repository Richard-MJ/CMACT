namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CJ;
/// <summary>
/// Clase de dominio encargada del Umbral de operaciones del lavado
/// </summary>
public class UmbralOperacionLavado : Empresa
{
    #region Constante
    /// <summary>
    /// Codigo Tipo operacion Umbral
    /// </summary>
    public const int codigoTipoOperacionUmbral = 1;
    #endregion

    #region Propiedades
    /// <summary>
    /// Codigo de agencia
    /// </summary>
    public string CodigoAgencia { get; private set; }
    /// <summary>
    /// Codigo de tipo de operacion
    /// </summary>
    public int CodigoTipoOperacion { get; private set; }
    /// <summary>
    /// Monto limite
    /// </summary>
    public decimal MontoLimite { get; private set; }
    /// <summary>
    /// Codigo de estado activo
    /// </summary>
    public string EstaActivo { get; private set; }
    #endregion
}