using HsmGateway.Application.Abstractions.Services;
using HsmGateway.HsmAdapter.Configuration;
using Microsoft.Extensions.Options;

namespace HsmGateway.HsmAdapter.Profiles;

public sealed class HsmProtocolProfileFactory : IHsmProtocolProfileFactory
{
    private readonly HsmOptions _options;

    public HsmProtocolProfileFactory(IOptions<HsmOptions> options)
    {
        _options = options.Value;
    }

    public IHsmProtocolProfile Create()
    {
        return _options.ActiveProfile switch
        {
            "LegacyPinv" => new LegacyPinvProtocolProfile(),
            "ZeroEcho" => new ZeroEchoProtocolProfile(),
            _ => throw new InvalidOperationException(
                $"El perfil '{_options.ActiveProfile}' no está soportado.")
        };
    }
}