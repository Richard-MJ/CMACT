import 'package:biometric_storage/biometric_storage.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

class BiometricStoragePlugin {
  static const PromptInfo _promptInfo = PromptInfo(
    androidPromptInfo: AndroidPromptInfo(
      title: 'Lector de huella para Caja Tacna App',
      description: 'Toca el sensor de huellas para continuar.',
      negativeButton: 'Cancelar',
    ),
  );

  static Future<void> setTokenBiometrico(
    String token,
    String numeroTarjeta,
  ) async {
    final response = await BiometricStorage().canAuthenticate();
    if (response != CanAuthenticateResponse.success) return;

    final BiometricStorageFile store = await BiometricStorage().getStorage(
      numeroTarjeta,
      promptInfo: _promptInfo,
    );

    try {
      await store.write(token);
    } catch (e) {
      if (e is AuthException) {
        throw ServiceException(e.message);
      }

      throw ServiceException('Ocurrió un error');
    }
  }

  static Future<String?> getTokenBiometrico(
    String numeroTarjeta,
  ) async {
    final response = await BiometricStorage().canAuthenticate();
    if (response != CanAuthenticateResponse.success) return null;

    final BiometricStorageFile store = await BiometricStorage().getStorage(
      numeroTarjeta,
      promptInfo: _promptInfo,
    );

    try {
      //regresará null cuando detecte que hay una huella digital adicional a las que habian cuando se registró
      final String? token = await store.read();
      return token;
    } catch (e) {
      if (e is AuthException) {
        throw ServiceException(e.message);
      }

      throw ServiceException('Ocurrió un error');
    }
  }
}
