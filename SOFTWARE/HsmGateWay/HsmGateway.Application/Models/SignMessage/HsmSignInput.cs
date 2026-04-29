namespace HsmGateway.Application.Models.SignMessage;
public sealed record HsmSignInput(
    string HeaderBase64Url,
    string PayloadBase64Url);