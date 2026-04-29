using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.ConsultasCuentas
{
    /// <summary>
    /// Proceso de Consulta de Cuenta
    /// Hace referencia al Tramo 4 (AV4) 7.2.3.1.4.
    /// </summary>
    public record ConsultaCuentaRecepcionSalidaDTO : GeneralConsultaCuentaDTO
    {        
        /// <summary>
        /// CCódigo de Respuesta. 
        /// </summary>  
        [Required]
        public string responseCode { get; set; }
        /// <summary>
        /// La razón del código de respuesta o Respuesta Aplicativa.
        /// </summary>           
        public string? reasonCode { get; set; }
        /// <summary>
        /// Corresponde al nombre del Cliente Receptor
        /// </summary>           
        public string? creditorName { get; set; }
        /// <summary>
        /// Número de Documento del Cliente Receptor.
        /// </summary>           
        public string? creditorId { get; set; }
        /// <summary>
        /// Tipo de Documento del Cliente Receptor.
        /// </summary>           
        [Required]
        public string? creditorIdCode { get; set; }
        /// <summary>
        /// Indicador de Mismo Cliente, usado para determinar afectación de ITF.
        /// </summary>           
        public string? sameCustomerFlag { get; set; }
        /// <summary>
        /// Autogenerado de identificación única de transacción. Proporcionado por IPS en AV2.
        /// </summary>           
        [Required]
        public string instructionId { get; set; }        
    } 
}

