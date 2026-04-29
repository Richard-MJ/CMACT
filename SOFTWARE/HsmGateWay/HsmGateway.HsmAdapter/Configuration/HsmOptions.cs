namespace HsmGateway.HsmAdapter.Configuration;

public sealed class HsmOptions
{
    public const string SectionName = "HsmConnection";
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public int ConnectTimeoutMs { get; set; } = 3000;
    public int ReadTimeoutMs { get; set; } = 3000;
    public string ActiveProfile { get; set; } = "LegacyPinv";
    public string SigningKeyVarK { get; set; } = string.Empty;
    public string VerificationKeyVarKEnvVar { get; set; } = "CCE_VERIFY_KEY_CONTAINER";
}
