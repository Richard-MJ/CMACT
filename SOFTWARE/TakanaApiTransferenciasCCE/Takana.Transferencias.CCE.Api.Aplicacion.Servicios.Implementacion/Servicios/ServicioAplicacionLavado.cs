using Takana.Transferencias.CCE.Api.Common.DTOs.CL;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Dominio.Servicios;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CJ;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.PA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Dominio.Servicios.Logica;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using static Takana.Transferencias.CCE.Api.Dominio.Entidades.CF.Moneda;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Servicios
{
    /// <summary>
    /// Clase capa Aplicacion encargado del lavado de activos
    /// </summary>
    public class ServicioAplicacionLavado : IServicioAplicacionLavado
    {
        private readonly IRepositorioGeneral _repositorioGeneral;
        private readonly IRepositorioOperacion _repositorioOperacion;
        private readonly IServicioDominioLavado _servicioDominioLavado;
        private readonly IServicioAplicacionCajero _servicioAplicacionCajero;
        public ServicioAplicacionLavado(
            IRepositorioGeneral repositorioGeneral,
            IRepositorioOperacion repositorioOperacion,
            IServicioDominioLavado servicioDominioLavado,
            IServicioAplicacionCajero servicioAplicacionCajero)
        {
            _repositorioGeneral = repositorioGeneral;
            _servicioDominioLavado = servicioDominioLavado;
            _repositorioOperacion = repositorioOperacion;
            _servicioAplicacionCajero = servicioAplicacionCajero;
        }

        /// <summary>
        /// Método que genera y realiza el lavado de la transferencia Inmediata Entrante CCE
        /// </summary>
        /// <param name="transaccion">Transaccion de transferencia inmediata</param>
        /// <param name="transferencia">Transferencias, operacion</param>
        /// <param name="clienteBeneficiario">Cliente beneficiado</param>
        /// <param name="asientoContable">Asiento Contable</param>
        /// <param name="entidadOrdenanteCCE">Entidad ordenante</param>
        /// <param name="fechaSistema">Fecha y hora del sistema</param>
        /// <returns>Retorna Operacion Unica o Menor Cuantia de Entrante</returns>
        public IRegistroLavado GenerarLavadoTransferenciaEntrante(
            SubTipoTransaccion subTipoTransaccion,
            Transferencia transferencia,
            ClienteExternoDTO clienteOriginante,
            Cliente clienteBeneficiario,
            AsientoContable asientoContable,
            EntidadFinancieraInmediata entidadOrdenanteCCE,
            DateTime fechaSistema)
        {
            var montoUmbralConvertido = ObtenerMontoUmbralLavado(
                    transferencia.CodigoAgencia,
                    fechaSistema,
                    transferencia.CuentaOrigen.Moneda);

            if (transferencia.MontoTransferencia >= montoUmbralConvertido)
            {
                var numeroOperacionUnica = _repositorioGeneral.ObtenerNumeroSerieNoBloqueante("%", Sistema.Cajas,
                    OperacionUnicaLavado.CodigoSerieLavado, 1);

                var operacionUnica = _servicioDominioLavado
                    .RegistrarLavadoOperacionUnicaTransferenciaEntrante(
                         subTipoTransaccion,
                         transferencia,
                         asientoContable,
                         clienteBeneficiario,
                         clienteOriginante,
                         fechaSistema,
                         entidadOrdenanteCCE,
                         numeroOperacionUnica);

                _repositorioOperacion.Adicionar(operacionUnica as OperacionUnicaLavado);

                return operacionUnica;
            }

            var numeroMenorCuantia = _repositorioGeneral.ObtenerNumeroSerieNoBloqueante("%", Sistema.Cajas,
                MenorCuantiaEncabezado.CodigoSerieLavado, 1);

            var menorCuantia = _servicioDominioLavado
                .RegistrarLavadoMenorCuantiaTransferenciaEntrante(
                    subTipoTransaccion,
                    transferencia,
                    asientoContable,
                    clienteBeneficiario,
                    clienteOriginante,
                    entidadOrdenanteCCE,
                    numeroMenorCuantia);

            _repositorioOperacion.Adicionar(menorCuantia as MenorCuantiaEncabezado);

            return menorCuantia;
        }

        /// <summary>
        /// Completa el lavado
        /// </summary>
        /// <param name="idOperacion">Numero que identifica a la transaccion</param>
        /// <param name="movimientoPrincipalTransferencia">Movimiento principal de la transferencia</param>
        /// <param name="detalleTransferenciaCCE">Detalle de la transferencia</param>
        /// <param name="numeroOperacionLavado">Numero de lavado</param>
        /// <returns></returns>
        public void CompletarLavadoTransferenciaInterbancario(
            short idOperacion,
            MovimientoDiario movimientoPrincipalTransferencia,
            TransferenciaDetalleSalienteCCE detalleTransferenciaCCE,
            decimal numeroOperacionLavado,
            string subCanal)
        {
            try
            {
                IRegistroLavado registroLavado = _repositorioOperacion
                    .ObtenerPorExpresionConLimite<OperacionUnicaLavado>(p => 
                        p.NumeroLavado == numeroOperacionLavado)
                    .FirstOrDefault();

                if (registroLavado == null)
                {
                    registroLavado = _repositorioOperacion
                        .ObtenerPorExpresionConLimite<MenorCuantiaEncabezado>(p => 
                            p.NumeroLavado == numeroOperacionLavado)
                        .FirstOrDefault();
                }

                var listaOperacionesLavado = new List<IOperacionLavado>();
                var tiposOperacionesOrigen = _repositorioOperacion
                    .ObtenerPorExpresionConLimite<TipoOperacionCanalOrigen>(
                    o => o.IdTipoOperacion == idOperacion
                    && o.CodigoCanal == General.CanalCCE
                    && o.CodigoSubCanal == Convert.ToByte(subCanal));

                var movimientoLavadoAuxiliar = new MovimientoLavadoAuxiliarLogico(
                    movimientoPrincipalTransferencia,
                    detalleTransferenciaCCE);

                listaOperacionesLavado.Add(movimientoLavadoAuxiliar);
                listaOperacionesLavado.Add(movimientoPrincipalTransferencia);
                _servicioDominioLavado.CompletarLavado(registroLavado,
                    listaOperacionesLavado.ToList(),
                    tiposOperacionesOrigen);
            }
            catch (Exception excepcion)
            {
                throw new Exception($"Error al completar el lavado: {excepcion.Message}");
            }
        }

        /// <summary>
        /// Anula el lavado de mayor o menor cuantia 
        /// </summary>
        /// <param name="operacionPrincipal">Movimiento principal</param>
        public void AnularLavado(MovimientoDiario operacionPrincipal)
        {
            IRegistroLavado registroLavado = _repositorioOperacion.ObtenerPorExpresionConLimite<OperacionUnicaLavado>(p =>
               p.NumeroMovimtoSistema == operacionPrincipal.NumeroMovimiento && p.IndicadorEstado != General.No
               && p.CodigoAgencia == ((IOperacionLavado)operacionPrincipal).CodigoAgencia).FirstOrDefault();
            if (registroLavado == null)
            {
                registroLavado = _repositorioOperacion.ObtenerPorExpresionConLimite<MenorCuantiaEncabezado>(p =>
                p.IndicadorEstado != General.No && p.CodigoAgencia == ((IOperacionLavado)operacionPrincipal).CodigoAgencia
                && p.Detalles.Where(q => q.NumeroSecuenciaDocumento == operacionPrincipal.NumeroMovimiento).Any()).FirstOrDefault();
            }
            if (registroLavado == null)
                throw new Exception("No se encontro un registro de lavado para anularlo.");
            _servicioDominioLavado.AnularLavadoActivo(registroLavado);
        }
        /// <summary>
        /// Obtiene el monto de umbral del lavado
        /// </summary>
        /// <param name="codigoAgencia">Codigo de la agencia</param>
        /// <param name="fecha">Fecha del sistema</param>
        /// <param name="monedaOperacion">Moneda de la cuenta efectivo</param>
        /// <returns>Retorna el monto umbral</returns>
        public decimal ObtenerMontoUmbralLavado(string codigoAgencia, DateTime fecha, Moneda monedaOperacion)
        {
            var umbralOperacionLavado = _repositorioOperacion
               .ObtenerPorCodigo<UmbralOperacionLavado>(
                   Empresa.CodigoPrincipal, codigoAgencia,
                   UmbralOperacionLavado.codigoTipoOperacionUmbral);

            var moneda = _repositorioOperacion
               .ObtenerPorCodigo<Moneda>(((int)MonedaCodigo.Dolares).ToString());

            var monedaLogicaOrigen = MonedaLogica.Crear(moneda);

            var tasaCambio = _servicioAplicacionCajero
                .ObtenerTasaCambio(TipoCambioActual.Lavado, fecha);

            return Dinero.Crear(monedaLogicaOrigen.Moneda, umbralOperacionLavado.MontoLimite)
                .ConvertirATasaCambio(monedaOperacion, tasaCambio)
                .Monto.Redondear(AsientoContableDetalle.DecimalesPorDefecto);
        }
        /// <summary>
        /// Obtiene el numero de lavado para la operacion
        /// </summary>
        /// <param name="montoOperacion">Monto de la operacion</param>
        /// <param name="montoUmbralLavado">Umbral de la operacion</param>
        /// <returns>Retorna el numero de lavado</returns>
        public int ObtenerNumeroLavado(decimal montoOperacion, decimal montoUmbralLavado)
        {
            if (montoOperacion >= montoUmbralLavado)
                return _repositorioGeneral.ObtenerNumeroSerieNoBloqueante("%", Sistema.Cajas,
                    OperacionUnicaLavado.CodigoSerieLavado, 1);
            else
                return _repositorioGeneral.ObtenerNumeroSerieNoBloqueante("%", Sistema.Cajas,
                    MenorCuantiaEncabezado.CodigoSerieLavado, 1);
        }

        /// <summary>
        /// Registra el numero de lavado segun el tipo de lavado 
        /// </summary>
        /// <param name="codigoMoneda">Codigo de moneda</param>
        /// <param name="fechaSistema">Fecha de la operacion</param>
        /// <param name="montoOperacion">Monto de la operacion</param>
        /// <param name="tipoTransferencia">Tipo de transferencia</param>
        /// <param name="codigoCCI">Codigo de Cuenta interbancaria</param>
        public int RegistrarNumeroLavado(string codigoMoneda, DateTime fechaSistema, decimal montoOperacion,
            string tipoTransferencia,string codigoCCI, string subcanal, string numeroCuentaOriginante,
            ClienteExternoDTO clienteExterno)
        {
            try
            {
                var moneda = _repositorioOperacion.ObtenerPorCodigo<Moneda>(codigoMoneda);
                var umbral = ObtenerMontoUmbralLavado(Agencia.Principal, fechaSistema, moneda);
                var numeroLavado = ObtenerNumeroLavado(montoOperacion, umbral);

                if (montoOperacion <= umbral)
                {
                    var menorCuantia = MenorCuantiaEncabezado.Crear(numeroLavado, Agencia.Principal,
                     General.ModalidadOtrosMediosNoPresenciales, General.SubModalidadAppMovil,
                     fechaSistema, General.Pendiente, General.UsuarioPorDefectoInteroperabilidad);

                    var clienteBeneficiario = _repositorioOperacion.ObtenerPorCodigo<CuentaEfectivo>(
                        Empresa.CodigoPrincipal, numeroCuentaOriginante);

                    var detalleCuantiaOrdenante = MenorCuantiaInterviniente.Crear(numeroLavado,
                        clienteBeneficiario.Cliente, (int)TipoInteviniente.Ordenante);
                    var detalleCuantiaBeneficiario = MenorCuantiaInterviniente.RegistrarDesdeClienteExternoBeneficiario(
                        menorCuantia, TipoInteviniente.Beneficiario, clienteExterno);

                    menorCuantia.AdicionarInterviniente(detalleCuantiaOrdenante);
                    menorCuantia.AdicionarInterviniente(detalleCuantiaBeneficiario);

                    _repositorioOperacion.Adicionar(menorCuantia);
                }
                else
                {
                    string codigoSubTipoTransaccion = ServicioDominioTransaccionOperacion.ObtenerSubTipoTransaccionSalida(
                         tipoTransferencia, subcanal);

                    var operacionUnica = OperacionUnicaLavado.CrearLavado(codigoMoneda, codigoCCI, montoOperacion,
                        fechaSistema, numeroLavado, 
                        ((int)CatalogoTransaccionEnum.CodigoTransferenciaInmediataSaliente).ToString(),
                        codigoSubTipoTransaccion);

                    var interviniente = OperacionUnicaDetalle.GenerarIntervinienteConClienteExterno(
                    numeroLavado, clienteExterno, (int)TipoInteviniente.Ordenante, fechaSistema);

                    operacionUnica.AdicionarInterviniente(interviniente);

                    _repositorioOperacion.Adicionar(operacionUnica);
                }

                _repositorioOperacion.GuardarCambios();

                return numeroLavado;
            }
            catch (Exception)
            {
                throw new Exception("No se pudo generar lavado");
            }
           
        }

    }
}
