namespace HsmGateway.Api.Contracts;

public sealed class HsmSignDiagnosticResponse
{
    public string Message { get; init; } = string.Empty;
    public string DigestHex { get; init; } = string.Empty;
    public List<HsmSignDiagnosticTryResponse> Tries { get; init; } = new();
}

public sealed class HsmSignDiagnosticTryResponse
{
    public string Variant { get; init; } = string.Empty;
    public string RequestBodyAscii { get; init; } = string.Empty;
    public string RequestFrameHex { get; init; } = string.Empty;
    public string? ResponseBodyAscii { get; init; }
    public string? ResponseFrameHex { get; init; }
    public string? Note { get; init; }
    public string? Error { get; init; }
}