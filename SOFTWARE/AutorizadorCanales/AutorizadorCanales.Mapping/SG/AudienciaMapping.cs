using AutoMapper;
using AutorizadorCanales.Contracts.SG.Response;
using AutorizadorCanales.Domain.Entidades;
using AutorizadorCanales.Domain.Entidades.SG;

namespace AutorizadorCanales.Mapping.SG;

public class AudienciaMapping : Profile
{
    public AudienciaMapping()
    {
        CreateMap<Audiencia, AudienciaResponse>()
            .ForCtorParam(nameof(AudienciaResponse.IdAudiencia), opt => opt.MapFrom(src => src.Id))
            .ForCtorParam(nameof(AudienciaResponse.IdSecreto), opt => opt.MapFrom(src => src.IdSecreto))
            .ForCtorParam(nameof(AudienciaResponse.IndicadorCanal), opt => opt.MapFrom(src => src.IndicadorCanal))
            .ForCtorParam(nameof(AudienciaResponse.DireccionOrigenPermitido), opt => opt.MapFrom(src => src.DireccionOrigenPermitido))
            .ForCtorParam(nameof(AudienciaResponse.AplicaInactividad), opt => opt.MapFrom(src => src.IndicadorAplicaInactividad == EstadoEntidad.SI));
    }
}
