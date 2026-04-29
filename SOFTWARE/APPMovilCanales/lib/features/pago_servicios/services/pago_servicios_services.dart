import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/pago_servicios/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/pago_servicios/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/pago_servicios/models/pagar_response.dart';
import 'package:caja_tacna_app/features/pago_servicios/models/obtener_cobro_servicio_response.dart';
import 'package:caja_tacna_app/features/pago_servicios/models/empresa.dart';
import 'package:caja_tacna_app/features/pago_servicios/models/pago_servicio.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class PagoServiciosService {
  //servicio implementado siguiendo la documentacion v.7
  static Future<DatosInicialesResponse> obtenerDatosIniciales() async {
    try {
      final response =
          await api.get('/api/pago-servicios-general/datos-iniciales');

      return DatosInicialesResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los datos.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.7
  static Future<List<Empresa>> obtenerEmpresasPorTexto({
    required String texto,
  }) async {
    try {
      final response =
          await api.get('/api/pago-servicios-general/empresas/$texto');
      return List<Empresa>.from(response.data.map((x) => Empresa.fromJson(x)));
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener las empresas.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.7
  static Future<List<Empresa>> obtenerEmpresasPorCategoria({
    required int idCategoria,
  }) async {
    try {
      final response = await api
          .get('/api/pago-servicios-general/categorias/$idCategoria/empresas');
      return List<Empresa>.from(response.data.map((x) => Empresa.fromJson(x)));
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener las empresas.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.7
  static Future<List<PagoServicio>> obtenerServicios({
    required int tipoPagoServicio,
    required String codigoEmpresa,
    required int codigoCategoria,
  }) async {
    try {
      final response = await api.get(
          '/api/pago-servicios-general/servicios/$tipoPagoServicio/$codigoEmpresa/$codigoCategoria');
      return List<PagoServicio>.from(
          response.data.map((x) => PagoServicio.fromJson(x)));
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los servicios.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.7
  static Future<ObtenerCobroServicioResponse> obtenerCobroServicio({
    required String suministro,
    required String? codigoEmpresa,
    required String? codigoServicio,
    required int? codigoGrupoEmpresa,
    required int? codigoCategoria,
    required String? nombreCategoria,
    required String? nombreEmpresa,
    required String? nombreServicio,
    required int? tipoPagoServicio,
  }) async {
    try {
      Map<String, dynamic> form = {
        "Suministro": suministro,
        "CodigoEmpresa": codigoEmpresa,
        "CodigoServicio": codigoServicio,
        "CodigoGrupoEmpresa": codigoGrupoEmpresa,
        "CodigoCategoria": codigoCategoria,
        "NombreCategoria": nombreCategoria,
        "NombreEmpresa": nombreEmpresa,
        "NombreServicio": nombreServicio,
        "TipoPagoServicio": tipoPagoServicio
      };

      final response =
          await api.post('/api/pago-servicios-general/deuda', form);
      return ObtenerCobroServicioResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener el cobro del servicio.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.7
  static Future<PagarResponse> pagarServicio({
    required String? numeroCuentaOrigen,
    required String? codigoMonedaDeuda,
    required double? montoDeuda,
    required String? identificadorDispositivo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "NumeroCuentaAhorroOrigen": numeroCuentaOrigen,
        "CodigoMonedaDeuda": codigoMonedaDeuda,
        "MontoOperacion": montoDeuda,
        "IdentificadorDispositivo": identificadorDispositivo,
      };

      final response =
          await api.post('/api/pago-servicios-general/confirmacion', form);
      return PagarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage =
          ErrorService.verificarErrorBase('Ocurrió un error al pagar.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.7
  static Future<ConfirmarResponse> confirmarServicio({
    required String? numeroCuentaOrigen,
    required String? codigoMonedaDeuda,
    required String? montoDeuda,
    required String tokenDigital,
    required double? moraDeuda,
    required double? comisionDeuda,
    required String? numeroRecibo,
    required String? codigoEmpresa,
    required String? codigoServicio,
    required int? tipoPagoServicio,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoAutorizacion": tokenDigital,
        "CodigoMonedaDeuda": codigoMonedaDeuda,
        "NumeroCuentaAhorros": numeroCuentaOrigen,
        "MontoDeuda": montoDeuda,
        "MoraDeuda": moraDeuda,
        "ComisionDeuda": comisionDeuda,
        "NumeroRecibo": numeroRecibo,
        "CodigoEmpresa": codigoEmpresa,
        "CodigoServicio": codigoServicio,
        "TipopagoServicio": tipoPagoServicio
      };

      final response =
          await api.post('/api/pago-servicios-general/pagar', form);
      return ConfirmarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar el pago', e);
      throw ServiceException(errorMessage);
    }
  }
}
