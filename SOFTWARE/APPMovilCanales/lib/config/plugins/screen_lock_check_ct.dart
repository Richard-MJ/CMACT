import 'package:flutter/services.dart';

class ScreenLockCheckCt {
  static const MethodChannel _channel = MethodChannel('screen_lock_check_ct');

  static Future<bool> isScreenLockEnabled() async {
    try {
      final bool result = await _channel.invokeMethod('isScreenLockEnabled');
      return result;
    } on PlatformException {
      return false;
    }
  }
}