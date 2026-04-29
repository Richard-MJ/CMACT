namespace HsmGateway.HsmAdapter.Protocol;

public sealed class HsmBodyReader
{
    private readonly string _body;
    private int _offset;

    public HsmBodyReader(string body)
    {
        _body = body;
    }

    public string Read(int length)
    {
        if (_offset + length > _body.Length)
            throw new InvalidOperationException(
                $"Respuesta HSM incompleta. Offset={_offset}, Length={length}, BodyLength={_body.Length}, Body='{_body}'");

        var value = _body.Substring(_offset, length);
        _offset += length;
        return value;
    }

    public int ReadNumeric(int digits)
    {
        var raw = Read(digits);

        if (!int.TryParse(raw, out var value))
            throw new InvalidOperationException($"No se pudo interpretar campo numérico '{raw}'.");

        return value;
    }

    public string ReadVarHByByteLength(int byteLength)
    {
        var charLength = HsmFieldFormatter.HexCharLengthFromByteLength(byteLength);
        return Read(charLength);
    }

    public string ReadToEnd()
    {
        if (_offset >= _body.Length)
            return string.Empty;

        var value = _body.Substring(_offset);
        _offset = _body.Length;
        return value;
    }
}