import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/constants/tipo_operacion_reenvio.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/providers/operaciones_frecuentes_provider.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/services/operaciones_frecuentes_service.dart';
import 'package:caja_tacna_app/features/pago_creditos/models/credito_abonar.dart';
import 'package:caja_tacna_app/features/pago_creditos/models/opcion_pago_credito.dart';
import 'package:caja_tacna_app/features/pago_creditos/models/producto_debito.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/models/index.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/services/pago_creditos_propios_service.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/widgets/dialog_monto.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_terceros/providers/pago_creditos_terceros_provider.dart';
import 'package:caja_tacna_app/features/pago_creditos/providers/pago_creditos_anticipo_provider.dart';
import 'package:caja_tacna_app/features/shared/inputs/email.dart';
import 'package:caja_tacna_app/features/shared/inputs/monto_abonar.dart';
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

final pagoCreditoPropioProvider =
    NotifierProvider<PagoCreditoPropioNotifier, PagoCreditoPropioState>(
        () => PagoCreditoPropioNotifier());

class PagoCreditoPropioNotifier extends Notifier<PagoCreditoPropioState> {
  @override
  PagoCreditoPropioState build() {
    return PagoCreditoPropioState();
  }

  initDatosMenu() {
    state = state.copyWith(
      creditos: [],
    );
    initDatosCuenta();
  }

  //reinicializacion de datos para la vista pagar_screen
  initDatosCuenta() {
    state = state.copyWith(
        cuentaOrigen: () => null,
        creditoAbonar: () => null,
        monto: const MontoAbonar.pure(''),
        tokenDigital: '',
        correoElectronicoDestinatario: const Email.pure(''),
        confirmarAppResponse: () => null,
        pagarAppResponse: () => null,
        generarCipResponse: () => null,
        nombreOperacionFrecuente: '',
        operacionFrecuente: false,
        tipoPagoCredito: TipoPagoCredito.abono,
        tipoAnticipo: TipoAnticipo.defecto);
    ref.read(pagoCreditoAnticipoProvider.notifier).initDatos();
  }

  getDatosIniciales() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final DatosInicialesResponse datosInicialesResponse =
          await PagoCreditosPropiosService.obtenerDatosIniciales();

