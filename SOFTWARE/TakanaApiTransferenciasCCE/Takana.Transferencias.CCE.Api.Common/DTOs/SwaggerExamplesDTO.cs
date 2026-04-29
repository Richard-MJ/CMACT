using Swashbuckle.AspNetCore.Filters;
using Takana.Transferencias.CCE.Api.Common.DTOs.BA;
using Takana.Transferencias.CCE.Api.Common.DTOs.CL;
using Takana.Transferencias.CCE.Api.Common.Rechazos;
using Takana.Transferencias.CCE.Api.Common.EchoTest;
using Takana.Transferencias.CCE.Api.Common.SignOnOff;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Common.DTOs.Reporte;
using Takana.Transferencias.CCE.Api.Common.Cancelaciones;
using Takana.Transferencias.CCE.Api.Common.Notificaciones;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;
using Takana.Transferencias.CCE.Api.Common.ReprocesarTransaccion;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad.DatosRegistroDirectorio;

namespace Takana.Transferencias.CCE.Api.Common
{
    public class SwaggerExamplesDTO
    {
        #region EndPoints Entrantes

        #region Consulta Cuenta
        public class EstructuraContenidoAV2Example : IExamplesProvider<EstructuraContenidoAV2>
        {
            public EstructuraContenidoAV2 GetExamples()
            {
                return new EstructuraContenidoAV2
                {
                    AV2 = new ConsultaCuentaRecepcionEntradaDTO
                    {
                        debtorParticipantCode = "0813",
                        creditorParticipantCode = "0813",
                        creationDate = DateTime.Now.ToString("yyyyMMdd"),
                        creationTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                        terminalId = "ABC00001",
                        retrievalReferenteNumber = "010315560014",
                        trace = "923451",
                        debtorName = "Nombre Cliente Originante",
                        debtorId = "20607810274",
                        debtorIdCode = "6",
                        debtorPhoneNumber = "2255784",
                        debtorAddressLine = "Av. Eduardo Ordoñez 379 - 101",
                        debtorMobileNumber = "999913014",
                        transactionType = "320",
                        channel = "15",
                        creditorAddressLine = "Av. Los Pinos 444",
                        creditorPhoneNumber = "3461229",
                        creditorMobileNumber = "999666333",
                        creditorCCI = "81300121110197586451",
                        creditorCreditCard = null,
                        debtorTypeOfPerson = "N",
                        currency = "604",
                        proxyValue = null,
                        proxyType = null,
                        instructionId = "2024633535724318362834412823202",
                        branchId = "1001"
                    }
                };
            }
        }

        public class EstructuraContenidoAV3Example : IExamplesProvider<EstructuraContenidoAV3>
        {
            public EstructuraContenidoAV3 GetExamples()
            {
                return new EstructuraContenidoAV3
                {
                    AV3 = new ConsultaCuentaRespuestaEntradaDTO
                    {
                        responseCode = "00",
                        reasonCode = null,
                        creditorName = "Nom_cliente",
                        creditorId = "001262423",
                        creditorIdCode = "5",
                        sameCustomerFlag = "O",
                        branchId = "1001",
                        instructionId = "2024633535724318362834412823204",
                        creationDate = DateTime.Now.ToString("yyyyMMdd"),
                        creationTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                        debtorName = "Nombre Cliente Originante",
                        debtorId = "20607810274",
                        debtorIdCode = "6",
                        debtorPhoneNumber = "2255784",
                        debtorAddressLine = "Av. Eduardo Ordoñez 379 - 101",
                        debtorMobileNumber = "999913014",
                        channel = "15",
                        creditorAddressLine = "Av. Los Pinos 444",
                        creditorPhoneNumber = "3461229",
                        creditorMobileNumber = "999666333",
                        proxyValue = null,
                        proxyType = null,
                        debtorParticipantCode = "0813",
                        creditorParticipantCode = "0813",
                        terminalId = "ABC00001",
                        retrievalReferenteNumber = "010315560014",
                        trace = "923451",
                        currency = "604",
                        transactionType = "320",
                        creditorCCI = "81300121110197586451",
                        creditorCreditCard = null
                    }
                };
            }
        }
        #endregion

        #region Orden de Transferencia
        public class EstructuraContenidoCT2Example : IExamplesProvider<EstructuraContenidoCT2>
        {
            public EstructuraContenidoCT2 GetExamples()
            {
                return new EstructuraContenidoCT2
                {
                    CT2 = new OrdenTransferenciaRecepcionEntradaDTO
                    {
                        debtorParticipantCode = "0813",
                        creditorParticipantCode = "0813",
                        creationDate = DateTime.Now.ToString("yyyyMMdd"),
                        creationTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                        terminalId = "ABC00001",
                        retrievalReferenteNumber = "010315560014",
                        trace = "888888",
                        channel = "15",
                        amount = 100000,
                        currency = "604",
                        transactionReference = "1234567890",
                        referenceTransactionId = "2024633535724318362834412823202",
                        transactionType = "320",
                        feeAmount = 80,
                        feeCode = "COMM",
                        applicationCriteria = "M",
                        debtorTypeOfPerson = "N",
                        debtorName = "Nombre Cliente Originante",
                        debtorAddressLine = "Av. Eduardo Ordoñez 379 - 101",
                        debtorIdCode = "2",
                        debtorId = "09250444",
                        debtorPhoneNumber = "2255784",
                        debtorMobileNumber = "999913014",
                        debtorCCI = "81300121100286341959",
                        creditorName = "CLIENTE PRUEBAS CCE",
                        creditorAddressLine = "Av. Los Pinos 444",
                        creditorPhoneNumber = "3461229",
                        creditorMobileNumber = "999666333",
                        creditorCCI = "81300121110201215959",
                        creditorCreditCard = null,
                        sameCustomerFlag = "O",
                        purposeCode = "ADMG",
                        unstructuredInformation = "Pago de Mantenimiento Abril",
                        salaryPaymentIndicator = "5",
                        grossSalaryAmount = 80,
                        monthOfThePayment = "11",
                        yearOfThePayment = "2024",
                        branchId = "1001",
                        settlementDate = DateTime.Now.ToString("yyyyMMdd"),
                        instructionId = "45181151324586164538213156225681",
                        interbankSettlementAmount = 100080
                    }
                };
            }
        }

