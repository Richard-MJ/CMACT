import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/features/adelanto_sueldo/inputs/monto_adelanto.dart';
import 'package:caja_tacna_app/features/adelanto_sueldo/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/adelanto_sueldo/models/cuenta_destino.dart';
import 'package:caja_tacna_app/features/adelanto_sueldo/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/adelanto_sueldo/models/pagar_response.dart';
import 'package:caja_tacna_app/features/adelanto_sueldo/services/adelanto_sueldo_service.dart';
import 'package:caja_tacna_app/features/adelanto_sueldo/states/adelanto_sueldo_state.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:formz/formz.dart';

final adelantoSueldoProvider =
    NotifierProvider<AdelantoSueldoNotifier, AdelantoSueldoState>(
        () => AdelantoSueldoNotifier());

class AdelantoSueldoNotifier extends Notifier<AdelantoSueldoState> {
  @override
  AdelantoSueldoState build() {
    return AdelantoSueldoState();
  }

  final api = Api();

  initDatos() {
    state = state.copyWith(
      cuentasDestino: [],
      cuentaDestino: () => null,
      confirmarResponse: () => null,
      pagarResponse: () => null,
      tokenDigital: '',
      monto: const MontoAdelanto.pure(''),
    );
  }

  getDatosIniciales() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final DatosInicialesResponse datosInicialesResponse =
          await AdelantoSueldoService.obtenerDatosIniciales();

      state = state.copyWith(
        cuentasDestino: datosInicialesResponse.productos,
        cuentaDestino: () => datosInicialesResponse.productos[0],
      );
    } on ServiceException catch (e) {
      ref.read(appRouterProvider).pop();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  pagar({required bool withPush}) async {
    FocusManager.instance.primaryFocus?.unfocus();

    if (state.cuentaDestino == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione la cuenta de destino', SnackbarType.error);
      return;
    }

    final monto = state.monto.copyWith(isPure: false);

    state = state.copyWith(
      monto: monto,
    );

    if (!Formz.validate([monto])) return;
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    resetToken();
    try {
      final PagarResponse pagarResponse = await AdelantoSueldoService.pagar(
        numeroCuentaDestino: state.cuentaDestino?.numeroProducto,
        codigoMonedaOperacion: state.cuentaDestino?.codigoMoneda,
        montoOperacion: state.monto.value,
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
        ref.read(appRouterProvider).push('/adelanto-sueldo/confirmar');
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
          await AdelantoSueldoService.confirmar(
        tokenDigital: state.tokenDigital,
        numeroCuentaDestino: state.cuentaDestino?.numeroProducto,
        montoOperacion: state.monto.value,
      );

      state = state.copyWith(
        confirmarResponse: () => confirmarResponse,
      );
      ref.read(homeProvider.notifier).getCuentas();
      ref.read(appRouterProvider).push('/adelanto-sueldo/pago-exitoso');
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

  changeProducto(CuentaDestinoAdelSuel cuenta) {
    state = state.copyWith(
      cuentaDestino: () => cuenta,
      monto: state.monto.copyWith(
        montoMaximo: cuenta.montoMaximo,
      ),
    );
  }

  changeMonto(MontoAdelanto monto) {
    state = state.copyWith(
      monto: monto,
    );
  }

  resetToken() {
    state = state.copyWith(
      tokenDigital: '',
    );
  }

  changeToken(String tokenDigital) {
    state = state.copyWith(
      tokenDigital: tokenDigital,
    );
  }
}
