import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/features/anulacion_tarjetas/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/anulacion_tarjetas/services/anulacion_tarjetas_service.dart';
import 'package:caja_tacna_app/features/anulacion_tarjetas/states/anulacion_tarjetas_state.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/auth_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/services/storage_service.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

final anulacionTarjetasProvider =
    NotifierProvider<AnulacionTarjetasNotifier, AnulacionTarjetasState>(
        () => AnulacionTarjetasNotifier());

class AnulacionTarjetasNotifier extends Notifier<AnulacionTarjetasState> {
  @override
  AnulacionTarjetasState build() {
    return AnulacionTarjetasState();
  }

  final storageService = StorageService();

  initDatos() {
    state = state.copyWith(
      tarjetas: [],
      tarjeta: () => null,
      motivos: [],
      motivo: () => null,
      tokenDigital: '',
      confirmarResponse: () => null,
      anularResponse: () => null,
    );
  }

  getDatosIniciales() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final datosInicialesResponse =
          await AnulacionTarjetasService.obtenerDatosIniciales();

      state = state.copyWith(
        tarjetas: datosInicialesResponse.tarjetasAnulacion,
        motivos: datosInicialesResponse.motivosAnulacionTarjeta,
      );
    } on ServiceException catch (e) {
      ref.read(appRouterProvider).pop();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  changeTarjeta(TarjetaAnulacion tarjeta) {
    state = state.copyWith(
      tarjeta: () => tarjeta,
    );
  }

  changeMotivo(MotivoAnulacionTarjeta motivo) {
    state = state.copyWith(
      motivo: () => motivo,
    );
  }

  anular({required bool withPush}) async {
    FocusManager.instance.primaryFocus?.unfocus();

    if (state.tarjeta == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione la tarjeta', SnackbarType.error);
      return;
    }

    if (state.motivo == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione el motivo', SnackbarType.error);
      return;
    }

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    resetToken();
    try {
      final anularResponse = await AnulacionTarjetasService.anular(
        numeroTarjeta: state.tarjeta?.numeroTarjeta,
        identificadorDispositivo: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),
      );

      state = state.copyWith(
        anularResponse: () => anularResponse,
        tokenDigital: await CoreService.desencriptarToken(
          anularResponse.codigoSolicitado,
        ),
      );
      initTimer();
      if (withPush) {
        ref.read(appRouterProvider).push('/anulacion-tarjetas/confirmar');
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
      final confirmarResponse = await AnulacionTarjetasService.confirmar(
        tokenDigital: state.tokenDigital,
        numeroTarjeta: state.tarjeta?.numeroTarjeta,
        codigoMotivoAnulacion: state.motivo?.codigoMotivo,
      );

      state = state.copyWith(
        confirmarResponse: () => confirmarResponse,
      );
      await ref.read(authProvider.notifier).logout();
      ref.read(appRouterProvider).push('/anulacion-tarjetas/anulacion-exitosa');
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
          initDate: state.anularResponse?.fechaSistema,
          date: state.anularResponse?.fechaVencimiento,
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
