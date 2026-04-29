import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/compras_internet/models/afiliacion.dart';
import 'package:caja_tacna_app/features/compras_internet/models/afiliar_response.dart';
import 'package:caja_tacna_app/features/compras_internet/models/confirmar_afiliacion_response.dart';
import 'package:caja_tacna_app/features/compras_internet/models/confirmar_desafiliacion_response.dart';
import 'package:caja_tacna_app/features/compras_internet/models/cuenta_afiliacion.dart';
import 'package:caja_tacna_app/features/compras_internet/models/desafiliar_response.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class ComprasInternetService {
  //servicio implementado siguiendo la documentacion v.7.1
  static Future<List<CuentaAfiliacion>> obtenerCuentas() async {
    try {
      final response = await api
          .get('/api/compras-por-internet/cuentas/?indicadorGrupo=VS_CI');

      return List<CuentaAfiliacion>.from(
          response.data.map((x) => CuentaAfiliacion.fromJson(x)));
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener las cuentas.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.7.1
  static Future<Afiliacion> obtenerAfiliacion() async {
    try {
      final response = await api.get('/api/compras-por-internet');

      return Afiliacion.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener la afiliacion.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.7.1
  static Future<AfiliarResponse> afiliar({
    required String? codigoMoneda,
    required String? identificadorDispositivo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoMonedaCuenta": codigoMoneda,
        "IdentificadorDispositivo": identificadorDispositivo
      };

      final response = await api.post(
          '/api/compras-por-internet/afiliacion/confirmacion', form);
      return AfiliarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage =
          ErrorService.verificarErrorBase('Ocurrió un error al afiliar.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.7.1
  static Future<ConfirmarAfiliacionResponse> confirmarAfiliacion({
    required String? numeroCuenta,
    required String? codigoMoneda,
    required String tokenDigital,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoAutorizacion": tokenDigital,
        "NumeroCuenta": numeroCuenta,
        "CodigoMonedaCuenta": codigoMoneda,
      };

      final response =
          await api.post('/api/compras-por-internet/afiliacion', form);
      return ConfirmarAfiliacionResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar la afiliación.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.7.1
  static Future<DesafiliarResponse> desafiliar({
    required String? codigoMoneda,
    required String? identificadorDispositivo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoMonedaCuenta": codigoMoneda,
        "IdentificadorDispositivo": identificadorDispositivo
      };

      final response = await api.post(
          '/api/compras-por-internet/desafiliacion/confirmacion', form);
      return DesafiliarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage =
          ErrorService.verificarErrorBase('Ocurrió un error al desafiliar.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.8.0
  static Future<ConfirmarDesafiliacionResponse> confirmarDesafiliacion({
    required String? codigoMoneda,
    required String tokenDigital,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoAutorizacion": tokenDigital,
        "CodigoMonedaCuenta": codigoMoneda,
      };

      final response =
          await api.delete('/api/compras-por-internet/afiliacion', data: form);
      return ConfirmarDesafiliacionResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar la desafiliación.', e);
      throw ServiceException(errorMessage);
    }
  }
}
