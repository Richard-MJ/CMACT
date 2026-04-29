import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/constants/storage_keys.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/features/biometria/models/password_biometrico.dart';
import 'package:caja_tacna_app/features/biometria/services/biometria_service.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/models/enviar_sms_response.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/models/login_response.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/services/login_service.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/widgets/dialog_biometria_cambiada.dart';
import 'package:caja_tacna_app/features/shared/data/documentos.dart';
import 'package:caja_tacna_app/features/shared/inputs/numero_documento.dart';
import 'package:caja_tacna_app/features/shared/inputs/numero_tarjeta.dart';
import 'package:caja_tacna_app/features/shared/models/tipo_documento_local.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/auth_status_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/auth_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/services/storage_service.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:formz/formz.dart';
import 'package:caja_tacna_app/config/plugins/biometric_storage_plugin.dart';

final loginProvider =
    NotifierProvider<LoginNotifier, LoginState>(() => LoginNotifier());

class LoginNotifier extends Notifier<LoginState> {
  @override
  LoginState build() {
    return LoginState();
  }

  final snackbar = SnackbarService();
  final storageService = StorageService();
  GlobalKey? dialogBiometricaCambiadaKey;

  initFormularioScreen() async {
    await storageService.remove(StorageKeys.token);
    await storageService.remove(StorageKeys.tokenAdmin);
    await storageService.remove(StorageKeys.xsp);
    await storageService.remove(StorageKeys.xua);

    ref.read(authProvider.notifier).cancelAuthInterval();
    ref.read(timerProvider.notifier).cancelTimer();

    //esta funcion se ejecuta desde la vista FormularioScreen al crearse
    final numeroTarjeta =
        await storageService.get<String>(StorageKeys.numeroTarjeta) ?? '';
    final numeroDocumento =
        await storageService.get<String>(StorageKeys.numeroDocumento) ?? '';
    final recordarTarjeta =
        await storageService.get<bool>(StorageKeys.recordarTarjeta) ?? false;
    final recordarDocumento =
        await storageService.get<bool>(StorageKeys.recordarDocumento) ?? false;
    final guid = await storageService.get<String>(StorageKeys.guid) ?? '';
    final idTipoDocumento =
        await storageService.get<int>(StorageKeys.tipoDocumento) ?? 1;

    state = state.copyWith(
      numeroTarjeta: recordarTarjeta
          ? NumeroTarjeta.pure(numeroTarjeta)
          : const NumeroTarjeta.pure(''),
      numeroDocumento: recordarDocumento
          ? NumeroDocumento.pure(numeroDocumento)
          : const NumeroDocumento.pure(''),
      recordarTarjeta: recordarTarjeta,
      recordarDocumento: recordarDocumento,
      guid: guid,
      documento: recordarDocumento
          ? tiposDocumento.firstWhere((tipoDocumento) =>
              tipoDocumento.idTipoDocumento == idTipoDocumento)
          : tiposDocumento[0],
      aceptarTerminos: false,
      claveInternet: '',
      claveSms: '',
      enviarSmsResponse: () => null,
    );
    verificarBiometria();
  }

  changeNumeroTarjeta(NumeroTarjeta value) {
    state = state.copyWith(
      numeroTarjeta: value,
    );
    verificarBiometria();
  }

  changeNumeroDocumento(NumeroDocumento value) {
    state = state.copyWith(
      numeroDocumento: value,
    );
  }

  changeClaveInternet(String value) {
    state = state.copyWith(
      claveInternet: value,
    );
  }

  toggleRecordarTarjeta() {
    state = state.copyWith(
      recordarTarjeta: !state.recordarTarjeta,
    );
  }

  toggleRecordarDocumento() {
    state = state.copyWith(
      recordarDocumento: !state.recordarDocumento,
    );
  }

