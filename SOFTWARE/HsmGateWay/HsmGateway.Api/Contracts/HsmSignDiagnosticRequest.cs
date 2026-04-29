namespace HsmGateway.Api.Contracts;

public sealed class HsmSignDiagnosticRequest
{
    public string Message { get; init; } = string.Empty;
}