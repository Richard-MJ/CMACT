using AutorizadorCanales.Aplication.Features.Autenticacion.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace AutorizadorCanales.Aplication;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR( cfg => cfg.RegisterServicesFromAssemblies(typeof(DependencyInjectionRegister).Assembly));

        //services.AddScoped(
        //    typeof(IPipelineBehavior<,>),
        //    typeof(ValidationBehavior<,>));

        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<IAutenticacionCommandFactory, AutenticacionCommandFactory>();
        return services;
    }
}
