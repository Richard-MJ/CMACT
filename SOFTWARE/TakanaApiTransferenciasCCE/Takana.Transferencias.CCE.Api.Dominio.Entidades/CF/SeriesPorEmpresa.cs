namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
public class SeriesPorEmpresa
{
    /// <summary>
    /// Codigo de sistema
    /// </summary>
    public string CodigoSistema { get; private set; }
    /// <summary>
    /// Codigo de empresa
    /// </summary>
    public string CodigoEmpresa { get; private set; }
    /// <summary>
    /// Codigo de serie
    /// </summary>
    public string CodigoSerie { get; private set; }
    /// <summary>
    /// Descripcion de serie
    /// </summary>
    public string DescripcionSerie { get; private set; }
    /// <summary>
    /// Valor del siguiente
    /// </summary>
    public decimal ValorSiguiente{ get; private set; } 
}
