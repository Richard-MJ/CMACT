using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.ConsultasCuentas
{
    /// <summary>
    /// Proceso de Consulta de Cuenta
    /// Hace referencia al Tramo 3 (AV3) 7.2.3.1.3.
    /// </summary>
    public record ConsultaCuentaRespuestaEntradaDTO : GeneralConsultaCuentaDTO
    {
        /// <summary>
        /// Código de Respuesta. 
        /// </summary>  
        [Required]
        [SwaggerSchema("Código de Respuesta. ")]
        public string responseCode { get; set; }
        /// <summary>
        /// La razón del código de respuesta o Respuesta Aplicativa.
        /// </summary>           
        [SwaggerSchema("La razón del código de respuesta o Respuesta Aplicativa.")]
        public string? reasonCode { get; set; }
        /// <summary>
        /// Corresponde al nombre del Cliente Receptor
        /// </summary>           
        [SwaggerSchema("Corresponde al nombre del Cliente Receptor")]
        public string? creditorName { get; set; }
        /// <summary>
        /// Número de Documento del Cliente Receptor.
        /// </summary>           
        [SwaggerSchema("Número de Documento del Cliente Receptor.")]
        public string? creditorId { get; set; }
        /// <summary>
        /// Tipo de Documento del Cliente Receptor.
        /// </summary>           
        [SwaggerSchema("Tipo de Documento del Cliente Receptor.")]
        public string? creditorIdCode { get; set; }
        /// <summary>
        /// Indicador de Mismo Cliente, usado para determinar afectación de ITF.
        /// </summary>           
        [SwaggerSchema("Indicador de Mismo Cliente, usado para determinar afectación de ITF.")]
        public string? sameCustomerFlag { get; set; }
        /// <summary>
        /// Autogenerado de identificación única de transacción. Proporcionado por IPS.
        /// </summary>         
        [Required]
        [SwaggerSchema("Autogenerado de identificación única de transacción. Proporcionado por IPS.")]
        public string branchId { get; set; }
        /// <summary>
        /// Autogenerado proporcionado por IPS, correspondiente al encabezado del mensaje.
        /// </summary>           
        [Required]
        [SwaggerSchema("Autogenerado proporcionado por IPS, correspondiente al encabezado del mensaje.")]
        public string instructionId { get; set; }
    }
}

