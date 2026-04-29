using NLog;
using NLog.Web;
using PagareElectronico.Api.Exceptions;
using PagareElectronico.Api.Middlewares;
using PagareElectronico.Infrastructure.DependencyInjection;

var logger = LogManager.Setup()
    .LoadConfigurationFromFile("Nlog.config")
    .GetCurrentClassLogger();

try
{
    logger.Info("==============================================================");
    logger.Info("Iniciando la aplicación API Pagaré Electrónico.");
    logger.Info("==============================================================");
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHealthChecks();
    builder.Services.AddProblemDetails();
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();

    app.Lifetime.ApplicationStarted.Register(() =>
    {
        logger.Info("La aplicación se inició correctamente.");
        logger.Info("Entorno actual: {0}", app.Environment.EnvironmentName);
        logger.Info("Ruta base de logs: C:\\logs\\PagareElectronico.Api");
    });

    app.Lifetime.ApplicationStopping.Register(() =>
    {
        var stoppingLogger = LogManager.GetCurrentClassLogger();
        stoppingLogger.Warn("La aplicación se está deteniendo.");
    });

    app.Lifetime.ApplicationStopped.Register(() =>
    {
        var stoppedLogger = LogManager.GetCurrentClassLogger();
        stoppedLogger.Info("La aplicación se detuvo correctamente.");
    });

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler();

    app.UseMiddleware<RequestResponseLoggingMiddleware>();

    app.MapHealthChecks("/health");
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
catch (Exception exception)
{
    logger.Fatal(exception, "La aplicación se detuvo por un error fatal durante el arranque.");
    throw;
}
finally
{
    LogManager.Shutdown();
}
