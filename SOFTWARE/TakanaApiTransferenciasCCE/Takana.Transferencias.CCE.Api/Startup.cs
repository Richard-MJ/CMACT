using Asp.Versioning.ApiExplorer;
using AspNetCoreRateLimit;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Hangfire;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;
using Prometheus;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;
using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Extensions;
using Takana.Transferencias.CCE.Api.Helpers;
using Takana.Transferencias.CCE.Api.Infraestructura.Contenedor;
using Takana.Transferencias.CCE.Api.Servicio;

namespace Takana.Transferencias.CCE.Api;
public class Startup
{
    private IConfiguration _configuration { get; }
    private const string CorsPolicy = "AllowSpecificOrigins";
    private readonly string swaggerBasePath = "docs/specification";

    /// <summary>
    /// Constructor del startup
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Método que configura el servicio
    /// </summary>
    /// <param name="servicios"></param>
    /// <returns>Retorna el servicio</returns>
    public IServiceProvider ConfigureServices(IServiceCollection servicios)
    {
        servicios.Configure<ConfiguracionBaseDatosSAF>(_configuration.GetSection("BaseDatosConexionSAF"));
        servicios.Configure<ConfiguracionBaseDatosRedis>(_configuration.GetSection("BaseDatosConexionRedis"));
        servicios.Configure<ConfiguracionColas>(_configuration.GetSection("ServicioColas"));
        servicios.Configure<ConfiguracionConsul>(_configuration.GetSection("ServicioConsul"));
        servicios.Configure<ConfiguracionReporteSFTP>(_configuration.GetSection("ServicioReporteSFTP"));
        servicios.Configure<ConfiguracionDirectorioSFTP>(_configuration.GetSection("ServicioDirectorioSFTP"));
        servicios.Configure<ConfiguracionCanalElectronicoWorkstation>(_configuration.GetSection("CanalElectronicoWorkstation"));
        servicios.Configure<ConfiguracionPollyWrapOptions>(_configuration.GetSection("PollyWrap"));

        var logger = LoggingHelper.ConfigurarNLog();

        ConfigurarCors(servicios, logger);
        ConfigurarIpRateLimiting(servicios, logger);

        servicios.AddConfiguracinHangFire(_configuration);
        servicios.AddScoped<TareasProgramadasServices>();

        servicios.AddConfiguracionInfraestructura();
        servicios.AddConfiguracionValidarCertificadoCCE(_configuration);
        servicios.AddConfiguracionPatronKeyStrategy();
        servicios.AddConfiguracionBitacora(logger);

        servicios.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        servicios.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
                options.TagActionsBy(apiDesc =>
                {
                    return apiDesc.CustomAttributes()
                        .OfType<SwaggerOperationAttribute>()
                        .SelectMany(attr => attr.Tags)
                        .ToList();
                });
            });

        servicios.AddHttpClient("HSMClient", client =>
        {
            int tiempo = _configuration.GetValue<int>("TIME_OUT_HTTP_CLIENT");
            client.Timeout = TimeSpan.FromSeconds(tiempo);
        }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        });

        servicios.AddSingleton<DescryptacionResquestBodyMiddleware>();
        servicios.AddScoped<TareasProgramadasServices>();
        servicios.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerVersionServices>();
        servicios.AddControllersWithViews();
        servicios.AddSignalR();
        servicios.Configure<KestrelServerOptions>(options => {
            options.AllowSynchronousIO = true;
            options.AddServerHeader = false;
        });
    
        servicios
            .AddConsul()
            .AddCustomMvc()
            .AddCustomSwagger()
            .AddCustomSecuritySwagger()
            .AddCustomAuthorization(_configuration)
            .AddCustomHealthChecks(_configuration, logger)
            .AddAppInsight(_configuration)
            .AddCustomIntegrations()
            .AddSwaggerExamplesFromAssemblyOf<SwaggerExamplesDTO>();

        var container = new ContainerBuilder();
        container.Populate(servicios);
        return new AutofacServiceProvider(container.Build());
    }

    /// <summary>
    /// Método que realiza la configuracion principal
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="provider"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
    {
        if (env.IsProduction())
        {
            app.UseHttpsRedirection();
            app.UseHsts();
        }

        app.UseHangfireDashboard(ConfigApi.RutaHangFire, new DashboardOptions
        {
            Authorization = new[] { new MyAuthorizationFilter() }
        });

        ConfigurarTareasProgramadasHangFire(app.ApplicationServices);
        app.UseStaticFiles();
        app.UseConsul();
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger(c => c.RouteTemplate = swaggerBasePath + "/{documentName}/swagger.json");
            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/{swaggerBasePath}/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                        options.RoutePrefix = $"{swaggerBasePath}";
                        options.InjectStylesheet($"/swagger-ui/custom.css");
                        options.InjectJavascript($"/swagger-ui/custom.js", "text/javascript");
                        options.DocumentTitle = $"{ConfigApi.Nombre} - {ConfigApi.Descripcion}";
                    }
                });
        }
        app.UseRouting();
        app.UseMiddleware<JsonLoggerMiddleware>();
        app.UseMiddleware<DescryptacionResquestBodyMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseIpRateLimiting();
        app.UseCors(CorsPolicy);
        app.UseHttpMetrics();
        app.UseMetricServer();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapMetrics();
            endpoints.MapControllers();
            endpoints.MapHangfireDashboard();
            endpoints.MapHealthChecks(ConfigApi.RutaHealthy, new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    var result = new
                    {
                        Estado = report.Status.ToString(),
                        Descripcion = report.Entries.First().Value.Description,
                        TotalDuracion = report.TotalDuration.TotalSeconds
                    };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(result));
                }
            });
        });

        Console.WriteLine($"Inicio del servicio: {ConfigApi.Nombre} {ConfigApi.Version}");
    }

    /// <summary>
    /// Método que configura el CORS
    /// </summary>
    /// <param name="services"></param>
    /// <param name="logger"></param>
    private void ConfigurarCors(IServiceCollection services, NLog.Logger logger)
    {
        var CorsOriginAllowed = _configuration.GetSection("Cors:AllowedOrigins").Get<List<string>>();
        if (CorsOriginAllowed == null || !CorsOriginAllowed.Any())        
            throw new InvalidOperationException("No se han configurado orígenes permitidos para CORS.");
        
        var origins = CorsOriginAllowed.ToArray();
        logger.Info("Configurando Origenes para ({CORS})", origins);

        services.AddCors(options =>
        {
            options.AddPolicy(CorsPolicy,
                builder => builder
                .WithOrigins(origins)
                .AllowAnyMethod()
                .AllowAnyHeader());
        });
        logger.Info("Fin de Configurando Origenes para ({CORS})", origins);
    }

    /// <summary>
    /// Método que configura el ip rate limiting
    /// </summary>
    /// <param name="servicios"></param>
    /// <param name="logger"></param>
    private void ConfigurarIpRateLimiting(IServiceCollection servicios, NLog.Logger logger)
    {
        logger.Info($"Configurando limitacion de velocidad de IP");

        servicios.AddMemoryCache();
        var rateLimitOptions = _configuration.GetSection("IpRateLimiting").Get<IpRateLimitOptions>();
        servicios.Configure<IpRateLimitOptions>(options =>
        {
            options.EnableEndpointRateLimiting = rateLimitOptions!.EnableEndpointRateLimiting;
            options.StackBlockedRequests = rateLimitOptions.StackBlockedRequests;
            options.RealIpHeader = rateLimitOptions.RealIpHeader;
            options.ClientIdHeader = rateLimitOptions.ClientIdHeader;
            options.HttpStatusCode = rateLimitOptions.HttpStatusCode;
            options.IpWhitelist = rateLimitOptions.IpWhitelist;
            options.EndpointWhitelist = rateLimitOptions.EndpointWhitelist;
            options.ClientWhitelist = rateLimitOptions.ClientWhitelist;
            options.GeneralRules = rateLimitOptions.GeneralRules;
            options.QuotaExceededResponse = rateLimitOptions.QuotaExceededResponse;
        });
        servicios.Configure<IpRateLimitPolicies>(_configuration.GetSection("IpRateLimitPolicies"));
        servicios.AddInMemoryRateLimiting();
        servicios.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        logger.Info($"Fin de Configuración de limitacion de velocidad de IP");
    }

    /// <summary>
    /// Se configuran las Tareas programadas con Hang Fire
    /// </summary>
    /// <param name="serviceProvider"></param>
    private void ConfigurarTareasProgramadasHangFire(IServiceProvider serviceProvider)
    {
        var logger = LoggingHelper.ConfigurarNLog();
        logger.Info($"Configurando las tareas programadas para reportes CCE");

        var tareasService = serviceProvider.GetService<TareasProgramadasServices>()!;
        tareasService.ProgramarTareasHangFire();

        logger.Info($"Fin de las tareas programadas para reportes CCE");
    }
}