import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/models/aperturar_response.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/models/calculo_dpf_response.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/models/obtener_agencias_response.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/models/validar_apertura_response.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class AperturaCuentasService {
  //servicio implementado siguiendo la documentacion v.8.0
  static Future<ValidarAperturaResponse> validarApertura() async {
    try {
      final response = await api.get('/api/aperturas/validacion-usuario');

      return ValidarAperturaResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los datos.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.8.0
  static Future<ObtenerAgenciasResponse> obtenerAgencias() async {
    try {
      final response = await api.get('/api/aperturas/agencias');

      return ObtenerAgenciasResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener las agencias.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.8.1
  static Future<DatosInicialesResponse> obtenerDatosIniciales() async {
    try {
      final response = await api.get('/api/aperturas/datos-iniciales');

      return DatosInicialesResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los datos.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.8.1
  static Future<CalculoDpfResponse> calculoDpf({
    required String? codigoProducto,
    required String? codigoMoneda,
    required String? codigoAgencia,
    required String? numeroCuentaOrigen,
    required String montoApertura,
    required String diasDpf,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoProducto": codigoProducto,
        "CodigoAgencia": codigoAgencia,
        "diaDpf": diasDpf,
        "MontoApertura": montoApertura,
        "CodigoMoneda": codigoMoneda,
        "NumeroCuentaOrigen": numeroCuentaOrigen,
      };

      final response =
          await api.post('/api/aperturas/dpf/datos-apertura', form);
      return CalculoDpfResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los datos.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.8.1
  static Future<bool> confirmacionInicial({
    required String? numeroCuentaOrigen,
    required String? codigoProducto,
    required String? codigoMoneda,
    required String? codigoAgencia,
    required String montoApertura,
    required String? codigoSistema,
    required String diasDpf,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoProducto": codigoProducto,
        "CodigoMoneda": codigoMoneda,
        "CodigoAgencia": codigoAgencia,
        "MontoApertura": montoApertura,
        "CodigoSistema": codigoSistema,
        "NumeroCuentaOrigen": numeroCuentaOrigen,
        "DiasDpfSeleccionado": diasDpf
      };

      final response =
          await api.post('/api/aperturas/confirmacion/datos-iniciales', form);
      return response.data;
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar la apertura.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.8.0
  static Future<AperturarResponse> aperturar({
    required String? codigoProducto,
    required String? codigoMoneda,
    required String? codigoAgencia,
    required String? numeroCuentaOrigen,
    required String? codigoSistema,
    required String montoApertura,
    required String? identificadorDispositivo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoProducto": codigoProducto,
        "CodigoMoneda": codigoMoneda,
        "CodigoAgencia": codigoAgencia,
        "MontoApertura": montoApertura,
        "CodigoSistema": codigoSistema,
        "NumeroCuentaOrigen": numeroCuentaOrigen,
        "IdentificadorDispositivo": identificadorDispositivo
      };

      final response = await api.post('/api/aperturas/confirmacion', form);
      return AperturarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage =
          ErrorService.verificarErrorBase('Ocurrió un error al aperturar.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.8.1
  static Future<ConfirmarResponse> confirmar({
    required String? numeroCuentaOrigen,
    required String tokenDigital,
    required String? codigoProducto,
    required String? codigoMoneda,
    required String? codigoAgencia,
    required String montoApertura,
    required String? codigoSistema,
    required String? email,
    required String diasDpf,
    required bool? conocimientoTdp,
    required bool? consentimientoTdp,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoVerificacion": tokenDigital,
        "CodigoProducto": codigoProducto,
        "CodigoMoneda": codigoMoneda,
        "CodigoAgencia": codigoAgencia,
        "MontoApertura": montoApertura,
        "CodigoSistema": codigoSistema,
        "NumeroCuentaOrigen": numeroCuentaOrigen,
        "DiasDpfSeleccionado": diasDpf,
        "CorreoElectronico": email,
        "ConocimientoTdp": conocimientoTdp,
        "ConsentimientoTdp": consentimientoTdp,
      };

      final response = await api.post('/api/aperturas/cuenta', form);
      return ConfirmarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar la apertura.', e);
      throw ServiceException(errorMessage);
    }
  }
}