      state = state.copyWith(
        creditos: datosInicialesResponse.creditosAbonar,
        cuentasOrigen: datosInicialesResponse.productosDebito,
      );
    } on ServiceException catch (e) {
      ref.read(appRouterProvider).pop();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  setCreditoAbonar(
    CreditoAbonar creditoAbonar,
  ) {
    initDatosCuenta();
    state = state.copyWith(
      creditoAbonar: () => creditoAbonar,
      monto: state.monto.copyWith(
        montoCuota: creditoAbonar.montoTotalPago,
        simboloMoneda: creditoAbonar.simboloMoneda,
        value: CtUtils.formatStringWithTwoDecimals(
          creditoAbonar.montoTotalPago.toString(),
        ),
      ),
    );
    ref.read(pagoCreditoAnticipoProvider.notifier).setMontoAnticipo(
          creditoAbonar,
        );
    ref.read(appRouterProvider).push('/pago-creditos/creditos-propios/pagar');
    autoCompletarOpFrecuente();
  }

  autoCompletarOpFrecuente() {
    try {
      final operacionFrecuente =
          ref.read(operacionesFrecuentesProvider).operacionSeleccionada;

      if (operacionFrecuente == null) return;

      final indexCuentaOrigen = state.cuentasOrigen.indexWhere(
          (cuenta) => cuenta.numeroProducto == operacionFrecuente.numeroCuenta);

      if (indexCuentaOrigen >= 0) {
        state = state.copyWith(
          cuentaOrigen: () => state.cuentasOrigen[indexCuentaOrigen],
        );
      }

      ref.read(operacionesFrecuentesProvider.notifier).resetOperacion();
    } catch (e) {
      throw ServiceException('Ocurrió un error al cargar la operación');
    }
  }

  pagar({required BuildContext context, required bool withPush}) async {
    if (state.cuentaOrigen == null && state.tipoPago == TipoPago.aplicativo) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione la cuenta de origen', SnackbarType.error);
      return;
    }

    if (!state.tipoPagoCredito.esAnticipo()) {
      final monto =
          state.monto.copyWith(value: state.monto.value, isPure: false);

      state = state.copyWith(
        monto: monto,
      );

      if (!Formz.validate([monto])) return;

      bool? continuar = true;
      //mostrar modal de confirmacion en caso de que el monto especificado sea menor al monto
      //que se debe abonar.
      if (double.parse(monto.value) < state.creditoAbonar!.montoTotalPago &&
          withPush &&
          !state.tipoPagoCredito.esAnticipo()) {
        continuar = await showDialog(
          context: context,
          builder: (BuildContext context) {
            return const DialogMonto();
          },
        );
      }

      if (continuar == null || !continuar) return;
    } else {
      if (!ref
          .read(pagoCreditoAnticipoProvider.notifier)
          .validarMontoAnticipo()) return;
    }

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    if (!context.mounted) return;
    if (state.tipoPago == TipoPago.aplicativo) {
      await pagarApp(withPush: withPush);
    } else {
      await generarCip();
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  pagarApp({required bool withPush}) async {
    resetToken();
    try {
      if (!state.tipoPagoCredito.esAnticipo()) {
        final pagarAppResponse = await PagoCreditosPropiosService.pagar(
          numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
          numeroCreditoDestino: state.creditoAbonar?.numeroCredito,
          monto: state.monto.value,
          cancelacionCredito:
              state.tipoPagoCredito == TipoPagoCredito.cancelacion,
          codigoMonedaOrigen: state.cuentaOrigen?.codigoMonedaProducto,
          codigoMonedaDestino: state.creditoAbonar?.codigoMoneda,
          identificadorDispositivo: ref
              .read(dispositivoProvider.notifier)
              .getIdentificadorDispositivo(),
        );

        state = state.copyWith(
          pagarAppResponse: () => pagarAppResponse,
          tokenDigital: await CoreService.desencriptarToken(
            pagarAppResponse.codigoSolicitado,
          ),
        );
      } else {
        final pagarAppAnticipoResponse = await ref
            .read(pagoCreditoAnticipoProvider.notifier)
            .pagarAnticipo(
                numeroProducto: state.cuentaOrigen?.numeroProducto,
                numeroCredito: state.creditoAbonar?.numeroCredito,
                tipoPago: state.tipoPagoCredito,
                tipoAnticipo: state.tipoAnticipo,
                tipoSolicitante: TipoSolicitante.titular.obtenerIdentificador(),
                codigoMonedaProducto: state.cuentaOrigen?.codigoMonedaProducto,
                withPush: withPush);

        state = state.copyWith(
          pagarAppResponse: () => pagarAppAnticipoResponse.datosTokenDigital,
          tokenDigital: await CoreService.desencriptarToken(
            pagarAppAnticipoResponse.datosTokenDigital.codigoSolicitado,
          ),
        );
      }

      initTimer();
      if (withPush) {
        ref.read(appRouterProvider).push(
          '/pago-creditos/creditos-propios/confirmar',
        );
      }
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  generarCip() async {
    try {
      final generarCipResponse = await PagoCreditosPropiosService.generarCip(
        monto: state.monto.value,
        cancelacionCredito:
            state.tipoPagoCredito == TipoPagoCredito.cancelacion,
        email: ref.read(homeProvider).datosCliente?.correoElectronico,
        numeroCredito: state.creditoAbonar?.numeroCredito,
        numeroCuota: state.creditoAbonar?.cuotaActual,
        numeroTelefono: ref.read(homeProvider).datosCliente?.numeroTelefonoSms,
      );

      state = state.copyWith(
        generarCipResponse: () => generarCipResponse,
      );

      ref.read(appRouterProvider).push(
        '/pago-creditos/creditos-propios/pago-exitoso-cip',
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  confirmar() async {
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
      if (!state.tipoPagoCredito.esAnticipo()) {
        final confirmarAppResponse = await PagoCreditosPropiosService.confirmar(
          numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
          numeroCreditoDestino: state.creditoAbonar?.numeroCredito,
          monto: state.monto.value,
          cancelacionCredito:
              state.tipoPagoCredito == TipoPagoCredito.cancelacion,
          codigoMonedaOrigen: state.cuentaOrigen?.codigoMonedaProducto,
          codigoMonedaDestino: state.creditoAbonar?.codigoMoneda,
          tokenDigital: state.tokenDigital,
        );

        state = state.copyWith(
          confirmarAppResponse: () => confirmarAppResponse,
        );
        ref.read(homeProvider.notifier).getCreditos();
        ref.read(homeProvider.notifier).getCuentas();
        agregarOperacionFrecuente();
      } else {
        var confirmarAppResponse = await ref
            .read(pagoCreditoAnticipoProvider.notifier)
            .confirmarAnticipo(
              numeroProducto: state.cuentaOrigen?.numeroProducto,
              numeroCredito: state.creditoAbonar?.numeroCredito,
              tipoAnticipo: state.tipoAnticipo,
              tipoPago: state.tipoPagoCredito,
              tipoSolicitante: TipoSolicitante.titular.obtenerIdentificador(),
              codigoMonedaProducto: state.cuentaOrigen?.codigoMonedaProducto,
              codigoAutorizacion: state.tokenDigital,
            );

        state = state.copyWith(
          confirmarAppResponse: () => confirmarAppResponse,
        );
      }

      ref.read(appRouterProvider).push(
        '/pago-creditos/creditos-propios/pago-exitoso-app',
      );
    } on ServiceException catch (e) {
      resetToken();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
      ref.read(timerProvider.notifier).cancelTimer();
    }
    ref.read(loaderProvider.notifier).dismissLoader();
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
          tipoOperacion: state.tipoPagoCredito.esAnticipo()
              ? TipoOperacionReenvio.anticipoPagoCredito
              : TipoOperacionReenvio.pagoCredito,
          correoElectronicoDestinatario:
              state.correoElectronicoDestinatario.value,
          idOperacionTts: state.confirmarAppResponse?.idOperacionTts,
          codigoTipoAnticipo: state.tipoAnticipo.obtenerIdentificador(),
          codigoTipoPago: state.tipoPagoCredito.obtenerIdentificador(),
          codigoTipoSolicitante:
              TipoSolicitante.titular.obtenerIdentificador());

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

  initTimer() {
    ref.read(timerProvider.notifier).initDateTimer(
          onFinish: () {
            resetToken();
          },
          initDate: state.pagarAppResponse?.fechaSistema,
          date: state.pagarAppResponse?.fechaVencimiento,
        );
  }

  setTipoPago(TipoPago tipoPago) {
    state = state.copyWith(
      tipoPago: tipoPago,
    );
  }

  changeCuentaOrigen(ProductoDebito producto) {
    state = state.copyWith(
      cuentaOrigen: () => producto,
    );
  }

  changeMonto(MontoAbonar monto) {
    state = state.copyWith(
      monto: monto,
    );
  }

  resetToken() {
    state = state.copyWith(
      tokenDigital: '',
    );
  }

  changeTipoPagoCredito(TipoPagoCredito opcionPagoCredito) {
    state = state.copyWith(
      tipoAnticipo: null,
      tipoPagoCredito: opcionPagoCredito,
      monto: opcionPagoCredito == TipoPagoCredito.cancelacion
          ? state.monto.copyWith(
              value: state.creditoAbonar?.montoSaldoCancelacion.toString(),
              montoCuota: state.creditoAbonar?.montoSaldoCancelacion,
              isPure: true,
            )
          : state.monto.copyWith(
              value: CtUtils.formatStringWithTwoDecimals(
                state.creditoAbonar?.montoTotalPago.toString() ?? '',
              ),
              montoCuota: state.creditoAbonar?.montoTotalPago,
              isPure: true,
            ),
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
      await OperacionesFrecuentesService.agregarPagoCredito(
        numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
        nombreOperacionFrecuente: state.nombreOperacionFrecuente,
        numeroCredito: state.creditoAbonar?.numeroCredito,
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  toggleOperacionFrecuente() {
    state = state.copyWith(
      operacionFrecuente: !state.operacionFrecuente,
      nombreOperacionFrecuente: '',
    );
  }

  changeNombreOperacionFrecuente(String nombreOperacionFrecuente) {
    state = state.copyWith(
      nombreOperacionFrecuente: nombreOperacionFrecuente,
    );
  }

  changeReduccionAnticipo(TipoAnticipo tipoAnticipo) {
    state = state.copyWith(tipoAnticipo: tipoAnticipo);
  }
}

class PagoCreditoPropioState {
  final List<CreditoAbonar> creditos;
  final List<ProductoDebito> cuentasOrigen;
  final ProductoDebito? cuentaOrigen;
  final CreditoAbonar? creditoAbonar;
  final MontoAbonar monto;
  final String tokenDigital;
  final TipoPago tipoPago;
  final Email correoElectronicoDestinatario;
  final PagarAppResponse? pagarAppResponse;
  final ConfirmarAppResponse? confirmarAppResponse;
  final GenerarCipResponse? generarCipResponse;
  final bool operacionFrecuente;
  final String nombreOperacionFrecuente;
  final TipoPagoCredito tipoPagoCredito;
  final TipoAnticipo tipoAnticipo;

  PagoCreditoPropioState({
    this.creditos = const [],
    this.cuentasOrigen = const [],
    this.cuentaOrigen,
    this.creditoAbonar,
    this.monto = const MontoAbonar.pure(''),
    this.tokenDigital = '',
    this.tipoPago = TipoPago.aplicativo,
    this.correoElectronicoDestinatario = const Email.pure(''),
    this.pagarAppResponse,
    this.confirmarAppResponse,
    this.generarCipResponse,
    this.operacionFrecuente = false,
    this.nombreOperacionFrecuente = '',
    this.tipoPagoCredito = TipoPagoCredito.abono,
    this.tipoAnticipo = TipoAnticipo.defecto,
  });

  PagoCreditoPropioState copyWith({
    List<CreditoAbonar>? creditos,
    List<ProductoDebito>? cuentasOrigen,
    ValueGetter<ProductoDebito?>? cuentaOrigen,
    ValueGetter<CreditoAbonar?>? creditoAbonar,
    MontoAbonar? monto,
    String? tokenDigital,
    TipoPago? tipoPago,
    Email? correoElectronicoDestinatario,
    ValueGetter<PagarAppResponse?>? pagarAppResponse,
    ValueGetter<ConfirmarAppResponse?>? confirmarAppResponse,
    ValueGetter<GenerarCipResponse?>? generarCipResponse,
    bool? operacionFrecuente,
    String? nombreOperacionFrecuente,
    TipoPagoCredito? tipoPagoCredito,
    TipoAnticipo? tipoAnticipo,
  }) =>
      PagoCreditoPropioState(
          creditos: creditos ?? this.creditos,
          cuentasOrigen: cuentasOrigen ?? this.cuentasOrigen,
          cuentaOrigen:
              cuentaOrigen != null ? cuentaOrigen() : this.cuentaOrigen,
          creditoAbonar:
              creditoAbonar != null ? creditoAbonar() : this.creditoAbonar,
          monto: monto ?? this.monto,
          tokenDigital: tokenDigital ?? this.tokenDigital,
          tipoPago: tipoPago ?? this.tipoPago,
          correoElectronicoDestinatario: correoElectronicoDestinatario ??
              this.correoElectronicoDestinatario,
          pagarAppResponse: pagarAppResponse != null
              ? pagarAppResponse()
              : this.pagarAppResponse,
          confirmarAppResponse: confirmarAppResponse != null
              ? confirmarAppResponse()
              : this.confirmarAppResponse,
          generarCipResponse: generarCipResponse != null
              ? generarCipResponse()
              : this.generarCipResponse,
          operacionFrecuente: operacionFrecuente ?? this.operacionFrecuente,
          nombreOperacionFrecuente:
              nombreOperacionFrecuente ?? this.nombreOperacionFrecuente,
          tipoPagoCredito: tipoPagoCredito ?? this.tipoPagoCredito,
          tipoAnticipo: tipoAnticipo ?? this.tipoAnticipo);
}
