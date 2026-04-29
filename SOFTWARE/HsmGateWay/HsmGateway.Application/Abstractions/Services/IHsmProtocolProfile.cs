using HsmGateway.Application.Models;

namespace HsmGateway.Application.Abstractions.Services;

public interface IHsmProtocolProfile
{
    string Name { get; }
    HsmCommandDefinition GetConnectionProbe();
}