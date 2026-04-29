using System.Transactions;
using Takana.Transferencias.CCE.Api.Common;
using Microsoft.Extensions.DependencyInjection;
using Takana.Transferencias.CCE.Api.Common.DTOs.CG;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Dominio.Servicios;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.PA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC;
using Takana.Transferencias.CCE.Api.Dominio.Servicios.Logica;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Dominio;
using Takana.Transferencias.CCE.Api.Common.DTOs.Notificaciones;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Dominio.Servicios.Extensiones;
using static Takana.Transferencias.CCE.Api.Dominio.Entidades.CF.Moneda;
using Takana.Transferencias.CCE.Api.Common.Constantes.Interoperabilidad;
using static Takana.Transferencias.CCE.Api.Dominio.Entidades.CF.CanalCCE;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Extensiones;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Servicios
{
    /// <summary>
    /// Clase de capa Aplicacion para el control de transacciones
    /// </summary>
    public class ServicioAplicacionTransaccionOperacion : ServicioBase, IServicioAplicacionTransaccionOperacion
    {
        #region Declaraciones
        private readonly IServiceProvider _servicioProvider;
        private readonly IRepositorioRedis _repositorioRedis;
        private readonly IRepositorioGeneral _repositorioGeneral;
        private readonly IRepositorioOperacion _repositorioOperacion;
        private readonly IServicioDominioCuenta _servicioDominioCuenta;
        private readonly IServicioDominioProducto _servicioDominioProducto;
        private readonly IServicioAplicacionLavado _servicioAplicacionLavado;
        private readonly IServicioAplicacionCajero _servicioAplicacionCajero;
        private readonly IServicioAplicacionCliente _servicioAplicacionCliente;
        private readonly IServicioAplicacionProducto _servicioAplicacionProducto;
        private readonly IServicioDominioContabilidad _servicioDominioContabilidad;
        private readonly IServicioDominioTransaccionOperacion _servicioDominioTransaccion;
        private readonly IServicioAplicacionParametroGeneral _serivicioAplicacionParametroGeneral;
        private readonly IServicioAplicacionTransferenciaSalida _servicioAplicacionTransferenciaSalida;
        private readonly IServicioAplicacionNotificaciones _servicioAplicacionNotificaciones;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor de Clase
        /// </summary>
        public ServicioAplicacionTransaccionOperacion(
            IContextoAplicacion contexto,
            IServiceProvider servicioProvider,
            IRepositorioRedis repositorioRedis,
            IRepositorioGeneral repositorioGeneral,
            IRepositorioOperacion repositorioOperaciones,
            IServicioDominioCuenta servicioDominioCuenta,
            IServicioDominioProducto servicioDominioProducto,
            IServicioAplicacionLavado servicioAplicacionLavado,
            IServicioAplicacionCajero servicioAplicacionCajero,
            IServicioAplicacionCliente servicioAplicacionCliente,
            IServicioAplicacionProducto servicioAplicacionProducto,
            IServicioDominioContabilidad servicioDominioContabilidad,
            IServicioDominioTransaccionOperacion servicioDominioTransaccion,
            IServicioAplicacionParametroGeneral servicioAplicacionParametroGeneral,
            IServicioAplicacionTransferenciaSalida servicioAplicacionTransferenciaSalida,
            IServicioAplicacionNotificaciones servicioAplicacionNotificaciones) : base(contexto)
        {
            _servicioProvider = servicioProvider;
            _repositorioRedis = repositorioRedis;
            _repositorioGeneral = repositorioGeneral;
            _servicioDominioCuenta = servicioDominioCuenta;
            _repositorioOperacion = repositorioOperaciones;
            _servicioDominioProducto = servicioDominioProducto;
            _servicioAplicacionLavado = servicioAplicacionLavado;
            _servicioAplicacionCajero = servicioAplicacionCajero;
            _servicioAplicacionCliente = servicioAplicacionCliente;
            _servicioAplicacionProducto = servicioAplicacionProducto;
            _servicioDominioTransaccion = servicioDominioTransaccion;
            _servicioDominioContabilidad = servicioDominioContabilidad;
            _servicioAplicacionNotificaciones = servicioAplicacionNotificaciones;
            _serivicioAplicacionParametroGeneral = servicioAplicacionParametroGeneral;
            _servicioAplicacionTransferenciaSalida = servicioAplicacionTransferenciaSalida;
        }

        #endregion

        #region Operaciones Entrantes

        #region Transacciones Entrantes
        /// <summary>
        /// Metodo que procesa la transferencia interbancaria Inmediata Entrante CCE
        /// </summary>
        /// <param name="datosTransferencia">datos de confirmacion</param>
        public async Task<(int, bool)> RealizarTransferenciaEntranteCCE(
            string identificadorInstruccion)
        {
            try
            { 
                var transaccion = _repositorioOperacion
                    .ObtenerPorExpresionConLimite<TransaccionOrdenTransferenciaInmediata>(
                        x => x.IdentificadorInstruccion == identificadorInstruccion &&
                        x.IndicadorTransaccion == General.Receptor)
                    .FirstOrDefault()
                    .ValidarOrdenTransferencia();
                
                var canalPorSubTransaccion = transaccion
                    .CanalCCE.CanalesPorSubTransaciones.FirstOrDefault(x =>
                        x.IndicadorTipo == General.Receptor);

                var usuario = _repositorioOperacion.ObtenerPorCodigo<Usuario>(
                    Empresa.CodigoPrincipal, Agencia.Principal, canalPorSubTransaccion!.CodigoUsuarioTransaccion);

                var fechaSistema = _repositorioOperacion.ObtenerCalendarioCuentaEfectivo();

                var cuentaEfectivo = _repositorioOperacion
                    .ObtenerPorExpresionConLimite<CuentaEfectivo>(x =>
                        x.CodigoCuentaInterbancario == transaccion.CodigoCuentaInterbancariaReceptor)
                    .FirstOrDefault()
                    .ValidarEntidad();

                var subTransaccionCargoITF = _repositorioOperacion
                   .ObtenerPorCodigo<SubTipoTransaccion>(
                    Empresa.CodigoPrincipal, Sistema.CuentaEfectivo,
                    ((int)CatalogoTransaccionEnum.CodigoTransaccionCargoITF).ToString(),
                    ((int)SubTipoTransaccionEnum.CodigoTransaccionCargoITF).ToString());

                int numeroMovimiento = _repositorioGeneral.ObtenerNumeroSerieNoBloqueante(
                    "%", Sistema.CuentaEfectivo, Movimiento.CodigoSerieMovimientoDiarioEnCC, 1);

                var movimientosDiariosCuentaEfectivo = _servicioDominioProducto
                    .GenerarMovimientoTransferenciaEntrante(usuario, canalPorSubTransaccion.SubTipoTransaccion,
                    fechaSistema.FechaHoraSistema, cuentaEfectivo, transaccion, numeroMovimiento);

                var movimientoPrincipalTransferencia = movimientosDiariosCuentaEfectivo.First(m => m.EsMovimientoPrincipal);

                _servicioAplicacionProducto.GenerarMovimientoITF(movimientosDiariosCuentaEfectivo, usuario, 
                    subTransaccionCargoITF, transaccion, cuentaEfectivo, movimientoPrincipalTransferencia);

                var listaTransaccionContabilidad = new List<ITransaccion>();
                listaTransaccionContabilidad.AddRange(movimientosDiariosCuentaEfectivo);

                var codigoCuentaContableAuxiliar = cuentaEfectivo
                    .ObtenerCodigoCuentaContable(AsientoContableDetalle.IndicadorEntrante);

                var cuentaContableDestino = _repositorioOperacion
                    .ObtenerPorCodigo<ParametroPorEmpresa>(Empresa.CodigoPrincipal,
                    Sistema.Bancos, codigoCuentaContableAuxiliar).ValorParametro;

                _servicioDominioContabilidad
                    .GenerarMovimientoAuxiliarContableTransferencia(
                    movimientosDiariosCuentaEfectivo,
                    listaTransaccionContabilidad,
                    AsientoContableDetalle.IndicadorEntrante,
                    cuentaContableDestino);

                var movimientoDiarioTTS = MovimientoInfoAdicional.Crear(
                    movimientoPrincipalTransferencia,
                    string.Empty,
                    _contextoAplicacion.IdTerminalOrigen,
                    _contextoAplicacion.IndicadorCanal,
                    _contextoAplicacion.IndicadorSubCanal);

                var transferencia = GenerarTransferenciaEntrante(
                    movimientoPrincipalTransferencia,
                    transaccion, fechaSistema.FechaHoraSistema,
                    transaccion.EntidadFinancieraOriginante,
                    usuario);

                var asientoContable = GenerarAsientoContableTransferenciaEntrante(
                    canalPorSubTransaccion.SubTipoTransaccion,
                    listaTransaccionContabilidad,
                    fechaSistema.FechaHoraSistema, cuentaEfectivo.CodigoMoneda,
                    transaccion.MontoComision);

                var clienteOriginante = _servicioAplicacionCliente
                    .ObtenerClienteOriginante(transaccion);

                var operacionLavado = _servicioAplicacionLavado.GenerarLavadoTransferenciaEntrante(
                    canalPorSubTransaccion.SubTipoTransaccion,
                    transferencia, clienteOriginante,
                    cuentaEfectivo.Cliente, asientoContable,
                    transaccion.EntidadFinancieraOriginante,
                    fechaSistema.FechaHoraSistema);

                using (var operacionTransaccion = new TransactionScope())
                {
                    transaccion.FinalizarTransaccionExitosa(operacionLavado.NumeroOperacionLavado,
                        transferencia, asientoContable, General.Finalizado,
                        canalPorSubTransaccion.CodigoUsuarioTransaccion, fechaSistema.FechaHoraSistema);
                    movimientosDiariosCuentaEfectivo.ForEach(m => _repositorioOperacion.Adicionar(m));
                    _repositorioOperacion.Adicionar(movimientoDiarioTTS);
                    _repositorioOperacion.GuardarCambios();
                    operacionTransaccion.Complete();
                }

                await Task.CompletedTask;

                return new (transferencia.NumeroMovimiento, EvaluarEnviarNotificacionCliente(cuentaEfectivo, transaccion.CodigoCanal));
            }
            catch (Exception excepcion)
            {
                throw new Exception("Ocurrio un error: " + excepcion.Message, excepcion.InnerException);
            }
        }

        private bool EvaluarEnviarNotificacionCliente(CuentaEfectivo cuentaEfectivo, string? codigoCanal)
        {
            var esInteroperatibilidad = codigoCanal == DatosGeneralesInteroperabilidad.CanalInteroperabilidad;

            if (!esInteroperatibilidad) 
                return true;

            var afiliacionDetalleExistente = _repositorioOperacion.ObtenerPorExpresionConLimite<AfiliacionInteroperabilidadDetalle>(x =>
                x.NumeroCelular == cuentaEfectivo.Cliente.NumeroTelefonoSecundario
                    && x.CodigoCuentaInterbancario == cuentaEfectivo.CodigoCuentaInterbancario
                    && x.IndicadorEstadoAfiliado == AfiliacionInteroperabilidadDetalle.Afiliado).FirstOrDefault();

            return afiliacionDetalleExistente?.IndicadoRecibirNotificacion == General.Si;
        }

        /// <summary>
        /// Metodo que procesa el rechazo de la transferencias Interbancarias Inmediata Pendiente y registra solo la comisión
        /// </summary>
        /// <param name="identificadorInstruccion"></param>
        public async Task RealizarRechazoTransferenciaEntranteCCE(
            string identificadorInstruccion)
        {
            try
            {
                var transaccion = _repositorioOperacion
                    .ObtenerPorExpresionConLimite<TransaccionOrdenTransferenciaInmediata>(
                        x => x.IdentificadorInstruccion == identificadorInstruccion &&
                        x.IndicadorTransaccion == General.Receptor)
                    .FirstOrDefault()
                    .ValidarOrdenTransferencia();

                var canalPorSubTransaccion = transaccion
                    .CanalCCE.CanalesPorSubTransaciones.FirstOrDefault(x =>
                        x.IndicadorTipo == General.Receptor);

                var usuario = _repositorioOperacion.ObtenerPorCodigo<Usuario>(
                    Empresa.CodigoPrincipal, Agencia.Principal, canalPorSubTransaccion!.CodigoUsuarioTransaccion);

                var fechaSistema = _repositorioOperacion.ObtenerCalendarioCuentaEfectivo();

                var numeroAsiento = _repositorioGeneral.ObtenerNumeroSerieNoBloqueante(
                    "%", Sistema.Contabilidad, AsientoContable.CodigoSerieAsientoEnCC, 1);

                var tipoCambioContabilidad = _servicioAplicacionCajero.ObtenerTasaCambio(
                    TipoCambioActual.Contabilidad, fechaSistema.FechaHoraSistema);

                var cuentasPuente = ObtenerCuentaPuenteContableTransferenciaEntrante(transaccion.CodigoMoneda)
                    ?? new List<ParametroPorEmpresa>();

                var listaCuentasContables = new List<CuentaContableInfoDTO>();

                foreach (var cuentasParametro in cuentasPuente)
                {
                    var descripcionDetalleAsiento = cuentasParametro.CodigoParametro == ParametroPorEmpresa.CodigoCuentaContableComisionSoles
                    || cuentasParametro.CodigoParametro == ParametroPorEmpresa.CodigoCuentaContableComisionDolares
                    ? cuentasParametro.DescripcionParametro : canalPorSubTransaccion.SubTipoTransaccion.DescripcionSubTransaccion;

                    var tipoAsientoContable = cuentasParametro.CodigoParametro == ParametroPorEmpresa.CodigoCuentaContableComisionSoles
                        || cuentasParametro.CodigoParametro == ParametroPorEmpresa.CodigoCuentaContableComisionDolares
                        ? AsientoContableDetalle.CodigoCredito : AsientoContableDetalle.CodigoDebito;

                    listaCuentasContables.Add(_servicioAplicacionCajero.ObtenerDatosCuentaContable(cuentasParametro.ValorParametro,
                        usuario.CodigoAgencia, AsientoContable.CodigoUnidadEjecutora, tipoAsientoContable, descripcionDetalleAsiento, fechaSistema.FechaHoraSistema));
                }

                var asientoContable = _servicioDominioContabilidad
                    .GenerarAsientoContableCompletoPendienteTransferenciaComisionEntrante(
                    listaCuentasContables, transaccion.MontoComision, canalPorSubTransaccion.SubTipoTransaccion,
                    fechaSistema.FechaHoraSistema, usuario, numeroAsiento, tipoCambioContabilidad);

                var montoDiferencia = asientoContable.DiferenciaDebitosCreditos();

                if (montoDiferencia != 0)
                    ExisteDiferenciasEnMontosAsiento(asientoContable, montoDiferencia, fechaSistema.FechaHoraSistema);

                _servicioDominioContabilidad.CerrarAsientoContable(asientoContable);

                using (var operacionTransaccion = new TransactionScope())
                {
                    transaccion.FinalizarTransaccionRechazo(asientoContable, canalPorSubTransaccion.CodigoUsuarioTransaccion, 
                        General.Rechazo, fechaSistema.FechaHoraSistema);
                    _repositorioOperacion.GuardarCambios();
                    operacionTransaccion.Complete();
                }
            }
            catch (Exception excepcion)
            {
                throw new Exception("Ocurrio un error: " + excepcion.Message, excepcion.InnerException);
            }
        }

        /// <summary>
        /// Metodo que procesa la devolucion transferencia interbancaria Inmediata CCE
        /// </summary>
        /// <param name="transaccion">datos de la transaccion</param>
        public async Task RealizarTransferenciaDevolucionCCE(
            TransaccionOrdenTransferenciaInmediata transaccion,
            string identificadorTransaccion,
            Calendario fechaSistema,
            string codigoUsuario)
        {
            try
            {
                var usuario = _repositorioOperacion.ObtenerPorCodigo<Usuario>(
                    Empresa.CodigoPrincipal, Agencia.Principal, codigoUsuario);

                var cuentaEfectivo = _repositorioOperacion
                    .ObtenerPorExpresionConLimite<CuentaEfectivo>(x =>
                        x.CodigoCuentaInterbancario == transaccion.CodigoCuentaInterbancariaOriginante)
                    .FirstOrDefault()
                    .ValidarEntidadTexto("Cuenta efectivo");

                var subTipoTransaccionTransferencia = _repositorioOperacion
                    .ObtenerPorCodigo<SubTipoTransaccion>(
                    Empresa.CodigoPrincipal, Sistema.CuentaEfectivo,
                    ((int)CatalogoTransaccionEnum.CodigoTransferenciaInmediataEntrante).ToString(),
                    ((int)SubTipoTransaccionEnum.CodigoDevolucionTransferenciaEntrante).ToString());

                int numeroMovimiento = _repositorioGeneral.ObtenerNumeroSerieNoBloqueante(
                    "%", Sistema.CuentaEfectivo, Movimiento.CodigoSerieMovimientoDiarioEnCC, 1);

                var movimientosDiariosCuentaEfectivo = _servicioDominioProducto
                    .GenerarMovimientoTransferenciaDevolucion(usuario,
                    subTipoTransaccionTransferencia, fechaSistema.FechaHoraSistema,
                    cuentaEfectivo, transaccion, numeroMovimiento);

                var movimientoPrincipalTransferencia = movimientosDiariosCuentaEfectivo
                    .FirstOrDefault(m => m.EsMovimientoPrincipal);

                var listaTransaccionContabilidad = new List<ITransaccion>();
                listaTransaccionContabilidad.AddRange(movimientosDiariosCuentaEfectivo);

                var codigoCuentaContableAuxiliar = cuentaEfectivo
                    .ObtenerCodigoCuentaContable(AsientoContableDetalle.IndicadorSaliente);

                var cuentaContableDestino = _repositorioOperacion.ObtenerPorCodigo<ParametroPorEmpresa>(
                    Empresa.CodigoPrincipal, Sistema.Bancos, codigoCuentaContableAuxiliar).ValorParametro;

                _servicioDominioContabilidad
                    .GenerarMovimientoAuxiliarContableTransferencia(
                    movimientosDiariosCuentaEfectivo,
                    listaTransaccionContabilidad,
                    AsientoContableDetalle.IndicadorEntrante,
                    cuentaContableDestino);

                var transferencia = GenerarTransferenciaEntrante(
                    movimientoPrincipalTransferencia,
                    transaccion, fechaSistema.FechaHoraSistema,
                    transaccion.EntidadFinancieraOriginante, usuario);

                var numeroAsiento = _repositorioGeneral.ObtenerNumeroSerieNoBloqueante(
                    "%", Sistema.Contabilidad, AsientoContable.CodigoSerieAsientoEnCC, 1);

                var asientoContable = GenerarAsientoContableTransferenciaEnCC(
                    listaTransaccionContabilidad,
                    fechaSistema.FechaHoraSistema,
                    numeroAsiento);

                var montoDiferencia = asientoContable.DiferenciaDebitosCreditos();

                if (montoDiferencia != 0)
                    ExisteDiferenciasEnMontosAsiento(asientoContable, montoDiferencia, fechaSistema.FechaHoraSistema);

                _servicioDominioContabilidad.CerrarAsientoContable(asientoContable);

                var clienteOriginante = _servicioAplicacionCliente
                    .ObtenerClienteOriginante(cuentaEfectivo);

                var operacionLavado = _servicioAplicacionLavado
                    .GenerarLavadoTransferenciaEntrante(
                    subTipoTransaccionTransferencia,
                    transferencia, clienteOriginante,
                    cuentaEfectivo.Cliente, asientoContable,
                    transaccion.EntidadFinancieraOriginante,
                    fechaSistema.FechaHoraSistema);

                using (var operacionTransaccion = new TransactionScope())
                {
                    transaccion.ActualizarTransaccionSaliente(General.Rechazo, codigoUsuario,
                        identificadorTransaccion, fechaSistema.FechaHoraSistema);
                    movimientosDiariosCuentaEfectivo.ForEach(m => _repositorioOperacion.Adicionar(m));
                    _repositorioOperacion.Adicionar(asientoContable);
                    _repositorioOperacion.Adicionar(transferencia);
                    _repositorioOperacion.GuardarCambios();
                    operacionTransaccion.Complete();
                }
            }
            catch (Exception excepcion)
            {
                throw new Exception("Ocurrio un error: " + excepcion.Message, excepcion.InnerException);
            }
        }

        #endregion

        /// <summary>
        /// Método que genera la Transferencia Inmediata Entrante con un solo detalle.
        /// </summary>
        /// <param name="movimientoPrincipalTransferencia">movimiento principal</param>
        /// <param name="transaccion">datos de la transaccion</param>
        /// <param name="fechaSistema">fecha del sistema</param>
        /// <param name="entidadOrdenanteCCE">datos de la entidad ordenante</param>
        /// <param name="usuario">usuario que realiza la operación</param>
        /// <returns>Retorna Transferencia Entrante </returns>
        private Transferencia GenerarTransferenciaEntrante(
            MovimientoDiario? movimientoPrincipalTransferencia,
            TransaccionOrdenTransferenciaInmediata transaccion,
            DateTime fechaSistema,
            EntidadFinancieraInmediata entidadOrdenanteCCE,
            Usuario usuario)
        {
            int numeroTransaferencia = _repositorioGeneral.ObtenerNumeroSerieNoBloqueante(
                "%", Sistema.CuentaEfectivo, Transferencia.CodigoSerieTransferenciaEnCC, 1);

            var clienteOriginante = _servicioAplicacionCliente
                .ObtenerClienteOriginante(transaccion);

            var transferencia = Transferencia.CrearTransferenciaInmediataCCE(
                numeroTransaferencia,
                transaccion.TipoTransferencia,
                movimientoPrincipalTransferencia!.Cuenta,
                (int)movimientoPrincipalTransferencia.NumeroMovimiento,
                transaccion.MontoTransferencia,
                fechaSistema,
                usuario,
                General.CanalCCE);

            var detalleTransferenciaCCE = _servicioDominioTransaccion
                .GenerarDetalleTransferencia(
                    transaccion,
                    transferencia,
                    clienteOriginante,
                    entidadOrdenanteCCE);

            transferencia.AgregarDetalleEntrantesCCE(detalleTransferenciaCCE);
            return transferencia;
        }

        /// <summary>
        /// Método que genera el asiento contable Transferencias Inmediata Entrante CCE
        /// </summary>
        /// <param name="listaTransaccionContabilidad"></param>
        /// <param name="fechaSistema"></param>
        /// <param name="codigoMoneda"></param>
        /// <param name="montoComision"></param>
        /// <returns>Retorna Asiento Contable Transferencias Inmediata Entrante</returns>
        private AsientoContable GenerarAsientoContableTransferenciaEntrante(
            SubTipoTransaccion subTipoTransaccionTransferencia,
            IList<ITransaccion> listaTransaccionContabilidad,
            DateTime fechaSistema,
            string codigoMoneda,
            decimal montoComision)
        {
            var numeroAsiento = _repositorioGeneral.ObtenerNumeroSerieNoBloqueante("%", Sistema.Contabilidad,
                AsientoContable.CodigoSerieAsientoEnCC, 1);

            var asiento = GenerarAsientoContableTransferenciaEnCC(listaTransaccionContabilidad, fechaSistema, numeroAsiento);

            var tipoCambioContabilidad = _servicioAplicacionCajero.ObtenerTasaCambio(
                    TipoCambioActual.Contabilidad, fechaSistema);

            var movimientoPrincipal = listaTransaccionContabilidad
                .ToList<IMovimientoContable>()
                .FirstOrDefault(m => m.EsPrincipal);

            var cuentasPuente = ObtenerCuentaPuenteContableTransferenciaEntrante(codigoMoneda)
                    ?? new List<ParametroPorEmpresa>();

            var listaCuentasContables = new List<CuentaContableInfoDTO>();

            foreach (var cuentasParametro in cuentasPuente)
            {
                var tipoAsientoContable = cuentasParametro.CodigoParametro == ParametroPorEmpresa.CodigoCuentaContableComisionSoles
                    || cuentasParametro.CodigoParametro == ParametroPorEmpresa.CodigoCuentaContableComisionDolares
                    ? AsientoContableDetalle.CodigoCredito : AsientoContableDetalle.CodigoDebito;

                listaCuentasContables.Add(_servicioAplicacionCajero.ObtenerDatosCuentaContable(cuentasParametro.ValorParametro, 
                    asiento.CodigoAgencia, movimientoPrincipal?.CodigoUnidadEjecutora ?? string.Empty, tipoAsientoContable, string.Empty, fechaSistema));
            }

            var descripcion = $"{AsientoContable.DescripcionAsientoComisionTinInmediataEntrante}-Cta {listaTransaccionContabilidad.First().NumeroCuenta}";

            _servicioDominioContabilidad.GenerarDetalleAsientoDeCuentaPuenteTransferenciaEntrante(
                subTipoTransaccionTransferencia, asiento, movimientoPrincipal, listaCuentasContables,
                montoComision, tipoCambioContabilidad, descripcion, fechaSistema);

            var montoDiferencia = asiento.DiferenciaDebitosCreditos();

            if (montoDiferencia != 0)
                ExisteDiferenciasEnMontosAsiento(asiento, montoDiferencia, fechaSistema);

            _servicioDominioContabilidad.CerrarAsientoContable(asiento);

            return asiento;
        }

        /// <summary>
        /// Generar el asiento contable para la transaccion
        /// </summary>
        /// <param name="listaTransaccionContabilidad"></param>
        /// <param name="fechaSistema"></param>
        /// <param name="numeroAsiento"></param>
        /// <returns>Retorna el Asiento Contable de CC</returns>
        public AsientoContable GenerarAsientoContableTransferenciaEnCC(
            IList<ITransaccion> listaTransaccionContabilidad,
            DateTime fechaSistema,
            int numeroAsiento)
        {
            var tipoCambioContabilidad = _servicioAplicacionCajero.ObtenerTasaCambio(
                TipoCambioActual.Contabilidad, fechaSistema);

            var listaTransaccionesContabilidad = listaTransaccionContabilidad
                .ToList<IMovimientoContable>()
                .Where(p => p.AplicaAsiento)
                .ToArray();

            foreach (var item in listaTransaccionesContabilidad)
            {
                var cuentaContableDatos = _servicioAplicacionCajero.ObtenerDatosCuentaContable(item.CuentaContable,
                    item.CodigoAgencia, Empresa.CodigoPrincipal, string.Empty, string.Empty, fechaSistema);

                item.AsignarTasaCambioLocal(cuentaContableDatos?.TasaCambioLocal ?? 0);
                item.AsignarTasaCambioCuenta(cuentaContableDatos?.TasaCambioCuenta ?? 0);
            }

            return _servicioDominioContabilidad.GenerarAsientoContableCompletoPendiente(
                numeroAsiento,
                fechaSistema,
                tipoCambioContabilidad.ValorCompra,
                tipoCambioContabilidad.ValorCompra,
                listaTransaccionesContabilidad);
        }

        /// <summary>
        /// Metodo que Obtiene las Cuentas Puente de Transferencias Inmediatas Entrantes
        /// </summary>
        /// <param name="codigoMoneda"></param>
        /// <returns>Retorna Cuentas Puente</returns>
        private List<ParametroPorEmpresa> ObtenerCuentaPuenteContableTransferenciaEntrante(
            string codigoMoneda)
        {
            var codigoCuentaContable = codigoMoneda == ((int)MonedaCodigo.Soles).ToString()
                ? ParametroPorEmpresa.CodigoCuentaContableEntradaSoles
                : ParametroPorEmpresa.CodigoCuentaContableEntradaDolares;

            var codigoCuentaContableComision = codigoMoneda == ((int)MonedaCodigo.Soles).ToString()
                ? ParametroPorEmpresa.CodigoCuentaContableComisionSoles
                : ParametroPorEmpresa.CodigoCuentaContableComisionDolares;

            return _servicioAplicacionCajero
                .ObtenerListaCuentaPuente(codigoCuentaContable, codigoCuentaContableComision);
        }

        #endregion

        #region  Operaciones Salientes
        /// <summary>
        /// Método que realiza la consulta de cuenta del Receptor
        /// </summary>
        /// <returns></returns>
        public async Task<ResultadoConsultaCuentaCCE> ConsultaCuentaReceptor(ConsultaCuentaReceptorDTO datos)
        {
            try
            {
                var estrategia = _servicioProvider
                    .GetRequiredKeyedService<IServicioTipoTransferencia>(datos.CodigoTipoTransferencia);

                var cuerpoConsulta = await estrategia.ConstruirCuerpoConsulta(datos);

                return await ConsultaCuentaReceptorPorCCE(cuerpoConsulta, false);
            }
            catch (Exception excepcion)
            {
                throw new ValidacionException(excepcion.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos del cliente receptor desde la CCE
        /// </summary>
        /// <param name="datosConsulta">Datos de la consulta cuenta origen</param>
        /// <param name="consultaPorQr">Si la consulta es por un QR</param>
        /// <returns>Datos del cliente recpetor desde la CCE</returns>
        public async Task<ResultadoConsultaCuentaCCE> ConsultaCuentaReceptorPorCCE(
            ConsultaCuentaOperacionDTO datosConsulta, bool consultaPorQr)
        {
            var fechaSistema = _repositorioOperacion.ObtenerCalendarioCuentaEfectivo();

            var cuerpoConsulta = datosConsulta.ADatosConsultaCuentaSalida();

            bool saldoValido = ServicioDominioTransaccionOperacion.ValidarSaldoDisponible(
                datosConsulta.CuentaEfectivoDTO.SaldoDisponible, datosConsulta.CuentaEfectivoDTO.MontoMinimo);

            if (!saldoValido) throw new ValidacionException("El cliente no cuenta con saldo suficiente");

            cuerpoConsulta.IndicadorSaldoValido = saldoValido;

            var cuentaReceptor = await _servicioAplicacionTransferenciaSalida
                .ObtenerDatosCuentaCCE(cuerpoConsulta, fechaSistema, consultaPorQr);

            if (cuentaReceptor.Codigo == CodigoRespuesta.Rechazada)
                throw new ValidacionException(cuentaReceptor.RazonExtra ?? cuentaReceptor.Razon);

            datosConsulta.NumeroCuentaOTarjeta = datosConsulta.Canal == DatosGeneralesInteroperabilidad.CanalInteroperabilidad
                ? cuentaReceptor.Datos.CodigoCuentaInterbancariaReceptor : datosConsulta.NumeroCuentaOTarjeta;

            var (codigoEntidad, codigoOficina) = ServicioDominioTransaccionOperacion
                .ObtenerFiltroParaOficina(
                    datosConsulta.EntidadReceptora?.CodigoEntidad,
                    datosConsulta.EntidadReceptora?.OficinaPagoTarjeta,
                    datosConsulta.NumeroCuentaOTarjeta,
                    datosConsulta.TipoTransferencia);

            var oficinaDestino = _repositorioOperacion
                .ObtenerPorExpresionConLimite<OficinaCCE>(x =>
                    x.EntidadFinancieraCCE.CodigoEntidad == codigoEntidad &&
                    x.CodigoOficina == codigoOficina &&
                    x.EstadoOficina == General.Activo)
                .FirstOrDefault()
                .ValidarEntidadTexto("Oficina destino");

            var entidadFinancieraOriginante = _repositorioOperacion
                .ObtenerPorExpresionConLimite<EntidadFinancieraDiferida>(x =>
                    x.CodigoEntidad == datosConsulta.CuentaEfectivoDTO.CodigoCuentaInterbancario.Substring(0, 3))
                .FirstOrDefault()
                .ValidarEntidadTexto("Entidad originante");

            var estrategia = _servicioProvider
                .GetRequiredKeyedService<IServicioTipoTransferencia>(datosConsulta.TipoTransferencia);

            var plaza = estrategia.DefinirPlaza(entidadFinancieraOriginante, oficinaDestino,
                datosConsulta.CuentaEfectivoDTO.CodigoCuentaInterbancario);

            var comision = _repositorioOperacion
                .ObtenerPorExpresionConLimite<ComisionCCE>(m =>
                    m.TipoTransferencia.Codigo == cuerpoConsulta.TipoTransaccion &&
                    m.Moneda.CodigoMoneda == cuerpoConsulta.Moneda &&
                    m.CodigoAplicacionTarifa == plaza)
                .FirstOrDefault()
                .ValidarEntidadTexto("Comision");

            bool esComisionExonerado = estrategia.VerificarSiEsExoneradoComisión(comision.EsMismaPlaza,
                datosConsulta.CuentaEfectivoDTO.NumeroCuenta, fechaSistema.FechaHoraSistema);

            return datosConsulta.AResultadoConsulta(cuentaReceptor.Datos, comision.ADatosComision(), esComisionExonerado, plaza);
        }

        /// <summary>
        /// Metodo que realizar el debito interno y el debito externo
        /// Realiza la transferencia interna, luego envia la ordne de transferencia a la CCE
        /// En caso de fallar algo en el envio de orden a la CCE, esta se reversara internamente
        /// </summary>
        /// <param name="datosOperacion">Datos para realizar la transferencia</param>
        /// <returns>Retorna el resultado la operacion en ventanilla</returns>
        public async Task<string> RealizarTransferenciaVentanilla(OrdenTransferenciaVentanillaDTO datosOperacion)
        {
            var fechaSistema = _repositorioOperacion.ObtenerCalendarioCuentaEfectivo();

            var cuerpoTransferencia = datosOperacion.ADatosRealizarOrdenVentanilla();

            var (operacion, asiento, transferencia, movimientoDiario) = await RealizarTransferenciaSalienteCCE(cuerpoTransferencia, false);

            var cuerpoOrden = operacion.ADatosOrdenTransferencia(
                datosOperacion.ControlMonto,
                datosOperacion.ConceptoCobroTarifa,
                datosOperacion.DatosConsultaCuentaCCE,
                datosOperacion.SesionUsuario);

            await EnviarOrdenTransferenciaCCE(cuerpoOrden, fechaSistema, asiento, transferencia);            

            await _servicioAplicacionCliente.EnviarNotificacionAntifraudeUNIBANCA(cuerpoOrden.NumeroMovimiento, NotificacionUnibancaDTO.NotificacionTransferenciaVentanilla);

            await _servicioAplicacionCliente.EnviarCorreoClienteTransferenciaInmediata(cuerpoOrden.NumeroMovimiento, 
                datosOperacion.NombreDocumentoTerminos, datosOperacion.DocumentoTerminos);

            return operacion.ResultadoImpresion;
        }

        /// <summary>
        /// Realiza la transferencia completa para interoperabilidad
        /// </summary>
        /// <param name="ordenTransferencia">Datos de la transferencia</param>
        /// <returns>Resultado de la transferencia</returns>
        public async Task<ResultadoTransferenciaCanalElectronico> RealizarTransferenciaCanalElectronico(
            OrdenTransferenciaCanalElectronicoDTO ordenTransferencia)
        {
            var subCanal = ((int)CanalInmediataEnum.SubCanalTinInmediata).ToString();

            var fechaSistema = _repositorioOperacion.ObtenerCalendarioCuentaEfectivo();

            var tipoDocumentoReceptor = _serivicioAplicacionParametroGeneral.ObtenertipoDocumentoTakana(
                ordenTransferencia.ResultadoConsultaCuenta.TipoDocumentoReceptor);

            var clienteBeneficiario = ordenTransferencia.ResultadoConsultaCuenta.AClienteExternoDTO(tipoDocumentoReceptor);

            var numeroLavado = _servicioAplicacionLavado.RegistrarNumeroLavado(
                ordenTransferencia.ResultadoConsultaCuenta.Comision.CodigoMoneda,
                fechaSistema.FechaHoraSistema, ordenTransferencia.ControlMonto.Monto,
                ordenTransferencia.ResultadoConsultaCuenta.TipoTransaccion,
                ordenTransferencia.ResultadoConsultaCuenta.CodigoCuentaInterbancariaDeudor,
                subCanal, ordenTransferencia.NumeroCuenta, clienteBeneficiario);

            var sesionUsuario = _contextoAplicacion.ASesionUsuarioCanalElectronico();

            var cuerpoTransferencia = ordenTransferencia.ARealizarTranferenciaCanalElectronico(sesionUsuario, numeroLavado, subCanal);

            var (operacion, asiento, transferencia, movimientoDiario) = await RealizarTransferenciaSalienteCCE(cuerpoTransferencia, true);

            await _servicioAplicacionNotificaciones.GenerarOperacionNotificada
                (movimientoDiario!.NumeroMovimiento.ToString(), movimientoDiario.SubTipoTransaccionMovimiento, movimientoDiario.FechaMovimiento);

            var cuerpoOrden = operacion.ADatosOrdenTransferencia(ordenTransferencia.ControlMonto, 
                ConceptoCobroCCE.CodigoConceptoOtro, ordenTransferencia.ResultadoConsultaCuenta, sesionUsuario);

            await EnviarOrdenTransferenciaCCE(cuerpoOrden, fechaSistema, asiento, transferencia);

            await _servicioAplicacionCliente.EnviarNotificacionAntifraudeUNIBANCA(cuerpoOrden.NumeroMovimiento, NotificacionUnibancaDTO.NotificacionTransferenciaCanalElectronico);

            await _servicioAplicacionNotificaciones.EnviarNotificacionCuentaEfectivo(movimientoDiario!);

            return operacion.AResultadoOperacionTransferencia(fechaSistema.FechaHoraSistema);
        }

        /// <summary>
        /// Metodo que realiza la transferencia
        /// </summary>
        /// <param name="datosOperacion">Datos de transferencia</param>
        public async Task<(ResultadoTransferenciaInmediataDTO, AsientoContable, Transferencia, MovimientoDiario?)> RealizarTransferenciaSalienteCCE(
            RealizarTransferenciaInmediataDTO datosOperacion, bool esCanalElectronico)
        {
            var respuestaConexion = _servicioAplicacionTransferenciaSalida.EchoTest();

            if (!respuestaConexion.Result.Datos)
                throw new ValidacionException("Lo sentimos, ocurrio un error en las comunicaciones con la CCE");

            var usuario = _repositorioOperacion
                .ObtenerPorCodigo<Usuario>(Empresa.CodigoPrincipal, datosOperacion.CodigoAgencia, datosOperacion.CodigoUsuario);

            var fechaSistema = _repositorioOperacion
                .ObtenerPorCodigo<Calendario>(Empresa.CodigoPrincipal, datosOperacion.CodigoAgencia, Sistema.CuentaEfectivo);

            var cuentaEfectivo = _repositorioOperacion
                .ObtenerPorCodigo<CuentaEfectivo>(Empresa.CodigoPrincipal, datosOperacion.NumeroCuentaOriginante);

            ValidarMontosYLimites(datosOperacion.CodigoTipoTransferenciaCce, cuentaEfectivo.CodigoMoneda,
                cuentaEfectivo.SaldoDisponible, datosOperacion.ControlMonto.Monto);

            string codigoSubTipoTransaccion = ServicioDominioTransaccionOperacion
                .ObtenerSubTipoTransaccionSalida(datosOperacion.CodigoTipoTransferenciaCce, datosOperacion.SubCanal);

            var subTipoTransaccionTransferencia = _repositorioOperacion
                .ObtenerPorCodigo<SubTipoTransaccion>(
                    Empresa.CodigoPrincipal, Sistema.CuentaEfectivo, ((int)CatalogoTransaccionEnum
                    .CodigoTransferenciaInmediataSaliente).ToString(), codigoSubTipoTransaccion);

            var subTransaccionITF = _repositorioOperacion
                .ObtenerPorCodigo<SubTipoTransaccion>(Empresa.CodigoPrincipal, Sistema.CuentaEfectivo,
                ((int)CatalogoTransaccionEnum.CodigoTransaccionCargoITF).ToString(),
                ((int)SubTipoTransaccionEnum.CodigoTransaccionCargoITF).ToString());

            var movimientosTransferencia = _servicioAplicacionProducto
                .GenerarMovimientoTransferenciaSaliente(cuentaEfectivo, datosOperacion, subTipoTransaccionTransferencia,
                    subTransaccionITF, usuario, fechaSistema.FechaHoraSistema)
                .ToList();

            var movimientoPrincipalTransferencia = movimientosTransferencia.FirstOrDefault(m => m.EsMovimientoPrincipal);

            var listaTransaccionContabilidad = new List<ITransaccion>();
            listaTransaccionContabilidad.AddRange(movimientosTransferencia);

            var codigoCuentaContableAuxiliar = cuentaEfectivo
                .ObtenerCodigoCuentaContable(AsientoContableDetalle.IndicadorSaliente);

            var cuentaContableDestino = _repositorioGeneral
                .ObtenerPorCodigo<ParametroPorEmpresa>(Empresa.CodigoPrincipal,
                Sistema.Bancos, codigoCuentaContableAuxiliar).ValorParametro;

            _servicioDominioContabilidad.GenerarMovimientoAuxiliarContableTransferencia(movimientosTransferencia,
                listaTransaccionContabilidad, AsientoContableDetalle.IndicadorSaliente, cuentaContableDestino);

            GenerarMovimientosComision(movimientoPrincipalTransferencia, listaTransaccionContabilidad, datosOperacion, usuario);

            var movimientoDiarioTTS = MovimientoInfoAdicional.Crear(
                movimientoPrincipalTransferencia,
                _contextoAplicacion.IdUsuarioAutenticado,
                _contextoAplicacion.IdTerminalLogin,
                _contextoAplicacion.IdCanalOrigen);

            int numeroTransaferencia = _repositorioGeneral.ObtenerNumeroSerieNoBloqueante("%",
                Sistema.CuentaEfectivo, Transferencia.CodigoSerieTransferenciaEnCC, 1);

            var entidadDestino = _repositorioOperacion
                .ObtenerPorExpresionConLimite<EntidadFinancieraDiferida>(x =>
                    x.CodigoEntidad == datosOperacion.CodigoEntidadReceptora.Substring(1, 3))
                .FirstOrDefault()
                .ValidarEntidadTexto("Entidad Destino");

            var transferencia = _servicioDominioTransaccion
                .GenerarTransferenciaInmediata(movimientoPrincipalTransferencia, datosOperacion,
                fechaSistema.FechaHoraSistema, usuario, entidadDestino, numeroTransaferencia);

            var numeroAsiento = _repositorioGeneral.ObtenerNumeroSerieNoBloqueante("%",
                Sistema.Contabilidad, AsientoContable.CodigoSerieAsientoEnCC, 1);

            var asiento = GenerarAsientoContableTransferenciaEnCC(
                listaTransaccionContabilidad, fechaSistema.FechaHoraSistema, numeroAsiento);

            var montoDiferencia = asiento.DiferenciaDebitosCreditos();

            if (montoDiferencia != 0)
                ExisteDiferenciasEnMontosAsiento(asiento, montoDiferencia, fechaSistema.FechaHoraSistema);

            _servicioDominioContabilidad.CerrarAsientoContable(asiento);

            _servicioAplicacionLavado.CompletarLavadoTransferenciaInterbancario(Transferencia.IdOperacionTransferenciaInmediata,
                movimientoPrincipalTransferencia, transferencia.DetallesSalientes.First(), datosOperacion.NumeroLavado, datosOperacion.SubCanal);

            if (ServicioDominioTransaccionOperacion.DefinirAgregacionVinculoMotivo(datosOperacion))
            {
                var vinculoMotivo = _servicioDominioTransaccion.AgregarVinculoMotivo(datosOperacion.MotivoVinculo!,
                    movimientoPrincipalTransferencia, datosOperacion.CodigoTipoTransferenciaCce);
                _repositorioOperacion.Adicionar(vinculoMotivo);
            }

            transferencia.AgregarAgencia(usuario.Agencia);
            var resultadoImpresion = ImpresionTransferencia(transferencia, datosOperacion, movimientoPrincipalTransferencia
                .NumeroMovimiento.ToString(), datosOperacion.NombreImpresora, esCanalElectronico);

            _repositorioOperacion.Adicionar(movimientoDiarioTTS);

            var resultado = transferencia.AResultadoTransferenciaInternaSaliente(
                asiento.NumeroAsiento, datosOperacion.NumeroLavado, resultadoImpresion);

            await Task.CompletedTask;

            return (resultado, asiento, transferencia, movimientoPrincipalTransferencia);
        }

        /// <summary>
        /// Metodo que envia la orden de transferencia a la CCE
        /// </summary>
        /// <param name="datos">Datos para armar el envio</param>
        public async Task EnviarOrdenTransferenciaCCE(
            OrdenTransferenciaCanalDTO datos, 
            Calendario fechaSistema, 
            AsientoContable asiento,
            Transferencia transferencia)
        {
            var resultado = await _servicioAplicacionTransferenciaSalida
                .EnviarOrdenTransferencia(datos, fechaSistema, asiento, transferencia);

            if (resultado.Codigo == CodigoRespuesta.Rechazada)
            {
                ReversarTransferenciaInmediata(datos.NumeroMovimiento, resultado);
                var mensaje = datos.EsMensajeCliente
                    ? "Se ha realizado la reversión por operación rechaza por la CCE."
                    : resultado.RazonExtra;
                throw new ValidacionException(mensaje);
            }

            var transaccion = _repositorioOperacion
                .ObtenerPorExpresionConLimite<TransaccionOrdenTransferenciaInmediata>(p =>
                    p.IndicadorTransaccion == General.Originante
                    && p.IdentificadorInstruccion == resultado.Datos.IdentificadorTransaccion
                    && p.IndicadorEstadoOperacion == General.Confirmado)
                .FirstOrDefault();

            if (transaccion == null)
                throw new ValidacionException("Se aceptado la operación por parte de la CCE, "
                    + "pero no se ha actualizado en la tabla de control de transacciones porque no se encuentra el registo"
                    + " con instrucción: " + resultado.Datos.IdentificadorTransaccion);

            transaccion.CambiarEstado(General.Finalizado);
            _repositorioOperacion.GuardarCambios();
        }

        /// <summary>
        /// Genera los movimientos de comision para transferencia inmediata
        /// </summary>
        /// <param name="movimientoPrincipalTransferencia">El movimiento principal de la transferencia que se está procesando.</param>
        /// <param name="listaTransaccionContabilidad">Lista de transacciones contables que se tomarán en cuenta para la operación.</param>
        /// <param name="datosOrden">Datos necesarios para realizar la transferencia inmediata, encapsulados en un DTO.</param>
        /// <param name="usuario">El usuario que está realizando la operación.</param>
        /// <returns>Una lista de transacciones contables resultantes de procesar la comisión inmediata.</returns>
        public List<ITransaccion> GenerarMovimientosComision(
            MovimientoDiario movimientoPrincipalTransferencia,
            List<ITransaccion> listaTransaccionContabilidad,
            RealizarTransferenciaInmediataDTO datosOrden,
            Usuario usuario)
        {
            var configuracionComisionTransferencia = _repositorioOperacion
                .ObtenerPorExpresionConLimite<ConfiguracionComision>(c =>
                    c.CodigoComision == ConfiguracionComision.CodigoTransferenciaInterbancaria)
                .FirstOrDefault()
               .ValidarEntidadTexto("Configuracion de codigo de comision");

            var comisionAhorros = ComisionAhorrosAuxiliar.Crear(
                configuracionComisionTransferencia,
                datosOrden.ControlMonto.MontoComisionEntidad);

            var comisionExonerada = _servicioDominioTransaccion.GenerarComisionExonerada(
                datosOrden.EsMismaPlaza, movimientoPrincipalTransferencia, comisionAhorros);

            if (comisionExonerada == null || datosOrden.CodigoTipoTransferenciaCce != TipoTransferencia.CodigoTransferenciaOrdinaria)
            {
                if (datosOrden.ControlMonto.MontoComisionEntidad <= 0)
                    return listaTransaccionContabilidad;

                var codigoCuentaContable = ServicioDominioCajero.ObtenerCodigoCuentaContableComision(
                    movimientoPrincipalTransferencia.Cuenta.CodigoMoneda);

                var cuentaContableComision = _repositorioGeneral.ObtenerPorCodigo<ParametroPorEmpresa>(
                    Empresa.CodigoPrincipal, Sistema.Bancos, codigoCuentaContable).ValorParametro;

                int numeroMovimiento = _repositorioGeneral.ObtenerNumeroSerieNoBloqueante(
                    "%", Sistema.CuentaEfectivo, Movimiento.CodigoSerieMovimientoDiarioEnCC, 1);

                bool indicadorCuentaSueldo = _repositorioGeneral
                    .ObtenerValorParametroPorEmpresa(Sistema.CuentaEfectivo, CuentaEfectivoSueldo.IndicadorCuentaSueldo) == General.Si;

                var comision = _servicioDominioTransaccion.AplicarComisionTransferencia(
                    movimientoPrincipalTransferencia, usuario, comisionAhorros, numeroMovimiento, indicadorCuentaSueldo);

                var movimientoContableComision = _servicioDominioCuenta.GenerarMovimientoContableComision(
                    comision, cuentaContableComision);

                listaTransaccionContabilidad.Add(comision);
                listaTransaccionContabilidad.Add(movimientoContableComision);
                _repositorioOperacion.Adicionar(comision);
            }
            else
            {
                _repositorioOperacion.Adicionar(comisionExonerada);
            }

            return listaTransaccionContabilidad;
        }

        /// <summary>
        /// Metodo para resolver la forma de impresion de transfernecia inmediata
        /// </summary>
        /// <param name="transferencia">Transferencia generada</param>
        /// <param name="datosTransferenciaDetalleCCE">Datos de la transferencia</param>
        /// <param name="numeroMovimiento">Numero de  movimiento</param>
        /// <param name="nombreImpresora">Nombre de la impresora</param>
        /// <param name="esCanalElectronico">es Canal Electronico</param>
        /// <returns></returns>
        private string ImpresionTransferencia(
            Transferencia transferencia,
            RealizarTransferenciaInmediataDTO datosTransferenciaDetalleCCE,
            string numeroMovimiento,
            string nombreImpresora,
            bool esCanalElectronico)
        {
            return esCanalElectronico
                ? numeroMovimiento
                : new AdministradorComprobantes(nombreImpresora)
                    .ATextoInmediatasCCE(transferencia, datosTransferenciaDetalleCCE);
        }

        #region Métodos de Revertir Transferencia
        /// <summary>
        /// Reversa la operacion de transferencia inmediata
        /// </summary>
        /// <param name="numeroMovimiento">Numero de operacion de la transferencia</param>
        /// <param name="resultado">Indicador para revertir la comisión</param>
        public void ReversarTransferenciaInmediata(
            int numeroMovimiento, RespuestaSalidaDTO<OrdenTransferenciaRespuestaTraducidoDTO> resultado)
        {
            var IndicadorReversarComision = ServicioDominioTransaccionOperacion.DefinirReversarComision(resultado.Tipo!);

            var fechaSistema = _repositorioOperacion.ObtenerCalendarioCuentaEfectivo();

            var movimientos = ObtenerMovimientos(numeroMovimiento);
            var transaccion = ObtenerTransaccion(numeroMovimiento);

            var codigoRespuesta = _serivicioAplicacionParametroGeneral.ObtenerErrorLocal(resultado.Datos.RazonCodigoRespuesta);

            string indicadorCuentaSueldo = _repositorioGeneral
                .ObtenerValorParametroPorEmpresa(Sistema.CuentaEfectivo, CuentaEfectivoSueldo.IndicadorCuentaSueldo);

            _servicioDominioContabilidad.AnularAsientoContablePendiente(transaccion.AsientoContable);

            _servicioAplicacionLavado.AnularLavado(movimientos[0]);

            _servicioDominioCuenta.ReversarTransferenciaInmediata(
                transaccion.Transferencia, movimientos, indicadorCuentaSueldo, codigoRespuesta, IndicadorReversarComision);

            if (IndicadorReversarComision)
                transaccion.ActualizarComisionTarifaCCE(TransaccionOrdenTransferenciaInmediata.MontoTarifaReversion,
                    fechaSistema.FechaHoraSistema);
            else
            {
                var listaMovimientoComision = ObtenerMovimientoComision(movimientos, IndicadorReversarComision);

                if (listaMovimientoComision.Count() > 0)
                {
                    var numeroAsiento = _repositorioGeneral.ObtenerNumeroSerieNoBloqueante("%",
                        Sistema.Contabilidad, AsientoContable.CodigoSerieAsientoEnCC, 1);
                    var asientoComisionReversion = GenerarAsientoContableComisionPorReversion(listaMovimientoComision,
                        fechaSistema.FechaHoraSistema, numeroAsiento);
                    transaccion.ActualizarNumeroAsiento(asientoComisionReversion.NumeroAsiento);
                    _repositorioOperacion.Adicionar(asientoComisionReversion);
                }
            }

            transaccion.ActualizarEstadoTransaccion(General.Rechazo, fechaSistema.FechaHoraSistema);

            _repositorioOperacion.GuardarCambios();
        }

        /// <summary>
        /// Obtener movimiento principal
        /// </summary>
        /// <param name="numeroMovimiento"></param>
        /// <returns></returns>
        /// <exception cref="ValidacionException"></exception>
        private List<MovimientoDiario> ObtenerMovimientos(int numeroMovimiento)
        {
            var movimientos = new List<MovimientoDiario>();

            var movimiento = _repositorioOperacion
                .ObtenerPorExpresionConLimite<MovimientoDiario>(x => x.NumeroMovimiento == numeroMovimiento)
                .FirstOrDefault()
                .ValidarEntidad();

            movimientos.Add(movimiento);

            if (!movimiento.EsPrincipal) throw new ValidacionException("No se encontró una operación principal");

            var movimientosSecundariosCC = _repositorioOperacion
                .ObtenerPorExpresionConLimite<MovimientoDiario>(m =>
                    m.NumeroMovimientoFuente == movimiento.NumeroOperacion &&
                    m.NumeroCuenta == movimiento.NumeroCuenta)
                .ToList();

            if (movimientosSecundariosCC.Any())
                movimientos.AddRange(movimientosSecundariosCC);

            return movimientos;
        }

        /// <summary>
        /// Obtener movimiento comisión
        /// </summary>
        /// <param name="movimientos"></param>
        /// <param name="indicadorReversarComision"></param>
        /// <returns></returns>
        private List<ITransaccion> ObtenerMovimientoComision(List<MovimientoDiario> movimientos, bool indicadorReversarComision)
        {
            var movimientoComision = new List<MovimientoDiario>();
            var listaMovimientoComision = new List<ITransaccion>();
            var movimientoPrincipal = movimientos
                .FirstOrDefault(m => m.EsPrincipal);

            foreach (var movimiento in movimientos.Where(g => !g.EsTransaccionITF))
            {
                if (movimiento.SubTipoTransaccionMovimiento.CodigoTipoTransaccion ==
                        ((int)CatalogoTransaccionEnum.CodigoTransferenciaInmediataComision).ToString()
                    && movimiento.SubTipoTransaccionMovimiento.CodigoSubTipoTransaccion ==
                        ((int)SubTipoTransaccionEnum.CodigoTransaccionCargoComision).ToString()
                    && !indicadorReversarComision)
                    movimientoComision.Add(movimiento);
            }

            if (movimientoComision.Count() > 0)
            {               
                listaMovimientoComision.AddRange(movimientoComision);

                var codigoCuentaContable = ServicioDominioCajero.ObtenerCodigoCuentaContableComision(
                    movimientoPrincipal?.Cuenta?.CodigoMoneda ?? movimientos[0].Cuenta.CodigoMoneda);

                var cuentaContableComision = _repositorioGeneral.ObtenerPorCodigo<ParametroPorEmpresa>(
                    Empresa.CodigoPrincipal, Sistema.Bancos, codigoCuentaContable).ValorParametro;

                var movimientoContableComision = _servicioDominioCuenta.GenerarMovimientoContableComision(
                    movimientoComision[0], cuentaContableComision);

                listaMovimientoComision.Add(movimientoContableComision);
            }
            return listaMovimientoComision;
        }

        /// <summary>
        /// Obtener Transaccion
        /// </summary>
        /// <param name="numeroMovimiento"></param>
        /// <returns></returns>
        private TransaccionOrdenTransferenciaInmediata ObtenerTransaccion(int numeroMovimiento)
        {
            return _repositorioOperacion
                .ObtenerPorExpresionConLimite<TransaccionOrdenTransferenciaInmediata>(p =>
                    p.IndicadorTransaccion == General.Originante &&
                    p.NumeroMovimiento == numeroMovimiento)
                .FirstOrDefault()
                .ValidarEntidad();
        }

        /// <summary>
        /// Generar el asiento contable para la comisión por reversión de transferencia
        /// </summary>
        /// <param name="listaTransaccionContabilidad"></param>
        /// <param name="fechaSistema"></param>
        /// <param name="numeroAsiento"></param>
        /// <returns>Retorna el Asiento Contable de CC</returns>
        public AsientoContable GenerarAsientoContableComisionPorReversion(
            IList<ITransaccion> listaTransaccionContabilidad,
            DateTime fechaSistema,
            int numeroAsiento)
        {
            var tipoCambioContabilidad = _servicioAplicacionCajero.ObtenerTasaCambio(
                TipoCambioActual.Contabilidad, fechaSistema);

            var listaTransaccionesContabilidad = listaTransaccionContabilidad
                .ToList<IMovimientoContable>()
                .Where(p => p.AplicaAsiento)
                .ToArray();

            foreach (var item in listaTransaccionesContabilidad)
            {
                var cuentaContableDatos = _servicioAplicacionCajero.ObtenerDatosCuentaContable(item.CuentaContable,
                   item.CodigoAgencia, Empresa.CodigoPrincipal, string.Empty, string.Empty, fechaSistema);

                item.AsignarTasaCambioLocal(cuentaContableDatos.TasaCambioLocal);
                item.AsignarTasaCambioCuenta(cuentaContableDatos.TasaCambioCuenta);
            }

            var asientoPendienteAjuste = _servicioDominioContabilidad.GenerarAsientoContablePorComision(
                numeroAsiento,
                fechaSistema,
                tipoCambioContabilidad.ValorCompra,
                tipoCambioContabilidad.ValorCompra,
                listaTransaccionesContabilidad);

            var montoDiferencia = asientoPendienteAjuste.DiferenciaDebitosCreditos();

            if (montoDiferencia != 0)
                ExisteDiferenciasEnMontosAsiento(asientoPendienteAjuste, montoDiferencia, fechaSistema);

            return _servicioDominioContabilidad.CerrarAsientoContable(asientoPendienteAjuste);
        }

        #endregion

        #region Limites y Montos
        /// <summary>
        /// Valida detalles de la transferencia
        /// </summary>
        /// <param name="datosValidar">Datos a validar</param>
        /// <returns>Retorna codigo de respuesta exitosa</returns>
        public async Task<string> ObtenerDetallesValidadosMontosYLimites(
            ValidarTransaccionInmediataDTO datosValidar)
        {
            ValidarMontosYLimites(datosValidar.CodigoTipoTransferenciaCce, datosValidar.CodigoMoneda,
                datosValidar.SaldoActual, datosValidar.MontoOperacion);

            return CodigoRespuesta.Aceptada;
        }

        /// <summary>
        /// Método que valida que el saldo no sobrepasen los limites y el saldo actual
        /// </summary>
        /// <param name="tipoTransferencia"></param>
        /// <param name="codigoModena"></param>
        /// <param name="saldoActual"></param>
        /// <param name="montoOperacion"></param>
        /// <exception cref="ValidacionException"></exception>
        private void ValidarMontosYLimites(
            string tipoTransferencia,
            string codigoModena,
            decimal saldoActual,
            decimal montoOperacion)
        {
            var limites = _repositorioOperacion
                .ObtenerPorExpresionConLimite<LimiteTransferenciaInmediata>(x =>
                    x.TipoTransferencia.Codigo == tipoTransferencia &&
                    x.CodigoCanal == _contextoAplicacion.IdCanalOrigen &&
                    x.CodigoMoneda == codigoModena &&
                    x.EstadoLimite == General.Activo)
                .FirstOrDefault();

            if (limites == null)
                throw new ValidacionException("No se pueden obtener los límites TIN para el tipo de transaferencia " +
                    "y moneda seleccionada.");

            ServicioDominioTransaccionOperacion.ValidarMontoTransferenciaInmediata(
                saldoActual, montoOperacion, limites.MontoLimiteMaximo, limites.MontoLimiteMinimo);
        }

        /// <summary>
        /// Calcula los totales segun la comision obtenida (expuesto)
        /// </summary>
        /// <param name="comision">Datos de transferencia donde esta la comision</param>
        /// <returns>Control monto lleno</returns>
        public async Task<CalculoComisionDTO> CalcularMontosTotales(CalculoComisionDTO comision)
        {
            var fechaSistema = _repositorioOperacion.ObtenerCalendarioCuentaEfectivo();

            var montoOriginal = comision.MontoOperacion;
            decimal montoItfTotal = 0m;

            var cuentaEfectivo = _repositorioOperacion.ObtenerPorCodigo<CuentaEfectivo>(
                Empresa.CodigoPrincipal, comision.NumeroCuenta);

            await ValidarLimitesPorCanal(cuentaEfectivo.CodigoMoneda, montoOriginal);

            var logicaCuenta = LogicaCuentaEfectivo.ObtenerLogica(cuentaEfectivo);

            var indicadorCuentaSueldo = _repositorioGeneral
                .ObtenerValorParametroPorEmpresa(Sistema.CuentaEfectivo, CuentaEfectivoSueldo.IndicadorCuentaSueldo) == General.Si;

            var movimientosDiariosTts = _repositorioOperacion
                .ObtenerPorExpresionConLimite<MovimientoInfoAdicional>
                    (m => m.NumeroCuenta == comision.NumeroCuenta
                        && m.CodigoCanal == _contextoAplicacion.IdCanalOrigen
                        && m.FechaTransaccion == fechaSistema.FechaHoraSistema.Date)
                .ToList();

            logicaCuenta.ValidarLimitesPorCuenta(montoOriginal, movimientosDiariosTts.Count() , _contextoAplicacion);

            var montoADebitar = logicaCuenta.Retirar(montoOriginal, indicadorCuentaSueldo);
            montoItfTotal += _servicioAplicacionCajero.ObtenerMontoITF(montoADebitar.NoRemunerativo, comision.EsExoneradaITF,
                comision.EsCuentaSueldo, fechaSistema.FechaHoraSistema, comision.MismoTitular);

            var (comisionEntidad, comisionCce) = ServicioDominioTransaccionOperacion.CalcularMontosComision(comision);
            var comisionTotal = ServicioDominioTransaccionOperacion.CalcularComisionTotal(comisionEntidad, comisionCce);

            comision.ControlMonto = montoItfTotal.ADatosControlMonto(
                montoOriginal, comisionCce, comisionEntidad, comisionTotal);

            return comision;
        }

        /// <summary>
        /// Método que valida los limites por canales
        /// </summary>
        /// <param name="codigoMoneda"></param>
        /// <param name="monto"></param>
        /// <returns></returns>
        /// <exception cref="ValidacionException"></exception>
        private async Task ValidarLimitesPorCanal(string codigoMoneda, decimal monto)
        {
            if (_contextoAplicacion.IdCanalOrigen == General.CanalVentanilla)
                return;

            var cantidadMaxima = _repositorioOperacion
                .ObtenerPorExpresionConLimite<ParametroCanalElectronico>(x =>
                    x.CodigoParametro == ParametroCanalElectronico.MontoMaximoPorTransferencia &&
                    x.CodigoMoneda == codigoMoneda &&
                    x.CodigoCanal == _contextoAplicacion.IdCanalOrigen)
                .FirstOrDefault()?.ValorParametro;

            var cantidadMinima = _repositorioOperacion
                .ObtenerPorExpresionConLimite<ParametroCanalElectronico>(x =>
                    x.CodigoParametro == ParametroCanalElectronico.MontoMinimoPorTransferencia &&
                    x.CodigoMoneda == codigoMoneda &&
                    x.CodigoCanal == _contextoAplicacion.IdCanalOrigen)
                .FirstOrDefault()?.ValorParametro;

            if (cantidadMaxima == null || cantidadMinima == null)
                return;

            if(monto > cantidadMaxima)
                throw new ValidacionException("El monto de transferencia es mayor al permitido por el canal");

            if (monto < cantidadMinima)
                throw new ValidacionException("El monto de transferencia es minimo al permitido por el canal");
        }

        /// <summary>
        /// Valida si existen diferencias en los montos del asiento contable y, de ser así,
        /// genera un ajuste contable utilizando la cuenta configurada según si la diferencia
        /// corresponde a débito o crédito.
        /// </summary>
        /// <param name="asiento"></param>
        /// <param name="montoDiferencia"></param>
        /// <param name="fechaSistema"></param>
        private void ExisteDiferenciasEnMontosAsiento(AsientoContable asiento, decimal montoDiferencia, DateTime fechaSistema)
        {
            string numeroCuentaContableAjuste = string.Empty;
            var debeHaber = string.Empty;
            if (montoDiferencia > 0)
            {
                debeHaber = AsientoContableDetalle.CodigoCredito;
                numeroCuentaContableAjuste = _repositorioGeneral.ObtenerValorParametroPorEmpresa(Sistema.Contabilidad,
                    "CTA_DIFCAM_CRED");
            }
            else
            {
                debeHaber = AsientoContableDetalle.CodigoDebito;
                numeroCuentaContableAjuste = _repositorioGeneral.ObtenerValorParametroPorEmpresa(Sistema.Contabilidad,
                    "CTA_DIFCAM_DEB");
            }

            var codigoUnidadEjecutoraAjuste = _repositorioGeneral.ObtenerValorParametroPorEmpresa(Sistema.Contabilidad, "UNIDAD_AJUSTE");

            var cuentaAjuste = _servicioAplicacionCajero.ObtenerDatosCuentaContable(numeroCuentaContableAjuste,
                asiento.CodigoAgencia, codigoUnidadEjecutoraAjuste, debeHaber, string.Empty, fechaSistema);

            _servicioDominioContabilidad.GenerarAjuste(asiento, cuentaAjuste);
        }

        #endregion

        #region Operación Frecuente
        /// <summary>
        /// Método que agrega a una operacion frecuente
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public async Task AgregarOperacionFrecuente(OperacionFrecuenteDTO datos)
        {
            var cuentaOrigen = _repositorioOperacion
                .ObtenerPorCodigo<CuentaEfectivo>(Empresa.CodigoPrincipal, datos.NumeroCuenta);

            await ValidarMaximoOperacionesFrecuentes(cuentaOrigen.CodigoCliente);

            var fechaSistema = _repositorioOperacion.ObtenerCalendarioCuentaEfectivo().FechaHoraSistema;

            var detalles = datos.AFormatoOperacionDetalle();

            var detalleOperacionFrecuente = detalles.Select(x => OperacionFrecuenteDetalle
                .RegistrarDetalle(x.Value, x.Key)).ToList();

            var operacionFrecuente = OperacionFrecuente
                .RegistrarOperacionFrecuente(datos.NumeroCuenta, datos.NombreOperacionFrecuente,
                _contextoAplicacion.CodigoUsuario, fechaSistema, datos.TipoOperacionFrecuente, 
                detalleOperacionFrecuente);

            _repositorioOperacion.Adicionar(operacionFrecuente);
            _repositorioOperacion.GuardarCambios();
        }

        /// <summary>
        /// Validar numero maximo de operaciones freceuntes
        /// </summary>
        private async Task ValidarMaximoOperacionesFrecuentes(string codigoCliente)
        {
            var cantidadMaxima = _repositorioOperacion
                .ObtenerPorCodigo<ParametroCanalElectronico>(
                    ParametroCanalElectronico.NumeroMaximoOperacionesFrecuentes, 
                    ((int)MonedaCodigo.Soles).ToString(), General.APP);

            var operacionesFrecuentesCliente = _servicioAplicacionProducto
                .ObtenerOperacionesFrecuentesDeCliente(codigoCliente);

            if (operacionesFrecuentesCliente.Count >= (int)cantidadMaxima.ValorParametro)
                throw new ValidacionException("Estimado cliente, no puede agregar más operaciones frecuentes." +
                    "Para poder agregar nuevas operaciones frecuentes, primero debe eliminar otras operaciones");
        }
        #endregion

        #endregion  Salientes
    }
}