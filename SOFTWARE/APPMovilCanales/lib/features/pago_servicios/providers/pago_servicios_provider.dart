import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/providers/operaciones_frecuentes_provider.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/services/operaciones_frecuentes_service.dart';
import 'package:caja_tacna_app/features/pago_servicios/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/pago_servicios/models/cuenta_origen.dart';
import 'package:caja_tacna_app/features/pago_servicios/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/pago_servicios/models/empresa.dart';
import 'package:caja_tacna_app/features/pago_servicios/models/obtener_cobro_servicio_response.dart';
import 'package:caja_tacna_app/features/pago_servicios/models/pagar_response.dart';
import 'package:caja_tacna_app/features/pago_servicios/models/pago_servicio.dart';
import 'package:caja_tacna_app/features/pago_servicios/services/pago_servicios_services.dart';
import 'package:caja_tacna_app/features/shared/inputs/email.dart';
import 'package:caja_tacna_app/features/shared/inputs/monto_deuda_servicio.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/services/shared_services.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:formz/formz.dart';

final pagoServiciosProvider =
    NotifierProvider<PagoServiciosNotifier, PagoServiciosState>(
        () => PagoServiciosNotifier());

class PagoServiciosNotifier extends Notifier<PagoServiciosState> {
  @override
  PagoServiciosState build() {
    return PagoServiciosState();
  }

  initDatos() {
    state = state.copyWith(
        cuentasOrigen: [],
        categorias: [],
        ultimosPagos: [],
        cuentaOrigen: () => null,
        empresas: [],
        servicios: {},
        categoriaSeleccionada: () => null,
        numeroSuministro: '',
        searchEmpresa: '',
        servicioPagar: () => null,
        cobroServicio: () => null,
        confirmarResponse: () => null,
        correoElectronicoDestinatario: const Email.pure(''),
        nombreOperacionFrecuente: '',
        operacionFrecuente: false,
        pagarResponse: () => null,
        tokenDigital: '',
        statusBusqueda: StatusBusqueda.editando,
        mostrarCobro: false,
        montoDeudaServicio: const MontoDeudaServicio.pure(''));
  }

  getDatosIniciales() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final DatosInicialesResponse datosInicialesResponse =
          await PagoServiciosService.obtenerDatosIniciales();

