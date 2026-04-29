import 'dart:convert';
import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/constants/storage_keys.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/models/crear_clave_response.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/models/eviar_sms_response.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/models/validar_sms_response.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/services/storage_service.dart';

final api = Api();
final storageService = StorageService();

class AfiliacionCanalesService {
  static Future<CrearClaveResponse> crearClave({
    required String numeroTarjeta,
    required String claveInternet,
    required String claveTarjeta,
    required String numeroDocumento,
    required int? idTipoDocumento,
    required String? modeloDispositivo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "grant_type": "password",
        "username": numeroTarjeta,
        "password": claveInternet,
        "passwordPrimario": claveTarjeta,
        "client_id": '85974641fa9942d58b0c669b3cd2b29c',
        "terminal": "ND",
        "numeroDocumento": numeroDocumento,
        "IdTipoDocumento": idTipoDocumento,
        "IdTipoOperacionCanalElectronico": 2,
        "guids": jsonEncode([]),
        "modeloDispositivo": modeloDispositivo,
      };

      final response = await api.postAuth('/oauth2/token', form);

      //almacenamos los headers x-sp y x-ua que llegan de la respuesta
      if (response.headers['x-sp'] != null) {
        await storageService.set<String>(
            StorageKeys.xsp, response.headers['x-sp']![0]);
      }

      if (response.headers['x-ua'] != null) {
        await storageService.set<String>(
            StorageKeys.xua, response.headers['x-ua']![0]);
      }

      return CrearClaveResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorAuth(
          'Ocurrió un error al crear la clave.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<EnviarSmsResponse> enviarSms() async {
    try {
      final response = await api.get('/api/clientes/datos/confirmacion/3');
      return EnviarSmsResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al enviar el sms.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ValidarSmsResponse> validarSms({
    required int? idVerificacion,
    required String claveSms,
  }) async {
    try {
      Map<String, dynamic> form = {
        "IdVerificacion": idVerificacion,
        "CodigoAutorizacion": claveSms,
      };

      final response = await api.post('/api/clientes/datos/confirmacion', form);

      return ValidarSmsResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage =
          ErrorService.verificarErrorBase('Ocurrió un error al validar.', e);
      throw ServiceException(errorMessage);
    }
  }
}
