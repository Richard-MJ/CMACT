import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/pago_safetypay/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/pago_safetypay/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/pago_safetypay/models/deuda.dart';
import 'package:caja_tacna_app/features/pago_safetypay/models/pagar_response.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class PagoSafetypayService {
  //servicio implementado siguiendo la documentacion v.7.1
  static Future<DatosInicialesResponse> obtenerDatosIniciales() async {
    try {
      final response =
          await api.get('/api/ventanillas/safetypay/datosiniciales');

      return DatosInicialesResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los datos.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.7.1
  static Future<Deuda> obtenerDeuda({
    required String codigoPago,
  }) async {
    try {
      final response =
          await api.get('/api/ventanillas/safetypay/deuda/$codigoPago');

      return Deuda.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los datos.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.7.1
  static Future<PagarResponse> pagar({
    required String? codigoTransaccion,
    required String? numeroCuentaOrigen,
    required String? codigoMonedaOperacion,
    required double? montoOperacion,
    required String? identificadorDispositivo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoTransaccion": codigoTransaccion,
        "NumeroCuentaAhorros": numeroCuentaOrigen,
        "CodigoMonedaOperacion": codigoMonedaOperacion,
        "MontoOperacion": montoOperacion,
        "IdentificadorDispositivo": identificadorDispositivo
      };

      final response =
          await api.post('/api/ventanillas/safetypay/confirmacion', form);
      return PagarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage =
          ErrorService.verificarErrorBase('Ocurrió un error al pagar.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.7.1
  static Future<ConfirmarResponse> confirmar({
    required String? codigoTransaccion,
    required String? numeroCuentaOrigen,
    required String? codigoMonedaOperacion,
    required double? montoOperacion,
    required String tokenDigital,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoAutorizacion": tokenDigital,
        "CodigoTransaccion": codigoTransaccion,
        "NumeroCuentaAhorros": numeroCuentaOrigen,
        "CodigoMonedaOperacion": codigoMonedaOperacion,
        "MontoOperacion": montoOperacion
      };

      final response = await api.post('/api/ventanillas/safetypay/pago', form);
      return ConfirmarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar el pago.', e);
      throw ServiceException(errorMessage);
    }
  }
}
