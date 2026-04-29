import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/novedades/models/novedad.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class NovedadesService {
  static Future<List<Novedad>> obtenerNovedadesPorCategoria({
    required int categoria,
  }) async {
    try {
      final response = await api.get(
        '/api/v1/novedad/by-categoria',
        queryParameters: {'id_categoria': categoria},
      );

      return List<Novedad>.from(response.data.map((x) => Novedad.fromJson(x)));
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener las novedades.', e);
      throw ServiceException(errorMessage);
    }
  }
}
