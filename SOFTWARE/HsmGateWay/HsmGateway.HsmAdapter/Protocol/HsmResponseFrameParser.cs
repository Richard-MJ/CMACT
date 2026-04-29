using System.Text;
using System.Buffers.Binary;
using HsmGateway.Application.Models;

namespace HsmGateway.HsmAdapter.Protocol;

public static class HsmResponseFrameParser
{
    public static HsmParsedResponse Parse(byte[] rawResponse)
    {
        if (rawResponse is null || rawResponse.Length < 2)
        {
            throw new InvalidOperationException("La respuesta del HSM no contiene longitud.");
        }

        ushort length = BinaryPrimitives.ReadUInt16BigEndian(rawResponse.AsSpan(0, 2));

        if (rawResponse.Length < 2 + length)
        {
            throw new InvalidOperationException("La respuesta del HSM está incompleta.");
        }

        var payload = rawResponse.AsSpan(2, length).ToArray();
        var ascii = Encoding.ASCII.GetString(payload);
        var hex = BitConverter.ToString(payload).Replace("-", string.Empty);

        return new HsmParsedResponse(
            Length: length,
            PayloadAscii: ascii,
            PayloadHex: hex,
            Summary: "Respuesta recibida correctamente.");
    }
}