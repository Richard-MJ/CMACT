namespace HsmGateway.Application.Models;

public sealed record HsmConnectionStatus(
    bool IsConnected,
    string ProfileName,
    string Endpoint,
    long LatencyMs,
    string Summary,
    string ResponseAscii,
    string ResponseHex);