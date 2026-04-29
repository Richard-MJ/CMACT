import 'package:flutter/services.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:emv_nfc_plugin/emv_nfc_plugin_method_channel.dart';

void main() {
  TestWidgetsFlutterBinding.ensureInitialized();

  MethodChannelEmvNfcPlugin platform = MethodChannelEmvNfcPlugin();
  const MethodChannel channel = MethodChannel('emv_nfc_plugin');

  setUp(() {
    TestDefaultBinaryMessengerBinding.instance.defaultBinaryMessenger.setMockMethodCallHandler(
      channel,
      (MethodCall methodCall) async {
        return true;
      },
    );
  });

  tearDown(() {
    TestDefaultBinaryMessengerBinding.instance.defaultBinaryMessenger.setMockMethodCallHandler(channel, null);
  });

  test('startNfcEmvReading', () async { 
    expect(await platform.startNfcEmvReading(), true);
  });
}
