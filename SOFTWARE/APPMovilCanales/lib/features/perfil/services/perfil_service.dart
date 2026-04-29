import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/perfil/models/actualizar_response.dart';
import 'package:caja_tacna_app/features/perfil/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/perfil/models/obtener_datos_response.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class PerfilService {
  static Future<ObtenerDatosResponse> obtenerDatos() async {
    try {
      final response = await api.get('/api/clientes/datos/persona-fisica');

      return ObtenerDatosResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al iniciar el pago.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ActualizarResponse> actualizar({
    required String? correoElectronico,
    required String? numeroCelular,
    required String? identificadorDispositivo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "DireccionCorreoElectronico": correoElectronico,
        "NumeroTelefonoCasa": numeroCelular,
        "IdentificadorDispositivo": identificadorDispositivo,
      };

      final response = await api.post(
        '/api/clientes/datos/persona-fisica/confirmacion',
        form,
      );

      return ActualizarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al actualizar los datos.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicios implementados segun documentacion v8.1
  static Future<ConfirmarResponse> confirmar({
    required String tokenDigital,
    required String? correoElectronico,
    required String? numeroCelular,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoAutorizacion": tokenDigital,
        "DireccionCorreoElectronico": correoElectronico,
        "NumeroTelefonoCasa": numeroCelular,
      };

      final response = await api.put(
        '/api/clientes/datos/persona-fisica/realizar',
        form,
      );

      return ConfirmarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar la actualización.', e);
      throw ServiceException(errorMessage);
    }
  }
}
