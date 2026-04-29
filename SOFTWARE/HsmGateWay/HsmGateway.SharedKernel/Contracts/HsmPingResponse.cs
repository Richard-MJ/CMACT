namespace HsmGateway.SharedKernel.Contracts;
public sealed class HsmPingResponse
{
    public bool Success { get; init; }
    public string Header { get; init; } = string.Empty;
    public string Command { get; init; } = string.Empty;
    public int ResponseLength { get; init; }
    public string ResponseAscii { get; init; } = string.Empty;
    public string ResponseHex { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}