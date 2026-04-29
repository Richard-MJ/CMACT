import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/adelanto_sueldo/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/adelanto_sueldo/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/adelanto_sueldo/models/pagar_response.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class AdelantoSueldoService {
  //servicio implementado siguiendo la documentacion v.7.1
  static Future<DatosInicialesResponse> obtenerDatosIniciales() async {
    try {
      final response =
          await api.get('/api/ventanillas/adelantosueldo/obtenerafiliacion');

      return DatosInicialesResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los datos.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.7.1
  static Future<PagarResponse> pagar({
    required String? numeroCuentaDestino,
    required String? codigoMonedaOperacion,
    required String montoOperacion,
    required String? identificadorDispositivo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "NumeroCuenta": numeroCuentaDestino,
        "MontoOperacion": montoOperacion,
        "CodigoMoneda": codigoMonedaOperacion,
        "IdentificadorDispositivo": identificadorDispositivo
      };

      final response =
          await api.post('/api/ventanillas/adelantosueldo/confirmacion', form);
      return PagarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage =
          ErrorService.verificarErrorBase('Ocurrió un error al pagar.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.7.1
  static Future<ConfirmarResponse> confirmar({
    required String? numeroCuentaDestino,
    required String montoOperacion,
    required String tokenDigital,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoAutorizacion": tokenDigital,
        "NumeroCuenta": numeroCuentaDestino,
        "MontoOperacion": montoOperacion
      };

      final response = await api.post(
          '/api/ventanillas/adelantosueldo/realizaradelantosueldo', form);
      return ConfirmarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar el pago.', e);
      throw ServiceException(errorMessage);
    }
  }
}
