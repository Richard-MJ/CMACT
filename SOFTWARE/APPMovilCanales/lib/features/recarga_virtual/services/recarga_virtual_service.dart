import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/recarga_virtual/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/recarga_virtual/models/pagar_response.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/recarga_virtual/models/datos_iniciales_response.dart';

final api = Api();

class RecargaVirtualService {
  static Future<DatosInicialesResponse> obtenerDatosIniciales() async {
    try {
      final response =
          await api.get('/api/ventanillas/recargasvirtuales/datosiniciales');

      return DatosInicialesResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los datos.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<PagarResponse> pagar({
    required String? numeroCuentaAhorros,
    required String? codigoMonedaOrigen,
    required String? codigoOperador,
    required String montoRecarga,
    required String numeroCelular,
    required String? identificadorDispositivo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "NumeroCuentaAhorrosADebitar": numeroCuentaAhorros,
        "CodigoMonedaOrigen": codigoMonedaOrigen,
        "CodigoOperador": codigoOperador,
        "MontoRecarga": montoRecarga,
        "NumeroCelular": numeroCelular,
        "IdentificadorDispositivo": identificadorDispositivo,
      };

      final response = await api.post(
        '/api/ventanillas/recargasvirtuales/confirmacion',
        form,
      );

      return PagarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al realizar la recarga.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicios implementados segun documentacion v7
  static Future<ConfirmarResponse> confirmar({
    required String tokenDigital,
    required String? numeroCuentaAhorros,
    required String? codigoMonedaOrigen,
    required String? codigoOperador,
    required String montoRecarga,
    required String numeroCelular,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoAutorizacion": tokenDigital,
        "NumeroCuentaAhorrosADebitar": numeroCuentaAhorros,
        "CodigoMonedaOrigen": codigoMonedaOrigen,
        "CodigoOperador": codigoOperador,
        "MontoRecarga": montoRecarga,
        "NumeroCelular": numeroCelular,
      };

      final response = await api.post(
        '/api/ventanillas/recargasvirtuales/recargar',
        form,
      );

      return ConfirmarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar la recarga.', e);
      throw ServiceException(errorMessage);
    }
  }
}
