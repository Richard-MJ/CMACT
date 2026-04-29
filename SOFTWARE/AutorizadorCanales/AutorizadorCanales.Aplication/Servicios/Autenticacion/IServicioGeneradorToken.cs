using AutorizadorCanales.Aplication.Features.Autenticacion.Commands;
using AutorizadorCanales.Aplication.Servicios.Autenticacion.DTOs;
using AutorizadorCanales.Contracts.SG.Response;
using AutorizadorCanales.Core.DTO;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace AutorizadorCanales.Aplication.Servicios.Autenticacion;

public interface IServicioGeneradorToken
{
    /// <summary>
    /// Método que genra el token de acceso
    /// </summary>
    /// <param name="autenticarCommand">Datos que contiene el comando</param>s
    /// <param name="usuario"></param>
    /// <param name="roles"></param>
    /// <param name="idSesion"></param>
    /// <param name="sistemaCliente"></param>
    /// <param name="claimsToken"></param>
    /// <returns></returns>
    (string, string) GenerarToken(
        string[] roles,
        AudienciaResponse sistemaCliente,
        AutenticarClienteCommand? autenticarCommand = null,
        UsuarioLogueadoDto? usuario = null,
        string? idSesion = null,
        List<Claim>? claimsToken = null);

    /// <summary>
    /// Método que genera el token de acceso para Apertura Web
    /// </summary>
    /// <param name="roles"></param>
    /// <param name="sistemaCliente"></param>
    /// <param name="autenticarCommand"></param>
    /// <param name="usuario"></param>
    /// <param name="idSesion"></param>
    /// <param name="claimsToken"></param>
    /// <returns></returns>
    (string, string) GenerarTokenAperturaProducto(
        string[] roles,
        AudienciaResponse sistemaCliente,
        AutenticarAperturaProductoCommand? autenticarCommand = null,
        UsuarioLogueadoDto? usuario = null,
        string? idSesion = null,
        List<Claim>? claimsToken = null);

    /// <summary>
    /// Método que genera el token de refresco
    /// </summary>
    /// <param name="datos">Datos necesarios para generar el token de refresco</param>
    /// <returns>Token de refresco</returns>
    Task<string> GenerarTokenRefresco(GenerarTokenRefrescoDto datos);

    /// <summary>
    /// Método que deserializa un token de acceso
    /// </summary>
    /// <param name="tokenSerializado">token serializado</param>
    /// <returns>token original</returns>
    AuthenticationTicket DeserializarToken(string tokenSerializado);

    /// <summary>
    /// Anular tokens de refresco
    /// </summary>
    /// <param name="idVisual">Id visual</param>
    /// <param name="idAudiencia">id de audiencia</param>
    /// <returns></returns>
    Task AnularTokensRefresco(string idVisual, string idAudiencia);

    (string, string) GenerarTokenKiosko(List<Claim> claims, DtoSesion datos, AudienciaResponse sistemaCliente, string idSesion);

    int ObtenerMinutosVidaToken();
    /// <summary>
    /// Método que obtiene los minutos de vida del token de apertura
    /// </summary>
    /// <returns></returns>
    int ObtenerMinutosVidaTokenApertura();
}
