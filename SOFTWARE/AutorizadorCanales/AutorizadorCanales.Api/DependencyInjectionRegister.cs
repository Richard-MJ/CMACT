using AutorizadorCanales.Api.Filters;
using AutorizadorCanales.Mapping;

namespace AutorizadorCanales.Api;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<EstablecerSesionFilter>();
        });
        services.AddMappgins();
        return services;
    }
}
