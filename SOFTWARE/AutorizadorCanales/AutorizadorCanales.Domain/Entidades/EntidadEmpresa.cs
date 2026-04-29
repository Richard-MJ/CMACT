namespace AutorizadorCanales.Domain.Entidades;

public abstract class EntidadEmpresa
{
    public string CodigoEmpresa { get; set; } = null!;

    #region Constantes
    public const string EMPRESA = "1";
    #endregion
}
