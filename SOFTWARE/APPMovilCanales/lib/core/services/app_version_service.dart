import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/models/app_version_response.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class AppVersionService {
  static Future<AppVersionResponse> obtenerVersionamiento({
    required int proyectoId,
  }) async {
    try {
      final response = await api.get(
        '/api/v1/versionamiento/by-proyecto',
        queryParameters: {
          "proyecto_id": proyectoId,
        },
      );

      return AppVersionResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = 'Ocurrió un error al obtener el versionamiento.';
      errorMessage = ErrorService.verificarErrorBase(errorMessage, e);
      throw ServiceException(errorMessage);
    }
  }
}