        public class EstructuraContenidoCT3Example : IExamplesProvider<EstructuraContenidoCT3>
        {
            public EstructuraContenidoCT3 GetExamples()
            {
                return new EstructuraContenidoCT3
                {
                    CT3 = new OrdenTransferenciaRespuestaEntradaDTO
                    {
                        responseDate = DateTime.Now.ToString("yyyyMMdd"),
                        responseTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                        responseCode = "00",
                        settlementDate = DateTime.Now.ToString("yyyyMMdd"),
                        reasonCode = null,
                        branchId = "1001",
                        instructionId = "45181151324586164538213156225680",
                        amount = 7500,
                        transactionReference = "1234567890",
                        feeAmount = 80,
                        debtorCCI = "81301421200001678387",
                        sameCustomerFlag = "O",
                        debtorParticipantCode = "0813",
                        creditorParticipantCode = "0813",
                        terminalId = "ABC00001",
                        retrievalReferenteNumber = "010315560014",
                        trace = "888888",
                        currency = "604",
                        transactionType = "320",
                        creditorCCI = "81300121110197586451",
                        creditorCreditCard = null
                    }
                };
            }
        }
        #endregion

        #region Confirmacion
        public class EstructuraContenidoCT5Example : IExamplesProvider<EstructuraContenidoCT5>
        {
            public EstructuraContenidoCT5 GetExamples()
            {
                return new EstructuraContenidoCT5
                {
                    CT5 = new OrdenTransferenciaConfirmacionEntradaDTO
                    {
                        debtorParticipantCode = "0813",
                        creditorParticipantCode = "0813",
                        responseDate = DateTime.Now.ToString("yyyyMMdd"),
                        responseTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                        terminalId = "ABC00001",
                        retrievalReferenteNumber = "010315560014",
                        trace = "123456",
                        amount = 100000,
                        currency = "604",
                        transactionReference = "1234567890",
                        responseCode = "00",
                        feeAmount = 80,
                        settlementDate = "20240705",
                        transactionType = "320",
                        debtorCCI = "81301421200001678387",
                        creditorCCI = "81300121110201215959",
                        creditorCreditCard = null,
                        sameCustomerFlag = "O",
                        instructionId = "45181151324586164538213156225681",
                        creationDate = DateTime.Now.ToString("yyyyMMdd"),
                        creationTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                        channel = "15",
                        interbankSettlementAmount = 100080
                    }
                };
            }
        }
        #endregion

        #region Cancelacion
        public class EstructuraContenidoCTC1Example : IExamplesProvider<EstructuraContenidoCTC1>
        {
            public EstructuraContenidoCTC1 GetExamples()
            {
                return new EstructuraContenidoCTC1
                {
                    CTC1 = new CancelacionRecepcionDTO
                    {
                        creditorParticipantCode = "0813",
                        creationDate = DateTime.Now.ToString("yyyyMMdd"),
                        creationTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                        currency = "604",
                        transactionReference = "1234567890",
                        referenceInstructionId = "6253396153769655854288125",
                        instructionId = "20240310642434223547423427",
                        branchId = "1002"
                    }
                };
            }
        }

        public class EstructuraContenidoCTC2Example : IExamplesProvider<EstructuraContenidoCTC2>
        {
            public EstructuraContenidoCTC2 GetExamples()
            {
                return new EstructuraContenidoCTC2
                {
                    CTC2 = new CancelacionRespuestaDTO
                    {
                        responseDate = DateTime.Now.ToString("yyyyMMdd"),
                        responseTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                        responseCode = "00",
                        reasonCode = null,
                        creditorParticipantCode = "0813",
                        currency = "604",
                        instructionId = "20240310642434223547423427",
                        branchId = "1002"
                    }
                };
            }
        }

        #endregion

        #region Echo Test
        public class EstructuraContenidoET1Example : IExamplesProvider<EstructuraContenidoET1>
        {
            public EstructuraContenidoET1 GetExamples()
            {
                return new EstructuraContenidoET1
                {
                    ET1 = new EchoTestDTO
                    {
                        participantCode = "0813",
                        creationDate = DateTime.Now.ToString("yyyyMMdd"),
                        creationTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                        trace = "123456"
                    }
                };
            }
        }

        public class EstructuraContenidoET2Example : IExamplesProvider<EstructuraContenidoET2>
        {
            public EstructuraContenidoET2 GetExamples()
            {
                return new EstructuraContenidoET2
                {
                    ET2 = new EchoTestRespuestaDTO
                    {
                        responseDate = DateTime.Now.ToString("yyyyMMdd"),
                        responseTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                        status = "00",
                        reasonCode = null,
                        participantCode = "0813",
                        trace = "123456"
                    }
                };
            }
        }
        #endregion

        #region Rechazo
        public class EstructuraContenidoRejectExample : IExamplesProvider<EstructuraContenidoReject>
        {
            public EstructuraContenidoReject GetExamples()
            {
                return new EstructuraContenidoReject
                {
                    Reject = new RechazoRecepcionDTO
                    {
                        originalMessage = "XASASDA….",
                        responseDate = DateTime.Now.ToString("yyyyMMdd"),
                        responseTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                        status = "05",
                        reasonCode = "AB05",
                        instructionId = "20160614654331616615000005",
                        trace = "123456",
                        transactionReference = "1234567890",
                        creationDate = "20240311",
                        creationTime = "101000",
                        debtorParticipantCode = "0813",
                        originalMessageDefinitionIdentifier = "pacs.008.001.08",
                        terminalId = "ABC00001"
                    }
                };
            }
        }

        #endregion

        #region Mensaje de Notificación
        public class EstructuraContenidoSNM971Example : IExamplesProvider<EstructuraContenidoSNM971>
        {
            public EstructuraContenidoSNM971 GetExamples()
            {
                return new EstructuraContenidoSNM971
                {
                    SNM971 = new Notificacion971DTO
                    {
                        eventCode = "971",
                        eventDate = DateTime.Now.ToString("yyyyMMdd"),
                        eventTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                        messageIdentification = "M202102041937000002H1070484",
                        messageClass = "Broadcast",
                        newServiceStatus = "NORMAL"
                    }
                };
            }
        }

        public class EstructuraContenidoSNM972Example : IExamplesProvider<EstructuraContenidoSNM972>
        {
            public EstructuraContenidoSNM972 GetExamples()
            {
                return new EstructuraContenidoSNM972
                {
                    SNM972 = new Notificacion972DTO
                    {
                        eventCode = "972",
                        eventDate = DateTime.Now.ToString("yyyyMMdd"),
                        eventTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                        messageIdentification = "M202102041937000002H1121167",
                        messageClass = "Broadcast",
                        currency = "604",
                        memberIdentification = "0813",
                        name = "Caja Tacna",
                        status = "NORMAL"
                    }
                };
            }
        }

        public class EstructuraContenidoSNM981Example : IExamplesProvider<EstructuraContenidoSNM981>
        {
            public EstructuraContenidoSNM981 GetExamples()
            {
                return new EstructuraContenidoSNM981
                {
                    SNM981 = new Notificacion981DTO
                    {
                        eventCode = "981",
                        eventDate = DateTime.Now.ToString("yyyyMMdd"),
                        eventTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                        messageIdentification = "M202102041937000002H1151282",
                        messageClass = "Notification",
                        eventDescription = "Mensaje…"
                    }
                };
            }
        }

