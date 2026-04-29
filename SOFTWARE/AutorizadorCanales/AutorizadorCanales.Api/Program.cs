using AutorizadorCanales.Api;
using AutorizadorCanales.Aplication;
using AutorizadorCanales.Infrastructure;
using NLog;

var logger = LogManager.Setup()
    .LoadConfigurationFromFile("NLog.config")
    .GetCurrentClassLogger();

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddHttpContextAccessor()
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder,logger);
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseCors(options => options
        .WithOrigins("*").AllowAnyMethod().AllowAnyHeader());
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.MapGet("/", () => "AutorizadorCanales v1.0 - TeamApp © 2024");
    app.Run();
}