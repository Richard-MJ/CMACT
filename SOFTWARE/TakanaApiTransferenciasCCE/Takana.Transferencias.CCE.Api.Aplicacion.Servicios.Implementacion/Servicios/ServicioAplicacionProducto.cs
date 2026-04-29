using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.TJ;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;
using Takana.Transferencias.CCE.Api.Dominio.Servicios.Logica;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;


namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Servicios
{
    public class ServicioAplicacionProducto : IServicioAplicacionProducto
    {
        #region Declaraciones

        private readonly IRepositorioGeneral _repositorioGeneral;
        private readonly IRepositorioOperacion _repositorioOperacion;
        private readonly IServicioDominioProducto _servicioDominioProducto;
        private readonly IServicioAplicacionCajero _servicioAplicacionCajero;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repositorioGeneral"></param>
        public ServicioAplicacionProducto(
            IRepositorioGeneral repositorioGeneral,
            IRepositorioOperacion repositorioOperacion,
            IServicioDominioProducto servicioDominioProducto,
            IServicioAplicacionCajero servicioAplicacionCajero)
        {
            _repositorioGeneral = repositorioGeneral;
            _repositorioOperacion = repositorioOperacion;
            _servicioDominioProducto = servicioDominioProducto;
            _servicioAplicacionCajero = servicioAplicacionCajero;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Obtiene la entidad cliente a partir de el número de su tarjeta de debito.
        /// </summary>
        /// <param name="numeroTarjeta">Número de tarjeta de debito de cliente.</param>
        /// <returns>Entidad cliente dueña de la tarjeta de debito.</returns>
        public Tarjeta ObtenerClienteAPartirDeTarjeta(string numeroTarjeta)
        {
            decimal numeroTarjetaDecimal;

            try
            {
                numeroTarjetaDecimal = Convert.ToDecimal(numeroTarjeta);
            }
            catch (Exception excepcion)
            {
                throw new Exception($"Número de tarjeta no válida: {numeroTarjeta}", excepcion);
            }

            var tarjetaDeCliente = _repositorioOperacion.ObtenerPorCodigo<Tarjeta>(numeroTarjetaDecimal);

            var fechaSistema = _repositorioOperacion.ObtenerCalendarioCuentaEfectivo().FechaHoraSistema;

            if (tarjetaDeCliente.TarjetaVencida(fechaSistema) || !tarjetaDeCliente.Activa())
                throw new ValidacionException($"Tarjeta no válida {tarjetaDeCliente.NumeroTarjeta}");

            return tarjetaDeCliente;
        }

        /// <summary>
        /// Obtiene Las cuentas del cliente donde este figure como TITULAR de la misma
        /// </summary>
        /// <param name="codigoCliente"></param>
        /// <returns></returns>
        public ICollection<CuentaEfectivo> ObtenerProductosPasivosDeGrupoDebito(string codigoCliente, string codigoGrupoProductos)
        {
            var productosPermitidos = _repositorioOperacion
                .ObtenerPorExpresionConLimite<GrupoProducto>(x =>
                    x.IndicadorGrupo == codigoGrupoProductos &&
                    x.IndicadorEstado == General.Activo)
                .Select(p => p.CodigoProducto)
                .ToHashSet();

            var cuentasAhorro = _repositorioOperacion
                .ObtenerPorExpresionConLimite<FirmaCliente>(x =>
                    x.CodigoCliente == codigoCliente &&
                    x.IndicadorTipoFirmante == "T")
                .Where(x => productosPermitidos.Contains(x.Cuenta.CodigoProducto))
                .Select(x => x.Cuenta)
                .ToList();

            var productosDeCliente = cuentasAhorro
                .Where(x => 
                    x.EstadoCuenta.IndicadorEstadoVigente == General.Si && 
                    x.EsDuenio(codigoCliente) &&
                    x.CodigoEstado == General.Activo)
                .ToList();

            return productosDeCliente;
        }

        /// <summary>
        /// Obtiene las operaciones frecuentes del cliente
        /// </summary>
        public List<OperacionFrecuente> ObtenerOperacionesFrecuentesDeCliente(string codigoCliente)
        {
            var cuentasCliente = ObtenerProductosPasivosDeGrupoDebito(codigoCliente, GrupoProducto.IndicadorGrupoDebito)
                .Select(c => c.NumeroCuenta)
                .ToList();

            var operacionesFrecuentes = _repositorioOperacion
                .ObtenerPorExpresionConLimite<OperacionFrecuente>(x =>
                    x.IndicadorEstado == General.Activo)
                .Where(x => cuentasCliente.Contains(x.NumeroCuenta))
                .OrderByDescending(x => x.NumeroOperacionFrecuente)
                .ToList();

            return operacionesFrecuentes;
        }

        /// <summary>
        /// Metodo que crea el debito por transferencia inmediata
        /// </summary>
        /// <param name="cuenta">Cuenta efectivo de la transaccion</param>
        /// <param name="detalle">datos de la transaccion</param>
        /// <param name="subTransaccion">subtransaccion</param>
        /// <param name="subTransaccionITF">subtransaccion ITF</param>
        /// <param name="usuario">Usuario del sistema</param>
        /// <param name="fechaSistema">fecha del sistema</param>
        /// <returns>Retorna los movimientos debitados</returns>
        public IList<MovimientoDiario> GenerarMovimientoTransferenciaSaliente(
            CuentaEfectivo cuenta,
            RealizarTransferenciaInmediataDTO detalle,
            SubTipoTransaccion subTransaccion,
            SubTipoTransaccion subTransaccionITF,
            Usuario usuario,
            DateTime fechaSistema)
        {
            bool indicadorCuentaSueldo = _repositorioGeneral
                .ObtenerValorParametroPorEmpresa(Sistema.CuentaEfectivo, CuentaEfectivoSueldo.IndicadorCuentaSueldo) == General.Si;

            int cantidadSeries = cuenta.EsCuentaSueldo ? 2 : 1;

            int numeroMovimientoPrincipal = _repositorioGeneral
                .ObtenerNumeroSerieNoBloqueante("%", Sistema.CuentaEfectivo, Movimiento.CodigoSerieMovimientoDiarioEnCC, cantidadSeries);

            var logicaCuenta = LogicaCuentaEfectivo.ObtenerLogica(cuenta);
            var montoADebitar = logicaCuenta.Retirar(detalle.ControlMonto.Monto, indicadorCuentaSueldo);
            var listaMovimientos = logicaCuenta.GenerarMovimientos(subTransaccion, subTransaccion.DescripcionSubTransaccion, montoADebitar,
                usuario, Sistema.CuentaEfectivo, fechaSistema, numeroMovimientoPrincipal);
            var movimientoPrincipal = _servicioDominioProducto.ObtenerMovimientoPrincipal(listaMovimientos);

            if (movimientoPrincipal == null)
                throw new ValidacionException("Al generar los movimientos de debito no se genero una operación principal");

            foreach (MovimientoDiario movimiento in listaMovimientos)
            {
                if (movimiento.TipoMontoMovimiento == TipoMontoCuentaEfectivo.NoRemunerativo)
                {
                    decimal montoITF = _servicioAplicacionCajero.ObtenerMontoItfMovimiento(movimiento);

                    if (montoITF > 0 && !detalle.MismoTitularEnDestino)
                    {
                        logicaCuenta.DebitarPorOperacionITF(montoITF, indicadorCuentaSueldo);

                        int numeroMovimienoITF = _repositorioGeneral.ObtenerNumeroSerieNoBloqueante("%",
                            Sistema.CuentaEfectivo, Movimiento.CodigoSerieMovimientoDiarioEnCC, 1);
                        movimiento.EstablecerAfectadoItf();
                        var movimientoITF = _servicioDominioProducto.GenerarMovimientoITF(
                            numeroMovimienoITF,
                            montoITF,
                            cuenta,
                            usuario,
                            subTransaccionITF,
                            movimientoPrincipal);
                        listaMovimientos.Add(movimientoITF);
                        break;
                    }
                }
            }
            return listaMovimientos;
        }

        /// <summary>
        /// Método para agregar el movimiento ITF si es necesario
        /// </summary>
        /// <param name="movimientosDiariosCuentaEfectivo"></param>
        /// <param name="usuario"></param>
        /// <param name="subTipoTransaccionITF"></param>
        /// <param name="transaccion"></param>
        /// <param name="cuentaEfectivo"></param>
        /// <param name="movimientoEnCuentaEfectivo"></param>
        public void GenerarMovimientoITF(
            List<MovimientoDiario> movimientosDiariosCuentaEfectivo,
            Usuario usuario,
            SubTipoTransaccion subTipoTransaccionITF,
            TransaccionOrdenTransferenciaInmediata transaccion,
            CuentaEfectivo cuentaEfectivo,
            MovimientoDiario movimientoEnCuentaEfectivo)
        {
            var movimientoPrincipal = _servicioDominioProducto.ObtenerMovimientoPrincipal(movimientosDiariosCuentaEfectivo);

            decimal montoITF = _servicioAplicacionCajero.ObtenerMontoItfMovimiento(movimientoEnCuentaEfectivo);

            if (montoITF > 0 && transaccion.CodigoTitular != General.MismoTitular)
            {
                var logicaCuenta = LogicaCuentaEfectivo.ObtenerLogica(cuentaEfectivo);

                logicaCuenta.DebitarPorOperacionITF(montoITF, false);

                int numeroMovimientoITF = _repositorioGeneral.ObtenerNumeroSerieNoBloqueante(
                    "%", Sistema.CuentaEfectivo, Movimiento.CodigoSerieMovimientoDiarioEnCC, 1);
                movimientoPrincipal.EstablecerAfectadoItf();
                var movimientoITF = _servicioDominioProducto
                    .GenerarMovimientoITF(
                    numeroMovimientoITF,
                    montoITF,
                    cuentaEfectivo,
                    usuario,
                    subTipoTransaccionITF,
                    movimientoEnCuentaEfectivo);
                movimientosDiariosCuentaEfectivo.Add(movimientoITF);
            }

            transaccion.EstablecerITF(montoITF);
        }
        #endregion
    }
}
