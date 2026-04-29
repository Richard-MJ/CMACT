import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/models/enviar_sms_response.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/token_digital/models/obtener_dispositivo_response.dart';
import 'package:caja_tacna_app/features/token_digital/models/obtener_token_response.dart';

final api = Api();

class TokenDigitalService {
  //servicio implementado siguiendo la documentacion v.8.2
  static Future<ObtenerDispositivoResponse?>
      obtenerDispositivoAfiliado() async {
    try {
      final response = await api.get('/api/token-digital/obtener-dispositivo');

      //retornara nulo cuando no esta afiliado.
      if (response.data == null) return null;
      return ObtenerDispositivoResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener el dispositivo.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.8.1
  static Future<ObtenerTokenResponse> obtenerToken({
    required String? identificadorDispositivo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "IdentificadorDispositivo": identificadorDispositivo
      };

      final response = await api.post('/api/clientes/datos/tokendigital', form);

      return ObtenerTokenResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener el token.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ObtenerDispositivoResponse> afiliarNuevoDispositivo({
    required String deviceModel,
    required String deviceManufacturer,
    required String deviceName,
    required String devicePlatform,
    required String deviceIdiom,
    required String deviceType,
    required String? deviceUuid,
    required String claveTarjeta,
    required String claveInternet,
  }) async {
    try {
      Map<String, dynamic> form = {
        "device_model": deviceModel,
        "device_manufacturer": deviceManufacturer,
        "device_name": deviceName,
        "device_platform": devicePlatform,
        "device_idiom": deviceIdiom,
        "device_type": deviceType,
        "device_uuid": deviceUuid,
        "ClaveTarjeta": claveTarjeta,
        "ClaveInternet": claveInternet,
      };

      final response =
          await api.post('/api/token-digital/afiliar-dispositivo', form);
      return ObtenerDispositivoResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage =
          ErrorService.verificarErrorBase('Ocurrió un error al afiliarse.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.8.1
  static Future<ObtenerDispositivoResponse> reafiliar({
    required ObtenerDispositivoResponse? obtenerDispositivoResponse,
    required String? deviceUuid,
    required String claveTarjeta,
    required String claveInternet,
  }) async {
    try {
      Map<String, dynamic> form = {
        "id": obtenerDispositivoResponse?.id,
        "device_uuid": deviceUuid,
        "ClaveTarjeta": claveTarjeta,
        "ClaveInternet": claveInternet,
      };

      final response =
          await api.post('/api/token-digital/afiliar-dispositivo', form);
      return ObtenerDispositivoResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage =
          ErrorService.verificarErrorBase('Ocurrió un error al afiliarse.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.8.1
  static Future<ObtenerDispositivoResponse> desafiliar({
    required ObtenerDispositivoResponse? obtenerDispositivoResponse,
    required String claveTarjeta,
    required String claveInternet,
    required String? deviceUuid,
  }) async {
    try {
      Map<String, dynamic> form = {
        "id": obtenerDispositivoResponse?.id,
        "device_uuid": deviceUuid,
        "ClaveTarjeta": claveTarjeta,
        "ClaveInternet": claveInternet,
      };

      final response =
          await api.post('/api/token-digital/desafiliar-dispositivo', form);
      return ObtenerDispositivoResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al desafiliarse.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<EnviarSmsResponse> smsRestablecerTokenDigital() async {
    try {
      final response = await api.get('/api/token-digital/sms-restablecer');
      return EnviarSmsResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al restablecer.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ObtenerDispositivoResponse> restablecerTokenDigital({
    required String deviceModel,
    required String deviceManufacturer,
    required String deviceName,
    required String devicePlatform,
    required String deviceIdiom,
    required String deviceType,
    required String? deviceUuid,
    required String claveTarjeta,
    required String claveInternet,
    required String numeroTarjeta,
    required String fechaVencimiento,
    required String idVerificacion,
    required String codigoAutorizacion,
  }) async {
    try {
      Map<String, dynamic> form = {
        "device_model": deviceModel,
        "device_manufacturer": deviceManufacturer,
        "device_name": deviceName,
        "device_platform": devicePlatform,
        "device_idiom": deviceIdiom,
        "device_type": deviceType,
        "device_uuid": deviceUuid,
        "ClaveTarjeta": claveTarjeta,
        "ClaveInternet": claveInternet,
        "NumeroTarjeta": numeroTarjeta,
        "FechaVencimiento": fechaVencimiento,
        "IdVerificacion": idVerificacion,
        "CodigoAutorizacion": codigoAutorizacion,
      };

      final response = await api.post('/api/token-digital/restablecer', form);
      return ObtenerDispositivoResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al restablecer.', e);
      throw ServiceException(errorMessage);
    }
  }
}
