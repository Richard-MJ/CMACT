namespace AutorizadorCanales.Contracts.SG.Autenticacion;

public record VerificacionAutenticacionRequest
(
    string idVerificacion,
    string codigoAutorizacion,
    string idVisual,
    string newGuid
);
