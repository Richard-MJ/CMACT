using HsmGateway.Application.Abstractions.Services;

namespace HsmGateway.HsmAdapter.Profiles;

public interface IHsmProtocolProfileFactory
{
    IHsmProtocolProfile Create();
}