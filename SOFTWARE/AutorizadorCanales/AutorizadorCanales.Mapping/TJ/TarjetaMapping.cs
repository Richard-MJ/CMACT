using AutoMapper;
using AutorizadorCanales.Core.DTO;
using AutorizadorCanales.Domain.Entidades.CL;
using AutorizadorCanales.Domain.Entidades.TJ;

namespace AutorizadorCanales.Mapping.TJ;

public class TarjetaMapping : Profile
{
    public TarjetaMapping()
    {
        CreateMap<Tarjeta, TarjetaDto>()
               .ForCtorParam(nameof(TarjetaDto.NumeroTarjeta), opt => opt.MapFrom(src => src.NumeroTarjeta))
               .ForCtorParam(nameof(TarjetaDto.AfiliacionCanalElectronico), opt => opt.MapFrom(src => src.AfiliacionesCanalElectronico
                   .Where(a => a.IndicadorActivo)
                   .OrderByDescending(a => a.FechaAfiliacionPrincipal)
                   .FirstOrDefault()))
                .ForCtorParam(nameof(TarjetaDto.EstaAfiliadoHomebanking), opt => opt.MapFrom(src => src.EstaAfiliadoHomeBanking()))
                .ForCtorParam(nameof(TarjetaDto.TieneClaveHomebanking), opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.NumeroPvvHomebanking1) && !string.IsNullOrEmpty(src.NumeroPvvHomebanking2)));

        CreateMap<AfiliacionCanalElectronico, AfiliacionCanalElectronicoDto>()
            .ForCtorParam(nameof(AfiliacionCanalElectronicoDto.IdAfiliacionCanalElectronico), opt => opt.MapFrom(src => src.IdAfiliacionCanalElectronico))
            .ForCtorParam(nameof(AfiliacionCanalElectronicoDto.EsTarjetaAfiliada), opt => opt.MapFrom(src => src.EsTarjetaAfiliada))
            .ForCtorParam(nameof(AfiliacionCanalElectronicoDto.EsAfiliacionSms), opt => opt.MapFrom(src => src.EsAfiliacionSms));

        CreateMap<Cliente, ClienteDto>()
            .ForCtorParam(nameof(ClienteDto.CodigoCliente), opt => opt.MapFrom(src => src.CodigoCliente))
            .ForCtorParam(nameof(ClienteDto.Documentos), opt => opt.MapFrom(src => src.Documentos));

        CreateMap<DocumentoCliente, DocumentoClienteDto>()
            .ForCtorParam(nameof(DocumentoClienteDto.CodigoTipoDocumento), opt => opt.MapFrom(src => src.CodigoTipoDocumento))
            .ForCtorParam(nameof(DocumentoClienteDto.NumeroDocumento), opt => opt.MapFrom(src => src.NumeroDocumento));
    }
}
