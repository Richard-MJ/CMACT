using System.Text.Json;

namespace HsmGateway.Api.Contracts
{
    public class HsmVerifyResponse
    {
        public bool IsValid { get; init; }
        public JsonElement? PayloadObject { get; init; }
    }
}
