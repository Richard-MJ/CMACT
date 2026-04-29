namespace HsmGateway.Application.Models.SignMessage;
public sealed record HsmVerifyResult(
    bool IsValid,
    string DecodedPayloadJson);