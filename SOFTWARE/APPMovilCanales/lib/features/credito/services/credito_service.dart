import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/credito/models/movimiento_credito.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class CreditoService {
  static getMovimientos({
    required String numeroCredito,
  }) async {
    try {
      final response = await api
          .get('/api/clientes/productosactivos/$numeroCredito/movimientos');
      return List<MovimientoCredito>.from(
          response.data.map((x) => MovimientoCredito.fromJson(x)));
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al cargar los movimientos.', e);
      throw ServiceException(errorMessage);
    }
  }

  static enviarPlanPagosCredito({
    required int? numeroCredito,
  }) async {
    try {
      Map<String, dynamic> form = {
        "NumeroCredito": numeroCredito,
      };

      await api.post(
        '/api/cartera-productos/enviar-plan-pago-credito-correo',
        form,
      );
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al enviar el correo.', e);
      throw ServiceException(errorMessage);
    }
  }
}
