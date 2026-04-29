import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/constants/storage_keys.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/features/biometria/models/password_biometrico.dart';
import 'package:caja_tacna_app/features/biometria/services/biometria_service.dart';
import 'package:caja_tacna_app/features/cambio_clave/models/cambiar_clave_response.dart';
import 'package:caja_tacna_app/features/cambio_clave/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/cambio_clave/services/cambio_clave_service.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/providers/login_provider.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:caja_tacna_app/features/shared/services/storage_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

final cambioClaveProvider =
    StateNotifierProvider<CambioClaveNotifier, CambioClaveState>((ref) {
  return CambioClaveNotifier(ref);
});

class CambioClaveNotifier extends StateNotifier<CambioClaveState> {
  CambioClaveNotifier(this.ref) : super(CambioClaveState());

  final Ref ref;

  initDatos() {
    state = state.copyWith(
      passwordAntiguo: '',
      passwordNuevo: '',
      tokenDigital: '',
      cambiarClaveResponse: () => null,
      confirmarResponse: () => null,
      aceptoTerminos: false,
      passwordNuevoConfirm: '',
    );
  }

  crearClaveNueva() {
    ref.read(appRouterProvider).push('/cambio-clave/clave-nueva');
  }

  confirmarClaveNueva() {
    ref.read(appRouterProvider).push('/cambio-clave/confirmar-clave-nueva');
  }

  cambiarClave({required bool withPush}) async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    resetToken();
    try {
      final response = await CambioClaveService.cambiarClave(
        identificadorDispositivo: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),
      );

      state = state.copyWith(
        cambiarClaveResponse: () => response,
        tokenDigital: await CoreService.desencriptarToken(
          response.codigoSolicitado,
        ),
      );
      initTimer();

      if (withPush) {
        ref.read(appRouterProvider).push('/cambio-clave/confirmar');
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
    if (!state.aceptoTerminos) {
      ref.read(snackbarProvider.notifier).showSnackbar(
          'Acepte los terminos y condiciones.', SnackbarType.error);
      return;
    }
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final confirmarResponse = await CambioClaveService.confirmar(
        tokenDigital: state.tokenDigital,
        idTipoDocumento: ref.read(loginProvider).documento?.idTipoDocumento,
        numeroDocumento: ref.read(loginProvider).numeroDocumento.value,
        passwordAntiguo: state.passwordAntiguo,
        passwordNuevo: state.passwordNuevo,
      );

      //eliminamos el password de storage
      final List<PasswordBiometrico> passwordsBiometricos =
          await BiometriaService.getPasswordsBiometricos();
      final List<PasswordBiometrico> nuevosPasswordsBiometricos =
          passwordsBiometricos
              .where((p) =>
                  p.numeroTarjeta !=
                  ref.read(loginProvider).numeroTarjeta.value)
              .toList();

      await StorageService().set<List<dynamic>>(
          StorageKeys.passwordsBiometricos, nuevosPasswordsBiometricos);

      state = state.copyWith(
        confirmarResponse: () => confirmarResponse,
      );
      ref.read(homeProvider.notifier).getCuentas();
      ref.read(appRouterProvider).push('/cambio-clave/cambio-exitoso');
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
          initDate: state.cambiarClaveResponse?.fechaSistema,
          date: state.cambiarClaveResponse?.fechaVencimiento,
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

  changePasswordAntiguo(String passwordAntiguo) {
    state = state.copyWith(
      passwordAntiguo: passwordAntiguo,
    );
  }

  changePasswordNuevo(String passwordNuevo) {
    state = state.copyWith(
      passwordNuevo: passwordNuevo,
    );
  }

  changePasswordNuevoConfirm(String passwordNuevoConfirm) {
    state = state.copyWith(
      passwordNuevoConfirm: passwordNuevoConfirm,
    );
  }

  toggleAceptarTerminos() {
    state = state.copyWith(
      aceptoTerminos: !state.aceptoTerminos,
    );
  }
}

class CambioClaveState {
  final String passwordAntiguo;
  final String passwordNuevo;
  final String passwordNuevoConfirm;
  final bool aceptoTerminos;
  final String tokenDigital;
  final CambiarClaveResponse? cambiarClaveResponse;
  final ConfirmarResponse? confirmarResponse;

  CambioClaveState({
    this.passwordAntiguo = '',
    this.passwordNuevo = '',
    this.passwordNuevoConfirm = '',
    this.tokenDigital = '',
    this.aceptoTerminos = false,
    this.cambiarClaveResponse,
    this.confirmarResponse,
  });

  CambioClaveState copyWith({
    String? passwordAntiguo,
    String? passwordNuevo,
    String? passwordNuevoConfirm,
    String? tokenDigital,
    bool? aceptoTerminos,
    ValueGetter<CambiarClaveResponse?>? cambiarClaveResponse,
    ValueGetter<ConfirmarResponse?>? confirmarResponse,
  }) =>
      CambioClaveState(
        passwordAntiguo: passwordAntiguo ?? this.passwordAntiguo,
        passwordNuevo: passwordNuevo ?? this.passwordNuevo,
        passwordNuevoConfirm: passwordNuevoConfirm ?? this.passwordNuevoConfirm,
        tokenDigital: tokenDigital ?? this.tokenDigital,
        aceptoTerminos: aceptoTerminos ?? this.aceptoTerminos,
        cambiarClaveResponse: cambiarClaveResponse != null
            ? cambiarClaveResponse()
            : this.cambiarClaveResponse,
        confirmarResponse: confirmarResponse != null
            ? confirmarResponse()
            : this.confirmarResponse,
      );
}
