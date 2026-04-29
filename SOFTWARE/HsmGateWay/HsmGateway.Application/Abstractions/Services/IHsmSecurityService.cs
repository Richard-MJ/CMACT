using HsmGateway.Application.Models.SignMessage;

namespace HsmGateway.Application.Abstractions.Services;

public interface IHsmSecurityService
{
    Task<HsmSignedMessage> SignAsync(HsmSignInput input, CancellationToken cancellationToken);
    Task<HsmVerifyResult> VerifyAsync(HsmVerifyInput input, CancellationToken cancellationToken);
}