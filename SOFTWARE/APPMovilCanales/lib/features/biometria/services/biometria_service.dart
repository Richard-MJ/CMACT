import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/constants/storage_keys.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/biometria/models/afiliacion_biometrica.dart';
import 'package:caja_tacna_app/features/biometria/models/afiliar_response.dart';
import 'package:caja_tacna_app/features/biometria/models/confirmar_afiliacion_response.dart';
import 'package:caja_tacna_app/features/biometria/models/confirmar_desafiliacion_response.dart';
import 'package:caja_tacna_app/features/biometria/models/password_biometrico.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/services/storage_service.dart';

final api = Api();

class BiometriaService {
  //servicio implementado siguiendo la documentacion v.8.1
  static Future<List<AfiliacionBiometrica>> obtenerAfiliaciones({
    required String? identificadorDispositivo,
  }) async {
    try {
      final response =
          await api.get('/api/biometria-app/$identificadorDispositivo');

      return List<AfiliacionBiometrica>.from(
          response.data.map((x) => AfiliacionBiometrica.fromJson(x)));
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener las afiliaciones.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.8.1
  static Future<AfiliarResponse> afiliarDesafiliar({
    required String? identificadorDispositivo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "IdentificadorDispositivo": identificadorDispositivo
      };

      final response = await api.post('/api/biometria-app/confirmar', form);
      return AfiliarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage =
          ErrorService.verificarErrorBase('Ocurrió un error al afiliar.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.8.1
  static Future<ConfirmarAfiliacionResponse> confirmarAfiliacion({
    required String tokenDigital,
    required int codigoTipoBiometria,
    required String? identificadorDispositivo,
    required String? guid,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoAutorizacion": tokenDigital,
        "CodigoTipoBiometria": codigoTipoBiometria,
        "IdentificadorDispositivo": identificadorDispositivo,
        "ListaDispositivos": [guid]
      };

      final response = await api.post('/api/biometria-app', form);
      return ConfirmarAfiliacionResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar la afiliación.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ConfirmarDesafiliacionResponse> confirmarDesafiliacion({
    required String tokenDigital,
    required int? numeroAfiliacionBiometria,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoAutorizacion": tokenDigital,
        "NumeroAfiliacionBiometria": numeroAfiliacionBiometria
      };

      final response = await api.put('/api/biometria-app/desafiliacion', form);
      return ConfirmarDesafiliacionResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar la desafiliación.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<List<PasswordBiometrico>> getPasswordsBiometricos() async {
    final passwordsBiometricos = await StorageService()
        .get<List<dynamic>>(StorageKeys.passwordsBiometricos);
    if (passwordsBiometricos != null) {
      return List<PasswordBiometrico>.from(
          passwordsBiometricos.map((x) => PasswordBiometrico.fromJson(x)));
    }

    return [];
  }

  static Future<void> addPasswordBiometrico(
    PasswordBiometrico passwordBiometrico,
  ) async {
    final List<PasswordBiometrico> passwordsBiometricos =
        await BiometriaService.getPasswordsBiometricos();

    final List<PasswordBiometrico> nuevosPasswordsBiometricos = [
      ...passwordsBiometricos
          .where((p) => p.numeroTarjeta != passwordBiometrico.numeroTarjeta),
      passwordBiometrico,
    ];

    await StorageService().set<List<dynamic>>(
        StorageKeys.passwordsBiometricos, nuevosPasswordsBiometricos);
  }

  static Future<PasswordBiometrico?> getPasswordBiometrico(
    String numeroTarjeta,
  ) async {
    final List<PasswordBiometrico> passwordsBiometricos =
        await getPasswordsBiometricos();

    final int indexPassword = passwordsBiometricos
        .indexWhere((p) => p.numeroTarjeta == numeroTarjeta);
    if (indexPassword >= 0) {
      return passwordsBiometricos[indexPassword];
    }

    return null;
  }

  static Future<void> deletePasswordBiometrico(
    String numeroTarjeta,
  ) async {
    final List<PasswordBiometrico> passwordsBiometricos =
        await BiometriaService.getPasswordsBiometricos();

    final List<PasswordBiometrico> nuevosPasswordsBiometricos = [
      ...passwordsBiometricos.where((p) => p.numeroTarjeta != numeroTarjeta),
    ];

    await StorageService().set<List<dynamic>>(
      StorageKeys.passwordsBiometricos,
      nuevosPasswordsBiometricos,
    );
  }
}
