namespace Takana.Transferencias.CCE.Api.Common.Interoperabilidad;
/// <summary>
/// Clase de respuesta de registro de directorios DTO
/// </summary>
public record RespuestaRegistroDirectorioDTO
{
    /// <summary>
    /// Respuesta del registro de directorio
    /// </summary>
    public string Respuesta {get; set;}
    /// <summary>
    /// Razon respuesta
    /// </summary>
    public string? RazonRespuesta {get; set;}
    /// <summary>
    /// Numero de seguimiento
    /// </summary>
    public string? NumeroSeguimiento {get; set;}
    /// <summary>
    /// Indicaro de tramo1
    /// </summary>
    public string IndicadorTramo1 {get; set;}
    /// <summary>
    /// Indicador de tramo 2
    /// </summary>
    public string IndicadorTramo2 {get; set;}
}
