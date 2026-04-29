import 'package:flutter/services.dart';
import 'package:local_auth/local_auth.dart';
import 'package:local_auth_android/local_auth_android.dart';
import 'package:local_auth_darwin/local_auth_darwin.dart';
import 'package:local_auth/error_codes.dart' as auth_error;

class LocalAuthPlugin {
  static final LocalAuthentication auth = LocalAuthentication();

  static Future<List<BiometricType>> availableBiometrics() async {
    final List<BiometricType> availableBiometrics =
        await auth.getAvailableBiometrics();
    return availableBiometrics;
  }

  static Future<bool> dispositivoConBiometricos() async {
    //solo indica si hay soporte de hardware disponible, no si el dispositivo tiene datos biométricos registrados.
    return await auth.canCheckBiometrics;
  }

  static Future<(bool, String)> autheticate() async {
    try {
      final bool didAuthenticate = await auth.authenticate(
          localizedReason: 'Toca el sensor de huellas para continuar.',
          options: const AuthenticationOptions(
            biometricOnly: true,
          ),
          authMessages: const <AuthMessages>[
            AndroidAuthMessages(
                signInTitle: 'Lector de huella para Caja Tacna App',
                cancelButton: 'Cancelar',
                biometricHint: ''),
          ]);
      return (
        didAuthenticate,
        didAuthenticate ? 'Hecho' : 'Cancelado por el usuario'
      );
    } on PlatformException catch (e) {
      if (e.code == auth_error.notEnrolled) {
        return (false, 'No hay biometricos disponibles');
      }

      if (e.code == auth_error.lockedOut) {
        return (false, 'Muchos intentos fallidos');
      }

      if (e.code == auth_error.notAvailable) {
        return (false, 'Cancelado por el usuario');
      }

      if (e.code == auth_error.passcodeNotSet) {
        return (false, 'No hay un PIN configurado');
      }

      if (e.code == auth_error.permanentlyLockedOut) {
        return (false, 'Se requiere desbloquear el telefono');
      }

      return (false, e.toString());
    }
  }
}
