import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/pago_safetypay/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/pago_safetypay/models/cuenta_origen.dart';
import 'package:caja_tacna_app/features/pago_safetypay/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/pago_safetypay/models/deuda.dart';
import 'package:caja_tacna_app/features/pago_safetypay/models/pagar_response.dart';
import 'package:caja_tacna_app/features/pago_safetypay/services/pago_safetypay_service.dart';
import 'package:caja_tacna_app/features/shared/inputs/email.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/services/shared_services.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:formz/formz.dart';

final pagoSafetypayProvider =
    StateNotifierProvider<PagoSafetypayNotifier, PagoSafetypayState>((ref) {
  return PagoSafetypayNotifier(ref);
});

class PagoSafetypayNotifier extends StateNotifier<PagoSafetypayState> {
  PagoSafetypayNotifier(this.ref) : super(PagoSafetypayState());

  final Ref ref;

  initDatos() {
    state = state.copyWith(
      cuentasOrigen: [],
      cuentaOrigen: () => null,
      confirmarResponse: () => null,
      correoElectronicoDestinatario: const Email.pure(''),
      nombreOperacionFrecuente: '',
      operacionFrecuente: false,
      pagarResponse: () => null,
      tokenDigital: '',
      codigoPago: '',
      deuda: () => null,
    );
  }

  getDatosIniciales() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final DatosInicialesResponse datosInicialesResponse =
          await PagoSafetypayService.obtenerDatosIniciales();

      state = state.copyWith(
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

  obtenerDeuda() async {
    if (state.codigoPago.isEmpty) {
      return;
    }

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final Deuda deuda = await PagoSafetypayService.obtenerDeuda(
        codigoPago: state.codigoPago,
      );

      state = state.copyWith(
        deuda: () => deuda,
      );
      FocusManager.instance.primaryFocus?.unfocus();
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  pagar({required bool withPush}) async {
    if (state.cuentaOrigen == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione la cuenta de origen', SnackbarType.error);
      return;
    }
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    resetToken();
    try {
      final PagarResponse pagarResponse = await PagoSafetypayService.pagar(
        codigoTransaccion: state.deuda?.codigoPago,
        codigoMonedaOperacion: state.deuda?.codigoMoneda,
        montoOperacion: state.deuda?.monto,
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
        ref.read(appRouterProvider).push('/pago-safetypay/confirmar');
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
          await PagoSafetypayService.confirmar(
        tokenDigital: state.tokenDigital,
        numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
        codigoMonedaOperacion: state.deuda?.codigoMoneda,
        codigoTransaccion: state.deuda?.codigoPago,
        montoOperacion: state.deuda?.monto,
      );

      state = state.copyWith(
        confirmarResponse: () => confirmarResponse,
      );
      ref.read(homeProvider.notifier).getCuentas();
      ref.read(appRouterProvider).push('/pago-safetypay/pago-exitoso');
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

  changeCodigoPago(String codigoPago) {
    state = state.copyWith(
      deuda: () => null,
      codigoPago: codigoPago,
    );
  }

  changeProducto(CuentaOrigenSafety cuenta) {
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
        tipoOperacion: "9",
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
}

class PagoSafetypayState {
  final List<CuentaOrigenSafety> cuentasOrigen;
  final CuentaOrigenSafety? cuentaOrigen;
  final String codigoPago;
  final Deuda? deuda;
  final PagarResponse? pagarResponse;
  final String tokenDigital;
  final bool operacionFrecuente;
  final String nombreOperacionFrecuente;
  final Email correoElectronicoDestinatario;
  final ConfirmarResponse? confirmarResponse;

  PagoSafetypayState({
    this.cuentasOrigen = const [],
    this.cuentaOrigen,
    this.codigoPago = '',
    this.deuda,
    this.pagarResponse,
    this.tokenDigital = '',
    this.operacionFrecuente = false,
    this.nombreOperacionFrecuente = '',
    this.correoElectronicoDestinatario = const Email.pure(''),
    this.confirmarResponse,
  });

  PagoSafetypayState copyWith({
    List<CuentaOrigenSafety>? cuentasOrigen,
    ValueGetter<CuentaOrigenSafety?>? cuentaOrigen,
    String? codigoPago,
    ValueGetter<Deuda?>? deuda,
    ValueGetter<PagarResponse?>? pagarResponse,
    String? tokenDigital,
    bool? operacionFrecuente,
    String? nombreOperacionFrecuente,
    Email? correoElectronicoDestinatario,
    ValueGetter<ConfirmarResponse?>? confirmarResponse,
  }) =>
      PagoSafetypayState(
        cuentasOrigen: cuentasOrigen ?? this.cuentasOrigen,
        cuentaOrigen: cuentaOrigen != null ? cuentaOrigen() : this.cuentaOrigen,
        codigoPago: codigoPago ?? this.codigoPago,
        deuda: deuda != null ? deuda() : this.deuda,
        pagarResponse:
            pagarResponse != null ? pagarResponse() : this.pagarResponse,
        tokenDigital: tokenDigital ?? this.tokenDigital,
        operacionFrecuente: operacionFrecuente ?? this.operacionFrecuente,
        nombreOperacionFrecuente:
            nombreOperacionFrecuente ?? this.nombreOperacionFrecuente,
        correoElectronicoDestinatario:
            correoElectronicoDestinatario ?? this.correoElectronicoDestinatario,
        confirmarResponse: confirmarResponse != null
            ? confirmarResponse()
            : this.confirmarResponse,
      );
}
