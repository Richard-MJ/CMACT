import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/anulacion_tarjetas/models/anular_response.dart';
import 'package:caja_tacna_app/features/anulacion_tarjetas/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/anulacion_tarjetas/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class AnulacionTarjetasService {
  static Future<DatosInicialesResponse> obtenerDatosIniciales() async {
    try {
      final response = await api.get('/api/tarjetas/anulacion/datosiniciales');

      return DatosInicialesResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los datos.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<AnularResponse> anular({
    required String? numeroTarjeta,
    required String? identificadorDispositivo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "NumeroTarjeta": numeroTarjeta,
        "IdentificadorDispositivo": identificadorDispositivo,
      };

      final response = await api.post(
        '/api/tarjetas/anulacion/confirmacion',
        form,
      );

      return AnularResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al anular la tarjeta.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ConfirmarResponse> confirmar({
    required String tokenDigital,
    required String? numeroTarjeta,
    required String? codigoMotivoAnulacion,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoAutorizacion": tokenDigital,
        "NumeroTarjeta": numeroTarjeta,
        "CodigoMotivoAnulacion": codigoMotivoAnulacion,
      };

      final response = await api.post(
        '/api/tarjetas/anulacion/anular',
        form,
      );

      return ConfirmarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar la anulación.', e);
      throw ServiceException(errorMessage);
    }
  }
}
