using AutoMapper;
using AutorizadorCanales.Core.DTO;
using AutorizadorCanales.Domain.Entidades.CF;

namespace AutorizadorCanales.Mapping.CF;

public class CalendarioMapping : Profile
{
    public CalendarioMapping()
    {
        CreateMap<Calendario, CalendarioDto>()
            .ForMember(des => des.CodigoSistema, opt => opt.MapFrom(src => src.CodigoSistema))
            .ForMember(des => des.CodigoAgencia, opt => opt.MapFrom(src => src.CodigoAgencia))
            .ForMember(des => des.FechaSistema, opt => opt.MapFrom(src => src.FechaSistema));
    }
}
