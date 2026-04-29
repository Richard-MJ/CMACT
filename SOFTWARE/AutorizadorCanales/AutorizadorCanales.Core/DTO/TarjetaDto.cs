namespace AutorizadorCanales.Core.DTO;

/// <summary>
/// Dto de tarjeta
/// </summary>
/// <param name="NumeroTarjeta">Número de tarjeta</param>
/// <param name="AfiliacionCanalElectronico">Entidad afiliación canal electrónico</param>
/// <param name="Duenio">Entidad cliente</param>
/// <param name="EstaAfiliadoHomebanking">Indicador si está afiliado homebanking</param>
/// <param name="TieneClaveHomebanking">Indicador si tiene clave homebanking</param>
public record TarjetaDto
(
    decimal NumeroTarjeta,
    AfiliacionCanalElectronicoDto? AfiliacionCanalElectronico,
    ClienteDto? Duenio,
    bool EstaAfiliadoHomebanking,
    bool TieneClaveHomebanking
);
