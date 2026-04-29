using Takana.Transferencias.CCE.Api.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takana.Transferencias.CCE.Api.Common.EchoTest;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Cancelaciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;
using Takana.Transferencias.CCE.Api.Common.Excepciones;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Pruebas
{
    [TestClass]
    public class ServicioDominioValidacionPruebas
    {

        #region Declaracion Variables
        private List<EntidadFinancieraInmediata> _entidadesFinancieras = new List<EntidadFinancieraInmediata>();
        private EntidadFinancieraInmediata _entidadFinanciera = new EntidadFinancieraInmediata();
        private EntidadFinancieraInmediata _otraEntidadFinanciera = new EntidadFinancieraInmediata();
        private LimiteTransferenciaInmediata _limiteTransferencia = new LimiteTransferenciaInmediata();
        private ClienteReceptorDTO _datosCliente = new ClienteReceptorDTO();
        #endregion

        #region Inicializaciones
        [TestInitialize]
        public void Inicializar()
        {
            _entidadFinanciera = new EntidadFinancieraInmediata();
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEntidad))?
                .SetValue(_entidadFinanciera, "0002");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.NombreEntidad))?
                .SetValue(_entidadFinanciera, "PRUEBA");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEstadoSign))?
                .SetValue(_entidadFinanciera, "H");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEstadoCCE))?
                .SetValue(_entidadFinanciera, "N");
            _entidadesFinancieras.Add(_entidadFinanciera);
            _otraEntidadFinanciera = new EntidadFinancieraInmediata();
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEntidad))?
                .SetValue(_otraEntidadFinanciera, "0813");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.NombreEntidad))?
                .SetValue(_otraEntidadFinanciera, "PRUEBA");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEstadoSign))?
                .SetValue(_otraEntidadFinanciera, "H");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEstadoCCE))?
                .SetValue(_otraEntidadFinanciera, "N");
            _entidadesFinancieras.Add(_otraEntidadFinanciera);

            _limiteTransferencia = new LimiteTransferenciaInmediata();
            typeof(LimiteTransferenciaInmediata)?
                .GetProperty(nameof(LimiteTransferenciaInmediata.MontoLimiteMinimo))?
                .SetValue(_limiteTransferencia, 0m);
            typeof(LimiteTransferenciaInmediata)?
                .GetProperty(nameof(LimiteTransferenciaInmediata.MontoLimiteMaximo))?
                .SetValue(_limiteTransferencia, 30000m);

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

        #region Obtener Datos
        public EstructuraContenidoAV2 ObtenerDatosConsultaCuenta() {
            return new EstructuraContenidoAV2()
            {
                AV2 = new ConsultaCuentaRecepcionEntradaDTO() {
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

        public EstructuraContenidoCT2 ObtenerDatosOrdenTransferencia() {
            return new EstructuraContenidoCT2()
            {
                CT2 = new OrdenTransferenciaRecepcionEntradaDTO() {
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

        public EstructuraContenidoCT5 ObtenerDatosConfirmacion() {
            return new EstructuraContenidoCT5()
            {
                CT5 = new OrdenTransferenciaConfirmacionEntradaDTO() {
                    debtorParticipantCode = "0002",
                    creditorParticipantCode = "0813",
                    responseDate = DateTime.Now.ToString("yyyyMMdd"),
                    responseTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                    terminalId = "ABC00001",
                    retrievalReferenteNumber = "041510100014",
                    trace = "123456",
                    amount = 10000,
                    currency = "604",
                    transactionReference = "1234567890",
                    responseCode = "00",
                    feeAmount = 80,
                    settlementDate = "20210415",
                    transactionType = "320",
                    debtorCCI = "00281400020887813712",
                    creditorCCI = "81300121110200937057",
                    creditorCreditCard = null,
                    sameCustomerFlag = "M",
                    instructionId = "2021041510100000114115123456",
                    creationDate = DateTime.Now.ToString("yyyyMMdd"),
                    creationTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                    channel = "15",
                    interbankSettlementAmount = 10080
                }
            };
        }

        public EstructuraContenidoCTC1 ObtenerDatosCancelacion() {
            return new EstructuraContenidoCTC1()
            {
                CTC1 = new CancelacionRecepcionDTO() {
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
        public EstructuraContenidoET1 ObtenerDatosEchoTest() {
            return new EstructuraContenidoET1()
            {
                ET1 = new EchoTestDTO() {
                    participantCode = "0813",
                    creationDate = DateTime.Now.ToString("yyyyMMdd"),
                    creationTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                    trace = "123456"
                }
            };
        }
        #endregion

        #region Validaciones de Transferencias Entrantes

        [TestMethod]
        public void ValidacionReglasConsultaCuentaExitosa()
        {
            var datosConsultaCuenta = ObtenerDatosConsultaCuenta();

            var resultado = datosConsultaCuenta.AV2
                .ValidarReglas(_datosCliente, null,
                    _entidadesFinancieras, null, new CanalCCE(), RazonRespuesta.codigo0000);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado, RazonRespuesta.codigo0000);
        }

        [TestMethod]
        public void ValidacionReglasOrdenTransferenciaExitosa() {

            var datosOrdenTransferencia = ObtenerDatosOrdenTransferencia();

            var resultado = datosOrdenTransferencia.CT2
                    .ValidarReglas(_datosCliente, null,
                        _entidadesFinancieras,
                        _limiteTransferencia, new CanalCCE(), RazonRespuesta.codigo0000);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado, RazonRespuesta.codigo0000);
        }

        [TestMethod]
        public void ValidacionReglasOrdenTransferenciaConfirmacionExitosa() {

            var datosConfirmacion = ObtenerDatosConfirmacion();

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
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.IdentificadorInstruccion))?
                .SetValue(transaccion, "2024020915100008134115981116");

            var resultado = datosConfirmacion.CT5.ValidarReglas(transaccion, RazonRespuesta.codigo0000);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado, RazonRespuesta.codigo0000);
        }

        [TestMethod]
        public void ValidacionReglasOrdenTransferenciaCancelacionExitosa() {

            var datosCancelacion = ObtenerDatosCancelacion();

            var resultado = datosCancelacion.CTC1.ValidarReglas(RazonRespuesta.codigo0000);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado, RazonRespuesta.codigo0000); ;
        }

        [TestMethod]
        public void ValidacionReglasEchoTestExitosa() {

            var datosEchoTest = ObtenerDatosEchoTest();

            var resultado = datosEchoTest.ET1.ValidarReglas(RazonRespuesta.codigo0000);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado, RazonRespuesta.codigo0000);
        }

        [TestMethod]
        public void NoExisteEntidadOriginante() {

            var datosConsultaCuenta = ObtenerDatosConsultaCuenta();

            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEstadoSign))?
                .SetValue(_entidadFinanciera, "N");

            _entidadesFinancieras.Add(_entidadFinanciera);

            var resultado = datosConsultaCuenta.AV2
                .ValidarReglas(_datosCliente, null,
                    _entidadesFinancieras, null, new CanalCCE(), RazonRespuesta.codigo0000);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado, RazonRespuesta.codigoDNOR);
        }

        [TestMethod]
        public void NoExisteEntidadReceptoraNoHabilitada() {

            var datosConsultaCuenta = ObtenerDatosConsultaCuenta();

            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEstadoSign))?
                .SetValue(_otraEntidadFinanciera, "N");

            _entidadesFinancieras.Add(_otraEntidadFinanciera);

            var resultado = datosConsultaCuenta.AV2
                .ValidarReglas(_datosCliente, null,
                    _entidadesFinancieras, null, new CanalCCE(), RazonRespuesta.codigo0000);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado, RazonRespuesta.codigoFF07);
        }

        [TestMethod]
        public void ErrorDeFormato() {

            var datosConsultaCuenta = ObtenerDatosConsultaCuenta();
            datosConsultaCuenta.AV2.creditorCCI = "81300121110200937";

            var resultado = datosConsultaCuenta.AV2
                .ValidarReglas(_datosCliente, null,
                    _entidadesFinancieras, null, new CanalCCE(), RazonRespuesta.codigo0000);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado, RazonRespuesta.codigoFF02);
        }

        [TestMethod]
        public void ErrorDeFormatoTipoDocumento() {

            var datosConsultaCuenta = ObtenerDatosConsultaCuenta();
            datosConsultaCuenta.AV2.debtorIdCode = string.Empty;

            var resultado = datosConsultaCuenta.AV2
                .ValidarReglas(_datosCliente, null,
                    _entidadesFinancieras, null, new CanalCCE(), RazonRespuesta.codigo0000);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado, RazonRespuesta.codigoFF02);
        }

        [TestMethod]
        public void CodigoIdentificacionOriginanteInvalido() {

            var datosConsultaCuenta = ObtenerDatosConsultaCuenta();
            datosConsultaCuenta.AV2.debtorId = "1234567";

            var resultado = datosConsultaCuenta.AV2
                .ValidarReglas(_datosCliente, null,
                    _entidadesFinancieras, null, new CanalCCE(), RazonRespuesta.codigo0000);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado, RazonRespuesta.codigoBE16);
        }

        [TestMethod]
        public void NumeroCuentaReceptorIncorrecto() {

            var datosConsultaCuenta = ObtenerDatosConsultaCuenta();

            var resultado = datosConsultaCuenta.AV2
                .ValidarReglas(new ClienteReceptorDTO(), null,
                    _entidadesFinancieras, null, new CanalCCE(), RazonRespuesta.codigo0000);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado, RazonRespuesta.codigoAC01);
        }

        [TestMethod]
        public void NumeroCuentaReceptorBloqueadaCerrada() {

            var datosConsultaCuenta = ObtenerDatosConsultaCuenta();

            _datosCliente.IndicadorCuentaValida = CuentaEfectivo.CuentaNoValida;

            var resultado = datosConsultaCuenta.AV2
                .ValidarReglas(_datosCliente, null,
                    _entidadesFinancieras, null, new CanalCCE(), RazonRespuesta.codigo0000);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado, RazonRespuesta.codigoAC06);
        }

        [TestMethod]
        public void TipoMonedaCuentaNoCoincide() {

            var datosConsultaCuenta = ObtenerDatosConsultaCuenta();

            _datosCliente.CodigoMonedaISO = DatosGenerales.CodigoMonedaDolaresCCE;

            var resultado = datosConsultaCuenta.AV2
                .ValidarReglas(_datosCliente, null,
                    _entidadesFinancieras, null, new CanalCCE(), RazonRespuesta.codigo0000);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado, RazonRespuesta.codigoAC11);
        }

        [TestMethod]
        public void TransferenciasPagoTarjetaCredito() {

            var datosConsultaCuenta = ObtenerDatosConsultaCuenta();

            datosConsultaCuenta.AV2.transactionType = TipoTransferencia.CodigoPagoTarjeta;

            var resultado = datosConsultaCuenta.AV2
                .ValidarReglas(_datosCliente, null,
                    _entidadesFinancieras, null, new CanalCCE(), RazonRespuesta.codigo0000);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado, RazonRespuesta.codigoAC14);
        }

        #endregion

        #region Validacion de Transfernecias Salientes
        [TestMethod]
        public void ValidarIndicadoresFallido()
        {
            var resultadoEsperado = "La cuenta originante no es valida para la transacción";
            var casosExisto = new ConsultaCanalDTO
            {
                IndicadorCuentaValidaTransaccion = false,
                IndicadorSaldoValido = true,
            };

            var excepcion = Assert.ThrowsException<Exception>(() =>
               ServicioDominioValidacion.ValidarIndicadores(casosExisto));

            Assert.AreEqual(resultadoEsperado, excepcion.Message);
        }

        [TestMethod]
        public void ValidarEstructuraCCIDestinoExitoso()
        {
            var casoExisto = new ConsultaCanalDTO
            {
                TipoTransaccion = TipoTransferencia.CodigoTransferenciaOrdinaria,
                AcreedorCCI = "01823500423525284326",
            };
            try
            {
                ServicioDominioValidacion.ValidarEstructuraNumeroDestino(casoExisto);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ValidarEstructuraNumeroTarjeta()
        {
            var tarjetaErroneo = "444444444";
            var resultadoEsperado = "La estructura de la tarjeta es invalida: " + tarjetaErroneo;
            var excepcion = Assert.ThrowsException<Exception>(() =>
               ServicioDominioValidacion.ValidarEstructuraNumeroTarjeta(tarjetaErroneo));

            Assert.AreEqual(resultadoEsperado, excepcion.Message);
        }

        [TestMethod]
        public void ValidarLongitudCCI()
        {
            var cciErroneo = "1111";
            var resultadoEsperado = "El número de CCI debe ser un número y su longitud" +
                    " no debe ser diferente de 20 caracteres.";

            var excepcion = Assert.ThrowsException<DomainException>(() =>
               ServicioDominioValidacion.ValidarEstructuraCodigoCuentaInterbancario(cciErroneo));

            Assert.AreEqual(resultadoEsperado, excepcion.Message);
        }

        [TestMethod]
        public void ValidarMismaInstucionCCI()
        {
            var cciMismaInstitucion = "81323500423525284326";
            var resultadoEsperado = "Estimado cliente, no se permite realizar transferencias interbancarias entre cuentas de la misma Caja Tacna.";

            var excepcion = Assert.ThrowsException<DomainException>(() =>
               ServicioDominioValidacion.ValidarEstructuraCodigoCuentaInterbancario(cciMismaInstitucion));

            Assert.AreEqual(resultadoEsperado, excepcion.Message);
        }

        [TestMethod]
        public void ValidarDigitosControlEntidadOficinaDeCCI()
        {
            var cciErroneo = "01823500423525284020";
            var digitosControl = "00";
            var resultadoEsperado = "Dígito de control de la cuenta interbancaria es incorrecto."
                + cciErroneo;

            var excepcion = Assert.ThrowsException<DomainException>(() =>
               ServicioDominioValidacion.ValidarEstructuraCodigoCuentaInterbancario(cciErroneo));

            Assert.AreEqual(resultadoEsperado, excepcion.Message);
        }

        [TestMethod]
        public void VerificarEntidadesHabilitadas()
        {
            var entidades = new List<EntidadFinancieraPorTransferencia>();
            entidades.Add(new EntidadFinancieraPorTransferencia());
            entidades.Add(new EntidadFinancieraPorTransferencia());
            try
            {
                ServicioDominioValidacion.VerificarEntidadesHabilitadas(entidades);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void VerificarEstadoEntidadSuspendida()
        {
            var esperado = "La entidad originante esta suspendida";
            var estadoSistemaNoDisponible = "N";

            var originante = "0813";
            var receptora = "0018";
            var singOffOriginante = "H";
            var estadoCCENormalizadoOriginante = "N";
            var singOffReceptor = "H";
            var estadoCCENormalizadoReceptor = "N";

            var resultado = VerificarEstadoEntidades(originante, receptora, estadoSistemaNoDisponible,
                singOffOriginante, estadoCCENormalizadoOriginante, singOffReceptor, estadoCCENormalizadoReceptor);

            Assert.AreEqual(esperado, resultado);
        }
        [TestMethod]
        public void VerificarEntidadOrigineSingOff()
        {
            var esperado = "La entidad origen esta en Sign Off";
            var estadoSistemaDisponible = "D";
            var originante = "0813";
            var receptora = "0018";
            var singOffOriginante = "I";
            var estadoCCENormalizadoOriginante = "N";
            var singOffReceptor = "H";
            var estadoCCENormalizadoReceptor = "N";

            var resultado = VerificarEstadoEntidades(originante, receptora, estadoSistemaDisponible,
                singOffOriginante, estadoCCENormalizadoOriginante, singOffReceptor, estadoCCENormalizadoReceptor);

            Assert.AreEqual(esperado, resultado);
        }
        [TestMethod]
        public void VerificarEntidadarReceptoraSingOff()
        {
            var esperado = "La entidad receptora esta en Sign Off";
            var estadoSistemaDisponible = "D";
            var originante = "0813";
            var receptora = "0018";
            var singOffOriginante = "H";
            var estadoCCENormalizadoOriginante = "N";
            var singOffReceptor = "I";
            var estadoCCENormalizadoReceptor = "N";

            var resultado = VerificarEstadoEntidades(originante, receptora, estadoSistemaDisponible,
                singOffOriginante, estadoCCENormalizadoOriginante, singOffReceptor, estadoCCENormalizadoReceptor);

            Assert.AreEqual(esperado, resultado);
        }
        [TestMethod]
        public void VerificarEntidadarOriginanteBloqueado()
        {
            var esperado = "La entidad origen esta en estado Bloqueado";
            var estadoSistemaDisponible = "D";
            var originante = "0813";
            var receptora = "0018";
            var singOffOriginante = "H";
            var estadoCCENormalizadoOriginante = "S";
            var singOffReceptor = "H";
            var estadoCCENormalizadoReceptor = "N";

            var resultado = VerificarEstadoEntidades(originante, receptora, estadoSistemaDisponible,
                singOffOriginante, estadoCCENormalizadoOriginante, singOffReceptor, estadoCCENormalizadoReceptor);

            Assert.AreEqual(esperado, resultado);
        }
        [TestMethod]
        public void VerificarEntidadarReceptoraBloqueada()
        {
            var esperado = "La entidad receptora esta en estado Bloqueado";
            var estadoSistemaDisponible = "D";
            var originante = "0813";
            var receptora = "0018";
            var singOffOriginante = "H";
            var estadoCCENormalizadoOriginante = "N";
            var singOffReceptor = "H";
            var estadoCCENormalizadoReceptor = "S";

            var resultado = VerificarEstadoEntidades(originante, receptora, estadoSistemaDisponible,
                singOffOriginante, estadoCCENormalizadoOriginante, singOffReceptor, estadoCCENormalizadoReceptor);

            Assert.AreEqual(esperado, resultado);
        }
        public string VerificarEstadoEntidades(
            string originante,
            string receptora,
            string estadoSistemaDisponible,
            string singOffOriginante,
            string estadoCCENormalizadoOriginante,
            string singOffReceptor,
            string estadoCCENormalizadoReceptor)
        {
            var entidades = new List<EntidadFinancieraInmediata>();

            var estadoSignOriginante = new EstadoInmediata();
            typeof(EstadoInmediata)?
                .GetProperty(nameof(EstadoInmediata.Codigo))?
                .SetValue(estadoSignOriginante, singOffOriginante);

            var estadoCCEOriginante = new EstadoInmediata();
            typeof(EstadoInmediata)?
                .GetProperty(nameof(EstadoInmediata.Codigo))?
                .SetValue(estadoCCEOriginante, estadoCCENormalizadoOriginante);

            var estadoSignReceptor = new EstadoInmediata();
            typeof(EstadoInmediata)?
                .GetProperty(nameof(EstadoInmediata.Codigo))?
                .SetValue(estadoSignReceptor, singOffReceptor);

            var estadoCCEReceptor = new EstadoInmediata();
            typeof(EstadoInmediata)?
                .GetProperty(nameof(EstadoInmediata.Codigo))?
                .SetValue(estadoCCEReceptor, estadoCCENormalizadoReceptor);

            var entidadOriginante = new EntidadFinancieraInmediata();
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEntidad))?
                .SetValue(entidadOriginante, originante);
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.EstadoSign))?
                .SetValue(entidadOriginante, estadoSignOriginante);
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.EstadoCCE))?
                .SetValue(entidadOriginante, estadoCCEOriginante);

            var entidadReceptora = new EntidadFinancieraInmediata();
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEntidad))?
                .SetValue(entidadReceptora, receptora);
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.EstadoSign))?
                .SetValue(entidadReceptora, estadoSignReceptor);
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.EstadoCCE))?
                .SetValue(entidadReceptora, estadoCCEReceptor);

            entidades.Add(entidadOriginante);
            entidades.Add(entidadReceptora);

            var excepcion = Assert.ThrowsException<Exception>(() =>
               ServicioDominioValidacion.VerificarEstadoEntidades(originante,
                receptora, estadoSistemaDisponible, entidades));

            return excepcion.Message;
        }

        #endregion
    }
}

