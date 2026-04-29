using System.Globalization;
using Takana.Transferencias.CCE.Api.Common.Rechazos;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Common.Cancelaciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.SolicitudEstadoPago;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    public class BitacoraTransferenciaInmediata
    {
        #region Propiedades

        /// <summary>
        /// Numero de bitacora
        /// </summary>
        public int NumeroBitacora { get; private set; }
        /// <summary>
        /// Indicador de bitacora
        /// </summary>
        public string? IndicadorBitacora { get; private set; } = string.Empty;
        /// <summary>
        /// Identificador de trama
        /// </summary>
        public int IdentificadorTrama { get; private set; }
        /// <summary>
        /// Tipo de transferencia
        /// </summary>
        public string? TipoTransferencia { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de entidad originante
        /// </summary>
        public string? CodigoEntidadOriginante { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de entidad receptor
        /// </summary>
        public string? CodigoEntidadReceptor { get; private set; } = string.Empty;
        /// <summary>
        /// Fecha bitacora de operacion
        /// </summary>
        public DateTime FechaBitacoraOperacion { get; private set; }
        /// <summary>
        /// Fecha de bitacora respuesta
        /// </summary>
        public DateTime? FechaBitacoraRespuesta { get; private set; }
        /// <summary>
        /// Fecha de liquidacion
        /// </summary>
        public DateTime? FechaLiquidacion { get; private set; }
        /// <summary>
        /// Numero de trace
        /// </summary>
        public string? NumeroTrace { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de cuenta interbancaria originante
        /// </summary>
        public string? CodigoCuentaInterbancariaOriginante { get; private set; } = string.Empty;
        /// <summary>
        /// Tipo persona originante
        /// </summary>
        public string? TipoPersonaOriginante { get; private set; } = string.Empty;
        /// <summary>
        /// Nombre de originante
        /// </summary>
        public string? NombreOriginante { get; private set; } = string.Empty;
        /// <summary>
        /// Numero de documento originante
        /// </summary>
        public string? NumeroDocumentoOriginante { get; private set; } = string.Empty;
        /// <summary>
        /// Tipo de documento originante
        /// </summary>
        public string? TipoDocumentoOriginante { get; private set; } = string.Empty;
        /// <summary>
        /// telefono del originante
        /// </summary>
        public string? TelefonoOriginante { get; private set; } = string.Empty;
        /// <summary>
        /// celular del originante
        /// </summary>
        public string? CelularOriginante { get; private set; } = string.Empty;
        /// <summary>
        /// Direccion del originante
        /// </summary>
        public string? DireccionOriginante { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de cuenta interbancanria del receptor
        /// </summary>
        public string? CodigoCuentaInterbancariaReceptor { get; private set; } = string.Empty;
        /// <summary>
        /// Nombre de receptor
        /// </summary>
        public string? NombreReceptor { get; private set; } = string.Empty;
        /// <summary>
        /// Numero de documento receptor
        /// </summary>
        public string? NumeroDocumentoReceptor { get; private set; } = string.Empty;
        /// <summary>
        /// TIipo de documento receptor
        /// </summary>
        public string? TipoDocumentoReceptor { get; private set; } = string.Empty;
        /// <summary>
        /// Direccion del receptor
        /// </summary>
        public string? DireccionReceptor { get; private set; } = string.Empty;
        /// <summary>
        /// Telefono de receptor
        /// </summary>
        public string? TelefonoReceptor { get; private set; } = string.Empty;
        /// <summary>
        /// Celular del receptor
        /// </summary>
        public string? CelularReceptor { get; private set; } = string.Empty;
        /// <summary>
        /// Numero de tarjeta del receptor
        /// </summary>
        public string? NumeroTarjetaReceptor { get; private set; } = string.Empty;
        /// <summary>
        /// Identificador de referencia de transaccion
        /// </summary>
        public string? IdentificadorReferenciaTransaccion { get; private set; } = string.Empty;
        /// <summary>
        /// Referencia de transaccion
        /// </summary>
        public string? ReferenciaTransaccion { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de numero de referencia
        /// </summary>
        public string? CodigoNumeroReferencia { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de respuesta
        /// </summary>
        public string? CodigoRespuesta { get; private set; } = string.Empty;
        /// <summary>
        /// Razon de respuesta
        /// </summary>
        public string? RazonRespuesta { get; private set; } = string.Empty;
        /// <summary>
        /// Mensaje de reenvio
        /// </summary>
        public string? MensajeReenvio { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de moneda
        /// </summary>
        public string? CodigoMoneda { get; private set; } = string.Empty;
        /// <summary>
        /// Valor de proxy
        /// </summary>
        public string? ValorProxy { get; private set; } = string.Empty;
        /// <summary>
        /// Tipo de proxy
        /// </summary>
        public string? TipoProxy { get; private set; } = string.Empty;
        /// <summary>
        /// Monto de importe
        /// </summary>
        public decimal? MontoImporte { get; private set; } = decimal.Zero;
        /// <summary>
        /// Monto de comisión
        /// </summary>
        public decimal? MontoComision { get; private set; } = decimal.Zero;
        /// <summary>
        /// Monto de liquidacion
        /// </summary>
        public decimal? MontoLiquidacion { get; private set; } = decimal.Zero;
        /// <summary>
        /// Monto de salario bruto
        /// </summary>
        public decimal? MontoSalarioBruto { get; private set; } = decimal.Zero;
        /// <summary>
        /// Indicador de ITF
        /// </summary>
        public string? IndicadorITF { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de tarifa
        /// </summary>
        public string? CodigoTarifa { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de criterio de aplicacion
        /// </summary>
        public string? CodigoCriterioAplicacion { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de canal
        /// </summary>
        public string? CodigoCanal { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de proposito
        /// </summary>
        public string? CodigoProposito { get; private set; } = string.Empty;
        /// <summary>
        /// Descripcion de informacion estructurada
        /// </summary>
        public string? DescripcionInformacionEstructurada { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de terminal
        /// </summary>
        public string? CodigoTerminal { get; private set; } = string.Empty;
        /// <summary>
        /// Indicador de pago salarial
        /// </summary>
        public string? IndicadorPagoSalario { get; private set; } = string.Empty;
        /// <summary>
        /// Mes de pago
        /// </summary>
        public string? MesPago { get; private set; } = string.Empty;
        /// <summary>
        /// Año de pago
        /// </summary>
        public string? AnioPago { get; private set; } = string.Empty;
        /// <summary>
        /// Identificador de instruccion
        /// </summary>
        public string? IdentificadorInstruccion { get; private set; } = string.Empty;
        /// <summary>
        /// Identificador de rama
        /// </summary>
        public string? IdentificadorRama { get; private set; } = string.Empty;
        /// <summary>
        /// Descripcion de mensaje original
        /// </summary>
        public string? DescripcionMensajeOriginal { get; private set; } = string.Empty;
        /// <summary>
        /// Descripcion Definicion de mensaje
        /// </summary>
        public string? DescripcionDefinicionMensaje { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de Sistema
        /// </summary>
        public string? CodigoSistema { get; private set; } = string.Empty;        
        /// <summary>
        /// Indicador de estado
        /// </summary>
        public string IndicadorConsultaQR { get; private set; } = General.No;
        /// <summary>
        /// Indicador de estado
        /// </summary>
        public string IndicadorEstado { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de Usuario Registro
        /// </summary>
        public string? CodigoUsuarioRegistro { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de usuario modifico
        /// </summary>
        public string CodigoUsuarioModifico { get; private set; } = string.Empty;
        /// <summary>
        /// Fecha de registro
        /// </summary>
        public DateTime FechaRegistro { get; private set; }
        /// <summary>
        /// Fecha de modifico
        /// </summary>
        public DateTime? FechaModifico { get; private set; }
        /// <summary>
        /// Tipo de trama
        /// </summary>
        public virtual TipoTrama TipoTrama { get; private set; }

        #endregion Propiedades

        #region Operaciones

        /// <summary>
        /// Extension que mapea el registro de una bitacora
        /// </summary>
        /// <param name="datosBitacora"></param>
        /// <param name="fechaSistema"></param>
        /// <returns>Retorna la bitacora mapeada</returns>
        public static BitacoraTransferenciaInmediata RegistrarBitacora(
            ConsultaCuentaRecepcionEntradaDTO datosBitacora,
            DateTime fechaSistema)
        {
            return new BitacoraTransferenciaInmediata
            {
                IndicadorBitacora = General.Receptor,
                IdentificadorTrama = (int)TipoTramaEnum.ConsultaCuenta,
                CodigoEntidadOriginante = datosBitacora.debtorParticipantCode,
                CodigoEntidadReceptor = datosBitacora.creditorParticipantCode,
                FechaBitacoraOperacion = DateTime.ParseExact(datosBitacora.creationDate + " " + datosBitacora.creationTime,
                                            "yyyyMMdd HHmmss",
                                            CultureInfo.InvariantCulture),
                CodigoTerminal = datosBitacora.terminalId,
                CodigoNumeroReferencia = datosBitacora.retrievalReferenteNumber,
                NumeroTrace = datosBitacora.trace,
                NombreOriginante = datosBitacora.debtorName,
                NumeroDocumentoOriginante = datosBitacora.debtorId,
                TipoDocumentoOriginante = datosBitacora.debtorIdCode,
                TelefonoOriginante = datosBitacora.debtorPhoneNumber.CompletarTextoAVacio(),
                CelularOriginante = datosBitacora.debtorMobileNumber.CompletarTextoAVacio(),
                DireccionOriginante = datosBitacora.debtorAddressLine.CompletarTextoAVacio(),
                TipoTransferencia = datosBitacora.transactionType,
                CodigoCanal = datosBitacora.channel,
                DireccionReceptor = datosBitacora.creditorAddressLine.CompletarTextoAVacio(),
                TelefonoReceptor = datosBitacora.creditorPhoneNumber.CompletarTextoAVacio(),
                CelularReceptor = datosBitacora.creditorMobileNumber.CompletarTextoAVacio(),
                CodigoCuentaInterbancariaReceptor = datosBitacora.creditorCCI.CompletarTextoAVacio(),
                NumeroTarjetaReceptor = datosBitacora.creditorCreditCard.CompletarTextoAVacio(),
                TipoPersonaOriginante = datosBitacora.debtorTypeOfPerson,
                CodigoMoneda = datosBitacora.currency,
                ValorProxy = datosBitacora.proxyValue,
                TipoProxy = datosBitacora.proxyType,
                IdentificadorInstruccion = datosBitacora.instructionId,
                IdentificadorRama = datosBitacora.branchId,
                DescripcionInformacionEstructurada = string.Empty,
                CodigoUsuarioRegistro = General.UsuarioPorDefecto,
                FechaRegistro = fechaSistema,
                IndicadorEstado = General.Pendiente,
                CodigoSistema = Sistema.CuentaEfectivo,
            };
        }

        /// <summary>
        /// Extension que mapea la actualizacion de una bitacora
        /// </summary>
        /// <param name="datosBitacora"></param>
        /// <param name="fechaSistema"></param>
        /// <param name="codigoValidacionFirma"></param>
        public void ActualizarBitacora(
            ConsultaCuentaRespuestaEntradaDTO datosBitacora,
            ClienteReceptorDTO clienteReceptor,
            Calendario fechaSistema,
            string codigoValidacionFirma)
        {
            DireccionReceptor = clienteReceptor.Direccion.LimitarCaracteres(70);
            TelefonoReceptor = clienteReceptor.NumeroTelefono.LimitarCaracteres(7);
            CelularReceptor = clienteReceptor.NumeroCelular.CompletarTextoAVacio();
            NombreReceptor = clienteReceptor.NombreCliente;
            NumeroDocumentoReceptor = clienteReceptor.NumeroDocumento;
            CodigoRespuesta = datosBitacora.responseCode;
            RazonRespuesta = codigoValidacionFirma != CF.CodigoRespuesta.codigo0000
                ? codigoValidacionFirma : datosBitacora.reasonCode;
            TipoDocumentoReceptor = clienteReceptor.TipoDocumentoCCE;
            IndicadorITF = datosBitacora.sameCustomerFlag;
            FechaBitacoraRespuesta = fechaSistema.FechaHoraSistema;
            IndicadorEstado = datosBitacora.responseCode == CF.CodigoRespuesta.Aceptada
                ? General.Aceptado : General.Rechazo;
            FechaModifico = fechaSistema.FechaHoraSistema;
            CodigoUsuarioModifico = General.UsuarioPorDefecto;
        }

        /// <summary>
        /// Extension que mapea el registro de una bitacora
        /// </summary>
        /// <param name="datosBitacora"></param>
        /// <param name="fechaSistema"></param>
        /// <returns>Retorna la bitacora mapeada</returns>
        public static BitacoraTransferenciaInmediata RegistrarBitacora(
            OrdenTransferenciaRecepcionEntradaDTO datosBitacora, 
            DateTime fechaSistema)
        {
            return new BitacoraTransferenciaInmediata
            {
                IndicadorBitacora = General.Receptor,
                IdentificadorTrama = (int)TipoTramaEnum.OrdenTransferencia,
                CodigoEntidadOriginante = datosBitacora.debtorParticipantCode,
                CodigoEntidadReceptor = datosBitacora.creditorParticipantCode,
                FechaBitacoraOperacion = DateTime.ParseExact(datosBitacora.creationDate + " " + datosBitacora.creationTime,
                                                            "yyyyMMdd HHmmss",
                                                            CultureInfo.InvariantCulture),
                CodigoTerminal = datosBitacora.terminalId,
                CodigoNumeroReferencia = datosBitacora.retrievalReferenteNumber,
                NumeroTrace = datosBitacora.trace,
                CodigoCanal = datosBitacora.channel,
                NombreOriginante = datosBitacora.debtorName,
                NumeroDocumentoOriginante = datosBitacora.debtorId,
                TipoDocumentoOriginante = datosBitacora.debtorIdCode,
                TelefonoOriginante = datosBitacora.debtorPhoneNumber.CompletarTextoAVacio(),
                CelularOriginante = datosBitacora.debtorMobileNumber.CompletarTextoAVacio(),
                DireccionOriginante = datosBitacora.debtorAddressLine.CompletarTextoAVacio(),
                TipoTransferencia = datosBitacora.transactionType,
                DireccionReceptor = datosBitacora.creditorAddressLine.CompletarTextoAVacio(),
                TelefonoReceptor = datosBitacora.creditorPhoneNumber.CompletarTextoAVacio(),
                CelularReceptor = datosBitacora.creditorMobileNumber.CompletarTextoAVacio(),
                CodigoCuentaInterbancariaReceptor = datosBitacora.creditorCCI.CompletarTextoAVacio(),
                NumeroTarjetaReceptor = datosBitacora.creditorCreditCard.CompletarTextoAVacio(),
                CodigoMoneda = datosBitacora.currency,
                IdentificadorInstruccion = datosBitacora.instructionId,
                IdentificadorRama = datosBitacora.branchId,
                NombreReceptor = datosBitacora.creditorName,
                IndicadorITF = datosBitacora.sameCustomerFlag,
                MontoImporte = datosBitacora.amount.ObtenerImporteOperacion(),
                ReferenciaTransaccion = datosBitacora.transactionReference,
                IdentificadorReferenciaTransaccion = datosBitacora.referenceTransactionId,
                MontoComision = datosBitacora.feeAmount.ObtenerImporteOperacion(),
                CodigoTarifa = datosBitacora.feeCode,
                CodigoCriterioAplicacion = datosBitacora.applicationCriteria,
                TipoPersonaOriginante = datosBitacora.debtorTypeOfPerson,
                CodigoCuentaInterbancariaOriginante = datosBitacora.debtorCCI,
                CodigoProposito = datosBitacora.purposeCode.CompletarTextoAVacio(),
                DescripcionInformacionEstructurada = datosBitacora.unstructuredInformation.CompletarTextoAVacio(),
                MontoSalarioBruto = datosBitacora.grossSalaryAmount != null && datosBitacora.grossSalaryAmount != 0.00m ?
                    Utilidades.ObtenerImporteOperacion((decimal)datosBitacora.grossSalaryAmount) : 0.00m,
                IndicadorPagoSalario = datosBitacora.salaryPaymentIndicator,
                MesPago = datosBitacora.monthOfThePayment,
                AnioPago = datosBitacora.yearOfThePayment,
                FechaLiquidacion = DateTime.ParseExact(datosBitacora.settlementDate,
                                                    "yyyyMMdd",
                                                    CultureInfo.InvariantCulture),
                MontoLiquidacion = datosBitacora.interbankSettlementAmount.ObtenerImporteOperacion(),
                CodigoUsuarioRegistro = General.UsuarioPorDefecto,
                FechaRegistro = fechaSistema,
                IndicadorEstado = General.Pendiente,
                CodigoSistema = Sistema.CuentaEfectivo
            };
        }

        /// <summary>
        /// Extension que mapea la actualizacion de una bitacora
        /// </summary>
        /// <param name="datosBitacora"></param>
        /// <param name="datosBitacora"></param>
        /// <param name="fechaSistema"></param>
        public void ActualizarBitacora(
            OrdenTransferenciaRespuestaEntradaDTO datosBitacora, 
            DateTime fechaSistema,
            string codigoValidacionFirma)
        {
            CodigoRespuesta = datosBitacora.responseCode;
            RazonRespuesta = codigoValidacionFirma != CF.CodigoRespuesta.codigo0000
                ? codigoValidacionFirma : datosBitacora.reasonCode;
            FechaBitacoraRespuesta = fechaSistema;
            IndicadorITF = datosBitacora.sameCustomerFlag;
            IndicadorEstado = datosBitacora.responseCode == CF.CodigoRespuesta.Aceptada
                ? General.Aceptado : General.Rechazo;
            CodigoUsuarioModifico = General.UsuarioPorDefecto;
            FechaModifico = fechaSistema;
        }

        /// <summary>
        /// Extension que mapea el registro de una bitacora
        /// </summary>
        /// <param name="datosBitacora"></param>
        /// <param name="fechaSistema"></param>
        /// <returns>Retorna la bitacora mapeada</returns>
        public static BitacoraTransferenciaInmediata RegistrarBitacora(
            OrdenTransferenciaConfirmacionEntradaDTO datosBitacora, 
            DateTime fechaSistema)
        {
            return new BitacoraTransferenciaInmediata
            {
                IndicadorBitacora = General.Receptor,
                IdentificadorTrama = (int)TipoTramaEnum.Confirmacion,
                CodigoEntidadOriginante = datosBitacora.debtorParticipantCode,
                CodigoEntidadReceptor = datosBitacora.creditorParticipantCode,
                FechaBitacoraOperacion = DateTime.ParseExact(datosBitacora.creationDate + " " + datosBitacora.creationTime,
                                                            "yyyyMMdd HHmmss",
                                                            CultureInfo.InvariantCulture),
                CodigoTerminal = datosBitacora.terminalId,
                CodigoNumeroReferencia = datosBitacora.retrievalReferenteNumber.CompletarTextoAVacio(),
                NumeroTrace = datosBitacora.trace,
                TipoTransferencia = datosBitacora.transactionType,
                CodigoCanal = datosBitacora.channel,
                CodigoCuentaInterbancariaReceptor = datosBitacora.creditorCCI,
                CodigoCuentaInterbancariaOriginante = datosBitacora.debtorCCI,
                NumeroTarjetaReceptor = datosBitacora.creditorCreditCard,
                CodigoMoneda = datosBitacora.currency,
                IdentificadorInstruccion = datosBitacora.instructionId,
                CodigoRespuesta = datosBitacora.responseCode,
                MontoImporte = datosBitacora.amount.ObtenerImporteOperacion(),
                ReferenciaTransaccion = datosBitacora.transactionReference,
                MontoComision = datosBitacora.feeAmount.ObtenerImporteOperacion(),
                IndicadorITF = datosBitacora.sameCustomerFlag,
                FechaLiquidacion = DateTime.ParseExact(datosBitacora.settlementDate,
                                                    "yyyyMMdd",
                                                    CultureInfo.InvariantCulture),
                MontoLiquidacion = datosBitacora.interbankSettlementAmount.ObtenerImporteOperacion(),
                FechaRegistro = fechaSistema,
                CodigoUsuarioRegistro = General.UsuarioPorDefecto,
                IndicadorEstado = General.Confirmado,
                CodigoSistema = Sistema.CuentaEfectivo
            };
        }

        /// <summary>
        /// Extension que mapea la actualizacion de una bitacora
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="datosBitacora"></param>
        /// <param name="fechaSistema"></param>
        public void ActualizarBitacora(
            TransaccionOrdenTransferenciaInmediata datosBitacora, 
            DateTime fechaSistema,
            string codigoValidacionFirma)
        {
            if (codigoValidacionFirma != CF.CodigoRespuesta.codigo0000)
                RazonRespuesta = codigoValidacionFirma;
            NumeroDocumentoOriginante = datosBitacora.NumeroDocumentoIdentidadOriginante;
            NombreOriginante = datosBitacora.NombreOriginante;
            NumeroDocumentoReceptor = datosBitacora.NumeroDocumentoIdentidadReceptor;
            NombreReceptor = datosBitacora.NombreReceptor;
            FechaBitacoraRespuesta = fechaSistema;
            IndicadorEstado = General.Finalizado;
            FechaModifico = fechaSistema;
            CodigoUsuarioModifico = General.UsuarioPorDefecto;
        }

        /// <summary>
        /// Extension que mapea el registro de una bitacora
        /// </summary>
        /// <param name="datosBitacora"></param>
        /// <param name="fechaSistema"></param>
        /// <returns>Retorna la bitacora mapeada</returns>
        public static BitacoraTransferenciaInmediata RegistrarBitacora(
            CancelacionRecepcionDTO datosBitacora, 
            DateTime fechaSistema)
        {
            return new BitacoraTransferenciaInmediata
            {
                IndicadorBitacora = General.Receptor,
                IdentificadorTrama = (int)TipoTramaEnum.Cancelacion,
                CodigoEntidadReceptor = datosBitacora.creditorParticipantCode,
                FechaBitacoraOperacion = DateTime.ParseExact(datosBitacora.creationDate + " " + datosBitacora.creationTime,
                                                            "yyyyMMdd HHmmss",
                                                            CultureInfo.InvariantCulture),
                CodigoMoneda = datosBitacora.currency,
                ReferenciaTransaccion = datosBitacora.transactionReference.CompletarTextoAVacio(),
                IdentificadorInstruccion = datosBitacora.instructionId,
                IdentificadorRama = datosBitacora.branchId,
                FechaRegistro = fechaSistema,
                CodigoUsuarioRegistro = General.UsuarioPorDefecto,
                IndicadorEstado = General.Pendiente,
                CodigoSistema = Sistema.CuentaEfectivo
            };
        }

        /// <summary>
        /// Extension que mapea la actualizacion de una bitacora
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="datosBitacora"></param>
        /// <param name="fechaSistema"></param>
        public void ActualizarBitacora(
            CancelacionRespuestaDTO datosBitacora, 
            DateTime fechaSistema,
            string codigoValidacionFirma)
        {
            CodigoRespuesta = datosBitacora.responseCode;
            RazonRespuesta = codigoValidacionFirma != CF.CodigoRespuesta.codigo0000
                ? codigoValidacionFirma : datosBitacora.reasonCode;
            FechaBitacoraRespuesta = DateTime
                .ParseExact(datosBitacora.responseDate + " " + datosBitacora.responseTime,
                "yyyyMMdd HHmmss",
                CultureInfo.InvariantCulture);
            IndicadorEstado = General.Finalizado;
            CodigoUsuarioModifico = General.UsuarioPorDefecto;
            FechaModifico = fechaSistema;
        }

        /// <summary>
        /// Extension que mapea el registro de una bitacora
        /// </summary>
        /// <param name="datosBitacora"></param>
        /// <param name="fechaSistema"></param>
        /// <returns>Retorna la bitacora mapeada</returns>
        public static BitacoraTransferenciaInmediata RegistrarBitacora(
            SolicitudEstadoPagoSalidaDTO datosBitacora, 
            DateTime fechaSistema)
        {
            return new BitacoraTransferenciaInmediata
            {
                IndicadorBitacora = General.Originante,
                IdentificadorTrama = (int)TipoTramaEnum.SolicitudEstadoPago,
                CodigoEntidadReceptor = datosBitacora.creditorParticipantCode,
                FechaBitacoraOperacion = DateTime.ParseExact(datosBitacora.creationDate + " " + datosBitacora.creationTime,
                                                            "yyyyMMdd HHmmss",
                                                            CultureInfo.InvariantCulture),
                FechaBitacoraRespuesta = DateTime.ParseExact(datosBitacora.originalCreationDate + " " + datosBitacora.originalCreationTime,
                                                            "yyyyMMdd HHmmss",
                                                            CultureInfo.InvariantCulture),
                CodigoMoneda = datosBitacora.currency,
                IdentificadorInstruccion = datosBitacora.instructionId,
                CodigoUsuarioRegistro = General.UsuarioPorDefecto,
                IndicadorEstado = General.Pendiente,
                CodigoSistema = Sistema.CuentaEfectivo,
                FechaRegistro = fechaSistema
            };
        }

        /// <summary>
        /// Extension que mapea la actualizacion de una bitacora
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="datosBitacora"></param>
        /// <param name="fechaSistema"></param>
        public void ActualizarBitacora(
            TransaccionOrdenTransferenciaInmediata transaccion,
            SolicitudEstadoPagoRespuestaDTO datosBitacora,
            DateTime fechaSistema)
        {
            CodigoRespuesta = datosBitacora.responseCode;
            RazonRespuesta = datosBitacora.reasonCode.CompletarTextoAVacio();
            FechaBitacoraRespuesta = DateTime.ParseExact(datosBitacora.responseDate + " " + datosBitacora.responseTime,
                                                             "yyyyMMdd HHmmss",
                                                             CultureInfo.InvariantCulture);
            NumeroDocumentoOriginante = transaccion.NumeroDocumentoIdentidadOriginante;
            NombreOriginante = transaccion.NombreOriginante;
            NumeroDocumentoReceptor = transaccion.NumeroDocumentoIdentidadReceptor;
            NombreReceptor = transaccion.NombreReceptor;
            CodigoEntidadOriginante = datosBitacora.debtorParticipantCode;
            CodigoEntidadReceptor = datosBitacora.creditorParticipantCode;
            CodigoTerminal = datosBitacora.terminalId;
            CodigoNumeroReferencia = datosBitacora.retrievalReferenteNumber.CompletarTextoAVacio();
            NumeroTrace = datosBitacora.trace;
            FechaLiquidacion = DateTime.ParseExact(datosBitacora.settlementDate,
                                                    "yyyyMMdd",
                                                    CultureInfo.InvariantCulture);
            ReferenciaTransaccion = datosBitacora.transactionReference;
            IdentificadorInstruccion = datosBitacora.instructionId;
            MontoImporte = datosBitacora.amount.ObtenerImporteOperacion();
            MontoComision = datosBitacora.feeAmount.ObtenerImporteOperacion();
            TipoTransferencia = datosBitacora.transactionType;
            CodigoCuentaInterbancariaOriginante = datosBitacora.debtorCCI;
            CodigoCuentaInterbancariaReceptor = datosBitacora.creditorCCI.CompletarTextoAVacio();
            NumeroTarjetaReceptor = datosBitacora.creditorCreditCard.CompletarTextoAVacio();
            IndicadorITF = datosBitacora.sameCustomerFlag;
            MontoLiquidacion = datosBitacora.interbankSettlementAmount.ObtenerImporteOperacion();
            IndicadorEstado = datosBitacora.responseCode == CF.CodigoRespuesta.Aceptada ? General.Aceptado :
                datosBitacora.responseCode == CF.CodigoRespuesta.Rechazada ? General.Rechazo : General.NoEncontrado;
            FechaModifico = fechaSistema;
            CodigoUsuarioModifico = General.UsuarioPorDefecto;
        }

        /// <summary>
        /// Extension que mapea el registro de una bitacora
        /// </summary>
        /// <param name="datosBitacora"></param>
        /// <param name="fechaSistema"></param>
        /// <returns>Retorna la bitacora mapeada</returns>
        public static BitacoraTransferenciaInmediata RegistrarBitacora(
            RechazoRecepcionDTO datosBitacora, 
            DateTime fechaSistema)
        {
            return new BitacoraTransferenciaInmediata
            {
                IndicadorBitacora = General.Receptor,
                IdentificadorTrama = (int)TipoTramaEnum.Rechazo,
                DescripcionMensajeOriginal = datosBitacora.originalMessage,
                FechaBitacoraOperacion = DateTime.ParseExact(datosBitacora.responseDate + " " + datosBitacora.responseTime,
                                                            "yyyyMMdd HHmmss",
                                                            CultureInfo.InvariantCulture),
                FechaBitacoraRespuesta = DateTime.ParseExact(datosBitacora.creationDate + " " + datosBitacora.creationTime,
                                                            "yyyyMMdd HHmmss",
                                                            CultureInfo.InvariantCulture),
                CodigoRespuesta = datosBitacora.status,
                RazonRespuesta = datosBitacora.reasonCode,
                IdentificadorInstruccion = datosBitacora.instructionId,
                NumeroTrace = datosBitacora.trace,
                ReferenciaTransaccion = datosBitacora.transactionReference.CompletarTextoAVacio(),
                CodigoEntidadOriginante = datosBitacora.debtorParticipantCode,
                DescripcionDefinicionMensaje = datosBitacora.originalMessageDefinitionIdentifier.CompletarTextoAVacio(),
                CodigoUsuarioRegistro = General.UsuarioPorDefecto,
                IndicadorEstado = General.Rechazo,
                CodigoSistema = Sistema.CuentaEfectivo,
                FechaRegistro = fechaSistema,
                FechaModifico = fechaSistema
            };
        }


        /// <summary>
        /// Registra bitacora de la consulta cuenta
        /// </summary>
        /// <param name="datosBitacora"></param>
        /// <param name="fechaSistema"></param>
        /// <param name="consultaPorQr"></param>
        /// <returns>Retorna Bitacora</returns>
        public static BitacoraTransferenciaInmediata RegistrarBitacora(
            ConsultaCuentaSalidaDTO datosBitacora,
            DateTime fechaSistema,
            bool consultaPorQr)
        {
            return new BitacoraTransferenciaInmediata
            {

                CodigoEntidadOriginante = datosBitacora.debtorParticipantCode,
                CodigoEntidadReceptor = datosBitacora.creditorParticipantCode,
                FechaBitacoraOperacion = DateTime.ParseExact(datosBitacora.creationDate + " " + datosBitacora.creationTime,
                                            "yyyyMMdd HHmmss",
                                            CultureInfo.InvariantCulture),                
                CodigoTerminal = datosBitacora.terminalId,
                CodigoNumeroReferencia = datosBitacora.retrievalReferenteNumber,
                NumeroTrace = datosBitacora.trace,
                NombreOriginante = datosBitacora.debtorName,
                NumeroDocumentoOriginante = datosBitacora.debtorId,
                TipoDocumentoOriginante = datosBitacora.debtorIdCode,
                TelefonoOriginante = datosBitacora.debtorPhoneNumber.CompletarTextoAVacio(),
                CelularOriginante = datosBitacora.debtorMobileNumber.CompletarTextoAVacio(),
                TipoTransferencia = datosBitacora.transactionType,
                CodigoCanal = datosBitacora.channel,
                TelefonoReceptor = datosBitacora.creditorPhoneNumber.CompletarTextoAVacio(),
                CelularReceptor = datosBitacora.creditorMobileNumber.CompletarTextoAVacio(),
                CodigoCuentaInterbancariaReceptor = datosBitacora.creditorCCI.CompletarTextoAVacio(),
                NumeroTarjetaReceptor = datosBitacora.creditorCreditCard.CompletarTextoAVacio(),
                TipoPersonaOriginante = datosBitacora.debtorTypeOfPerson,
                CodigoMoneda = datosBitacora.currency,
                IndicadorBitacora = General.Originante,
                IdentificadorTrama = (int)TipoTramaEnum.ConsultaCuenta,
                FechaRegistro = fechaSistema,
                CodigoUsuarioRegistro = General.UsuarioPorDefecto,
                IndicadorEstado = General.Pendiente,
                CodigoSistema = Sistema.CuentaEfectivo,
                DescripcionMensajeOriginal = consultaPorQr ? General.DescripcionConsultaPorQR : string.Empty,
                IndicadorConsultaQR = consultaPorQr ? General.Si : General.No,
                TipoProxy = datosBitacora.proxyType,
                ValorProxy = datosBitacora.proxyValue
            };
        }
        /// <summary>
        /// Actualiza bitacora de consulta cuenta
        /// </summary>
        /// <param name="bitacora">Bitacora registrada antes del envio</param>
        /// <param name="datosBitacora"datos para la bitaora></param>
        public void ActualizarBitacora(
             ConsultaCuentaRecepcionSalidaDTO datosBitacora,
             DateTime fechaSistema)
        {
            CodigoRespuesta = datosBitacora.responseCode;
            RazonRespuesta = datosBitacora.reasonCode.CompletarTextoAVacio();
            FechaBitacoraRespuesta = fechaSistema;
            DireccionReceptor = datosBitacora.creditorAddressLine.CompletarTextoAVacio();
            TelefonoReceptor = datosBitacora.creditorPhoneNumber.CompletarTextoAVacio();
            CelularReceptor = datosBitacora.creditorMobileNumber.CompletarTextoAVacio();
            ValorProxy = datosBitacora.proxyValue.CompletarTextoAVacio();
            TipoProxy = datosBitacora.proxyType.CompletarTextoAVacio();
            IdentificadorInstruccion = datosBitacora.instructionId;
            NombreReceptor = datosBitacora.creditorName;
            NumeroDocumentoReceptor = datosBitacora.creditorId;
            TipoDocumentoReceptor = datosBitacora.creditorIdCode;
            IndicadorITF = datosBitacora.sameCustomerFlag;
            CodigoSistema = Sistema.CuentaEfectivo;
            IndicadorEstado = datosBitacora.responseCode == CF.CodigoRespuesta.Aceptada ? General.Aceptado :
                datosBitacora.responseCode == CF.CodigoRespuesta.Rechazada ? General.Rechazo : General.NoEncontrado;
            FechaModifico = fechaSistema;
            CodigoUsuarioModifico = General.UsuarioPorDefecto;

        }
        /// <summary>
        /// Registro de bitacora de Orden de transferencia
        /// </summary>
        /// <param name="datosBitacora">Datos de la bitacora</param>
        /// <returns>Retorna Bitacora</returns>
        public static BitacoraTransferenciaInmediata RegistrarBitacora(
            OrdenTransferenciaSalidaDTO datosBitacora, 
            OrdenTransferenciaCanalDTO datosCanal,
            DateTime fechaSistema)
        {
            return new BitacoraTransferenciaInmediata
            {
                CodigoEntidadOriginante = datosBitacora.debtorParticipantCode,
                CodigoEntidadReceptor = datosBitacora.creditorParticipantCode,
                FechaBitacoraOperacion = DateTime.ParseExact(datosBitacora.creationDate + " " + datosBitacora.creationTime,
                            "yyyyMMdd HHmmss",
                            CultureInfo.InvariantCulture),
                CodigoTerminal = datosBitacora.terminalId,
                CodigoNumeroReferencia = datosBitacora.retrievalReferenteNumber.CompletarTextoAVacio(),
                NumeroTrace = datosBitacora.trace,
                CodigoCanal = datosBitacora.channel,
                NombreOriginante = datosBitacora.debtorName,
                NumeroDocumentoOriginante = datosBitacora.debtorId,
                TipoDocumentoOriginante = datosCanal.TipoDocumentoDeudorCCE,
                NumeroDocumentoReceptor = datosCanal.NumeroIdentidadReceptor,
                TipoDocumentoReceptor = datosCanal.TipoDocumentoReceptorCCE,
                TelefonoOriginante = datosBitacora.debtorPhoneNumber.CompletarTextoAVacio(),
                CelularOriginante = datosBitacora.debtorMobileNumber.CompletarTextoAVacio(),
                DireccionOriginante = datosBitacora.debtorAddressLine.CompletarTextoAVacio(),
                TipoTransferencia = datosBitacora.transactionType,
                DireccionReceptor = datosBitacora.creditorAddressLine.CompletarTextoAVacio(),
                TelefonoReceptor = datosBitacora.creditorPhoneNumber.CompletarTextoAVacio(),
                CelularReceptor = datosBitacora.creditorMobileNumber.CompletarTextoAVacio(),
                CodigoCuentaInterbancariaReceptor = datosBitacora.creditorCCI.CompletarTextoAVacio(),
                NumeroTarjetaReceptor = datosBitacora.creditorCreditCard.CompletarTextoAVacio(),
                CodigoMoneda = datosBitacora.currency,
                NombreReceptor = datosBitacora.creditorName,
                IndicadorITF = datosBitacora.sameCustomerFlag,
                MontoImporte = datosBitacora.amount.ObtenerImporteOperacion(),
                ReferenciaTransaccion = datosBitacora.transactionReference,
                IdentificadorReferenciaTransaccion = datosBitacora.referenceTransactionId,
                MontoComision = datosBitacora.feeAmount.ObtenerImporteOperacion(),
                CodigoTarifa = datosBitacora.feeCode,
                CodigoCriterioAplicacion = datosBitacora.applicationCriteria,
                TipoPersonaOriginante = datosBitacora.debtorTypeOfPerson,
                CodigoCuentaInterbancariaOriginante = datosBitacora.debtorCCI.CompletarTextoAVacio(),
                CodigoProposito = datosBitacora.purposeCode.CompletarTextoAVacio(),
                DescripcionInformacionEstructurada = datosBitacora.unstructuredInformation.CompletarTextoAVacio(),
                MontoSalarioBruto = datosBitacora.grossSalaryAmount,
                IndicadorPagoSalario = datosBitacora.salaryPaymentIndicator.CompletarTextoAVacio(),
                MesPago = datosBitacora.monthOfThePayment.CompletarTextoAVacio(),
                AnioPago = datosBitacora.yearOfThePayment.CompletarTextoAVacio(),
                MensajeReenvio = datosBitacora.messageTypeId.CompletarTextoAVacio(),
                IndicadorEstado = General.Pendiente,
                CodigoSistema = Sistema.CuentaEfectivo,
                IndicadorBitacora = General.Originante,
                IdentificadorTrama = (int)TipoTramaEnum.OrdenTransferencia,
                CodigoUsuarioRegistro = datosCanal.UsuarioRegistro,
                CodigoUsuarioModifico = datosCanal.UsuarioRegistro,
                FechaRegistro = fechaSistema,
                FechaModifico = fechaSistema
            };
        }
        /// <summary>
        ///  Actualiza la bitacora despues de la respuesta de la orden de transferencia
        /// </summary>
        /// <param name="bitacora">Bitacora ya registrada antes del envio</param>
        /// <param name="datosBitacora">Datos de orden de transferencia</param>
        public void ActualizarBitacora(
             OrdenTransferenciaRecepcionSalidaDTO datosBitacora,
             DateTime fechaSistema)
        {
            CodigoRespuesta = datosBitacora.responseCode;
            RazonRespuesta = datosBitacora.reasonCode;
            FechaBitacoraRespuesta = DateTime.ParseExact(datosBitacora.responseDate + " " + datosBitacora.responseTime,
                                                        "yyyyMMdd HHmmss",
                                                        CultureInfo.InvariantCulture);
            CodigoCuentaInterbancariaReceptor = datosBitacora.creditorCCI;
            NumeroTarjetaReceptor = datosBitacora.creditorCreditCard;
            IdentificadorInstruccion = datosBitacora.instructionId;
            ReferenciaTransaccion = datosBitacora.transactionReference;
            FechaLiquidacion = DateTime.ParseExact(datosBitacora.settlementDate,
                                                "yyyyMMdd",
                                                CultureInfo.InvariantCulture);
            IndicadorITF = datosBitacora.sameCustomerFlag;
            MontoLiquidacion = datosBitacora.interbankSettlementAmount;
            CodigoSistema = Sistema.CuentaEfectivo;
            IndicadorEstado = datosBitacora.responseCode == CF.CodigoRespuesta.Aceptada
                ? General.Finalizado
                : General.Rechazo;
            FechaModifico = fechaSistema;
            CodigoUsuarioModifico = General.UsuarioPorDefecto;
        }

        /// <summary>
        /// Extension que mapea la actualizacion de una bitacora
        /// </summary>
        /// <param name="identificadorInstruccion"></param>
        /// <param name="estado"></param>
        /// <param name="fechaSistema"></param>
        /// <param name="usuario"></param>
        public void ActualizarBitacora(
            string identificadorInstruccion,
            string estado, Calendario fechaSistema,
            string usuario)
        {
            FechaBitacoraRespuesta = fechaSistema.FechaHoraSistema;
            IdentificadorInstruccion = identificadorInstruccion;
            IndicadorEstado = estado == General.Aceptado
                ? General.Finalizado : General.Rechazo;
            FechaModifico = fechaSistema.FechaHoraSistema;
            CodigoUsuarioModifico = usuario;
        }

        /// <summary>
        /// Extension que mapea la actualizacion de una bitacora
        /// </summary>
        /// <param name="razonBitacora"></param>
        /// <param name="fechaSistema"></param>
        public void ActualizarBitacora(
            string razonBitacora,
            DateTime fechaSistema)
        {
            RazonRespuesta = razonBitacora;
            FechaModifico = fechaSistema;
            IndicadorEstado = General.Rechazo;
        }
        #endregion
    }
}