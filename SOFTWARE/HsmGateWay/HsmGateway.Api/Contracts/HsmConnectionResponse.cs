namespace HsmGateway.Api.Contracts;

public sealed class HsmConnectionResponse
{
    public bool IsConnected { get; init; }
    public string ProfileName { get; init; } = string.Empty;
    public string Endpoint { get; init; } = string.Empty;
    public long LatencyMs { get; init; }
    public string Summary { get; init; } = string.Empty;
    public string ResponseAscii { get; init; } = string.Empty;
    public string ResponseHex { get; init; } = string.Empty;
}