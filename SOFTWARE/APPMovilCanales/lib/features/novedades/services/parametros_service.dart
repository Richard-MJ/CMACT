import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/novedades/models/categoria_tipo_aviso.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class ParametrosService {
  static Future<List<CategoriaTipoAviso>> obtenerCategoriasPorTipoAviso({
    required int tipoAviso,
  }) async {
    try {
      final response = await api.get(
        '/api/v1/parametros/categorias',
        queryParameters: {'tipo_aviso': tipoAviso},
      );

      return List<CategoriaTipoAviso>.from(
          response.data.map((x) => CategoriaTipoAviso.fromJson(x)));
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener las categorias.', e);
      throw ServiceException(errorMessage);
    }
  }
}
