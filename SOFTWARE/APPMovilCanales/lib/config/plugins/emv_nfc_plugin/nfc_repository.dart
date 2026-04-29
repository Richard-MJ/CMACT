abstract class NfcRepository {
  Future<String> isNfcAvailable();
  Future<bool> startNfcEmvReading();
  Future<bool> stopNfcEmvReading();
  Stream<Map<String, dynamic>> getEmvReadResultStream();
}
