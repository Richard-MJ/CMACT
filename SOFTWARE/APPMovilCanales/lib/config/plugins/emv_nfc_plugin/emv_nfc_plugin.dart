import 'dart:async';
import 'dart:io';

import 'package:caja_tacna_app/config/plugins/emv_nfc_plugin/nfc_repository.dart';
import 'package:emv_nfc_plugin/emv_nfc_plugin.dart';

class NfcPlugin implements NfcRepository {
  final EmvNfcPlugin _emvNfcPlugin;
  final StreamController<Map<String, dynamic>> _emvReadResultController =
      StreamController<Map<String, dynamic>>.broadcast();

  NfcPlugin(this._emvNfcPlugin) {
    _emvNfcPlugin.setEmvReadResultCallback((status, message, data) {
      _emvReadResultController.add({
        'status': status,
        'message': message,
        'data': data,
      });
    });
  }

  @override
  Future<String> isNfcAvailable() async {
    if (Platform.isIOS) return "UNAVAILABLE";
    return await _emvNfcPlugin.isNfcAvailable();
  }

  @override
  Future<bool> startNfcEmvReading() async {
    return await _emvNfcPlugin.startNfcEmvReading();
  }

  @override
  Future<bool> stopNfcEmvReading() async {
    return await _emvNfcPlugin.stopNfcEmvReading();
  }

  @override
  Stream<Map<String, dynamic>> getEmvReadResultStream() {
    return _emvReadResultController.stream;
  }

  void dispose() {
    _emvReadResultController.close();
  }
}
