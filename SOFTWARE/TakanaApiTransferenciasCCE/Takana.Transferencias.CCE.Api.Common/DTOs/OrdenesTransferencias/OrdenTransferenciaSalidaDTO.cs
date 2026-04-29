using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Takana.Transferencias.CCE.Api.Common.Utilidades;

namespace Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias
{
/// <summary>
/// Proceso de orden de transferencia
/// Clase que hace referencia a CT1 (7.2.3.2.1.)
/// </summary>
public record OrdenTransferenciaSalidaDTO : GeneralOrdenTransferenciaDTO
{
        /// <summary>
        /// Fecha Local de la transacción.
        /// </summary> 
        [Required]     
        public string creationDate {get; set;} 

        /// <summary>
        /// Hora Local de la transacción.
        /// </summary> 
        [Required]                          
        public string creationTime {get; set;}  

        /// <summary>
        /// Hace referencia al Código de Mensaje
        /// 0200: Mensaje de Requerimiento Transaccional)
        /// 0201: Mensaje de Reenvío de Requerimiento Transaccional
        /// </summary> 
        [Required]                       
        public string messageTypeId {get; set;}      

        /// <summary>
        /// Código del canal que origina la transacción Anexo 3 del documento
        /// </summary> 
        [Required]                                                                                   
        public string channel {get; set;}               

        /// <summary>
        /// Identificador o Instruction ID generado durante la consulta de cuenta.
        /// </summary> 
        [Required]              
        public string referenceTransactionId {get; set;}                                    

        /// <summary>
        /// Hace referencia al Código de Tarifa.
        /// </summary> 
        [Required]
        public string feeCode {get; set;}        

        /// <summary>
        /// Hace referencia al Criterio de Aplicación. 
        /// </summary> 
        [Required] 
        public string applicationCriteria {get; set;}     

        /// <summary>
        /// Tipo de Persona del Cliente Originante.
        /// N: Natural y J: Jurídica
        /// </summary> 
        [Required] 
        public string debtorTypeOfPerson {get; set;}      

        /// <summary>
        /// Nombre del Cliente Originante.
        /// </summary> 
        [Required]
        public string debtorName {get; set;}                         

        /// <summary>
        /// Dirección del Cliente Originante
        /// </summary> 
        public string? debtorAddressLine {get; set;}                                  

        /// <summary>
        /// Tipo de Documento del Cliente 
        /// </summary> 
        [Required]
        public string debtorIdCode {get; set;}      

        /// <summary>
        /// Número de Documento del Cliente Originante.
        /// </summary> 
        [Required]
        public string debtorId {get; set;}             

        /// <summary>
        /// Número de teléfono fijo del Cliente Originante.
        /// </summary> 
        public string? debtorPhoneNumber {get; set;}            

        /// <summary>
        /// Número de celular del Cliente Originante.
        /// </summary> 
        public string? debtorMobileNumber {get; set;}                                   

        /// <summary>
        /// Corresponde al nombre del Cliente
        /// </summary> 
        public string? creditorName {get; set;}                       

        /// <summary>
        /// Dirección del Cliente Receptor.
        /// </summary> 
        public string? creditorAddressLine {get; set;}                      

        /// <summary>
        /// Número de teléfono fijo del Cliente Receptor.
        /// </summary> 
        public string? creditorPhoneNumber {get; set;}            

        /// <summary>
        /// Número de celular del Cliente Receptor.

        /// </summary> 
        public string? creditorMobileNumber {get; set;}                                                                 

        /// <summary>
        /// Hace referencia al Concepto de cobro de tarifa.
        /// </summary> 
        public string? purposeCode {get; set;}                                    

        /// <summary>
        /// Glosa de la transacción. Este es un campo de libre disponibilidad para que el Cliente Originante indique la razón por la que realiza la transacción
        /// </summary> 
        public string? unstructuredInformation {get; set;}                                    

        /// <summary>
        /// Hace referencia a Importe de Sueldos Brutos (los 2 últimos dígitos indican la parte decimal).
        /// </summary> 
        [JsonPropertyName("grossSalaryAmount")]                      
        [JsonConverter(typeof(ConvertirdorJsonAtipo<decimal>))] 
        public decimal? grossSalaryAmount {get; set;}  

        /// <summary>
        /// Corresponde al Indicador Pago Haberes
        /// </summary> 
        public string? salaryPaymentIndicator {get; set;}                                   

        /// <summary>
        /// Hace referencia al Mes de Pago
        /// </summary> 
        public string? monthOfThePayment {get; set;}                                    

        /// <summary>
        /// Hace referencia al Año de Pago
        /// </summary> 
        public string? yearOfThePayment {get; set;}   
        
      
        }            
}
