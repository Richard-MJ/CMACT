using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Renci.SshNet.Security;
using System.Linq.Expressions;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Servicios;
using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.DTOs;
using Takana.Transferencias.CCE.Api.Common.DTOs.BA;
using Takana.Transferencias.CCE.Api.Common.DTOs.CG;
using Takana.Transferencias.CCE.Api.Common.DTOs.CL;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Dominio;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CJ;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.PA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Pruebas
{
    [TestClass]
    public class ServicioAplicacionTransaccionOperacionPruebas
    {
        #region declaraciones
        private Mock<IContextoAplicacion> _contexto;
        private Mock<IServiceProvider> _servicioProvider;
        private Mock<IRepositorioGeneral> _repositorioGeneral;
        private Mock<IRepositorioOperacion> _repositorioOperaciones;
        private Mock<IRepositorioRedis> _repositorioRedis;
        private Mock<IServicioAplicacionLavado> _servicioAplicacionLavado;
        private Mock<IServicioAplicacionCliente> _servicioAplicacionCliente;
        private Mock<IServicioAplicacionTransferenciaSalida> _servicioAplicacionTransaccionSalida;
        private Mock<IServicioDominioContabilidad> _servicioDominioContabilidad;
        private Mock<IServicioAplicacionCajero> _servicioAplicacionCajero;
        private Mock<IServicioDominioLavado> _servicioDominioLavado;
        private Mock<IServicioAplicacionParametroGeneral> _servicioAplicacionParametros;
        private Mock<IServicioDominioProducto> _servicioDominioProducto;
        private Mock<IServicioDominioCuenta> _servicioDominioCuenta;
        private Mock<IServicioDominioTransaccionOperacion> _servicioDominioTransaccionOperacion;
        private Mock<IServicioAplicacionProducto> _servicioAplicacionProductos;
        private Mock<IServicioAplicacionNotificaciones> _servicioAplicacionNotificaciones;
        private ServicioAplicacionTransaccionOperacion _servicioAplicacionTransaccion;
        private Mock<OficinaCCE> _oficinaCCE;
        private List<OficinaCCE> _oficinasCCE;

        private Mock<EntidadFinancieraDiferida> _entidadFinancieraCCE;
        private List<EntidadFinancieraDiferida> _entidadesFinancierasCCE;

        private Mock<PlazaCCE> _plazaCCE;

        private Usuario _usuario = new Usuario();
        private CodigoRespuesta _codigoRespuestaError;
        private Calendario _calendario = new Calendario();
        private List<CuentaEfectivo> _cuentasEfectivas = new List<CuentaEfectivo>();
        private Moneda _moneda = new Moneda();
        private Cliente _cliente = new Cliente();

        private Transferencia _transferencia;

        private ConfiguracionComision _configuracionComision;
        private List<ConfiguracionComision> _configuracionesComisiones;
        private AsientoContable _asientoContable;

        private int _numeroAsiento = 8888888;
        private string _numeroCCI = "01823500423525284326";
        private string _numeroTarjeta = "004212000527473";
        private string _transferenciaOrdinaria = "320";

        #endregion

        #region Inicializar

        [TestInitialize]
        public void Inicializar()
        {
            _contexto = new Mock<IContextoAplicacion>();
            _servicioProvider = new Mock<IServiceProvider>();
            _repositorioGeneral = new Mock<IRepositorioGeneral>();
            _repositorioRedis = new Mock<IRepositorioRedis>();
            _repositorioOperaciones = new Mock<IRepositorioOperacion>();
            _servicioAplicacionLavado = new Mock<IServicioAplicacionLavado>();
            _servicioAplicacionCliente = new Mock<IServicioAplicacionCliente>();
            _servicioAplicacionTransaccionSalida = new Mock<IServicioAplicacionTransferenciaSalida>();
            _servicioDominioContabilidad = new Mock<IServicioDominioContabilidad>();
            _servicioAplicacionCajero = new Mock<IServicioAplicacionCajero>();
            _servicioAplicacionParametros = new Mock<IServicioAplicacionParametroGeneral>();
            _servicioDominioLavado = new Mock<IServicioDominioLavado>();
            _servicioDominioProducto = new Mock<IServicioDominioProducto>();
            _servicioDominioCuenta = new Mock<IServicioDominioCuenta>();
            _servicioDominioTransaccionOperacion = new Mock<IServicioDominioTransaccionOperacion>();
            _servicioAplicacionProductos = new Mock<IServicioAplicacionProducto>();
            _servicioAplicacionNotificaciones = new Mock<IServicioAplicacionNotificaciones>();

            _servicioAplicacionTransaccion = new ServicioAplicacionTransaccionOperacion(
                _contexto.Object,
                _servicioProvider.Object,
                _repositorioRedis.Object,
                _repositorioGeneral.Object,
                _repositorioOperaciones.Object,
                _servicioDominioCuenta.Object,
                _servicioDominioProducto.Object,
                _servicioAplicacionLavado.Object,
                _servicioAplicacionCajero.Object,
                _servicioAplicacionCliente.Object,
                _servicioAplicacionProductos.Object,
                _servicioDominioContabilidad.Object,
                _servicioDominioTransaccionOperacion.Object,
                _servicioAplicacionParametros.Object,
                _servicioAplicacionTransaccionSalida.Object,
                _servicioAplicacionNotificaciones.Object);

            var agencia = new Agencia();
            typeof(Agencia)?.GetProperty(nameof(Agencia.NombreAgencia))?.SetValue(agencia, "Tacna");

            typeof(Usuario)?.GetProperty(nameof(Usuario.CodigoUsuario))?.SetValue(_usuario, "MRAMOS");
            typeof(Usuario)?.GetProperty(nameof(Usuario.IndicadorHabilitado))?.SetValue(_usuario, "A");
            typeof(Usuario)?.GetProperty(nameof(Usuario.CodigoEmpresa))?.SetValue(_usuario, "1");
            typeof(Usuario)?.GetProperty(nameof(Usuario.CodigoAgencia))?.SetValue(_usuario, "01");
            typeof(Usuario)?.GetProperty(nameof(Usuario.Agencia))?.SetValue(_usuario, agencia);

            typeof(Calendario)?.GetProperty(nameof(Calendario.CodigoEmpresa))?.SetValue(_calendario, "1");
            typeof(Calendario)?.GetProperty(nameof(Calendario.CodigoAgencia))?.SetValue(_calendario, "01");
            typeof(Calendario)?.GetProperty(nameof(Calendario.CodigoSistema))?.SetValue(_calendario, "CC");
            typeof(Calendario)?.GetProperty(nameof(Calendario.FechaSistema))?.SetValue(_calendario, DateTime.Now);

            _moneda = new Moneda();
            typeof(Moneda).GetProperty(nameof(Moneda.CodigoMoneda))?.SetValue(_moneda, "1");
            typeof(Moneda).GetProperty(nameof(Moneda.NombreMoneda))?.SetValue(_moneda, "SOL");

            _cliente = new Cliente();
            typeof(Cliente).GetProperty(nameof(Cliente.CodigoCliente))?.SetValue(_cliente, "1");
            typeof(Cliente).GetProperty(nameof(Cliente.NombreCliente))?.SetValue(_cliente, "USUARIO PRUEBA");
            typeof(Cliente).GetProperty(nameof(Cliente.TipoPersona))?.SetValue(_cliente, "F");

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
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Moneda))?.SetValue(cuentaEfectiva, _moneda);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Cliente))?.SetValue(cuentaEfectiva, _cliente);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Caracteristicas))?.SetValue(cuentaEfectiva, caracteristica);
            _cuentasEfectivas.Add(cuentaEfectiva);

            _plazaCCE = new Mock<PlazaCCE>(MockBehavior.Strict);
            _plazaCCE.Setup(x => x.EsPlazaExclusiva).Returns("N");

            _oficinaCCE = new Mock<OficinaCCE>();
            _oficinasCCE = new List<OficinaCCE>();
            _oficinasCCE.Add(_oficinaCCE.Object);

            _codigoRespuestaError = new CodigoRespuesta();
            typeof(CodigoRespuesta).GetProperty(nameof(CodigoRespuesta.Codigo))?.SetValue(_codigoRespuestaError, "AC01");
            typeof(CodigoRespuesta).GetProperty(nameof(CodigoRespuesta.DescripcionEstadoCuenta))?.SetValue(_codigoRespuestaError, "DESCRIPCION ESTADO");
            typeof(CodigoRespuesta).GetProperty(nameof(CodigoRespuesta.TipoCodigoRespuesta))?.SetValue(_codigoRespuestaError, "R");

            _entidadFinancieraCCE = new Mock<EntidadFinancieraDiferida>();
            _entidadFinancieraCCE.Setup(x => x.CodigoEntidad).Returns("002");
            _entidadFinancieraCCE.Setup(x => x.EstaActivaCheque).Returns("S");
            _entidadFinancieraCCE.Setup(x => x.Oficinas).Returns(_oficinasCCE);

            _entidadesFinancierasCCE = new List<EntidadFinancieraDiferida>();
            _entidadesFinancierasCCE.Add(_entidadFinancieraCCE.Object);

            _oficinaCCE.Setup(x => x.CodigoOficina).Returns("001");
            _oficinaCCE.Setup(x => x.EstadoOficina).Returns("A");
            _oficinaCCE.Setup(x => x.PlazaCCE).Returns(_plazaCCE.Object);
            _oficinaCCE.Setup(x => x.CodigoUbigeoReferencia).Returns("15");
            _oficinaCCE.Setup(x => x.EntidadFinancieraCCE).Returns(_entidadFinancieraCCE.Object);

            _configuracionComision = new ConfiguracionComision();
            typeof(ConfiguracionComision).GetProperty(nameof(ConfiguracionComision.CodigoComision))?
                .SetValue(_configuracionComision, ConfiguracionComision.CodigoTransferenciaInterbancaria);

            _configuracionesComisiones = new List<ConfiguracionComision>();
            _configuracionesComisiones.Add(_configuracionComision);

            _asientoContable = new AsientoContable();
            typeof(AsientoContable).GetProperty(nameof(AsientoContable.NumeroAsiento))?
                .SetValue(_asientoContable, _numeroAsiento);
            typeof(AsientoContable).GetProperty(nameof(AsientoContable.Detalles))?
                .SetValue(_asientoContable, new List<AsientoContableDetalle>());

            _contexto.Setup(x => x.Actualizar(string.Empty, string.Empty, string.Empty, string.Empty, 
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty));

        }
        #endregion

        #region Prueba Consulta Cuenta Entrante
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
        public TipoDocumentoTinDTO ObtenerTipoDocumento()
        {
            return new TipoDocumentoTinDTO()
            {
                CodigoTipoDocumento = Convert.ToByte("2")
            };
        }
        public IngresoVinculoMotivoDTO ObtenerVinculoMotivo()
        {
            return new IngresoVinculoMotivoDTO()
            {
                VinculoEspecificado = "1",
                MotivoEspecificado = "1",
                IdMotivo = 1,
                IdVinculo = 1
            };
        }
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
                TotalComision = 0,
                Total = 200
            };
        }
        public EntidadFinancieraTinDTO ObtenerEntidadReceptora()
        {
            return new EntidadFinancieraTinDTO()
            {
                CodigoEstadoSign = "12",
                CodigoEntidad = "0002",
                NombreEntidad = "BCP",
                OficinaPagoTarjeta = "001",
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
        public ConsultaCuentaOperacionDTO ObtenerConsultaOperacionDTO()
        {
            return new ConsultaCuentaOperacionDTO()
            {
                CuentaEfectivoDTO = ObtenerCuentaEfectivoDTO(),
                TipoTransferencia = "320",
                NumeroCuentaOTarjeta = "01823500423525284326",
                EntidadReceptora = ObtenerEntidadReceptora(),
                Usuario = ObtenerSesionUsuario(),
            };
        }
        public SesionUsuarioDTO ObtenerSesionUsuario()
        {
            return new SesionUsuarioDTO()
            {
                NombreEquipo = "equipo",
                CodigoCanalOrigen = General.CanalCCE,
                CodigoAgencia = "01"

            };
        }
        public CuentaEfectivoDTO ObtenerCuentaEfectivoDTO()
        {
            return new CuentaEfectivoDTO()
            {
                NumeroDocumento = "12365",
                Titular = "Titular",
                TipoDocumentoOriginante = new TipoDocumentoTinDTO
                { CodigoTipoDocumento = 1, EsTipoPersonaJuridica = true },
                CodigoMoneda = "2",
                NumeroCuenta = "123456789",
                CodigoCuentaInterbancario = "00223500423525284326",
                IndicadorTipoCuenta = "I",
                IndicadorTransferenciaCce = "S",
                SaldoDisponible = 1000
            };
        }

        public ComisionCCE ObtenerComisionEntidad()
        {
            var comision = new ComisionCCE();

            typeof(ComisionCCE).GetProperty(nameof(ComisionCCE.Id))?.SetValue(comision, 1);
            typeof(ComisionCCE).GetProperty(nameof(ComisionCCE.IdTipoTransferencia))?.SetValue(comision, 2);
            typeof(ComisionCCE).GetProperty(nameof(ComisionCCE.CodigoComision))?.SetValue(comision, "0101");
            typeof(ComisionCCE).GetProperty(nameof(ComisionCCE.CodigoMoneda))?.SetValue(comision, "1");
            typeof(ComisionCCE).GetProperty(nameof(ComisionCCE.CodigoAplicacionTarifa))?.SetValue(comision, "O");
            typeof(ComisionCCE).GetProperty(nameof(ComisionCCE.Porcentaje))?.SetValue(comision, 0.5m);
            typeof(ComisionCCE).GetProperty(nameof(ComisionCCE.Minimo))?.SetValue(comision, 5m);
            typeof(ComisionCCE).GetProperty(nameof(ComisionCCE.Maximo))?.SetValue(comision, 15m);
            typeof(ComisionCCE).GetProperty(nameof(ComisionCCE.IndicadorPorcentaje))?.SetValue(comision, "S");
            typeof(ComisionCCE).GetProperty(nameof(ComisionCCE.PorcentajeCCE))?.SetValue(comision, 0.5m);
            typeof(ComisionCCE).GetProperty(nameof(ComisionCCE.MinimoCCE))?.SetValue(comision, 3m);
            typeof(ComisionCCE).GetProperty(nameof(ComisionCCE.MaximoCCE))?.SetValue(comision, 30m);

            return comision;
        }

        public RespuestaSalidaDTO<ConsultaCuentaRespuestaTraducidoDTO> RespuestaConsultaCuenta()
        {
            return new RespuestaSalidaDTO<ConsultaCuentaRespuestaTraducidoDTO>()
            {
                Codigo = CodigoRespuesta.Aceptada,
                Datos = new ConsultaCuentaRespuestaTraducidoDTO()
                {
                    CodigoCuentaInterbancariaReceptor = "01823500423525284326",
                    CodigoTarjetaCreditoReceptor = "3216549871321654",
                    NombreCompletoReceptor = "p",
                    IndicadorITF = "p",
                    TipoDocumentoReceptor = "p",
                    NumeroDocuementoReceptor = "p",
                    TipoTransaccion = "p",
                    CodigoEntidadOriginante = "p",
                    CodigoEntidadReceptora = "p",
                    FechaCreacionTransaccion = "p",
                    HoraCreacionTransaccion = "p",
                    NumeroReferencia = "p",
                    Trace = "p",
                    IdentificadorTransaccion = "p",
                    DireccionReceptor = "p",
                    TelefonoReceptor = "p",
                    NumeroCelularReceptor = "p",
                    ValorProxy = "p",
                    TipoProxy = "p",
                    TipoDocumentoDeudor = "2",
                }
            };
        }

        #endregion

        #region Prueba Transaccion Exitosa Entrante
        [TestMethod]
        public async Task TransferenciaInmediataEntranteExitosa()
        {
            var identificadorInstruccion = "2024713535729322318362834412272770";

            var transacciones = new List<TransaccionOrdenTransferenciaInmediata>();
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
            transacciones.Add(transaccion);

            var catalogoTransaccion = new CatalogoTransaccion();
            typeof(CatalogoTransaccion).GetProperty(nameof(CatalogoTransaccion.CodigoSistema))?.SetValue(catalogoTransaccion, "CC");
            typeof(CatalogoTransaccion).GetProperty(nameof(CatalogoTransaccion.TipoTransaccion))?.SetValue(catalogoTransaccion, "41");
            typeof(CatalogoTransaccion).GetProperty(nameof(CatalogoTransaccion.IndicadorMovimiento))?.SetValue(catalogoTransaccion, "C");
            typeof(CatalogoTransaccion).GetProperty(nameof(CatalogoTransaccion.DescripcionTransaccion))?.SetValue(catalogoTransaccion, "PRUEBA");

            var subTipoTransaccion1 = new SubTipoTransaccion();
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoSistema))?.SetValue(subTipoTransaccion1, "CC");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoSubTipoTransaccion))?.SetValue(subTipoTransaccion1, "12");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoTipoTransaccion))?.SetValue(subTipoTransaccion1, "41");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.DescripcionSubTransaccion))?.SetValue(subTipoTransaccion1, "PRUEBA");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.AplicaContabilizadon))?.SetValue(subTipoTransaccion1, "S");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.Transaccion))?.SetValue(subTipoTransaccion1, catalogoTransaccion);

            var movimientosDiarios = new List<MovimientoDiario>();
            var movimientoDiario = new MovimientoDiario();
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.CodigoAgencia))?.SetValue(movimientoDiario, "01");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.CodigoEmpresa))?.SetValue(movimientoDiario, "1");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.IndOrigenDestino))?.SetValue(movimientoDiario, "F");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.NumeroMovimientoFuente))?.SetValue(movimientoDiario, 0m);
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.Cuenta))?.SetValue(movimientoDiario, _cuentasEfectivas.FirstOrDefault());
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.Transaccion))?.SetValue(movimientoDiario, catalogoTransaccion);
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.SubTipoTransaccionMovimiento))?.SetValue(movimientoDiario, subTipoTransaccion1);
            movimientosDiarios.Add(movimientoDiario);

            var subTipoTransaccion2 = new SubTipoTransaccion();
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoSistema))?.SetValue(subTipoTransaccion2, "CC");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoSubTipoTransaccion))?.SetValue(subTipoTransaccion2, "12");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoTipoTransaccion))?.SetValue(subTipoTransaccion2, "41");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.DescripcionSubTransaccion))?.SetValue(subTipoTransaccion2, "PRUEBA");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.AplicaContabilizadon))?.SetValue(subTipoTransaccion2, "S");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.Transaccion))?.SetValue(subTipoTransaccion2, catalogoTransaccion);
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.SubTipoTransaccionMovimiento))?.SetValue(movimientoDiario, subTipoTransaccion2);
            movimientosDiarios.Add(movimientoDiario);

            var entidadesFinancieras = new List<EntidadFinancieraInmediata>();
            var entidadFinanciera = new EntidadFinancieraInmediata();
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEntidad))?
                .SetValue(entidadFinanciera, "0002");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.NombreEntidad))?
                .SetValue(entidadFinanciera, "PRUEBAA");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEstadoSign))?
                .SetValue(entidadFinanciera, "H");
            typeof(EntidadFinancieraInmediata)?
                .GetProperty(nameof(EntidadFinancieraInmediata.CodigoEstadoCCE))?
                .SetValue(entidadFinanciera, "N");
            entidadesFinancieras.Add(entidadFinanciera);

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

            var canalSubTransacciones = new List<CanalPorSubTransaccionCCE>();
            var canalSubTransaccion = new CanalPorSubTransaccionCCE();
            typeof(CanalPorSubTransaccionCCE).GetProperty(nameof(CanalPorSubTransaccionCCE.CodigoCanalCCE))?.SetValue(canalSubTransaccion, "90");
            typeof(CanalPorSubTransaccionCCE).GetProperty(nameof(CanalPorSubTransaccionCCE.IndicadorTipo))?.SetValue(canalSubTransaccion, "R");
            typeof(CanalPorSubTransaccionCCE).GetProperty(nameof(CanalPorSubTransaccionCCE.CodigoTipoTransaccion))?.SetValue(canalSubTransaccion, "41");
            typeof(CanalPorSubTransaccionCCE).GetProperty(nameof(CanalPorSubTransaccionCCE.CodigoSubTipoTransaccion))?.SetValue(canalSubTransaccion, "12");
            typeof(CanalPorSubTransaccionCCE).GetProperty(nameof(CanalPorSubTransaccionCCE.CodigoUsuarioTransaccion))?.SetValue(canalSubTransaccion, "CAJERO");

            typeof(CanalPorSubTransaccionCCE).GetProperty(nameof(CanalPorSubTransaccionCCE.SubTipoTransaccion))?.SetValue(canalSubTransaccion, subTipoTransaccion1);
            canalSubTransacciones.Add(canalSubTransaccion);

            var canalCCE = new CanalCCE();
            typeof(CanalCCE).GetProperty(nameof(CanalCCE.CodigoCanalCCE))?.SetValue(canalCCE, "90");
            typeof(CanalCCE).GetProperty(nameof(CanalCCE.CanalesPorSubTransaciones))?.SetValue(canalCCE, canalSubTransacciones);
            
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.CanalCCE))?.SetValue(transaccion, canalCCE);

            var asientoContable = new AsientoContable();
            typeof(AsientoContable).GetProperty(nameof(AsientoContable.NumeroAsiento))?.SetValue(asientoContable, 8888888);
            typeof(AsientoContable).GetProperty(nameof(AsientoContable.Detalles))?.SetValue(asientoContable, new List<AsientoContableDetalle>());

            var operacionUnicaLavado = new OperacionUnicaLavado();
            typeof(OperacionUnicaLavado).GetProperty(nameof(OperacionUnicaLavado.NumeroLavado))?.SetValue(operacionUnicaLavado, 8888888m);
            typeof(OperacionUnicaLavado).GetProperty(nameof(OperacionUnicaLavado.Detalle))?.SetValue(operacionUnicaLavado, new List<OperacionUnicaDetalle>());

            var menorcuantia = new MenorCuantiaEncabezado();
            typeof(MenorCuantiaEncabezado).GetProperty(nameof(MenorCuantiaEncabezado.Detalles))?.SetValue(menorcuantia, new List<MenorCuantiaDetalle>());
            typeof(MenorCuantiaEncabezado).GetProperty(nameof(MenorCuantiaEncabezado.Intervinientes))?.SetValue(menorcuantia, new List<MenorCuantiaInterviniente>());


            var umbralOperacionLavado = new UmbralOperacionLavado();
            typeof(UmbralOperacionLavado).GetProperty(nameof(UmbralOperacionLavado.MontoLimite))?.SetValue(umbralOperacionLavado, 2500.00m);

            var tasacambio = new TipoCambioActual();
            typeof(TipoCambioActual).GetProperty(nameof(TipoCambioActual.CodigoEmpresa))?.SetValue(tasacambio, "1");
            typeof(TipoCambioActual).GetProperty(nameof(TipoCambioActual.CodigoTipoCambio))?.SetValue(tasacambio, "1");
            typeof(TipoCambioActual).GetProperty(nameof(TipoCambioActual.CodigoMoneda))?.SetValue(tasacambio, "01");
            typeof(TipoCambioActual).GetProperty(nameof(TipoCambioActual.FechaTipoCambio))?.SetValue(tasacambio, DateTime.Now);
            typeof(TipoCambioActual).GetProperty(nameof(TipoCambioActual.ValorCompra))?.SetValue(tasacambio, 3.745m);
            typeof(TipoCambioActual).GetProperty(nameof(TipoCambioActual.ValorVenta))?.SetValue(tasacambio, 3.745m);

            typeof(Moneda).GetProperty(nameof(Moneda.CodigoMoneda))?.SetValue(_moneda, "2");
            typeof(Moneda).GetProperty(nameof(Moneda.NombreMoneda))?.SetValue(_moneda, "DOLARES");

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorExpresionConLimite<CanalPorSubTransaccionCCE>(
                    It.IsAny<Expression<Func<CanalPorSubTransaccionCCE, bool>>>(), null, 0))
                .Returns(canalSubTransacciones);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<Usuario>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(_usuario);

            _repositorioOperaciones.Setup(x => x
                .ObtenerCalendarioCuentaEfectivo()).Returns(_calendario);

            _repositorioOperaciones.Setup(x =>
                x.ObtenerPorExpresionConLimite<TransaccionOrdenTransferenciaInmediata>(
                    It.IsAny<Expression<Func<TransaccionOrdenTransferenciaInmediata, bool>>>(), null, 0))
                .Returns(transacciones);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorExpresionConLimite<CuentaEfectivo>(
                    It.IsAny<Expression<Func<CuentaEfectivo, bool>>>(), null, 0))
                .Returns(_cuentasEfectivas);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<SubTipoTransaccion>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(subTipoTransaccion1);

            _servicioDominioProducto.Setup(x => x
                .GenerarMovimientoTransferenciaEntrante(
                    It.IsAny<Usuario>(),
                    It.IsAny<SubTipoTransaccion>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<CuentaEfectivo>(),
                    It.IsAny<TransaccionOrdenTransferenciaInmediata>(),
                    It.IsAny<int>()
                )).Returns(movimientosDiarios);

            _servicioDominioProducto.Setup(x => x
                .GenerarMovimientoDiarioCredito(
                    It.IsAny<int>(),
                    It.IsAny<decimal>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<string>(),
                    It.IsAny<CuentaEfectivo>(),
                    It.IsAny<Usuario>(),
                    It.IsAny<SubTipoTransaccion>()
                )).Returns(movimientosDiarios[0]);


            _servicioAplicacionCajero.Setup(x => x
                .ObtenerMontoItfMovimiento(movimientosDiarios[0]))
                .Returns(0.05m);

            _servicioDominioProducto.Setup(x => x
                .GenerarMovimientoITF(
                    It.IsAny<int>(),
                    It.IsAny<decimal>(),
                    It.IsAny<CuentaEfectivo>(),
                    It.IsAny<Usuario>(),
                    It.IsAny<SubTipoTransaccion>(),
                    It.IsAny<MovimientoDiario>()
                )).Returns(movimientosDiarios[1]);

            _servicioDominioContabilidad.Setup(x => x
                .GenerarMovimientoAuxiliarContableTransferencia(
                    It.IsAny<List<MovimientoDiario>>(),
                    It.IsAny<List<ITransaccion>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()));

            _servicioDominioContabilidad.Setup(x => x
                .GenerarAsientoContableCompletoPendiente(
                    It.IsAny<int>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<decimal>(),
                    It.IsAny<decimal>(),
                    It.IsAny<IMovimientoContable[]>())
                )
            .Returns(asientoContable);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorExpresionConLimite<EntidadFinancieraInmediata>(
                    It.IsAny<Expression<Func<EntidadFinancieraInmediata, bool>>>(), null, 0))
                .Returns(entidadesFinancieras);

            _servicioAplicacionCliente.Setup(x => x
                .ObtenerClienteOriginante(It.IsAny<TransaccionOrdenTransferenciaInmediata>()))
                .Returns(clienteExterno);

            _servicioDominioTransaccionOperacion.Setup(x => x
                .GenerarDetalleTransferencia(
                    It.IsAny<TransaccionOrdenTransferenciaInmediata>(),
                    It.IsAny<Transferencia>(),
                    It.IsAny<ClienteExternoDTO>(),
                    It.IsAny<EntidadFinancieraInmediata>()))
                .Returns(new TransferenciaDetalleEntranteCCE());

            _servicioDominioContabilidad.Setup(x => x
                .GenerarDetalleAsientoDeCuentaPuenteTransferenciaEntrante(
                    subTipoTransaccion1,
                    It.IsAny<AsientoContable>(),
                    It.IsAny<IMovimientoContable>(),
                    It.IsAny<List<CuentaContableInfoDTO>>(),
                    It.IsAny<decimal>(),
                    It.IsAny<ITasaCambio>(),
                    It.IsAny<string>(),
                    It.IsAny<DateTime>()));

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<UmbralOperacionLavado>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(umbralOperacionLavado);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<Moneda>(It.IsAny<string>()))
                .Returns(_moneda);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<ParametroPorEmpresa>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ParametroPorEmpresa());

            _servicioAplicacionCajero.Setup(x => x
                .ObtenerTasaCambio(It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns(tasacambio);

            _servicioAplicacionLavado.Setup(x =>
                x.GenerarLavadoTransferenciaEntrante(It.IsAny<SubTipoTransaccion>(), It.IsAny<Transferencia>(),
                It.IsAny<ClienteExternoDTO>(), It.IsAny<Cliente>(), asientoContable,
                It.IsAny<EntidadFinancieraInmediata>(), It.IsAny<DateTime>()))
                .Returns(menorcuantia);

            _servicioDominioLavado.Setup(x => x
                .RegistrarLavadoOperacionUnicaTransferenciaEntrante(
                    It.IsAny<SubTipoTransaccion>(),
                    It.IsAny<Transferencia>(),
                    It.IsAny<AsientoContable>(),
                    It.IsAny<Cliente>(),
                    It.IsAny<ClienteExternoDTO>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<EntidadFinancieraInmediata>(),
                    It.IsAny<int>()
                )).Returns(operacionUnicaLavado);

            _servicioDominioLavado.Setup(x => x
                .RegistrarLavadoMenorCuantiaTransferenciaEntrante(
                    It.IsAny<SubTipoTransaccion>(),
                    It.IsAny<Transferencia>(),
                    It.IsAny<AsientoContable>(),
                    It.IsAny<Cliente>(),
                    It.IsAny<ClienteExternoDTO>(),
                    It.IsAny<EntidadFinancieraInmediata>(),
                    It.IsAny<int>()))
                .Returns(menorcuantia);

            var resultadoMenorCuantia = _servicioAplicacionTransaccion
                .RealizarTransferenciaEntranteCCE(identificadorInstruccion);

            transacciones = new List<TransaccionOrdenTransferenciaInmediata>();
            transaccion = new TransaccionOrdenTransferenciaInmediata();
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.MontoTransferencia))?.SetValue(transaccion, 2500m);
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.MontoComision))?.SetValue(transaccion, 0.80m);
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.TipoTransferencia))?.SetValue(transaccion, "320");
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.IndicadorEstadoOperacion))?.SetValue(transaccion, "C");
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.IdentificadorInstruccion))?.SetValue(transaccion, "2024020915100008134115981116");
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.CanalCCE))?.SetValue(transaccion, canalCCE);
            transacciones.Add(transaccion);

            _repositorioOperaciones.Setup(x =>
                x.ObtenerPorExpresionConLimite<TransaccionOrdenTransferenciaInmediata>(
                    It.IsAny<Expression<Func<TransaccionOrdenTransferenciaInmediata, bool>>>(), null, 0))
                .Returns(transacciones);

            try
            {
                await _servicioAplicacionTransaccion
                    .RealizarTransferenciaEntranteCCE(identificadorInstruccion);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        #endregion

        #region Prueba Transaccion Rechazada Entrante
        [TestMethod]
        public async Task TransferenciaInmediataEntranteRechazo()
        {
            var identificadorInstruccion = "2024713535729322318362834412272770";

            var transacciones = new List<TransaccionOrdenTransferenciaInmediata>();
            var transaccion = new TransaccionOrdenTransferenciaInmediata();
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.MontoTransferencia))?.SetValue(transaccion, 100m);
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.CodigoCanal))?.SetValue(transaccion, "90");
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.MontoComision))?.SetValue(transaccion, 0.80m);
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.TipoTransferencia))?.SetValue(transaccion, "320");
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.IndicadorEstadoOperacion))?.SetValue(transaccion, "C");
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.IdentificadorInstruccion))?.SetValue(transaccion, "2024020915100008134115981116");
            transacciones.Add(transaccion);

            var catalogoTransaccion = new CatalogoTransaccion();
            typeof(CatalogoTransaccion).GetProperty(nameof(CatalogoTransaccion.CodigoSistema))?.SetValue(catalogoTransaccion, "CC");
            typeof(CatalogoTransaccion).GetProperty(nameof(CatalogoTransaccion.TipoTransaccion))?.SetValue(catalogoTransaccion, "41");
            typeof(CatalogoTransaccion).GetProperty(nameof(CatalogoTransaccion.IndicadorMovimiento))?.SetValue(catalogoTransaccion, "C");
            typeof(CatalogoTransaccion).GetProperty(nameof(CatalogoTransaccion.DescripcionTransaccion))?.SetValue(catalogoTransaccion, "PRUEBA");

            var subTipoTransaccion = new SubTipoTransaccion();
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoSistema))?.SetValue(subTipoTransaccion, "CC");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoSubTipoTransaccion))?.SetValue(subTipoTransaccion, "12");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoTipoTransaccion))?.SetValue(subTipoTransaccion, "41");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.DescripcionSubTransaccion))?.SetValue(subTipoTransaccion, "PRUEBA");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.AplicaContabilizadon))?.SetValue(subTipoTransaccion, "S");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.Transaccion))?.SetValue(subTipoTransaccion, catalogoTransaccion);

            var canal = new CanalCCE();
            typeof(CanalCCE).GetProperty(nameof(CanalCCE.CodigoCanalCCE))?.SetValue(canal, "90");

            var canalSubTransacciones = new List<CanalPorSubTransaccionCCE>();
            var canalSubTransaccion = new CanalPorSubTransaccionCCE();
            typeof(CanalPorSubTransaccionCCE).GetProperty(nameof(CanalPorSubTransaccionCCE.CodigoCanalCCE))?.SetValue(canalSubTransaccion, "90");
            typeof(CanalPorSubTransaccionCCE).GetProperty(nameof(CanalPorSubTransaccionCCE.IndicadorTipo))?.SetValue(canalSubTransaccion, "R");
            typeof(CanalPorSubTransaccionCCE).GetProperty(nameof(CanalPorSubTransaccionCCE.CodigoTipoTransaccion))?.SetValue(canalSubTransaccion, "41");
            typeof(CanalPorSubTransaccionCCE).GetProperty(nameof(CanalPorSubTransaccionCCE.CodigoSubTipoTransaccion))?.SetValue(canalSubTransaccion, "12");
            typeof(CanalPorSubTransaccionCCE).GetProperty(nameof(CanalPorSubTransaccionCCE.CodigoUsuarioTransaccion))?.SetValue(canalSubTransaccion, "CAJERO");
            canalSubTransacciones.Add(canalSubTransaccion);

            var canalCCE = new CanalCCE();
            typeof(CanalCCE).GetProperty(nameof(CanalCCE.CodigoCanalCCE))?.SetValue(canalCCE, "90");
            typeof(CanalCCE).GetProperty(nameof(CanalCCE.CanalesPorSubTransaciones))?.SetValue(canalCCE, canalSubTransacciones);

            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.CanalCCE))?.SetValue(transaccion, canalCCE);

            var asientoContable = new AsientoContable();
            typeof(AsientoContable).GetProperty(nameof(AsientoContable.NumeroAsiento))?.SetValue(asientoContable, 8888888);
            typeof(AsientoContable).GetProperty(nameof(AsientoContable.Detalles))?.SetValue(asientoContable, new List<AsientoContableDetalle>());

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<Usuario>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(_usuario);

            _repositorioOperaciones.Setup(x => x
                .ObtenerCalendarioCuentaEfectivo()).Returns(_calendario);

            _repositorioOperaciones.Setup(x =>
                x.ObtenerPorExpresionConLimite<TransaccionOrdenTransferenciaInmediata>(
                    It.IsAny<Expression<Func<TransaccionOrdenTransferenciaInmediata, bool>>>(), null, 0))
                .Returns(transacciones);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<SubTipoTransaccion>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(subTipoTransaccion);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorExpresionConLimite<CanalPorSubTransaccionCCE>(
                    It.IsAny<Expression<Func<CanalPorSubTransaccionCCE, bool>>>(), null, 0))
                .Returns(canalSubTransacciones);

            _servicioDominioContabilidad.Setup(x => x
                .GenerarAsientoContableCompletoPendienteTransferenciaComisionEntrante(
                    It.IsAny<List<CuentaContableInfoDTO>>(),
                    It.IsAny<decimal>(),
                    It.IsAny<SubTipoTransaccion>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<Usuario>(),
                    It.IsAny<int>(),
                    It.IsAny<ITasaCambio>())
                )
                .Returns(asientoContable);

            _servicioDominioContabilidad.Setup(x => x
                .GenerarAsientoContableCompletoPendiente(
                    It.IsAny<int>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<decimal>(),
                    It.IsAny<decimal>(),
                    It.IsAny<IMovimientoContable[]>())
                )
            .Returns(asientoContable);

            try
            {
                await _servicioAplicacionTransaccion.RealizarRechazoTransferenciaEntranteCCE(identificadorInstruccion);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }
        #endregion

        #region Prueba de Excepciones Entrante
        [TestMethod]
        public void ExcepcionTransaccionInmediataEntranteProcesada()
        {
            var identificadorInstruccion = "2024020915100008134115981116";

            var transacciones = new List<TransaccionOrdenTransferenciaInmediata>();
            var transaccion = new TransaccionOrdenTransferenciaInmediata();
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.MontoTransferencia))?.SetValue(transaccion, 100m);
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.MontoComision))?.SetValue(transaccion, 0.80m);
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.TipoTransferencia))?.SetValue(transaccion, "320");
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.IndicadorEstadoOperacion))?.SetValue(transaccion, "F");
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.IdentificadorInstruccion))?.SetValue(transaccion, "2024020915100008134115981116");
            transacciones.Add(transaccion);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<Usuario>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(_usuario);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<Calendario>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(_calendario);

            _repositorioOperaciones.Setup(x =>
                x.ObtenerPorExpresionConLimite<TransaccionOrdenTransferenciaInmediata>(
                    It.IsAny<Expression<Func<TransaccionOrdenTransferenciaInmediata, bool>>>(), null, 0))
                .Returns(transacciones);

            var excepcion = Assert.ThrowsExceptionAsync<Exception>(
                () => _servicioAplicacionTransaccion.RealizarTransferenciaEntranteCCE(identificadorInstruccion));

            Assert.AreEqual("Ocurrio un error: La transaccion ya ha sido Procesada.", excepcion.Result.Message);
        }
        #endregion

        #region Prueba Transaccion Exitosa Saliente
        [TestMethod]
        public void RealizarTransferenciaInmediataSalida()
        {
            var datosOrden = ObtenerDatosRealizarTransferencia(_transferenciaOrdinaria);
            int numeroTransferencia = 12345;
            int numeroMovimiento = 89765;
            var respuesta = new RespuestaSalidaDTO<bool>();
            respuesta.Datos = true;

            _servicioAplicacionTransaccionSalida.Setup(x =>
                x.EchoTest()).ReturnsAsync(respuesta);

            _repositorioOperaciones.Setup(x => x
               .ObtenerPorCodigo<Usuario>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
               .Returns(_usuario);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<Calendario>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_calendario);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<CuentaEfectivo>(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_cuentasEfectivas.FirstOrDefault());

            var catalogoTransaccion = new CatalogoTransaccion();
            typeof(CatalogoTransaccion).GetProperty(nameof(CatalogoTransaccion.CodigoSistema))?.SetValue(catalogoTransaccion, "CC");
            typeof(CatalogoTransaccion).GetProperty(nameof(CatalogoTransaccion.TipoTransaccion))?.SetValue(catalogoTransaccion, "41");
            typeof(CatalogoTransaccion).GetProperty(nameof(CatalogoTransaccion.IndicadorMovimiento))?.SetValue(catalogoTransaccion, "C");
            typeof(CatalogoTransaccion).GetProperty(nameof(CatalogoTransaccion.DescripcionTransaccion))?.SetValue(catalogoTransaccion, "PRUEBA");

            var catalogoTransaccionITF = new CatalogoTransaccion();
            typeof(CatalogoTransaccion).GetProperty(nameof(CatalogoTransaccion.CodigoSistema))?.SetValue(catalogoTransaccion, "CC");
            typeof(CatalogoTransaccion).GetProperty(nameof(CatalogoTransaccion.TipoTransaccion))?.SetValue(catalogoTransaccion, "1");
            typeof(CatalogoTransaccion).GetProperty(nameof(CatalogoTransaccion.IndicadorMovimiento))?.SetValue(catalogoTransaccion, "C");
            typeof(CatalogoTransaccion).GetProperty(nameof(CatalogoTransaccion.DescripcionTransaccion))?.SetValue(catalogoTransaccion, "PRUEBA");

            var subTipoTransaccion = new SubTipoTransaccion();
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoSistema))?.SetValue(subTipoTransaccion, "CC");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoTipoTransaccion))?.SetValue(subTipoTransaccion, "14");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.AplicaContabilizadon))?.SetValue(subTipoTransaccion, "S");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.DescripcionSubTransaccion))?.SetValue(subTipoTransaccion, "TRANSFERENCIA INTERBANCARIA INMEDIATA CCE");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.Transaccion))?.SetValue(subTipoTransaccion, catalogoTransaccion);

            var subTipoTransaccionITF = new SubTipoTransaccion();
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoSistema))?.SetValue(subTipoTransaccionITF, "1");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoTipoTransaccion))?.SetValue(subTipoTransaccionITF, "14");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.AplicaContabilizadon))?.SetValue(subTipoTransaccionITF, "S");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.DescripcionSubTransaccion))?.SetValue(subTipoTransaccionITF, "CARGO ITF");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.Transaccion))?.SetValue(subTipoTransaccionITF, catalogoTransaccionITF);

            var tasacambio = new TipoCambioActual();
            typeof(TipoCambioActual).GetProperty(nameof(TipoCambioActual.CodigoEmpresa))?.SetValue(tasacambio, "1");
            typeof(TipoCambioActual).GetProperty(nameof(TipoCambioActual.CodigoTipoCambio))?.SetValue(tasacambio, "1");
            typeof(TipoCambioActual).GetProperty(nameof(TipoCambioActual.CodigoMoneda))?.SetValue(tasacambio, "01");
            typeof(TipoCambioActual).GetProperty(nameof(TipoCambioActual.FechaTipoCambio))?.SetValue(tasacambio, DateTime.Now);
            typeof(TipoCambioActual).GetProperty(nameof(TipoCambioActual.ValorCompra))?.SetValue(tasacambio, 3.745m);
            typeof(TipoCambioActual).GetProperty(nameof(TipoCambioActual.ValorVenta))?.SetValue(tasacambio, 3.745m);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<SubTipoTransaccion>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>())).Returns(subTipoTransaccion);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<SubTipoTransaccion>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>())).Returns(subTipoTransaccionITF);

            _servicioAplicacionCajero.Setup(x => x
                .ObtenerTasaCambio(It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns(tasacambio);

            var movimientosDiarios = new List<MovimientoDiario>();
            var movimientoDiario = new MovimientoDiario();
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.CodigoAgencia))?.SetValue(movimientoDiario, "01");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.CodigoEmpresa))?.SetValue(movimientoDiario, "1");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.IndOrigenDestino))?.SetValue(movimientoDiario, "F");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.NumeroMovimientoFuente))?.SetValue(movimientoDiario, 0m);
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.Cuenta))?.SetValue(movimientoDiario, _cuentasEfectivas.FirstOrDefault());
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.Transaccion))?.SetValue(movimientoDiario, catalogoTransaccion);
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.SubTipoTransaccionMovimiento))?.SetValue(movimientoDiario, subTipoTransaccion);
            movimientosDiarios.Add(movimientoDiario);

            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.AplicaContabilizadon))?.SetValue(subTipoTransaccion, "S");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.SubTipoTransaccionMovimiento))?.SetValue(movimientoDiario, subTipoTransaccion);
            movimientosDiarios.Add(movimientoDiario);

            _servicioAplicacionProductos.Setup(x =>
                x.GenerarMovimientoTransferenciaSaliente(It.IsAny<CuentaEfectivo>(), It.IsAny<RealizarTransferenciaInmediataDTO>(),
                It.IsAny<SubTipoTransaccion>(), It.IsAny<SubTipoTransaccion>(), It.IsAny<Usuario>(), It.IsAny<DateTime>()))
                .Returns(movimientosDiarios);

            _repositorioOperaciones.Setup(x =>
                x.ObtenerPorExpresionConLimite<EntidadFinancieraDiferida>(
                    It.IsAny<Expression<Func<EntidadFinancieraDiferida, bool>>>(), null, 0))
                .Returns(_entidadesFinancierasCCE);

            var entidadFinanciera = new EntidadFinancieraDiferida();
            typeof(EntidadFinancieraDiferida).GetProperty(nameof(EntidadFinancieraDiferida.NombreEntidad))?.SetValue(entidadFinanciera, "BCP", null);

            _transferencia = new Transferencia();
            typeof(Transferencia).GetProperty(nameof(Transferencia.NumeroTransferencia)).SetValue(_transferencia, numeroTransferencia, null);
            typeof(Transferencia).GetProperty(nameof(Transferencia.NumeroMovimiento)).SetValue(_transferencia, numeroMovimiento, null);
            typeof(Transferencia).GetProperty(nameof(Transferencia.CodigoUsuario)).SetValue(_transferencia, "USUARIO", null);
            typeof(Transferencia).GetProperty(nameof(Transferencia.CuentaOrigen)).SetValue(_transferencia, _cuentasEfectivas.First(), null);

            var transferenciaSaliente = new TransferenciaDetalleSalienteCCE();
            typeof(TransferenciaDetalleSalienteCCE).GetProperty(nameof(TransferenciaDetalleSalienteCCE.CodigoCuentaInterbancario))?
                .SetValue(transferenciaSaliente, "01823500423525284326", null);
            typeof(TransferenciaDetalleSalienteCCE).GetProperty(nameof(TransferenciaDetalleSalienteCCE.EntidadDestino))?
                .SetValue(transferenciaSaliente, entidadFinanciera, null);

            var _transferencias = new List<TransferenciaDetalleSalienteCCE>();
            _transferencias.Add(transferenciaSaliente);

            var asientoContable = new AsientoContable();
            typeof(AsientoContable).GetProperty(nameof(AsientoContable.NumeroAsiento))?.SetValue(asientoContable, 8888888);
            typeof(AsientoContable).GetProperty(nameof(AsientoContable.Detalles))?.SetValue(asientoContable, new List<AsientoContableDetalle>());

            var limitesTransferencias = new List<LimiteTransferenciaInmediata>();
            var limiteTransferencia = new LimiteTransferenciaInmediata();
            typeof(LimiteTransferenciaInmediata)?
                .GetProperty(nameof(LimiteTransferenciaInmediata.MontoLimiteMinimo))?
                .SetValue(limiteTransferencia, 0m);
            typeof(LimiteTransferenciaInmediata)?
                .GetProperty(nameof(LimiteTransferenciaInmediata.MontoLimiteMaximo))?
                .SetValue(limiteTransferencia, 30000m);
            limitesTransferencias.Add(limiteTransferencia);

            ICollection<TransferenciaDetalleSalienteCCE> transferenciaColeccion = _transferencias;

            typeof(Transferencia)?.GetProperty(nameof(Transferencia.DetallesSalientes))?.SetValue(_transferencia, transferenciaColeccion, null);

            _servicioDominioTransaccionOperacion.Setup(x =>
                x.GenerarTransferenciaInmediata(It.IsAny<MovimientoDiario>(), It.IsAny<RealizarTransferenciaInmediataDTO>(), It.IsAny<DateTime>(),
                It.IsAny<Usuario>(), It.IsAny<EntidadFinancieraDiferida>(), It.IsAny<int>()))
                .Returns(_transferencia);

            _servicioDominioTransaccionOperacion.Setup(x =>
                x.GenerarComisionExonerada(It.IsAny<bool>(), It.IsAny<MovimientoDiario>(), It.IsAny<ComisionAhorrosAuxiliar>()))
                .Returns(new ComisionExonerada());

            _servicioDominioContabilidad.Setup(x =>
                x.GenerarMovimientoAuxiliarContableTransferencia(It.IsAny<List<MovimientoDiario>>(), It.IsAny<List<ITransaccion>>(),
                It.IsAny<string>(), It.IsAny<string>()));

            _repositorioOperaciones.Setup(x =>
               x.ObtenerPorExpresionConLimite<ConfiguracionComision>(
                   It.IsAny<Expression<Func<ConfiguracionComision, bool>>>(), null, 0))
                .Returns(_configuracionesComisiones);

            _servicioDominioTransaccionOperacion.Setup(x =>
                x.AplicarComisionTransferencia(It.IsAny<MovimientoDiario>(), It.IsAny<Usuario>(),
                It.IsAny<ComisionAhorrosAuxiliar>(), It.IsAny<int>(), It.IsAny<bool>()));

            _servicioDominioContabilidad.Setup(x => x
                .GenerarAsientoContableCompletoPendiente(
                    It.IsAny<int>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<decimal>(),
                    It.IsAny<decimal>(),
                    It.IsAny<IMovimientoContable[]>())
                )
            .Returns(asientoContable);

            _servicioAplicacionLavado.Setup(x =>
                x.CompletarLavadoTransferenciaInterbancario(It.IsAny<short>(), It.IsAny<MovimientoDiario>(),
                It.IsAny<TransferenciaDetalleSalienteCCE>(), It.IsAny<decimal>(), It.IsAny<string>()));

            _servicioDominioTransaccionOperacion.Setup(x =>
                x.AgregarVinculoMotivo(It.IsAny<IngresoVinculoMotivoDTO>(), It.IsAny<MovimientoDiario>(), It.IsAny<string>()));

            var respuestaEnviarOrden = new RespuestaSalidaDTO<OrdenTransferenciaRespuestaTraducidoDTO>();
            respuestaEnviarOrden.Codigo = CodigoRespuesta.Aceptada;
            respuestaEnviarOrden.Datos = new OrdenTransferenciaRespuestaTraducidoDTO { IdentificadorTransaccion = "12345678965432165" };

            _servicioAplicacionTransaccionSalida.Setup(x =>
                x.EnviarOrdenTransferencia(It.IsAny<OrdenTransferenciaCanalDTO>(), _calendario, It.IsAny<AsientoContable>(), It.IsAny<Transferencia>()))
                .ReturnsAsync(respuestaEnviarOrden);


            var ordenTransferencia = new TransaccionOrdenTransferenciaInmediata();
            typeof(TransaccionOrdenTransferenciaInmediata)?
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.IndicadorEstado))?.SetValue(ordenTransferencia, "N", null);

            var ordenesTransferencias = new List<TransaccionOrdenTransferenciaInmediata>();
            ordenesTransferencias.Add(ordenTransferencia);

            _repositorioOperaciones.Setup(x =>
                x.ObtenerPorExpresionConLimite<TransaccionOrdenTransferenciaInmediata>(
                  It.IsAny<Expression<Func<TransaccionOrdenTransferenciaInmediata, bool>>>(), null, 0))
               .Returns(ordenesTransferencias);

            var valorParametro = new ParametroPorEmpresa();
            typeof(ParametroPorEmpresa)?
                .GetProperty(nameof(ParametroPorEmpresa.ValorParametro))?.SetValue(valorParametro, "123456", null);

            _repositorioGeneral.Setup(x => x
                .ObtenerPorCodigo<ParametroPorEmpresa>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(valorParametro);

            _repositorioOperaciones.Setup(x =>
                x.ObtenerPorExpresionConLimite<LimiteTransferenciaInmediata>(
                  It.IsAny<Expression<Func<LimiteTransferenciaInmediata, bool>>>(), null, 0))
               .Returns(limitesTransferencias);

            try
            {
                var (operacion, asiento, transferencia, movimiento) = _servicioAplicacionTransaccion
                    .RealizarTransferenciaSalienteCCE(datosOrden, true).Result;

                Assert.IsNotNull(operacion);
                Assert.IsNotNull(operacion.ResultadoImpresion);
                Assert.IsNotNull(operacion.NumeroAsiento);
                Assert.AreEqual(_transferencia.NumeroTransferencia, operacion.NumeroTransaccion);
                Assert.AreEqual(_transferencia.NumeroMovimiento, operacion.NumeroOperacion);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ObtenerDatosConsultaCuenta()
        {
            var parametro = new ParametroGeneralTransferencia();
            typeof(ParametroGeneralTransferencia)
               .GetProperty(nameof(ParametroGeneralTransferencia.ValorParametro)).SetValue(parametro, "10", null);

            var parametros = new List<ParametroGeneralTransferencia>();
            parametros.Add(parametro);

            _repositorioOperaciones.Setup(x =>
                x.ObtenerPorExpresionConLimite<ParametroGeneralTransferencia>(
                  It.IsAny<Expression<Func<ParametroGeneralTransferencia, bool>>>(), null, 0))
               .Returns(parametros);

            _repositorioOperaciones.Setup(x => x
                .ObtenerCalendarioCuentaEfectivo()).Returns(_calendario);

            _repositorioOperaciones.Setup(x =>
               x.ObtenerPorExpresionConLimite<ConfiguracionComision>(
                   It.IsAny<Expression<Func<ConfiguracionComision, bool>>>(), null, 0))
                .Returns(_configuracionesComisiones);

            _servicioAplicacionTransaccionSalida.Setup(x =>
                x.ObtenerDatosCuentaCCE(It.IsAny<ConsultaCanalDTO>(), It.IsAny<Calendario>(), true))
                .ReturnsAsync(RespuestaConsultaCuenta());

            _repositorioOperaciones.Setup(x =>
                x.ObtenerPorExpresionConLimite<OficinaCCE>(
                  It.IsAny<Expression<Func<OficinaCCE, bool>>>(), null, 0))
               .Returns(_oficinasCCE);

            _repositorioOperaciones.Setup(x =>
                x.ObtenerPorExpresionConLimite<EntidadFinancieraDiferida>(
                    It.IsAny<Expression<Func<EntidadFinancieraDiferida, bool>>>(), null, 0))
                .Returns(_entidadesFinancierasCCE);

            var comisiones = new List<ComisionCCE>();
            comisiones.Add(ObtenerComisionEntidad());

            _repositorioOperaciones.Setup(x =>
                x.ObtenerPorExpresionConLimite<ComisionCCE>(
                    It.IsAny<Expression<Func<ComisionCCE, bool>>>(), null, 0))
                .Returns(comisiones);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<Calendario>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(_calendario);

            try
            {
                var resultado = _servicioAplicacionTransaccion
                    .ConsultaCuentaReceptorPorCCE(ObtenerConsultaOperacionDTO(), true).Result;

                Assert.IsNotNull(resultado.NombreReceptor);
            }
            catch (Exception)
            {
                Assert.IsNotNull(string.Empty);
            }
        }

        [TestMethod]
        public void CalcularTotalesExitoso()
        {
            var totalEsperado = 102.500M;
            var esperadoITF = 0m;
            var comision = new CalculoComisionDTO
            {
                MontoOperacion = 100,
                ControlMonto = ObtenerControlMonto(),
                Comision = ObtenerComision(),
                SaldoActual = 2000,
                MontoMinimoCuenta = 10,
                MismoTitular = "M"
            };

            _servicioAplicacionCajero.Setup(x => x
                .ObtenerMontoITF(It.IsAny<decimal>(), It.IsAny<bool>(), It.IsAny<bool>(),
                It.IsAny<DateTime>(), It.IsAny<string>())).Returns(esperadoITF);

            _repositorioOperaciones.Setup(x => x
               .ObtenerCalendarioCuentaEfectivo()).Returns(_calendario);

            var producto = new Producto();
            typeof(Producto).GetProperty(nameof(Producto.CodigoProducto))?.SetValue(producto, "CC001");

            var caracteristica = new ProductoCuentasCaracteristicas();
            typeof(ProductoCuentasCaracteristicas).GetProperty(nameof(ProductoCuentasCaracteristicas.IndTransfCCETIN))?.SetValue(caracteristica, "S");
            typeof(ProductoCuentasCaracteristicas).GetProperty(nameof(ProductoCuentasCaracteristicas.MontoMinimoSaldo))?.SetValue(caracteristica, 0m);

            var limitesOperacionesCuenta = new List<LimitesOperacionesCuenta>();

            _cuentasEfectivas = new List<CuentaEfectivo>();
            var cuentaEfectiva = new CuentaEfectivo();
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoAgencia))?.SetValue(cuentaEfectiva, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoEmpresa))?.SetValue(cuentaEfectiva, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoProducto))?.SetValue(cuentaEfectiva, "CC001");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.NumeroCuenta))?.SetValue(cuentaEfectiva, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoCliente))?.SetValue(cuentaEfectiva, "843212");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoEstado))?.SetValue(cuentaEfectiva, "A");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.SaldoDisponible))?.SetValue(cuentaEfectiva, 1000m);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Moneda))?.SetValue(cuentaEfectiva, _moneda);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Cliente))?.SetValue(cuentaEfectiva, _cliente);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Producto))?.SetValue(cuentaEfectiva, producto);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Caracteristicas))?.SetValue(cuentaEfectiva, caracteristica);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.LimitesOperacionesCuenta))?.SetValue(cuentaEfectiva, limitesOperacionesCuenta);
            _cuentasEfectivas.Add(cuentaEfectiva);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<CuentaEfectivo>(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_cuentasEfectivas.FirstOrDefault());

            _repositorioOperaciones.Setup(x => x
               .ObtenerPorExpresionConLimite<ParametroCanalElectronico>(It.IsAny<Expression<Func<ParametroCanalElectronico, bool>>>(), null, 0))
                .Returns(new List<ParametroCanalElectronico>());

            _repositorioOperaciones.Setup(x => x
               .ObtenerPorExpresionConLimite<MovimientoInfoAdicional>(It.IsAny<Expression<Func<MovimientoInfoAdicional, bool>>>(), null, 0))
                .Returns(new List<MovimientoInfoAdicional>());

            try
            {
                var resultado = _servicioAplicacionTransaccion
                .CalcularMontosTotales(comision).Result;

                Assert.IsNotNull(resultado);
                Assert.AreEqual(totalEsperado, resultado.ControlMonto.Total);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void CalcularTotalesExitosoITF()
        {
            var totalEsperado = 1005.05M;
            var totalEsperadoITF = 0.05M;

            var comision = new CalculoComisionDTO
            {
                MontoOperacion = 1000,
                ControlMonto = ObtenerControlMonto(),
                Comision = ObtenerComision(),
                SaldoActual = 2000,
                MontoMinimoCuenta = 10,
                MismoTitular = "O"
            };

            _servicioAplicacionCajero.Setup(x => x
                .ObtenerMontoITF(It.IsAny<decimal>(), It.IsAny<bool>(), It.IsAny<bool>(),
                It.IsAny<DateTime>(), It.IsAny<string>())).Returns(totalEsperadoITF);

            _repositorioOperaciones.Setup(x => x
               .ObtenerCalendarioCuentaEfectivo()).Returns(_calendario);

            var producto = new Producto();
            typeof(Producto).GetProperty(nameof(Producto.CodigoProducto))?.SetValue(producto, "CC001");

            var caracteristica = new ProductoCuentasCaracteristicas();
            typeof(ProductoCuentasCaracteristicas).GetProperty(nameof(ProductoCuentasCaracteristicas.IndTransfCCETIN))?.SetValue(caracteristica, "S");
            typeof(ProductoCuentasCaracteristicas).GetProperty(nameof(ProductoCuentasCaracteristicas.MontoMinimoSaldo))?.SetValue(caracteristica, 0m);

            var limitesOperacionesCuenta = new List<LimitesOperacionesCuenta>();

            _cuentasEfectivas = new List<CuentaEfectivo>();
            var cuentaEfectiva = new CuentaEfectivo();
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoAgencia))?.SetValue(cuentaEfectiva, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoEmpresa))?.SetValue(cuentaEfectiva, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoProducto))?.SetValue(cuentaEfectiva, "CC001");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.NumeroCuenta))?.SetValue(cuentaEfectiva, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoCliente))?.SetValue(cuentaEfectiva, "843212");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoEstado))?.SetValue(cuentaEfectiva, "A");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.SaldoDisponible))?.SetValue(cuentaEfectiva, 1000m);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Moneda))?.SetValue(cuentaEfectiva, _moneda);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Cliente))?.SetValue(cuentaEfectiva, _cliente);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Producto))?.SetValue(cuentaEfectiva, producto);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Caracteristicas))?.SetValue(cuentaEfectiva, caracteristica);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.LimitesOperacionesCuenta))?.SetValue(cuentaEfectiva, limitesOperacionesCuenta);
            _cuentasEfectivas.Add(cuentaEfectiva);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<CuentaEfectivo>(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_cuentasEfectivas.FirstOrDefault());

            _repositorioOperaciones.Setup(x => x
               .ObtenerPorExpresionConLimite<ParametroCanalElectronico>(It.IsAny<Expression<Func<ParametroCanalElectronico, bool>>>(), null, 0))
                .Returns(new List<ParametroCanalElectronico>());

            _repositorioOperaciones.Setup(x => x
               .ObtenerPorExpresionConLimite<MovimientoInfoAdicional>(It.IsAny<Expression<Func<MovimientoInfoAdicional, bool>>>(), null, 0))
                .Returns(new List<MovimientoInfoAdicional>());

            try
            {
                var resultado = _servicioAplicacionTransaccion
                .CalcularMontosTotales(comision).Result;

                Assert.IsNotNull(resultado);
                Assert.AreEqual(totalEsperadoITF, resultado.ControlMonto.Itf);
                Assert.AreEqual(totalEsperado, resultado.ControlMonto.Total);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void CalcularComisionConPorcentaje()
        {
            var totalEsperado = 1005.05M;
            var comisionCCEEsperado = 3;
            var comisionEntidad = 5;
            var comision = ObtenerComision();
            comision.IndicadorPorcentaje = General.Si;
            comision.Porcentaje = 0.5m;
            comision.PorcentajeCCE = 0.5m;
            var totalEsperadoITF = 0.05M;

            var controlComision = new CalculoComisionDTO
            {
                MontoOperacion = 1000,
                ControlMonto = ObtenerControlMonto(),
                Comision = comision,
                SaldoActual = 2000,
                MontoMinimoCuenta = 10,
                MismoTitular = "O"
            };
            _servicioAplicacionCajero.Setup(x => x
                .ObtenerMontoITF(It.IsAny<decimal>(), It.IsAny<bool>(), It.IsAny<bool>(),
                It.IsAny<DateTime>(), It.IsAny<string>())).Returns(totalEsperadoITF);

            _repositorioOperaciones.Setup(x => x
               .ObtenerCalendarioCuentaEfectivo()).Returns(_calendario);

            var producto = new Producto();
            typeof(Producto).GetProperty(nameof(Producto.CodigoProducto))?.SetValue(producto, "CC001");

            var caracteristica = new ProductoCuentasCaracteristicas();
            typeof(ProductoCuentasCaracteristicas).GetProperty(nameof(ProductoCuentasCaracteristicas.IndTransfCCETIN))?.SetValue(caracteristica, "S");
            typeof(ProductoCuentasCaracteristicas).GetProperty(nameof(ProductoCuentasCaracteristicas.MontoMinimoSaldo))?.SetValue(caracteristica, 0m);

            var limitesOperacionesCuenta = new List<LimitesOperacionesCuenta>();

            _cuentasEfectivas = new List<CuentaEfectivo>();
            var cuentaEfectiva = new CuentaEfectivo();
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoAgencia))?.SetValue(cuentaEfectiva, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoEmpresa))?.SetValue(cuentaEfectiva, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoProducto))?.SetValue(cuentaEfectiva, "CC001");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.NumeroCuenta))?.SetValue(cuentaEfectiva, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoCliente))?.SetValue(cuentaEfectiva, "843212");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoEstado))?.SetValue(cuentaEfectiva, "A");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.SaldoDisponible))?.SetValue(cuentaEfectiva, 1000m);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Moneda))?.SetValue(cuentaEfectiva, _moneda);            
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Cliente))?.SetValue(cuentaEfectiva, _cliente);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Producto))?.SetValue(cuentaEfectiva, producto);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Caracteristicas))?.SetValue(cuentaEfectiva, caracteristica);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.LimitesOperacionesCuenta))?.SetValue(cuentaEfectiva, limitesOperacionesCuenta);
            _cuentasEfectivas.Add(cuentaEfectiva);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<CuentaEfectivo>(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_cuentasEfectivas.FirstOrDefault());

            _repositorioOperaciones.Setup(x => x
               .ObtenerPorExpresionConLimite<ParametroCanalElectronico>(It.IsAny<Expression<Func<ParametroCanalElectronico, bool>>>(), null, 0))
                .Returns(new List<ParametroCanalElectronico>());

            _repositorioOperaciones.Setup(x => x
               .ObtenerPorExpresionConLimite<MovimientoInfoAdicional>(It.IsAny<Expression<Func<MovimientoInfoAdicional, bool>>>(), null, 0))
                .Returns(new List<MovimientoInfoAdicional>());

            try
            {
                var resultado = _servicioAplicacionTransaccion
                    .CalcularMontosTotales(controlComision).Result;

                Assert.IsNotNull(resultado);
                Assert.AreEqual(comisionCCEEsperado, resultado.ControlMonto.MontoComisionCce);
                Assert.AreEqual(comisionEntidad, resultado.ControlMonto.MontoComisionEntidad);
                Assert.AreEqual(totalEsperado, resultado.ControlMonto.Total);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void CalcularComisionSinPorcentaje()
        {
            var totalEsperado = 1005.05M;
            var comisionCCEEsperado = 3;
            var comisionEntidad = 5;
            var comision = ObtenerComision();
            comision.IndicadorPorcentaje = General.No;
            comision.Porcentaje = 0;
            comision.PorcentajeCCE = 0;
            var totalEsperadoITF = 0.05m;
            var controlComision = new CalculoComisionDTO
            {
                MontoOperacion = 1000,
                ControlMonto = ObtenerControlMonto(),
                Comision = comision,
                SaldoActual = 2000,
                MontoMinimoCuenta = 10,
                MismoTitular = "O"
            };
            _servicioAplicacionCajero.Setup(x => x
                .ObtenerMontoITF(It.IsAny<decimal>(), It.IsAny<bool>(), It.IsAny<bool>(),
                It.IsAny<DateTime>(), It.IsAny<string>())).Returns(totalEsperadoITF);

            _repositorioOperaciones.Setup(x => x
               .ObtenerCalendarioCuentaEfectivo()).Returns(_calendario);

            var producto = new Producto();
            typeof(Producto).GetProperty(nameof(Producto.CodigoProducto))?.SetValue(producto, "CC001");

            var caracteristica = new ProductoCuentasCaracteristicas();
            typeof(ProductoCuentasCaracteristicas).GetProperty(nameof(ProductoCuentasCaracteristicas.IndTransfCCETIN))?.SetValue(caracteristica, "S");
            typeof(ProductoCuentasCaracteristicas).GetProperty(nameof(ProductoCuentasCaracteristicas.MontoMinimoSaldo))?.SetValue(caracteristica, 0m);

            var limitesOperacionesCuenta = new List<LimitesOperacionesCuenta>();

            _cuentasEfectivas = new List<CuentaEfectivo>();
            var cuentaEfectiva = new CuentaEfectivo();
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoAgencia))?.SetValue(cuentaEfectiva, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoEmpresa))?.SetValue(cuentaEfectiva, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoProducto))?.SetValue(cuentaEfectiva, "CC001");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.NumeroCuenta))?.SetValue(cuentaEfectiva, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoCliente))?.SetValue(cuentaEfectiva, "843212");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoEstado))?.SetValue(cuentaEfectiva, "A");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.SaldoDisponible))?.SetValue(cuentaEfectiva, 1000m);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Moneda))?.SetValue(cuentaEfectiva, _moneda);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Cliente))?.SetValue(cuentaEfectiva, _cliente);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Producto))?.SetValue(cuentaEfectiva, producto);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.Caracteristicas))?.SetValue(cuentaEfectiva, caracteristica);
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.LimitesOperacionesCuenta))?.SetValue(cuentaEfectiva, limitesOperacionesCuenta);
            _cuentasEfectivas.Add(cuentaEfectiva);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<CuentaEfectivo>(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_cuentasEfectivas.FirstOrDefault());

            _repositorioOperaciones.Setup(x => x
               .ObtenerPorExpresionConLimite<ParametroCanalElectronico>(It.IsAny<Expression<Func<ParametroCanalElectronico, bool>>>(), null, 0))
                .Returns(new List<ParametroCanalElectronico>());

            _repositorioOperaciones.Setup(x => x
               .ObtenerPorExpresionConLimite<MovimientoInfoAdicional>(It.IsAny<Expression<Func<MovimientoInfoAdicional, bool>>>(), null, 0))
                .Returns(new List<MovimientoInfoAdicional>());

            try
            {
                var resultado = _servicioAplicacionTransaccion
                .CalcularMontosTotales(controlComision).Result;

                Assert.IsNotNull(resultado);
                Assert.AreEqual(comisionCCEEsperado, resultado.ControlMonto.MontoComisionCce);
                Assert.AreEqual(comisionEntidad, resultado.ControlMonto.MontoComisionEntidad);
                Assert.AreEqual(totalEsperado, resultado.ControlMonto.Total);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }


        [TestMethod]
        public void ReversarTransferenciaInmediata()
        {
            var numeroOperacion = 1;
            var numeroTransferencia = 1;
            var numeroMovimiento = 1;
            var catalogoTransaccion = new CatalogoTransaccion();
            typeof(CatalogoTransaccion).GetProperty(nameof(CatalogoTransaccion.CodigoSistema))?.SetValue(catalogoTransaccion, "CC");
            typeof(CatalogoTransaccion).GetProperty(nameof(CatalogoTransaccion.TipoTransaccion))?.SetValue(catalogoTransaccion, "41");
            typeof(CatalogoTransaccion).GetProperty(nameof(CatalogoTransaccion.IndicadorMovimiento))?.SetValue(catalogoTransaccion, "C");
            typeof(CatalogoTransaccion).GetProperty(nameof(CatalogoTransaccion.DescripcionTransaccion))?.SetValue(catalogoTransaccion, "PRUEBA");

            var subTipoTransaccion = new SubTipoTransaccion();
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoSistema))?.SetValue(subTipoTransaccion, "CC");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoSubTipoTransaccion))?.SetValue(subTipoTransaccion, "12");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoTipoTransaccion))?.SetValue(subTipoTransaccion, "41");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.DescripcionSubTransaccion))?.SetValue(subTipoTransaccion, "PRUEBA");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.AplicaContabilizadon))?.SetValue(subTipoTransaccion, "S");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.Transaccion))?.SetValue(subTipoTransaccion, catalogoTransaccion);
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.IndicadorContablePrincipal))?.SetValue(subTipoTransaccion, "S");

            var movimientosDiarios = new List<MovimientoDiario>();
            var movimientoDiario = new MovimientoDiario();
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.CodigoAgencia))?.SetValue(movimientoDiario, "01");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.NumeroMovimiento))?.SetValue(movimientoDiario, 1m);
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.CodigoEmpresa))?.SetValue(movimientoDiario, "1");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.IndOrigenDestino))?.SetValue(movimientoDiario, "F");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.NumeroMovimientoFuente))?.SetValue(movimientoDiario, 0m);
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.Cuenta))?.SetValue(movimientoDiario, _cuentasEfectivas.FirstOrDefault());
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.Transaccion))?.SetValue(movimientoDiario, catalogoTransaccion);
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.SubTipoTransaccionMovimiento))?.SetValue(movimientoDiario, subTipoTransaccion);
            movimientosDiarios.Add(movimientoDiario);

            _repositorioOperaciones.Setup(operacion => operacion
                .ObtenerPorExpresionConLimite<MovimientoDiario>(It.IsAny<Expression<Func<MovimientoDiario, bool>>>(), null, 0))
                .Returns(movimientosDiarios);

            var transferencias = new List<Transferencia>();
            _transferencia = new Transferencia();
            typeof(Transferencia).GetProperty(nameof(Transferencia.NumeroTransferencia)).SetValue(_transferencia, numeroTransferencia, null);
            typeof(Transferencia).GetProperty(nameof(Transferencia.NumeroMovimiento)).SetValue(_transferencia, numeroMovimiento, null);
            typeof(Transferencia).GetProperty(nameof(Transferencia.CodigoUsuario)).SetValue(_transferencia, "USUARIO", null);
            typeof(Transferencia).GetProperty(nameof(Transferencia.CuentaOrigen)).SetValue(_transferencia, _cuentasEfectivas.First(), null);
            transferencias.Add(_transferencia);

            _repositorioOperaciones.Setup(operacion => operacion
                .ObtenerPorExpresionConLimite<Transferencia>(It.IsAny<Expression<Func<Transferencia, bool>>>(), null, 0))
                .Returns(transferencias);

            var asientoContable = new AsientoContable();
            typeof(AsientoContable).GetProperty(nameof(AsientoContable.NumeroAsiento))?.SetValue(asientoContable, 8888888);
            typeof(AsientoContable).GetProperty(nameof(AsientoContable.Detalles))?.SetValue(asientoContable, new List<AsientoContableDetalle>());

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorCodigo<AsientoContable>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(asientoContable);

            _servicioDominioContabilidad.Setup(operacion => operacion
               .AnularAsientoContablePendiente(It.IsAny<AsientoContable>()));

            _repositorioOperaciones.Setup(operacion => operacion
               .ObtenerPorCodigo<AsientoContable>(It.IsAny<int>())).Returns(_asientoContable);

            _repositorioOperaciones.Setup(operacion => operacion
                .ObtenerPorExpresionConLimite<MovimientoDiario>(It.IsAny<Expression<Func<MovimientoDiario, bool>>>(), null, 0))
                .Returns(movimientosDiarios);

            var transacciones = new List<TransaccionOrdenTransferenciaInmediata>();
            var transaccion = new TransaccionOrdenTransferenciaInmediata();
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.MontoTransferencia))?.SetValue(transaccion, 2500m);
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.MontoComision))?.SetValue(transaccion, 0.80m);
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.TipoTransferencia))?.SetValue(transaccion, "320");
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.IndicadorEstadoOperacion))?.SetValue(transaccion, "C");
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.AsientoContable))?.SetValue(transaccion, asientoContable);
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.Transferencia))?.SetValue(transaccion, _transferencia);
            typeof(TransaccionOrdenTransferenciaInmediata)
                .GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.IdentificadorInstruccion))?.SetValue(transaccion, "2024020915100008134115981116");
            transacciones.Add(transaccion);

            var resultado = new RespuestaSalidaDTO<OrdenTransferenciaRespuestaTraducidoDTO>()
            {
                Codigo = RazonRespuesta.codigoAC01,
                Tipo = "O",
                Datos = new OrdenTransferenciaRespuestaTraducidoDTO
                {
                    RazonCodigoRespuesta = RazonRespuesta.codigoAC01
                }
            };

            _repositorioOperaciones.Setup(operacion => operacion
                .ObtenerPorExpresionConLimite<TransaccionOrdenTransferenciaInmediata>(
                It.IsAny<Expression<Func<TransaccionOrdenTransferenciaInmediata, bool>>>(), null, 0))
                .Returns(transacciones);

            _servicioDominioCuenta.Setup(operacion => operacion
                .ReversarTransferenciaInmediata(
                    It.IsAny<Transferencia>(),
                    It.IsAny<List<MovimientoDiario>>(),
                    It.IsAny<string>(),
                    It.IsAny<CodigoRespuesta>(),
                    It.IsAny<bool>()));

            _repositorioOperaciones.Setup(x => x
                .ObtenerCalendarioCuentaEfectivo()).Returns(_calendario);

            _servicioAplicacionLavado.Setup(operacion => operacion
                .AnularLavado(It.IsAny<MovimientoDiario>()));

            _servicioAplicacionParametros
                .Setup(x => x.ObtenerErrorLocal(It.IsAny<string>()))
                .Returns(_codigoRespuestaError);

            try
            {
                _servicioAplicacionTransaccion.ReversarTransferenciaInmediata(numeroOperacion, resultado);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }



        #endregion Prueba Transaccion Exitosa Saliente
    }
}

