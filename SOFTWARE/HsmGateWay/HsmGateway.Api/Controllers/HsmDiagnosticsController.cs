using Microsoft.AspNetCore.Mvc;
using HsmGateway.Api.Contracts;
using HsmGateway.Application.Abstractions.Services;

namespace HsmGateway.Api.Controllers;

[ApiController]
[Route("api/v1/hsm-diagnostics")]
public sealed class HsmDiagnosticsController : ControllerBase
{
    private readonly IHsmSignatureDiagnosticService _signatureDiagnosticService;

    public HsmDiagnosticsController(IHsmSignatureDiagnosticService signatureDiagnosticService)
    {
        _signatureDiagnosticService = signatureDiagnosticService;
    }

    [HttpPost("sign-1103")]
    [ProducesResponseType(typeof(HsmSignDiagnosticResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<HsmSignDiagnosticResponse>> Sign1103(
        [FromBody] HsmSignDiagnosticRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _signatureDiagnosticService.TrySignAsync(request.Message, cancellationToken);

        return Ok(new HsmSignDiagnosticResponse
        {
            Message = result.Message,
            DigestHex = result.DigestHex,
            Tries = result.Tries.Select(x => new HsmSignDiagnosticTryResponse
            {
                Variant = x.Variant,
                RequestBodyAscii = x.RequestBodyAscii,
                RequestFrameHex = x.RequestFrameHex,
                ResponseBodyAscii = x.ResponseBodyAscii,
                ResponseFrameHex = x.ResponseFrameHex,
                Note = x.Note,
                Error = x.Error
            }).ToList()
        });
    }
}