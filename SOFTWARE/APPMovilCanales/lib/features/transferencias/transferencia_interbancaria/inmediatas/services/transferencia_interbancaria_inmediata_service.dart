import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/inmediatas/models/datos_operacion_exitosa_response.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/inmediatas/models/detalle_transferencia_response.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/inmediatas/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/inmediatas/models/montos_totales_response.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/inmediatas/models/token_response.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/api/api.dart';
import 'dart:convert';

final api = Api();

class TransferenciaInterbancariaInmediataService {
  static Future<DatosInicialesResponse> obtenerDatosIniciales() async {
    try {
      final response =
          await api.get('/api/transferencia-inmediata/obtener-datos-iniciales');

      return DatosInicialesResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'En este momento las transferencias inmediatas no se encuentran disponibles.',
          e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<DetalleTransferenciaResponse> obtenerDatosCuentaReceptor(
      {required String? codigoTipoTransferencia,
      required String? numeroCuentaOrigen,
      required String codigoCuentaInterbancariaReceptor,
      required String codigoCanalCCE}) async {
    try {
      Map<String, dynamic> form = {
        "CodigoTipoTransferencia": codigoTipoTransferencia,
        "NumeroCuentaOriginante": numeroCuentaOrigen,
        "NumeroCuentaReceptor": codigoCuentaInterbancariaReceptor,
        "CodigoCanalCCE": codigoCanalCCE
      };

      final response = await api.post(
          '/api/transferencia-inmediata/consulta-cuenta-receptor', form);

      return DetalleTransferenciaResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un al realizar la consulta de cuenta.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<MontosTotales> calcularMontosTotales(
      {required numeroCuenta,
      required mismoTitular,
      required saldoActual,
      required montoOperacion,
      required montoMinimoCuenta,
      required esExoneradaItf,
      required esCuentaSueldo,
      required comision,
      required esExoneradoComision}) async {
    try {
      Map<String, dynamic> form = {
        "NumeroCuenta": numeroCuenta,
        "MismoTitular": mismoTitular,
        "SaldoActual": saldoActual,
        "MontoOperacion": montoOperacion,
        "MontoMinimoCuenta": montoMinimoCuenta,
        "EsExoneradaITF": esExoneradaItf,
        "EsCuentaSueldo": esCuentaSueldo,
        "Comision": comision,
        "EsExoneradoComision": esExoneradoComision
      };

      final response = await api.post(
          '/api/transferencia-inmediata/calcular-montos-totales', form);

      return MontosTotales.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los datos del cliente originante.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<TokenResponse> obtenerTokenDigital(
      {required codigoMonedaCuenta, required identificadorDispositivo}) async {
    try {
      Map<String, dynamic> form = {
        "CodigoMonedaCuenta": codigoMonedaCuenta,
        "IdentificadorDispositivo": identificadorDispositivo,
      };

      final response = await api.post('/api/interoperabilidad/confirmar', form);

      return TokenResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener el token.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<void> validarToken({required numeroVerificacion}) async {
    try {
      Map<String, dynamic> form = {
        "CodigoAutorizacion": numeroVerificacion,
      };

      await api.post('/api/interoperabilidad/validar-token', form);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al validar el Token.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<DatosOperacionExitosaResponse> confirmarTransferencia(
      {required numeroCuenta,
      required controlMonto,
      required detalleTransferencia,
      required motivo,
      required nombreTerminos,
      required documentoTerminos}) async {
    try {
      Map<String, dynamic> form = {
        "NumeroCuenta": numeroCuenta,
        "ControlMonto": controlMonto,
        "ResultadoConsultaCuenta": detalleTransferencia,
        "Motivo": motivo,
        "NombreDocumentoTerminos": nombreTerminos,
        "DocumentoTerminos": base64Encode(documentoTerminos)
      };

      final response = await api.post(
          '/api/transferencia-inmediata/realizar-orden-transferencia', form);

      return DatosOperacionExitosaResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar la operación.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<void> enviarCorreoElectronicoTransferencia(
      {required numeroMovimiento,
      required correoElectronicoDestinatario,
      required nombreTerminos,
      required documentoTerminos}) async {
    try {
      Map<String, dynamic> form = {
        "NumeroMovimiento": numeroMovimiento,
        "CorreoDestinatario": correoElectronicoDestinatario,
        "NombreDocumentoTerminos": nombreTerminos,
        "DocumentoTerminos": base64Encode(documentoTerminos)
      };

      await api.post(
          '/api/transferencia-inmediata/enviar-correo-electronico', form);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al validar el Token.', e);
      throw ServiceException(errorMessage);
    }
  }
}
