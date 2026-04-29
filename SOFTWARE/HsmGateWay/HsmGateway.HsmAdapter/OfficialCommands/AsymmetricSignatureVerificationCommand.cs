using HsmGateway.HsmAdapter.Protocol;

namespace HsmGateway.HsmAdapter.OfficialCommands;

public static class AsymmetricSignatureVerificationCommand
{
    public const string CommandId = "1104";

    private const string HashIdentifier = "12";
    private const string Mechanism = "01";
    private const string Algorithm = "01";

    public static string BuildRequestData(string rsaKeyVarK, byte[] digest, byte[] signatureBytes)
    {
        if (string.IsNullOrWhiteSpace(rsaKeyVarK))
            throw new ArgumentException("La RSA key varK es obligatoria.", nameof(rsaKeyVarK));

        if (digest is null || digest.Length == 0)
            throw new ArgumentException("El digest es obligatorio.", nameof(digest));

        if (signatureBytes is null || signatureBytes.Length == 0)
            throw new ArgumentException("La firma es obligatoria.", nameof(signatureBytes));

        var digestHex = HsmFieldFormatter.ToHex(digest);
        var signatureHex = HsmFieldFormatter.ToHex(signatureBytes);

        return rsaKeyVarK
             + HashIdentifier
             + Mechanism
             + Algorithm
             + HsmFieldFormatter.To6N(digest.Length)
             + digestHex
             + HsmFieldFormatter.To6N(signatureBytes.Length)
             + signatureHex;
    }

    public static bool ParseResponse(string payloadAscii)
    {
        var reader = new HsmBodyReader(payloadAscii);

        var commandId = reader.Read(4);
        if (commandId != CommandId)
            throw new InvalidOperationException($"Se esperaba {CommandId} y llegó {commandId}.");

        var errorCode = reader.Read(8);
        if (errorCode != "00000000")
            throw new InvalidOperationException($"El HSM devolvió error en {CommandId}: {errorCode}");

        var verificationResult = reader.ReadNumeric(1);
        return verificationResult == 0;
    }
}