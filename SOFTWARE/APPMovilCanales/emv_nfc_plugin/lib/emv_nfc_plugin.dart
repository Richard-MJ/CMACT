
import 'emv_nfc_plugin_platform_interface.dart';

class EmvNfcPlugin {
  Future<String> isNfcAvailable() {
    return EmvNfcPluginPlatform.instance.isNfcAvailable();
  }

  Future<bool> startNfcEmvReading() {
    return EmvNfcPluginPlatform.instance.startNfcEmvReading();
  }

  Future<bool> stopNfcEmvReading() {
    return EmvNfcPluginPlatform.instance.stopNfcEmvReading();
  }

  void setEmvReadResultCallback(EmvReadResultCallback callback) {
    EmvNfcPluginPlatform.instance.setEmvReadResultCallback(callback);
  }
}
