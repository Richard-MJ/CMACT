import 'dart:io';
import 'package:caja_tacna_app/config/plugins/device_uuid_ct.dart';
import 'package:caja_tacna_app/core/models/datos_dispositivo.dart';
import 'package:caja_tacna_app/core/services/encrypt_service.dart';
import 'package:caja_tacna_app/core/states/dispositivo_state.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/providers/login_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_ios_identifiers.dart';
import 'package:device_info_plus/device_info_plus.dart';
import 'package:flutter/services.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

final dispositivoProvider =
    NotifierProvider<DispositivoNotifier, DispositivoState>(
        () => DispositivoNotifier());

class DispositivoNotifier extends Notifier<DispositivoState> {
  @override
  DispositivoState build() {
    return DispositivoState();
  }

  DeviceInfoPlugin deviceInfo = DeviceInfoPlugin();
  final _deviceUuidPlugin = DeviceUuidCt();

  setDispositivo() async {
    try {
      if (Platform.isIOS) {
        final iosInfo = await deviceInfo.iosInfo;

        state = state.copyWith(
          dispositivo: () => DatosDispositivo(
            modelo: CtUtils.truncateString(
                CtIosIdentifiers.getIosModelName(iosInfo.utsname.machine), 0, 50),
            manufacturer: 'Apple',
            name: iosInfo.name,
            platform: iosInfo.systemName,
            idiom: 'spanish',
            type: iosInfo.isPhysicalDevice
                ? 'dispositivo_fisico'
                : 'dispositivo_emulado',
          ),
        );
      }

      if (Platform.isAndroid) {
        final androidInfo = await deviceInfo.androidInfo;
        state = state.copyWith(
          dispositivo: () => DatosDispositivo(
            modelo: CtUtils.truncateString(androidInfo.model, 0, 50),
            manufacturer: androidInfo.manufacturer,
            name: androidInfo.product,
            platform: 'android',
            idiom: 'spanish',
            type: androidInfo.isPhysicalDevice
                ? 'dispositivo_fisico'
                : 'dispositivo_emulado',
          ),
        );
      }
    } catch (_) {
      ref.read(snackbarProvider.notifier).showSnackbar(
          'Ocurrió un error al obtener los datos del dispositivo',
          SnackbarType.error);
    }
  }

  setIdentificadorDispositivo() async {
    String uuid;

    try {
      uuid = await _deviceUuidPlugin.getUUID() ?? 'Unknown uuid version';
    } on PlatformException {
      uuid = 'Failed to get uuid version.';
    }

    String identificador = EncryptService.calculateSHA256(
        '$uuid${ref.read(loginProvider).numeroTarjeta.value}${ref.read(loginProvider).numeroDocumento.value}');

    state = state.copyWith(identificadorDispositivo: identificador);
  }

  getIdentificadorDispositivo() {
    return state.identificadorDispositivo;
  }
}
