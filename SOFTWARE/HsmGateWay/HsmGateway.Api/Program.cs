using HsmGateway.Application.Abstractions.Services;
using HsmGateway.HsmAdapter.Configuration;
using HsmGateway.HsmAdapter.Execution;
using HsmGateway.HsmAdapter.Probing;
using HsmGateway.HsmAdapter.Profiles;
using HsmGateway.HsmAdapter.Services;
using HsmGateway.HsmAdapter.Transport;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

builder.Services.Configure<HsmOptions>(
    builder.Configuration.GetSection(HsmOptions.SectionName));

builder.Services.AddScoped<IHsmTransport, TcpHsmTransport>();
builder.Services.AddScoped<IHsmSecurityService, HsmSecurityService>();
builder.Services.AddScoped<IHsmSignatureDiagnosticService, HsmSignatureDiagnosticService>();
builder.Services.AddScoped<IHsmCommandExecutor, HsmCommandExecutor>();
builder.Services.AddScoped<IHsmConnectionProbe, HsmConnectionProbe>();

builder.Services.AddScoped<IHsmProtocolProfileFactory, HsmProtocolProfileFactory>();
builder.Services.AddScoped(sp =>
{
    var factory = sp.GetRequiredService<IHsmProtocolProfileFactory>();
    return factory.Create();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();