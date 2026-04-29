import 'package:caja_tacna_app/config/plugins/emv_nfc_plugin/nfc_card_data.dart';
import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/constants/estado_entidad.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/inputs/clave_tarjeta.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/inputs/clave_intenet.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/home/services/home_service.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/token_digital/providers/nfc_scanner_provider.dart';
import 'package:caja_tacna_app/features/token_digital/services/token_digital_service.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:caja_tacna_app/features/token_digital/states/token_digital_state.dart';
import 'package:caja_tacna_app/features/token_digital/widgets/dialog_nfc_no_disponible.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:formz/formz.dart';
import 'package:device_info_plus/device_info_plus.dart';

final tokenDigitalProvider =
    StateNotifierProvider<TokenDigitalNotifier, TokenDigitalState>((ref) {
  return TokenDigitalNotifier(ref);
});

class TokenDigitalNotifier extends StateNotifier<TokenDigitalState> {
  TokenDigitalNotifier(this.ref) : super(TokenDigitalState());

  final Ref ref;
  DeviceInfoPlugin deviceInfo = DeviceInfoPlugin();
  String codigoDocumentoTerminos = "COD_04";
  initDatos() {
    state = state.copyWith(
      claveCajero: const ClaveTarjeta.pure(''),
      claveInternet: const ClaveInternet.pure(''),
      dispositivoAfiliado: () => null,
      afiliarResponse: () => null,
      desafiliarResponse: () => null,
      aceptarTerminos: false,
      obtenerTokenResponse: () => null,
      esDispositivoAfiliado: false,
      pasoActualRestablecer: 1,
      documentoTermino:() => null,
    );
    primeraCarga = true;
  }

