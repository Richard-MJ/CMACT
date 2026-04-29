using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Pruebas
{
    [TestClass]
    public class ServicioDominioCuentaPruebas
    {
        private  ServicioDominioCuenta  _servicioDominioCuenta;

        [TestInitialize]
        public void Inicializar()
        {
            _servicioDominioCuenta = new ServicioDominioCuenta();
        }

        [TestMethod]
        public void ReversarTransferenciaInmediataExitoso()
        {
            var subTipoTransaccion = new SubTipoTransaccion();
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.IndicadorMovimientoLavando))?.SetValue(subTipoTransaccion, "S");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.IndicadorContablePrincipal))?.SetValue(subTipoTransaccion, "S");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.EsDetalleContablePrincipal))?.GetValue(subTipoTransaccion);

            var cuentaEfectivo = new CuentaEfectivo();
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.CodigoMoneda))?.SetValue(cuentaEfectivo, "1");
            typeof(CuentaEfectivo).GetProperty(nameof(CuentaEfectivo.SaldoDisponible))?.SetValue(cuentaEfectivo, 1000m);

            var movimientosDiarios = new List<MovimientoDiario>();
            var movimientoDiario = new MovimientoDiario();
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.CodigoAgencia))?.SetValue(movimientoDiario, "01");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.MontoMovimiento))?.SetValue(movimientoDiario, 123m);
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.CodigoTipoTransaccion))?.SetValue(movimientoDiario, "203");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.Cuenta))?.SetValue(movimientoDiario, cuentaEfectivo);
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.SubTipoTransaccionMovimiento))?.SetValue(movimientoDiario, subTipoTransaccion);
            movimientosDiarios.Add(movimientoDiario);

            var transferencias = new List<Transferencia>();
            var transferencia = new Transferencia();
            typeof(Transferencia).GetProperty(nameof(Transferencia.NumeroTransferencia)).SetValue(transferencia, 123, null);
            typeof(Transferencia).GetProperty(nameof(Transferencia.NumeroMovimiento)).SetValue(transferencia, 123, null);
            typeof(Transferencia).GetProperty(nameof(Transferencia.CodigoUsuario)).SetValue(transferencia, "USUARIO", null);
            typeof(Transferencia).GetProperty(nameof(Transferencia.CuentaOrigen)).SetValue(transferencia, cuentaEfectivo, null);
            transferencias.Add(transferencia);

            try
            {
                _servicioDominioCuenta.ReversarTransferenciaInmediata(transferencia, movimientosDiarios, "N", new CodigoRespuesta(), false);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }


    }
}
