using HsmGateway.Application.Models;

namespace HsmGateway.Application.Abstractions.Services;

public interface IHsmConnectionProbe
{
    Task<HsmConnectionStatus> CheckAsync(CancellationToken cancellationToken);
}