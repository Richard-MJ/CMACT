using Moq;
using System.Linq.Expressions;
using Takana.Transferencias.CCE.Api.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.EchoTest;
using Takana.Transferencias.CCE.Api.Common.Rechazos;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Cancelaciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Common.Notificaciones;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Pruebas
{
    [TestClass]
    public class ServicioAplicacionTransferenciaEntradaPruebas
    {
        #region declaraciones
        private Mock<IContextoAplicacion> _contextoAplicacion;
        private Mock<IRepositorioGeneral> _repositorioGeneral;
        private Mock<IRepositorioOperacion> _repositorioOperacion;
        private Mock<IServicioAplicacionColas> _servicioAplicacionColas;
        private Mock<IServicioAplicacionCliente> _servicioAplicaionCliente;
        private Mock<IServicioAplicacionPeticion> _aplicacionPeticionServicio;
        private Mock<IServicioAplicacionBitacora> _servicioAplicacionBitacora;
        private Mock<IServicioAplicacionParametroGeneral> _servicioAplicacionParametro;
        private Mock<IServicioAplicacionTransaccionOperacion> _servicioAplicacionTransaccionOperacion;
        private ServicioAplicacionTransferenciaEntrada _servicioAplicacionTransferenciasInmediatas;
        #endregion

        #region Inicializar
        [TestInitialize]
        public void Inicializar()
        {
            _repositorioGeneral = new Mock<IRepositorioGeneral>();
            _contextoAplicacion = new Mock<IContextoAplicacion>();
            _repositorioOperacion = new Mock<IRepositorioOperacion>();
            _servicioAplicacionColas = new Mock<IServicioAplicacionColas>();
            _servicioAplicaionCliente = new Mock<IServicioAplicacionCliente>();
            _aplicacionPeticionServicio = new Mock<IServicioAplicacionPeticion>();
            _servicioAplicacionBitacora = new Mock<IServicioAplicacionBitacora>();
            _servicioAplicacionParametro = new Mock<IServicioAplicacionParametroGeneral>();
            _servicioAplicacionTransaccionOperacion = new Mock<IServicioAplicacionTransaccionOperacion>();

            _servicioAplicacionTransferenciasInmediatas = new ServicioAplicacionTransferenciaEntrada(
                _repositorioGeneral.Object, 
                _contextoAplicacion.Object,
                _repositorioOperacion.Object,
                _servicioAplicacionColas.Object,
                _servicioAplicaionCliente.Object,
                _aplicacionPeticionServicio.Object,
                _servicioAplicacionBitacora.Object,
                _servicioAplicacionParametro.Object,
                _servicioAplicacionTransaccionOperacion.Object);
        }
        #endregion

        #region Obtener Datos
        public EstructuraContenidoAV2 ObtenerDatosConsultaCuenta()
        {
            return new EstructuraContenidoAV2()
            {
                AV2 = new ConsultaCuentaRecepcionEntradaDTO()
                {
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

        public EstructuraContenidoCT2 ObtenerDatosOrdenTransferencia()
        {
            return new EstructuraContenidoCT2()
            {
                CT2 = new OrdenTransferenciaRecepcionEntradaDTO()
                {
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

        public EstructuraContenidoCT5 ObtenerDatosConfirmacion()
        {
            return new EstructuraContenidoCT5()
            {
                CT5 = new OrdenTransferenciaConfirmacionEntradaDTO()
                {
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

        public EstructuraContenidoCTC1 ObtenerDatosCancelacion()
        {
            return new EstructuraContenidoCTC1()
            {
                CTC1 = new CancelacionRecepcionDTO()
                {
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
        public EstructuraContenidoET1 ObtenerDatosEchoTest()
        {
            return new EstructuraContenidoET1()
            {
                ET1 = new EchoTestDTO()
                {
                    participantCode = "0813",
                    creationDate = DateTime.Now.ToString("yyyyMMdd"),
                    creationTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                    trace = "123456"
                }
            };
        }
        #endregion

        #region Prueba Consulta Cuenta
        [TestMethod]
        public void ConsultaCuenta()
        {
            var datosConsultaCuenta = ObtenerDatosConsultaCuenta();

            var consultaCuenta = new ClienteReceptorDTO()
            {
                CodigoCuentaInterbancaria = "81300121110200937057",
                NombreCliente = "Nombre Cliente",
                NumeroDocumento = "12345678",
                TipoDocumento = "1",
                Direccion = "Direccion",
                NumeroTelefono = "",
                NumeroCelular = "",
                EstadoCuenta = "A",
                CodigoMoneda = "1",
                CodigoMonedaISO = "604",
                IndicadorCuentaValida = "S",
            };

            var entidadesFinancieras = new List<EntidadFinancieraInmediata>();
            var entidadFinanciera = new EntidadFinancieraInmediata();
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEntidad))?
                .SetValue(entidadFinanciera, "0813");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.NombreEntidad))?
                .SetValue(entidadFinanciera, "CMAC TACNA");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEstadoSign))?
                .SetValue(entidadFinanciera, "H");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEstadoCCE))?
                .SetValue(entidadFinanciera, "N");
            entidadesFinancieras.Add(entidadFinanciera);
            var otraEntidadFinanciera = new EntidadFinancieraInmediata();
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEntidad))?
                .SetValue(otraEntidadFinanciera, "0002");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.NombreEntidad))?
                .SetValue(otraEntidadFinanciera, "BCP");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEstadoSign))?
                .SetValue(otraEntidadFinanciera, "H");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEstadoCCE))?
                .SetValue(otraEntidadFinanciera, "N");
            entidadesFinancieras.Add(otraEntidadFinanciera);

            var tipoDocumentos = new List<Dominio.Entidades.CL.TipoDocumento>();
            var tipoDocumento = new Dominio.Entidades.CL.TipoDocumento();
            typeof(Dominio.Entidades.CL.TipoDocumento)?
                .GetProperty(nameof(Dominio.Entidades.CL.TipoDocumento.CodigoTipoDocumento))?
                .SetValue(tipoDocumento, "1");
            typeof(Dominio.Entidades.CL.TipoDocumento)?
                .GetProperty(nameof(Dominio.Entidades.CL.TipoDocumento.DescripcionTipoDocumento))?
                .SetValue(tipoDocumento, "DNI");
            typeof(Dominio.Entidades.CL.TipoDocumento)?
                .GetProperty(nameof(Dominio.Entidades.CL.TipoDocumento.CodigoTipoDocumentoInmediataCce))?
                .SetValue(tipoDocumento, "2");
            tipoDocumentos.Add(tipoDocumento);

            var calendario = new Calendario();
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoEmpresa))?
                .SetValue(calendario, "1");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoAgencia))?
                .SetValue(calendario, "01");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoSistema))?
                .SetValue(calendario, "CC");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.FechaSistema))?
                .SetValue(calendario, DateTime.Now);

            var bitacora = new BitacoraTransferenciaInmediata();

            _repositorioGeneral.Setup(x =>
                x.ObtenerPorExpresionConLimite<BitacoraTransferenciaInmediata>(
                    It.IsAny<Expression<Func<BitacoraTransferenciaInmediata, bool>>>(), null, 0))
                .Returns(new List<BitacoraTransferenciaInmediata>());

            _servicioAplicacionBitacora.Setup(x => x.RegistrarBitacora(
                    It.IsAny<ConsultaCuentaRecepcionEntradaDTO>(),
                    It.IsAny<DateTime>()))
                .Returns(bitacora);

            _servicioAplicaionCliente
                .Setup(x => x.ObtenerDatosPorCodigoCuentaInterbancaria(datosConsultaCuenta.AV2.creditorCCI))
                .Returns(consultaCuenta);

            _repositorioGeneral.Setup(x => x
                .ObtenerPorExpresionConLimite<EntidadFinancieraInmediata>(
                    It.IsAny<Expression<Func<EntidadFinancieraInmediata, bool>>>(), null, 0))
                .Returns(entidadesFinancieras);

            _repositorioGeneral.Setup(x => x
                .ObtenerPorExpresionConLimite<AfiliacionInteroperabilidadDetalle>(
                    It.IsAny<Expression<Func<AfiliacionInteroperabilidadDetalle, bool>>>(), null, 0))
                .Returns(new List<AfiliacionInteroperabilidadDetalle>());

            _repositorioGeneral.Setup(x => x
                .ObtenerPorExpresionConLimite<CanalCCE>(
                    It.IsAny<Expression<Func<CanalCCE, bool>>>(), null, 0))
                .Returns(new List<CanalCCE>() { new CanalCCE() });

            _repositorioGeneral.Setup(x => x
                .ObtenerPorExpresionConLimite<Dominio.Entidades.CL.TipoDocumento>(
                    It.IsAny<Expression<Func<Dominio.Entidades.CL.TipoDocumento, bool>>>(), null, 0))
                .Returns(tipoDocumentos);

            _repositorioGeneral.Setup(x => x
                .ObtenerCalendarioCuentaEfectivo())
                .Returns(calendario);

            var resultado = _servicioAplicacionTransferenciasInmediatas
                .ConsultaCuentaDeCCE(datosConsultaCuenta.AV2, RazonRespuesta.codigo0000);

            Assert.AreEqual(resultado.Result.AV3.responseCode, CodigoRespuesta.Aceptada);
        }
        #endregion

        #region Prueba Orden Transferencia
        [TestMethod]
        public void OrdenTransferencia()
        {
            var datosOrdenTransferencia = ObtenerDatosOrdenTransferencia();

            var consultaCuenta = new ClienteReceptorDTO()
            {
                CodigoCuentaInterbancaria = "81300121110200937057",
                NombreCliente = "Nombre Cliente",
                NumeroDocumento = "12345678",
                TipoDocumento = "1",
                Direccion = "Direccion",
                NumeroTelefono = "",
                NumeroCelular = "",
                EstadoCuenta = "A",
                CodigoMoneda = "1",
                CodigoMonedaISO = "604",
                IndicadorCuentaValida = "S",
            };

            var entidadesFinancieras = new List<EntidadFinancieraInmediata>();
            var entidadFinanciera = new EntidadFinancieraInmediata();
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEntidad))?
                .SetValue(entidadFinanciera, "0813");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.NombreEntidad))?
                .SetValue(entidadFinanciera, "CMAC TACNA");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEstadoSign))?
                .SetValue(entidadFinanciera, "H");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEstadoCCE))?
                .SetValue(entidadFinanciera, "N");
            entidadesFinancieras.Add(entidadFinanciera);
            var otraEntidadFinanciera = new EntidadFinancieraInmediata();
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEntidad))?
                .SetValue(otraEntidadFinanciera, "0002");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.NombreEntidad))?
                .SetValue(otraEntidadFinanciera, "BCP");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEstadoSign))?
                .SetValue(otraEntidadFinanciera, "H");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEstadoCCE))?
                .SetValue(otraEntidadFinanciera, "N");
            entidadesFinancieras.Add(otraEntidadFinanciera);

            var tipoDocumentos = new List<Dominio.Entidades.CL.TipoDocumento>();
            var tipoDocumento = new Dominio.Entidades.CL.TipoDocumento();
            typeof(Dominio.Entidades.CL.TipoDocumento)?
                .GetProperty(nameof(Dominio.Entidades.CL.TipoDocumento.CodigoTipoDocumento))?
                .SetValue(tipoDocumento, "1");
            typeof(Dominio.Entidades.CL.TipoDocumento)?
                .GetProperty(nameof(Dominio.Entidades.CL.TipoDocumento.DescripcionTipoDocumento))?
                .SetValue(tipoDocumento, "DNI");
            typeof(Dominio.Entidades.CL.TipoDocumento)?
                .GetProperty(nameof(Dominio.Entidades.CL.TipoDocumento.CodigoTipoDocumentoInmediataCce))?
                .SetValue(tipoDocumento, "2");
            tipoDocumentos.Add(tipoDocumento);

            var calendario = new Calendario();
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoEmpresa))?
                .SetValue(calendario, "1");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoAgencia))?
                .SetValue(calendario, "01");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoSistema))?
                .SetValue(calendario, "CC");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.FechaSistema))?
                .SetValue(calendario, DateTime.Now);

            var limitesTransferencias = new List<LimiteTransferenciaInmediata>();
            var limiteTransferencia = new LimiteTransferenciaInmediata();
            typeof(LimiteTransferenciaInmediata)?
                .GetProperty(nameof(LimiteTransferenciaInmediata.MontoLimiteMinimo))?
                .SetValue(limiteTransferencia, 0m);
            typeof(LimiteTransferenciaInmediata)?
                .GetProperty(nameof(LimiteTransferenciaInmediata.MontoLimiteMaximo))?
                .SetValue(limiteTransferencia, 30000m);
            limitesTransferencias.Add(limiteTransferencia);

            _repositorioOperacion.Setup(x =>
                x.ObtenerPorExpresionConLimite<TransaccionOrdenTransferenciaInmediata>(
                    It.IsAny<Expression<Func<TransaccionOrdenTransferenciaInmediata, bool>>>(), null, 0))
                .Returns(new List<TransaccionOrdenTransferenciaInmediata>());

            _servicioAplicaionCliente
                .Setup(x => x.ObtenerDatosPorCodigoCuentaInterbancaria(datosOrdenTransferencia.CT2.creditorCCI))
                .Returns(consultaCuenta);

            _servicioAplicacionBitacora.Setup(x => x.RegistrarBitacora(
                It.IsAny<OrdenTransferenciaRecepcionEntradaDTO>(),
                It.IsAny<DateTime>()))
            .Returns(new BitacoraTransferenciaInmediata());

            _repositorioOperacion.Setup(x => x
                .ObtenerPorExpresionConLimite<EntidadFinancieraInmediata>(
                    It.IsAny<Expression<Func<EntidadFinancieraInmediata, bool>>>(), null, 0))
                .Returns(entidadesFinancieras);

            _repositorioOperacion.Setup(x => x
                .ObtenerPorExpresionConLimite<AfiliacionInteroperabilidadDetalle>(
                    It.IsAny<Expression<Func<AfiliacionInteroperabilidadDetalle, bool>>>(), null, 0))
                .Returns(new List<AfiliacionInteroperabilidadDetalle>());

            _repositorioOperacion.Setup(x => x
                .ObtenerPorExpresionConLimite<Dominio.Entidades.CL.TipoDocumento>(
                    It.IsAny<Expression<Func<Dominio.Entidades.CL.TipoDocumento, bool>>>(), null, 0))
                .Returns(tipoDocumentos);

            _repositorioOperacion.Setup(x => x
                .ObtenerPorExpresionConLimite<CanalCCE>(
                    It.IsAny<Expression<Func<CanalCCE, bool>>>(), null, 0))
                .Returns(new List<CanalCCE>() { new CanalCCE() });

            _repositorioOperacion.Setup(x => x
                .ObtenerPorExpresionConLimite<LimiteTransferenciaInmediata>(
                    It.IsAny<Expression<Func<LimiteTransferenciaInmediata, bool>>>(), null, 0))
                .Returns(limitesTransferencias);

            _repositorioOperacion.Setup(x => x
                .ObtenerCalendarioCuentaEfectivo())
                .Returns(calendario);

            _aplicacionPeticionServicio.Setup(x => x.EnviarParaProcesarRechazoTransferenciaEntrante(It.IsAny<string>()));

            var resultado = _servicioAplicacionTransferenciasInmediatas
                .AutorizaOrdenTransferenciaDeCCE(datosOrdenTransferencia.CT2, RazonRespuesta.codigo0000);

            Assert.AreEqual(resultado.Result.CT3.responseCode, CodigoRespuesta.Aceptada);
        }
        #endregion

        #region Prueba Confirmacion Orden Transferencia
        [TestMethod]
        public async Task ConfirmacionOrdenTransferencia()
        {
            var datosConfirmacion = ObtenerDatosConfirmacion();

            var calendario = new Calendario();
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoEmpresa))?
                .SetValue(calendario, "1");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoAgencia))?
                .SetValue(calendario, "01");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoSistema))?
                .SetValue(calendario, "CC");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.FechaSistema))?
                .SetValue(calendario, DateTime.Now);

            var transacciones = new List<TransaccionOrdenTransferenciaInmediata>();
            var transaccion = new TransaccionOrdenTransferenciaInmediata();
            typeof(TransaccionOrdenTransferenciaInmediata)?
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.CodigoCuentaInterbancariaReceptor))?
                .SetValue(transaccion, "81300121110200937057");
            typeof(TransaccionOrdenTransferenciaInmediata)?
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.MontoTransferencia))?
                .SetValue(transaccion, 100m);
            typeof(TransaccionOrdenTransferenciaInmediata)?
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.MontoComision))?
                .SetValue(transaccion, 0.80m);
            transacciones.Add(transaccion);

            _repositorioOperacion.Setup(x =>
                x.ObtenerPorExpresionConLimite<TransaccionOrdenTransferenciaInmediata>(
                    It.IsAny<Expression<Func<TransaccionOrdenTransferenciaInmediata, bool>>>(), null, 0))
                .Returns(transacciones);

            _repositorioOperacion.Setup(x => x
                .ObtenerCalendarioCuentaEfectivo())
                .Returns(calendario);

            _aplicacionPeticionServicio.Setup(x => x.EnviarParaProcesarTransferenciaEntrante(It.IsAny<string>()));

            try
            {
                await _servicioAplicacionTransferenciasInmediatas.ConfirmaOrdenTransferenciaDeCCE(datosConfirmacion.CT5, string.Empty);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        #endregion

        #region Prueba Cancelacion Orden Transferencia
        [TestMethod]
        public void CancelacionOrdenTransferencia()
        {
            var datosCancelacion = ObtenerDatosCancelacion();

            var calendario = new Calendario();
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoEmpresa))?
                .SetValue(calendario, "1");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoAgencia))?
                .SetValue(calendario, "01");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoSistema))?
                .SetValue(calendario, "CC");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.FechaSistema))?
                .SetValue(calendario, DateTime.Now);

            var transacciones = new List<TransaccionOrdenTransferenciaInmediata>();
            var transaccion = new TransaccionOrdenTransferenciaInmediata();
            typeof(TransaccionOrdenTransferenciaInmediata)?
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.CodigoCuentaInterbancariaReceptor))?
                .SetValue(transaccion, "81300121110200937057");
            typeof(TransaccionOrdenTransferenciaInmediata)?
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.MontoTransferencia))?
                .SetValue(transaccion, 100m);
            typeof(TransaccionOrdenTransferenciaInmediata)?
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.MontoComision))?
                .SetValue(transaccion, 0.80m);
            transacciones.Add(transaccion);


            _repositorioGeneral.Setup(x =>
                x.ObtenerPorExpresionConLimite<TransaccionOrdenTransferenciaInmediata>(
                    It.IsAny<Expression<Func<TransaccionOrdenTransferenciaInmediata, bool>>>(), null, 0))
                .Returns(transacciones);

            _servicioAplicacionBitacora.Setup(x => x.RegistrarBitacora(
                It.IsAny<CancelacionRecepcionDTO>(),
                It.IsAny<DateTime>()))
            .Returns(new BitacoraTransferenciaInmediata());  

            _repositorioGeneral.Setup(x => x
                .ObtenerCalendarioCuentaEfectivo())
                .Returns(calendario);

            var resultado = _servicioAplicacionTransferenciasInmediatas
                .CancelacionOrdenTransferenciaDeCCE(datosCancelacion.CTC1, RazonRespuesta.codigo0000);

            Assert.AreEqual(resultado.Result.CTC2.responseCode, CodigoRespuesta.Aceptada);
        }
        #endregion

        #region Prueba Rechazo
        [TestMethod]
        public async Task Rechazo()
        {
            var datos = new RechazoRecepcionDTO()
            {
                responseDate = DateTime.Now.ToString("yyyyMMdd"),
                responseTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
                creationDate = DateTime.Now.ToString("yyyyMMdd"),
                creationTime = DateTime.Now.TimeOfDay.ToString("hhmmss"),
            };
            var calendario = new Calendario();
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoEmpresa))?
                .SetValue(calendario, "1");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoAgencia))?
                .SetValue(calendario, "01");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoSistema))?
                .SetValue(calendario, "CC");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.FechaSistema))?
                .SetValue(calendario, DateTime.Now);

            _repositorioGeneral.Setup(x => x
                .ObtenerCalendarioCuentaEfectivo())
                .Returns(calendario);

            try
            {
                await _servicioAplicacionTransferenciasInmediatas.RechazoDeCCE(datos);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        #endregion

        #region Prueba Echo Test
        [TestMethod]
        public void EchoTest()
        {
            var datosEchoTest = ObtenerDatosEchoTest();

            var calendario = new Calendario();
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoEmpresa))?
                .SetValue(calendario, "1");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoAgencia))?
                .SetValue(calendario, "01");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoSistema))?
                .SetValue(calendario, "CC");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.FechaSistema))?
                .SetValue(calendario, DateTime.Now);

            _repositorioGeneral.Setup(x => x
                .ObtenerCalendarioCuentaEfectivo())
                .Returns(calendario);

            var resultado = _servicioAplicacionTransferenciasInmediatas
                .EchoTestDeCCE(datosEchoTest.ET1, RazonRespuesta.codigo0000);

            Assert.AreEqual(resultado.Result.ET2.status, CodigoRespuesta.Aceptada);
        }
        #endregion

        #region Prueba Notificaciones
        [TestMethod]
        public async Task Notificaciones()
        {
            var parametrosGenerales = new List<ParametroGeneralTransferencia>();
            var parametroGeneral = new ParametroGeneralTransferencia();
            typeof(ParametroGeneralTransferencia)?
                .GetProperty(nameof(ParametroGeneralTransferencia.CodigoParametro))?
                .SetValue(parametroGeneral, "IPS");
            parametrosGenerales.Add(parametroGeneral);

            var entidadesFinancieras = new List<EntidadFinancieraInmediata>();
            var entidadFinanciera = new EntidadFinancieraInmediata();
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEntidad))?
                .SetValue(entidadFinanciera, "0813");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.NombreEntidad))?
                .SetValue(entidadFinanciera, "CMAC TACNA");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEstadoSign))?
                .SetValue(entidadFinanciera, "H");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEstadoCCE))?
                .SetValue(entidadFinanciera, "N");
            entidadesFinancieras.Add(entidadFinanciera);

            var tipoTrama = new TipoTrama();
            typeof(TipoTrama)?
                .GetProperty(nameof(TipoTrama.Descripcion))?
                .SetValue(tipoTrama, "TRAMA DE EJEMPLO");

            var calendario = new Calendario();
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoEmpresa))?
                .SetValue(calendario, "1");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoAgencia))?
                .SetValue(calendario, "01");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoSistema))?
                .SetValue(calendario, "CC");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.FechaSistema))?
                .SetValue(calendario, DateTime.Now);

            _repositorioGeneral.Setup(x =>
                x.ObtenerPorExpresionConLimite<ParametroGeneralTransferencia>(
                    It.IsAny<Expression<Func<ParametroGeneralTransferencia, bool>>>(), null, 0))
                .Returns(parametrosGenerales);

            _repositorioGeneral.Setup(x =>
                x.ObtenerPorCodigo<TipoTrama>(It.IsAny<int>()))
                .Returns(tipoTrama);

            _repositorioGeneral.Setup(x =>
                x.ObtenerPorExpresionConLimite<EntidadFinancieraInmediata>(
                    It.IsAny<Expression<Func<EntidadFinancieraInmediata, bool>>>(), null, 0))
                .Returns(entidadesFinancieras);

            _repositorioGeneral.Setup(x => x
                .ObtenerCalendarioCuentaEfectivo())
                .Returns(calendario);

            try
            {
                await _servicioAplicacionTransferenciasInmediatas.Notificacion971DeCCE(
                    new Notificacion971DTO() { eventDate = "20240528", eventTime = "100000" });

                await _servicioAplicacionTransferenciasInmediatas.Notificacion972DeCCE(
                    new Notificacion972DTO() { eventDate = "20240528", eventTime = "100000" });

                await _servicioAplicacionTransferenciasInmediatas.Notificacion981DeCCE(
                    new Notificacion981DTO() { eventDate = "20240528", eventTime = "100000" });

                await _servicioAplicacionTransferenciasInmediatas.Notificacion982DeCCE(
                    new Notificacion982DTO() { eventDate = "20240528", eventTime = "100000" });

                await _servicioAplicacionTransferenciasInmediatas.Notificacion993DeCCE(
                    new Notificacion993DTO() { eventDate = "20240528", eventTime = "100000" });

                await _servicioAplicacionTransferenciasInmediatas.Notificacion998DeCCE(
                    new Notificacion998DTO() { eventDate = "20240528", eventTime = "100000" });

                await _servicioAplicacionTransferenciasInmediatas.Notificacion999DeCCE(
                    new Notificacion999DTO()
                    {
                        eventDate = "20240528",
                        eventTime = "100000",
                        previousSettlementWindowDate = "20240528",
                        reconciliationCheckpointCutOffDateAndTime = "20240528100000",
                        newSettlementWindowDate = "20240528"
                    });
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        #endregion
    }
}