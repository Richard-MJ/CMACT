using HsmGateway.Application.Abstractions.Services;
using HsmGateway.Application.Models;
using HsmGateway.HsmAdapter.Protocol;

namespace HsmGateway.HsmAdapter.Execution;

public sealed class HsmCommandExecutor : IHsmCommandExecutor
{
    private readonly IHsmTransport _transport;

    public HsmCommandExecutor(IHsmTransport transport)
    {
        _transport = transport;
    }

    public async Task<HsmParsedResponse> ExecuteAsync(HsmCommandDefinition command, CancellationToken cancellationToken)
    {
        var requestFrame = HsmRequestFrameBuilder.Build(command);
        var rawResponse = await _transport.SendAsync(requestFrame, cancellationToken);
        return HsmResponseFrameParser.Parse(rawResponse);
    }
}