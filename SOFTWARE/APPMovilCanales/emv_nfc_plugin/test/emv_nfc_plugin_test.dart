import 'package:flutter_test/flutter_test.dart';
import 'package:emv_nfc_plugin/emv_nfc_plugin.dart';
import 'package:emv_nfc_plugin/emv_nfc_plugin_platform_interface.dart';
import 'package:emv_nfc_plugin/emv_nfc_plugin_method_channel.dart';
import 'package:plugin_platform_interface/plugin_platform_interface.dart';

class MockEmvNfcPluginPlatform
    with MockPlatformInterfaceMixin
    implements EmvNfcPluginPlatform {

  @override
  Future<bool> startNfcEmvReading() async {
    return true;
  }
  
  @override
  Future<bool> stopNfcEmvReading() {
    // TODO: implement stopNfcEmvReading
    throw UnimplementedError();
  }
  
  @override
  Future<String> isNfcAvailable() {
    // TODO: implement isNfcAvailable
    throw UnimplementedError();
  }
  
  @override
  void setEmvReadResultCallback(EmvReadResultCallback callback) {
    // TODO: implement setEmvReadResultCallback
  }
}

void main() {
  TestWidgetsFlutterBinding.ensureInitialized();
  final EmvNfcPluginPlatform initialPlatform = EmvNfcPluginPlatform.instance;

  test('$MethodChannelEmvNfcPlugin is the default instance', () {
    expect(initialPlatform, isInstanceOf<MethodChannelEmvNfcPlugin>());
  });

  test('startNfcEmvReading', () async {
    EmvNfcPlugin emvNfcPlugin = EmvNfcPlugin();
    MockEmvNfcPluginPlatform fakePlatform = MockEmvNfcPluginPlatform();
    EmvNfcPluginPlatform.instance = fakePlatform;

    expect(await emvNfcPlugin.startNfcEmvReading(), true);
  });
}
