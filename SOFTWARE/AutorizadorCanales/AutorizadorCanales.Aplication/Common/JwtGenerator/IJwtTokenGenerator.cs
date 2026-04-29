using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace AutorizadorCanales.Aplication.Common.JwtGenerator;

public interface IJwtTokenGenerator
{
    int GetLifeTimeToken();
    /// <summary>
    /// Obtener los minutos de vida de token apertura
    /// </summary>
    /// <returns></returns>
    int GetLifeTimeTokenApertura();
    /// <summary>
    /// Obtiene el identificador del token de sesión
    /// </summary>
    /// <param name="token">Token de sesión</param>
    /// <returns></returns>
    string ObtenerGuidToken(string token);
    string SerializarToken(string token);
    (string, string) GenerateJwtToken(List<Claim> claims, string secretKey, string issuer, string audience, bool esAperturaProducto = false);
}
