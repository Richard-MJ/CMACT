using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias
{
    /// <summary>
    /// Proceso de orden de transferencia
    /// Clase que hace referencia a CT3 (7.2.3.2.3.)
    /// </summary>
    public record OrdenTransferenciaRespuestaEntradaDTO : GeneralOrdenTransferenciaDTO
    {
        /// <summary>
        /// Fecha Local de la transacción.
        /// </summary> 
        [Required]
        [SwaggerSchema("Fecha Local de la transacción.")]
        public string responseDate { get; set; }

        /// <summary>
        /// Hora Local de la transacción.
        /// </summary>        
        [Required]
        [SwaggerSchema("Hora Local de la transacción.")]
        public string responseTime { get; set; }

        /// <summary>
        /// La razón del código de respuesta o Respuesta Aplicativa. Ver Anexo 4.
        /// </summary>
        [SwaggerSchema("La razón del código de respuesta o Respuesta Aplicativa. Ver Anexo 4.")]
        public string? responseCode { get; set; }

        /// <summary>
        /// Hace referencia a la fecha de liquidación.
        /// </summary>        
        [Required]
        [SwaggerSchema("Hace referencia a la fecha de liquidación.")]
        public string settlementDate { get; set; }

        /// <summary>
        /// La razón del código de respuesta o Respuesta Aplicativa. Ver Anexo 4.
        /// </summary>        
        [SwaggerSchema("La razón del código de respuesta o Respuesta Aplicativa. Ver Anexo 4.")]
        public string? reasonCode { get; set; }

        /// <summary>
        /// Autogenerado proporcionado por IPS en CT2.
        /// </summary>        
        [Required]
        [SwaggerSchema("Autogenerado proporcionado por IPS en CT2.")]
        public string branchId { get; set; }

        /// <summary>
        /// Autogenerado de identificación única de transacción. Proporcionado por IPS.
        /// </summary>        
        [Required]
        [SwaggerSchema("Autogenerado de identificación única de transacción. Proporcionado por IPS.")]
        public string instructionId { get; set; }
    }
}