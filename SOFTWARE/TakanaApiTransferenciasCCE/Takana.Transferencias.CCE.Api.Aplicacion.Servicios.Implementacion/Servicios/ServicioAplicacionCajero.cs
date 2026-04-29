using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Dominio.Servicios;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.PA;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
using Takana.Transferencias.CCE.Api.Common.DTOs.CG;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Servicios
{
    public class ServicioAplicacionCajero : IServicioAplicacionCajero
    {
        #region Declaraciones

        private readonly IRepositorioGeneral _repositorioGeneral;

        #endregion

        #region Constructor
        public ServicioAplicacionCajero(IRepositorioGeneral repositorioGeneral)
        {
            _repositorioGeneral = repositorioGeneral;
        }

        #endregion

        #region Métodos
        /// <summary>
        /// Obtiene la tasa de cambio
        /// </summary>
        /// <param name="codigoTipoTasaCambio">Codigo del tipo de tasa de cambio</param>
        /// <param name="fechaSistema">fecha del sistema</param>
        /// <returns>Tasa de cambio</returns>
        public ITasaCambio ObtenerTasaCambio(string codigoTipoTasaCambio, DateTime fechaSistema)
        {
            var monedaEmpresa = _repositorioGeneral.ObtenerMonedaLocal();

            var codigoMonedaBase = _repositorioGeneral.ObtenerValorParametroPorEmpresa(Sistema.Configuracion, TipoCambioActual.MonedaBase);

            if (monedaEmpresa.CodigoMoneda == codigoMonedaBase)
                return TipoCambioActual.Crear(TipoCambioActual.ValorNeutro, 
                    TipoCambioActual.ValorNeutro);

            var tipoCambioActual = _repositorioGeneral.Listar<TipoCambioActual>()
                    .Where(t => t.CodigoEmpresa == Empresa.CodigoPrincipal
                        && t.CodigoMoneda == monedaEmpresa.CodigoMoneda
                        && t.CodigoTipoCambio == codigoTipoTasaCambio)
                    .OrderByDescending(t => t.FechaTipoCambio).First();

            if (fechaSistema >= tipoCambioActual.FechaTipoCambio)
                return tipoCambioActual;

            DateTime fechaFija = fechaSistema.Date + new TimeSpan(23, 59, 59);
            var tipoCambioHistorico = _repositorioGeneral.Listar<TipoCambioHistorico>().Where(t =>
                t.CodigoEmpresa == Empresa.CodigoPrincipal && t.CodigoMoneda == monedaEmpresa.CodigoMoneda
                && t.CodigoTipoCambio == codigoTipoTasaCambio && t.FechaTipoCambio <= fechaFija)
                .OrderByDescending(t => t.FechaTipoCambio)
                .First();

            return tipoCambioHistorico;
        }

        /// <summary>
        /// Obtener listado de cuenta puente
        /// </summary>
        /// <param name="codigoCuentaContable"></param>
        /// <param name="codigoCuentaContableComision"></param>
        /// <returns>Retorna listado de cuentas puente</returns>
        public List<ParametroPorEmpresa> ObtenerListaCuentaPuente(
            string codigoCuentaContable, string codigoCuentaContableComision)
        {
            return _repositorioGeneral
                .ObtenerPorExpresionConLimite<ParametroPorEmpresa>(p =>
                    p.CodigoParametro == codigoCuentaContable ||
                    p.CodigoParametro == codigoCuentaContableComision)
                .ToList()
                .ValidarEntidadTexto("Cuenta Puente");
        }
        /// <summary>
        /// Metodo que obtiene el monto ITF con un movimiento
        /// </summary>
        /// <param name="movimiento">Movimiento de la operacion</param>
        /// <returns>Retorna el monto ITF</returns>
        public decimal ObtenerMontoItfMovimiento(MovimientoDiario movimiento)
        {
            if ((movimiento.Cuenta.EsExoneradaITF && !movimiento.Cuenta.EsCuentaSueldo) || movimiento.EsTransaccionITF) 
                return 0M;

            return CalcularMontoItf(movimiento.MontoMovimiento, movimiento.FechaMovimiento);
        }
        /// <summary>
        /// Metodo que obtiene le monto itf 
        /// </summary>
        /// <param name="monto">Monto de la operacion</param>
        /// <param name="cuentaExonerada">Indica si es cuenta exonerada</param>
        /// <param name="esCuentaSueldo">Indica si es cuenta sueldo</param>
        /// <param name="fecha">Fecha de la operacion</param>
        /// <returns>Retorna  el monto ITF</returns>
        public decimal ObtenerMontoITF(
            decimal monto,
            bool cuentaExonerada,
            bool esCuentaSueldo,
            DateTime fecha,
            string tipoTitular)
        {
            if (monto <= 0) return 0m;

            if (tipoTitular == General.MismoTitular 
                || (cuentaExonerada && !esCuentaSueldo)) return 0m;

            return CalcularMontoItf(monto, fecha);
        }

        /// <summary>
        /// Calcular el monto ITF con los indicadores de ITF
        /// </summary>
        /// <param name="monto">Monto de la operacion</param>
        /// <param name="fecha">Fecha de la operacion</param>
        /// <returns>Retorna el monto ITF</returns>
        private decimal CalcularMontoItf(decimal monto, DateTime fecha)
        {
            string aplicaItf = _repositorioGeneral.ObtenerValorParametroPorEmpresa(Sistema.Parametros, "IND_APLICA_ITF");
            if (aplicaItf == General.No) return 0M;

            string codigoItf = _repositorioGeneral.ObtenerValorParametroPorEmpresa(Sistema.Parametros, "COD_INDICE_ITF");
            string aplicaRedondeoItf = _repositorioGeneral.ObtenerValorParametroPorEmpresa(Sistema.Cajas, "IND_REDO_ITF");
            decimal porcentajeItf = _repositorioGeneral.ObtenerValorPorIndice(codigoItf, fecha) / 100M;
            decimal unidadRedondeo = _repositorioGeneral.ObtenerValorPorIndice("FRCVM", fecha);
            decimal montoItf = monto * porcentajeItf;
            montoItf -= (aplicaRedondeoItf == General.Si ? montoItf % unidadRedondeo : 0M);
            return Math.Round(montoItf, 2);
        }

        /// <summary>
        /// Obtiene la tasa de cambio local aplicable a una cuenta contable,
        /// considerando el tipo de cambio configurado (histórico, fijo, por agencia,
        /// ITF u otros) y la moneda local del sistema.
        /// </summary>
        /// <param name="cuentaContable">
        /// Cuenta contable que contiene la configuración del tipo y valor de la tasa de cambio.
        /// </param>
        /// <param name="codigoAgenciaOrigen">
        /// Código de la agencia de origen, utilizado cuando el tipo de cambio depende de la agencia.
        /// </param>
        /// <param name="fechaSistema">
        /// Fecha del sistema para la cual se requiere la tasa de cambio.
        /// </param>
        /// <returns>
        /// Valor decimal de la tasa de cambio local calculada según la configuración de la cuenta contable.
        /// </returns>
        public decimal ObtenerTasaCambioLocal(CuentaContable cuentaContable, string codigoAgenciaOrigen, DateTime fechaSistema)
        {
            try
            {
                var tipoCambioHistorico = TipoCambioActual.TipoCambioHistorico;

                if (cuentaContable.IndicadorTipoCambio == tipoCambioHistorico) return cuentaContable.ValorTasaCambio ?? 0m;

                var codigoMonedaLocal = _repositorioGeneral.ObtenerMonedaLocal();
                var codigoTipoTasaCambio = ObtenerCodigoTipoTasaCambio(cuentaContable.CodigoTipoCambio, codigoAgenciaOrigen);

                return ObtenerTasaCambioPorCuenta(codigoTipoTasaCambio, codigoMonedaLocal.CodigoMoneda, cuentaContable.CodigoClaseTipoCambio, fechaSistema);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la tasa de cambio correspondiente a una cuenta contable,
        /// considerando si el tipo de cambio es histórico o vigente, el tipo de tasa configurado
        /// (fijo, por agencia, ITF u otros) y la moneda asociada a la cuenta o a la moneda local,
        /// según corresponda.
        /// </summary>
        /// <param name="cuentaContable"></param>
        /// <param name="codigoAgenciaOrigen"></param>
        /// <param name="fechaSistema"></param>
        /// <returns>
        /// Valor decimal de la tasa de cambio calculada según la configuración de la cuenta contable.
        /// </returns>
        public decimal ObtenerTasaCambioCuenta(CuentaContable cuentaContable, string codigoAgenciaOrigen, DateTime fechaSistema)
        {
            try
            {
                var tipoCambioHistorico = TipoCambioActual.TipoCambioHistorico;
                var codigoMonedaLocal = _repositorioGeneral.ObtenerMonedaLocal();

                if (cuentaContable.IndicadorTipoCambio == tipoCambioHistorico)
                {
                    if ((cuentaContable.ValorTasaCambio ?? 0m) == 0m)
                        return 0m;

                    var codigoTipoTasaCambio =
                        ObtenerCodigoTipoTasaCambio(cuentaContable.CodigoTipoCambio, codigoAgenciaOrigen);

                    return ObtenerTasaCambioPorCuenta(
                        codigoTipoTasaCambio,
                        codigoMonedaLocal.CodigoMoneda,
                        cuentaContable.CodigoClaseTipoCambio,
                        fechaSistema);
                }

                var codigoTipoTasa = ObtenerCodigoTipoTasaCambio(cuentaContable.CodigoTipoCambio, codigoAgenciaOrigen);

                return ObtenerTasaCambioPorCuenta(codigoTipoTasa, cuentaContable.CodigoMoneda,
                    cuentaContable.CodigoClaseTipoCambio, fechaSistema);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }          
        }

        /// <summary>
        /// Obtiene el código del tipo de tasa de cambio a utilizar según el tipo de cambio indicado.
        /// Si el código de tipo de cambio es nulo, se utiliza el valor por defecto definido en el sistema.
        /// Dependiendo del tipo, el valor se obtiene desde parámetros por empresa o por agencia.
        /// </summary>
        /// <param name="codigoTipoCambio"></param>
        /// <param name="codigoAgenciaOrigen"></param>
        /// <returns>
        /// Código del tipo de tasa de cambio correspondiente según las reglas de negocio.
        /// </returns>
        private string ObtenerCodigoTipoTasaCambio(string? codigoTipoCambio, string codigoAgenciaOrigen)
        {
            codigoTipoCambio ??= TipoCambioActual.CodigoTipoCambioDefault;

            return codigoTipoCambio switch
            {
                TipoCambioActual.TipoCambioFijo =>
                    _repositorioGeneral.ObtenerValorParametroPorEmpresa(
                        Sistema.Contabilidad, TipoCambioActual.CodigoTipoCambioFijo),

                TipoCambioActual.TipoCambio =>
                    _repositorioGeneral.ObtenerPorCodigo<ParametroPorAgencia>(
                        Empresa.CodigoPrincipal,
                        codigoAgenciaOrigen,
                        Sistema.Cajas,
                        TipoCambioActual.CodigoTipoCambioCaja
                    ).ValorParametro,

                TipoCambioActual.TipoCambioITF =>
                    _repositorioGeneral.ObtenerValorParametroPorEmpresa(
                        Sistema.Contabilidad, TipoCambioActual.CodigoTipoCambioITF),

                _ => TipoCambioActual.CodigoTipoCambioDefault
            };
        }

        /// <summary>
        /// Obtiene la tasa de cambio aplicable a una cuenta en función del tipo de tasa,
        /// moneda, clase de tasa (compra o venta) y la fecha del sistema.
        /// Si la moneda corresponde a la moneda base de la empresa, retorna 1.
        /// En caso contrario, utiliza la tasa de cambio vigente o histórica según la fecha.
        /// </summary>
        /// <param name="codigoTipoTasaCambio"></param>
        /// <param name="codigoMoneda"></param>
        /// <param name="codigoClaseTasaCambio"></param>
        /// <param name="fechaSistema"></param>
        /// <returns>
        /// Valor decimal de la tasa de cambio correspondiente según los criterios indicados.
        /// </returns>
        private decimal ObtenerTasaCambioPorCuenta(string codigoTipoTasaCambio, string codigoMoneda, string codigoClaseTasaCambio, DateTime fechaSistema)
        {
            var codigoMonedaBase = _repositorioGeneral.ObtenerValorParametroPorEmpresa(Sistema.Configuracion, General.MonedaBase);

            if (codigoMoneda == codigoMonedaBase)
            {
                return 1;
            }

            var tipoCambioActual =
                _repositorioGeneral.Listar<TipoCambioActual>()
                    .Where(
                        t =>
                            t.CodigoEmpresa == Empresa.CodigoPrincipal && t.CodigoMoneda == codigoMoneda &&
                            t.CodigoTipoCambio == codigoTipoTasaCambio)
                    .OrderByDescending(t => t.FechaTipoCambio).First();

            if (fechaSistema >= tipoCambioActual.FechaTipoCambio)
                return codigoClaseTasaCambio == TipoCambioActual.CLASE_TASA_CAMBIO_VENTA
                    ? tipoCambioActual.ValorVenta
                    : tipoCambioActual.ValorCompra;

            DateTime fechaFija = fechaSistema.Date.AddDays(1).AddTicks(-1);

            var tipoCambioHistorico = _repositorioGeneral.Listar<TipoCambioHistorico>().Where(t =>
                t.CodigoEmpresa == Empresa.CodigoPrincipal && t.CodigoMoneda == codigoMoneda &&
                t.CodigoTipoCambio == codigoTipoTasaCambio && t.FechaTipoCambio <= fechaFija)
                .OrderByDescending(t => t.FechaTipoCambio)
                .First();
            return codigoClaseTasaCambio == TipoCambioActual.CLASE_TASA_CAMBIO_VENTA ? tipoCambioHistorico.ValorVenta : tipoCambioHistorico.ValorCompra;
        }

        /// <summary>
        /// Obtiene la información contable de una cuenta contable,
        /// incluyendo las tasas de cambio local y de la cuenta,
        /// luego de validar su configuración en el sistema.
        /// </summary>
        /// <param name="codigoCuenta"></param>
        /// <param name="codigoAgencia"></param>
        /// <param name="codigoUnidadEjecutora"></param>
        /// <param name="tipoAsiento"></param>
        /// <param name="detalleCuenta"></param>
        /// <param name="fechaSistema"></param>
        /// <returns>
        /// Objeto <see cref="CuentaContableInfoDTO"/> con la información contable y
        /// las tasas de cambio calculadas.
        /// </returns>
        public CuentaContableInfoDTO ObtenerDatosCuentaContable(
            string codigoCuenta,
            string codigoAgencia,
            string codigoUnidadEjecutora,
            string tipoAsiento,
            string detalleCuenta,
            DateTime fechaSistema)
        {
            var cuenta = _repositorioGeneral.ObtenerPorCodigo<CuentaContable>(Empresa.CodigoPrincipal, codigoCuenta);
            cuenta.Validar();

            return new CuentaContableInfoDTO
            {
                NumeroCuenta = cuenta.NumeroCuentaContable,
                TasaCambioLocal = ObtenerTasaCambioLocal(cuenta, codigoAgencia, fechaSistema),
                TasaCambioCuenta = ObtenerTasaCambioCuenta(cuenta, codigoAgencia, fechaSistema),
                CodigoTipoCambio = cuenta.CodigoClaseTipoCambio,
                CodigoUnidadEjecutora = codigoUnidadEjecutora,
                TipoAsiento = tipoAsiento,
                DetalleCuenta = detalleCuenta
            };
        }
        #endregion
    }
}
