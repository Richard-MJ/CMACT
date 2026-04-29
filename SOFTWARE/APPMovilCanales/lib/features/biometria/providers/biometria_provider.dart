import 'package:caja_tacna_app/config/plugins/local_auth_plugin.dart';
import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/constants/storage_keys.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/features/biometria/models/afiliacion_biometrica.dart';
import 'package:caja_tacna_app/features/biometria/models/afiliar_response.dart';
import 'package:caja_tacna_app/features/biometria/models/confirmar_afiliacion_response.dart';
import 'package:caja_tacna_app/features/biometria/models/confirmar_desafiliacion_response.dart';
import 'package:caja_tacna_app/features/biometria/models/password_biometrico.dart';
import 'package:caja_tacna_app/features/biometria/services/biometria_service.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/providers/login_provider.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/services/storage_service.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:local_auth/local_auth.dart';
import 'package:caja_tacna_app/config/plugins/biometric_storage_plugin.dart';

final biometriaProvider =
    StateNotifierProvider<BiometriaNotifier, BiometriaState>((ref) {
  return BiometriaNotifier(ref);
});

class BiometriaNotifier extends StateNotifier<BiometriaState> {
  BiometriaNotifier(this.ref) : super(BiometriaState());

  final Ref ref;

  initDatos() {
    state = state.copyWith(
      faceId: false,
      touchId: false,
      aceptoTerminos: false,
      afiliacion: () => null,
      afiliarResponse: () => null,
      confirmarAfiliacionResponse: () => null,
      confirmarDesafiliacionResponse: () => null,
      dispositivoConBiometricos: false,
      tokenDigital: '',
    );
  }

  verificarBiometricos() async {
    final dispositivoConBiometricos =
        await LocalAuthPlugin.dispositivoConBiometricos();
    state = state.copyWith(
      dispositivoConBiometricos: dispositivoConBiometricos,
    );

    final availableBiometrics = await LocalAuthPlugin.availableBiometrics();

    if (availableBiometrics.contains(BiometricType.strong) ||
        availableBiometrics.contains(BiometricType.fingerprint)) {
      state = state.copyWith(
        touchId: true,
      );
    }

    if (availableBiometrics.contains(BiometricType.face)) {
      state = state.copyWith(
        faceId: true,
      );
    }
  }

  obtenerAfiliaciones({required int codigoTipoBiometria}) async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    state = state.copyWith(
      afiliacion: () => null,
      switchCard: false,
      codigoTipoBiometria: codigoTipoBiometria,
      afiliarResponse: () => null,
      confirmarAfiliacionResponse: () => null,
      confirmarDesafiliacionResponse: () => null,
      tokenDigital: '',
      aceptoTerminos: false,
    );

    try {
      final afiliaciones = await BiometriaService.obtenerAfiliaciones(
        identificadorDispositivo: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),
      );
      int index = afiliaciones.indexWhere(
        (afiliacion) => afiliacion.codigoTipoBiometria == codigoTipoBiometria,
      );

      PasswordBiometrico? passwordBiometrico =
          await BiometriaService.getPasswordBiometrico(
              ref.read(loginProvider).numeroTarjeta.value);

      if (index == -1 || passwordBiometrico == null) {
        state = state.copyWith(
          afiliacion: () => null,
          switchCard: false,
        );
      } else {
        state = state.copyWith(
          afiliacion: () => afiliaciones[index],
          switchCard: true,
        );
      }
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  afiliar({required bool withPush}) async {
    if (withPush) {
      final (autheticate, message) = await LocalAuthPlugin.autheticate();
      if (!autheticate) {
        ref
            .read(snackbarProvider.notifier)
            .showSnackbar(message, SnackbarType.error);
        return;
      }
    }

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    resetToken();

    try {
      final response = await BiometriaService.afiliarDesafiliar(
        identificadorDispositivo: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),
      );

      state = state.copyWith(
        afiliarResponse: () => response,
        tokenDigital: await CoreService.desencriptarToken(
          response.codigoSolicitado,
        ),
      );
      initTimer();
      if (withPush) {
        ref.read(appRouterProvider).push('/biometria/confirmar');
      }
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  confirmar({bool esAfiliacion = true}) async {
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
    if (!state.aceptoTerminos && esAfiliacion) {
      ref.read(snackbarProvider.notifier).showSnackbar(
          'Acepte los terminos y condiciones.', SnackbarType.error);
      return;
    }

    if (state.switchCard) {
      confirmarAfiliacion();
    } else {
      confirmarDesafiliacion();
    }
  }

  confirmarAfiliacion() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final guid = await StorageService().get<String>(StorageKeys.guid) ?? '';

      final response = await BiometriaService.confirmarAfiliacion(
        tokenDigital: state.tokenDigital,
        codigoTipoBiometria: state.codigoTipoBiometria,
        guid: guid,
        identificadorDispositivo: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),
      );

