namespace AutorizadorCanales.Infrastructure.Configuracion;

public class ConfiguracionAudiencia
{
    public static string NombreSeccion = "ConfiguracionAudiencia";
    public int MinutosVida { get; init; }
    public int MinutosVidaOB { get; init; }
    public List<Audiencia> AudienciasPermitidas { get; init; } = null!;
}

public class Audiencia
{
    public string SistemaCliente { get; set; } = null!;
    public string IdSistemaCliente { get; set; } = null!;
    public string IdSecreto { get; set; } = null!;
    public string OrigenPermitido { get; set; } = null!;
}
