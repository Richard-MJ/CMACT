using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.ConsultasCuentas
{
    /// <summary>
    /// Proceso de Consulta de Cuenta
    /// Hace referencia al Tramo 1 (AV1) 7.2.3.1.1.
    /// </summary>
    public record ConsultaCuentaSalidaDTO : GeneralConsultaCuentaDTO
    {
        /// <summary>
        /// Tipo de Persona del Cliente Originante.
        /// </summary>  
        [Required]
        public string debtorTypeOfPerson { get; set; }

       
    }
}

