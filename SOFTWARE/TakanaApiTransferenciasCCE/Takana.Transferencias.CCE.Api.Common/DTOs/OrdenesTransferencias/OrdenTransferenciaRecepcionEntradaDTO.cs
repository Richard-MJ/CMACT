using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias
{
    /// <summary>
    /// Proceso de orden de transferencia
    /// Clase que hace referencia a CT2 (7.2.3.2.2.)
    /// </summary>
    public record OrdenTransferenciaRecepcionEntradaDTO : GeneralOrdenTransferenciaDTO
    {
        /// <summary>
        /// Hace referencia al Código de Tarifa.
        /// </summary> 
        [Required]
        [SwaggerSchema("Hace referencia al Código de Tarifa.")]
        public string feeCode { get; set; }

        /// <summary>
        /// Identificador o Instruction ID enviado a la Entidad Originante en la respuesta a la consulta de cuenta (AV4).
        /// </summary> 
        [Required]
        [SwaggerSchema("Identificador o Instruction ID enviado a la Entidad Originante en la respuesta a la consulta de cuenta (AV4).")]
        public string referenceTransactionId { get; set; }

        /// <summary>
        /// Código del canal que origina la transacción.
        /// </summary> 
        [Required]
        [SwaggerSchema("Código del canal que origina la transacción.")]
        public string channel { get; set; }

        /// <summary>
        /// Fecha Local de la transacción. Formato: AAAAMMDD.
        /// </summary> 
        [Required]
        [SwaggerSchema("Fecha Local de la transacción. Formato: AAAAMMDD.")]
        public string creationDate { get; set; }

        /// <summary>
        /// Hora Local de la transacción. Formato: hhmmss.
        /// </summary> 
        [Required]
        [SwaggerSchema("Hora Local de la transacción. Formato: hhmmss.")]
        public string creationTime { get; set; }

        /// <summary>
        /// Hace referencia al Criterio de Aplicación.
        /// </summary> 
        [Required]
        [SwaggerSchema("Hace referencia al Criterio de Aplicación.")]
        public string applicationCriteria { get; set; }

        /// <summary>
        /// Tipo de Persona del Cliente Originante
        /// </summary> 
        [SwaggerSchema("Tipo de Persona del Cliente Originante")]
        public string? debtorTypeOfPerson { get; set; }

        /// <summary>
        /// Nombre del Cliente Originante.
        /// </summary> 
        [SwaggerSchema("Nombre del Cliente Originante.")]
        public string? debtorName { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [SwaggerSchema("Descripcion de dirección del originante")]
        public string? debtorAddressLine { get; set; }

        /// <summary>
        /// Dirección del Cliente Originante
        /// </summary> 
        [SwaggerSchema("Dirección del Cliente Originante")]
        public string? debtorIdCode { get; set; }

        /// <summary>
        /// Tipo de Documento del Cliente Originante.
        /// </summary> 
        [SwaggerSchema("Tipo de Documento del Cliente Originante.")]
        public string? debtorId { get; set; }

        /// <summary>
        /// Número de teléfono fijo del Cliente Originante.
        /// </summary> 
        [SwaggerSchema("Número de teléfono fijo del Cliente Originante.")]
        public string? debtorPhoneNumber { get; set; }

        /// <summary>
        /// Número de celular del Cliente Originante.
        /// </summary> 
        [SwaggerSchema("Número de celular del Cliente Originante.")]
        public string? debtorMobileNumber { get; set; }

        /// <summary>
        /// Corresponde al nombre del Cliente Receptor
        /// </summary> 
        [SwaggerSchema("Corresponde al nombre del Cliente Receptor")]
        public string? creditorName { get; set; }

        /// <summary>
        /// Dirección del Cliente Receptor.
        /// </summary> 
        [SwaggerSchema("Dirección del Cliente Receptor.")]
        public string? creditorAddressLine { get; set; }

        /// <summary>
        /// Número de teléfono fijo del Cliente Receptor
        /// </summary> 
        [SwaggerSchema("Número de teléfono fijo del Cliente Receptor")]
        public string? creditorPhoneNumber { get; set; }

        /// <summary>
        /// Número de celular del Cliente Receptor.
        /// </summary> 
        [SwaggerSchema("Número de celular del Cliente Receptor.")]
        public string? creditorMobileNumber { get; set; }

        /// <summary>
        /// Hace referencia al Concepto de cobro de tarifa. Ver Anexo 6 del documento
        /// </summary> 
        [Required]
        [SwaggerSchema("Hace referencia al Concepto de cobro de tarifa. Ver Anexo 6 del documento")]
        public string? purposeCode { get; set; }

        /// <summary>
        /// Glosa de la transacción. Este es un campo de libre disponibilidad para que el Cliente Originante indique la razón por la que realiza la transacción.
        /// </summary> 
        [SwaggerSchema("Glosa de la transacción. Este es un campo de libre disponibilidad para que el Cliente Originante indique la razón por la que realiza la transacción.")]
        public string? unstructuredInformation { get; set; }

        /// <summary>
        /// Hace referencia a Importe de Sueldos Brutos.
        /// </summary> 
        [SwaggerSchema("Hace referencia a Importe de Sueldos Brutos.")]
        public decimal? grossSalaryAmount { get; set; }

        /// <summary>
        /// Corresponde al Indicador Pago Haberes
        /// </summary> 
        [SwaggerSchema("Corresponde al Indicador Pago Haberes")]
        public string? salaryPaymentIndicator { get; set; }

        /// <summary>
        /// Hace referencia al Mes de Pago
        /// </summary> 
        [SwaggerSchema("Hace referencia al Mes de Pago")]
        public string? monthOfThePayment { get; set; }

        /// <summary>
        /// Hace referencia al Año de Pago
        /// </summary> 
        [SwaggerSchema("Hace referencia al Año de Pago")]
        public string? yearOfThePayment { get; set; }

        /// <summary>
        /// Hace referencia al monto de liquidación interbancaria – entiéndase, monto +/- comisión
        /// </summary> 
        [Required]
        [SwaggerSchema("Hace referencia al monto de liquidación interbancaria – entiéndase, monto +/- comisión")]
        public decimal interbankSettlementAmount { get; set; }

        /// <summary>
        /// Hace referencia a la fecha de liquidación.
        /// </summary> 
        [Required]
        [SwaggerSchema("Hace referencia a la fecha de liquidación.")]
        public string settlementDate { get; set; }

        /// <summary>
        /// Autogenerado proporcionado por IPS, correspondiente al encabezado del mensaje.
        /// </summary> 
        [Required]
        [SwaggerSchema("Autogenerado proporcionado por IPS, correspondiente al encabezado del mensaje.")]
        public string branchId { get; set; }

        /// <summary>
        /// Autogenerado de identificación única de transacción. Proporcionado por IPS.
        /// </summary> 
        [Required]
        [SwaggerSchema("Autogenerado de identificación única de transacción. Proporcionado por IPS.")]
        public string instructionId { get; set; }
    }
}
