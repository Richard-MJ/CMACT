import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/models/generar_cip_response.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/models/pagar_app_anticipo_response.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/models/pagar_app_response.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/models/confirmar_response.dart';

final api = Api();

class PagoCreditosPropiosService {
  static Future<DatosInicialesResponse> obtenerDatosIniciales() async {
    try {
      final response =
          await api.get('/api/ventanillas/pagocreditos/datosiniciales');

      return DatosInicialesResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los datos.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<PagarAppResponse> pagar({
    required String? numeroCuentaOrigen,
    required int? numeroCreditoDestino,
    required String monto,
    required bool cancelacionCredito,
    required String? codigoMonedaOrigen,
    required String? codigoMonedaDestino,
    required String? identificadorDispositivo,
  }) async {
    try {
      String url = '';

      if (cancelacionCredito) {
        url = '/api/ventanillas/pagocreditos/confirmacion/cancelacion';
      } else {
        url = '/api/ventanillas/pagocreditos/confirmacion';
      }

      Map<String, dynamic> form = {
        "NumeroCuentaAhorrosADebitar": numeroCuentaOrigen,
        "CodigoMonedaOrigen": codigoMonedaOrigen,
        "NumeroCreditoAbonar": numeroCreditoDestino,
        "CodigoMonedaDestino": codigoMonedaDestino,
        "MontoAbonar": monto,
        "IndicadorCancelacion": cancelacionCredito,
        "IdentificadorDispositivo": identificadorDispositivo
      };

      final response = await api.post(url, form);

      return PagarAppResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al iniciar el pago.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ConfirmarAppResponse> confirmar({
    required String? numeroCuentaOrigen,
    required int? numeroCreditoDestino,
    required String monto,
    required bool cancelacionCredito,
    required String? codigoMonedaOrigen,
    required String? codigoMonedaDestino,
    required String tokenDigital,
  }) async {
    try {
      String url = '';

      if (cancelacionCredito) {
        url = '/api/ventanillas/pagocreditos/cancelacion';
      } else {
        url = '/api/ventanillas/pagocreditos/abono';
      }

      Map<String, dynamic> form = {
        "CodigoAutorizacion": tokenDigital,
        "NumeroCuentaAhorrosADebitar": numeroCuentaOrigen,
        "CodigoMonedaOrigen": codigoMonedaOrigen,
        "NumeroCreditoAbonar": numeroCreditoDestino,
        "CodigoMonedaDestino": codigoMonedaDestino,
        "MontoAbonar": monto
      };

      final response = await api.post(url, form);

      return ConfirmarAppResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar el pago.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<GenerarCipResponse> generarCip({
    required int? numeroCredito,
    required String monto,
    required bool cancelacionCredito,
    required int? numeroCuota,
    required String? numeroTelefono,
    required String? email,
  }) async {
    try {
      Map<String, dynamic> form = {
        "MontoOperacion": monto,
        "NumeroCredito": numeroCredito,
        "TipoOperacionCredito": "Créditos Propios",
        "TipoOperacionPago": cancelacionCredito ? "Cancelacion" : "Abono",
        "NumeroCuota": numeroCuota,
        "EsCancelacionCredito": cancelacionCredito,
        "NumeroTelefono": numeroTelefono,
        "Email": email,
      };

      final response = await api.post('/api/pagos-efectivos', form);

      return GenerarCipResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al generar el cip.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<PagarAppAnticipoResponse> pagarAnticipo({
    required String? numeroCuenta,
    required int? numeroCredito,
    required int? codigoTipoAnticipo,
    required int? codigoTipoPago,
    required int? codigoTipoSolicitante,
    required String? codigoMonedaOperacion,
    required String montoAdelantar,
    required String? identificadorDispositivo,
  }) async {
    try {
      String url = '/api/ventanillas/pagocreditos/anticipo/confirmacion';

      Map<String, dynamic> form = {
        "NumeroCuenta": numeroCuenta,
        "NumeroCredito": numeroCredito,
        "CodigoTipoAnticipo": codigoTipoAnticipo,
        "CodigoTipoPago": codigoTipoPago,
        "CodigoTipoSolicitante": codigoTipoSolicitante,
        "CodigoMonedaOperacion": codigoMonedaOperacion,
        "MontoAdelantar": montoAdelantar,
        "IdentificadorDispositivo": identificadorDispositivo
      };

      final response = await api.post(url, form);

      return PagarAppAnticipoResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al iniciar el pago.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ConfirmarAppResponse> confirmarAnticipo({
  required String? numeroCuenta,
    required int? numeroCredito,
    required int? codigoTipoAnticipo,
    required int? codigoTipoPago,
    required int? codigoTipoSolicitante,
    required String? codigoMonedaOperacion,
    required String montoAdelantar,
    required String? identificadorDispositivo,
    required String? codigoAutorizacion,
  }) async {
    try {
       String url = '/api/ventanillas/pagocreditos/anticipo';

      Map<String, dynamic> form = {
        "NumeroCuenta": numeroCuenta,
        "NumeroCredito": numeroCredito,
        "CodigoTipoAnticipo": codigoTipoAnticipo,
        "CodigoTipoPago": codigoTipoPago,
        "CodigoTipoSolicitante": codigoTipoSolicitante,
        "CodigoMonedaOperacion": codigoMonedaOperacion,
        "MontoAdelantar": montoAdelantar,
        "IdentificadorDispositivo": identificadorDispositivo,
        "CodigoAutorizacion": codigoAutorizacion,
      };

      final response = await api.post(url, form);

      return ConfirmarAppResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al iniciar el pago.', e);
      throw ServiceException(errorMessage);
    }
  }

}
