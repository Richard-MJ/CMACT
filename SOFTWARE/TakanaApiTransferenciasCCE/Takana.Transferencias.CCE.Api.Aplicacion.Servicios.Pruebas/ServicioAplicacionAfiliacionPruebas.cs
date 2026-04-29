using Moq;
using System.Linq.Expressions;
using Takana.Transferencias.CCE.Api.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.TJ;
using Takana.Transferencias.CCE.Api.Common.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Dominio;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Servicios;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Pruebas
{
    [TestClass]
    public class ServicioAplicacionAfiliacionPruebas
    {
        #region Declaraciones
        private Mock<IContextoAplicacion> _contexto;
        private Mock<IRepositorioOperacion> _repositorioOperaciones;
        private Mock<IRepositorioGeneral> _repositorioGeneral;
        private Mock<IServicioAplicacionCliente> _servicioAplicacionCliente;
        private Mock<IServicioDominioAfiliacion> _servicioDominioAfiliacion;
        private Mock<IServicioAplicacionColas> _servicioAplicacionColas;
        private Mock<IServicioAplicacionProducto> _servicioAplicacionProductos;
        private Mock<IServicioAplicacionInteroperabilidad> _servicioAplicacionInteroperabilidad;
        private Mock<IServicioAplicacionNotificaciones> _servicioApliacionNotificaciones;

        private Calendario _calendario;
        private ServicioAplicacionAfiliacion _servicioAplicacionAfiliacion;
        private CuentaEfectivo _cuentaEfectivo;
        private Moneda _moneda;
        private Cliente _cliente;
        private Usuario _usuario;
        private Producto _producto;
        #endregion Declaraciones

        #region Inicializar

        [TestInitialize]
        public void Inicializar()
        {
            _contexto = new Mock<IContextoAplicacion>();
            _repositorioOperaciones = new Mock<IRepositorioOperacion>();
            _repositorioGeneral = new Mock<IRepositorioGeneral>();
            _servicioAplicacionCliente = new Mock<IServicioAplicacionCliente>();
            _servicioAplicacionProductos = new Mock<IServicioAplicacionProducto>();
            _servicioDominioAfiliacion = new Mock<IServicioDominioAfiliacion>();
            _servicioAplicacionInteroperabilidad = new Mock<IServicioAplicacionInteroperabilidad>();
            _servicioAplicacionColas = new Mock<IServicioAplicacionColas>();
            _servicioApliacionNotificaciones = new Mock<IServicioAplicacionNotificaciones>();

            _servicioAplicacionAfiliacion = new ServicioAplicacionAfiliacion(
                _contexto.Object,
                _repositorioGeneral.Object,
                _repositorioOperaciones.Object,
                _servicioDominioAfiliacion.Object,
                _servicioAplicacionProductos.Object,
                _servicioAplicacionInteroperabilidad.Object,
                _servicioAplicacionColas.Object,
                _servicioApliacionNotificaciones.Object
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


            _moneda = new Moneda();
            typeof(Moneda).GetProperty(nameof(Moneda.CodigoMoneda))?.SetValue(_moneda, "1");
            typeof(Moneda).GetProperty(nameof(Moneda.NombreMoneda))?.SetValue(_moneda, "SOL");

            _cliente = new Cliente();
            typeof(Cliente).GetProperty(nameof(Cliente.CodigoCliente))?.SetValue(_cliente, "1");
            typeof(Cliente).GetProperty(nameof(Cliente.NombreCliente))?.SetValue(_cliente, "USUARIO PRUEBA");
            typeof(Cliente).GetProperty(nameof(Cliente.TipoPersona))?.SetValue(_cliente, "F");
            typeof(Cliente).GetProperty(nameof(Cliente.DireccionCorreoElectronico))?.SetValue(_cliente, "yuqensi@fail");

            _producto = new Producto();
            typeof(Producto).GetProperty(nameof(Producto.NombreProducto))?.SetValue(_producto, "C001");

            _cuentaEfectivo = new CuentaEfectivo();
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoAgencia))?.SetValue(_cuentaEfectivo, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoEmpresa))?.SetValue(_cuentaEfectivo, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoProducto))?.SetValue(_cuentaEfectivo, "CC063");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.NumeroCuenta))?.SetValue(_cuentaEfectivo, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoCliente))?.SetValue(_cuentaEfectivo, "843212");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.SaldoDisponible))?.SetValue(_cuentaEfectivo, 100m);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Moneda))?.SetValue(_cuentaEfectivo, _moneda);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Cliente))?.SetValue(_cuentaEfectivo, _cliente);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Producto))?.SetValue(_cuentaEfectivo, _producto);

            _usuario = new Usuario();
            typeof(Usuario)?.GetProperty(nameof(Usuario.CodigoUsuario))?.SetValue(_usuario, "MRAMOS");
            typeof(Usuario)?.GetProperty(nameof(Usuario.IndicadorHabilitado))?.SetValue(_usuario, "A");
            typeof(Usuario)?.GetProperty(nameof(Usuario.CodigoEmpresa))?.SetValue(_usuario, "1");
            typeof(Usuario)?.GetProperty(nameof(Usuario.CodigoAgencia))?.SetValue(_usuario, "01");
        }

        #endregion Inicializar

        #region Pruebas
        [TestMethod]
        public async Task AfiliacionServicioExitoso()
        {
            var numeroAfiliacion = 123;
            var numeroCuenta = "012364564565";
            var numeroTarjeta = 524224m;
            var codigoServicioInteroperabilidad = "18";

            var tarjeta = new Tarjeta();
            typeof(Tarjeta).GetProperty(nameof(Tarjeta.Duenio))?.SetValue(tarjeta, _cliente);
            typeof(Tarjeta).GetProperty(nameof(Tarjeta.NumeroTarjeta))?. SetValue(tarjeta, numeroTarjeta);

            _servicioAplicacionProductos.Setup(x => x
                .ObtenerClienteAPartirDeTarjeta(It.IsAny<string>()))
                .Returns(tarjeta);

            _repositorioGeneral.Setup(x => x
                .ObtenerNumeroSerieNoBloqueanteAsync("%", Cliente.EsquemaCliente, Afiliado.CodigoNumeroAfiliacion, 1)
                .Result)
                .Returns(numeroAfiliacion);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigoAsync<CuentaEfectivo>(It.IsAny<string>(), It.IsAny<string>())
                .Result)
                .Returns(_cuentaEfectivo);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigoAsync<Usuario>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())
                .Result)
                .Returns(_usuario);

            _repositorioGeneral.Setup(x => x
                .ObtenerCalendarioCuentaEfectivo())
                .Returns(_calendario);

            _servicioApliacionNotificaciones.Setup(x =>
                x.GenerarNotificacionTarjeta(
                    It.IsAny<Tarjeta>(), 
                    It.IsAny<string>(), 
                    It.IsAny<string>(), 
                    It.IsAny<DateTime>(), 
                    0, 
                    null)
                .Result)
                .Returns(new TarjetaMovimiento());

            var (resultadoAfiliacion, tarjetaMovimiento) = await _servicioAplicacionAfiliacion.AfiliacionServicioInterno(
                numeroTarjeta.ToString(),
                numeroCuenta,
                codigoServicioInteroperabilidad);

            Assert.AreEqual(numeroAfiliacion, resultadoAfiliacion.NumeroAfiliacion);
        }

        [TestMethod]
        public async Task DesafiliacionServicioExitoso()
        {
            var numeroTarjeta = 524224m;
            var codigoServicioInteroperabilidad = "18";

            var afiliacionInteroperabilidad = new AfiliacionInteroperabilidad();
            typeof(AfiliacionInteroperabilidad).GetProperty(nameof(AfiliacionInteroperabilidad.NumeroCuenta))?
                .SetValue(afiliacionInteroperabilidad, "001211101939555");
            var afiliaciones = new List<AfiliacionInteroperabilidadDetalle>();
            var afiliacion = new AfiliacionInteroperabilidadDetalle();
            typeof(AfiliacionInteroperabilidadDetalle).GetProperty(nameof(AfiliacionInteroperabilidadDetalle.FechaBloqueo))?
                .SetValue(afiliacion, DateTime.Now);
            typeof(AfiliacionInteroperabilidadDetalle).GetProperty(nameof(AfiliacionInteroperabilidadDetalle.CodigoCuentaInterbancario))?
                .SetValue(afiliacion, "81300121110193955555");
            typeof(AfiliacionInteroperabilidadDetalle).GetProperty(nameof(AfiliacionInteroperabilidadDetalle.IndicadorEstadoAfiliado))?
                .SetValue(afiliacion, "S");
            typeof(AfiliacionInteroperabilidadDetalle).GetProperty(nameof(AfiliacionInteroperabilidadDetalle.NumeroCelular))?
                .SetValue(afiliacion, "987654321");
            typeof(AfiliacionInteroperabilidadDetalle).GetProperty(nameof(AfiliacionInteroperabilidadDetalle.afiliacion))?
                .SetValue(afiliacion, afiliacionInteroperabilidad);
            typeof(AfiliacionInteroperabilidadDetalle).GetProperty(nameof(AfiliacionInteroperabilidadDetalle.FechaRegistro))?
                .SetValue(afiliacion, DateTime.Now);
            typeof(AfiliacionInteroperabilidadDetalle).GetProperty(nameof(AfiliacionInteroperabilidadDetalle.FechaModifico))?
                .SetValue(afiliacion, DateTime.Now);
            afiliaciones.Add(afiliacion);

            _repositorioOperaciones.Setup(x => x
               .ObtenerPorExpresionConLimite<AfiliacionInteroperabilidadDetalle>(
                   It.IsAny<Expression<Func<AfiliacionInteroperabilidadDetalle, bool>>>(), null, 0))
               .Returns(afiliaciones);

            var cuentaEfectivas = new List<CuentaEfectivo>();
            var cuentaEfectiva = new CuentaEfectivo();
            var tarjeta = new Tarjeta();

            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoAgencia))?.SetValue(cuentaEfectiva, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoEmpresa))?.SetValue(cuentaEfectiva, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoProducto))?.SetValue(cuentaEfectiva, "CC063");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.NumeroCuenta))?.SetValue(cuentaEfectiva, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoCuentaInterbancario))?.SetValue(cuentaEfectiva, "81300121110193955555");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoCliente))?.SetValue(cuentaEfectiva, "843212");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.SaldoDisponible))?.SetValue(cuentaEfectiva, 100m);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Moneda))?.SetValue(cuentaEfectiva, _moneda);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Cliente))?.SetValue(cuentaEfectiva, _cliente);

            typeof(Cliente).GetProperty(nameof(Cliente.Afiliaciones))?.SetValue(_cliente, new List<Afiliado>());
            cuentaEfectivas.Add(cuentaEfectiva);

            _repositorioOperaciones.Setup(x => x
               .ObtenerPorCodigoAsync<CuentaEfectivo>(
                   It.IsAny<string>(),
                   It.IsAny<string>())
                .Result)
               .Returns(cuentaEfectiva);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigoAsync<Tarjeta>(
                It.IsAny<decimal>())
                .Result)
                .Returns(tarjeta);

            _repositorioOperaciones.Setup(x => x
               .ObtenerPorExpresionConLimite<Afiliado>(
                   It.IsAny<Expression<Func<Afiliado, bool>>>(), null, 0)).Returns(new List<Afiliado>());

            _repositorioOperaciones.Setup(x => x
               .ObtenerPorCodigoAsync<Usuario>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()).Result)
               .Returns(_usuario);

            _repositorioGeneral.Setup(x => x
                .ObtenerCalendarioCuentaEfectivo())
                .Returns(_calendario);

            var historico = new CuentaAfiliadaHistorica();
            var afiliadoServicio = new AfiliadoServicio();
            var afiliado = new Afiliado();

            typeof(Afiliado).GetProperty(nameof(Afiliado.NumeroTarjeta))?.SetValue(afiliado, numeroTarjeta);
            typeof(Afiliado).GetProperty(nameof(Afiliado.NumeroMovimiento))?.SetValue(afiliado, 123456m);
            typeof(AfiliadoServicio).GetProperty(nameof(AfiliadoServicio.Afiliado))?.SetValue(afiliadoServicio, afiliado);

            typeof(CuentaAfiliadaHistorica).GetProperty(nameof(CuentaAfiliadaHistorica.NumeroCuenta))?.SetValue(historico, "123456");
            typeof(CuentaAfiliadaHistorica).GetProperty(nameof(CuentaAfiliadaHistorica.Cuenta))?.SetValue(historico, _cuentaEfectivo);
            typeof(CuentaAfiliadaHistorica).GetProperty(nameof(CuentaAfiliadaHistorica.AfiliadoServicio))?.SetValue(historico, afiliadoServicio);

            _servicioDominioAfiliacion.Setup(x => x
                .DesafiliarServicio(
                    It.IsAny<List<Afiliado>>(),
                    It.IsAny<ParametroPorEmpresa>(),
                    It.IsAny<Usuario>(),
                    It.IsAny<DateTime>()
                )).Returns(historico);

            var parametro = new ParametroPorEmpresa();
            typeof(ParametroPorEmpresa).GetProperty(nameof(ParametroPorEmpresa.ValorParametro))?.SetValue(parametro, codigoServicioInteroperabilidad);

            _servicioApliacionNotificaciones.Setup(x =>
                x.GenerarNotificacionTarjeta(
                    It.IsAny<Tarjeta>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<decimal>(),
                    It.IsAny<string>()))
                .ReturnsAsync(new TarjetaMovimiento());

            var (resultadoAfiliacion, tarjetaMovimiento) = await _servicioAplicacionAfiliacion.DesafiliacionAServicioInterno(numeroTarjeta.ToString(), parametro);

            Assert.IsNotNull(resultadoAfiliacion);
        }

        [TestMethod]
        public async Task AfiliarDirectorioCCEExitoso()
        {
            var codigoServicioInteroperabilidad = "18";
            var datosEntraAfiliacion = ADatosAfiliacion();

            var parametro = new ParametroPorEmpresa();
            typeof(ParametroPorEmpresa).GetProperty(nameof(ParametroPorEmpresa.ValorParametro))?.SetValue(parametro, codigoServicioInteroperabilidad);

            var qrGenerado = new RespuestaGenerarQR
            {
                IdentificadorQR = "12fdsfsd",
                CadenaQR = "asllalasldlalsflsfakfslaskalfsafsklasgksdgmokmgskdmgkelpepe"
            };

            var resultadoAfiliacionCCE = new RespuestaSalidaDTO<RespuestaRegistroDirectorioDTO>()
            {
                Datos = new RespuestaRegistroDirectorioDTO
                {
                    Respuesta = "ACTC",
                    NumeroSeguimiento = "132456"
                }
            };

            _repositorioOperaciones.Setup(x => x
               .ObtenerPorExpresionConLimite<AfiliacionInteroperabilidad>(
                   It.IsAny<Expression<Func<AfiliacionInteroperabilidad, bool>>>(), null, 0))
                .Returns(new List<AfiliacionInteroperabilidad>());

            _repositorioOperaciones.Setup(x => x
               .ObtenerPorExpresionConLimite<AfiliacionInteroperabilidadDetalle>(
                   It.IsAny<Expression<Func<AfiliacionInteroperabilidadDetalle, bool>>>(), null, 0))
                .Returns(new List<AfiliacionInteroperabilidadDetalle>());

            _repositorioGeneral.Setup(x => x
                .ObtenerCalendarioCuentaEfectivo())
                .Returns(_calendario);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<ParametroPorEmpresa>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(parametro);

            _servicioAplicacionInteroperabilidad.Setup(x => x
              .GestionarAfiliacionDirectorioCCE(
                  It.IsAny<EntradaAfiliacionDirectorioDTO>(),
                  It.IsAny<AfiliacionInteroperabilidadDetalle>()
              )).ReturnsAsync(resultadoAfiliacionCCE);

            _servicioAplicacionInteroperabilidad.Setup(x => x
               .GenerarQR(
                   It.IsAny<GenerarQRDTO>()
               )).ReturnsAsync(qrGenerado);

            _servicioDominioAfiliacion.Setup(x => x
               .CrearDetalleAfiliacion(
                   It.IsAny<AfiliacionInteroperabilidadDetalle>(),
                   It.IsAny<EntradaAfiliacionDirectorioDTO>(),
                   It.IsAny<RespuestaGenerarQR>(),
                   It.IsAny<string>(),
                   It.IsAny<DateTime>(),
                   It.IsAny<bool>(),
                   It.IsAny<bool>()
               )).Returns(new AfiliacionInteroperabilidadDetalle());

            await AfiliacionServicioExitoso();

            var resultadoAfiliacion = await _servicioAplicacionAfiliacion.AfiliacionDirectorioCCE(datosEntraAfiliacion);

            Assert.IsNotNull(resultadoAfiliacion.FechaOperacion);
            Assert.IsNotNull(resultadoAfiliacion.CadenaHash);
        }

        [TestMethod]
        public async Task DesafiliacionDirectorioCCEExitoso()
        {
            var codigoServicioInteroperabilidad = "18";
            var datosEntraAfiliacion = ADatosAfiliacion();
            datosEntraAfiliacion.TipoInstruccion = "DEAC";

            var parametro = new ParametroPorEmpresa();
            typeof(ParametroPorEmpresa).GetProperty(nameof(ParametroPorEmpresa.ValorParametro))?.SetValue(parametro, codigoServicioInteroperabilidad);

            var afiliacionInteroperabilidad = new AfiliacionInteroperabilidad();
            typeof(AfiliacionInteroperabilidad).GetProperty(nameof(AfiliacionInteroperabilidad.NumeroCuenta))?
                .SetValue(afiliacionInteroperabilidad, "001211101939555");
            var afiliaciones = new List<AfiliacionInteroperabilidadDetalle>();
            var afiliacion = new AfiliacionInteroperabilidadDetalle();
            typeof(AfiliacionInteroperabilidadDetalle).GetProperty(nameof(AfiliacionInteroperabilidadDetalle.FechaBloqueo))?
                .SetValue(afiliacion, DateTime.Now); 
            typeof(AfiliacionInteroperabilidadDetalle).GetProperty(nameof(AfiliacionInteroperabilidadDetalle.CodigoCuentaInterbancario))?
                .SetValue(afiliacion, "81300121110193955555");
            typeof(AfiliacionInteroperabilidadDetalle).GetProperty(nameof(AfiliacionInteroperabilidadDetalle.IndicadorEstadoAfiliado))?
                .SetValue(afiliacion, "S"); 
            typeof(AfiliacionInteroperabilidadDetalle).GetProperty(nameof(AfiliacionInteroperabilidadDetalle.NumeroCelular))?
                .SetValue(afiliacion, "987654321");
            typeof(AfiliacionInteroperabilidadDetalle).GetProperty(nameof(AfiliacionInteroperabilidadDetalle.afiliacion))?
                .SetValue(afiliacion, afiliacionInteroperabilidad);
            typeof(AfiliacionInteroperabilidadDetalle).GetProperty(nameof(AfiliacionInteroperabilidadDetalle.FechaRegistro))?
                .SetValue(afiliacion, DateTime.Now);
            typeof(AfiliacionInteroperabilidadDetalle).GetProperty(nameof(AfiliacionInteroperabilidadDetalle.FechaModifico))?
                .SetValue(afiliacion, DateTime.Now);
            afiliaciones.Add(afiliacion);

            var cuentaEfectivas = new List<CuentaEfectivo>();
            var cuentaEfectiva = new CuentaEfectivo();
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoAgencia))?.SetValue(cuentaEfectiva, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoEmpresa))?.SetValue(cuentaEfectiva, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoProducto))?.SetValue(cuentaEfectiva, "CC063");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.NumeroCuenta))?.SetValue(cuentaEfectiva, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoCuentaInterbancario))?.SetValue(cuentaEfectiva, "81300121110193955555");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoCliente))?.SetValue(cuentaEfectiva, "843212");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.SaldoDisponible))?.SetValue(cuentaEfectiva, 100m);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Moneda))?.SetValue(cuentaEfectiva, _moneda);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Cliente))?.SetValue(cuentaEfectiva, _cliente);

            typeof(Cliente).GetProperty(nameof(Cliente.Afiliaciones))?.SetValue(_cliente, new List<Afiliado>());            
            cuentaEfectivas.Add(cuentaEfectiva);

            _repositorioOperaciones.Setup(x => x
              .ObtenerPorCodigo<CuentaEfectivo>(
                   It.IsAny<string>(),
                   It.IsAny<string>()))
               .Returns(cuentaEfectiva);

            var resultadoAfiliacionCCE = new RespuestaSalidaDTO<RespuestaRegistroDirectorioDTO>()
            {
                Datos = new RespuestaRegistroDirectorioDTO
                {
                    Respuesta = "ACTC",
                    NumeroSeguimiento = "132456"
                }
            };

            _repositorioOperaciones.Setup(x => x
               .ObtenerPorExpresionConLimite<AfiliacionInteroperabilidadDetalle>(
                   It.IsAny<Expression<Func<AfiliacionInteroperabilidadDetalle, bool>>>(), null, 0))
               .Returns(afiliaciones);

            _repositorioGeneral.Setup(x => x
               .ObtenerCalendarioCuentaEfectivo())
               .Returns(_calendario);

            _servicioAplicacionInteroperabilidad.Setup(x => x
              .GestionarAfiliacionDirectorioCCE(
                  It.IsAny<EntradaAfiliacionDirectorioDTO>(),
                  It.IsAny<AfiliacionInteroperabilidadDetalle>()
              )).ReturnsAsync(resultadoAfiliacionCCE);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<ParametroPorEmpresa>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(parametro);

            await DesafiliacionServicioExitoso();

            var resultadoAfiliacion = await _servicioAplicacionAfiliacion.DesafiliacionDirectorioCCE(datosEntraAfiliacion);

            Assert.IsNotNull(resultadoAfiliacion.FechaOperacion);
        }

        #endregion

        #region Datos para pruebas 
        public EntradaAfiliacionDirectorioDTO ADatosAfiliacion()
        {
            return new EntradaAfiliacionDirectorioDTO
            {
                CodigoEntidadOriginante = "0813",
                TipoInstruccion = "NEWR",
                NumeroCelular = "987654321",
                TipoOperacion = "19",
                CodigoCliente = "987654321",
                NumeroCuentaAfiliada = "321654987",
                CodigoCuentaInterbancario = "81300121110193955555",
                IndicadorModificarNumero = "N",
                NumeroAntiguo = "",
                NumeroTarjeta = "4772000011999999"
            };
        }

        #endregion Datos para pruebas 
    }

}