        public class EstructuraContenidoSNM982Example : IExamplesProvider<EstructuraContenidoSNM982>
        {
            public EstructuraContenidoSNM982 GetExamples()
            {
                return new EstructuraContenidoSNM982
                {
                    SNM982 = new Notificacion982DTO
                    {
                        eventCode = "982",
                        eventDate = DateTime.Now.ToString("yyyyMMdd"),
                        eventTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                        messageIdentification = "M202102041937000002H1241182",
                        messageClass = "Broadcast",
                        participantIdentification = "0002",
                        participantName = "BCP",
                        participantStatus = "SIGNON"
                    }
                };
            }
        }

        public class EstructuraContenidoSNM993Example : IExamplesProvider<EstructuraContenidoSNM993>
        {
            public EstructuraContenidoSNM993 GetExamples()
            {
                return new EstructuraContenidoSNM993
                {
                    SNM993 = new Notificacion993DTO
                    {
                        eventCode = "993",
                        eventDate = DateTime.Now.ToString("yyyyMMdd"),
                        eventTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                        messageIdentification = "M202102041937000002H1160789",
                        messageClass = "Notification",
                        bankName = "BCP",
                        currency = "604",
                        availablePrefundBalanceStatus = "NORMAL",
                        openingPrefundedBalance = 1000000000,
                        lowWatermarkValue = 250000000,
                        highWatermarkValue = 300000000,
                        availablePrefundedBalance = 340000000
                    }
                };
            }
        }

        public class EstructuraContenidoSNM998Example : IExamplesProvider<EstructuraContenidoSNM998>
        {
            public EstructuraContenidoSNM998 GetExamples()
            {
                return new EstructuraContenidoSNM998
                {
                    SNM998 = new Notificacion998DTO
                    {
                        eventCode = "998",
                        eventDate = DateTime.Now.ToString("yyyyMMdd"),
                        eventTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                        messageIdentification = "M202102041937000002H1270656",
                        messageClass = "Notification",
                        memberIdentification = "0813",
                        bankName = "CMAC TACNA",
                        currency = "604",
                        prefundedBalanceChangeStatus = "SUCCESS",
                        newPrefundedBalance = 5000000000,
                        previousPrefundedBalance = 2400000000
                    }
                };
            }
        }

        public class EstructuraContenidoSNM999Example : IExamplesProvider<EstructuraContenidoSNM999>
        {
            public EstructuraContenidoSNM999 GetExamples()
            {
                return new EstructuraContenidoSNM999
                {
                    SNM999 = new Notificacion999DTO
                    {
                        eventCode = "999",
                        eventDate = DateTime.Now.ToString("yyyyMMdd"),
                        eventTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                        messageIdentification = "M202102041937000002H1030555",
                        messageClass = "Notification",
                        previousSettlementWindowDate = "20210104",
                        previousSettlementCycleStatus = "COMPLETE",
                        reconciliationCheckpointCutOffDateAndTime = "20210104163515",
                        newSettlementWindowDate = "20210105",
                        newSettlementWindowStatus = "INPROGRESS",
                        currency = "604",
                        memberIdentification = "0002",
                        bankName = "BCP",
                        previousOpeningPrefundedBalance = 2400000000,
                        newOpeningPrefundedBalance = 5000000000,
                        numberOfCreditTransferReceivedAndAccepted = "3000",
                        valueOfCreditTransferReceivedAndAccepted = 1200000000,
                        numberOfCreditTransferReceivedAndRejected = "50",
                        valueOfCreditTransferReceivedAndRejected = 50000000,
                        numberOfCreditTransferSentAndAccepted = "3100",
                        valueOfCreditTransferSentAndAccepted = 1100000000,
                        numberCreditTransferSentAndRejected = "55",
                        valueOfCreditTransferSentAndRejected = 600000,
                        netPosition = 100000000,
                        countOfSupplementalFunding = "1",
                        grossValueOfSupplementalFunding = 800000,
                        countOfDrawdowns = "1",
                        grossValueOfDrawdowns = 100000
                    }
                };
            }
        }

        #endregion

        #region Reprocesar
        public class ReprocesarTransaccionDTOExample : IExamplesProvider<List<ReprocesarTransaccionDTO>>
        {
            public List<ReprocesarTransaccionDTO> GetExamples()
            {
                return new List<ReprocesarTransaccionDTO>
                {
                    new ReprocesarTransaccionDTO
                    {
                        IdTransaccion = 4,
                        IdArchivoMovimiento = 3,
                        Usuario = "MRAMOS"
                    },
                    new ReprocesarTransaccionDTO
                    {
                        IdTransaccion = 5,
                        IdArchivoMovimiento = 6,
                        Usuario = "MRAMOS"
                    },
                };
            }
        }
        #endregion

        public class stringExample : IExamplesProvider<string>
        {
            public string GetExamples()
            {
                return "45181151324586164538213156225681";
            }
        }

        #endregion

        #region EndPoint Salientes

        #region Afiliacion y desafiliacion interoperabilidad
        public class EntradaAfiliacionDirectorioDTOExample : IExamplesProvider<EntradaAfiliacionDirectorioDTO>
        {
            public EntradaAfiliacionDirectorioDTO GetExamples()
            {
                return new EntradaAfiliacionDirectorioDTO
                {
                    CodigoEntidadOriginante = "0813",
                    TipoInstruccion = "NEWR",
                    CodigoCuentaInterbancario = "81300121110201065255",
                    NumeroCelular = "999706011",
                    TipoOperacion = "19",
                    CodigoCliente = "114019106641",
                    NumeroCuentaAfiliada = "001211102010652",
                    NumeroAntiguo = "",
                    NumeroTarjeta = "4772000012390041",
                    IndicadorModificarNumero = "N",
                    Canal = "A"
                };
            }
        }

        public class RespuestaAfiliacionCCEDTOExample : IExamplesProvider<RespuestaAfiliacionCCEDTO>
        {
            public RespuestaAfiliacionCCEDTO GetExamples()
            {
                return new RespuestaAfiliacionCCEDTO
                {
                    FechaOperacion = DateTime.Now,
                    CadenaHash = "000201010211263700028001039030220240903081319934712905204482953036045802PE5930APELLIDO1 APELLIDO2 PRUEBA UNO6004Lima80550003ID10144DPAtSs54iL8VE/qn/CDAGXBha3zFc4y2v4VdlUOwhDo=63042924"
                };
            }
        }

