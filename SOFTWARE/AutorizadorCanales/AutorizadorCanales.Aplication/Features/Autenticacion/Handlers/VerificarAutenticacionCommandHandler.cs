using AutorizadorCanales.Aplication.Common.ServicioSeguridad;
using AutorizadorCanales.Aplication.Common.ServicioSeguridad.DTOs;
using AutorizadorCanales.Aplication.Extensiones;
using AutorizadorCanales.Aplication.Features.Autenticacion.Commands;
using AutorizadorCanales.Aplication.Features.Common;
using AutorizadorCanales.Aplication.Servicios.Autenticacion;
using AutorizadorCanales.Aplication.Servicios.Sesion;
using AutorizadorCanales.Contracts.SG.Response;
using AutorizadorCanales.Core.Constantes;
using AutorizadorCanales.Domain.Entidades;
using AutorizadorCanales.Domain.Entidades.CF;
using AutorizadorCanales.Domain.Entidades.CL;
using AutorizadorCanales.Domain.Entidades.SG;
using AutorizadorCanales.Domain.Repositorios;
using AutorizadorCanales.Excepciones;
using AutorizadorCanales.Logging.Interfaz;
using MediatR;
using System.Security.Claims;

namespace AutorizadorCanales.Aplication.Features.Autenticacion.Handlers;

public class VerificarAutenticacionCommandHandler : IRequestHandler<VerificarAutenticacionCommand, VerificacionAutenticacionResponse>
{
    private readonly IServicioApiSeguridad _servicioApiSeguridad;
    private readonly IRepositorioLectura _repositorioLectura;
    private readonly IContexto _contexto;
    private readonly IServicioGeneradorToken _servicioGeneradorToken;
    private readonly IServicioSesionCanalElectronico _servicioSesionCanalElectronico;
    private readonly IServicioAlertaInicioSesion _servicioAlertaInicioSesion;

    private const string CODIGO_AUTORIZACION_INVALIDO = "07";

    public VerificarAutenticacionCommandHandler(
        IServicioApiSeguridad servicioSeguridad,
        IServicioGeneradorToken servicioGeneradorToken,
        IContexto contexto,
        IRepositorioLectura repositorioLectura,
        IServicioSesionCanalElectronico servicioSesionCanalElectronico,
        IServicioAlertaInicioSesion servicioAlertaInicioSesion)
    {
        _servicioApiSeguridad = servicioSeguridad;
        _servicioGeneradorToken = servicioGeneradorToken;
        _contexto = contexto;
        _repositorioLectura = repositorioLectura;
        _servicioSesionCanalElectronico = servicioSesionCanalElectronico;
        _servicioAlertaInicioSesion = servicioAlertaInicioSesion;
    }

    /// <summary>
    /// Maneja el comando de verificar autenticación
    /// </summary>
    /// <param name="command">Datos del comando</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Retorna la respuesta de verificación de identidad</returns>
    public async Task<VerificacionAutenticacionResponse> Handle(VerificarAutenticacionCommand command, CancellationToken cancellationToken)
    {
        RespuestaAutorizacionCanalElectronico autorizacionCanalElectronico;
        try
        {
            autorizacionCanalElectronico = await _servicioApiSeguridad.ValidarCodigoAutorizacionSmsLogin
                (command.AAutorizacionCanalElectronico());
        }
        catch (ExcepcionAUsuario ex)
        {
            if (ex.CodigoError == CODIGO_AUTORIZACION_INVALIDO)
                await _servicioAlertaInicioSesion.RegistrarAlertaInicioSesion(EstadoEntidad.NO, _contexto.IdUsuarioAutenticado, command.NewGuid);

            throw new ExcepcionAUsuario(ex.CodigoError, ex.Message);
        }

        command.Claims.Add(new Claim("confirmacion_sms", "true"));

        var idVisual = command.Claims.First(t => t.Type == "x:idVisual").Value.ToString();
        var clientesApi = await _repositorioLectura.ObtenerPorExpresionConLimiteAsync<ClienteApi>
            (c => c.IdVisual == idVisual && c.IdSistemaCliente == command.Audiencia.IdAudiencia && c.IndicadorEstado == ClienteApi.AFILIADO);
        var clienteApi = clientesApi.FirstOrDefault()
            ?? throw new ExcepcionAUsuario("06", "Cliente final no valido.");

        var (tokenAcceso, tokenGuid) = _servicioGeneradorToken.GenerarToken(
            sistemaCliente: command.Audiencia,
            roles: Utilitarios.ObtenerRolesFinales(),
            claimsToken: command.Claims
        );

        var tokenRefresco = await _servicioGeneradorToken.GenerarTokenRefresco
            (new(command.Audiencia.IdAudiencia, clienteApi.Id, idVisual, tokenAcceso, _contexto.IndicadorCanal, TokenRefresco.TIPO_AUTENTICACION_PASSWORD));

        var dispositivoCanalElectronico = await _servicioAlertaInicioSesion.RegistrarAlertaInicioSesion
            (EstadoEntidad.SI, _contexto.IdUsuarioAutenticado, command.NewGuid, true, autorizacionCanalElectronico.NumeroMovimiento);

        await _servicioSesionCanalElectronico.CrearSesionCanalElectronico(false, dispositivoCanalElectronico, tokenGuid);

        var tiempoMaximoInactividad = await _repositorioLectura.ObtenerPorCodigoAsync<ParametroCanalElectronico>
            (ParametroCanalElectronico.TIEMPO_INACTIVIDAD, Moneda.SOLES, _contexto.IndicadorCanal, _contexto.IndicadorSubCanal);

        return new VerificacionAutenticacionResponse(
            AutenticacionConstante.TIPO_BEARER,
            tokenRefresco,
            tokenAcceso,
            TimeSpan.FromMinutes(_servicioGeneradorToken.ObtenerMinutosVidaToken()).TotalSeconds.ToString(),
            (int)tiempoMaximoInactividad.ValorParametro,
            _contexto.FechaSistema.ToUniversalTime().ToString(),
            _contexto.FechaSistema.AddMinutes(_servicioGeneradorToken.ObtenerMinutosVidaToken()).ToUniversalTime().ToString(),
            autorizacionCanalElectronico.IdNavegador);
    }
}
