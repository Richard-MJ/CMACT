using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using HsmGateway.Application.Models;
using HsmGateway.HsmAdapter.Protocol;
using HsmGateway.HsmAdapter.Configuration;
using HsmGateway.HsmAdapter.OfficialCommands;
using HsmGateway.Application.Abstractions.Services;

namespace HsmGateway.HsmAdapter.Services;

public sealed class HsmSignatureDiagnosticService : IHsmSignatureDiagnosticService
{
    private readonly IHsmTransport _transport;
    private readonly HsmOptions _HsmOptions;

    public HsmSignatureDiagnosticService(
        IHsmTransport transport,
        IOptions<HsmOptions> cceOptions)
    {
        _transport = transport;
        _HsmOptions = cceOptions.Value;
    }

    public async Task<HsmSignDiagnosticResult> TrySignAsync(string message, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("El mensaje no puede ser vacío.", nameof(message));

        if (string.IsNullOrWhiteSpace(_HsmOptions.SigningKeyVarK))
            throw new InvalidOperationException("CceHsm:SigningKeyVarK no está configurado.");

        var digest = SHA256.HashData(Encoding.UTF8.GetBytes(message));
        var digestHex = Convert.ToHexString(digest);

        var requestData = AsymmetricSignatureCommand.BuildRequestData(_HsmOptions.SigningKeyVarK, digest);

        var tries = new List<HsmSignDiagnosticTry>();

        // Probamos solo con TU conexión actual ya validada:
        // 2 bytes big-endian + payload ASCII
        // cambiando únicamente el header
        var headers = new[]
        {
            string.Empty,
            "PINV",
            "0000"
        };

        foreach (var header in headers)
        {
            tries.Add(await ExecuteVariantAsync(header, requestData, cancellationToken));
        }

        return new HsmSignDiagnosticResult(
            Message: message,
            DigestHex: digestHex,
            Tries: tries);
    }

    private async Task<HsmSignDiagnosticTry> ExecuteVariantAsync(
        string header,
        string requestData,
        CancellationToken cancellationToken)
    {
        var command = new HsmCommandDefinition(
            Header: header,
            Command: AsymmetricSignatureCommand.CommandId,
            Data: requestData);

        var requestFrame = HsmRequestFrameBuilder.Build(command);
        var requestBodyAscii = (header ?? string.Empty) + AsymmetricSignatureCommand.CommandId + requestData;

        try
        {
            // Reutiliza TU transporte actual ya validado
            var rawResponse = await _transport.SendAsync(requestFrame, cancellationToken);

            // Reutiliza TU parser actual ya validado
            var parsedResponse = HsmResponseFrameParser.Parse(rawResponse);

            return new HsmSignDiagnosticTry(
                Variant: string.IsNullOrEmpty(header) ? "CurrentTransport-EmptyHeader" : $"CurrentTransport-{header}",
                RequestBodyAscii: requestBodyAscii,
                RequestFrameHex: Convert.ToHexString(requestFrame),
                ResponseBodyAscii: parsedResponse.PayloadAscii,
                ResponseFrameHex: Convert.ToHexString(rawResponse),
                Note: BuildNote(parsedResponse.PayloadAscii),
                Error: null);
        }
        catch (Exception ex)
        {
            return new HsmSignDiagnosticTry(
                Variant: string.IsNullOrEmpty(header) ? "CurrentTransport-EmptyHeader" : $"CurrentTransport-{header}",
                RequestBodyAscii: requestBodyAscii,
                RequestFrameHex: Convert.ToHexString(requestFrame),
                ResponseBodyAscii: null,
                ResponseFrameHex: null,
                Note: null,
                Error: ex.Message);
        }
    }

    private static string BuildNote(string responseBodyAscii)
    {
        if (string.IsNullOrWhiteSpace(responseBodyAscii))
            return "Respuesta vacía.";

        if (responseBodyAscii.Length >= 12)
            return "La respuesta tiene al menos el tamaño mínimo esperado para CommandId + ErrorCode.";

        if (responseBodyAscii == "11033267")
            return "Respuesta corta: no cumple el formato oficial esperado de 1103 (4H + 8H + ...).";

        return "Respuesta corta o formato no oficial.";
    }
}