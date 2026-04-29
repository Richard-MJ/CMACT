import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/novedades/models/obtener_publicidad_response.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class PublicidadService {
  static Future<List<Publicidad>> obtenerPublicidadCategoria({
    required int categoria,
  }) async {
    try {
      final response =
          await api.get('/api/v1/publicidad/by-categoria', queryParameters: {
        'id_categoria': categoria,
      });

      return List<Publicidad>.from(
          response.data.map((x) => Publicidad.fromJson(x)));
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener las publicidades.', e);
      throw ServiceException(errorMessage);
    }
  }
}
