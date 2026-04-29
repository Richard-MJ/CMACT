import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/cambio_clave/models/cambiar_clave_response.dart';
import 'package:caja_tacna_app/features/cambio_clave/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class CambioClaveService {
  //servicio implementado siguiendo la documentacion v.8.1
  static Future<CambiarClaveResponse> cambiarClave({
    required String? identificadorDispositivo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "IdentificadorDispositivo": identificadorDispositivo
      };

      final response = await api.post(
          '/api/clientes/cambio-clave-internet/confirmacion', form);
      return CambiarClaveResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al cambiar la clave.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.8.1
  static Future<ConfirmarResponse> confirmar({
    required String tokenDigital,
    required int? idTipoDocumento,
    required String? numeroDocumento,
    required String passwordAntiguo,
    required String passwordNuevo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoAutorizacion": tokenDigital,
        "IdTipoDocumento": idTipoDocumento,
        "NumeroDocumento": numeroDocumento,
        "PasswordPrimario": passwordAntiguo,
        "PasswordInternet": passwordNuevo
      };

      final response =
          await api.post('/api/clientes/cambio-clave-internet', form);
      return ConfirmarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar el cambio de clave.', e);
      throw ServiceException(errorMessage);
    }
  }
}
