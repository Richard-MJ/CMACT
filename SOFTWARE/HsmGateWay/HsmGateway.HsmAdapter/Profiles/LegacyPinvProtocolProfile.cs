using HsmGateway.Application.Abstractions.Services;
using HsmGateway.Application.Models;

namespace HsmGateway.HsmAdapter.Profiles;

public sealed class LegacyPinvProtocolProfile : IHsmProtocolProfile
{
    public string Name => "LegacyPinv";

    public HsmCommandDefinition GetConnectionProbe()
    {
        return new HsmCommandDefinition(
            Header: "PINV",
            Command: "AS");
    }
}