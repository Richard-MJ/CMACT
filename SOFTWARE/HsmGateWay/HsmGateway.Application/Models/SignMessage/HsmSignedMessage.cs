namespace HsmGateway.Application.Models.SignMessage;

public sealed record HsmSignedMessage(
    string Payload,
    string Protected,
    string Signature);