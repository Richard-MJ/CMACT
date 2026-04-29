using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Extensiones
{
    /// <summary>
    /// Clase que mapea los datos de las ordenes de transferencia
    /// </summary>
    public static class OrdenTransferenciaExtensiones
    {
        /// <summary>
        /// Extension que mapea la estrucuta de contenido de orden de tranferencia
        /// </summary>
        /// <param name="datosRecibidos"></param>
        /// <param name="datosCalculados"></param>
        /// <returns>Retorna estructura CT3</returns>
        public static EstructuraContenidoCT3 ArmarDatos(
            this OrdenTransferenciaRecepcionEntradaDTO datosRecibidos,
            OrdenTransferenciaRespuestaEntradaDTO datosCalculados
        ){
            return new EstructuraContenidoCT3()
            {
                CT3 = new OrdenTransferenciaRespuestaEntradaDTO
                {
                    debtorParticipantCode = datosRecibidos.debtorParticipantCode,
                    creditorParticipantCode = datosRecibidos.creditorParticipantCode,
                    responseDate = datosCalculados.responseDate,
                    responseTime = datosCalculados.responseTime,
                    trace = datosRecibidos.trace,
                    transactionType = datosRecibidos.transactionType,
                    amount = datosRecibidos.amount,
                    feeAmount = datosRecibidos.feeAmount,
                    currency = datosRecibidos.currency,
                    transactionReference = datosRecibidos.transactionReference?.EsVacioTexto(),
                    settlementDate = datosRecibidos.settlementDate,
                    debtorCCI = datosRecibidos.debtorCCI,
                    creditorCCI = datosRecibidos.creditorCCI?.EsVacioTexto(),
                    creditorCreditCard = datosRecibidos.creditorCreditCard?.EsVacioTexto(),
                    branchId = datosRecibidos.branchId,
                    instructionId = datosRecibidos.instructionId,                
                    terminalId = datosRecibidos.terminalId.LimitarCaracteres(8), 
                    retrievalReferenteNumber = datosRecibidos.retrievalReferenteNumber,
                    responseCode = datosCalculados.responseCode,
                    reasonCode = datosCalculados.reasonCode?.EsVacioTexto(),
                    sameCustomerFlag =  datosCalculados.sameCustomerFlag?.EsVacioTexto(),
                }   
            };        
        }
        
        /// <summary>
        /// Metodo que mapea los datos de la orden transferencia
        /// </summary>
        /// <param name="esquema">Datos de transferencia</param>
        /// <param name="datosOrden">Datos del canla origen</param>
        /// <returns>Retorna datos para enviar la transferencia</returns>
        public static OrdenTransferenciaSalidaDTO ArmarDatos(
            this OrdenTransferenciaSalidaDTO esquema,OrdenTransferenciaCanalDTO datosOrden,
            Calendario fechaSistema, string numeroSeguimiento)
        {
                esquema.debtorParticipantCode=datosOrden.CodigoEntidadOriginante;
                esquema.creditorParticipantCode=datosOrden.CodigoEntidadReceptora;
                esquema.terminalId = datosOrden.IdentificadorTerminal.Length > 6 
                    ? datosOrden.IdentificadorTerminal.Substring(0,6) : datosOrden.IdentificadorTerminal;
                esquema.retrievalReferenteNumber=datosOrden.NumeroReferencia;
                esquema.amount=(long)(datosOrden.MontoImporte * 100);
                esquema.channel = datosOrden.Canal;
                esquema.referenceTransactionId=datosOrden.CodigoTransferencia;
                esquema.transactionType=datosOrden.TipoTransaccion;
                esquema.feeAmount= (long)(datosOrden.MontoComision * 100);
                esquema.feeCode=datosOrden.CodigoTarifa;
                esquema.applicationCriteria=datosOrden.CriterioPlaza;
                esquema.debtorTypeOfPerson=datosOrden.TipoPersonaDeudor;
                esquema.debtorName=datosOrden.NommbreDeudor;
                esquema.debtorAddressLine=datosOrden.DireccionDeudor;
                esquema.debtorIdCode=datosOrden.TipoDocumentoDeudorCCE;
                esquema.debtorId=datosOrden.NumeroIdentidadDeudor;
                esquema.debtorPhoneNumber=datosOrden.NumeroTelefonoDeudor;
                esquema.debtorMobileNumber=datosOrden.NumeroCelularDeudor;
                esquema.debtorCCI=datosOrden.CodigoCuentaInterbancariaDeudor;
                esquema.creditorName=datosOrden.NombreReceptor;
                esquema.creditorAddressLine=datosOrden.DireccionReceptor;
                esquema.creditorPhoneNumber=datosOrden.NumeroTelefonoReceptor;
                esquema.creditorMobileNumber=datosOrden.NumeroCelularReceptor;
                esquema.creditorCCI=datosOrden.CodigoCuentaInterbancariaReceptor;
                esquema.creditorCreditCard=datosOrden.CodigoTarjetaReceptor;
                esquema.sameCustomerFlag=datosOrden.IndicadroITF;
                esquema.purposeCode= string.IsNullOrEmpty(datosOrden.ConceptoCobroTarifa)
                    ? DatosGenerales.CodigoTarifaInmediata : datosOrden.ConceptoCobroTarifa;
                esquema.unstructuredInformation=datosOrden.GlosaTransaccion;
                esquema.grossSalaryAmount=null;
                esquema.salaryPaymentIndicator=datosOrden.IndicadorHaberes;
                esquema.messageTypeId = DatosGenerales.PrimerReintento;
                esquema.creationDate = fechaSistema.FechaFormato;
                esquema.creationTime = fechaSistema.HoraFormato;
                esquema.currency = datosOrden.CodigoMoneda;
                esquema.trace = numeroSeguimiento;
                esquema.monthOfThePayment=null;
                esquema.yearOfThePayment = null;

            return esquema;
            
        }

        /// <summary>
        /// Traduce los campos de la CCE a campos en español
        /// </summary>
        /// <param name="datos">Datos de respuesta de la CCE</param>
        /// <returns>Retorna datos de respuesta de la CCE</returns>
        public static OrdenTransferenciaRespuestaTraducidoDTO TraduccionOrdenTransferenciaSalida(OrdenTransferenciaRecepcionSalidaDTO datos)
        {
            return new OrdenTransferenciaRespuestaTraducidoDTO{
                CodigoRespuesta=datos.responseCode,
                RazonCodigoRespuesta=datos.reasonCode,
                Trace=datos.trace,     
                IdentificadorTransaccion=datos.instructionId,
                CodigoCuentaInterbancariaReceptor=datos.creditorCCI,                   
                CodigoTarjetaCreditoReceptor=datos.creditorCreditCard,               
                IndicadorITF=datos.sameCustomerFlag,
                CodigoMoneda=datos.currency,
                Monto= datos.amount
                
            };
        }  
        }
}

