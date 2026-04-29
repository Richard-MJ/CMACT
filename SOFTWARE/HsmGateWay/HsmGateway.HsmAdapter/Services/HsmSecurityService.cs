using HsmGateway.Application.Abstractions.Services;
using HsmGateway.Application.Models.SignMessage;
using HsmGateway.HsmAdapter.OfficialCommands;
using HsmGateway.HsmAdapter.Configuration;
using HsmGateway.HsmAdapter.Protocol;
using HsmGateway.Application.Models;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;


namespace HsmGateway.HsmAdapter.Services;

public sealed class HsmSecurityService : IHsmSecurityService
{
    private readonly IHsmCommandExecutor _executor;
    private readonly HsmOptions _options;

    public HsmSecurityService(
        IHsmCommandExecutor executor,
        IOptions<HsmOptions> options)
    {
        _executor = executor;
        _options = options.Value;
    }

    public async Task<HsmSignedMessage> SignAsync(HsmSignInput input, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(input.HeaderBase64Url))
            throw new ArgumentException("HeaderBase64Url es obligatorio.", nameof(input));

        if (string.IsNullOrWhiteSpace(input.PayloadBase64Url))
            throw new ArgumentException("PayloadBase64Url es obligatorio.", nameof(input));

        var signingInput = $"{input.HeaderBase64Url}.{input.PayloadBase64Url}";
        var digest = SHA256.HashData(Encoding.UTF8.GetBytes(signingInput));

        var requestData = AsymmetricSignatureCommand.BuildRequestData(_options.SigningKeyVarK, digest);

        var response = await _executor.ExecuteAsync(
            new HsmCommandDefinition(
                Header: string.Empty,
                Command: AsymmetricSignatureCommand.CommandId,
                Data: requestData),
            cancellationToken);

        var signatureHex = AsymmetricSignatureCommand.ParseResponse(response.PayloadAscii);
        var signatureBytes = Convert.FromHexString(signatureHex);
        var signatureBase64Url = Base64Url.Encode(signatureBytes);

        return new HsmSignedMessage(
            Payload: input.PayloadBase64Url,
            Protected: input.HeaderBase64Url,
            Signature: signatureBase64Url);
    }

    public async Task<HsmVerifyResult> VerifyAsync(HsmVerifyInput input, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(input.Protected))
            throw new ArgumentException("Protected es obligatorio.", nameof(input));

        if (string.IsNullOrWhiteSpace(input.Payload))
            throw new ArgumentException("Payload es obligatorio.", nameof(input));

        if (string.IsNullOrWhiteSpace(input.Signature))
            throw new ArgumentException("Signature es obligatoria.", nameof(input));

        var verificationKeyVarK = ResolveVerificationKeyVarKFromEnvironment();

        var signingInput = $"{input.Protected}.{input.Payload}";
        var digest = SHA256.HashData(Encoding.UTF8.GetBytes(signingInput));
        var signatureBytes = Base64Url.DecodeToBytes(input.Signature);

        var requestData = AsymmetricSignatureVerificationCommand.BuildRequestData(
            verificationKeyVarK,
            digest,
            signatureBytes);

        var response = await _executor.ExecuteAsync(
            new HsmCommandDefinition(
                Header: string.Empty,
                Command: AsymmetricSignatureVerificationCommand.CommandId,
                Data: requestData),
            cancellationToken);

        var isValid = AsymmetricSignatureVerificationCommand.ParseResponse(response.PayloadAscii);

        if (!isValid)
            return new HsmVerifyResult(false, string.Empty);

        var decodedPayloadJson = Base64Url.DecodeToString(input.Payload);
        return new HsmVerifyResult(true, decodedPayloadJson);
    }

    private string ResolveVerificationKeyVarKFromEnvironment()
    {
        if (string.IsNullOrWhiteSpace(_options.VerificationKeyVarKEnvVar))
            throw new InvalidOperationException("VerificationKeyVarKEnvVar no está configurado.");

        var value = Environment.GetEnvironmentVariable(_options.VerificationKeyVarKEnvVar);

        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException(
                $"No se encontró la variable de entorno '{_options.VerificationKeyVarKEnvVar}'.");

        return value.Trim();
    }
}