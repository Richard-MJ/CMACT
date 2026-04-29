import 'package:caja_tacna_app/features/pago_tarjetas_credito/diferidas/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/diferidas/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/diferidas/models/pagar_response.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/api/api.dart';

final api = Api();

class PagoTarjetasCreditoDiferidaService {
  static Future<DatosInicialesResponse> obtenerDatosIniciales() async {
    try {
      final response =
          await api.get('/api/ventanillas/pagotarjeta/datosiniciales');

      return DatosInicialesResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los datos.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<PagarResponse> pagar({
    required String? numeroCuentaOrigen,
    required String? numeroTarjetaCredito,
    required String monto,
    required String? identificadorDispositivo,
    required String? codigoMoneda,
    required bool esTitular,
    required String nombreReceptor,
    required int? idEntidad,
    required int? idTipoDocumentoCompensacion,
    required String numeroDocumento,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoMonedaOperacion": codigoMoneda,
        "MismoTitularEnDestino": esTitular,
        "NombreDestino": nombreReceptor,
        "CuentaOrigen": numeroCuentaOrigen,
        "NumeroTarjeta": numeroTarjetaCredito,
        "IdEntidadCce": idEntidad,
        "MontoTransferir": monto,
        "IdTipoDocumento": idTipoDocumentoCompensacion,
        "NumeroDocumento": numeroDocumento,
        "IdentificadorDispositivo": identificadorDispositivo,
      };

      final response = await api.post(
        '/api/ventanillas/pagotarjeta/confirmacion',
        form,
      );

      return PagarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al iniciar el pago.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ConfirmarResponse> confirmar({
    required String? numeroCuentaOrigen,
    required String? numeroTarjetaCredito,
    required String monto,
    required String? codigoMoneda,
    required bool esTitular,
    required String numeroDocumento,
    required int? idTipoDocumentoCompensacion,
    required double? montoComision,
    required bool? esPersonaNatural,
    required String nombreReceptor,
    required String? codigoComision,
    required String tokenDigital,
    required int? idEntidadFinancieraCce,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoMonedaOperacion": codigoMoneda,
        "CodigoTarifarioComision": codigoComision,
        "NumeroTarjeta": numeroTarjetaCredito,
        "CuentaOrigen": numeroCuentaOrigen,
        "EsPersonaNatural": esPersonaNatural,
        "IdEntidadFinancieraCce": idEntidadFinancieraCce,
        "IdTipoDocumento": idTipoDocumentoCompensacion,
        "CodigoAutorizacion": tokenDigital,
        "MismoTitularEnDestino": esTitular,
        "MontoComision": montoComision,
        "MontoTransferir": monto,
        "NombreDestino": nombreReceptor,
        "NumeroDocumento": numeroDocumento,
      };

      final response = await api.post(
        '/api/ventanillas/pagotarjeta/transferencia',
        form,
      );

      return ConfirmarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar el pago.', e);
      throw ServiceException(errorMessage);
    }
  }
}
