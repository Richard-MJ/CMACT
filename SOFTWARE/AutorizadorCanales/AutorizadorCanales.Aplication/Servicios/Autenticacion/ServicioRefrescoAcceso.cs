using AutorizadorCanales.Aplication.Features.Autenticacion.Commands;
using AutorizadorCanales.Aplication.Features.Common;
using AutorizadorCanales.Aplication.Servicios;
using AutorizadorCanales.Aplication.Servicios.Autenticacion;
using AutorizadorCanales.Contracts.SG.Response;
using AutorizadorCanales.Core.Constantes;
using AutorizadorCanales.Core.Utilitarios;
using AutorizadorCanales.Domain.Entidades;
using AutorizadorCanales.Domain.Entidades.CF;
using AutorizadorCanales.Domain.Entidades.CL;
using AutorizadorCanales.Domain.Entidades.SG;
using AutorizadorCanales.Domain.Entidades.TJ;
using AutorizadorCanales.Domain.Repositorios;
using AutorizadorCanales.Excepciones;
using AutorizadorCanales.Logging.Interfaz;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace AutorizadorCanales.Infrastructure.Servicios;

public class ServicioRefrescoAcceso : ServicioBase<ServicioRefrescoAcceso>, IServicioRefrescoAcceso
{
    private readonly IRepositorioLectura _repositorioLectura;
    private readonly IRepositorioEscritura _repositorioEscritura;
    private readonly IServicioGeneradorToken _servicioGeneradorToken;

    public ServicioRefrescoAcceso(
        IRepositorioLectura repositorioLectura,
        IRepositorioEscritura repositorioEscritura,
        IServicioGeneradorToken servicioGeneradorToken,
        IContexto contexto,
        IBitacora<ServicioRefrescoAcceso> bitacora) : base(contexto, bitacora)
    {
        _repositorioLectura = repositorioLectura;
        _repositorioEscritura = repositorioEscritura;
        _servicioGeneradorToken = servicioGeneradorToken;
    }

    public async Task<JsonObject> RefrescarTokenCliente(RefrescarTokenCommand datos)
    {
        var tokenRefrescoActivo = await ObtenerTokenActivo(datos.RefreshToken);
        var autenticacion = _servicioGeneradorToken.DeserializarToken(tokenRefrescoActivo.TicketProtegido);

        var (idAudiencia, idSesion, idVisual, indicadorCanal) = ObtenerVariablesToken(autenticacion);

        if (datos.Audiencia.AplicaInactividad)
        {
            if (TokenConInactividadMayorALaPermitida(autenticacion.Principal))
            {
                await _servicioGeneradorToken.AnularTokensRefresco(idVisual, idAudiencia);
                throw new ExcepcionAUsuario("06", "No se permite el token refresco para este sistema");
            }
        }

        var numeroTarjeta = Utilitarios.ObtenerValorClaim(autenticacion.Principal.Claims, "sub");

        if (!await ValidarClienteParaAcceso(numeroTarjeta, idAudiencia, idVisual))
        {
            await _servicioGeneradorToken.AnularTokensRefresco(idVisual, idAudiencia);
            throw new ExcepcionAUsuario("06", "No se permite el token refresco para este sistema");
        }

        var (tokenAcceso, tokenGuid) = _servicioGeneradorToken.GenerarToken(
           sistemaCliente: datos.Audiencia,
           roles: Utilitarios.ObtenerRolesFinales(),
           claimsToken: autenticacion.Principal.Claims.ToList()
       );

        var tokenRefresco = await _servicioGeneradorToken.GenerarTokenRefresco
            (new(datos.Audiencia.IdAudiencia, tokenRefrescoActivo.IdClienteApi, idVisual, tokenAcceso, Contexto.IndicadorCanal, TokenRefresco.TIPO_AUTENTICACION_REFRESH));

        var dispositivoAutorizado = !string.IsNullOrEmpty(Utilitarios.ObtenerValorClaim(autenticacion.Principal.Claims, "confirmacion_sms"));

        var tiempoMaximoInactividad = await _repositorioLectura.ObtenerPorCodigoAsync<ParametroCanalElectronico>
            (ParametroCanalElectronico.TIEMPO_INACTIVIDAD, Moneda.SOLES, indicadorCanal, (byte)0);

        var autenticacionResponse = new AutenticacionResponse(
        tokenAcceso,
            AutenticacionConstante.TIPO_BEARER,
            (int)TimeSpan.FromMinutes(_servicioGeneradorToken.ObtenerMinutosVidaToken()).TotalSeconds,
            (int)tiempoMaximoInactividad.ValorParametro,
            tokenRefresco,
            datos.Audiencia.IdAudiencia,
            dispositivoAutorizado.ToString().ToLower(),
            idVisual,
            idVisual,
            idSesion,
            Contexto.FechaSistema.ToUniversalTime().ToString(),
            Contexto.FechaSistema.AddMinutes(_servicioGeneradorToken.ObtenerMinutosVidaToken()).ToUniversalTime().ToString());

        return JsonNode.Parse
            (JsonSerializer.Serialize(autenticacionResponse))!.AsObject();
    }

