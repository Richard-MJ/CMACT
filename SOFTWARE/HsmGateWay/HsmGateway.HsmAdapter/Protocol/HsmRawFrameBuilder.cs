using System.Buffers.Binary;
using System.Text;

namespace HsmGateway.HsmAdapter.Protocol;

public static class HsmRawFrameBuilder
{
    public static byte[] BuildOfficialD6(string header, string bodyAscii)
    {
        header ??= string.Empty;
        bodyAscii ??= string.Empty;

        var payload = header + bodyAscii;
        var len = payload.Length.ToString("D6");
        var full = len + payload;

        return Encoding.ASCII.GetBytes(full);
    }

    public static byte[] BuildLegacyU16(string header, string bodyAscii)
    {
        header ??= string.Empty;
        bodyAscii ??= string.Empty;

        var payloadAscii = header + bodyAscii;
        var payloadBytes = Encoding.ASCII.GetBytes(payloadAscii);

        if (payloadBytes.Length > ushort.MaxValue)
            throw new InvalidOperationException("Payload demasiado largo para framing de 2 bytes.");

        var frame = new byte[2 + payloadBytes.Length];
        BinaryPrimitives.WriteUInt16BigEndian(frame.AsSpan(0, 2), (ushort)payloadBytes.Length);
        payloadBytes.CopyTo(frame, 2);

        return frame;
    }
}