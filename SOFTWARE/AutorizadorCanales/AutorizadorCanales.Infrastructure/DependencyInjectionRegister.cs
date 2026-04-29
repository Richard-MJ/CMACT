using AutorizadorCanales.Aplication.Common.JwtGenerator;
using AutorizadorCanales.Aplication.Common.ServicioColas;
using AutorizadorCanales.Aplication.Common.ServicioColas.Configuracion;
using AutorizadorCanales.Aplication.Common.ServicioPinOperacionesApi;
using AutorizadorCanales.Aplication.Common.ServicioPinOperacionesApi.Configuracion;
using AutorizadorCanales.Aplication.Common.ServicioSeguridad;
using AutorizadorCanales.Aplication.Common.ServicioSeguridad.Configuracion;
using AutorizadorCanales.Aplication.Servicios.Afiliacion;
using AutorizadorCanales.Aplication.Servicios.Autenticacion;
using AutorizadorCanales.Aplication.Servicios.Sesion;
using AutorizadorCanales.Domain.Repositorios;
using AutorizadorCanales.Infrastructure.Configuracion;
using AutorizadorCanales.Infrastructure.Logging.Nlog;
using AutorizadorCanales.Infrastructure.Modelos;
using AutorizadorCanales.Infrastructure.Persistencia;
using AutorizadorCanales.Infrastructure.Persistencia.Repositorios;
using AutorizadorCanales.Infrastructure.ServicioColas.Implementacion;
using AutorizadorCanales.Infrastructure.Servicios;
using AutorizadorCanales.Logging.Interfaz;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace AutorizadorCanales.Infrastructure;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        WebApplicationBuilder builder,
        Logger logger)
    {
        services.AgregarContexto();
        services.AgregarBitacoraConfiguracion(builder, logger);
        services.AgregarAutorizacion(builder.Configuration);
        services.AgregarPersistencia(builder.Configuration);
        services.AgregarServicioColas(builder.Configuration);
        services.AgregarServicioPinOperacionesApi(builder.Configuration);
        services.AgregarServicioSeguridad(builder.Configuration);
        services.AgregarServicios();

        return services;
    }

    public static IServiceCollection AgregarPersistencia(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionStrings = new ConnectionStrings();
        configuration.Bind(ConnectionStrings.NombreSeccion, connectionStrings);
        services.AddDbContext<ContextoLectura>(options => options.UseSqlServer(connectionStrings.Query));
        services.AddDbContext<ContextoEscritura>(options => options.UseSqlServer(connectionStrings.Command));

        services.AddScoped<IRepositorioLectura, RepositorioLectura>();
        services.AddScoped<IRepositorioEscritura, RepositorioEscritura>();

        return services;
    }

    public static IServiceCollection AgregarContexto(this IServiceCollection services)
    {
        services.AddScoped<Sesion>();

        services.AddScoped<IContextoBitacora>(x => x.GetRequiredService<Sesion>());
        services.AddScoped<IContextoApiColas>(x => x.GetRequiredService<Sesion>());
        services.AddScoped<IContexto>(x => x.GetRequiredService<Sesion>());

        services.AddTransient(typeof(IBitacora<>), typeof(NlogProxy<>));

        return services;
    }

    public static IServiceCollection AgregarAutorizacion(this IServiceCollection services,
        IConfiguration configuration)
    {
        var configuracionAudiencia = new ConfiguracionAudiencia();
        configuration.Bind(ConfiguracionAudiencia.NombreSeccion, configuracionAudiencia);
        services.AddSingleton(configuracionAudiencia);
        services.AddScoped<IServicioGeneradorToken, ServicioGeneradorToken>();

        var autenticacion = services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        });

        configuracionAudiencia.AudienciasPermitidas.ForEach(sistema =>
        {
            autenticacion.AddJwtBearer(sistema.SistemaCliente, option =>
            {
                option.RequireHttpsMetadata = true;
                option.SaveToken = false;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(sistema.IdSecreto)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = sistema.OrigenPermitido,
                    ValidAudience = sistema.IdSistemaCliente,
                    ClockSkew = TimeSpan.Zero
                };
            });
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("ClientPolicy", policy =>
            {
                configuracionAudiencia.AudienciasPermitidas.ForEach(sistema =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.AddAuthenticationSchemes(sistema.SistemaCliente);
                });
            });
        });

        return services;
    }

    public static IServiceCollection AgregarServicioColas(this IServiceCollection services,
        IConfiguration configuration)
    {
        var configuracionServicioColas = new ConfiguracionServicioColas();
        configuration.Bind(ConfiguracionServicioColas.NombreSeccion, configuracionServicioColas);
        services.AddSingleton(configuracionServicioColas);
        services.AddScoped<IServicioColasCore, ServicioColasCore>();

        return services;
    }

    public static IServiceCollection AgregarServicioPinOperacionesApi(this IServiceCollection services,
        IConfiguration configuration)
    {
        var configuracionServicioPinOperacionesApi = new ConfiguracionServicioPinOperacionesApi();
        configuration.Bind(ConfiguracionServicioPinOperacionesApi.NombreSeccion, configuracionServicioPinOperacionesApi);
        services.AddSingleton(configuracionServicioPinOperacionesApi);
        services.AddScoped<IServicioPinOperaciones, ServicioPinOperaciones>();

        return services;
    }

    public static IServiceCollection AgregarServicioSeguridad(this IServiceCollection services,
        IConfiguration configuration)
    {
        var configuracionServicioSeguridad = new ConfiguracionServicioSeguridad();
        configuration.Bind(ConfiguracionServicioSeguridad.NombreSeccion, configuracionServicioSeguridad);
        services.AddSingleton(configuracionServicioSeguridad);
        services.AddScoped<IServicioApiSeguridad, ServicioApiSeguridad>();

        return services;
    }

    /// <summary>
    /// Metodo que agrega servicios
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AgregarServicios(this IServiceCollection services)
    {
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IServicioAutenticacion, ServicioAutenticacion>();
        services.AddScoped<IServicioAutenticacionKiosko, ServicioAutenticacionKiosko>();
        services.AddScoped<IServicioAfiliacion, ServicioAfiliacion>();
        services.AddScoped<IServicioSesionCanalElectronico, ServicioSesionCanalElectronico>();
        services.AddScoped<IServicioRefrescoAcceso, ServicioRefrescoAcceso>();
        services.AddScoped<IServicioAlertaInicioSesion, ServicioAlertaInicioSesion>();
        services.AddScoped<IServicioAutenticacionAperturaProductos, ServicioAutenticacionAperturaProductos>();

        return services;
    }

    public static IServiceCollection AgregarBitacoraConfiguracion(this IServiceCollection services,
        WebApplicationBuilder builder, NLog.Logger logger)
    {
        var contexto = new Sesion();
        logger.WithProperty("idSesion", contexto.IdSesion);
        logger.WithProperty("codigoUsuario", contexto.CodigoUsuario);
        logger.WithProperty("indicadorCanal", contexto.IndicadorCanal);
        logger.WithProperty("indicadorSubCanal", contexto.IndicadorSubCanal);
        logger.WithProperty("idTerminalCliente", contexto.IdTerminalOrigen);
        logger.WithProperty("fechaEvento", DateTime.Now.ToString("O"));
        logger.WithProperty("idServicio", contexto.IdServicio);
        logger.Debug($"Inicio del servicio: Autorizador Canales");

        builder.Logging.ClearProviders();
        builder.Logging.SetMinimumLevel(LogLevel.Trace);
        builder.Host.UseNLog();

        return services;
    }
}
