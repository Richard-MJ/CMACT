using System.Net.Sockets;
using System.Buffers.Binary;
using Microsoft.Extensions.Options;
using HsmGateway.HsmAdapter.Configuration;
using HsmGateway.Application.Abstractions.Services;

namespace HsmGateway.HsmAdapter.Transport;

public sealed class TcpHsmTransport : IHsmTransport
{
    private readonly HsmOptions _options;

    public TcpHsmTransport(IOptions<HsmOptions> options)
    {
        _options = options.Value;
    }

    public async Task<byte[]> SendAsync(byte[] request, CancellationToken cancellationToken)
    {
        using var client = new TcpClient();

        using var connectCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        connectCts.CancelAfter(_options.ConnectTimeoutMs);

        await client.ConnectAsync(_options.Host, _options.Port, connectCts.Token);

        client.ReceiveTimeout = _options.ReadTimeoutMs;
        client.SendTimeout = _options.ReadTimeoutMs;

        await using var stream = client.GetStream();

        await stream.WriteAsync(request, cancellationToken);
        await stream.FlushAsync(cancellationToken);

        var lengthBuffer = await ReadExactAsync(stream, 2, cancellationToken);
        ushort payloadLength = BinaryPrimitives.ReadUInt16BigEndian(lengthBuffer);

        if (payloadLength == 0)
            throw new InvalidOperationException("El HSM respondió con longitud 0.");

        var payloadBuffer = await ReadExactAsync(stream, payloadLength, cancellationToken);

        var fullResponse = new byte[2 + payloadBuffer.Length];
        Buffer.BlockCopy(lengthBuffer, 0, fullResponse, 0, 2);
        Buffer.BlockCopy(payloadBuffer, 0, fullResponse, 2, payloadBuffer.Length);

        return fullResponse;
    }

    private static async Task<byte[]> ReadExactAsync(NetworkStream stream, int length, CancellationToken cancellationToken)
    {
        var buffer = new byte[length];
        var offset = 0;

        while (offset < length)
        {
            var read = await stream.ReadAsync(buffer.AsMemory(offset, length - offset), cancellationToken);

            if (read == 0)
                throw new IOException("El HSM cerró la conexión antes de completar la respuesta.");

            offset += read;
        }

        return buffer;
    }
}