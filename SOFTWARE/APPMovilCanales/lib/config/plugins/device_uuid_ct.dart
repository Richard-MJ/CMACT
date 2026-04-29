import 'package:flutter/services.dart';

class DeviceUuidCt {
  static const MethodChannel _channel = MethodChannel('device_uuid_ct');

  Future<String?> getUUID() async {
      final String result = await _channel.invokeMethod('getUUID');
      return result;
  }
}