        public class EntradaAfiliacionDirectorioExample : IExamplesProvider<EntradaAfiliacionDirectorioDTO>
        {
            public EntradaAfiliacionDirectorioDTO GetExamples()
            {
                return new EntradaAfiliacionDirectorioDTO
                {
                    CodigoEntidadOriginante = "0813",
                    TipoInstruccion = "DEAC",
                    CodigoCuentaInterbancario = "81300121110201065255",
                    NumeroCelular = "999706011",
                    TipoOperacion = "19",
                    CodigoCliente = "114019106641",
                    NumeroCuentaAfiliada = "001211102010652",
                    NumeroAntiguo = "",
                    NumeroTarjeta = "4772000012390041",
                    IndicadorModificarNumero = "N",
                    Canal = "A"
                };

            }
        }

        public class RespuestaAfiliacionCCEExample : IExamplesProvider<RespuestaAfiliacionCCEDTO>
        {
            public RespuestaAfiliacionCCEDTO GetExamples()
            {
                return new RespuestaAfiliacionCCEDTO
                {
                    FechaOperacion = DateTime.Now,
                };
            }
        }
        #endregion

        #region Barrido de contactos
        public class EntradaBarridoDTOExample : IExamplesProvider<EntradaBarridoDTO>
        {
            public EntradaBarridoDTO GetExamples()
            {
                return new EntradaBarridoDTO
                {
                    CodigoCCI = "81300121110201065255",
                    NumeroCelularOrigen = "999706011",
                    ContactosBarrido = new List<ContactosBarrido> { new ContactosBarrido { NumeroCelular = "999706011" } }
                };
            }
        }

        public class ResultadoPrincipalBarridoDTOExample : IExamplesProvider<ResultadoPrincipalBarridoDTO>
        {
            public ResultadoPrincipalBarridoDTO GetExamples()
            {
                return new ResultadoPrincipalBarridoDTO
                {
                    LimiteMontoMaximo = 12000.0m,
                    LimiteMontoMinimo = 1.0m,
                    MontoMaximoDia = 20000.0m,
                    ResultadosBarrido = new List<ResultadoBarridoDTO>
                    {
                        new ResultadoBarridoDTO
                        {
                            NumeroCelular = "+51954033257",
                            EntidadesReceptor = new List<EntidadesReceptorAfiliado>
                            {
                                new EntidadesReceptorAfiliado
                                {
                                    CodigoEntidad = "0775",
                                    NombreEntidad = "LUQEA"
                                }
                            }
                        }
                    }
                };
            }
        }

        #endregion

        #region Operaciones Ventanilla
        public class ClienteOriginanteDTOExample : IExamplesProvider<string>
        {
            public string GetExamples()
            {
                return "001211102010652";
            }
        }

        public class RespuetaConsultaCuentaVentanillaDTOExample : IExamplesProvider<CuentaEfectivoDTO>
        {
            public CuentaEfectivoDTO GetExamples()
            {
                return new CuentaEfectivoDTO
                {
                    NumeroCuenta = "001211102010652",
                    Titular = "APELLIDO1 APELLIDO2 PRUEBA UNO",
                    TipoProductoInterno = "CC001 - CTA AHORRO PN SOLES",
                    Moneda = "1 - SOLES",
                    TipoCuentaTitular = "I - Individual",
                    ExoneradoImpuestos = false,
                    CodigoCliente = "114019106641",
                    IndicadorCuentaSueldo = false,
                    MontoRemunerativo = 0,
                    MontoNoremunerativo = 0,
                    CodigoProducto = "CC001",
                    EsExoneradoCobroComisiones = false,
                    NombreProducto = "CTA AHORRO PN SOLES",
                    CodigoMoneda = "1",
                    CodigoCuentaInterbancario = "81300121110201065255",
                    IndicadorTipoCuenta = "I",
                    Nombres = "PRUEBA UNO",
                    ApellidoMaterno = "APELLIDO2",
                    ApellidoPaterno = "APELLIDO1",
                    EsMismoFirmante = false,
                    IndicadorTransferenciaCce = "S",
                    SaldoDisponible = 10059.01m,
                    NumeroDocumento = "45002920",
                    TipoDocumentoOriginante = new TipoDocumentoTinDTO
                    {
                        CodigoTipoDocumento = 1,
                        CodigoTipoDocumentoCCE = 0,
                        DescripcionTipoDocumento = null,
                        EsTipoPersonaJuridica = false,
                        CodigoTipoDocumentoCceTransferenciasInmediatas = null
                    },
                    MontoMinimo = 1465.00m,
                    EsExoneradaITF = false,
                    EsCuentaSueldo = false
                    
                };
            }
        }

        public class ConsultaCuentaOperacionVDTOExample : IExamplesProvider<ConsultaCuentaOperacionDTO>
        {
            public ConsultaCuentaOperacionDTO GetExamples()
            {
                return new ConsultaCuentaOperacionDTO
                {
                    CuentaEfectivoDTO = new CuentaEfectivoDTO
                    {
                        NumeroCuenta = "001211102010652",
                        Titular = "APELLIDO1 APELLIDO2 PRUEBA UNO",
                        TipoProductoInterno = "CC001 - CTA AHORRO PN SOLES",
                        Moneda = "1 - SOLES",
                        TipoCuentaTitular = "I - Individual",
                        ExoneradoImpuestos = false,
                        CodigoCliente = "114019106641",
                        IndicadorCuentaSueldo = false,
                        MontoRemunerativo = 0,
                        MontoNoremunerativo = 0,
                        CodigoProducto = "CC001",
                        EsExoneradoCobroComisiones = false,
                        NombreProducto = "CTA AHORRO PN SOLES",
                        CodigoMoneda = "1",
                        CodigoCuentaInterbancario = "81300121110201065255",
                        IndicadorTipoCuenta = "I",
                        Nombres = "PRUEBA UNO",
                        ApellidoMaterno = "APELLIDO2",
                        ApellidoPaterno = "APELLIDO1",
                        EsMismoFirmante = false,
                        IndicadorTransferenciaCce = "S",
                        SaldoDisponible = 10059.01m,
                        NumeroDocumento = "45002920",
                        TipoDocumentoOriginante = new TipoDocumentoTinDTO
                        {
                            CodigoTipoDocumento = 1,
                            CodigoTipoDocumentoCCE = 0,
                            DescripcionTipoDocumento = null,
                            EsTipoPersonaJuridica = false,
                            CodigoTipoDocumentoCceTransferenciasInmediatas = null
                        },
                        MontoMinimo = 1465.00m,
                        EsExoneradaITF = false,
                        EsCuentaSueldo = false

                    },
                    TipoTransferencia = "320",
                    NumeroCuentaOTarjeta = "7894561231321456",
                    EntidadReceptora = new EntidadFinancieraTinDTO()
                    {
                        CodigoEstadoSign = "12",
                        CodigoEntidad = "0002",
                        NombreEntidad = "BCP",
                        OficinaPagoTarjeta = "001",
                    },
                    Usuario  = new SesionUsuarioDTO
                    {
                        NombreEquipo = "equipo",
                        CodigoCanalOrigen = General.CanalCCE,
                        CodigoAgencia = "01"
                    }
                };
            }
        }

