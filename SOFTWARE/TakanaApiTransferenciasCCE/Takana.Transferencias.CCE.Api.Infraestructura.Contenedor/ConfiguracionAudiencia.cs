namespace Takana.Transferencias.CCE.Api.Infraestructura.Contenedor;

/// <summary>
/// Clase de configuracion de audiencias
/// </summary>
public class ConfiguracionAudiencia
{
    /// <summary>
    /// Nombre de la sección
    /// </summary>
    public static string NombreSeccion = "ConfiguracionAudiencia";
    /// <summary>
    /// Lista de audiencias
    /// </summary>
    public List<Audiencia> AudienciasPermitidas { get; init; }
}

/// <summary>
/// Audiencia
/// </summary>
public class Audiencia
{
    /// <summary>
    /// Sistema del cliente
    /// </summary>
    public string SistemaCliente { get; set; }
    /// <summary>
    /// Identificador de la Audiencia
    /// </summary>
    public string IdAudiencia { get; set; }
    /// <summary>
    /// Identificador de clave secreta
    /// </summary>
    public string IdSecreto { get; set; }
    /// <summary>
    /// Origen de audiencias Permitidas
    /// </summary>
    public string OrigenPermitido { get; set; }
}
