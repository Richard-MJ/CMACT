using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.PA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Common.DTOs.CG;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Pruebas
{
    [TestClass]
    public class ServicioDominioContabilidadPruebas
    {
        #region Declaracion Variables
        private Mock<IServicioAplicacionCajero> _servicioAplicacionCajero;
        private ServicioDominioContabilidad _servicioDominioContabilidad;
        #endregion

        #region Inicializaciones
        [TestInitialize]
        public void Inicializar()
        {
            _servicioAplicacionCajero = new Mock<IServicioAplicacionCajero>();
            _servicioDominioContabilidad = new ServicioDominioContabilidad();
        }

        #endregion

        #region Contabilidad Transferencias Entrantes
        [TestMethod]
        public void GenerarDetalleCuentaPuenteTransferenciaEntrante()
        {
            var comision = 0.80m;
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
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.IndicadorContablePrincipal))?.SetValue(subTipoTransaccion, "S");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.Transaccion))?.SetValue(subTipoTransaccion, catalogoTransaccion);            

            var tipoCambio = new TipoCambioActual();
            typeof(TipoCambioActual).GetProperty(nameof(TipoCambioActual.ValorCompra))?.SetValue(tipoCambio, 3.4405M);
            typeof(TipoCambioActual).GetProperty(nameof(TipoCambioActual.ValorVenta))?.SetValue(tipoCambio, 3.4405M);

            var parametrosPorEmpresa = new List<ParametroPorEmpresa>();
            var parametroPorEmpresa = new ParametroPorEmpresa();
            typeof(ParametroPorEmpresa).GetProperty(nameof(ParametroPorEmpresa.CodigoSistema))?.SetValue(parametroPorEmpresa, "CC");
            typeof(ParametroPorEmpresa).GetProperty(nameof(ParametroPorEmpresa.CodigoParametro))?.SetValue(parametroPorEmpresa, "CTA_TIN_SOL_COM");
            typeof(ParametroPorEmpresa).GetProperty(nameof(ParametroPorEmpresa.ValorParametro))?.SetValue(parametroPorEmpresa, "290324");
            typeof(ParametroPorEmpresa).GetProperty(nameof(ParametroPorEmpresa.DescripcionParametro))?.SetValue(parametroPorEmpresa, "TRANSFERENCIA");
            parametrosPorEmpresa.Add(parametroPorEmpresa);

            var cuentaEfectivo = new CuentaEfectivo();
            var movimientosDiarios = new List<MovimientoDiario>();
            var movimientoDiario = new MovimientoDiario();
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.CodigoAgencia))?.SetValue(movimientoDiario, "01");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.CodigoEmpresa))?.SetValue(movimientoDiario, "1");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.IndOrigenDestino))?.SetValue(movimientoDiario, "F");
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.NumeroMovimientoFuente))?.SetValue(movimientoDiario, 0m);
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.Cuenta))?.SetValue(movimientoDiario, cuentaEfectivo);
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.Transaccion))?.SetValue(movimientoDiario, catalogoTransaccion);     
            typeof(MovimientoDiario).GetProperty(nameof(MovimientoDiario.SubTipoTransaccionMovimiento))?.SetValue(movimientoDiario, subTipoTransaccion);
            movimientosDiarios.Add(movimientoDiario);

            var asiento = AsientoContable.Generar(1, "1", "01", "CJ", "1", "1", DateTime.Now
                , "COMPROBANTE GENERAL DE INGRESO", "N", "MRAMOS");

            _servicioAplicacionCajero.Setup(x => x
                .ObtenerTasaCambio(
                    It.IsAny<string>(),
                    It.IsAny<DateTime>()))
                .Returns(tipoCambio);

            _servicioAplicacionCajero.Setup(x => x
                .ObtenerListaCuentaPuente(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(parametrosPorEmpresa);

            ITasaCambio tasaCambio = TipoCambioActual.Crear(1,1);

            _servicioDominioContabilidad
                .GenerarDetalleAsientoDeCuentaPuenteTransferenciaEntrante(
                subTipoTransaccion, asiento,movimientosDiarios.ToList<IMovimientoContable>()
                    .FirstOrDefault(m => m.EsPrincipal), new List<CuentaContableInfoDTO>(), comision, tasaCambio, string.Empty,
                DateTime.Now);

            Assert.IsNotNull(asiento);
        }

        [TestMethod]
        public void GenerarAsientoTransferenciaComisionEntrante()
        {
            var numeroAsiento = 2;
            var comision = 0.00m;
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
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.IndicadorContablePrincipal))?.SetValue(subTipoTransaccion, "S");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.Transaccion))?.SetValue(subTipoTransaccion, catalogoTransaccion);

            var tipoCambio = new TipoCambioActual();
            typeof(TipoCambioActual).GetProperty(nameof(TipoCambioActual.ValorCompra))?.SetValue(tipoCambio, 3.4405M);
            typeof(TipoCambioActual).GetProperty(nameof(TipoCambioActual.ValorVenta))?.SetValue(tipoCambio, 3.4405M);

            var parametrosPorEmpresa = new List<ParametroPorEmpresa>();
            var parametroPorEmpresa = new ParametroPorEmpresa();
            typeof(ParametroPorEmpresa).GetProperty(nameof(ParametroPorEmpresa.CodigoSistema))?.SetValue(parametroPorEmpresa, "CC");
            typeof(ParametroPorEmpresa).GetProperty(nameof(ParametroPorEmpresa.CodigoParametro))?.SetValue(parametroPorEmpresa, "CTA_TIN_SOL_ENT");
            typeof(ParametroPorEmpresa).GetProperty(nameof(ParametroPorEmpresa.ValorParametro))?.SetValue(parametroPorEmpresa, "19180711");
            typeof(ParametroPorEmpresa).GetProperty(nameof(ParametroPorEmpresa.DescripcionParametro))?.SetValue(parametroPorEmpresa, "TRANSFERENCIA");
            parametrosPorEmpresa.Add(parametroPorEmpresa);
            typeof(ParametroPorEmpresa).GetProperty(nameof(ParametroPorEmpresa.CodigoSistema))?.SetValue(parametroPorEmpresa, "CC");
            typeof(ParametroPorEmpresa).GetProperty(nameof(ParametroPorEmpresa.CodigoParametro))?.SetValue(parametroPorEmpresa, "CTA_TIN_SOL_COM");
            typeof(ParametroPorEmpresa).GetProperty(nameof(ParametroPorEmpresa.ValorParametro))?.SetValue(parametroPorEmpresa, "290324");
            typeof(ParametroPorEmpresa).GetProperty(nameof(ParametroPorEmpresa.DescripcionParametro))?.SetValue(parametroPorEmpresa, "TRANSFERENCIA");
            parametrosPorEmpresa.Add(parametroPorEmpresa);

            _servicioAplicacionCajero.Setup(x => x
                .ObtenerTasaCambio(
                    It.IsAny<string>(),
                    It.IsAny<DateTime>()))
                .Returns(tipoCambio);

            _servicioAplicacionCajero.Setup(x => x
                .ObtenerListaCuentaPuente(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(parametrosPorEmpresa);

            ITasaCambio tasaCambio = TipoCambioActual.Crear(1, 1);

            var asiento = _servicioDominioContabilidad
                .GenerarAsientoContableCompletoPendienteTransferenciaComisionEntrante(
                new List<CuentaContableInfoDTO>(), comision, subTipoTransaccion,
                DateTime.Now, new Usuario(),numeroAsiento, tasaCambio);

            Assert.IsNotNull(asiento);
        }
        #endregion

    }
}

