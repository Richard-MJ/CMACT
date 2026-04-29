import 'dart:math';
import 'dart:typed_data';
import 'package:caja_tacna_app/constants/environment.dart';
import 'package:caja_tacna_app/core/models/encrypt_response.dart';
import 'package:crypto/crypto.dart';
import 'dart:convert';
import 'package:cryptography/cryptography.dart' as cryptography;

final String keyEncriptacion = Environment.keyEncriptacion;

class EncryptService {

  static Future<Map<String, dynamic>> encrypt(String text) async {
    Uint8List salt = _generateRandomBytes(32);

    final key = await _deriveKey(salt);
    final nonce = _generateRandomBytes(12);

    final algorithm = cryptography.AesGcm.with256bits();
    final secretKey = cryptography.SecretKey(key);

    final secretBox = await algorithm.encrypt(
      utf8.encode(text),
      secretKey: secretKey,
      nonce: nonce,
    );

    return {
      "ct": base64.encode(secretBox.cipherText),
      "iv": base64.encode(nonce),
      "s": base64.encode(salt),
      "tag": base64.encode(secretBox.mac.bytes),
    };
  }

  static Future<dynamic> decrypt(EncrypResponse encrypResponse) async {
    final decrypted = await _decryptRaw(encrypResponse);
    return _parseDecrypted(decrypted);
  }

  static Future<String> _decryptRaw(EncrypResponse encrypResponse) async {
    Uint8List salt = base64.decode(encrypResponse.s);
    Uint8List nonce = base64.decode(encrypResponse.iv);
    Uint8List cipherText = base64.decode(encrypResponse.ct);
    Uint8List tag = base64.decode(encrypResponse.tag!);

    final key = await _deriveKey(salt);

    final algorithm = cryptography.AesGcm.with256bits();
    final secretKey = cryptography.SecretKey(key);

    final secretBox = cryptography.SecretBox(
      cipherText,
      nonce: nonce,
      mac: cryptography.Mac(tag),
    );

    final decryptedBytes = await algorithm.decrypt(
      secretBox,
      secretKey: secretKey,
    );

    return utf8.decode(decryptedBytes);
  }

  @Deprecated('CBC legacy: eliminar cuando todos los usuarios hayan migrado a GCM')
  static Future<dynamic> decrypLegacyCBC(EncrypResponse encrypResponse) async {
    final decrypted = await _decryptRawLegacyCBC(encrypResponse);
    return _parseDecrypted(decrypted);
  }

  @Deprecated('CBC legacy: eliminar cuando todos los usuarios hayan migrado a GCM')
  static Future<String> _decryptRawLegacyCBC(EncrypResponse encrypResponse) async {
    Uint8List salt = base64.decode(encrypResponse.s);
    Uint8List iv = base64.decode(encrypResponse.iv);
    Uint8List cipherText = base64.decode(encrypResponse.ct);

    final key = await _deriveKey(salt);

    final algorithm = cryptography.AesCbc.with256bits(
      macAlgorithm: cryptography.MacAlgorithm.empty,
    );
    final secretKey = cryptography.SecretKey(key);

    final secretBox = cryptography.SecretBox(
      cipherText,
      nonce: iv,
      mac: cryptography.Mac.empty,
    );

    final decryptedBytes = await algorithm.decrypt(
      secretBox,
      secretKey: secretKey,
    );

    final padLength = decryptedBytes.last;
    final unpadded = decryptedBytes.sublist(0, decryptedBytes.length - padLength);

    return utf8.decode(unpadded);
  }

  static dynamic _parseDecrypted(String decrypted) {
    try {
      if (decrypted.contains('{') || decrypted.contains('[')) {
        return jsonDecode(decrypted);
      }
      if (decrypted == 'true') return true;
      if (decrypted == 'false') return false;
      if (decrypted == 'null') return null;
      return decrypted;
    } catch (e) {
      return decrypted;
    }
  }

  static Future<List<int>> _deriveKey(Uint8List salt) async {
    final pbkdf2 = cryptography.Pbkdf2(
      macAlgorithm: cryptography.Hmac.sha256(),
      iterations: 1000,
      bits: 256,
    );

    final newSecretKey = await pbkdf2.deriveKey(
      secretKey: cryptography.SecretKey(keyEncriptacion.codeUnits),
      nonce: salt,
    );

    return await newSecretKey.extractBytes();
  }

  static Uint8List _generateRandomBytes(int length) {
    var random = Random.secure();
    var values = List<int>.generate(length, (index) => random.nextInt(256));
    return Uint8List.fromList(values);
  }

  static String calculateSHA256(String data) {
    List<int> bytes = utf8.encode(data);
    Digest sha256Result = sha256.convert(bytes);
    return sha256Result.toString();
  }
}
