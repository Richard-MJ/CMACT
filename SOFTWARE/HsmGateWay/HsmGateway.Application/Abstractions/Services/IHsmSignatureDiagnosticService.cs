using HsmGateway.Application.Models;

namespace HsmGateway.Application.Abstractions.Services;

public interface IHsmSignatureDiagnosticService
{
    Task<HsmSignDiagnosticResult> TrySignAsync(string message, CancellationToken cancellationToken);
}