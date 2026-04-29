namespace HsmGateway.Application.Models;

public sealed record HsmParsedResponse(
    int Length,
    string PayloadAscii,
    string PayloadHex,
    string Summary);