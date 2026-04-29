import 'package:flutter_dotenv/flutter_dotenv.dart';

class Environment {
  static Future<void> initEnvironment() async {
    await dotenv.load(fileName: ".env");
  }

  static String urlAuth =
      dotenv.env['URL_AUTH'] ?? 'No esta configurado el URL_AUTH';

  static String urlBase =
      dotenv.env['URL_BASE'] ?? 'No esta configurado el URL_BASE';

  static bool serviciosPuente =
      dotenv.env['SERVICIOS_PUENTE'] == 'true' ? true : false;

  static bool encriptacion =
      dotenv.env['ENCRIPTACION'] == 'true' ? true : false;

  static String keyEncriptacion = dotenv.env['KEY_ENCRIPTACION'] ??
      'No esta configurado el KEY_ENCRIPTACION';

  static bool dispositivoSeguro =
      dotenv.env['DISPOSITIVO_SEGURO'] == 'true' ? true : false;

  static bool validarCertificado =
      dotenv.env['VALIDAR_CERTIFICADO'] == 'true' ? true : false;

  static String shaFingerprint =
      dotenv.env['SHA_FINGERPRINT'] ?? 'No esta configurado el SHA_FINGERPRINT';
}
