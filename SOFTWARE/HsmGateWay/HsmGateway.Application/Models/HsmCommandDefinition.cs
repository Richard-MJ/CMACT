namespace HsmGateway.Application.Models;

public sealed record HsmCommandDefinition(
    string Header,
    string Command,
    string? Data = null);