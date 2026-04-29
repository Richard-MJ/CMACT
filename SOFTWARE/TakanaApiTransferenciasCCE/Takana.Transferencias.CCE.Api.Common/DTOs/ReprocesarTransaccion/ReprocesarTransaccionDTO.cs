using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.ReprocesarTransaccion
{
    public record ReprocesarTransaccionDTO
    {
        /// <summary>
        /// Identificador de transaccion
        /// </summary>
        [Required]
        [SwaggerSchema("Identificador de transaccion")]
        public int IdTransaccion { get; set; }
        /// <summary>
        /// Identificador de archivo movimiento
        /// </summary>
        [Required]
        [SwaggerSchema("Identificador de archivo movimiento")]
        public int IdArchivoMovimiento { get; set; }
        /// <summary>
        /// usuario que realizo la devolucion
        /// </summary>
        [Required]
        public string Usuario { get; set; }
    }
}




