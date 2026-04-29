using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.ConsultasCuentas
{   
    /// <summary>
    /// Proceso de Consulta de Cuenta
    /// Hace referencia al Tramo 2 (AV2) 7.2.3.1.2.
    /// </summary>
    /// 
    public record ConsultaCuentaRecepcionEntradaDTO : GeneralConsultaCuentaDTO
    {
        /// <summary>
        /// Tipo de Persona del Cliente Originante. 
        /// </summary> 
        [Required]
        [SwaggerSchema("Tipo de Persona del Cliente Originante.")]
        public string debtorTypeOfPerson { get; set; }
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

