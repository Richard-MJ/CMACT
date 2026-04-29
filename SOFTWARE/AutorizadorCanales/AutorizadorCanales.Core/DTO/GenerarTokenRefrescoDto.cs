namespace AutorizadorCanales.Core.DTO;

/// <summary>
/// Dto para generar token de refresco
/// </summary>
/// <param name="IdAudiencia">Id de la audiencia</param>
/// <param name="IdClienteApi">Id del api cliente</param>
/// <param name="IdVisual">Id visual</param>
/// <param name="TokenAcceso">Token de acceso</param>
/// <param name="IndicadorCanal">Indicador de canal</param>
/// <param name="TipoAutenticacion">Tipo de autenticación</param>
public record GenerarTokenRefrescoDto
(
    string IdAudiencia,
    int IdClienteApi,
    string IdVisual,
    string TokenAcceso,
    string IndicadorCanal,
    byte TipoAutenticacion
);
