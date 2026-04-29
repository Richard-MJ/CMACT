
namespace HsmGateway.HsmAdapter.Protocol;

public sealed record HsmParsedFrame(
    int Length,
    string PayloadAscii,
    string PayloadHex);