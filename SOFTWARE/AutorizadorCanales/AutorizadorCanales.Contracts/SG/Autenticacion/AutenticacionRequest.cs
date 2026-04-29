namespace AutorizadorCanales.Contracts.SG.Autenticacion;

public record AutenticacionRequest(
    string grant_type,
    string? username,
    string? password,
    string? passwordPrimario,
    string client_id,
    string? terminal,
    string? idTipoDocumento,
    string? numeroDocumento,
    int idTipoOperacionCanalElectronico,
    string? guids,
    string? modeloDispositivo,
    string? userAgent,
    string? ipRequest,
    string? refresh_token,
    string? usuario);