using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using HsmGateway.Api.Contracts;
using HsmGateway.Application.Models.SignMessage;
using HsmGateway.Application.Abstractions.Services;

namespace HsmGateway.Api.Controllers;

[ApiController]
[Route("api/v1/hsm")]
public sealed class HsmController : ControllerBase
{
    private readonly IHsmConnectionProbe _connectionProbe;
    private readonly IHsmSecurityService _hsmSecurityService;

    public HsmController(
        IHsmConnectionProbe connectionProbe, 
        IHsmSecurityService hsmSecurityService)
    {
        _connectionProbe = connectionProbe;
        _hsmSecurityService = hsmSecurityService;
    }

    [HttpGet("connection")]
    [ProducesResponseType(typeof(HsmConnectionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<HsmConnectionResponse>> GetConnectionStatus(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _connectionProbe.CheckAsync(cancellationToken);

            return Ok(new HsmConnectionResponse
            {
                IsConnected = result.IsConnected,
                ProfileName = result.ProfileName,
                Endpoint = result.Endpoint,
                LatencyMs = result.LatencyMs,
                Summary = result.Summary,
                ResponseAscii = result.ResponseAscii,
                ResponseHex = result.ResponseHex
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new
            {
                isConnected = false,
                message = "No se pudo establecer comunicación con el HSM.",
                detail = ex.Message
            });
        }
    }

    [HttpPost("sign")]
    [ProducesResponseType(typeof(HsmSignedResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<HsmSignedResponse>> Sign(
        [FromBody] HsmSignRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hsmSecurityService.SignAsync(
            new HsmSignInput(request.Header, request.Contenido),
            cancellationToken);

        return Ok(new HsmSignedResponse
        {
            payload = result.Payload,
            @protected = result.Protected,
            signature = result.Signature
        });
    }

    [HttpPost("verify")]
    [ProducesResponseType(typeof(HsmVerifyResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HsmVerifyResponse>> Verify(
        [FromBody] HsmVerifyRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hsmSecurityService.VerifyAsync(
            new HsmVerifyInput(request.payload, request.@protected, request.signature),
            cancellationToken);

        if (!result.IsValid)
        {
            return BadRequest(new
            {
                isValid = false,
                message = "La firma no es válida."
            });
        }

        using var json = JsonDocument.Parse(result.DecodedPayloadJson);

        return Ok(new HsmVerifyResponse
        {
            IsValid = true,
            PayloadObject = json.RootElement.Clone()
        });
    }
}