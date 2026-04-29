namespace HsmGateway.Api.Contracts;

public sealed class HsmSignRequest
{
    public string Header { get; init; } = string.Empty;
    public string Contenido { get; init; } = string.Empty;
}