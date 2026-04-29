using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.DTOs.CL;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Servicios;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using NUnit.Framework.Constraints;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Pruebas
{
    [TestClass]
    public class ServicioAplicacionClientePruebas
    {
        #region declaraciones
        private Mock<IContextoAplicacion> _contextoAplicacion;
        private Mock<IRepositorioGeneral> _repositorioGeneral;
        private Mock<IRepositorioOperacion> _repositorioOperaciones;
        private Mock<IServiceProvider> _servicioProvider;
        private Mock<IServicioAplicacionColas> _servicioAplicacionColas;
        private ServicioAplicacionCliente _servicioAplicacionCliente;
        private Mock<IBitacora<ServicioAplicacionCliente>> _bitacora;
        private Mock<IServicioAplicacionProducto> _servicioAplicacionProductos;
        private List<CuentaEfectivo> _cuentasEfectivas = new List<CuentaEfectivo>();
        private Cliente _cliente;
        private TipoDocumento _tipoDocumento;
        #endregion

        #region Inicializar

        [TestInitialize]
        public void Inicializar()
        {
            _servicioProvider = new Mock<IServiceProvider>();
            _contextoAplicacion = new Mock<IContextoAplicacion>();
            _repositorioGeneral = new Mock<IRepositorioGeneral>();
            _repositorioOperaciones = new Mock<IRepositorioOperacion>();
            _servicioAplicacionColas = new Mock<IServicioAplicacionColas>();
            _servicioAplicacionProductos = new Mock<IServicioAplicacionProducto>();
            _bitacora = new Mock<IBitacora<ServicioAplicacionCliente>>();

            _servicioAplicacionCliente = new ServicioAplicacionCliente(
                _servicioProvider.Object,
                _repositorioGeneral.Object,
                _contextoAplicacion.Object,
                _repositorioOperaciones.Object,
                _bitacora.Object,
                _servicioAplicacionColas.Object,
                _servicioAplicacionProductos.Object);

            _tipoDocumento = new TipoDocumento();
            typeof(TipoDocumento).GetProperty(nameof(TipoDocumento.CodigoTipoDocumento))?.SetValue(_tipoDocumento, "1");
            typeof(TipoDocumento).GetProperty(nameof(TipoDocumento.CodigoTipoDocumentoInmediataCce))?.SetValue(_tipoDocumento, "2");

            var documento = new DocumentoCliente();
            typeof(DocumentoCliente).GetProperty(nameof(DocumentoCliente.CodigoTipoDocumento))?.SetValue(documento, "1");
            typeof(DocumentoCliente).GetProperty(nameof(DocumentoCliente.TipoDocumento))?.SetValue(documento, _tipoDocumento);

            var documentos = new List<DocumentoCliente>() { documento } ;
            ICollection<DocumentoCliente> coleccionDocumento = documentos;

            var personafisica = new PersonaFisica();
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.PrimerNombre))?.SetValue(personafisica, "nombres1");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.SegundoNombre))?.SetValue(personafisica, "nombres2");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.PrimerApellido))?.SetValue(personafisica, "apellidos1");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.SegundoApellido))?.SetValue(personafisica, "apellidos2");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.CodigoEstadoCivil))?.SetValue(personafisica, "x");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.IndicadorSexo))?.SetValue(personafisica, "N");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.ApellidoCasado))?.SetValue(personafisica, "apellido");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.SegundoApellidoCuantia))?.GetValue(personafisica);

            _cliente = new Cliente();
            typeof(Cliente).GetProperty(nameof(Cliente.CodigoCliente))?.SetValue(_cliente, "1");
            typeof(Cliente).GetProperty(nameof(Cliente.NombreCliente))?.SetValue(_cliente, "USUARIO PRUEBA");
            typeof(Cliente).GetProperty(nameof(Cliente.TipoPersona))?.SetValue(_cliente, "F");
            typeof(Cliente).GetProperty(nameof(Cliente.PersonaFisica))?.SetValue(_cliente, personafisica);
            typeof(Cliente).GetProperty(nameof(Cliente.Documentos))?.SetValue(_cliente, coleccionDocumento);

            var caracteristica = new ProductoCuentasCaracteristicas();
            typeof(ProductoCuentasCaracteristicas).GetProperty(nameof(ProductoCuentasCaracteristicas.IndTransfCCETIN))?.SetValue(caracteristica, "S");

            var producto = new Producto();
            typeof(Producto).GetProperty(nameof(Producto.NombreProducto))?.SetValue(producto, "AC01");
            var moneda = new Moneda();
            typeof(Moneda).GetProperty(nameof(Moneda.CodigoMoneda))?.SetValue(moneda, "1");

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
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Producto))?.SetValue(cuentaEfectiva, producto);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Moneda))?.SetValue(cuentaEfectiva, moneda);
            _cuentasEfectivas.Add(cuentaEfectiva);
        }
        #endregion

        #region Prueba Consulta Cuenta Entrante
        [TestMethod]
        public void ObtenerClienteOriginanteTinInmediata()
        {
            var transaccion = new TransaccionOrdenTransferenciaInmediata();
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.MontoTransferencia))?.SetValue(transaccion, 100m);
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.MontoComision))?.SetValue(transaccion, 0.80m);
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.TipoTransferencia))?.SetValue(transaccion, "320");
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.IndicadorEstadoOperacion))?.SetValue(transaccion, "C");
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.IdentificadorInstruccion))?.SetValue(transaccion, "2024020915100008134115981116");
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.TipoDocumentoIdentidadOriginante))?.SetValue(transaccion, "1");
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.NumeroDocumentoIdentidadOriginante))?.SetValue(transaccion, "73646961");
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.NombreOriginante))?.SetValue(transaccion, "Nombre Originante");
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.TipoPersonaOriginante))?.SetValue(transaccion, "N");
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.CodigoCuentaInterbancariaOriginante))?.SetValue(transaccion, "81300121100001211959");
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.CodigoCuentaInterbancariaReceptor))?.SetValue(transaccion, "81300121110200937057");


            var cliente = new Cliente();
            typeof(Cliente).GetProperty(nameof(Cliente.CodigoCliente))?.SetValue(cliente, "1");
            typeof(Cliente).GetProperty(nameof(Cliente.NombreCliente))?.SetValue(cliente, "USUARIO PRUEBA");
            typeof(Cliente).GetProperty(nameof(Cliente.TipoPersona))?.SetValue(cliente, "F");

            var personaFisica = new PersonaFisica();
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.CodigoCliente))?.SetValue(personaFisica, "CAJERROC");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.PrimerNombre))?.SetValue(personaFisica, "Pepe");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.SegundoNombre))?.SetValue(personaFisica, "Pepe");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.PrimerApellido))?.SetValue(personaFisica, "Ramirez");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.SegundoApellido))?.SetValue(personaFisica, "Ramirez");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.IndicadorSexo))?.SetValue(personaFisica, "M");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.CodigoEstadoCivil))?.SetValue(personaFisica, "01");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.Cliente))?.SetValue(personaFisica, cliente);

            typeof(Cliente).GetProperty(nameof(Cliente.PersonaFisica))?.SetValue(cliente, personaFisica);

            var tipoDocumento = new Dominio.Entidades.CL.TipoDocumento();
            typeof(Dominio.Entidades.CL.TipoDocumento).GetProperty(nameof(Dominio.Entidades.CL.TipoDocumento.CodigoTipoDocumento))?.SetValue(tipoDocumento, "1");
            typeof(Dominio.Entidades.CL.TipoDocumento).GetProperty(nameof(Dominio.Entidades.CL.TipoDocumento.CodigoTipoDocumentoInmediataCce))?.SetValue(tipoDocumento, "2");
            typeof(Dominio.Entidades.CL.TipoDocumento).GetProperty(nameof(Dominio.Entidades.CL.TipoDocumento.IndicadorPersonaNatural))?.SetValue(tipoDocumento, General.Si);

            var documentoClienteOriginante = new List<DocumentoCliente>();
            var documento = new DocumentoCliente();
            typeof(DocumentoCliente).GetProperty(nameof(DocumentoCliente.CodigoTipoDocumento))?.SetValue(documento, "1");
            typeof(DocumentoCliente).GetProperty(nameof(DocumentoCliente.NumeroDocumento))?.SetValue(documento, "73646962");
            typeof(DocumentoCliente).GetProperty(nameof(DocumentoCliente.Cliente))?.SetValue(documento, cliente);
            typeof(DocumentoCliente).GetProperty(nameof(DocumentoCliente.TipoDocumento))?.SetValue(documento, tipoDocumento);
            documentoClienteOriginante.Add(documento);

            var parametro = new ParametroPorEmpresa();
            typeof(ParametroPorEmpresa).GetProperty(nameof(ParametroPorEmpresa.CodigoSistema))?.SetValue(parametro, "CC");
            typeof(ParametroPorEmpresa).GetProperty(nameof(ParametroPorEmpresa.CodigoParametro))?.SetValue(parametro, "NO_CLIENTE_1");
            typeof(ParametroPorEmpresa).GetProperty(nameof(ParametroPorEmpresa.ValorParametro))?.SetValue(parametro, "CAJERROC");

            var clienteExterno = new ClienteExternoDTO()
            {
                CodigoCliente = "CAJERROC",
                CodigoCuentaInterbancaria = "81300121110200937057",
                Nombres = "Luis torres huanca",
                NumeroDocumento = "73646901",
                CodigoTipoDocumento = "2",
                CodigoUsuario = "CAJERROC",
                TipoPersona = "2",
                Telefono = "999621321",
                TipoCliente = "EX"
            };

            typeof(Cliente).GetProperty(nameof(Cliente.Documentos))?.SetValue(cliente, documentoClienteOriginante);

            _repositorioOperaciones.Setup(x => x
                .Listar<DocumentoCliente>())
                .Returns(documentoClienteOriginante.AsQueryable());

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<ParametroPorEmpresa>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(parametro);

            var valorEsperado = clienteExterno;
            var resultado = _servicioAplicacionCliente
                .ObtenerClienteOriginante(transaccion);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(string.Empty, resultado.CodigoCliente);
        }

        [TestMethod]
        public void ObtenerDatosConsultaCuentaEntrante()
        {
            var codigoCuentaInterbancaria = "81300121110200937057";

            var consultaCuenta = new ClienteReceptorDTO();
            consultaCuenta.CodigoCuentaInterbancaria = "81300121110200937057";
            consultaCuenta.NombreCliente = "Nombre Cliente";
            consultaCuenta.NumeroDocumento = "12345678";
            consultaCuenta.TipoDocumento = "1";
            consultaCuenta.TipoDocumentoCCE = "2";
            consultaCuenta.Direccion = "Direccion";
            consultaCuenta.NumeroTelefono = "";
            consultaCuenta.NumeroCelular = "";
            consultaCuenta.EstadoCuenta = "A";
            consultaCuenta.CodigoMoneda = "1";
            consultaCuenta.CodigoMonedaISO = "604";
            consultaCuenta.IndicadorCuentaValida = "S";

            _repositorioGeneral
                .Setup(x => x.ObtenerDatosClientePorCodigoCuentaInterbancaria(codigoCuentaInterbancaria))
                .Returns(consultaCuenta);

            var resultado = _servicioAplicacionCliente
                .ObtenerDatosPorCodigoCuentaInterbancaria(codigoCuentaInterbancaria);

            Assert.AreEqual(resultado.CodigoCuentaInterbancaria, codigoCuentaInterbancaria);
        }
        #endregion

        #region Metodos Salientes
        [TestMethod]
        public void ObtenerDatosCuentaOriginante()
        {
            var indicadorRemunerativo = "S";
            var numeroCuenta = "123456";
            _repositorioGeneral
                .Setup(x => x.ObtenerValorParametroPorEmpresa(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(indicadorRemunerativo);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<CuentaEfectivo>(It.IsAny<string>(), It.IsAny<string>())).Returns(_cuentasEfectivas.First());

            var resultado = _servicioAplicacionCliente.ObtenerDatosCuentaOrigen(numeroCuenta);

            Assert.IsNotNull(resultado);
        }
        #endregion Metodos Salientes

    }
}

