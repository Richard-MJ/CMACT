using Takana.Transferencias.CCE.Api.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takana.Transferencias.CCE.Api.Common.EchoTest;
using Takana.Transferencias.CCE.Api.Common.Cancelaciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Pruebas
{
    [TestClass]
    public class ServicioDominioMaquetacionPruebas
    {
        #region Declaracion Variables
        private ClienteReceptorDTO _datosCliente = new ClienteReceptorDTO();
        #endregion

        #region Inicializaciones
        [TestInitialize]
        public void Inicializar()
        {
            _datosCliente = new ClienteReceptorDTO()
            {
                CodigoCuentaInterbancaria = "81300121110200937057",
                NombreCliente = "Risther",
                NumeroDocumento = "12345678",
                TipoDocumento = "2",
                EstadoCuenta = "A",
                IndicadorCuentaValida = "S",
                CodigoMonedaISO = "604",
                CodigoMoneda = "1"
            };
        }  

        #endregion

        #region Obtener Datos Entradas
        public EstructuraContenidoAV2 ObtenerDatosConsultaCuenta(){
            return new EstructuraContenidoAV2()
            {
                AV2 = new ConsultaCuentaRecepcionEntradaDTO(){
                    debtorParticipantCode = "0002",
                    creditorParticipantCode = "0813",
                    creationDate = DateTime.Now.ToString("yyyyMMdd"),
                    creationTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                    terminalId = "ABC00001",
                    retrievalReferenteNumber = "042010100014",
                    trace = "123456",
                    debtorName = "Pedro Gamero Butorac",
                    debtorId = "42102129",
                    debtorIdCode = "2",
                    debtorPhoneNumber = "2255784",
                    debtorAddressLine = "Av. Eduardo Ordoñez 379 - 101",
                    debtorMobileNumber = "999913014",
                    transactionType = "320",
                    channel = "15",
                    creditorAddressLine = "Av. Los Pinos 444",
                    creditorPhoneNumber = "3461229",
                    creditorMobileNumber = "999666333",
                    creditorCCI = "81300121110200937057",
                    creditorCreditCard = null,
                    debtorTypeOfPerson = "N",
                    currency = "604",
                    proxyValue = null,
                    proxyType = null,
                    instructionId = "2024713535729322318362834412272770",
                    branchId = "1001"
                }
            };
        }

        public EstructuraContenidoCT2 ObtenerDatosOrdenTransferencia(){
            return new EstructuraContenidoCT2()
            {
                CT2 = new OrdenTransferenciaRecepcionEntradaDTO(){
                    debtorParticipantCode = "0002",
                    creditorParticipantCode = "0813",
                    creationDate = DateTime.Now.ToString("yyyyMMdd"),
                    creationTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                    terminalId = "ABC00001",
                    retrievalReferenteNumber = "041510100014",
                    trace = "123456",
                    channel = "15",
                    amount = 10000,
                    currency = "604",
                    transactionReference = "1234567890",
                    referenceTransactionId = "2024713535729322318362834412272770",
                    transactionType = "320",
                    feeAmount = 80,
                    feeCode = "0101",
                    applicationCriteria = "M",
                    debtorTypeOfPerson = "N",
                    debtorName = "Pedro Gamero Butorac",
                    debtorAddressLine = "Av. Eduardo Ordoñez 379 - 101",
                    debtorIdCode = "2",
                    debtorId = "42102129",
                    debtorPhoneNumber = "2255784",
                    debtorMobileNumber = "999913014",
                    debtorCCI = "00281400020887813712",
                    creditorName = "Risther",
                    creditorAddressLine = "Av. Los Pinos 444",
                    creditorPhoneNumber = "3461229",
                    creditorMobileNumber = "999666333",
                    creditorCCI = "81300121110200937057",
                    creditorCreditCard = null,
                    sameCustomerFlag = "M",
                    purposeCode = "ADMG",
                    unstructuredInformation = "Pago de Mantenimiento Abril",
                    grossSalaryAmount = 10000,
                    salaryPaymentIndicator = "5",
                    monthOfThePayment = "11",
                    yearOfThePayment = "2021",
                    branchId = "1001",
                    settlementDate = DateTime.Now.ToString("yyyyMMdd"),
                    instructionId = "2021041510100000114115123490",
                    interbankSettlementAmount = 10080
                }
            };
        }

        public EstructuraContenidoCTC1 ObtenerDatosCancelacion(){
            return new EstructuraContenidoCTC1()
            {
                CTC1 = new CancelacionRecepcionDTO(){
                    creditorParticipantCode = "0813",
                    creationDate = DateTime.Now.ToString("yyyyMMdd"),
                    creationTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                    currency = "604",
                    transactionReference = "1234567890",
                    referenceInstructionId = "2021041510100000114115123456",
                    instructionId = "M202004271011030999H2345621",
                    branchId = "1001"
                }
            };
        }
        public EstructuraContenidoET1 ObtenerDatosEchoTest(){
            return new EstructuraContenidoET1()
            {
                ET1 = new EchoTestDTO(){
                    participantCode = "0813",
                    creationDate = DateTime.Now.ToString("yyyyMMdd"),
                    creationTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                    trace = "123456"
                }
            };
        }
        #endregion

        #region Maquetaciones de Entradas
        [TestMethod]
        public void MaquetarDatosConsultaCuenta()
        {
            var datosConsultaCuenta = ObtenerDatosConsultaCuenta();
            var codigoValidacion = "0000";
            var tipoDocumento = new Entidades.CL.TipoDocumento();

            var resultado = datosConsultaCuenta.AV2
                .MaquetarDatos(codigoValidacion, _datosCliente);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado.AV3.responseCode, CodigoRespuesta.Aceptada);

            codigoValidacion = "AC01";
            resultado = datosConsultaCuenta.AV2
                .MaquetarDatos(codigoValidacion, _datosCliente);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado.AV3.responseCode, CodigoRespuesta.Rechazada);
        }

        [TestMethod]
        public void MaquetarDatosOrdenTransferencia()
        {
            var datosOrdenTransferencia = ObtenerDatosOrdenTransferencia();
            var codigoValidacion = "0000";
            var tipoDocumento = new Entidades.CL.TipoDocumento();
            var calendario = new Calendario();

            var resultado = datosOrdenTransferencia.CT2
                .MaquetarDatos(codigoValidacion, _datosCliente,
                    calendario);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado.CT3.responseCode, CodigoRespuesta.Aceptada);

            codigoValidacion = "AC01";
            resultado = datosOrdenTransferencia.CT2
                .MaquetarDatos(codigoValidacion, _datosCliente,
                    calendario);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado.CT3.responseCode, CodigoRespuesta.Rechazada);
        }

        [TestMethod]
        public void MaquetarDatosOrdenTransferenciaCancelacion()
        {
            var datosCancelacion = ObtenerDatosCancelacion();
            var codigoValidacion = "0000";
            var calendario = new Calendario();

            var resultado = datosCancelacion.CTC1
                .MaquetarDatos(codigoValidacion, calendario);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado.CTC2.responseCode, CodigoRespuesta.Aceptada);

            codigoValidacion = "AC01";
            resultado = datosCancelacion.CTC1
                .MaquetarDatos(codigoValidacion, calendario);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado.CTC2.responseCode, CodigoRespuesta.Rechazada);
        }

        [TestMethod]
        public void MaquetarDatosSolicitudEstadoPago()
        {
            var transaccion = new TransaccionOrdenTransferenciaInmediata();
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.CodigoCuentaInterbancariaReceptor))?
                .SetValue(transaccion, "81300121110200937057");
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.MontoTransferencia))?
                .SetValue(transaccion, 100m);
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.MontoComision))?
                .SetValue(transaccion, 0.80m);
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.TipoTransferencia))?
                .SetValue(transaccion, "320");
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.IndicadorEstadoOperacion))?
                .SetValue(transaccion, "C");
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.FechaOperacion))?
                .SetValue(transaccion, DateTime.Now);
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.IdentificadorInstruccion))?
                .SetValue(transaccion, "2024020915100008134115981116");

            var resultado = transaccion.MaquetarDatos(DateTime.Now);

            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void MaquetarDatosEchoTest()
        {
            var datosEchoTest = ObtenerDatosEchoTest();
            var codigoValidacion = "0000";
            var calendario = new Calendario();

            var resultado = datosEchoTest.ET1
                .MaquetarDatos(codigoValidacion, calendario);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado.ET2.status, CodigoRespuesta.Aceptada);

            codigoValidacion = "AC01";
            resultado = datosEchoTest.ET1
                .MaquetarDatos(codigoValidacion, calendario);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado.ET2.status, CodigoRespuesta.Rechazada);
        }

        #endregion
    }
}

