import 'package:plugin_platform_interface/plugin_platform_interface.dart';

import 'emv_nfc_plugin_method_channel.dart';

typedef EmvReadResultCallback = void Function(
  String status,
  String message,
  Map<dynamic, dynamic>? data,
);

abstract class EmvNfcPluginPlatform extends PlatformInterface {
  EmvNfcPluginPlatform() : super(token: _token);

  static final Object _token = Object();

  static EmvNfcPluginPlatform _instance = MethodChannelEmvNfcPlugin();

  static EmvNfcPluginPlatform get instance => _instance;

  static set instance(EmvNfcPluginPlatform instance) {
    PlatformInterface.verifyToken(instance, _token);
    _instance = instance;
  }

  Future<String> isNfcAvailable();

  Future<bool> startNfcEmvReading();

  Future<bool> stopNfcEmvReading();

  void setEmvReadResultCallback(EmvReadResultCallback callback);
}
