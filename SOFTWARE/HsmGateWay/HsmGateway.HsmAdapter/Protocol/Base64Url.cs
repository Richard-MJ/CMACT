using System.Text;

namespace HsmGateway.HsmAdapter.Protocol;

public static class Base64Url
{
    public static string Encode(byte[] bytes)
    {
        return Convert.ToBase64String(bytes)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }

    public static byte[] DecodeToBytes(string input)
    {
        string base64 = input
            .Replace('-', '+')
            .Replace('_', '/');

        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }

    public static string DecodeToString(string input)
        => Encoding.UTF8.GetString(DecodeToBytes(input));
}