        public class ResultadoConsultaCuentaCCEExample : IExamplesProvider<ResultadoConsultaCuentaCCE>
        {
            public ResultadoConsultaCuentaCCE GetExamples()
            {
                return new ResultadoConsultaCuentaCCE
                {
                    CodigoCuentaTransaccion = "81300121110201065255",
                    CodigoEntidadOriginante = "0813",
                    CodigoEntidadReceptora = "0813",
                    FechaCreacionTransaccion = "20240925",
                    HoraCreacionTransaccion = "183644",
                    NumeroReferencia = "092518364487",
                    Trace = "005927",
                    Canal = "90",
                    CodigoMoneda = "604",
                    CodigoTransferencia = "2024092518364408138190005927",
                    CriterioPlaza = "M",
                    TipoPersonaDeudor = "N",
                    NommbreDeudor = "Luis",
                    TipoDocumentoDeudor = "2",
                    NumeroIdentidadDeudor = "75599370",
                    NumeroCelularDeudor = null,
                    CodigoCuentaInterbancariaDeudor = "81300121110200936970",
                    NombreReceptor = "APELLIDO1 APELLIDO2 PRUEBA UNO",
                    DireccionReceptor = null,
                    NumeroTelefonoReceptor = null,
                    NumeroCelularReceptor = null,
                    CodigoCuentaInterbancariaReceptor = "81300121110201065255",
                    CodigoTarjetaReceptor = null,
                    IndicadorITF = "O",
                    Plaza = "M",
                    TipoTransaccion = "320",
                    InstruccionId = "2024092518364408138190005927",
                    TipoDocumentoReceptor = "2",
                    NumeroIdentidadReceptor = "45002920",
                    MismoTitular = "O",
                    Comision = new ComisionDTO
                    {
                        Id = 0,
                        IdTipoTransferencia = 1,
                        CodigoComision = "0101",
                        CodigoMoneda = "1",
                        CodigoAplicacionTarifa = "M",
                        Porcentaje = 0.00m,
                        Minimo = 15.00m,
                        Maximo = 15.00m,
                        IndicadorPorcentaje = "N",
                        IndicadorFijo = "S",
                        PorcentajeCCE = 0.00m,
                        MinimoCCE = 0.80m,
                        MaximoCCE = 0.80m
                    }
                };
            }
        }

        public class CalculoComisionDTOxample : IExamplesProvider<CalculoComisionDTO>
        {
            public CalculoComisionDTO GetExamples()
            {
                return new CalculoComisionDTO
                {
                    MismoTitular = "M",
                    SaldoActual = 90000m,
                    MontoOperacion = 1.0m,
                    MontoMinimoCuenta = 10.0m,
                    EsExoneradaITF = true,
                    EsCuentaSueldo = true,
                    Comision = new ComisionDTO
                    {
                        Id = 0,
                        IdTipoTransferencia = 1,
                        CodigoComision = "0101",
                        CodigoMoneda = "1",
                        CodigoAplicacionTarifa = "M",
                        Porcentaje = 0.00m,
                        Minimo = 15.00m,
                        Maximo = 15.00m,
                        IndicadorPorcentaje = "N",
                        IndicadorFijo = "S",
                        PorcentajeCCE = 0.00m,
                        MinimoCCE = 0.80m,
                        MaximoCCE = 0.80m
                    }
                };
            }
        }

        public class CalculoComisionRespuestaDTOxample : IExamplesProvider<CalculoComisionDTO>
        {
            public CalculoComisionDTO GetExamples()
            {
                return new CalculoComisionDTO
                {
                    MismoTitular = "M",
                    SaldoActual = 90000m,
                    MontoOperacion = 1.0m,
                    MontoMinimoCuenta = 10.0m,
                    EsExoneradaITF = true,
                    EsCuentaSueldo = true,
                    Comision = new ComisionDTO
                    {
                        Id = 0,
                        IdTipoTransferencia = 1,
                        CodigoComision = "0101",
                        CodigoMoneda = "1",
                        CodigoAplicacionTarifa = "M",
                        Porcentaje = 0.00m,
                        Minimo = 15.00m,
                        Maximo = 15.00m,
                        IndicadorPorcentaje = "N",
                        IndicadorFijo = "S",
                        PorcentajeCCE = 0.00m,
                        MinimoCCE = 0.80m,
                        MaximoCCE = 0.80m
                    },
                    ControlMonto = new ControlMontoDTO
                    {
                        CodigoMoneda = null,
                        CodigoMonedaOrigen = null,
                        Monto = 1.0m,
                        MontoComisionEntidad = 0.0m,
                        MontoComisionCce = 0.80m,
                        Itf = 0,
                        TotalComision = 0.0m,
                        Total = 1.0m
                    }
                };
            }
        }
        public class ValidarTransaccionInmediataDTOExample : IExamplesProvider<ValidarTransaccionInmediataDTO>
        {
            public ValidarTransaccionInmediataDTO GetExamples()
            {
                return new ValidarTransaccionInmediataDTO
                {
                    CodigoTipoTransferenciaCce = "320",
                    CodigoMoneda = "1",
                    SaldoActual = 1000m,
                    MontoOperacion = 100
                };
            }
        }

        public class RespuestaValidacionExample : IExamplesProvider<string>
        {
            public string GetExamples()
            {
                return "00";
            }
        }

