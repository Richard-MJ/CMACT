import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/models/cancelar_saldo_cero.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/models/cancelar_trans_interbancaria_response.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/models/cancelar_trans_interna_response.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/models/confirmar_saldo_cero.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/models/confirmar_trans_interbancaria.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/models/confirmar_trans_interna_response.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/models/cuenta_tercero.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/diferidas/models/datos_iniciales_response.dart';

final api = Api();

class CancelacionCuentasService {
  //para cancelaciones con trans propia o terceros
  static Future<CancelarTransInternaResponse> cancelarTransInterna({
    required String? numeroCuentaAhorro,
    required String? numeroCuentaDestino,
    required String tipoCancelacion,
    required String? codigoTipo,
    required String? identificadorDispositivo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "NumeroCuentaAhorros": numeroCuentaAhorro,
        "NumeroCuentaCredito": numeroCuentaDestino,
        "TipoCancelacion": tipoCancelacion,
        "CodigoTipo": codigoTipo,
        "IdentificadorDispositivo": identificadorDispositivo
      };

      final response =
          await api.post('/api/ventanillas/cancelaciones/confirmacion', form);
      return CancelarTransInternaResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al cancelar la cuenta.', e);
      throw ServiceException(errorMessage);
    }
  }

  //para cancelaciones con trans propia o terceros
  static Future<ConfirmarTransInternaResponse> confirmarTransInterna(
      {required String tokenDigital,
      required String? numeroCuentaAhorro,
      required String? numeroCuentaDestino,
      required double? montoCancelar,
      required String? codigoTipo,
      required bool? cancelacionAnticipada,
      required double? interesCancelacion,
      required String? codigoAgencia}) async {
    try {
      Map<String, dynamic> form = {
        "CodigoAutorizacion": tokenDigital,
        "NumeroCuentaDebito": numeroCuentaAhorro,
        "NumeroCuentaCredito": numeroCuentaDestino,
        "MontoCancelar": montoCancelar,
        "CodigoTipo": codigoTipo,
        "CancelacionAnticipada": cancelacionAnticipada,
        "InteresCancelacion": interesCancelacion,
        "CodigoAgencia": codigoAgencia
      };

      final response =
          await api.post('/api/ventanillas/cancelaciones/cancelacion', form);
      return ConfirmarTransInternaResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar la cancelación.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<CuentaTercero> obtenerCuentaTercero({
    required String numCuenta,
  }) async {
    try {
      final response = await api
          .get('/api/ventanillas/cancelaciones/cuentatercero/$numCuenta');

      return CuentaTercero.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al buscar la cuenta.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<CancelarSaldoCero> cancelarSaldoCero({
    required String? numeroCuentaAhorro,
    required String? identificadorDispositivo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "NumeroCuentaAhorros": numeroCuentaAhorro,
        "IdentificadorDispositivo": identificadorDispositivo
      };

      final response = await api.post(
          '/api/ventanillas/cancelaciones/saldo-cero/confirmacion', form);
      return CancelarSaldoCero.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al cancelar la cuenta.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ConfirmarSaldoCero> confirmarSaldoCero({
    required String? numeroCuentaAhorro,
    required String tokenDigital,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoAutorizacion": tokenDigital,
        "NumeroCuentaDebito": numeroCuentaAhorro,
        "NumeroCuentaCredito": "",
        "MontoCancelar": 0
      };

      final response = await api.post(
          '/api/ventanillas/cancelaciones/saldo-cero/cancelacion', form);
      return ConfirmarSaldoCero.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar la cancelación.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<DatosInicialesResponse> obtenerDatosInicialesCce() async {
    try {
      final response = await api.get(
        '/api/ventanillas/transferencias/cce/datosiniciales',
      );

      return DatosInicialesResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los datos.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<CancelarTransInterbancariaResponse> cancelarTransInterbancaria({
    required String? numeroCuentaOrigen,
    required String numeroCuentaDestino,
    required String? codigoMoneda,
    required String? numeroDocumento,
    required int? idTipoDocumento,
    required String nombreReceptor,
    required String? identificadorDispositivo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoMonedaOperacion": codigoMoneda,
        "EsOperacionPropia": false,
        "NombreDestino": nombreReceptor,
        "CuentaOrigen": numeroCuentaOrigen,
        "CuentaDestinoCci": numeroCuentaDestino,
        "IdTipoDocumento": idTipoDocumento,
        "NumeroDocumento": numeroDocumento,
        "IdentificadorDispositivo": identificadorDispositivo,
      };

      final response = await api.post(
        '/api/ventanillas/cancelaciones/cce/confirmacion',
        form,
      );

      return CancelarTransInterbancariaResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al cancelar la cuenta.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ConfirmarTransInterbancariaResponse>
      confirmarTransInterbancaria({
    required String? numeroCuentaOrigen,
    required String? numeroCuentaDestino,
    required double? montoCancelar,
    required String? codigoMoneda,
    required String numeroDocumento,
    required int? idTipoDocumento,
    required bool? esPersonaNatural,
    required String nombreReceptor,
    required String tokenDigital,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoAutorizacion": tokenDigital,
        "EsOperacionPropia": false,
        "NombreDestino": nombreReceptor,
        "EsPersonaNatural": esPersonaNatural,
        "CuentaOrigen": numeroCuentaOrigen,
        "CuentaDestinoCci": numeroCuentaDestino,
        "MontoCancelar": montoCancelar,
        "IdTipoDocumento": idTipoDocumento,
        "NumeroDocumento": numeroDocumento,
        "CodigoMonedaOperacion": codigoMoneda
      };

      final response = await api.post(
        '/api/ventanillas/cancelaciones/cce/cancelacion',
        form,
      );

      return ConfirmarTransInterbancariaResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar la cancelación.', e);
      throw ServiceException(errorMessage);
    }
  }
}
