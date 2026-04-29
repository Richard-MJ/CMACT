import 'package:flutter/foundation.dart';
import 'package:flutter/services.dart';

import 'emv_nfc_plugin_platform_interface.dart';

class MethodChannelEmvNfcPlugin extends EmvNfcPluginPlatform {
  @visibleForTesting
  final methodChannel = const MethodChannel('emv_nfc_plugin');

  EmvReadResultCallback? _callback;

  MethodChannelEmvNfcPlugin() {
    methodChannel.setMethodCallHandler(_handleNativeMethodCall);
  }

  @override
  Future<String> isNfcAvailable() async {
    return await methodChannel.invokeMethod<String>('isNfcAvailable') ??
        'UNAVAILABLE';
  }

  @override
  Future<bool> startNfcEmvReading() async {
    final result = await methodChannel.invokeMethod<bool>('startNfcEmvReading');
    return result ?? false;
  }

  @override
  Future<bool> stopNfcEmvReading() async {
    var result = await methodChannel.invokeMethod<bool>('stopNfcEmvReading');
    return result ?? false;
  }

  @override
  void setEmvReadResultCallback(EmvReadResultCallback callback) {
    _callback = callback;
  }

  Future<void> _handleNativeMethodCall(MethodCall call) async {
    if (call.method == 'onEmvReadResult') {
      final result = call.arguments as Map<dynamic, dynamic>;
      _callback?.call(
        result['status'] as String,
        result['message'] as String,
        result['data'] as Map<dynamic, dynamic>?,
      );
    }
  }
}