        public class TransferenciaVentanillaExample : IExamplesProvider<OrdenTransferenciaVentanillaDTO>
        {
            public OrdenTransferenciaVentanillaDTO GetExamples()
            {
                return new OrdenTransferenciaVentanillaDTO
                {
                    DatosConsultaCuentaCCE =  new ResultadoConsultaCuentaCCE
                    {
                        CodigoCuentaTransaccion = "81300121110201065255",
                        CodigoEntidadOriginante = "0813",
                        CodigoEntidadReceptora = "0813",
                        FechaCreacionTransaccion = "20240925",
                        HoraCreacionTransaccion = "183644",
                        NumeroReferencia = "092518364487",
                        Trace = "005927",
                        Canal = "90",
                        CodigoMoneda = "604",
                        CodigoTransferencia = "2024092518364408138190005927",
                        CriterioPlaza = "M",
                        TipoPersonaDeudor = "N",
                        NommbreDeudor = "Luis",
                        TipoDocumentoDeudor = "2",
                        NumeroIdentidadDeudor = "75599370",
                        NumeroCelularDeudor = null,
                        CodigoCuentaInterbancariaDeudor = "81300121110200938070",
                        NombreReceptor = "APELLIDO1 APELLIDO2 PRUEBA UNO",
                        DireccionReceptor = null,
                        NumeroTelefonoReceptor = null,
                        NumeroCelularReceptor = null,
                        CodigoCuentaInterbancariaReceptor = "81300121110201065255",
                        CodigoTarjetaReceptor = null,
                        IndicadorITF = "O",
                        Plaza = "M",
                        TipoTransaccion = "320",
                        InstruccionId = "2024092518364408138190005927",
                        TipoDocumentoReceptor = "2",
                        NumeroIdentidadReceptor = "45002920",
                        MismoTitular = "O",
                        Comision = new ComisionDTO
                        {
                            Id = 0,
                            IdTipoTransferencia = 1,
                            CodigoComision = "0101",
                            CodigoMoneda = "1",
                            CodigoAplicacionTarifa = "M",
                            Porcentaje = 0.00m,
                            Minimo = 15.00m,
                            Maximo = 15.00m,
                            IndicadorPorcentaje = "N",
                            IndicadorFijo = "S",
                            PorcentajeCCE = 0.00m,
                            MinimoCCE = 0.80m,
                            MaximoCCE = 0.80m
                        }
                    },
                    SesionUsuario = new SesionUsuarioDTO
                    {
                        NombreEquipo = "equipo",
                        CodigoCanalOrigen = General.CanalCCE,
                        CodigoAgencia = "01"
                    },
                    ControlMonto = new ControlMontoDTO
                    {
                        CodigoMoneda = null,
                        CodigoMonedaOrigen = null,
                        Monto = 1.0m,
                        MontoComisionEntidad = 0.0m,
                        MontoComisionCce = 0.80m,
                        Itf = 0,
                        TotalComision = 0.0m,
                        Total = 1.0m
                    },
                    NumeroCuenta = "",
                    NumeroLavado = 123456,
                    CodigoUsuario = "CAJEROCCE",
                    ConceptoCobroTarifa = "OTHR"
                };
            }
        }

        public class TransferenciaVentanillarRespuestaExample : IExamplesProvider<string>
        {
            public string GetExamples()
            {
                return "123456";
            }
        }

        #endregion

        #region Operaciones Interoperabilidad
        public class ConsultaCuentaOriginanteDTOExample : IExamplesProvider<ConsultaCuentaOriginanteDTO>
        {
            public ConsultaCuentaOriginanteDTO GetExamples()
            {
                return new ConsultaCuentaOriginanteDTO
                {
                    NumeroCuenta = "001211102010652"
                };
            }
        }
        public class RespuetaConsultaCuentaDTOExample : IExamplesProvider<RespuetaConsultaCuentaDTO>
        {
            public RespuetaConsultaCuentaDTO GetExamples()
            {
                return new RespuetaConsultaCuentaDTO
                {
                    CuentaEfectivo = new CuentaEfectivoDTO
                    {
                        NumeroCuenta = "001211102010652",
                        Titular = "APELLIDO1 APELLIDO2 PRUEBA UNO",
                        TipoProductoInterno = "CC001 - CTA AHORRO PN SOLES",
                        Moneda = "1 - SOLES",
                        TipoCuentaTitular = "I - Individual",
                        ExoneradoImpuestos = false,
                        CodigoCliente = "114019106641",
                        IndicadorCuentaSueldo = false,
                        MontoRemunerativo = 0,
                        MontoNoremunerativo = 0,
                        CodigoProducto = "CC001",
                        EsExoneradoCobroComisiones = false,
                        NombreProducto = "CTA AHORRO PN SOLES",
                        CodigoMoneda = "1",
                        CodigoCuentaInterbancario = "81300121110201065255",
                        IndicadorTipoCuenta = "I",
                        Nombres = "PRUEBA UNO",
                        ApellidoMaterno = "APELLIDO2",
                        ApellidoPaterno = "APELLIDO1",
                        EsMismoFirmante = false,
                        IndicadorTransferenciaCce = "S",
                        SaldoDisponible = 10059.01m,
                        NumeroDocumento = "45002920",
                        TipoDocumentoOriginante = new TipoDocumentoTinDTO
                        {
                            CodigoTipoDocumento = 1,
                            CodigoTipoDocumentoCCE = 0,
                            DescripcionTipoDocumento = null,
                            EsTipoPersonaJuridica = false,
                            CodigoTipoDocumentoCceTransferenciasInmediatas = null
                        },
                        MontoMinimo = 1465.00m,
                        EsExoneradaITF = false,
                        EsCuentaSueldo = false
                    }
                };
            }
        }

        public class ConsultaCuentaCelularDTOExample : IExamplesProvider<ConsultaCuentaCelularDTO>
        {
            public ConsultaCuentaCelularDTO GetExamples()
            {
                return new ConsultaCuentaCelularDTO
                {
                    NumeroCelular = "999706011",
                    CodigoEntidad = "0091",
                    CuentaEfectivo = new CuentaEfectivoDTO
                    {
                        NumeroCuenta = "001211102010652",
                        Titular = "APELLIDO1 APELLIDO2 PRUEBA UNO",
                        TipoProductoInterno = "CC001 - CTA AHORRO PN SOLES",
                        Moneda = "1 - SOLES",
                        TipoCuentaTitular = "I - Individual",
                        ExoneradoImpuestos = false,
                        CodigoCliente = "114019106641",
                        IndicadorCuentaSueldo = false,
                        MontoRemunerativo = 0,
                        MontoNoremunerativo = 0,
                        CodigoProducto = "CC001",
                        EsExoneradoCobroComisiones = false,
                        NombreProducto = "CTA AHORRO PN SOLES",
                        CodigoMoneda = "1",
                        CodigoCuentaInterbancario = "81300121110201065255",
                        IndicadorTipoCuenta = "I",
                        Nombres = "PRUEBA UNO",
                        ApellidoMaterno = "APELLIDO2",
                        ApellidoPaterno = "APELLIDO1",
                        EsMismoFirmante = false,
                        IndicadorTransferenciaCce = "S",
                        SaldoDisponible = 10059.01m,
                        NumeroDocumento = "45002920",
                        TipoDocumentoOriginante = new TipoDocumentoTinDTO
                        {
                            CodigoTipoDocumento = 1,
                            CodigoTipoDocumentoCCE = 0,
                            DescripcionTipoDocumento = null,
                            EsTipoPersonaJuridica = false,
                            CodigoTipoDocumentoCceTransferenciasInmediatas = null
                        },
                        MontoMinimo = 1465.00m,
                        EsExoneradaITF = false,
                        EsCuentaSueldo = false
                    }
                };
            }
        }

