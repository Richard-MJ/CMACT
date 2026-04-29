import 'dart:io';

import 'package:caja_tacna_app/core/services/app_version_service.dart';
import 'package:caja_tacna_app/core/states/app_version_state.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:device_info_plus/device_info_plus.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:package_info_plus/package_info_plus.dart';

final appVersionProvider =
    NotifierProvider<AppVersionNotifier, AppVersionState>(
        () => AppVersionNotifier());

class AppVersionNotifier extends Notifier<AppVersionState> {
  @override
  AppVersionState build() {
    return AppVersionState();
  }

  DeviceInfoPlugin deviceInfo = DeviceInfoPlugin();

  getAppVersion() async {
    PackageInfo packageInfo = await PackageInfo.fromPlatform();
    state = state.copyWith(
      appVersion: packageInfo.version,
      versionamiento: () => null,
    );

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      late int proyectoId;
      if (Platform.isAndroid) {
        final androidInfo = await deviceInfo.androidInfo;
        if (androidInfo.manufacturer.toLowerCase() == 'huawei') {
          proyectoId = 3;
        } else {
          proyectoId = 1;
        }
      }
      if (Platform.isIOS) {
        proyectoId = 2;
      }

      final response =
          await AppVersionService.obtenerVersionamiento(proyectoId: proyectoId);

      state = state.copyWith(
        versionamiento: () => response,
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }
}