      state = state.copyWith(
        confirmarAfiliacionResponse: () => response,
      );

      final newPasswordBiometrico = PasswordBiometrico(
        numeroTarjeta: ref.read(loginProvider).numeroTarjeta.value,
        codigoTipoBiometria: state.codigoTipoBiometria,
      );

      await BiometricStoragePlugin.setTokenBiometrico(
        response.claveAfiliacion,
        newPasswordBiometrico.numeroTarjeta,
      );

      await BiometriaService.addPasswordBiometrico(newPasswordBiometrico);

      ref.read(appRouterProvider).push('/biometria/operacion-exitosa');
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
      ref.read(timerProvider.notifier).cancelTimer();
      resetToken();
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  confirmarDesafiliacion() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final response = await BiometriaService.confirmarDesafiliacion(
          tokenDigital: state.tokenDigital,
          numeroAfiliacionBiometria:
              state.afiliacion?.numeroAfiliacionBiometria);

      state = state.copyWith(
        confirmarDesafiliacionResponse: () => response,
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

      ref.read(appRouterProvider).push('/biometria/operacion-exitosa');
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
      ref.read(timerProvider.notifier).cancelTimer();
      resetToken();
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  toggleSwitchCard() {
    state = state.copyWith(
      switchCard: !state.switchCard,
    );
  }

  toggleAceptarTerminos() {
    state = state.copyWith(
      aceptoTerminos: !state.aceptoTerminos,
    );
  }

  initTimer() {
    ref.read(timerProvider.notifier).initDateTimer(
          onFinish: () {
            resetToken();
          },
          initDate: state.afiliarResponse?.fechaSistema,
          date: state.afiliarResponse?.fechaVencimiento,
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

class BiometriaState {
  final bool faceId;
  final bool touchId;
  final AfiliacionBiometrica? afiliacion;
  final bool switchCard;
  final AfiliarResponse? afiliarResponse;
  final ConfirmarAfiliacionResponse? confirmarAfiliacionResponse;
  final ConfirmarDesafiliacionResponse? confirmarDesafiliacionResponse;
  final bool aceptoTerminos;
  final String tokenDigital;
  final int codigoTipoBiometria;
  final bool dispositivoConBiometricos;

  BiometriaState({
    this.faceId = false,
    this.touchId = false,
    this.afiliacion,
    this.switchCard = false,
    this.afiliarResponse,
    this.confirmarAfiliacionResponse,
    this.confirmarDesafiliacionResponse,
    this.tokenDigital = '',
    this.aceptoTerminos = false,
    this.codigoTipoBiometria = 1,
    this.dispositivoConBiometricos = false,
  });

  BiometriaState copyWith({
    bool? faceId,
    bool? touchId,
    ValueGetter<AfiliacionBiometrica?>? afiliacion,
    bool? switchCard,
    ValueGetter<AfiliarResponse?>? afiliarResponse,
    ValueGetter<ConfirmarAfiliacionResponse?>? confirmarAfiliacionResponse,
    ValueGetter<ConfirmarDesafiliacionResponse?>?
        confirmarDesafiliacionResponse,
    String? tokenDigital,
    bool? aceptoTerminos,
    int? codigoTipoBiometria,
    bool? dispositivoConBiometricos,
  }) =>
      BiometriaState(
        faceId: faceId ?? this.faceId,
        touchId: touchId ?? this.touchId,
        afiliacion: afiliacion != null ? afiliacion() : this.afiliacion,
        switchCard: switchCard ?? this.switchCard,
        afiliarResponse:
            afiliarResponse != null ? afiliarResponse() : this.afiliarResponse,
        confirmarAfiliacionResponse: confirmarAfiliacionResponse != null
            ? confirmarAfiliacionResponse()
            : this.confirmarAfiliacionResponse,
        confirmarDesafiliacionResponse: confirmarDesafiliacionResponse != null
            ? confirmarDesafiliacionResponse()
            : this.confirmarDesafiliacionResponse,
        tokenDigital: tokenDigital ?? this.tokenDigital,
        aceptoTerminos: aceptoTerminos ?? this.aceptoTerminos,
        codigoTipoBiometria: codigoTipoBiometria ?? this.codigoTipoBiometria,
        dispositivoConBiometricos:
            dispositivoConBiometricos ?? this.dispositivoConBiometricos,
      );
}
