using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using PagareElectronico.Infraestructura.Http;
using PagareElectronico.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using PagareElectronico.Infraestructura.Logging;
using PagareElectronico.Infrastructure.Configuration;
using PagareElectronico.Infrastructure.Authentication;
using PagareElectronico.Application.Abstractions.Services;
using PagareElectronico.Infrastructure.Integrations.Clients;
using PagareElectronico.Application.Abstractions.Integrations;
using PagareElectronico.Infrastructure.Integrations.Cavali.Mappers;

namespace PagareElectronico.Infrastructure.DependencyInjection;

/// <summary>
/// Contiene métodos de extensión para registrar dependencias de infraestructura.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registra los servicios necesarios para consumir CAVALI desde la capa de infraestructura.
    /// </summary>
    /// <param name="services">Colección de servicios del contenedor de dependencia.</param>
    /// <param name="configuration">Configuración de la aplicación.</param>
    /// <returns>La colección de servicios configurada.</returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<CavaliApiOptions>(configuration.GetSection(CavaliApiOptions.SectionName));

        services.AddMemoryCache();

        services.AddScoped<IPagareService, PagareService>();
        services.AddScoped<ICavaliTokenProvider, CavaliTokenProvider>();
        services.AddSingleton(typeof(IBitacora<>), typeof(Bitacora<>));
        services.AddTransient<CavaliAuthHandler>();
        services.AddTransient<ThirdPartyLoggingHandler>();

        services.AddScoped<AnnotationAccountRequestMapper>();
        services.AddScoped<CancellationRequestMapper>();
        services.AddScoped<DeletionRequestMapper>();
        services.AddScoped<ReverseRequestMapper>();

        services.AddHttpClient("CavaliAuth", (sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<CavaliApiOptions>>().Value;
            client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
            client.DefaultRequestHeaders.Add("x-api-key", options.ApiKey);
        });

        services.AddHttpClient<ICavaliPagareGateway, CavaliPagareClient>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<CavaliApiOptions>>().Value;

            client.BaseAddress = new Uri(options.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("x-api-key", options.ApiKey);
        })
        .AddHttpMessageHandler<CavaliAuthHandler>()
        .AddHttpMessageHandler<ThirdPartyLoggingHandler>();

        return services;
    }
}