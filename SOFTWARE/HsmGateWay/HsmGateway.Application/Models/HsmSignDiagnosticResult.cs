namespace HsmGateway.Application.Models;

public sealed record HsmSignDiagnosticResult(
    string Message,
    string DigestHex,
    IReadOnlyList<HsmSignDiagnosticTry> Tries);

public sealed record HsmSignDiagnosticTry(
    string Variant,
    string RequestBodyAscii,
    string RequestFrameHex,
    string? ResponseBodyAscii,
    string? ResponseFrameHex,
    string? Note,
    string? Error);