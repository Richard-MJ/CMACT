namespace HsmGateway.HsmAdapter.Protocol;

/// <summary>
/// Clase para formatear los campos
/// </summary>
public static class HsmFieldFormatter
{
    public static string To2N(int value) => value.ToString("D2");
    public static string To4N(int value) => value.ToString("D4");
    public static string To6N(int value) => value.ToString("D6");
    public static string ToHex(byte[] bytes) => Convert.ToHexString(bytes);
    public static int HexCharLengthFromByteLength(int byteLength) => byteLength * 2;
}