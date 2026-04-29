namespace AutorizadorCanales.Core.DTO;

/// <summary>
/// Dto de cliente
/// </summary>
/// <param name="CodigoCliente">Código de cliente</param>
/// <param name="Documentos">Lista de documentos</param>
public record ClienteDto
(
    string CodigoCliente,
    List<DocumentoClienteDto> Documentos
);
