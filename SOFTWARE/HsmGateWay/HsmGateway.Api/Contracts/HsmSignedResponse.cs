namespace HsmGateway.Api.Contracts;
public sealed class HsmSignedResponse
{
    public string payload { get; init; } = string.Empty;
    public string @protected { get; init; } = string.Empty;
    public string signature { get; init; } = string.Empty;
}