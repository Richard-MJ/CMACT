namespace HsmGateway.Application.Abstractions.Services;

public interface IHsmTransport
{
    Task<byte[]> SendAsync(byte[] request, CancellationToken cancellationToken);
}