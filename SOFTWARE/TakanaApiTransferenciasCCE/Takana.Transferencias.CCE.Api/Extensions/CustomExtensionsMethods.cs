using System.Net;
using Microsoft.OpenApi;
using System.Reflection;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.IdentityModel.Tokens;
using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Servicio;
using Takana.Transferencias.CCE.Api.Atributos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Infraestructura.Contenedor;

namespace Takana.Transferencias.CCE.Api.Extensions;
public static class CustomExtensionsMethods
{
    /// <summary>
    /// Método que prioriza la configuracion del appsetting.json
    /// </summary>
    /// <param name="servicios"></param>
    /// <param name="configuracion"></param>
    /// <returns>Retorna el servicio agregado</returns>
    public static IServiceCollection AddAppInsight(this IServiceCollection servicios, IConfiguration configuracion)
    {
        servicios.AddApplicationInsightsTelemetry(configuracion);
        return servicios;
    }

    /// <summary>
    /// Método que configura el MVC
    /// </summary>
    /// <param name="servicios"></param>
    /// <returns>Retorna el servicio agregado</returns>
    public static IServiceCollection AddCustomMvc(this IServiceCollection servicios)
    {
        servicios.AddControllers(options =>
        {
            options.Filters.Add(typeof(HttpsRequeridoAttribute));
            options.Filters.Add(typeof(BitacorizarOperacionAttribute));
            options.Filters.Add<HttpGlobalExceptionFilter>();
            options.EnableEndpointRouting = false;
        }).ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        }).AddJsonOptions(opts =>
        {
            opts.JsonSerializerOptions.PropertyNamingPolicy = null;
        });

        return servicios;
    }

    /// <summary>
    /// Agrega el detalle del swagger
    /// </summary>
    /// <param name="servicios"></param>
    /// <returns>Retorna el servicio agregado</returns>
    public static IServiceCollection AddCustomSwagger(this IServiceCollection servicios)
    {
        servicios.AddSwaggerGen(c =>
        {
            c.SwaggerDoc($"{ConfigApi.Version}.0", new OpenApiInfo
            {
                Title = ConfigApi.Nombre,
                Version = ConfigApi.Version,
                Description = ConfigApi.Descripcion,
                Contact = new OpenApiContact
                {
                    Name = ConfigApi.Nombre,
                    Email = "AreadeTIC@cmactacna.com.pe"
                },
                License = new OpenApiLicense
                {
                    Name = "Derechos Reservados"
                },
            });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
            c.ExampleFilters();
            c.EnableAnnotations();
        });

        return servicios;
    }

    /// <summary>
    /// Método que revisa la autorización de token
    /// </summary>
    /// <param name="servicios"></param>
    /// <param name="configuracion"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomAuthorization(this IServiceCollection servicios, IConfiguration configuracion)
    {
        var configuracionAudiencia = new ConfiguracionAudiencia();
        configuracion.Bind(ConfiguracionAudiencia.NombreSeccion, configuracionAudiencia);

        var autenticacion = servicios.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        });

        configuracionAudiencia.AudienciasPermitidas.ForEach(sistema =>
        {
            autenticacion.AddJwtBearer(sistema.SistemaCliente, options =>
            {
                ConfigurarJwt(options,
                    sistema.IdSecreto,
                    sistema.OrigenPermitido,
                    sistema.IdAudiencia);
            });
        });

        servicios.AddAuthorization(options =>
        {
            options.AddPolicy("ModuloVentanilla", policy =>
            {
                policy.AuthenticationSchemes.Add(JwtSchemes.TakanaCliente);
                policy.RequireAuthenticatedUser();
            });

            options.AddPolicy("ModuloTesoreria", policy =>
            {
                policy.AuthenticationSchemes.Add(JwtSchemes.Tesoreria);
                policy.RequireAuthenticatedUser();
            });

            options.AddPolicy("ModuloAppMovil", policy =>
            {
                policy.AuthenticationSchemes.Add(JwtSchemes.AppMovil);
                policy.RequireAuthenticatedUser();
            });

            options.AddPolicy("TodosModulos", policy =>
            {
                policy.AuthenticationSchemes.Add(JwtSchemes.TakanaCliente);
                policy.AuthenticationSchemes.Add(JwtSchemes.Tesoreria);
                policy.AuthenticationSchemes.Add(JwtSchemes.AppMovil);
                policy.RequireAuthenticatedUser();
            });
        });

        return servicios;
    }

    /// <summary>
    /// Configurar los JWT
    /// </summary>
    /// <param name="opciones"></param>
    /// <param name="claveSecreta"></param>
    /// <param name="usuarioValido"></param>
    /// <param name="audiencia"></param>
    private static void ConfigurarJwt(JwtBearerOptions opciones, string claveSecreta, string usuarioValido, string audiencia)
    {
        opciones.RequireHttpsMetadata = true;
        opciones.SaveToken = false;
        opciones.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(claveSecreta)),
            ValidateIssuer = true,
            ValidIssuer = usuarioValido,
            ValidateAudience = true,
            ValidAudience = audiencia,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        opciones.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                var logger = context.HttpContext.RequestServices
                    .GetRequiredService<ILoggerFactory>()
                    .CreateLogger("JWT");

                logger.LogError(context.Exception, "Error de autenticación JWT");
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                var logger = context.HttpContext.RequestServices
                    .GetRequiredService<ILoggerFactory>()
                    .CreateLogger("JWT");

                logger.LogWarning("Desafío JWT lanzado. Error: {Error}, Descripción: {ErrorDescription}",
                    context.Error, context.ErrorDescription);   
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                var logger = context.HttpContext.RequestServices
                    .GetRequiredService<ILoggerFactory>()
                    .CreateLogger("JWT");

                logger.LogInformation("Token JWT validado correctamente.");
                return Task.CompletedTask;
            }
        };
    }

    /// <summary>
    /// Class de los JWTSchemes
    /// </summary>
    public static class JwtSchemes
    {
        public const string TakanaCliente = "TakanaCliente";
        public const string Tesoreria = "TesoreriaWeb";
        public const string AppMovil = "ClienteAPPMovil";
    }

    /// <summary>
    /// Agrega seguridad de JWT a swagger
    /// </summary>
    /// <param name="servicios"></param>
    /// <returns>Retorna el servicio agregado</returns>
    public static IServiceCollection AddCustomSecuritySwagger(this IServiceCollection servicios)
    {
        servicios.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Introduzca el token JWT con el prefijo Bearer"
            });
        });

        return servicios;
    }

    /// <summary>
    /// Encapsula la infomracion sobre una solicitud y una respuesta del HTTP
    /// </summary>
    /// <param name="servicios"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomIntegrations(this IServiceCollection servicios)
    {
        servicios.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        return servicios;
    }

    /// <summary>
    /// Servicio que verifica los estados de los servicios estes habilitados
    /// </summary>
    /// <param name="servicios"></param>
    /// <param name="configuracion"></param>
    /// <param name="logger"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Retorna la coleccion de retursos que estan habilitados</returns>
    public static IServiceCollection AddCustomHealthChecks(
        this IServiceCollection servicios, 
        IConfiguration configuracion, 
        NLog.Logger logger,
        CancellationToken cancellationToken = default)
    {
        var urlPinOperaciones = configuracion["URL_SERVICIO_API_PIN_OPERACIONES"];

        var healthBuilder = servicios.AddHealthChecks();

        var serviceProvider = servicios.BuildServiceProvider();
        var servicioSeguridad = serviceProvider.GetService<IServicioAplicacionSeguridad>();

        var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>()!;
        var httpClient = httpClientFactory.CreateClient("HSMClient");

        healthBuilder.AddCheck("self", () =>
        {
            try
            {
                var respuesta = httpClient.GetAsync(urlPinOperaciones + ConfigApi.RutaHealthy, cancellationToken).GetAwaiter().GetResult();

                if (respuesta.StatusCode != HttpStatusCode.OK) 
                {
                    logger.Error($"El servicio de alta disponibilidad para el utilizar el HSM {urlPinOperaciones} no esta saludable");
                    return HealthCheckResult.Unhealthy($"El servicio de alta disponibilidad para el utilizar el HSM {urlPinOperaciones} no esta saludable");
                }
            }
            catch (HttpRequestException excepcion)
            {
                logger.Error($"Error de solicitud HTTP al verificar el servicio de alta disponibilidad para el utilizar el HSM en {urlPinOperaciones}: {excepcion.Message}");
                return HealthCheckResult.Unhealthy($"Error de solicitud HTTP al verificar el servicio de alta disponibilidad para el utilizar el HSM en {urlPinOperaciones}");
            }
            catch (TaskCanceledException excepcion)
            {
                logger.Error($"El tiempo de espera al consultar el servicio de alta disponibilidad para el utilizar el HSM {urlPinOperaciones} expiró: {excepcion.Message}");
                return HealthCheckResult.Unhealthy($"El tiempo de espera al consultar el servicio de alta disponibilidad para el utilizar el HSM {urlPinOperaciones} expiró");
            }
            catch (Exception excepcion)
            {
                logger.Error($"Error inesperado al consultar el servicio de alta disponibilidad para el utilizar el HSM en {urlPinOperaciones}: {excepcion.Message}");
                return HealthCheckResult.Unhealthy($"Error inesperado al consultar el servicio de alta disponibilidad para el utilizar el HSM en {urlPinOperaciones}:");
            }

            return HealthCheckResult.Healthy($"La aplicacion {ConfigApi.Nombre} esta saludable");
        });

        return servicios;
    }
}