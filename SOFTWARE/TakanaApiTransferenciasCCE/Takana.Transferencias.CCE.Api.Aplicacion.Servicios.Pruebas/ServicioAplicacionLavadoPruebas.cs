using Moq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CJ;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Servicios;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Pruebas
{
    [TestClass]
    public class ServicioAplicacionLavadoPruebas
    {
        #region declaraciones
        private Mock<IServicioDominioLavado> _servicioDominioLavado;
        private Mock<IRepositorioOperacion> _repositorioOperaciones;
        private Mock<IRepositorioGeneral> _reposiTorioGeneral;
        private Mock<IServicioAplicacionCajero> _servicioAplicacionCajero;
        private IServicioAplicacionLavado servicioAplicacionLavado;
        private OperacionUnicaLavado _lavadoUnico;
        private List<OperacionUnicaLavado> _lavadosUnico;
        private MenorCuantiaEncabezado _lavadoMenorCuantia;
        private List<MenorCuantiaEncabezado> _lavadoMenorCuantias;
        #endregion

        [TestInitialize]
        public void Inicializar()
        {
            _servicioDominioLavado = new Mock<IServicioDominioLavado>();
            _repositorioOperaciones = new Mock<IRepositorioOperacion>();
            _reposiTorioGeneral = new Mock<IRepositorioGeneral>();
            _servicioAplicacionCajero = new Mock<IServicioAplicacionCajero>();

            servicioAplicacionLavado =  new ServicioAplicacionLavado(
                _reposiTorioGeneral.Object,
                _repositorioOperaciones.Object,
                _servicioDominioLavado.Object,
                _servicioAplicacionCajero.Object);

            _lavadoUnico =  new OperacionUnicaLavado();
            typeof(OperacionUnicaLavado).GetProperty(nameof(OperacionUnicaLavado.NumeroLavado))?.SetValue(_lavadoUnico, 111m);
            _lavadosUnico = new List<OperacionUnicaLavado>();
            _lavadosUnico.Add(_lavadoUnico);

            _lavadoMenorCuantia = new MenorCuantiaEncabezado();
            typeof(MenorCuantiaEncabezado).GetProperty(nameof(MenorCuantiaEncabezado.NumeroLavado))?.SetValue(_lavadoMenorCuantia, 111);
            _lavadoMenorCuantias = new List<MenorCuantiaEncabezado>();
            _lavadoMenorCuantias.Add(_lavadoMenorCuantia);
        }
        [TestMethod]
        public void CompletarLavadoMenorCuantia()
        {
            var lavadoUnico = new OperacionUnicaLavado();
            var lavadosUnicos = new List<OperacionUnicaLavado>();
            lavadosUnicos.Add(lavadoUnico);

            _repositorioOperaciones.Setup(operacion => operacion
               .ObtenerPorExpresionConLimite<OperacionUnicaLavado>(It.IsAny<Expression<Func<OperacionUnicaLavado, bool>>>(), null, 0))
               .Returns(_lavadosUnico);

            CompletarLavado();
        }
        public void CompletarLavado()
        {
            short idOperacion= 1;
            int numeroOperacionLavado = 123;
            string subCanal = "0";
            var subTipoTransaccion = new SubTipoTransaccion();
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.IndicadorMovimientoLavando))?.SetValue(subTipoTransaccion, "S"); 
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoFormaPagoLavado))?.SetValue(subTipoTransaccion, "S");

            var cuentaEfectivo =  new CuentaEfectivo();
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoMoneda))?.SetValue(cuentaEfectivo, "1");

            var movimientosDiarios = new List<MovimientoDiario>();
            var movimientoDiario = new MovimientoDiario();
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.CodigoAgencia))?.SetValue(movimientoDiario, "01");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.NumeroMovimiento))?.SetValue(movimientoDiario, 1m);
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.CodigoEmpresa))?.SetValue(movimientoDiario, "1");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.IndOrigenDestino))?.SetValue(movimientoDiario, "F");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.NumeroMovimientoFuente))?.SetValue(movimientoDiario, 0m);
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.SistemaFuente))?.SetValue(movimientoDiario,"x");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.CodigoAgencia))?.SetValue(movimientoDiario, "x");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.CodigoUsuario))?.SetValue(movimientoDiario, "x");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.FechaMovimiento))?.SetValue(movimientoDiario, DateTime.Now);
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.NumeroAsiento))?.SetValue(movimientoDiario, 123m);
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.CodigoSistema))?.SetValue(movimientoDiario, "x");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.SubTipoTransaccionMovimiento))?.SetValue(movimientoDiario, subTipoTransaccion);
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.Cuenta))?.SetValue(movimientoDiario, cuentaEfectivo);
            movimientosDiarios.Add(movimientoDiario);

            _repositorioOperaciones.Setup(operacion => operacion
                .ObtenerPorExpresionConLimite<MovimientoDiario>(It.IsAny<Expression<Func<MovimientoDiario, bool>>>(), null, 0))
                .Returns(movimientosDiarios);

            var entidadFinanciera = new EntidadFinancieraDiferida();
            typeof(EntidadFinancieraDiferida).GetProperty(nameof(EntidadFinancieraDiferida.CodigoEntidadSbs)).SetValue(entidadFinanciera, "S", null);

            var _transferencia = new TransferenciaDetalleSalienteCCE();
            typeof(TransferenciaDetalleSalienteCCE).GetProperty(nameof(TransferenciaDetalleSalienteCCE.CodigoCuentaInterbancario))
                .SetValue(_transferencia, "01823500423525284326");
            typeof(TransferenciaDetalleSalienteCCE).GetProperty(nameof(TransferenciaDetalleSalienteCCE.EntidadDestino))
                .SetValue(_transferencia, entidadFinanciera);

            var transferencias = new List<TransferenciaDetalleSalienteCCE>();
            transferencias.Add(_transferencia);

            _repositorioOperaciones.Setup(operacion => operacion
                .ObtenerPorExpresionConLimite<TipoOperacionCanalOrigen>(It.IsAny<Expression<Func<TipoOperacionCanalOrigen, bool>>>(), null, 0));

            _servicioDominioLavado.Setup(operacion => operacion
               .CompletarLavado(It.IsAny<IRegistroLavado>(), It.IsAny<IList<IOperacionLavado>>(), It.IsAny<IList<TipoOperacionCanalOrigen>>()));

            servicioAplicacionLavado.CompletarLavadoTransferenciaInterbancario(
                idOperacion,
                movimientosDiarios.FirstOrDefault(),
                transferencias.FirstOrDefault(),
                numeroOperacionLavado,
                subCanal);
        }

        [TestMethod]
        public void AnularLavadoFallido()
        {
            var lavadoUnico = new OperacionUnicaLavado();
            var lavadosUnicos = new List<OperacionUnicaLavado>();
            lavadosUnicos.Add(lavadoUnico);

            var movimientosDiarios = new List<MovimientoDiario>();
            var movimientoDiario = new MovimientoDiario();
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.CodigoAgencia))?.SetValue(movimientoDiario, "01");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.NumeroMovimiento))?.SetValue(movimientoDiario, 1m);
            movimientosDiarios.Add(movimientoDiario);

            _repositorioOperaciones.Setup(operacion => operacion
                .ObtenerPorExpresionConLimite<OperacionUnicaLavado>(It.IsAny<Expression<Func<OperacionUnicaLavado, bool>>>(), null, 0))
                .Returns(_lavadosUnico);

            _repositorioOperaciones.Setup(operacion => operacion
               .ObtenerPorExpresionConLimite<MenorCuantiaEncabezado>(It.IsAny<Expression<Func<MenorCuantiaEncabezado, bool>>>(), null, 0))
               .Returns(_lavadoMenorCuantias);
            try
            {
                servicioAplicacionLavado.AnularLavado(movimientosDiarios.FirstOrDefault());
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }


    }
}
