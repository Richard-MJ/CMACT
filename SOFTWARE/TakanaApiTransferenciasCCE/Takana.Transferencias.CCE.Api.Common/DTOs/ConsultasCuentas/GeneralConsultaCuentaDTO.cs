using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.ConsultasCuentas
{
    /// <summary>
    /// Clase Padre que representa los datos generales del proceso de Consulta Cuenta.
    /// Son datos redundantes de todos los tramos.
    /// </summary>
    public record GeneralConsultaCuentaDTO : GeneralTransferenciaDTO
    {
        /// <summary>
        /// Fecha Local de la transacción
        /// </summary>
        [Required]
        [SwaggerSchema("Fecha Local de la transacción")]
        public string creationDate { get; set; }
        /// <summary>
        /// Hora Local de la transacción
        /// </summary>        
        [Required]
        [SwaggerSchema("Hora Local de la transacción")]
        public string creationTime { get; set; }
        /// <summary>
        /// Nombre del Cliente Originante.
        /// </summary>        
        [SwaggerSchema("Nombre del Cliente Originante.")]
        public string? debtorName { get; set; }
        /// <summary>
        /// Número de Documento del Cliente Originante.
        /// </summary>        
        [Required]
        [SwaggerSchema("Número de Documento del Cliente Originante.")]
        public string debtorId { get; set; }
        /// <summary>
        /// Tipo de Documento del Cliente Originante.
        /// </summary>        
        [Required]
        [SwaggerSchema("Tipo de Documento del Cliente Originante.")]
        public string debtorIdCode { get; set; }
        /// <summary>
        /// Número de teléfono fijo del Cliente Originante.
        /// </summary>        
        [SwaggerSchema("Número de teléfono fijo del Cliente Originante.")]
        public string? debtorPhoneNumber { get; set; }
        /// <summary>
        /// Dirección del Cliente Originante.
        /// </summary>        
        [SwaggerSchema("Dirección del Cliente Originante.")]
        public string? debtorAddressLine { get; set; }
        /// <summary>
        /// Número de celular del Cliente Originante.
        /// </summary>        
        [SwaggerSchema("Número de celular del Cliente Originante.")]
        public string? debtorMobileNumber { get; set; }
        /// <summary>
        /// Código del canal que origina la transacción.
        /// </summary>        
        [Required]
        [SwaggerSchema("Código del canal que origina la transacción.")]
        public string channel { get; set; }
        /// <summary>
        /// Dirección del Cliente Receptor.
        /// </summary>        
        [SwaggerSchema("Dirección del Cliente Receptor.")]
        public string? creditorAddressLine { get; set; }
        /// <summary>
        /// Número de teléfono fijo del Cliente Receptor.
        /// </summary>        
        [SwaggerSchema("Número de teléfono fijo del Cliente Receptor.")]
        public string? creditorPhoneNumber { get; set; }
        /// <summary>
        /// Número de celular del Cliente Receptor.
        /// </summary>        
        [SwaggerSchema("Número de celular del Cliente Receptor.")]
        public string? creditorMobileNumber { get; set; }
        /// <summary>
        /// Dato específico para MPP que se utilizará en actualizaciones futuras
        /// </summary>        
        [SwaggerSchema("Dato específico para MPP que se utilizará en actualizaciones futuras")]
        public string? proxyValue { get; set; }
        /// <summary>
        /// Dato específico para MPP que se utilizará en actualizaciones futuras.
        /// </summary>        
        [SwaggerSchema("Dato específico para MPP que se utilizará en actualizaciones futuras.")]
        public string? proxyType { get; set; }
    }
}


