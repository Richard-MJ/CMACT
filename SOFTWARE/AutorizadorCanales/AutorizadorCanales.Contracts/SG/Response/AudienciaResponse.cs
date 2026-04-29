namespace AutorizadorCanales.Contracts.SG.Response;

public record AudienciaResponse(
    string IdAudiencia,
    string IdSecreto,
    string IndicadorCanal,
    string DireccionOrigenPermitido,
    bool AplicaInactividad
);