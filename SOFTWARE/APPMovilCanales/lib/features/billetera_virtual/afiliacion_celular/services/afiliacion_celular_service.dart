import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/models/configuracion_notificacion_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/models/datos_afiliacion_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/models/token_response.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class AfiliacionCelularService {
  static Future<bool> obtenerAfiliacionBilleteraVirtual() async {
    try {
      final response =
          await api.get('/api/interoperabilidad/validar-afiliacion-simple');

      return response.data;
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los datos.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<DatosValidacionResponse> obtenerDatosAfiliacion() async {
    try {
      final response =
          await api.get('/api/interoperabilidad/validar-afiliacion');

      return DatosValidacionResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los datos.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<TokenResponse> enviarAfiliacion(
      {required codigoMonedaCuenta, required identificadorDispositivo}) async {
    try {
      Map<String, dynamic> form = {
        "CodigoMonedaCuenta": codigoMonedaCuenta,
        "IdentificadorDispositivo": identificadorDispositivo,
      };

      final responseToken =
          await api.post('/api/interoperabilidad/confirmar', form);

      return TokenResponse.fromJson(responseToken.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al enviar la afiliación.', e);
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

  static Future<ConfirmarResponse> enviarAfiliacionCCE(
      {required DatosAfiliacionResponse? datos,
      required bool notificarOperacionesEnviadas,
      required bool notificarOperacionesRecibidas}) async {
    try {
      Map<String, dynamic> form = {
        "CodigoEntidadOriginante": datos?.codigoEntidadOriginante,
        "TipoInstruccion": "NEWR",
        "NumeroCelular": datos?.numeroCelular,
        "TipoOperacion": datos?.tipoOperacion,
        "CodigoCliente": datos?.codigoCliente,
        "NumeroCuentaAfiliada": datos?.numeroCuentaAfiliada,
        "CodigoCuentaInterbancario": datos?.codigoCuentaInterbancario,
        "IndicadorModificarNumero": "N",
        "NumeroAntiguo": "",
        "Canal": datos!.canal,
        "NumeroTarjeta": datos.numeroTarjeta,
        "NotificarOperacionesEnviadas": notificarOperacionesEnviadas,
        "NotificarOperacionesRecibidas": notificarOperacionesRecibidas
      };

      final response = await api.post('/api/interoperabilidad/afiliar', form);

      return ConfirmarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Error en el proceso  de afiliar a la billetera virtual.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ConfirmarResponse> enviarDesfiliacionCCE(
      DatosAfiliacionResponse? datos) async {
    try {
      Map<String, dynamic> form = {
        "CodigoEntidadOriginante": datos?.codigoEntidadOriginante,
        "TipoInstruccion": "DEAC",
        "NumeroCelular": datos?.numeroCelular,
        "TipoOperacion": datos?.tipoOperacion,
        "CodigoCliente": datos?.codigoCliente,
        "NumeroCuentaAfiliada": datos?.numeroCuentaAfiliada,
        "CodigoCuentaInterbancario": datos?.codigoCuentaInterbancario,
        "IndicadorModificarNumero": "N",
        "NumeroAntiguo": "",
        "Canal": datos!.canal,
        "NumeroTarjeta": datos.numeroTarjeta
      };

      final response =
          await api.post('/api/interoperabilidad/desafiliar', form);

      return ConfirmarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Error en el proceso de desafiliar a la billetera virtual.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ConfiguracionNotificacionResponse> configurarNotificacionesBilletera({
    required String codigoAutorizacion,
    required String numeroCuenta,
    required bool notificarOperacionesEnviadas,
    required bool notificarOperacionesRecibidas}) async {
    try {
      Map<String, dynamic> form = {
        "CodigoAutorizacion": codigoAutorizacion,
        "NumeroCuenta": numeroCuenta,
        "NotificarOperacionesEnviadas": notificarOperacionesEnviadas,
        "NotificarOperacionesRecibidas": notificarOperacionesRecibidas,
      };

      final response = await api.put('/api/interoperabilidad/configuracion-notificaciones-billetera', form);

      return ConfiguracionNotificacionResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al modificar las notificaciones.', e);
      throw ServiceException(errorMessage);
    }
  }
}
