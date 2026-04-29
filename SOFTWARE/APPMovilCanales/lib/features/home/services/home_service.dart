import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/credito/models/credito.dart';
import 'package:caja_tacna_app/features/cuenta_ahorro/models/cuenta_ahorro.dart';
import 'package:caja_tacna_app/features/home/models/configuracion.dart';
import 'package:caja_tacna_app/features/home/models/datos_cliente.dart';
import 'package:caja_tacna_app/features/home/models/tipo_cambio.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class HomeService {
  static Future<List<CuentaAhorro>> getCuentas() async {
    try {
      final response = await api.get('/api/clientes/productospasivos');

      return List<CuentaAhorro>.from(
          response.data.map((x) => CuentaAhorro.fromJson(x)));
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al cargar las cuentas de ahorro.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<List<Credito>> getCreditos() async {
    try {
      final response = await api.get('/api/clientes/productosactivos');

      return List<Credito>.from(response.data.map((x) => Credito.fromJson(x)));
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al cargar los créditos.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<DatosCliente> getDatosCliente() async {
    try {
      final response = await api.get('/api/clientes/datos');

      return DatosCliente.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al cargar los datos del cliente', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<TipoCambio> getTipoCambio() async {
    try {
      final response = await api.get('/api/ventanillas/tasacambio');

      return TipoCambio.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al cargar el tipo de cambio.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<Configuracion> getConfiguracion() async {
    try {
      final response = await api.get('/api/configuracion');

      return Configuracion.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener la configuración.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<TipoCambio> getTasaCambio() async {
    try {
      final response = await api.get('/api/ventanillas/tasacambio');
      return TipoCambio.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener la tasa de cambio.', e);
      throw ServiceException(errorMessage);
    }
  }
}
