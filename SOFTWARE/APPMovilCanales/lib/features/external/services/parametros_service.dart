import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/constants/canal_origen.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/home/models/configuracion.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class ParametrosService {
  static Future<List<EnlaceDocumento>?> obtenerParametros() async {
    try {
      final response = await api.get(
        '/api/v1/parametros/enlaces-externos',
        queryParameters: {
          "indicador_canal": CanalOrigen.appMovil,
        },
      );

      return List<EnlaceDocumento>.from(response.data.map((x) => EnlaceDocumento.fromJson(x)));
    } catch (e) {
      String errorMessage = 'Ocurrió un error al obtener los parámetros.';
      errorMessage = ErrorService.verificarErrorBase(errorMessage, e);
      throw ServiceException(errorMessage);
    }
  }
}