        public class ResultadoConsultaCuentaInteroperabilidadDTOExample : IExamplesProvider<ResultadoConsultaCuentaInteroperabilidadDTO>
        {
            public ResultadoConsultaCuentaInteroperabilidadDTO GetExamples()
            {
                return new ResultadoConsultaCuentaInteroperabilidadDTO
                {
                    ResultadoConsultaCuenta = new ResultadoConsultaCuentaCCE
                    {
                        CodigoCuentaTransaccion = "81300121110201065255",
                        CodigoEntidadOriginante = "0813",
                        CodigoEntidadReceptora = "0813",
                        FechaCreacionTransaccion = "20240925",
                        HoraCreacionTransaccion = "183644",
                        NumeroReferencia = "092518364487",
                        Trace = "005927",
                        Canal = "90",
                        CodigoMoneda = "604",
                        CodigoTransferencia = "2024092518364408138190005927",
                        CriterioPlaza = "M",
                        TipoPersonaDeudor = "N",
                        NommbreDeudor = "Luis",
                        TipoDocumentoDeudor = "2",
                        NumeroIdentidadDeudor = "75599370",
                        NumeroCelularDeudor = null,
                        CodigoCuentaInterbancariaDeudor = "81300121110200936970",
                        NombreReceptor = "APELLIDO1 APELLIDO2 PRUEBA UNO",
                        DireccionReceptor = null,
                        NumeroTelefonoReceptor = null,
                        NumeroCelularReceptor = null,
                        CodigoCuentaInterbancariaReceptor = "81300121110201065255",
                        CodigoTarjetaReceptor = null,
                        IndicadorITF = "O",
                        Plaza = "M",
                        TipoTransaccion = "320",
                        InstruccionId = "2024092518364408138190005927",
                        TipoDocumentoReceptor = "2",
                        NumeroIdentidadReceptor = "45002920",
                        MismoTitular = "O",
                        Comision = new ComisionDTO
                        {
                            Id = 0,
                            IdTipoTransferencia = 1,
                            CodigoComision = "0101",
                            CodigoMoneda = "1",
                            CodigoAplicacionTarifa = "M",
                            Porcentaje = 0.00m,
                            Minimo = 15.00m,
                            Maximo = 15.00m,
                            IndicadorPorcentaje = "N",
                            IndicadorFijo = "S",
                            PorcentajeCCE = 0.00m,
                            MinimoCCE = 0.80m,
                            MaximoCCE = 0.80m
                        }
                    }
                };
            }
        }

        public class ConsultaCuentaCompletaQRDTOExample : IExamplesProvider<ConsultaCuentaCompletaQRDTO>
        {
            public ConsultaCuentaCompletaQRDTO GetExamples()
            {
                return new ConsultaCuentaCompletaQRDTO
                {
                    NumeroCuentaOriginante = "001211102009370",
                    CadenaHash = "0002010102113932a119272fa7b354c29f3a76b25bc2740a5204561153036045802PE5906YAPERO6004Lima63040E99"
                };
            }
        }

        public class RespuestaConsultaCompletaQRExample : IExamplesProvider<RespuestaConsultaCompletaQR>
        {
            public RespuestaConsultaCompletaQR GetExamples()
            {
                return new RespuestaConsultaCompletaQR
                {
                    DatosConsulta = new ResultadoConsultaCuentaInteroperabilidadDTO
                    {
                        ResultadoConsultaCuenta = new ResultadoConsultaCuentaCCE
                        {
                            CodigoCuentaTransaccion = "81300121110201065255",
                            CodigoEntidadOriginante = "0813",
                            CodigoEntidadReceptora = "0813",
                            FechaCreacionTransaccion = "20240925",
                            HoraCreacionTransaccion = "183644",
                            NumeroReferencia = "092518364487",
                            Trace = "005927",
                            Canal = "90",
                            CodigoMoneda = "604",
                            CodigoTransferencia = "2024092518364408138190005927",
                            CriterioPlaza = "M",
                            TipoPersonaDeudor = "N",
                            NommbreDeudor = "Luis",
                            TipoDocumentoDeudor = "2",
                            NumeroIdentidadDeudor = "75599370",
                            NumeroCelularDeudor = null,
                            CodigoCuentaInterbancariaDeudor = "81300121110200938070",
                            NombreReceptor = "APELLIDO1 APELLIDO2 PRUEBA UNO",
                            DireccionReceptor = null,
                            NumeroTelefonoReceptor = null,
                            NumeroCelularReceptor = null,
                            CodigoCuentaInterbancariaReceptor = "81300121110201065255",
                            CodigoTarjetaReceptor = null,
                            IndicadorITF = "O",
                            Plaza = "M",
                            TipoTransaccion = "320",
                            InstruccionId = "2024092518364408138190005927",
                            TipoDocumentoReceptor = "2",
                            NumeroIdentidadReceptor = "45002920",
                            MismoTitular = "O",
                            Comision = new ComisionDTO
                            {
                                Id = 0,
                                IdTipoTransferencia = 1,
                                CodigoComision = "0101",
                                CodigoMoneda = "1",
                                CodigoAplicacionTarifa = "M",
                                Porcentaje = 0.00m,
                                Minimo = 15.00m,
                                Maximo = 15.00m,
                                IndicadorPorcentaje = "N",
                                IndicadorFijo = "S",
                                PorcentajeCCE = 0.00m,
                                MinimoCCE = 0.80m,
                                MaximoCCE = 0.80m
                            }
                        }
                    },
                    NombreEntidadReceptora = "CMAC Tacna",
                    IdentificadorQR = "24090308131993471290",
                    LimiteMontoMaximo = 12000m,
                    LimiteMontoMinimo = 1m,
                    MontoMaximoDia = 20000m
                };
            }
        }

        public class CalculoComisionDTOExample : IExamplesProvider<CalculoComisionDTO>
        {
            public CalculoComisionDTO GetExamples()
            {
                return new CalculoComisionDTO
                {

                    MismoTitular = "M",
                    SaldoActual = 90000m,
                    MontoOperacion = 1.0m,
                    MontoMinimoCuenta = 10.0m,
                    EsExoneradaITF = true,
                    EsCuentaSueldo = true,
                    Comision = new ComisionDTO
                    {
                        Id = 0,
                        IdTipoTransferencia = 1,
                        CodigoComision = "0101",
                        CodigoMoneda = "1",
                        CodigoAplicacionTarifa = "M",
                        Porcentaje = 0.00m,
                        Minimo = 15.00m,
                        Maximo = 15.00m,
                        IndicadorPorcentaje = "N",
                        IndicadorFijo = "S",
                        PorcentajeCCE = 0.00m,
                        MinimoCCE = 0.80m,
                        MaximoCCE = 0.80m
                    }

                };
            }
        }

