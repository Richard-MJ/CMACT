using HsmGateway.HsmAdapter.Protocol;

namespace HsmGateway.HsmAdapter.OfficialCommands;

public static class GetInternalRsaKeyCommand
{
    public const string CommandId = "0106";

    public static string BuildRequestData(string rsaLabel)
    {
        if (string.IsNullOrWhiteSpace(rsaLabel))
            throw new ArgumentException("El label RSA es obligatorio.", nameof(rsaLabel));

        return HsmFieldFormatter.To2N(rsaLabel.Length) + rsaLabel;
    }

    public static string ParseResponse(string payloadAscii)
    {
        var reader = new HsmBodyReader(payloadAscii);

        var commandId = reader.Read(4);
        if (commandId != CommandId)
            throw new InvalidOperationException($"Se esperaba {CommandId} y llegó {commandId}.");

        var errorCode = reader.Read(8);
        if (errorCode != "00000000")
            throw new InvalidOperationException($"El HSM devolvió error en {CommandId}: {errorCode}");

        return reader.ReadToEnd();
    }
}