      state = state.copyWith(
        cuentasOrigen: datosInicialesResponse.productosDebito,
        categorias: datosInicialesResponse.categorias,
        ultimosPagos: datosInicialesResponse.ultimosPagos,
        mostrarCobro: false,
      );
    } on ServiceException catch (e) {
      ref.read(appRouterProvider).pop();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  obtenerEmpresasPorCategoria(CategoriaPagServ categoria) async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    state = state.copyWith(
      searchEmpresa: '',
      categoriaSeleccionada: () => categoria,
      statusBusqueda: StatusBusqueda.editando,
    );
    try {
      final List<Empresa> empresas =
          await PagoServiciosService.obtenerEmpresasPorCategoria(
        idCategoria: categoria.idTipoCategoriaServicio,
      );

      state = state.copyWith(
        empresas: empresas,
        statusBusqueda: StatusBusqueda.mostrarResultados,
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  obtenerEmpresasPorTexto(
      {required String texto, required bool mostrarResultados}) async {
    if (texto.isEmpty) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('ingrese el nombre de la empresa', SnackbarType.error);
      return;
    }
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    state = state.copyWith(
      categoriaSeleccionada: () => null,
      statusBusqueda: StatusBusqueda.editando,
    );
    try {
      final List<Empresa> empresas =
          await PagoServiciosService.obtenerEmpresasPorTexto(
        texto: texto,
      );

      state = state.copyWith(
        empresas: empresas,
        statusBusqueda: mostrarResultados
            ? StatusBusqueda.mostrarResultados
            : StatusBusqueda.editando,
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  seleccionarServicio({required ServicioPagar servicio}) {
    state = state.copyWith(
      servicioPagar: () => servicio,
      numeroSuministro: '',
    );
    ref.read(appRouterProvider).push('/pago-servicios/buscar-cobro');
  }

  obtenerServiciosPorEmpresa(Empresa empresa) async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final List<PagoServicio> servicios =
          await PagoServiciosService.obtenerServicios(
        codigoCategoria: empresa.codigoCategoria,
        codigoEmpresa: empresa.codigoEmpresa,
        tipoPagoServicio: empresa.tipoPagoServicio,
      );

      state = state.copyWith(
        servicios: {...state.servicios, empresa.codigoEmpresa: servicios},
        mostrarCobro: false,
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  obtenerCobroServicio() async {
    if (state.numeroSuministro.isEmpty) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Ingrese el número de suministro', SnackbarType.error);
      return;
    }
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final ObtenerCobroServicioResponse cobroResponse =
          await PagoServiciosService.obtenerCobroServicio(
        codigoCategoria: state.servicioPagar?.codigoCategoria,
        codigoEmpresa: state.servicioPagar?.codigoEmpresa,
        codigoGrupoEmpresa: state.servicioPagar?.codigoGrupoEmpresa,
        codigoServicio: state.servicioPagar?.codigoServicio,
        nombreCategoria: state.servicioPagar?.nombreCategoria,
        nombreEmpresa: state.servicioPagar?.nombreEmpresa,
        nombreServicio: state.servicioPagar?.nombreServicio,
        suministro: state.numeroSuministro,
        tipoPagoServicio: state.servicioPagar?.tipoPagoServicio,
      );

      state = state.copyWith(
          montoDeudaServicio: cobroResponse.tipoServicio != 'Kasnet'
            ? const MontoDeudaServicio.pure('')
            : state.montoDeudaServicio!.copyWith(
                montoMaximoDeuda: cobroResponse.montoDeuda,
                montoMinimoDeduda: cobroResponse.montoDeudaMinima,
                simboloMoneda: cobroResponse.simboloMoneda,
                value: CtUtils.formatStringWithTwoDecimals(
                  cobroResponse.montoDeuda.toString()
                )
            ),
          cobroServicio: () => cobroResponse, mostrarCobro: true);
      FocusManager.instance.primaryFocus?.unfocus();
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  cargarServicios(int index) async {
    if (state.servicios[state.empresas[index].codigoEmpresa] == null) {
      await obtenerServiciosPorEmpresa(state.empresas[index]);
    }

    List<Empresa> empresas = state.empresas;
    empresas[index] = empresas[index].copyWith(
        servicios: state.servicios[state.empresas[index].codigoEmpresa]);
    state.copyWith(
      empresas: empresas,
    );
  }

  pagarServicio({required bool withPush}) async {
    if (state.cuentaOrigen == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione la cuenta de origen', SnackbarType.error);
      return;
    }
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    resetToken();
    try {
      final PagarResponse pagarResponse =
          await PagoServiciosService.pagarServicio(
        codigoMonedaDeuda: state.cobroServicio?.codigoMoneda,
        montoDeuda: state.cobroServicio?.tipoServicio != 'Kasnet'
          ? state.cobroServicio?.montoDeuda
          : double.parse(state.montoDeudaServicio!.value),
        numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
        identificadorDispositivo: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),
      );

      state = state.copyWith(
        pagarResponse: () => pagarResponse,
        tokenDigital: await CoreService.desencriptarToken(
          pagarResponse.codigoSolicitado,
        ),
      );
      initTimer();
      if (withPush) {
        ref.read(appRouterProvider).push('/pago-servicios/confirmar');
      }
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  confirmar() async {
    FocusManager.instance.primaryFocus?.unfocus();

    if (state.tokenDigital.isEmpty) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Ingrese su Token Digital', SnackbarType.error);
      return;
    }
    if (state.tokenDigital.length != 6) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('El token debe tener 6 dígitos', SnackbarType.error);
      return;
    }

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final ConfirmarResponse confirmarResponse =
          await PagoServiciosService.confirmarServicio(
        tokenDigital: state.tokenDigital,
        codigoEmpresa: state.cobroServicio?.codigoEmpresa,
        codigoMonedaDeuda: state.cobroServicio?.codigoMoneda,
        codigoServicio: state.cobroServicio?.codigoServicio,
        comisionDeuda: state.cobroServicio?.comisionDeuda,
        montoDeuda: state.cobroServicio?.tipoServicio != 'Kasnet'
          ? state.cobroServicio?.montoDeuda.toString()
          : state.montoDeudaServicio!.value,
        moraDeuda: state.cobroServicio?.moraDeuda,
        numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
        numeroRecibo: state.cobroServicio?.numeroRecibo,
        tipoPagoServicio: state.servicioPagar?.tipoPagoServicio,
      );

      state = state.copyWith(
        confirmarResponse: () => confirmarResponse,
      );
      ref.read(homeProvider.notifier).getCuentas();
      agregarOperacionFrecuente();
      ref.read(appRouterProvider).push('/pago-servicios/pago-exitoso');
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
      ref.read(timerProvider.notifier).cancelTimer();
      resetToken();
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  initTimer() {
    ref.read(timerProvider.notifier).initDateTimer(
          onFinish: () {
            resetToken();
          },
          initDate: state.pagarResponse?.fechaSistema,
          date: state.pagarResponse?.fechaVencimiento,
        );
  }

  changeSearchEmpresa(String searchEmpresa) {
    state = state.copyWith(
      searchEmpresa: searchEmpresa,
      statusBusqueda: StatusBusqueda.editando,
    );
  }

  changeNumeroSuministro(String numeroSuministro) {
    state = state.copyWith(
      cobroServicio: () => null,
      numeroSuministro: numeroSuministro,
    );
  }

  changeProducto(CuentaOrigenPagServ cuenta) {
    state = state.copyWith(
      cuentaOrigen: () => cuenta,
    );
  }

  reenviarComprobante() async {
    FocusManager.instance.primaryFocus?.unfocus();

    final correoElectronicoDestinatario =
        Email.dirty(state.correoElectronicoDestinatario.value);
    state = state.copyWith(
      correoElectronicoDestinatario: correoElectronicoDestinatario,
    );

    if (!Formz.validate([correoElectronicoDestinatario])) return;

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      await SharedService.reenviarComprobante(
        tipoOperacion: state.confirmarResponse?.tipoOperacion.toString(),
        correoElectronicoDestinatario:
            state.correoElectronicoDestinatario.value,
        idOperacionTts: state.confirmarResponse?.idOperacionTts,
      );

      ref.read(snackbarProvider.notifier).showSnackbar(
            'Correo enviado con éxito',
            SnackbarType.floating,
          );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  resetToken() {
    state = state.copyWith(
      tokenDigital: '',
    );
  }

  changeNombreOperacionFrecuente(String nombreOperacionFrecuente) {
    state = state.copyWith(
      nombreOperacionFrecuente: nombreOperacionFrecuente,
    );
  }

  toggleOperacionFrecuente() {
    state = state.copyWith(
      operacionFrecuente: !state.operacionFrecuente,
      nombreOperacionFrecuente: '',
    );
  }

  changeToken(String tokenDigital) {
    state = state.copyWith(
      tokenDigital: tokenDigital,
    );
  }

  changeCorreoDestinatario(Email correo) {
    state = state.copyWith(
      correoElectronicoDestinatario: correo,
    );
  }

  agregarOperacionFrecuente() async {
    try {
      if (!state.operacionFrecuente) return;
      await OperacionesFrecuentesService.agregarPagoServicios(
        numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
        nombreOperacionFrecuente: state.nombreOperacionFrecuente,
        codigoEmpresa: state.cobroServicio?.codigoEmpresa,
        codigoServicio: state.cobroServicio?.codigoServicio,
        tipoPagoServicio: state.servicioPagar?.tipoPagoServicio,
        codigoCategoria: state.servicioPagar?.codigoCategoria,
        codigoGrupoEmpresa: state.servicioPagar?.codigoGrupoEmpresa,
        nombreCategoria: state.servicioPagar?.nombreCategoria,
        nombreEmpresa: state.servicioPagar?.nombreEmpresa,
        nombreServicio: state.servicioPagar?.nombreServicio,
        suministro: state.numeroSuministro,
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  seleccionarOperacionFrecuente() async {
    initDatos();
    try {
      final operacionFrecuente =
          ref.read(operacionesFrecuentesProvider).operacionSeleccionada;

      if (operacionFrecuente == null) return;

      state = state.copyWith(
        servicioPagar: () => ServicioPagar(
          codigoEmpresa: operacionFrecuente
                  .operacionesFrecuenteDetalle.codigoEmpresaPagoServicio ??
              '',
          nombreEmpresa: operacionFrecuente
                  .operacionesFrecuenteDetalle.nombreEmpresaPagoServicio ??
              '',
          codigoServicio: operacionFrecuente
                  .operacionesFrecuenteDetalle.codigoServicioPagoServicio ??
              '',
          nombreServicio: operacionFrecuente
                  .operacionesFrecuenteDetalle.nombreServicioPagoServicio ??
              '',
          codigoCategoria: int.parse(operacionFrecuente
                  .operacionesFrecuenteDetalle.codigoCategoriaPagoServicio ??
              '0'),
          nombreCategoria: operacionFrecuente
                  .operacionesFrecuenteDetalle.nombreCategoriaPagoServicio ??
              '',
          codigoGrupoEmpresa: int.parse(operacionFrecuente
                  .operacionesFrecuenteDetalle.codigoGrupoEmpresaPagoServicio ??
              '0'),
          tipoPagoServicio: int.parse(operacionFrecuente
                  .operacionesFrecuenteDetalle.tipoPagoServicioPagoServicio ??
              ''),
        ),
        numeroSuministro: operacionFrecuente
            .operacionesFrecuenteDetalle.suministroPagoServicio,
      );

      await obtenerCobroServicio();

      if (state.cobroServicio == null) return;

      ref.read(appRouterProvider).push('/pago-servicios/pagar');
      await getDatosIniciales();

      final indexCuentaOrigen = state.cuentasOrigen.indexWhere(
          (cuenta) => cuenta.numeroProducto == operacionFrecuente.numeroCuenta);

      if (indexCuentaOrigen >= 0) {
        state = state.copyWith(
          cuentaOrigen: () => state.cuentasOrigen[indexCuentaOrigen],
        );
      }

      ref.read(operacionesFrecuentesProvider.notifier).resetOperacion();
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  ocultarCobro() {
    state = state.copyWith(
      mostrarCobro: false,
    );
  }

  changeMontoDedudaServicio(MontoDeudaServicio montoDeudaServicio) {
    state = state.copyWith(
      montoDeudaServicio: montoDeudaServicio,
    );
  }
}

enum StatusBusqueda { editando, mostrarResultados }

class PagoServiciosState {
  final List<CuentaOrigenPagServ> cuentasOrigen;
  final List<CategoriaPagServ> categorias;
  final List<ServicioPagar> ultimosPagos;
  final CuentaOrigenPagServ? cuentaOrigen;
  final CategoriaPagServ? categoriaSeleccionada;
  final List<Empresa> empresas;
  final String searchEmpresa;
  final Map<String, List<PagoServicio>> servicios;
  final ServicioPagar? servicioPagar;
  final String numeroSuministro;
  final ObtenerCobroServicioResponse? cobroServicio;
  final PagarResponse? pagarResponse;
  final String tokenDigital;
  final bool operacionFrecuente;
  final String nombreOperacionFrecuente;
  final Email correoElectronicoDestinatario;
  final ConfirmarResponse? confirmarResponse;
  final StatusBusqueda statusBusqueda;
  final bool mostrarCobro;
  final MontoDeudaServicio? montoDeudaServicio;

  PagoServiciosState(
      {this.cuentasOrigen = const [],
      this.categorias = const [],
      this.ultimosPagos = const [],
      this.cuentaOrigen,
      this.categoriaSeleccionada,
      this.empresas = const [],
      this.searchEmpresa = '',
      this.servicios = const {},
      this.servicioPagar,
      this.numeroSuministro = '',
      this.cobroServicio,
      this.pagarResponse,
      this.tokenDigital = '',
      this.operacionFrecuente = false,
      this.nombreOperacionFrecuente = '',
      this.correoElectronicoDestinatario = const Email.pure(''),
      this.confirmarResponse,
      this.statusBusqueda = StatusBusqueda.editando,
      this.mostrarCobro = false,
      this.montoDeudaServicio = const MontoDeudaServicio.pure('')});

  PagoServiciosState copyWith({
    List<CuentaOrigenPagServ>? cuentasOrigen,
    List<CategoriaPagServ>? categorias,
    List<ServicioPagar>? ultimosPagos,
    ValueGetter<CuentaOrigenPagServ?>? cuentaOrigen,
    ValueGetter<CategoriaPagServ?>? categoriaSeleccionada,
    List<Empresa>? empresas,
    String? searchEmpresa,
    Map<String, List<PagoServicio>>? servicios,
    ValueGetter<ServicioPagar?>? servicioPagar,
    String? numeroSuministro,
    ValueGetter<ObtenerCobroServicioResponse?>? cobroServicio,
    ValueGetter<PagarResponse?>? pagarResponse,
    String? tokenDigital,
    bool? operacionFrecuente,
    String? nombreOperacionFrecuente,
    Email? correoElectronicoDestinatario,
    ValueGetter<ConfirmarResponse?>? confirmarResponse,
    StatusBusqueda? statusBusqueda,
    bool? mostrarCobro,
    MontoDeudaServicio? montoDeudaServicio,
  }) =>
      PagoServiciosState(
          cuentasOrigen: cuentasOrigen ?? this.cuentasOrigen,
          categorias: categorias ?? this.categorias,
          ultimosPagos: ultimosPagos ?? this.ultimosPagos,
          cuentaOrigen:
              cuentaOrigen != null ? cuentaOrigen() : this.cuentaOrigen,
          categoriaSeleccionada: categoriaSeleccionada != null
              ? categoriaSeleccionada()
              : this.categoriaSeleccionada,
          empresas: empresas ?? this.empresas,
          searchEmpresa: searchEmpresa ?? this.searchEmpresa,
          servicios: servicios ?? this.servicios,
          servicioPagar:
              servicioPagar != null ? servicioPagar() : this.servicioPagar,
          numeroSuministro: numeroSuministro ?? this.numeroSuministro,
          cobroServicio:
              cobroServicio != null ? cobroServicio() : this.cobroServicio,
          pagarResponse:
              pagarResponse != null ? pagarResponse() : this.pagarResponse,
          tokenDigital: tokenDigital ?? this.tokenDigital,
          operacionFrecuente: operacionFrecuente ?? this.operacionFrecuente,
          nombreOperacionFrecuente:
              nombreOperacionFrecuente ?? this.nombreOperacionFrecuente,
          correoElectronicoDestinatario: correoElectronicoDestinatario ??
              this.correoElectronicoDestinatario,
          confirmarResponse: confirmarResponse != null
              ? confirmarResponse()
              : this.confirmarResponse,
          statusBusqueda: statusBusqueda ?? this.statusBusqueda,
          mostrarCobro: mostrarCobro ?? this.mostrarCobro,
          montoDeudaServicio: montoDeudaServicio ?? this.montoDeudaServicio);
}
