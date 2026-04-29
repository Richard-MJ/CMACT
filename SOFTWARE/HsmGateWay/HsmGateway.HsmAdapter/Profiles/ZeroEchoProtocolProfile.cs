using HsmGateway.Application.Abstractions.Services;
using HsmGateway.Application.Models;

namespace HsmGateway.HsmAdapter.Profiles;

public sealed class ZeroEchoProtocolProfile : IHsmProtocolProfile
{
    public string Name => "ZeroEcho";

    public HsmCommandDefinition GetConnectionProbe()
    {
        return new HsmCommandDefinition(
            Header: "0000",
            Command: "0000");
    }
}