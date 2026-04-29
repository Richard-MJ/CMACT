import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/cuenta_ahorro/models/cuenta_ahorro.dart';
import 'package:caja_tacna_app/features/cuenta_ahorro/models/datos_estado_cuenta_correo.dart';
import 'package:caja_tacna_app/features/cuenta_ahorro/models/movimiento_cuenta_ahorro.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class CuentaAhorroService {
  static Future<CuentaAhorro> obtenerDetalleCuenta(
    String codigoAgencia,
    String identificador,
  ) async {
    try {
      final response = await api
          .get('/api/clientes/productospasivos/$codigoAgencia/$identificador');
      return CuentaAhorro.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al cargar la cuenta.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<List<MovimientoCuentaAhorro>> obtenerMovimientos(
    String codigoAgencia,
    String identificador,
  ) async {
    try {
      final response = await api.get(
          '/api/clientes/productospasivos/$codigoAgencia/$identificador/movimientos');
      return List<MovimientoCuentaAhorro>.from(
          response.data.map((x) => MovimientoCuentaAhorro.fromJson(x)));
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al cargar los movimientos.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<List<MovimientoCuentaAhorro>> obtenerMovimientosCongelados(
    String numeroProducto,
  ) async {
    try {
      final response = await api.get(
          '/api/clientes/productosactivos/$numeroProducto/movimientos-congelados');
      return List<MovimientoCuentaAhorro>.from(
          response.data.map((x) => MovimientoCuentaAhorro.fromJson(x)));
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al cargar los movimientos.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<DatosEstadoCuentaCorreo> enviarEstadoCuentaAhorro({
    required int? anioInicio,
    required String mesInicio,
    required int? anioFin,
    required String mesFin,
    required String? numeroCuenta,
    required String? codigoSistema,
  }) async {
    Map<String, dynamic> form = {
      "AnioInicio": anioInicio,
      "MesInicio": mesInicio,
      "AnioFin": anioFin,
      "MesFin": mesFin,
      "NumeroCuenta": numeroCuenta,
      "CodigoSistema": codigoSistema,
    };

    try {
      final response = await api.post(
        '/api/cartera-productos/enviar-estado-cuenta-correo',
        form,
      );
      return DatosEstadoCuentaCorreo.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al enviar el correo.', e);
      throw ServiceException(errorMessage);
    }
  }
}
