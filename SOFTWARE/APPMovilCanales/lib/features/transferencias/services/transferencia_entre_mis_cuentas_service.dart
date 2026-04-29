import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/transferencias/models/confirmar_entre_cuentas_response.dart';
import 'package:caja_tacna_app/features/transferencias/models/transferir_entre_cuentas_response.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_entre_mis_cuentas/models/datos_iniciales_response.dart';

final api = Api();

class TransferenciaEntreCuentasService {
  static Future<DatosInicialesResponse> obtenerDatosIniciales() async {
    try {
      final response = await api
          .get('/api/ventanillas/transferencias/datos-iniciales-propias');

      return DatosInicialesResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = 'Ocurrió un error al obtener los datos.';
      errorMessage = ErrorService.verificarErrorBase(errorMessage, e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<TransferirEntreCuentasResponse> transferir({
    required String? numeroCuentaOrigen,
    required String? numeroCuentaDestino,
    required String monto,
    required String? codigoSistema,
    required String? identificadorDispositivo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoSistema": codigoSistema,
        "NumeroCuentaDebito": numeroCuentaOrigen,
        "MontoOperacion": monto,
        "NumeroCuentaCredito": numeroCuentaDestino,
        "IdentificadorDispositivo": identificadorDispositivo,
      };

      final response = await api.post(
        '/api/ventanillas/transferencias/confirmacion',
        form,
      );

      return TransferirEntreCuentasResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = 'Ocurrió un error al iniciar la transferencia.';
      errorMessage = ErrorService.verificarErrorBase(errorMessage, e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ConfirmarResponseTransEntreCuentas> confirmar({
    required String? numeroCuentaOrigen,
    required String? numeroCuentaDestino,
    required String monto,
    required String? codigoMoneda,
    required String tokenDigital,
    String? codigoSistema = 'CC',
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoSistema": codigoSistema,
        "NumeroCuentaDebito": numeroCuentaOrigen,
        "MontoTransferir": monto,
        "NumeroCuentaCredito": numeroCuentaDestino,
        "CodigoMoneda": codigoMoneda,
        "CodigoAutorizacion": tokenDigital,
      };

      final response = await api.post(
        '/api/ventanillas/transferencias/transferencia',
        form,
      );

      return ConfirmarResponseTransEntreCuentas.fromJson(response.data);
    } catch (e) {
      String errorMessage = 'Ocurrió un error al confirmar la transferencia.';
      errorMessage = ErrorService.verificarErrorBase(errorMessage, e);
      throw ServiceException(errorMessage);
    }
  }
}
