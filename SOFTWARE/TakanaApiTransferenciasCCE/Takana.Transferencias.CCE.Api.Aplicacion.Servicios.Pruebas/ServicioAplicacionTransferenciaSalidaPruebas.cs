using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion;
using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.EchoTest;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;
using Takana.Transferencias.CCE.Api.Common.SignOnOff;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Pruebas
{
    [TestClass]
    public class ServicioAplicacionTransferenciaSalidaPruebas
    {
        #region Propiedades
        
        private Mock<IContextoAplicacion> _contexto;
        private Mock<IRepositorioGeneral> _repositorioGeneral;
        private Mock<IRepositorioOperacion> _repositorioOperaciones;
        private Mock<IServicioAplicacionBitacora> _servicioAplicacionBitacora;
        private Mock<IServicioDominioTransaccionOperacion> _transaccionServicio;
        private Mock<IServicioAplicacionPeticion> _servicioAplicacionPeticion;
        private Mock<IServicioAplicacionParametroGeneral> _parametroGeneralServicio;
        private Mock<IServicioAplicacionValidacion> _servicioAplicacionValidacion;
        private ServicioAplicacionTransferenciaSalida _servicioAplicacionTransferenciasInmediatas;

        private Calendario _calendario;
        private CodigoRespuesta _codigoRespuestaError;
        private string _numeroSeguimiento="123456";
        private string _tiempoEspera = "100";
        private string _cantidadReintentos = "10";
        #endregion

        #region Inicializacion
        [TestInitialize]
        public void Inicializar()
        {
            _contexto = new Mock<IContextoAplicacion>();
            _repositorioGeneral = new Mock<IRepositorioGeneral>();
            _repositorioOperaciones = new Mock<IRepositorioOperacion>();
            _transaccionServicio = new Mock<IServicioDominioTransaccionOperacion>();
            _servicioAplicacionBitacora = new Mock<IServicioAplicacionBitacora>();
            _servicioAplicacionPeticion = new Mock<IServicioAplicacionPeticion>();
            _parametroGeneralServicio = new Mock<IServicioAplicacionParametroGeneral>();
            _servicioAplicacionValidacion = new Mock<IServicioAplicacionValidacion>();

            _servicioAplicacionTransferenciasInmediatas = new ServicioAplicacionTransferenciaSalida(
                _contexto.Object,
                _repositorioOperaciones.Object,
                _servicioAplicacionPeticion.Object,
                _servicioAplicacionBitacora.Object,
                _transaccionServicio.Object,
                _servicioAplicacionValidacion.Object,
                _parametroGeneralServicio.Object
            );

            _calendario = new Calendario();
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoEmpresa))?
                .SetValue(_calendario, "1");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoAgencia))?
                .SetValue(_calendario, "01");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoSistema))?
                .SetValue(_calendario, "CC");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.FechaSistema))?
                .SetValue(_calendario, DateTime.Now);

            _codigoRespuestaError = new CodigoRespuesta();
            _codigoRespuestaError.AgregarRespuesta(
                    "AC03", "Error desconocido", "No se identifica el codigo de error");
        }
        #endregion

        #region Obtencion de Datos
        public EsctruturaET2 ResultadoEchoTestExitoso()
        {
            return new EsctruturaET2
            {
                ET2 = new EchoTestRespuestaDTO
                {
                    status = CodigoRespuesta.Aceptada,
                    reasonCode = ""
                }
                
            };
        }
        public EsctruturaET2 ResultadoEchoTestSinConexion()
        {
            return new EsctruturaET2
            {
                ET2 = new EchoTestRespuestaDTO
                {
                    status = CodigoRespuesta.Rechazada,
                    reasonCode = "AB05"
                }

            };
        }
        public EsctruturaAV4 RespuestaConsultaCuenta(string tipoRespuesta)
        {
            return new EsctruturaAV4()
            {
                AV4 = new ConsultaCuentaRecepcionSalidaDTO()
                {
                    debtorParticipantCode = "0011",
                    creditorParticipantCode = "0002",
                    creationDate = "20210420",
                    creationTime = "101000",
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
                    instructionId = "2021042010100000118115000006",
                    responseCode = tipoRespuesta,
                    reasonCode = "AC11",
                    creditorName = "Luis Padilla Rojas",
                    creditorAddressLine = "Av. Los Pinos 444",
                    creditorId = "42102130",
                    creditorIdCode = "2",
                    creditorPhoneNumber = "3461229",
                    creditorMobileNumber = "999666333",
                    creditorCCI = "00219313693128502612",
                    creditorCreditCard = "3777444477778880",
                    sameCustomerFlag = "M",
                    currency = "604",
                    proxyValue = "999666333",
                    proxyType = "MSISDN"
                }
            };
        }
        public ConsultaCanalDTO ObtenerDatosConsultaCanal()
        {
            return new ConsultaCanalDTO()
            {
                IdTerminal = "t-0tic",
                IdDeudor = "d-1234567890",
                NombreDeudor = "juan pérez",
                TipoDocumentoDeudor = 1,
                NumeroTelefonoDeudor = "+1234567890",
                DireccionDeudor = "calle principal 123",
                NumeroCelularDeudor = "+1234567890",
                TipoTransaccion = "320",
                TipoPersonaDeudor = "persona natural",
                Moneda = "1",
                AcreedorCCI = "00219317042343608210",
                CodigoCuentaInterbancarioOriginante = "81300121110200937057",
                Canal = General.CanalCCE,
                TarjetaCreditoAcreedor = "1234567890123456",
                EntidadOriginante = "0813",
                EntidadReceptora = "0002",
                NumeroCuenta = "12345678",
                IndicadorCuentaValidaTransaccion = true,
                IndicadorCuentaMancomunada = false,
                IndicadorSaldoValido = true,

            };
        }

        public EsctruturaCT4 RespuestaDatosOrdenTransferencia(string tipoRespuesta)
        {
            return new EsctruturaCT4()
            {
                CT4 = new OrdenTransferenciaRecepcionSalidaDTO()
                {
                    debtorParticipantCode = "0011",
                    creditorParticipantCode = "0002",
                    responseDate = "20210415",
                    responseTime = "101005",
                    terminalId = "ABC00001",
                    retrievalReferenteNumber = "041510100014",
                    trace = "123456",
                    amount = 50000,
                    currency = "604",
                    transactionReference = "1234567890",
                    responseCode = tipoRespuesta,
                    reasonCode = "AC11",
                    feeAmount = 700,
                    settlementDate = "20210415",
                    transactionType = "320",
                    debtorCCI = "01181400020887813712",
                    creditorCCI = "00219313693128502612",
                    creditorCreditCard = "3777444477778880",
                    sameCustomerFlag = "M",
                    instructionId = "2021041510100000114115123456",
                    creationDate = "20210415",
                    creationTime = "101000",
                    channel = "15",
                    interbankSettlementAmount = 50700
                }
            };
        }

        public OrdenTransferenciaCanalDTO ObtenerDatosOrdenCanal()
        {
            return new OrdenTransferenciaCanalDTO
            {
                CodigoEntidadOriginante = "prueba",
                CodigoEntidadReceptora = "prueba",
                FechaCreacionTransaccion = "prueba",
                HoraCreacionTransaccion = "prueba",
                CodigoRequerimientoReintento = "prueba",
                IdentificadorTerminal = "prueba",
                NumeroReferencia = "prueba",
                Trace = "123456",
                Canal = "T",
                MontoImporte = 1000,
                CodigoMoneda = "1",
                CodigoTransferencia = "320",
                MontoComision = 10,
                MontoITF = 0,
                CodigoTarifa = "M",
                CriterioPlaza = "M",
                TipoPersonaDeudor = "J",
                NommbreDeudor = "prueba",
                DireccionDeudor = "prueba",
                TipoDocumentoDeudor = "prueba",
                NumeroIdentidadDeudor = "prueba",
                NumeroTelefonoDeudor = "prueba",
                NumeroCelularDeudor = "prueba",
                CodigoCuentaInterbancariaDeudor = "prueba",
                TipoDocumentoReceptor = "prueba",
                NumeroIdentidadReceptor = "prueba",
                NombreReceptor = "prueba",
                DireccionReceptor = "prueba",
                NumeroTelefonoReceptor = "prueba",
                NumeroCelularReceptor = "prueba",
                CodigoCuentaInterbancariaReceptor = "prueba",
                CodigoTarjetaReceptor = "prueba",
                IndicadroITF = "prueba",
                ConceptoCobroTarifa = "prueba",
                GlosaTransaccion = "prueba",
                ImporteSueldos = 1000,
                IndicadorHaberes = "prueba",
                MesPago = "prueba",
                Añopago = "prueba",
                TipoTransaccion = "prueba",
                InstruccionId = "prueba",
                MismoTitular = "prueba",
                NumeroTransferencia = 123456,
                NumeroMovimiento = 123456,
                NumeroLavado = 123456,
                NumeroAsiento = 123456,
                TipoDocumentoDeudorCCE = "prueba",
                TipoDocumentoReceptorCCE = "prueba"
            };
        }

        public EstructuraSignOn2 ResultadoSignOn(string tipoRespuesta)
        {
            return new EstructuraSignOn2
            {
                SignOn2 = new SignOnOffDTO
                {
                    participantCode = "prueba",
                    creationDate = "prueba",
                    creationTime = "prueba",
                    trace = "123456",
                    responseDate = "prueba",
                    responseTime = "prueba",
                    status = tipoRespuesta,
                    reasonCode = "AC03",
                }
            };
        }
        public EstructuraSignOff2 ResultadoSignOff(string tipoRespuesta)
        {
            return new EstructuraSignOff2
            {
                SignOff2 = new SignOnOffDTO
                {
                    participantCode = "prueba",
                    creationDate = "prueba",
                    creationTime = "prueba",
                    trace = "123456",
                    responseDate = "prueba",
                    responseTime = "prueba",
                    status = tipoRespuesta,
                    reasonCode = "AC03",
                }
            };
        }
        #endregion Obtencion de Datos

        #region Metodos
        [TestMethod]
        public void EchoTestExitoso()
        {
            var resultadoExitoso = ResultadoEchoTestExitoso();

           _parametroGeneralServicio
                .Setup(x => x.ObtenerNumeroSeguimiento(It.IsAny<string>()))
                .Returns(_numeroSeguimiento);
            _repositorioOperaciones.Setup(x => x
                .ObtenerCalendarioCuentaEfectivo())
                .Returns(_calendario);
            _servicioAplicacionPeticion
                .Setup(x => x.EnviarEchoTest(It.IsAny<EsctruturaET1>(), It.IsAny<string>()))
                .ReturnsAsync(resultadoExitoso);

            var resultado = _servicioAplicacionTransferenciasInmediatas.EchoTest();

            Assert.AreEqual(resultado.Result.Datos, true);
        }
        [TestMethod]
        public void EchoTestSinConexion()
        {
            var resultadoSinConexion = ResultadoEchoTestSinConexion();

            _parametroGeneralServicio
                 .Setup(x => x.ObtenerNumeroSeguimiento(It.IsAny<string>()))
                 .Returns(_numeroSeguimiento);
            _repositorioGeneral.Setup(x => x
                .ObtenerCalendarioCuentaEfectivo())
                .Returns(_calendario);
            _servicioAplicacionPeticion
                .Setup(x => x.EnviarEchoTest(It.IsAny<EsctruturaET1>(), It.IsAny<string>()))
                .ReturnsAsync(resultadoSinConexion);

            var resultado = _servicioAplicacionTransferenciasInmediatas.EchoTest();

            Assert.AreEqual(resultado.Result.Datos, false);
        }
        [TestMethod]
        public void ObtenerDatosCuentaCCEExisto()
        {
            var tipoDocumentoCCE = "2";
            var datosCanal = ObtenerDatosConsultaCanal();
            var respuestaConsultaExitosa = RespuestaConsultaCuenta(CodigoRespuesta.Aceptada);

            _servicioAplicacionBitacora.Setup(x => x.RegistrarBitacora(
                It.IsAny<ConsultaCuentaSalidaDTO>(),
                It.IsAny<DateTime>(),
                It.IsAny<bool>()))
            .Returns(new BitacoraTransferenciaInmediata());

            ObtenerDatosCuenta(tipoDocumentoCCE, respuestaConsultaExitosa);

            var resultado = _servicioAplicacionTransferenciasInmediatas
                .ObtenerDatosCuentaCCE(datosCanal, new Calendario(), true).Result;

            Assert.AreEqual(resultado.Codigo, CodigoRespuesta.Aceptada);
        }
        [TestMethod]
        public void ObtenerDatosCuentaCCEFallido()
        {
            var tipoDocumentoCCE = "2";
            var datosCanal = ObtenerDatosConsultaCanal();
            var respuestaConsultaFallido = RespuestaConsultaCuenta(CodigoRespuesta.Rechazada);

            ObtenerDatosCuenta(tipoDocumentoCCE, respuestaConsultaFallido);

            _parametroGeneralServicio
                .Setup(x => x.ObtenerErrorLocal(It.IsAny<string>()))
                .Returns(_codigoRespuestaError);

            var resultado = _servicioAplicacionTransferenciasInmediatas
                .ObtenerDatosCuentaCCE(datosCanal, new Calendario(), true).Result;

            Assert.AreEqual(resultado.Codigo, CodigoRespuesta.Rechazada);
        }
        public void ObtenerDatosCuenta(string tipoDocumentoCCE, EsctruturaAV4 respuestaConsultaExitosa)
        {
            EchoTestExitoso();

            _servicioAplicacionValidacion
                .Setup(x => x.ValidarReglasIPS(It.IsAny<ConsultaCanalDTO>()));

            _repositorioOperaciones.Setup(x => x
               .ObtenerCalendarioCuentaEfectivo())
               .Returns(_calendario);

            _parametroGeneralServicio
                .Setup(x => x.ObtenerNumeroSeguimiento(It.IsAny<string>()))
                .Returns(_numeroSeguimiento);

            _parametroGeneralServicio
                .Setup(x => x.obtenerParametrosConfiguracion(ParametroGeneralTransferencia.TiempoReintento))
                .Returns(_tiempoEspera);

            _parametroGeneralServicio
                .Setup(x => x.obtenerParametrosConfiguracion(ParametroGeneralTransferencia.MaximoReintento))
                .Returns(_cantidadReintentos);

            _servicioAplicacionPeticion
                .Setup(x => x.ObtenerDatosCuentaCCE(
                    It.IsAny<EsctruturaAV1>(),It.IsAny<int>()))
                .ReturnsAsync(respuestaConsultaExitosa);
        }

        [TestMethod]
        public void OrdenRespuestaExisto()
        {
            var datosCanal = ObtenerDatosOrdenCanal();

            var resultadoExitoso = RespuestaDatosOrdenTransferencia(CodigoRespuesta.Aceptada);

            _servicioAplicacionBitacora.Setup(x => x.RegistrarBitacora(
                It.IsAny<OrdenTransferenciaSalidaDTO>(),
                It.IsAny<OrdenTransferenciaCanalDTO>(),
                It.IsAny<DateTime>()))
            .Returns(new BitacoraTransferenciaInmediata());

            _servicioAplicacionBitacora.Setup(x => x.RegistrarTransaccion(
                It.IsAny<OrdenTransferenciaSalidaDTO>(),
                It.IsAny<OrdenTransferenciaCanalDTO>(),
                It.IsAny<AsientoContable>(),
                It.IsAny<Transferencia>(),
                It.IsAny<DateTime>()))
            .Returns(new TransaccionOrdenTransferenciaInmediata());

            EnviarOrdenTransferenciaExitoso(datosCanal, resultadoExitoso);

            var resultado = _servicioAplicacionTransferenciasInmediatas
               .EnviarOrdenTransferencia(datosCanal, _calendario, new AsientoContable(), new Transferencia()).Result;

            Assert.AreEqual(resultado.Codigo, CodigoRespuesta.Aceptada);
        }

        [TestMethod]
        public void OrdenRespuestaFallido()
        {
            var datosCanal = ObtenerDatosOrdenCanal();
            var resultadoExitoso = RespuestaDatosOrdenTransferencia(CodigoRespuesta.Rechazada);

            _servicioAplicacionBitacora.Setup(x => x.RegistrarBitacora(
                It.IsAny<OrdenTransferenciaSalidaDTO>(),
                It.IsAny<OrdenTransferenciaCanalDTO>(),
                It.IsAny<DateTime>()))
            .Returns(new BitacoraTransferenciaInmediata());

            _servicioAplicacionBitacora.Setup(x => x.RegistrarTransaccion(
                It.IsAny<OrdenTransferenciaSalidaDTO>(),
                It.IsAny<OrdenTransferenciaCanalDTO>(),
                It.IsAny<AsientoContable>(),
                It.IsAny<Transferencia>(),
                It.IsAny<DateTime>()))
            .Returns(new TransaccionOrdenTransferenciaInmediata());

            EnviarOrdenTransferenciaExitoso(datosCanal, resultadoExitoso);

            _parametroGeneralServicio
                .Setup(x => x.ObtenerErrorLocal(It.IsAny<string>()))
                .Returns(_codigoRespuestaError);

            var resultado = _servicioAplicacionTransferenciasInmediatas
               .EnviarOrdenTransferencia(datosCanal, _calendario, new AsientoContable(), new Transferencia()).Result;

            Assert.AreEqual(resultado.Codigo, CodigoRespuesta.Rechazada);
        }
        public void EnviarOrdenTransferenciaExitoso(
            OrdenTransferenciaCanalDTO datosCanal,
            EsctruturaCT4 resultadoExitoso)
        {
            _servicioAplicacionValidacion
                .Setup(x => x.VerificarEstadoEntidades(
                    It.IsAny<string>(),
                    It.IsAny<string>()));

            _parametroGeneralServicio
                .Setup(x => x.ObtenerNumeroSeguimiento(It.IsAny<string>()))
                .Returns(_numeroSeguimiento);

            _repositorioOperaciones.Setup(x => x
               .ObtenerCalendarioCuentaEfectivo())
               .Returns(_calendario);

            _parametroGeneralServicio
                .Setup(x => x.ConvertirTipoDocumentos(It.IsAny<OrdenTransferenciaCanalDTO>()))
                .Returns(datosCanal);

            _parametroGeneralServicio
                .Setup(x => x.obtenerParametrosConfiguracion(ParametroGeneralTransferencia.TiempoReintento))
                .Returns(_tiempoEspera);

            _parametroGeneralServicio
                .Setup(x => x.obtenerParametrosConfiguracion(ParametroGeneralTransferencia.MaximoReintento))
                .Returns(_cantidadReintentos);

            _servicioAplicacionPeticion
                .Setup(x => x.EnviarOrdenTransferencia(
                    It.IsAny<EsctruturaCT1>(), It.IsAny<int>()))
                .ReturnsAsync(resultadoExitoso);
        }


        #endregion Metodos

        [TestMethod]
        public void SignOnExistoso()
        {
            var resultadoExistoso = ResultadoSignOn(CodigoRespuesta.Aceptada);
            SignOn(resultadoExistoso);

            var resultado = _servicioAplicacionTransferenciasInmediatas
               .SignOn().Result;

            Assert.AreEqual(resultado.Codigo, CodigoRespuesta.Aceptada);
        }

        [TestMethod]
        public void SignOnFallido()
        {
            var resultadoFallido = ResultadoSignOn(CodigoRespuesta.Rechazada);

            SignOn(resultadoFallido);

            _parametroGeneralServicio
               .Setup(x => x.ObtenerErrorLocal(It.IsAny<string>()))
               .Returns(_codigoRespuestaError);

            var resultado = _servicioAplicacionTransferenciasInmediatas
               .SignOn().Result;

            Assert.AreEqual(resultado.Codigo, CodigoRespuesta.Rechazada);
        }

        [TestMethod]
        public void SignOffExistoso()
        {
            var resultadoExistoso = ResultadoSignOff(CodigoRespuesta.Aceptada);
            SignOff(resultadoExistoso);

            var resultado = _servicioAplicacionTransferenciasInmediatas
               .SignOff().Result;

            Assert.AreEqual(resultado.Codigo, CodigoRespuesta.Aceptada);
        }
        [TestMethod]
        public void SignOffFallido()
        {
            var resultadoFallido = ResultadoSignOff(CodigoRespuesta.Rechazada);

            SignOff(resultadoFallido);

            _parametroGeneralServicio
                .Setup(x => x.ObtenerErrorLocal(It.IsAny<string>()))
                .Returns(_codigoRespuestaError);

            var resultado = _servicioAplicacionTransferenciasInmediatas
               .SignOff().Result;
            Assert.AreEqual(resultado.Codigo, CodigoRespuesta.Rechazada);
        }

        public void SignOn(EstructuraSignOn2 resultadoExistoso)
        {
            _parametroGeneralServicio
                .Setup(x => x.ObtenerNumeroSeguimiento(It.IsAny<string>()))
                .Returns(_numeroSeguimiento);

            _repositorioGeneral.Setup(x => x
               .ObtenerCalendarioCuentaEfectivo())
               .Returns(_calendario);

            _repositorioOperaciones.Setup(x => x
                .ObtenerCalendarioCuentaEfectivo())
                .Returns(_calendario);

            _parametroGeneralServicio
                .Setup(x => x.obtenerParametrosConfiguracion(ParametroGeneralTransferencia.TiempoReintento))
                .Returns(_tiempoEspera);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<EntidadFinancieraInmediata>(It.IsAny<string>()))
                .Returns(new EntidadFinancieraInmediata());

            _servicioAplicacionPeticion
               .Setup(x => x.EnviarSignOn(
                   It.IsAny<EstructuraSignOn1>()))
               .ReturnsAsync(resultadoExistoso);
        }
        public void SignOff(EstructuraSignOff2 resultadoExistoso)
        {
            _parametroGeneralServicio
                .Setup(x => x.ObtenerNumeroSeguimiento(It.IsAny<string>()))
                .Returns(_numeroSeguimiento);

            _repositorioGeneral.Setup(x => x
               .ObtenerCalendarioCuentaEfectivo())
               .Returns(_calendario);

            _repositorioOperaciones.Setup(x => x
                .ObtenerCalendarioCuentaEfectivo())
                .Returns(_calendario);

            _parametroGeneralServicio
                .Setup(x => x.obtenerParametrosConfiguracion(ParametroGeneralTransferencia.TiempoReintento))
                .Returns(_tiempoEspera);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<EntidadFinancieraInmediata>(It.IsAny<string>()))
                .Returns(new EntidadFinancieraInmediata());

            _servicioAplicacionPeticion
               .Setup(x => x.EnviarSignOff(
                   It.IsAny<EstructuraSignOff1>()))
               .ReturnsAsync(resultadoExistoso);
        }
    }
}
