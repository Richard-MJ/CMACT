import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/constants/mensaje_error.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/diferidas/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/diferidas/models/transferir_response.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/diferidas/models/datos_iniciales_response.dart';

final api = Api();

class TransferenciaInterbancariaDiferidaService {
  static Future<DatosInicialesResponse> obtenerDatosIniciales() async {
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

  static Future<TransferirResponse> transferir({
    required String? numeroCuentaOrigen,
    required String numeroCuentaDestino,
    required String monto,
    required String? codigoMoneda,
    required bool esTitular,
    required String? numeroDocumento,
    required int? idTipoDocumentoCompensacion,
    required String nombreReceptor,
    required String? identificadorDispositivo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoMonedaOperacion": codigoMoneda,
        "MismoTitularEnDestino": esTitular,
        "NombreDestino": nombreReceptor,
        "CuentaOrigen": numeroCuentaOrigen,
        "CuentaDestinoCci": numeroCuentaDestino,
        "MontoTransferir": monto,
        "IdTipoDocumento": idTipoDocumentoCompensacion,
        "NumeroDocumento": numeroDocumento,
        "IdentificadorDispositivo": identificadorDispositivo,
      };

      final response = await api.post(
        '/api/ventanillas/transferencias/cce/confirmacion',
        form,
      );

      return TransferirResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al iniciar la transferencia.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ConfirmarResponse> confirmar({
    required String? numeroCuentaOrigen,
    required String? numeroCuentaDestino,
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
        "CuentaDestinoCci": numeroCuentaDestino,
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
        '/api/ventanillas/transferencias/cce/transferencia',
        form,
      );

      return ConfirmarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage =
          ErrorService.verificarErrorBase(MensajeError.errorGenerico, e);
      throw ServiceException(errorMessage);
    }
  }
}
