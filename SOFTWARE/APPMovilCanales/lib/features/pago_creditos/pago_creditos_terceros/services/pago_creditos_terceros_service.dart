import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/pago_creditos/models/credito_abonar.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class PagoCreditosTercerosService {
  static Future<CreditoAbonar> obtenerDatosCreditoTercero({
    required String numeroCredito,
  }) async {
    try {
      final response = await api
          .get('/api/ventanillas/pagocreditos/creditotercero/$numeroCredito');

      return CreditoAbonar.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los datos.', e);
      throw ServiceException(errorMessage);
    }
  }
}
