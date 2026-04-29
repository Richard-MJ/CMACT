using System.Text;
using System.Buffers.Binary;
using HsmGateway.Application.Models;

namespace HsmGateway.HsmAdapter.Protocol;

public static class HsmRequestFrameBuilder
{
    public static byte[] Build(HsmCommandDefinition command)
    {
        var header = command.Header ?? string.Empty;
        var data = command.Data ?? string.Empty;

        var payloadAscii = header + command.Command + data;
        var payloadBytes = Encoding.ASCII.GetBytes(payloadAscii);

        if (payloadBytes.Length > ushort.MaxValue)
            throw new InvalidOperationException("La longitud del payload excede ushort.MaxValue.");

        var frame = new byte[2 + payloadBytes.Length];
        BinaryPrimitives.WriteUInt16BigEndian(frame.AsSpan(0, 2), (ushort)payloadBytes.Length);
        payloadBytes.CopyTo(frame, 2);

        return frame;
    }
}