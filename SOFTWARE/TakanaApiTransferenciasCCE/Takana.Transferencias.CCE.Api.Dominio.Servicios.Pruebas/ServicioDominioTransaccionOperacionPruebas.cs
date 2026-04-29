using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Dominio;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Pruebas
{
    [TestClass]
    public class ServicioDominioTransaccionOperacionPruebas
    {
        private Mock<IRepositorioGeneral> _repositorioGeneral;
        private Mock<IServicioDominioCuenta> _servicioDominioCuenta;
        private ServicioDominioTransaccionOperacion _servicioDominioTransaccionOperacion;

        private List<CuentaEfectivo> _cuentasEfectivas = new List<CuentaEfectivo>();
        private Cliente _cliente;
        private TipoDocumento _tipoDocumento;

        private Mock<OficinaCCE> _oficinaCCEOrigen;
        private List<OficinaCCE> _oficinasCCEOrigen;
        private Mock<EntidadFinancieraDiferida> _entidadFinancieraCCEOrigen;
        private List<EntidadFinancieraDiferida> _entidadesFinancierasCCEOrigenes;
        private Mock<PlazaCCE> _plazaCCEOrigen;

        private Usuario _usuario;

        private string _numeroCCI = "01823500423525284326";
        private string _numeroTarjeta = "004212000527473";
        private string _transferenciaOrdinaria = "320";

        [TestInitialize]
        public void Inicializar()
        {
            _repositorioGeneral = new Mock<IRepositorioGeneral>();
            _servicioDominioCuenta = new Mock<IServicioDominioCuenta>();

            _servicioDominioTransaccionOperacion = new ServicioDominioTransaccionOperacion(
                _servicioDominioCuenta.Object);

            _tipoDocumento = new TipoDocumento();
            typeof(TipoDocumento).GetProperty(nameof(TipoDocumento.CodigoTipoDocumento))?.SetValue(_tipoDocumento, "1");
            typeof(TipoDocumento).GetProperty(nameof(TipoDocumento.CodigoTipoDocumentoInmediataCce))?.SetValue(_tipoDocumento, "2");

            var documento = new DocumentoCliente();
            typeof(DocumentoCliente).GetProperty(nameof(DocumentoCliente.CodigoTipoDocumento))?.SetValue(documento, "1");
            typeof(DocumentoCliente).GetProperty(nameof(DocumentoCliente.TipoDocumento))?.SetValue(documento, _tipoDocumento);
            typeof(DocumentoCliente).GetProperty(nameof(DocumentoCliente.NumeroDocumento))?.SetValue(documento, "12345678");

            var documentos = new List<DocumentoCliente>();
            documentos.Add(documento);
            ICollection<DocumentoCliente> coleccionDocumento = documentos;

            _cliente = new Cliente();
            typeof(Cliente).GetProperty(nameof(Cliente.CodigoCliente))?.SetValue(_cliente, "1");
            typeof(Cliente).GetProperty(nameof(Cliente.NombreCliente))?.SetValue(_cliente, "USUARIO PRUEBA");
            typeof(Cliente).GetProperty(nameof(Cliente.TipoPersona))?.SetValue(_cliente, "F");
            typeof(Cliente).GetProperty(nameof(Cliente.Documentos))?.SetValue(_cliente, coleccionDocumento);

            var caracteristica = new ProductoCuentasCaracteristicas();
            typeof(ProductoCuentasCaracteristicas).GetProperty(nameof(ProductoCuentasCaracteristicas.IndTransfCCETIN))?.SetValue(caracteristica, "S");

            _cuentasEfectivas = new List<CuentaEfectivo>();
            var cuentaEfectiva = new CuentaEfectivo();
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoAgencia))?.SetValue(cuentaEfectiva, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoEmpresa))?.SetValue(cuentaEfectiva, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoProducto))?.SetValue(cuentaEfectiva, "CC063");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.NumeroCuenta))?.SetValue(cuentaEfectiva, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoCliente))?.SetValue(cuentaEfectiva, "843212");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.SaldoDisponible))?.SetValue(cuentaEfectiva, 1000m);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoCuentaInterbancario))?.SetValue(cuentaEfectiva, "018765432165494");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.IndicadorTipoComercial))?.SetValue(cuentaEfectiva, "0");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoEstado))?.SetValue(cuentaEfectiva, "A");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Caracteristicas))?.SetValue(cuentaEfectiva, caracteristica);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Cliente))?.SetValue(cuentaEfectiva, _cliente);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoCliente))?.SetValue(cuentaEfectiva, "123456");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoMoneda))?.SetValue(cuentaEfectiva, "2");
            _cuentasEfectivas.Add(cuentaEfectiva);

            _usuario = new Usuario();
            typeof(Cliente).GetProperty(nameof(Cliente.CodigoAgencia))?.SetValue(_cliente, "01");
            typeof(Cliente).GetProperty(nameof(Cliente.CodigoUsuario))?.SetValue(_cliente, "USUARIO");



        }
        #region Obtener Datos
        public ControlMontoDTO ObtenerControlMonto()
        {
            return new ControlMontoDTO()
            {
                CodigoMoneda = "1",
                CodigoMonedaOrigen = "1",
                Monto = 200,
                MontoComisionEntidad = 200,
                MontoComisionCce = 200,
                Itf = 200,
                TotalComision = 200,
                Total = 200
            };
        }
        public ComisionDTO ObtenerComision()
        {
            return new ComisionDTO()
            {
                Id = 1,
                IdTipoTransferencia = 2,
                CodigoComision = "0101",
                CodigoMoneda = "1",
                CodigoAplicacionTarifa = "M",
                Porcentaje = 0.5M,
                Minimo = 2,
                Maximo = 5,
                IndicadorPorcentaje = "S",
                IndicadorFijo = "N",
                PorcentajeCCE = 0.5M,
                MinimoCCE = 2,
                MaximoCCE = 3
            };
        }

        public RealizarTransferenciaInmediataDTO ObtenerDatosRealizarTransferencia(
            string tipoTransferencia)
        {
            return new RealizarTransferenciaInmediataDTO()
            {
                ControlMonto = ObtenerControlMonto(),
                CodigoTipoTransferenciaCce = tipoTransferencia,
                CodigoCuentaTransaccionReceptor = tipoTransferencia == _transferenciaOrdinaria
                    ? _numeroCCI : _numeroTarjeta,
                NumeroDocumentoReceptor = "123456789",
                MismoTitularEnDestino = true,
                Beneficiario = "Nombre Beneficiario",
                CodigoTarifarioComision = "M",
                CodigoEntidadReceptora = "0002",
                Canal = General.CanalCCE,
                CodigoAgencia = "01",
                CodigoUsuario = "CAJEROCCE",
                NumeroCuentaOriginante = "81300121110200937057",
                CodigoTipoDocumentoReceptor = "1",
                NumeroLavado = 1234,
                NombreImpresora = "IMPRESORA",
                GlosarioTransaccion = "M"
            };
        }
        #endregion Obtener Datos
        [TestMethod]
        public void ObtenerCuentaOriginanteExitoso()
        {
            try
            {
                ServicioDominioTransaccionOperacion.ValidarCuentaOriginantePermitidaSaliente(
                _cuentasEfectivas.FirstOrDefault()!);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ValidarMontoSaldoInsuficiente()
        {
            decimal saldoActual = 1000;
            decimal montoOperacion = 2000;
            decimal limiteMaximo = 30000;
            decimal limiteMinimo = 10;
            var esperado = "No cuenta con saldo suficiente para realizar la operacion.";

            var excepcion = Assert.ThrowsException<ValidacionException>(() =>
               ServicioDominioTransaccionOperacion.ValidarMontoTransferenciaInmediata(
                saldoActual, montoOperacion, limiteMaximo, limiteMinimo));

            Assert.AreEqual(esperado, excepcion.Message);
        }

        [TestMethod]
        public void ValidarMontoFueraRango()
        {
            decimal saldoActual = 100000;
            decimal montoOperacion = 60000;
            decimal limiteMaximo = 30000;
            decimal limiteMinimo = 10;
            var esperado = "Monto de la transferencia fuera del rango permitido.";

            var excepcion = Assert.ThrowsException<ValidacionException>(() =>
               ServicioDominioTransaccionOperacion.ValidarMontoTransferenciaInmediata(
                saldoActual, montoOperacion, limiteMaximo, limiteMinimo));

            Assert.AreEqual(esperado, excepcion.Message);
        }
        [TestMethod]
        public void CalcularComisionPorcentajeConIndicadorCCE()
        {
            decimal montoOriginal = 1000;
            decimal minimo = 10;
            decimal maximo = 50;
            decimal porcentaje = 0.5M;
            string indicadorCCE = "S";
            var esperado = 10;

            var comision = ServicioDominioTransaccionOperacion.CalcularComisionConPorcentaje(
                montoOriginal, minimo, maximo, porcentaje, indicadorCCE);

            Assert.AreEqual(esperado, comision);
        }

        [TestMethod]
        public void CalcularComisionPorcentajeSINIndicadorCCE()
        {
            decimal montoOriginal = 1000;
            decimal minimo = 10;
            decimal maximo = 50;
            decimal porcentaje = 0.5M;
            string indicadorCCE = "N";
            var esperado = 15;

            var comision = ServicioDominioTransaccionOperacion.CalcularComisionConPorcentaje(
                montoOriginal, minimo, maximo, porcentaje, indicadorCCE);

            Assert.AreEqual(esperado, comision);
        }

        [TestMethod]
        public void CalcularComisionSinPorcentaje()
        {
            decimal montoOriginal = 100;
            decimal minimo = 10;
            decimal maximo = 50;
            var esperado = 50;

            var comision = ServicioDominioTransaccionOperacion.CalcularComisionSinPorcentaje(
                montoOriginal, minimo, maximo);

            Assert.AreEqual(esperado, comision);
        }


        [TestMethod]
        public void CalcularTotalesSaldoInsuficiente()
        {
            decimal montoOperacion = 1000;
            decimal montoMinimoCuenta = 10;
            decimal saldoCuenta = 1000;

            var esperado = "Saldo Insuficiente";

            var excepcion = Assert.ThrowsException<ValidacionException>(() =>
                ServicioDominioTransaccionOperacion.CalcularSaldoSuficiente(
                    montoOperacion, montoMinimoCuenta, saldoCuenta));

            Assert.AreEqual(esperado, excepcion.Message);
        }

        [TestMethod]
        public void CalcularMontosComisionExitoso()
        {
            var montoComisionEntidadEsperado = 2.5m;
            var montoComisionCceEsperado = 2;

            var comision = new CalculoComisionDTO
            {
                MontoOperacion = 100,
                ControlMonto = ObtenerControlMonto(),
                Comision = ObtenerComision(),
                SaldoActual = 2000,
                MontoMinimoCuenta = 10,
                MismoTitular = "M"
            };

            var (montoComisionEntidad, montoComisionCce) = ServicioDominioTransaccionOperacion.CalcularMontosComision(comision);

            Assert.AreEqual(montoComisionEntidadEsperado, montoComisionEntidad);
            Assert.AreEqual(montoComisionCceEsperado, montoComisionCce);
        }
        [TestMethod]
        public void ValidarSaldoDisponible()
        {
            var saldoDisponible = 1000m;
            var valorSaldoMinimo = new ParametroGeneralTransferencia();
            typeof(ParametroGeneralTransferencia).GetProperty(nameof(ParametroGeneralTransferencia.ValorParametro))?
                .SetValue(valorSaldoMinimo, "10");

            var comision = new CalculoComisionDTO
            {
                MontoOperacion = 100,
                ControlMonto = ObtenerControlMonto(),
                Comision = ObtenerComision(),
                SaldoActual = 2000,
                MontoMinimoCuenta = 10,
                MismoTitular = "M"
            };

            var resultado = ServicioDominioTransaccionOperacion.ValidarSaldoDisponible(
                saldoDisponible, 10);

            Assert.AreEqual(true, resultado);
        }

        [TestMethod]
        public void ValidarInsuficiente()
        {
            var saldoDisponible = 9m;
            var valorSaldoMinimo = new ParametroGeneralTransferencia();
            typeof(ParametroGeneralTransferencia).GetProperty(nameof(ParametroGeneralTransferencia.ValorParametro))?
                .SetValue(valorSaldoMinimo, "10");

            var comision = new CalculoComisionDTO
            {
                MontoOperacion = 100,
                ControlMonto = ObtenerControlMonto(),
                Comision = ObtenerComision(),
                SaldoActual = 10,
                MontoMinimoCuenta = 10,
                MismoTitular = "M"
            };

            var resultado = ServicioDominioTransaccionOperacion.ValidarSaldoDisponible(
                saldoDisponible, 10);

            Assert.AreEqual(false, resultado);
        }

        [TestMethod]
        public void GenerarTransferenciaInmediataExitoso()
        {
            var detalleTransferencia = ObtenerDatosRealizarTransferencia("320");
            var movimientosDiarios = new List<MovimientoDiario>();
            var movimientoDiario = new MovimientoDiario();
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.NumeroCuenta))?.SetValue(movimientoDiario, "123456");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.Cuenta))?.SetValue(movimientoDiario, _cuentasEfectivas.FirstOrDefault());
            movimientosDiarios.Add(movimientoDiario);

            var entidadFinancieraCCEReceptor = new EntidadFinancieraDiferida();
            typeof(EntidadFinancieraDiferida).GetProperty(nameof(EntidadFinancieraDiferida.IDEntidadFinanciera))?.SetValue(entidadFinancieraCCEReceptor, 1);

            _repositorioGeneral.Setup(x => x
                .ObtenerNumeroSerieNoBloqueante(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<int>()));
            int numeroMovimiento = 123;
            var resultado = _servicioDominioTransaccionOperacion.GenerarTransferenciaInmediata(
                movimientosDiarios.First(), detalleTransferencia, DateTime.Now, _usuario, entidadFinancieraCCEReceptor,
                numeroMovimiento);

            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void AplicarComisionExitoso()
        {
            decimal totalComision = 10;
            var movimientosDiarios = new List<MovimientoDiario>();
            var movimientoDiario = new MovimientoDiario();

            var configuracion = new ConfiguracionComision();
            typeof(ConfiguracionComision).GetProperty(nameof(ConfiguracionComision.CodigoComision))?.SetValue(configuracion, "M");

            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.NumeroCuenta))?.SetValue(movimientoDiario, "123456");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.Cuenta))?.SetValue(movimientoDiario, _cuentasEfectivas.FirstOrDefault());
            movimientosDiarios.Add(movimientoDiario);

            var parametro = new ParametroPorEmpresa();
            typeof(ParametroPorEmpresa).GetProperty(nameof(ParametroPorEmpresa.ValorParametro))?.SetValue(parametro, "cuentaContable");

            _servicioDominioCuenta.Setup(x => x
                .GenerarMovimientoDiarioDeComision(
                    It.IsAny<MovimientoDiario>(),
                    It.IsAny<ComisionAhorrosAuxiliar>(),
                    It.IsAny<Usuario>(),
                    It.IsAny<int>(),
                    It.IsAny<bool>()));

            _servicioDominioCuenta.Setup(x => x
                .GenerarMovimientoContableComision(
                    It.IsAny<MovimientoDiario>(),
                    It.IsAny<string>()));

            _repositorioGeneral.Setup(x => x
                .ObtenerPorCodigo<ParametroPorEmpresa>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(parametro);

            var transaccion = new List<ITransaccion>();

            try
            {
                int numeroMovimiento = 654;
                _servicioDominioTransaccionOperacion.AplicarComisionTransferencia(
                movimientosDiarios.First(), _usuario, new ComisionAhorrosAuxiliar(), numeroMovimiento, false);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}
