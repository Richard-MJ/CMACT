using HsmGateway.Application.Abstractions.Services;
using HsmGateway.Application.Models;
using HsmGateway.HsmAdapter.Configuration;
using Microsoft.Extensions.Options;

namespace HsmGateway.HsmAdapter.Probing;

public sealed class HsmConnectionProbe : IHsmConnectionProbe
{
    private readonly IHsmProtocolProfile _profile;
    private readonly IHsmCommandExecutor _executor;
    private readonly HsmOptions _options;

    public HsmConnectionProbe(
        IHsmProtocolProfile profile,
        IHsmCommandExecutor executor,
        IOptions<HsmOptions> options)
    {
        _profile = profile;
        _executor = executor;
        _options = options.Value;
    }

    public async Task<HsmConnectionStatus> CheckAsync(CancellationToken cancellationToken)
    {
        var command = _profile.GetConnectionProbe();

        var start = DateTime.UtcNow;
        var response = await _executor.ExecuteAsync(command, cancellationToken);
        var end = DateTime.UtcNow;

        return new HsmConnectionStatus(
            IsConnected: true,
            ProfileName: _profile.Name,
            Endpoint: $"{_options.Host}:{_options.Port}",
            LatencyMs: (long)(end - start).TotalMilliseconds,
            Summary: response.Summary,
            ResponseAscii: response.PayloadAscii,
            ResponseHex: response.PayloadHex);
    }
}