import 'dart:convert';
import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/constants/storage_keys.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/models/enviar_sms_response.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/models/login_response.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/models/validar_sms_response.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/services/storage_service.dart';

final api = Api();
final storageService = StorageService();

class LoginService {
  static Future<LoginResponse> login({
    required String numeroTarjeta,
    required String password,
    required String numeroDocumento,
    required int? idTipoDocumento,
    required int idTipoOperacionCanalElectronico,
    required String guid,
    required String? modeloDispositivo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "grant_type": "password",
        "username": numeroTarjeta,
        "password": password,
        "client_id": '85974641fa9942d58b0c669b3cd2b29c',
        "terminal": "ND",
        "numeroDocumento": numeroDocumento,
        "IdTipoDocumento": idTipoDocumento,
        "IdTipoOperacionCanalElectronico": idTipoOperacionCanalElectronico,
        "guids": jsonEncode([guid]),
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

      return LoginResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorAuth(
          'Ocurrió un error al iniciar sesión.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<EnviarSmsResponse> enviarSms() async {
    try {
      final responseSms = await api.get('/api/clientes/datos/confirmacion/5');

      return EnviarSmsResponse.fromJson(responseSms.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al enviar el sms.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ValidarSmsResponse> validarSms(
      {required int? idVerificacion,
      required String claveSms,
      required String guid,
      required String idVisual}) async {
    try {
      Map<String, dynamic> form = {
        "idVerificacion": idVerificacion,
        "codigoAutorizacion": claveSms,
        "idVisual": idVisual,
        "newGuid": guid
      };

      final response =
          await api.postAuth('/oauth2/token/verificar-autorizacion', form);

      return ValidarSmsResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage =
          ErrorService.verificarErrorAuth('Ocurrió un error al validar.', e);
      throw ServiceException(errorMessage);
    }
  }
}
