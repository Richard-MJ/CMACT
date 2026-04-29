namespace AutorizadorCanales.Core.DTO;

/// <summary>
/// Dto de afiliación a canales electrónicos
/// </summary>
/// <param name="IdAfiliacionCAnalElectronico">Id de la afiliación</param>
/// <param name="EsTarjetaAfiliada">Indicador si la tarjeta es afiliada</param>
/// <param name="EsAfiliacionSms">Indicador si es afiliación SMS</param>
public record AfiliacionCanalElectronicoDto
(
    int IdAfiliacionCanalElectronico,
    bool EsTarjetaAfiliada,
    bool EsAfiliacionSms
);
