using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.CL
{
    public class SesionUsuarioDTO
    {
        /// <summary>
        /// ID de login
        /// </summary>
        [SwaggerSchema("de login")]
        public string IdLogin { get; set; }

        /// <summary>
        /// ID de la sesión actual.
        /// </summary>
        [SwaggerSchema("ID de la sesión actual.")]
        public string IdSesion { get; set; }

        /// <summary>
        /// Código del usuario
        /// </summary>
        [SwaggerSchema("Código del usuario")]
        public string? CodigoUsuario { get; set; }

        /// <summary>
        /// Clave del usuario
        /// </summary>
        [SwaggerSchema("Clave del usuario")]
        public string? ClaveUsuario { get; set; }

        /// <summary>
        /// Código de la agencia
        /// </summary>
        [SwaggerSchema("Código de la agencia")]
        public string CodigoAgencia { get; set; }

        /// <summary>
        /// Fecha del sistena
        /// </summary>
        [SwaggerSchema("Fecha del sistena")]
        public DateTime FechaSistema { get; set; }

        /// <summary>
        /// Nombre de la impresora
        /// </summary>
        [SwaggerSchema("Nombre de la impresora")]
        public string NombreImpresora { get; set; }

        /// <summary>
        /// Nombre del equipo
        /// </summary>
        [SwaggerSchema("Nombre del equipo")]
        public string NombreEquipo { get; set; }

        /// <summary>
        /// Código del canal de origen
        /// </summary>
        [SwaggerSchema("Código del canal de origen")]
        public string CodigoCanalOrigen { get; set; }

        /// <summary>
        /// Código del subcanal del origen
        /// </summary>
        [SwaggerSchema("Código del subcanal del origen")]
        public byte CodigoSubCanalOrigen { get; set; }

        /// <summary>
        /// Token de la sesión
        /// </summary>
        [SwaggerSchema("Token de la sesión")]
        public string? Token { get; set; }
    }
}
