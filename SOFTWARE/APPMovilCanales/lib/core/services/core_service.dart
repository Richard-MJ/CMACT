import 'dart:convert';
import 'dart:typed_data';
import 'package:caja_tacna_app/constants/storage_keys.dart';
import 'package:caja_tacna_app/features/shared/services/storage_service.dart';
import 'package:cryptography/cryptography.dart' as cryptography;

class CoreService {
  static Future<String> desencriptarToken(String tokenEncriptado) async {
    final storageService = StorageService();
    final idVisual =
        await storageService.get<String>(StorageKeys.idVisual) ?? '';

    return StringCipher.decrypt(
        cipherText: tokenEncriptado, passPhrase: idVisual);
  }
}

class StringCipher {
  static const int _keySize = 128;
  static const int _keySizeInBytes = _keySize ~/ 8;
  static const int _derivationIterations = 10000;
  static const int _nonceSize = 12;
  static const int _tagSize = 16;
  static const String _gcmPrefix = 'v2:';

  static Future<String> decrypt(
      {required String cipherText, required String passPhrase}) async {
    if (cipherText.startsWith(_gcmPrefix)) {
      return _decryptGCM(cipherText.substring(_gcmPrefix.length), passPhrase);
    }

    return _decryptLegacyCBC(cipherText, passPhrase);
  }

  static Future<String> _decryptGCM(
      String base64Data, String passPhrase) async {
    final allBytes = base64.decode(base64Data);

    final salt = Uint8List.fromList(allBytes.sublist(0, _keySizeInBytes));
    final nonce = Uint8List.fromList(
        allBytes.sublist(_keySizeInBytes, _keySizeInBytes + _nonceSize));
    final tag = Uint8List.fromList(allBytes.sublist(
        _keySizeInBytes + _nonceSize,
        _keySizeInBytes + _nonceSize + _tagSize));
    final cipherBytes = Uint8List.fromList(
        allBytes.sublist(_keySizeInBytes + _nonceSize + _tagSize));

    final key = await _deriveKey(passPhrase, salt);

    final algorithm = cryptography.AesGcm.with128bits();
    final secretKey = cryptography.SecretKey(key);

    final secretBox = cryptography.SecretBox(
      cipherBytes,
      nonce: nonce,
      mac: cryptography.Mac(tag),
    );

    final decryptedBytes = await algorithm.decrypt(
      secretBox,
      secretKey: secretKey,
    );

    return utf8.decode(decryptedBytes);
  }

  @Deprecated('CBC legacy: eliminar cuando todos los tokens usen GCM')
  static Future<String> _decryptLegacyCBC(
      String cipherText, String passPhrase) async {
    final cipherTextBytes = base64.decode(cipherText);
    final salt =
        Uint8List.fromList(cipherTextBytes.sublist(0, _keySizeInBytes));
    final iv = Uint8List.fromList(
        cipherTextBytes.sublist(_keySizeInBytes, _keySizeInBytes * 2));
    final encryptedData =
        Uint8List.fromList(cipherTextBytes.sublist(_keySizeInBytes * 2));

    final key = await _deriveKey(passPhrase, salt);

    final algorithm = cryptography.AesCbc.with128bits(
      macAlgorithm: cryptography.MacAlgorithm.empty,
    );
    final secretKey = cryptography.SecretKey(key);

    final secretBox = cryptography.SecretBox(
      encryptedData,
      nonce: iv,
      mac: cryptography.Mac.empty,
    );

    final decryptedBytes = await algorithm.decrypt(
      secretBox,
      secretKey: secretKey,
    );

    return utf8.decode(decryptedBytes);
  }


  static Future<List<int>> _deriveKey(
      String passPhrase, Uint8List salt) async {
    final pbkdf2 = cryptography.Pbkdf2(
      macAlgorithm: cryptography.Hmac(cryptography.Sha1()),
      iterations: _derivationIterations,
      bits: _keySize,
    );

    final secretKey = await pbkdf2.deriveKey(
      secretKey: cryptography.SecretKey(passPhrase.codeUnits),
      nonce: salt,
    );

    return await secretKey.extractBytes();
  }
}