        public class ResultadoCalculoMontoExample : IExamplesProvider<ResultadoCalculoMonto>
        {
            public ResultadoCalculoMonto GetExamples()
            {
                return new ResultadoCalculoMonto
                {
                    ControlMonto = new ControlMontoDTO
                    {
                        CodigoMoneda = null,
                        CodigoMonedaOrigen = null,
                        Monto = 1.0m,
                        MontoComisionEntidad = 0.0m,
                        MontoComisionCce = 0.80m,
                        Itf = 0,
                        TotalComision = 0.0m,
                        Total = 1.0m
                    }
                };
            }
        }

        public class RealizarTransferenciaDTOExample : IExamplesProvider<OrdenTransferenciaCanalElectronicoDTO>
        {
            public OrdenTransferenciaCanalElectronicoDTO GetExamples()
            {
                return new OrdenTransferenciaCanalElectronicoDTO
                {
                    NumeroCuenta = "001211102009369",
                    IdentificadorQR = "24090308131993471290",
                    NumeroTarjeta = "4772000011500194",
                    EntidadDestino = "CMAC Tacna",
                    NumeroCelularOriginante = "999988887",
                    NumeroCelularReceptor = "",
                    ControlMonto = new ControlMontoDTO
                    {
                        CodigoMoneda = null,
                        CodigoMonedaOrigen = null,
                        Monto = 1.0m,
                        MontoComisionEntidad = 0.0m,
                        MontoComisionCce = 0.80m,
                        Itf = 0,
                        TotalComision = 0.0m,
                        Total = 1.0m
                    },
                    ResultadoConsultaCuenta = new ResultadoConsultaCuentaCCE
                    {
                        CodigoCuentaTransaccion = "81300121110201065255",
                        CodigoEntidadOriginante = "0813",
                        CodigoEntidadReceptora = "0813",
                        FechaCreacionTransaccion = "20240925",
                        HoraCreacionTransaccion = "183644",
                        NumeroReferencia = "092518364487",
                        Trace = "005927",
                        Canal = "90",
                        CodigoMoneda = "604",
                        CodigoTransferencia = "2024092518364408138190005927",
                        CriterioPlaza = "M",
                        TipoPersonaDeudor = "N",
                        NommbreDeudor = "Luis",
                        TipoDocumentoDeudor = "2",
                        NumeroIdentidadDeudor = "75599370",
                        NumeroCelularDeudor = null,
                        CodigoCuentaInterbancariaDeudor = "81300121110200938070",
                        NombreReceptor = "APELLIDO1 APELLIDO2 PRUEBA UNO",
                        DireccionReceptor = null,
                        NumeroTelefonoReceptor = null,
                        NumeroCelularReceptor = null,
                        CodigoCuentaInterbancariaReceptor = "81300121110201065255",
                        CodigoTarjetaReceptor = null,
                        IndicadorITF = "O",
                        Plaza = "M",
                        TipoTransaccion = "320",
                        InstruccionId = "2024092518364408138190005927",
                        TipoDocumentoReceptor = "2",
                        NumeroIdentidadReceptor = "45002920",
                        MismoTitular = "O",
                        Comision = new ComisionDTO
                        {
                            Id = 0,
                            IdTipoTransferencia = 1,
                            CodigoComision = "0101",
                            CodigoMoneda = "1",
                            CodigoAplicacionTarifa = "M",
                            Porcentaje = 0.00m,
                            Minimo = 15.00m,
                            Maximo = 15.00m,
                            IndicadorPorcentaje = "N",
                            IndicadorFijo = "S",
                            PorcentajeCCE = 0.00m,
                            MinimoCCE = 0.80m,
                            MaximoCCE = 0.80m
                        }
                    }
                };
            }
        }

        public class ResultadoTransferenciaCelularExample : IExamplesProvider<ResultadoTransferenciaCanalElectronico>
        {
            public ResultadoTransferenciaCanalElectronico GetExamples()
            {
                return new ResultadoTransferenciaCanalElectronico
                {
                    FechaOperacion = DateTime.Now,
                    NumeroOperacion = 445562
                };
            }
        }

        public class OperacionFrecuenteDTOExample : IExamplesProvider<OperacionFrecuenteDTO>
        {
            public OperacionFrecuenteDTO GetExamples()
            {
                return new OperacionFrecuenteDTO
                {
                    NumeroCuenta = "014211000030116",
                    NombreOperacionFrecuente = "Alias",
                    CodigoCuentaInterbancariaReceptor = "05830000010125300256",
                    NombreDestino = "NOMBRE RECEPTOR",
                    MismoTitularEnDestino = true,
                    TipoDocumento = 2,
                    NumeroDocumento = "73646908",
                    TipoOperacionFrecuente = 10
                };
            }
        }

        public class EnviarCorreoDTOExample : IExamplesProvider<EnviarCorreoDTO>
        {
            public EnviarCorreoDTO GetExamples()
            {
                return new EnviarCorreoDTO
                {
                    NumeroMovimiento = 12345,
                    CorreoDestinatario = "correonuevo@gmail.com"
                };
            }
        }        
        #endregion

        #region Operaciones Salidas
        public class ConsultaCuentaReceptorDTOExample : IExamplesProvider<ConsultaCuentaReceptorDTO>
        {
            public ConsultaCuentaReceptorDTO GetExamples()
            {
                return new ConsultaCuentaReceptorDTO
                {
                    CodigoTipoTransferencia = "320",
                    CodigoEntidadReceptora = "0002",
                    NumeroCuentaOriginante = "014211000030116",
                    NumeroCuentaReceptor = "04900200601810114793",
                    CodigoCanalCCE = "91"
                };
            }
        }
        
        #endregion

        #region Otras consultas CCE
        public class EchoTestRespuestaExample : IExamplesProvider<bool>
        {
            public bool GetExamples()
            {
                return true;
            }
        }

        public class SignOnOffDTOExample : IExamplesProvider<SignOnOffDTO>
        {
            public SignOnOffDTO GetExamples()
            {
                return new SignOnOffDTO
                {
                    participantCode = "prueba",
                    creationDate = "prueba",
                    creationTime = "prueba",
                    trace = "123456",
                    responseDate = "prueba",
                    responseTime = "prueba",
                    status = "00",
                    reasonCode = "AC03",
                };
            }
        }
        #endregion

        #endregion

        #region EndPoint Reportes
        public class GenerarReporteDTOExample : IExamplesProvider<GenerarReporteDTO>
        {
            public GenerarReporteDTO GetExamples()
            {
                return new GenerarReporteDTO
                {
                    Anio = DateTime.Now.Year.ToString(),
                    Mes = DateTime.Now.Month.ToString("D2"),
                    Dia = DateTime.Now.Day.ToString("D2"),
                    IdTipoReporte = (int)GenerarReporteDTO.TipoReporte.MantenimientoMensual,
                    Usuario = General.UsuarioPorDefecto,
                };
            }
        }
        #endregion
    }
}     