  bool primeraCarga = true;
  goTokenDigital() async {
    initDatos();
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    try {
      await obtenerDispositivoAfiliado();
      final documentoTermino = ref.read(homeProvider).configuracion?.enlacesDocumentos
        .firstWhere((x) => x.codigoDocumento == codigoDocumentoTerminos);
      state = state.copyWith(documentoTermino: () => documentoTermino);

      final esMismoDispositivo =
          ref.read(dispositivoProvider).identificadorDispositivo ==
              state.dispositivoAfiliado?.deviceUuid;

      if (state.dispositivoAfiliado == null ||
          (state.dispositivoAfiliado?.deviceStatus == EstadoEntidad.inactivo &&
              esMismoDispositivo)) {
        state = state.copyWith(esDispositivoAfiliado: false);
        ref.read(appRouterProvider).push('/token-digital/afiliar');
        ref.read(loaderProvider.notifier).dismissLoader();
      }

      if (state.dispositivoAfiliado?.deviceStatus == EstadoEntidad.activo ||
          (!esMismoDispositivo && state.dispositivoAfiliado != null)) {
        state = state.copyWith(esDispositivoAfiliado: esMismoDispositivo);
        ref.read(appRouterProvider).push('/token-digital/token');
      }
    } on ServiceException catch (e) {
      ref.read(loaderProvider.notifier).dismissLoader();

      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  obtenerDispositivoAfiliado() async {
    state = state.copyWith(
      dispositivoAfiliado: () => null,
    );
    try {
      final response = await TokenDigitalService.obtenerDispositivoAfiliado();

      state = state.copyWith(
        dispositivoAfiliado: () => response,
      );
    } on ServiceException catch (e) {
      throw ServiceException(e.message);
    }
  }

  obtenerToken() async {
    if (!state.esDispositivoAfiliado) {
      ref.read(loaderProvider.notifier).dismissLoader();
      return;
    }

    state = state.copyWith(
      obtenerTokenResponse: () => null,
    );
    if (primeraCarga) {
      ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    }
    primeraCarga = false;

    try {
      final response = await TokenDigitalService.obtenerToken(
        identificadorDispositivo: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),
      );

      response.codigoSolicitado =
          await CoreService.desencriptarToken(response.codigoSolicitado);

      state = state.copyWith(
        obtenerTokenResponse: () => response,
      );
      initTimer();
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    } catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Error al obtener token digital', SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  initTimer() {
    ref.read(timerProvider.notifier).initDateTimer(
          onFinish: () {
            obtenerToken();
          },
          initDate: state.obtenerTokenResponse?.fechaSistema,
          date: state.obtenerTokenResponse?.fechaVencimiento,
        );
  }

  changeClaveCajero(ClaveTarjeta claveCajero) {
    state = state.copyWith(
      claveCajero: claveCajero,
    );
  }

  resetClave() {
    state = state.copyWith(
      claveCajero: ClaveTarjeta.pure(''),
      claveInternet: ClaveInternet.pure(''),
    );
  }

  changeClaveInternet(ClaveInternet claveInternet) {
    state = state.copyWith(
      claveInternet: claveInternet,
    );
  }

  afiliar() async {
    FocusManager.instance.primaryFocus?.unfocus();

    final claveCajero = ClaveTarjeta.dirty(state.claveCajero.value);
    final claveInternet = ClaveInternet.dirty(state.claveInternet.value);

    state = state.copyWith(
      claveCajero: claveCajero,
      claveInternet: claveInternet,
    );

    if (!Formz.validate([claveCajero, claveInternet])) return;
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      if (state.dispositivoAfiliado == null) {
        final datosDispositivo = ref.read(dispositivoProvider).dispositivo;
        if (datosDispositivo == null) return;

        final response = await TokenDigitalService.afiliarNuevoDispositivo(
          deviceModel: datosDispositivo.modelo,
          deviceIdiom: datosDispositivo.idiom,
          deviceManufacturer: datosDispositivo.manufacturer,
          deviceName: datosDispositivo.name,
          devicePlatform: datosDispositivo.platform,
          deviceType: datosDispositivo.type,
          deviceUuid: ref
              .read(dispositivoProvider.notifier)
              .getIdentificadorDispositivo(),
          claveInternet: state.claveInternet.value,
          claveTarjeta: state.claveCajero.value,
        );
        state = state.copyWith(
          afiliarResponse: () => response,
        );
      } else {
        final response = await TokenDigitalService.reafiliar(
          obtenerDispositivoResponse: state.dispositivoAfiliado,
          claveInternet: state.claveInternet.value,
          claveTarjeta: state.claveCajero.value,
          deviceUuid: ref
              .read(dispositivoProvider.notifier)
              .getIdentificadorDispositivo(),
        );
        state = state.copyWith(
          afiliarResponse: () => response,
        );
      }

      ref.read(appRouterProvider).push('/token-digital/afiliacion-exitosa');
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  desafiliar() async {
    FocusManager.instance.primaryFocus?.unfocus();

    final claveCajero = ClaveTarjeta.dirty(state.claveCajero.value);
    final claveInternet = ClaveInternet.dirty(state.claveInternet.value);

    state = state.copyWith(
      claveCajero: claveCajero,
      claveInternet: claveInternet,
    );

    if (!Formz.validate([claveCajero, claveInternet])) return;
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final response = await TokenDigitalService.desafiliar(
        obtenerDispositivoResponse: state.dispositivoAfiliado,
        deviceUuid: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),
        claveInternet: state.claveInternet.value,
        claveTarjeta: state.claveCajero.value,
      );

      state = state.copyWith(
        desafiliarResponse: () => response,
      );

      ref.read(appRouterProvider).push('/token-digital/desafiliacion-exitosa');
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  toggleAceptarTerminos() {
    state = state.copyWith(
      aceptarTerminos: !state.aceptarTerminos,
    );
  }

  aceptarTerminos() {
    state = state.copyWith(
      aceptarTerminos: true,
    );
  }

  goToRestablecer() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    var configuracion = await HomeService.getConfiguracion();
    if (!configuracion.indicadorLecturaNfc) {
      showDialog(
        context: ref
            .read(appRouterProvider)
            .routerDelegate
            .navigatorKey
            .currentContext!,
        builder: (BuildContext context) {
          return const DialogNfc(
            contenido:
                'Para restablecer tu Token Digital, debes acudir a cualquiera de nuestras agencias a nivel nacional.',
          );
        },
      );
      return;
    }

    final nfcStatus =
        await ref.read(nfcScannerProvider.notifier).checkNfcAvailability();
    if (nfcStatus == "UNAVAILABLE") {
      showDialog(
        context: ref
            .read(appRouterProvider)
            .routerDelegate
            .navigatorKey
            .currentContext!,
        builder: (BuildContext context) {
          return const DialogNfc(
            titulo:
                'Tu dispositivo no cumple con los requisitos para utilizar esta función',
            contenido:
                'Para restablecer tu Token Digital, debes acudir a cualquiera de nuestras agencias a nivel nacional.',
          );
        },
      );
      return;
    }

    if (nfcStatus == "DISABLED") {
      showDialog(
        context: ref
            .read(appRouterProvider)
            .routerDelegate
            .navigatorKey
            .currentContext!,
        builder: (BuildContext context) {
          return const DialogNfc(
            contenido:
                'Para continuar, activa el NFC de tu teléfono desde Ajustes.',
          );
        },
      );
      return;
    }

    ref.read(appRouterProvider).push('/token-digital/restablecer');
  }

  goToRestablecerPaso(int paso) {
    state = state.copyWith(pasoActualRestablecer: paso);
  }

  changeClaveSms(String value) {
    state = state.copyWith(
      claveSms: value,
    );
  }

  goToSmsRestablecerTokenDigital({required bool withStep}) async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    state = state.copyWith(cargandoSms: true);

    try {
      final response = await TokenDigitalService.smsRestablecerTokenDigital();
      state = state.copyWith(
        enviarSmsResponse: () => response,
      );
      initTimerSms();
      if (withStep) {
        goToRestablecerPaso(2);
      }
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }

    state = state.copyWith(cargandoSms: false);
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  initTimerSms() {
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

  restablecerTokenDigital({required NfcCardData? cardData}) async {
    FocusManager.instance.primaryFocus?.unfocus();

    final claveCajero = ClaveTarjeta.dirty(state.claveCajero.value);
    final claveInternet = ClaveInternet.dirty(state.claveInternet.value);

    state = state.copyWith(
      claveCajero: claveCajero,
      claveInternet: claveInternet,
    );

    if (!Formz.validate([claveCajero, claveInternet])) return;
    if (cardData == null || state.claveSms.isEmpty) return;

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final datosDispositivo = ref.read(dispositivoProvider).dispositivo;
      if (datosDispositivo == null) return;

      final response = await TokenDigitalService.restablecerTokenDigital(
        deviceModel: datosDispositivo.modelo,
        deviceIdiom: datosDispositivo.idiom,
        deviceManufacturer: datosDispositivo.manufacturer,
        deviceName: datosDispositivo.name,
        devicePlatform: datosDispositivo.platform,
        deviceType: datosDispositivo.type,
        deviceUuid: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),
        claveInternet: state.claveInternet.value,
        claveTarjeta: state.claveCajero.value,
        numeroTarjeta: cardData.pan,
        fechaVencimiento: cardData.expiryDate,
        idVerificacion:
            state.enviarSmsResponse?.idVerificacion.toString() ?? '0',
        codigoAutorizacion: state.claveSms,
      );
      state = state.copyWith(
        afiliarResponse: () => response,
      );

      ref.read(appRouterProvider).push('/token-digital/restablecer-exitosa');
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }
}
