namespace HsmGateway.Application.Models.SignMessage;
public sealed record HsmVerifyInput(
    string Payload,
    string Protected,
    string Signature);