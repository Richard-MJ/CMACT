namespace AutorizadorCanales.Domain.Entidades.SG;

public class AccesosPorSistema
{
    /// <summary>
    /// Código de empresa
    /// </summary>
    public string CodigoEmpresa { get; private set; } = null!;
    /// <summary>
    /// Código de acceso
    /// </summary>
    public string CodigoAcceso { get; private set; } = null!;
    /// <summary>
    /// Código de sistema
    /// </summary>
    public string CodigoSistema { get; private set; } = null!;
}