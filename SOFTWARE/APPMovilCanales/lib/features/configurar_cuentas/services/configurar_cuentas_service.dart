import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/models/actualizar_alias_response.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class ConfigurarCuentasService {
  static Future<DatosInicialesResponse> obtenerDatosIniciales() async {
    try {
      final response =
          await api.get('/api/ventanillas/cancelaciones/datosiniciales');

      return DatosInicialesResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los datos.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ActualizarAliasResponse> actualizarAlias({
    required String alias,
    required String? codigoSistema,
    required String? numeroProducto,
  }) async {
    try {
      Map<String, dynamic> form = {
        "NombreAlias": alias,
        "CodigoSistema": codigoSistema,
        "NumeroProducto": numeroProducto,
      };

      final response = await api.post(
          '/api/alias-producto-cliente/agregar-actualizar', form);

      return ActualizarAliasResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al actualizar el alias.', e);
      throw ServiceException(errorMessage);
    }
  }
}
