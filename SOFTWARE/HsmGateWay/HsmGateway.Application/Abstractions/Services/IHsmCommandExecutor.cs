using HsmGateway.Application.Models;

namespace HsmGateway.Application.Abstractions.Services;

public interface IHsmCommandExecutor
{
    Task<HsmParsedResponse> ExecuteAsync(HsmCommandDefinition command, CancellationToken cancellationToken);
}