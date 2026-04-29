import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/constants/storage_keys.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/inputs/clave_tarjeta.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/models/crear_clave_response.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/models/eviar_sms_response.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/services/afiliacion_canales_electronicos_service.dart';
import 'package:caja_tacna_app/features/shared/data/documentos.dart';
import 'package:caja_tacna_app/features/shared/inputs/numero_documento.dart';
import 'package:caja_tacna_app/features/shared/inputs/numero_tarjeta.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/models/tipo_documento_local.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/services/storage_service.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:formz/formz.dart';

final afiliacionCanElectProvider = StateNotifierProvider<
    AfiliacionCanalesElectronicosNotifier,
    AfiliacionCanalesElectronicosState>((ref) {
  return AfiliacionCanalesElectronicosNotifier(ref);
});

class AfiliacionCanalesElectronicosNotifier
    extends StateNotifier<AfiliacionCanalesElectronicosState> {
  AfiliacionCanalesElectronicosNotifier(this.ref)
      : super(AfiliacionCanalesElectronicosState());

  final storageService = StorageService();
  final Ref ref;

  initForm() async {
    state = state.copyWith(
      numeroTarjeta: const NumeroTarjeta.pure(''),
      numeroDocumento: const NumeroDocumento.pure(''),
      claveTarjeta: const ClaveTarjeta.pure(''),
      claveInternet: '',
      documento: tiposDocumento[0],
    );
  }

  changeNumeroTarjeta(NumeroTarjeta value) {
    state = state.copyWith(
      numeroTarjeta: value,
    );
  }

  changeNumeroDocumento(NumeroDocumento value) {
    state = state.copyWith(
      numeroDocumento: value,
    );
  }

  changeClaveTarjeta(ClaveTarjeta claveTarjeta) {
    state = state.copyWith(
      claveTarjeta: claveTarjeta,
    );
  }

  changeClaveInternet(String value) {
    state = state.copyWith(
      claveInternet: value,
    );
  }

  changeClaveSms(String value) {
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

  goPaso2() {
    FocusManager.instance.primaryFocus?.unfocus();
    final numeroTarjeta = NumeroTarjeta.dirty(state.numeroTarjeta.value);
    final numeroDocumento = NumeroDocumento.dirty(state.numeroDocumento.value);
    final claveTarjeta = ClaveTarjeta.dirty(state.claveTarjeta.value);

    state = state.copyWith(
      numeroTarjeta: numeroTarjeta,
      numeroDocumento: numeroDocumento,
      claveTarjeta: claveTarjeta,
    );

    //validacion
    if (!Formz.validate([numeroTarjeta, numeroDocumento, claveTarjeta])) return;

    ref.read(appRouterProvider).push('/afiliacion-canales-electronicos/crear-clave');
  }

  crearClaveSubmit({required bool withPush}) async {
    FocusManager.instance.primaryFocus?.unfocus();
    ref.read(loaderProvider.notifier).showLoader();
    resetClaveSms();
    ref.read(timerProvider.notifier).cancelTimer();
    try {
      final CrearClaveResponse afiliacionCanElectResponse =
          await AfiliacionCanalesService.crearClave(
        numeroTarjeta: state.numeroTarjeta.value,
        claveInternet: state.claveInternet,
        claveTarjeta: state.claveTarjeta.value,
        numeroDocumento: state.numeroDocumento.value,
        idTipoDocumento: state.documento?.idTipoDocumento,
        modeloDispositivo: ref.read(dispositivoProvider).dispositivo?.modelo,
      );

      await storageService.set(
          StorageKeys.token, afiliacionCanElectResponse.accessToken);

      if (afiliacionCanElectResponse.tokenAdmin != null) {
        await storageService.set<String>(
            StorageKeys.tokenAdmin, afiliacionCanElectResponse.tokenAdmin!);
      }

      await enviarSms(withPush: withPush);
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
      ref.read(loaderProvider.notifier).dismissLoader();
    }
  }

  enviarSms({required bool withPush}) async {
    FocusManager.instance.primaryFocus?.unfocus();
    state = state.copyWith(cargandoSms: true);

    try {
      final enviarSmsResponse = await AfiliacionCanalesService.enviarSms();
      state = state.copyWith(
        enviarSmsResponse: () => enviarSmsResponse,
      );
      initTimer();
      if (withPush) {
        ref.read(appRouterProvider).push('/afiliacion-canales-electronicos/verificar-identidad');
      }
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }

    state = state.copyWith(cargandoSms: false);
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

  verificarIdentidad() async {
    if (!state.aceptarTerminos) {
      ref.read(snackbarProvider.notifier).showSnackbar(
          'Acepte los terminos y condiciones.', SnackbarType.error);
      return;
    }

    ref.read(loaderProvider.notifier).showLoader();

    ref.read(timerProvider.notifier).cancelTimer();

    try {
      await AfiliacionCanalesService.validarSms(
        idVerificacion: state.enviarSmsResponse?.idVerificacion,
        claveSms: state.claveSms,
      );

      ref.read(appRouterProvider).replace('/afiliacion-canales-electronicos/afiliacion-exitosa');
    } on ServiceException catch (e) {
      resetClaveSms();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }

    ref.read(loaderProvider.notifier).dismissLoader();
  }
}

class AfiliacionCanalesElectronicosState {
  final NumeroTarjeta numeroTarjeta;
  final NumeroDocumento numeroDocumento;
  final ClaveTarjeta claveTarjeta;
  final String claveInternet;
  final TipoDocumentoLocal? documento;
  final String claveSms;
  final bool aceptarTerminos;
  final EnviarSmsResponse? enviarSmsResponse;
  final bool cargandoSms;

  AfiliacionCanalesElectronicosState(
      {this.numeroTarjeta = const NumeroTarjeta.pure(''),
      this.numeroDocumento = const NumeroDocumento.pure(''),
      this.claveTarjeta = const ClaveTarjeta.pure(''),
      this.claveInternet = '',
      this.documento,
      this.claveSms = '',
      this.aceptarTerminos = false,
      this.enviarSmsResponse,
      this.cargandoSms = false});

  AfiliacionCanalesElectronicosState copyWith({
    NumeroTarjeta? numeroTarjeta,
    NumeroDocumento? numeroDocumento,
    ClaveTarjeta? claveTarjeta,
    String? claveInternet,
    TipoDocumentoLocal? documento,
    String? claveSms,
    bool? aceptarTerminos,
    ValueGetter<EnviarSmsResponse?>? enviarSmsResponse,
    bool? cargandoSms,
  }) =>
      AfiliacionCanalesElectronicosState(
          numeroTarjeta: numeroTarjeta ?? this.numeroTarjeta,
          numeroDocumento: numeroDocumento ?? this.numeroDocumento,
          claveTarjeta: claveTarjeta ?? this.claveTarjeta,
          claveInternet: claveInternet ?? this.claveInternet,
          documento: documento ?? this.documento,
          claveSms: claveSms ?? this.claveSms,
          aceptarTerminos: aceptarTerminos ?? this.aceptarTerminos,
          enviarSmsResponse: enviarSmsResponse != null
              ? enviarSmsResponse()
              : this.enviarSmsResponse,
          cargandoSms: cargandoSms ?? this.cargandoSms);
}
