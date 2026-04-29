namespace AutorizadorCanales.Core.DTO;

/// <summary>
/// Dto de documento de cliente
/// </summary>
/// <param name="CodigoTipoDocumento">Código tipo de documento</param>
/// <param name="NumeroDocumento">Número de documento</param>
public record DocumentoClienteDto
(
    string CodigoTipoDocumento,
    string NumeroDocumento
);
