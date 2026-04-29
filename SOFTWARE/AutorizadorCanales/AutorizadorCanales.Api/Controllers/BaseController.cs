using AutorizadorCanales.Aplication.Features.Audiencias.Queries;
using AutorizadorCanales.Aplication.Features.Calendarios.Queries;
using AutorizadorCanales.Aplication.Features.TramasProcesadas.Commands;
using AutorizadorCanales.Contracts.SG.Autenticacion;
using AutorizadorCanales.Contracts.SG.Response;
using AutorizadorCanales.Core.Constantes;
using AutorizadorCanales.Domain.Entidades.CF;
using AutorizadorCanales.Excepciones;
using AutorizadorCanales.Logging.Interfaz;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AutorizadorCanales.Api.Controllers;

public class BaseController<T> : ControllerBase where T : class
{
    public readonly ISender _mediator;
    public readonly IContexto _contexto;
    public readonly IBitacora<T> _bitacora;

    public BaseController(
        IContexto contexto,
        ISender mediator,
        IBitacora<T> bitacora)
    {
        _contexto = contexto;
        _mediator = mediator;
        _bitacora = bitacora;
    }

    /// <summary>
    /// Método que crea un registro en tramas procedadas
    /// </summary>
    /// <typeparam name="T">Tipo de respuesta esperada</typeparam>  
    /// <param name="request">Petición de entrada</param>
    /// <param name="operacion">Función a ejecutar</param>
    /// <returns></returns>
    protected async Task<IActionResult> RealizarOperacionBitacorizada<T>
        (AutenticacionRequest request, Func<AudienciaResponse, int, Task<T>> operacion)
    {
        int idTrama = 0;

        try
        {
            var fechaSistema = await _mediator.Send
                (new ObtenerCalendarioPorSistemaQuery(_contexto.CodigoAgencia, Sistema.CUENTA_CORRIENTE));
            var audiencia = await _mediator.Send
                (new ObtenerAudienciaPorIdQuery(request.client_id));

            idTrama = await _mediator.Send(new RegistrarDatosInicioSesionCommand(
                audiencia.IndicadorCanal,
                decimal.Parse(request.username ?? "0"),
                request.terminal?.PadLeft(TramaProcesadaConstante.LIMITE_ID_TERMINAL).Trim() ?? "",
                request.username?.PadRight(TramaProcesadaConstante.LIMITE_NUMERO_TARJETA).Trim() ?? "",
                fechaSistema.FechaHoraSistema));

            var resultado = await operacion(audiencia, idTrama);

            await _mediator.Send(new RegistrarRespuestaOperacionCommand
                (idTrama, "00", 0, "", _contexto.FechaHoraServidor));

            return Ok(resultado);
        }
        catch (ExcepcionAUsuario ex)
        {
            _bitacora.Error($"Error en operación: {ex.Message}");

            if (idTrama > 0)
            {
                await _mediator.Send(new RegistrarRespuestaOperacionCommand
                    (idTrama, ex.CodigoError, 0, "", _contexto.FechaHoraServidor));
            }
            return BadRequest(new { error = ex.CodigoError, error_description = ex.Message });
        }
        catch (Exception ex)
        {
            _bitacora.Fatal(ex.Message + ". " + ex.InnerException + " | Stack: " + ex.StackTrace);
            return StatusCode((int)HttpStatusCode.InternalServerError, new { error = "06", error_description = "Error al invocar servicio" });
        }
    }

    /// <summary>
    /// Método que no genera un registro en la trama Tramas Procesadas
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="operacion">función a realizar</param>
    /// <returns></returns>
    public async Task<IActionResult> RealizarOperacionNoBitacorizadaConAudiencia<T>
        (Func<AudienciaResponse, Task<T>> operacion)
    {
        try
        {
            var audiencia = await _mediator.Send
                (new ObtenerAudienciaPorIdQuery(_contexto.IdAudiencia));
            _bitacora.Info($"Iniciando petición no bitacorizada, con audiencia {audiencia.IndicadorCanal}");
            var resultado = await operacion(audiencia);
            return Ok(resultado);
        }
        catch (ExcepcionAUsuario ex)
        {
            _bitacora.Error($"Error en operación: {ex.Message}");
            return BadRequest(new { error = ex.CodigoError, error_description = ex.Message });
        }
        catch (Exception ex)
        {
            _bitacora.Fatal(ex.Message + ". " + ex.InnerException + " | Stack: " + ex.StackTrace);
            return StatusCode((int)HttpStatusCode.InternalServerError, new { error = "06", error_description = "Error al invocar servicio" });
        }
    }

    /// <summary>
    /// Método que no genera un registro en la trama Tramas Procesadas
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="operacion">función a realizar</param>
    /// <returns></returns>
    public async Task<IActionResult> RealizarOperacionNoBitacorizada<T>
        (Func<Task<T>> operacion)
    {
        try
        {
            var fechaSistema = await _mediator.Send
              (new ObtenerCalendarioPorSistemaQuery(Agencia.PRINCIPAL, Sistema.CUENTA_CORRIENTE));

            var resultado = await operacion();
            return Ok(resultado);
        }
        catch (ExcepcionAUsuario ex)
        {
            _bitacora.Error($"Error en operación: {ex.Message}");
            return BadRequest(new { error = ex.CodigoError, error_description = ex.Message });
        }
        catch (Exception ex)
        {
            _bitacora.Fatal(ex.Message + ". " + ex.InnerException + " | Stack: " + ex.StackTrace);
            return StatusCode((int)HttpStatusCode.InternalServerError, new { error = "06", error_description = "Error al invocar servicio" });
        }
    }
}
