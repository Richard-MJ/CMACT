using AutorizadorCanales.Api.Helpers;
using AutorizadorCanales.Aplication.Features.Autenticacion.Commands;
using AutorizadorCanales.Aplication.Features.Autenticacion.Factories;
using AutorizadorCanales.Aplication.Features.Autenticacion.Queries;
using AutorizadorCanales.Aplication.Features.Sesion.Commands;
using AutorizadorCanales.Contracts.SG.Autenticacion;
using AutorizadorCanales.Contracts.SG.Response;
using AutorizadorCanales.Logging.Interfaz;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json.Nodes;

namespace AutorizadorCanales.Api.Controllers;

[Route("oauth2/[controller]")]
[ApiController]
public class TokenController : BaseController<TokenController>
{
    #region Propiedades
    private readonly IAutenticacionCommandFactory _commandFactory;
    #endregion

    #region Constructor

    public TokenController(IContexto contexto,
                           ISender mediator,
                           IAutenticacionCommandFactory commandFactory,
                           IBitacora<TokenController> bitacora)
        : base(contexto, mediator, bitacora)
    {
        _commandFactory = commandFactory;
    }

    #endregion

    #region Rutas

    /// <summary>
    /// Petición POST que devuelve un token de acceso
    /// </summary>
    /// <param name="request">Datos necesarios para la autenticación</param>
    /// <returns></returns>
    [HttpPost()]
    [Consumes("application/x-www-form-urlencoded")]
    [ProducesResponseType(typeof(JsonObject), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Autenticacion([FromForm] AutenticacionRequest request)
    {
        return await RealizarOperacionBitacorizada
            (request, async (sistema, idTrama) =>
            {
                var command = _commandFactory.CrearComando
                    (request, sistema, idTrama, HttpContext.Request.Headers.UserAgent.ToString(), IpHelper.ObtenerIp(HttpContext));
                var result = await _mediator.Send(command);
                return result;
            });
    }

    /// <summary>
    /// Petición POST verifica el acceso
    /// </summary>
    /// <param name="request">Datos necesarios para la verificación</param>
    /// <returns></returns>
    [HttpPost("verificar-autorizacion")]
    [Consumes("application/x-www-form-urlencoded")]
    [ProducesResponseType(typeof(VerificacionAutenticacionResponse), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ClientPolicy")]
    public async Task<IActionResult> VerificarAutorizacion([FromForm] VerificacionAutenticacionRequest request)
    {
        return await RealizarOperacionNoBitacorizadaConAudiencia
            (async (sistema) =>
            {
                var result = await _mediator.Send
                    (new VerificarAutenticacionCommand(
                        request.idVerificacion,
                        request.codigoAutorizacion,
                        request.idVisual,
                        request.newGuid,
                        User.Claims.ToList(),
                        sistema));
                return result;
            });
    }

    /// <summary>
    /// Peticón GET que valida el token de sesión
    /// </summary>
    /// <returns></returns>
    [HttpGet("validar-token")]
    [Authorize(Policy = "ClientPolicy")]
    public async Task<IActionResult> ValidarToken()
    {
        return await RealizarOperacionNoBitacorizada(
            async () =>
            {
                await _mediator.Send(new ValidarTokenQuery());
                return new { Mensaje = "Token válido" };
            });
    }

    /// <summary>
    /// Actualiza la sesión con el id de conexión de SignalR
    /// </summary>
    /// <param name="command">Comando para actualizar la sesión</param>
    /// <returns></returns>
    [HttpPut("actualizar-sesion")]
    [Consumes("application/json")]
    [Authorize(Policy = "ClientPolicy")]
    public async Task<IActionResult> ActualizarSesion([FromBody] ActualizarSesionCommand command)
    {
        return await RealizarOperacionNoBitacorizadaConAudiencia(
            async (audiencia) =>
            {
                await _mediator.Send(command);
                return new { Mensaje = "Sesión actualizada" };
            });
    }

    /// <summary>
    /// Cierra la sesión
    /// </summary>
    /// <param name="command">Comando para cerrar sesión</param>
    /// <returns></returns>
    [HttpPost("cerrar-sesion")]
    [Consumes("application/json")]
    public async Task<IActionResult> CerrarSesion([FromBody] CerrarSesionCommand command)
    {
        return await RealizarOperacionNoBitacorizada
            (async () =>
            {
                await _mediator.Send(command);
                return new { Mensaje = "Sesión cerrada" };
            });
    }

    /// <summary>
    /// Petición POST que devuelve un token de acceso
    /// </summary>
    /// <param name="request">Datos necesarios para la autenticación de apertura productos</param>
    /// <returns></returns>
    [HttpPost("apertura-producto")]
    [Consumes("application/x-www-form-urlencoded")]
    [ProducesResponseType(typeof(JsonObject), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AutenticacionAperturaProductos([FromForm] AutenticacionRequest request)
    {
        return await RealizarOperacionBitacorizada
            (request, async (sistema, idTrama) =>
            {
                var command = _commandFactory.CrearComando
                    (request, sistema, idTrama, HttpContext.Request.Headers.UserAgent.ToString(), IpHelper.ObtenerIp(HttpContext));
                var result = await _mediator.Send(command);
                return result;
            });
    }
    #endregion
}
