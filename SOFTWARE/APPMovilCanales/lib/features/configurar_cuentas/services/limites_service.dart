import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/models/actualizar_limite_response.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/models/confirmar_limite_response.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/models/limites_response.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class LimitesService {
  static Future<LimitesResponse> obtenerLimites(
      {required String? numeroCuenta}) async {
    try {
      final response = await api
          .get('/api/personalizacion-producto-cliente/limites/$numeroCuenta');

      return LimitesResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los datos.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ActualizarLimiteResponse> actualizarLimite({
    required String? numeroCuenta,
    required int codigoTipoLimite,
    required String? valorLimite,
    required String? identificadorDispositivo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "NumeroCuenta": numeroCuenta,
        "CodigoTipoLimite": codigoTipoLimite,
        "ValorLimite": valorLimite,
        "IdentificadorDispositivo": identificadorDispositivo,
      };

      final response = await api.post(
          '/api/personalizacion-producto-cliente/confirmacion', form);

      return ActualizarLimiteResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage =
          ErrorService.verificarErrorBase('Ocurrió un error al actualizar.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ConfirmarLimiteResponse> confirmarLimite({
    required String? numeroCuenta,
    required int codigoTipoLimite,
    required String? valorLimite,
    required String tokenDigital,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoAutorizacion": tokenDigital,
        "NumeroCuenta": numeroCuenta,
        "CodigoTipoLimite": codigoTipoLimite,
        "ValorLimite": valorLimite,
      };

      final response = await api.post(
          '/api/personalizacion-producto-cliente/realizar', form);

      return ConfirmarLimiteResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage =
          ErrorService.verificarErrorBase('Ocurrió un error al confirmar.', e);
      throw ServiceException(errorMessage);
    }
  }
}
