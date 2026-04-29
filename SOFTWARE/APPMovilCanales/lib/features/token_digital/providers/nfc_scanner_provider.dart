import 'dart:async';

import 'package:caja_tacna_app/config/plugins/emv_nfc_plugin/emv_nfc_plugin.dart';
import 'package:caja_tacna_app/config/plugins/emv_nfc_plugin/nfc_card_data.dart';
import 'package:caja_tacna_app/config/plugins/emv_nfc_plugin/nfc_repository.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/providers/login_provider.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:caja_tacna_app/features/token_digital/states/nfc_scanner_state.dart';
import 'package:emv_nfc_plugin/emv_nfc_plugin.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';

final emvNfcPluginInstanceProvider = Provider<EmvNfcPlugin>((ref) {
  return EmvNfcPlugin();
});

final nfcRepositoryProvider = Provider<NfcRepository>((ref) {
  final emvNfcPlugin = ref.watch(emvNfcPluginInstanceProvider);
  return NfcPlugin(emvNfcPlugin);
});

final nfcScannerProvider =
    StateNotifierProvider<NfcScannerNotifier, NfcScannerState>((ref) {
  return NfcScannerNotifier(ref);
});

class NfcScannerNotifier extends StateNotifier<NfcScannerState> {
  final Ref ref;
  StreamSubscription? _nfcReadSubscription;
  late final NfcRepository _nfcRepository;

  NfcScannerNotifier(this.ref) : super(NfcScannerState()) {
    _nfcRepository = ref.read(nfcRepositoryProvider);
    _nfcReadSubscription =
        _nfcRepository.getEmvReadResultStream().listen((result) {
      _handleNfcResult(result);
    });
  }

  Future<void> _handleNfcResult(Map<String, dynamic> result) async {
    final status = result['status'] as String;

    if (status == "ERROR") {
      ref.read(snackbarProvider.notifier).showSnackbar(
          'No se pudo leer tu tarjeta. Asegúrate de acercarla correctamente al lector NFC y vuelve a intentarlo.',
          SnackbarType.error);
      return;
    }
    
    final message = result['message'] as String;
    final dynamic rawData = result['data'];

    NfcCardData? cardData;
    if (rawData is Map<dynamic, dynamic> && rawData.isNotEmpty) {
      try {
        cardData = NfcCardData.fromMap(rawData);
        var isValid = !_invalidCardData(cardData);

        if (!isValid) {
          ref.read(snackbarProvider.notifier).showSnackbar(
              'Datos de la tarjeta no válidos.', SnackbarType.error);
          return;
        }

        state = state.copyWith(
          status: status,
          message: message,
          nfcCardData: () => cardData,
          isLoading: false,
        );

        if (status == "SUCCESS") {
          await stopNfcReading();
        }
      } catch (e) {
        ref.read(snackbarProvider.notifier).showSnackbar(
            'Error al leer datos de la tarjeta', SnackbarType.error);
      }
    }
  }

  Future<String> checkNfcAvailability() async {
    state = state.copyWith(
      status: 'VERIFICANDO',
      message: 'Verificando disponibilidad NFC...',
      nfcCardData: () => null,
      isLoading: true,
    );
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final status = await _nfcRepository.isNfcAvailable();
      state = state.copyWith(
        status: status,
        message: 'Disponibilidad NFC: $status',
        isLoading: false,
      );
    } on ServiceException catch (e) {
      state = state.copyWith(
        status: 'ERROR',
        message: 'Error al verificar NFC: ${e.message}',
        isLoading: false,
      );
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    } finally {
      ref.read(loaderProvider.notifier).dismissLoader();
    }

    return state.status;
  }

  Future<void> startNfcReading(BuildContext context) async {
    final status = await checkNfcAvailability();
    if (status != 'AVAILABLE') {
      context.pop();
      ref.read(snackbarProvider.notifier).showSnackbar(
          'NFC deshabilitado o no soportado en este dispositivo.',
          SnackbarType.error);
      return;
    }

    state = state.copyWith(
      status: 'INICIANDO',
      message: 'Acerque la tarjeta...',
      nfcCardData: () => null,
      isLoading: true,
    );

    try {
      final success = await _nfcRepository.startNfcEmvReading();
      if (!success) {
        state = state.copyWith(
          status: 'ERROR',
          message: 'No se pudo iniciar la lectura NFC.',
          isLoading: false,
        );
        ref
            .read(snackbarProvider.notifier)
            .showSnackbar('No se pudo iniciar la lectura.', SnackbarType.error);
      }
    } on ServiceException catch (e) {
      state = state.copyWith(
        status: 'ERROR',
        message: 'Error al iniciar lectura: ${e.message}',
        isLoading: false,
      );
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  Future<void> stopNfcReading() async {
    try {
      await _nfcRepository.stopNfcEmvReading();
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  @override
  void dispose() {
    _nfcReadSubscription?.cancel();
    _nfcRepository.stopNfcEmvReading();

    if (_nfcRepository is NfcPlugin) {
      (_nfcRepository).dispose();
    }
    super.dispose();
  }

  bool _invalidCardData(NfcCardData cardData) {
    return cardData.pan.isEmpty ||
        cardData.expiryDate.isEmpty ||
        cardData.pan != ref.read(loginProvider).numeroTarjeta.value;
  }
}