    private (string, string, string, string) ObtenerVariablesToken(AuthenticationTicket autenticacion)
    {
        var idUsuarioLogin = Utilitarios.ObtenerValorClaim(autenticacion.Principal.Claims, ClaimTypes.Name);
        var idAudiencia = Utilitarios.ObtenerValorClaim(autenticacion.Principal.Claims, "aud");
        var idSesion = Utilitarios.ObtenerValorClaim(autenticacion.Principal.Claims, "x:idSesion");
        var idVisual = Utilitarios.ObtenerValorClaim(autenticacion.Principal.Claims, "x:idVisual");
        var indicadorCanal = Utilitarios.ObtenerValorClaim(autenticacion.Principal.Claims, "x:canal");

        var operacion = new
        {
            accion = 19,
            grant_type = "refresh_token",
            idLogin = idSesion,
            idUsuarioLogin = idUsuarioLogin,
            client_id = idAudiencia
        };

        Bitacora.Info(JsonSerializer.Serialize(operacion));

        return new(idAudiencia, idSesion, idVisual, indicadorCanal);
    }

    private async Task<TokenRefresco> ObtenerTokenActivo(string tokenRefresco)
    {
        var tokenHashed = tokenRefresco.HashString();
        var tokenActivo = await _repositorioEscritura.ObtenerPrimeroPor<TokenRefresco>
            (t => t.IdSecreto == tokenHashed && t.FechaExpiracion > Contexto.FechaSistema);
        if (tokenActivo == null)
            throw new ExcepcionAUsuario("06", "Token de refresco no válido");
        return tokenActivo;
    }

    public async Task<bool> ValidarClienteParaAcceso(string numeroTarjeta, string idAudiencia, string idVisual)
    {
        var calendarioSistema = await _repositorioLectura.ObtenerPorCodigoAsync<Calendario>
            (EntidadEmpresa.EMPRESA, Contexto.CodigoAgencia, Sistema.CUENTA_CORRIENTE);

        var tarjeta = await _repositorioLectura.ObtenerPorCodigoAsync<Tarjeta>(decimal.Parse(numeroTarjeta));

        if (!tarjeta.Activa() || tarjeta.TarjetaVencida(calendarioSistema.FechaHoraSistema))
            return false;

        var tokensRefresco = await _repositorioLectura.ObtenerPorExpresionConLimiteAsync<TokenRefresco>
            (t => t.IdSistemaCliente == idAudiencia && t.ClienteApi.IdVisual == idVisual && t.IndicadorEstado == EstadoEntidad.ACTIVO);

        if (tokensRefresco.Count == 0)
            return false;

        var tokenRefresco = tokensRefresco.OrderBy(x => x.FechaEmision).FirstOrDefault();

        return !Utilitarios.FechaCambioClaveMayorAFechaEmisionTokenRefresco
            (tarjeta.FechaUltimoCambioPassHomebanking, tokenRefresco!.FechaEmision, calendarioSistema.FechaHoraSistema);
    }

    /// <summary>
    /// verifica si el token aplica inactividad mayor a la permitida
    /// </summary>
    /// <param name="claimsPrincipal">Claims</param>
    /// <returns>Indicador válido</returns>
    private bool TokenConInactividadMayorALaPermitida(ClaimsPrincipal claimsPrincipal)
    {
        var reclamaciones = new ClaimsIdentity(claimsPrincipal.Claims);
        var fechaEmisionTokenAcceso = reclamaciones.FindFirst("fechaEmision")!.Value;
        var minutosTiempoVidaTexto = _servicioGeneradorToken.ObtenerMinutosVidaToken();
        var fechaActual = DateTime.UtcNow;
        var diferencia = fechaActual - Utilitarios.ConvertirFechaUnixAFecha(Convert.ToInt64(fechaEmisionTokenAcceso));

        return diferencia.TotalMinutes > minutosTiempoVidaTexto;
    }
}
