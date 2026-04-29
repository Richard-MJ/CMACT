using AutoMapper;
using AutorizadorCanales.Mapping.CF;
using AutorizadorCanales.Mapping.SG;
using AutorizadorCanales.Mapping.TJ;
using Microsoft.Extensions.DependencyInjection;

namespace AutorizadorCanales.Mapping;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddMappgins(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new AudienciaMapping());
            mc.AddProfile(new CalendarioMapping());
            mc.AddProfile(new TarjetaMapping());
        });

        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}
