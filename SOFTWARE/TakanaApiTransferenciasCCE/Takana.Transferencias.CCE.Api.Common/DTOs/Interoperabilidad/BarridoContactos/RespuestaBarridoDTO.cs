using static Takana.Transferencias.CCE.Api.Common.Interoperatibilidad.EstructuraJsonInteroperatibilidad;

namespace Takana.Transferencias.CCE.Api.Common.Interoperabilidad;
/// <summary>
/// Clase DTO de respuesta para barrido
/// </summary>
public record RespuestaBarridoDTO
{
    /// <summary>
    /// Codigo de respuesta del barrido
    /// </summary>
    public string CodigoRespuesta { get; set; }
    /// <summary>
    /// Razon del codigo de respuesta en caso de rechazo
    /// </summary>
    public string? RazonRespuesta { get; set; }
    /// <summary>
    /// Numero de seguimiento
    /// </summary>
    public string? NumeroSeguimiento { get; set; }
    /// <summary>
    /// Directorios resultantes
    /// </summary>
    public Directories[]? Directorios { get; set; }
    /// <summary>
    /// Directorios pertenecientes
    /// </summary>
    public List<Proxy> DirecytoriosPertenecientes { get; set; }
}
