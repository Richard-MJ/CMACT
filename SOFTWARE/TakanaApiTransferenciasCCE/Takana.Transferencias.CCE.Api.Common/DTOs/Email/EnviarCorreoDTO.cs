using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Email
{
    public class EnviarCorreoDTO
    {
        #region Propiedades
        /// <summary>
        /// Número de movimiento de la operaciones de transferencia
        /// </summary>
        [SwaggerSchema("Número de movimiento de la operaciones de transferencia")]
        public int NumeroMovimiento { get; set; }
        /// <summary>
        /// Correo de destino de la operacion
        /// </summary>
        [SwaggerSchema("Correo de destino de la operacion")]
        public string CorreoDestinatario { get; set; }
        /// <summary>
        /// Documento en bytes de terminos de beneficios y Riesgos
        /// </summary>
        [SwaggerSchema("Documento en bytes de terminos de beneficios y Riesgos")]
        public byte[] DocumentoTerminos { get; set; }
        /// <summary>
        /// Nombre de documento de terminos
        /// </summary>
        [SwaggerSchema("Nombre de documento de terminos")]
        public string NombreDocumentoTerminos { get; set; }
        #endregion
    }
}