  iniciarSesion({required bool withPush}) async {
    ref.read(loaderProvider.notifier).showLoader();
    state = state.copyWith(cargandoSms: true);

    try {
      final LoginResponse loginResponse = await LoginService.login(
        numeroTarjeta: state.numeroTarjeta.value,
        password: state.claveInternet,
        numeroDocumento: state.numeroDocumento.value,
        idTipoDocumento: state.documento?.idTipoDocumento,
        guid: state.guid,
        idTipoOperacionCanalElectronico: 1,
        modeloDispositivo: ref.read(dispositivoProvider).dispositivo?.modelo,
      );

      await storageService.set<String>(
          StorageKeys.token, loginResponse.accessToken);
      await storageService.set<String>(
          StorageKeys.idVisual, loginResponse.xIdVisual);
      await storageService.set<int>(
          StorageKeys.tiempoInactividad, loginResponse.inactivityIn);

      if (loginResponse.tokenAdmin != null) {
        await storageService.set<String>(
            StorageKeys.tokenAdmin, loginResponse.tokenAdmin!);
      }

      state = state.copyWith(expiresIn: loginResponse.expiresIn);

      await setRemember();

      // si la cuenta  esta verificada con el dispositivo actual
      if (loginResponse.autorizado == 'true') {
        goHome();
      } else {
        // si la cuenta no esta verificada con el dispositivo actual
        state = state.copyWith(
          guid: loginResponse.newGuid,
        );
        await enviarSmsVerificacion(withPush: withPush);
      }
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    state = state.copyWith(cargandoSms: false);
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  verificarBiometria() async {
    if (state.numeroTarjeta.isNotValid) {
      state = state.copyWith(
        passwordBiometrico: () => null,
      );
      return;
    }
    PasswordBiometrico? passwordBiometrico =
        await BiometriaService.getPasswordBiometrico(state.numeroTarjeta.value);
    state = state.copyWith(
      passwordBiometrico: () => passwordBiometrico,
    );
  }

  iniciarSesionBiometria() async {
    //esta funcion se ejecuta al presionar el icono de biometrico
    FocusManager.instance.primaryFocus?.unfocus();
    final numeroTarjeta = NumeroTarjeta.dirty(state.numeroTarjeta.value);
    final numeroDocumento = NumeroDocumento.dirty(state.numeroDocumento.value);
    state = state.copyWith(
      numeroTarjeta: numeroTarjeta,
      numeroDocumento: numeroDocumento,
    );

    //validacion
    if (!Formz.validate([numeroTarjeta, numeroDocumento])) return;

    if (state.passwordBiometrico == null) return;
    try {
      final String? tokenBiometrico =
          await BiometricStoragePlugin.getTokenBiometrico(
        state.passwordBiometrico!.numeroTarjeta,
      );
      if (tokenBiometrico == null) {
        dialogBiometricaCambiadaKey = GlobalKey();
        showDialog(
          barrierDismissible: false,
          context: rootNavigatorKey.currentContext!,
          builder: (context) {
            return PopScope(
              canPop: false,
              child: DialogBiometricaCambiada(
                key: dialogBiometricaCambiadaKey,
              ),
            );
          },
        );

        await BiometriaService.deletePasswordBiometrico(
          state.passwordBiometrico!.numeroTarjeta,
        );

        await verificarBiometria();

        return;
      }

      ref.read(loaderProvider.notifier).showLoader();

      final LoginResponse loginResponse = await LoginService.login(
        numeroTarjeta: state.numeroTarjeta.value,
        password: await CoreService.desencriptarToken(
          tokenBiometrico,
        ),
        numeroDocumento: state.numeroDocumento.value,
        idTipoDocumento: state.documento?.idTipoDocumento,
        guid: state.guid,
        idTipoOperacionCanalElectronico: 3,
        modeloDispositivo: ref.read(dispositivoProvider).dispositivo?.modelo,
      );

      await storageService.set<String>(
          StorageKeys.token, loginResponse.accessToken);
      await storageService.set<String>(
          StorageKeys.idVisual, loginResponse.xIdVisual);
      await storageService.set<int>(
          StorageKeys.tiempoInactividad, loginResponse.inactivityIn);

      if (loginResponse.tokenAdmin != null) {
        await storageService.set<String>(
            StorageKeys.tokenAdmin, loginResponse.tokenAdmin!);
      }

      state = state.copyWith(expiresIn: loginResponse.expiresIn);

      await setRemember();

      // si la cuenta  esta verificada con el dispositivo actual
      if (loginResponse.autorizado == 'true') {
        goHome();
      }
    } on ServiceException catch (e) {
      if (e.message == "Dispositivo no registrado") {
        //eliminamos el password de storage
        final List<PasswordBiometrico> passwordsBiometricos =
            await BiometriaService.getPasswordsBiometricos();
        final List<PasswordBiometrico> nuevosPasswordsBiometricos =
            passwordsBiometricos
                .where((p) => p.numeroTarjeta != state.numeroTarjeta.value)
                .toList();

        await StorageService().set<List<dynamic>>(
            StorageKeys.passwordsBiometricos, nuevosPasswordsBiometricos);

        verificarBiometria();
      }

      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }

    ref.read(loaderProvider.notifier).dismissLoader();
  }

  goHome() {
    ref.read(loaderProvider.notifier).dismissLoader();
    ref.read(authProvider.notifier).initAuthInterval();
    ref.read(dispositivoProvider.notifier).setDispositivo();
    ref.read(dispositivoProvider.notifier).setIdentificadorDispositivo();
    ref.read(authStatusProvider.notifier).changeStatus(AuthStatus.authenticated);
    ref.read(appRouterProvider).go('/home');
  }

  initVerificarIdentidadScreen() {
    //esta funcion se ejecuta desde la vista VerificarIdentidadScreen al crearse
    state = state.copyWith(
      aceptarTerminos: false,
      claveSms: '',
    );
  }

  enviarSmsVerificacion({required bool withPush}) async {
    ref.read(loaderProvider.notifier).showLoader();

    ref.read(timerProvider.notifier).cancelTimer();
    resetClaveSms();
    try {
      final enviarSmsResponse = await LoginService.enviarSms();

      state = state.copyWith(
        enviarSmsResponse: () => enviarSmsResponse,
      );

      if (withPush) {
        ref.read(appRouterProvider).push('/inicio-sesion/verificar-identidad');
      }
      initTimer();
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
            resetClaveSms();
          },
          initDate: state.enviarSmsResponse?.fechaSistema,
          date: state.enviarSmsResponse?.fechaVencimiento,
        );
  }

  resetClaveSms() {
    state = state.copyWith(
      claveSms: '',
    );
  }

  setRemember() async {
    if (state.recordarTarjeta) {
      await storageService.set<String>(
          StorageKeys.numeroTarjeta, state.numeroTarjeta.value);
    }
    await storageService.set<bool>(
        StorageKeys.recordarTarjeta, state.recordarTarjeta);
    if (state.recordarDocumento) {
      await storageService.set<String>(
          StorageKeys.numeroDocumento, state.numeroDocumento.value);
      await storageService.set<int>(
          StorageKeys.tipoDocumento, state.documento!.idTipoDocumento);
    }
    await storageService.set<bool>(
        StorageKeys.recordarDocumento, state.recordarDocumento);
  }

  verificarIdentidad() async {
    //se oculta el teclado si esta visible
    FocusManager.instance.primaryFocus?.unfocus();

    if (!state.aceptarTerminos) {
      ref.read(snackbarProvider.notifier).showSnackbar(
          'Acepte los terminos y condiciones.', SnackbarType.error);
      return;
    }
    ref.read(loaderProvider.notifier).showLoader();

    ref.read(timerProvider.notifier).cancelTimer();

    try {
      final verificacionResponse = await LoginService.validarSms(
          idVerificacion: state.enviarSmsResponse?.idVerificacion,
          claveSms: state.claveSms,
          guid: state.guid,
          idVisual:
              await storageService.get<String>(StorageKeys.idVisual) ?? '');

      await storageService.set<String>(
          'token', verificacionResponse.accessToken);
      await storageService.set<String>('guid', state.guid);
      await storageService.set<int>(
          StorageKeys.tiempoInactividad, verificacionResponse.inactivityIn);

      state =
          state.copyWith(expiresIn: int.parse(verificacionResponse.expiresIn));

      goHome();
    } on ServiceException catch (e) {
      state = state.copyWith(guid: '');
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  ingresarClave() {
    //esta funcion se ejecuta al presionar el input de clave de internet en la vista FormularioScreen
    FocusManager.instance.primaryFocus?.unfocus();
    final numeroTarjeta = NumeroTarjeta.dirty(state.numeroTarjeta.value);
    final numeroDocumento = NumeroDocumento.dirty(state.numeroDocumento.value);
    state = state.copyWith(
      numeroTarjeta: numeroTarjeta,
      numeroDocumento: numeroDocumento,
    );

    //validacion
    if (!Formz.validate([numeroTarjeta, numeroDocumento])) return;

    //se limpia la clave de internet antes de desplegar el modal
    state = state.copyWith(
      claveInternet: '',
    );

    ref.read(appRouterProvider).push('/inicio-sesion/ingresar-clave');
  }

  goAfiliarse() {
    ref
        .read(appRouterProvider)
        .push('/afiliacion-canales-electronicos/formulario');
  }

  changeCodigoAutorizacion(String value) {
    state = state.copyWith(
      claveSms: value,
    );
  }

  toggleAceptarTerminos() {
    state = state.copyWith(
      aceptarTerminos: !state.aceptarTerminos,
    );
  }

  changeDocumento(TipoDocumentoLocal documento) {
    state = state.copyWith(
      documento: documento,
      numeroDocumento: const NumeroDocumento.pure(''),
    );
  }
}

class LoginState {
  final NumeroTarjeta numeroTarjeta;
  final NumeroDocumento numeroDocumento;
  final String claveInternet;
  final TipoDocumentoLocal? documento;
  final bool recordarTarjeta;
  final bool recordarDocumento;
  final bool aceptarTerminos;
  final String claveSms;
  final String guid;
  final EnviarSmsResponse? enviarSmsResponse;
  final PasswordBiometrico? passwordBiometrico;
  final int? expiresIn;
  final bool cargandoSms;

  LoginState(
      {this.numeroTarjeta = const NumeroTarjeta.pure(''),
      this.numeroDocumento = const NumeroDocumento.pure(''),
      this.claveInternet = '',
      this.documento,
      this.recordarTarjeta = false,
      this.recordarDocumento = false,
      this.aceptarTerminos = false,
      this.claveSms = '',
      this.guid = '',
      this.enviarSmsResponse,
      this.passwordBiometrico,
      this.expiresIn = 0,
      this.cargandoSms = false});

  LoginState copyWith({
    NumeroTarjeta? numeroTarjeta,
    NumeroDocumento? numeroDocumento,
    String? claveInternet,
    TipoDocumentoLocal? documento,
    bool? recordarTarjeta,
    bool? recordarDocumento,
    bool? aceptarTerminos,
    String? claveSms,
    String? guid,
    ValueGetter<EnviarSmsResponse?>? enviarSmsResponse,
    ValueGetter<PasswordBiometrico?>? passwordBiometrico,
    int? expiresIn,
    bool? cargandoSms,
  }) =>
      LoginState(
          numeroTarjeta: numeroTarjeta ?? this.numeroTarjeta,
          numeroDocumento: numeroDocumento ?? this.numeroDocumento,
          claveInternet: claveInternet ?? this.claveInternet,
          documento: documento ?? this.documento,
          recordarTarjeta: recordarTarjeta ?? this.recordarTarjeta,
          recordarDocumento: recordarDocumento ?? this.recordarDocumento,
          aceptarTerminos: aceptarTerminos ?? this.aceptarTerminos,
          claveSms: claveSms ?? this.claveSms,
          guid: guid ?? this.guid,
          enviarSmsResponse: enviarSmsResponse != null
              ? enviarSmsResponse()
              : this.enviarSmsResponse,
          passwordBiometrico: passwordBiometrico != null
              ? passwordBiometrico()
              : this.passwordBiometrico,
          expiresIn: expiresIn ?? this.expiresIn,
          cargandoSms: cargandoSms ?? this.cargandoSms);
}
