using AutorizadorCanales.Aplication.Common.JwtGenerator;
using AutorizadorCanales.Infrastructure.Configuracion;
using AutorizadorCanales.Logging.Interfaz;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AutorizadorCanales.Infrastructure.Servicios
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly ConfiguracionAudiencia _configuracionAudiencia;
        private readonly IContexto _contexto;

        public JwtTokenGenerator(ConfiguracionAudiencia configuracionAudiencia, IContexto contexto)
        {
            _configuracionAudiencia = configuracionAudiencia;
            _contexto = contexto;
        }

        /// <summary>
        /// Método que obtiene los minutos de vida
        /// </summary>
        /// <returns>Minutos de vida de token</returns>
        public int GetLifeTimeToken()
        {
            return _configuracionAudiencia.MinutosVida;
        }

        /// <summary>
        /// Método que obtiene los minutos de vida del token de apertura
        /// </summary>
        /// <returns></returns>
        public int GetLifeTimeTokenApertura() => _configuracionAudiencia.MinutosVidaOB;

        public (string, string) GenerateJwtToken(List<Claim> claims, string secretKey, string issuer, string audience, bool esAperturaProducto)
        {
            var tokenGuid = Guid.NewGuid().ToString();
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, tokenGuid));

            var keyByteArray = Convert.FromBase64String(secretKey);
            var symmetricSecurityKey = new SymmetricSecurityKey(keyByteArray);
            var signInCredentials = new SigningCredentials
                (symmetricSecurityKey, CreateSignatureAlgorithm(keyByteArray), CreateDigestAlgorithm(keyByteArray));

            var minutosVida = esAperturaProducto ? GetLifeTimeTokenApertura() : GetLifeTimeToken();

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: _contexto.FechaSistema.ToUniversalTime(),
                expires: _contexto.FechaSistema.ToUniversalTime().AddMinutes(minutosVida),
                signingCredentials: signInCredentials);

            return (new JwtSecurityTokenHandler().WriteToken(token), tokenGuid);
        }

        /// <summary>
        /// Método que serializa un token de acceso
        /// </summary>
        /// <param name="token">token de acceso</param>
        /// <returns>Token serializado</returns>
        public string SerializarToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var principal = new ClaimsPrincipal(new ClaimsIdentity(jwtToken.Claims));
            var authenticationTicket = new AuthenticationTicket(principal, "Bearer");
            return Convert.ToBase64String(TicketSerializer.Default.Serialize(authenticationTicket));
        }

        /// <summary>
        /// Obtiene el identificador del token de sesión
        /// </summary>
        /// <param name="token">Token de sesión</param>
        /// <returns></returns>
        public string ObtenerGuidToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            return jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value ?? "";
        }

        private string CreateSignatureAlgorithm(byte[] key)
        {
            return key.Length switch
            {
                32 => SecurityAlgorithms.HmacSha256Signature,
                48 => SecurityAlgorithms.HmacSha384Signature,
                64 => SecurityAlgorithms.HmacSha512Signature,
                _ => SecurityAlgorithms.HmacSha256Signature,
            };
        }

        private string CreateDigestAlgorithm(byte[] key)
        {
            return key.Length switch
            {
                32 => SecurityAlgorithms.Sha256Digest,
                48 => SecurityAlgorithms.Sha384Digest,
                64 => SecurityAlgorithms.Sha512Digest,
                _ => SecurityAlgorithms.Sha256Digest
            };
        }
    }
}
