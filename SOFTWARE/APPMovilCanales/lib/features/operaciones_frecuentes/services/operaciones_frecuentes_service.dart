import 'package:caja_tacna_app/features/operaciones_frecuentes/models/agregar_operacion_frecuente_response.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/models/listar_operaciones_response.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/models/modificar_operacion.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/api/api.dart';

final api = Api();

class OperacionesFrecuentesService {
  static Future<ListarOperacionesResponse> listarOperaciones() async {
    try {
      final response =
          await api.get('/api/operaciones-frecuentes-cliente/obtener');

      return ListarOperacionesResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener las operaciones frecuentes.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ModificarOperacion> editarAliasOperacionFrecuente({
    required int? numeroOperacionFrecuente,
    required String nombreOperacion,
  }) async {
    try {
      Map<String, dynamic> form = {
        "NumeroOperacionFrecuente": numeroOperacionFrecuente,
        "NombreOperacionFrecuente": nombreOperacion,
      };

      final response = await api.put(
        '/api/operaciones-frecuentes-cliente',
        form,
      );

      return ModificarOperacion.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al cambiar el alias.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ModificarOperacion> eliminarOperacionFrecuente({
    required int? numeroOperacionFrecuente,
  }) async {
    try {
      final response = await api.delete(
          '/api/operaciones-frecuentes-cliente/$numeroOperacionFrecuente');

      return ModificarOperacion.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al eliminar la operación frecuente.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<AgregarOperacionFrecuenteResponse> agregarTransPropia({
    required String? numeroCuentaOrigen,
    required String? numeroCuentaDestino,
    required String nombreOperacionFrecuente,
    required String? codigoSistema,
  }) async {
    try {
      Map<String, dynamic> form = {
        "NumeroCuenta": numeroCuentaOrigen,
        "NombreOperacionFrecuente": nombreOperacionFrecuente,
        "Detalle": {
          "NumeroCuentaCredito": numeroCuentaDestino,
          "CodigoSistema": codigoSistema
        },
      };

      final response = await api.post(
        '/api/operaciones-frecuentes-cliente/agregar/transferencia-propia',
        form,
      );

      return AgregarOperacionFrecuenteResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al agregar operación fecuente.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<AgregarOperacionFrecuenteResponse> agregarTransTerceros({
    required String? numeroCuentaOrigen,
    required String? numeroCuentaDestino,
    required String nombreOperacionFrecuente,
  }) async {
    try {
      Map<String, dynamic> form = {
        "NumeroCuenta": numeroCuentaOrigen,
        "NombreOperacionFrecuente": nombreOperacionFrecuente,
        "Detalle": {
          "NumeroCuentaCredito": numeroCuentaDestino,
          "CodigoSistema": "CC"
        },
      };

      final response = await api.post(
        '/api/operaciones-frecuentes-cliente/agregar/transferencia-terceros',
        form,
      );

      return AgregarOperacionFrecuenteResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al agregar operación fecuente.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<AgregarOperacionFrecuenteResponse> agregarTransInterbancaria({
    required String? numeroCuentaOrigen,
    required String numeroCuentaCci,
    required String nombreOperacionFrecuente,
    required String nombreReceptor,
    required bool esTitular,
    required int? idTipoDocumentoCompensacion,
    required String numeroDocumento,
  }) async {
    try {
      Map<String, dynamic> form = {
        "NumeroCuenta": numeroCuentaOrigen,
        "NombreOperacionFrecuente": nombreOperacionFrecuente,
        "Detalle": {
          "CuentaDestinoCci": numeroCuentaCci,
          "NombreDestino": nombreReceptor,
          "MismoTitularEnDestino": esTitular,
          "TipoDocumento": idTipoDocumentoCompensacion,
          "NumeroDocumento": numeroDocumento
        },
      };

      final response = await api.post(
        '/api/operaciones-frecuentes-cliente/agregar/transferencia-interbancaria',
        form,
      );

      return AgregarOperacionFrecuenteResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al agregar operación fecuente.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<AgregarOperacionFrecuenteResponse> agregarPagoCredito({
    required String? numeroCuentaOrigen,
    required int? numeroCredito,
    required String nombreOperacionFrecuente,
  }) async {
    try {
      Map<String, dynamic> form = {
        "NumeroCuenta": numeroCuentaOrigen,
        "NombreOperacionFrecuente": nombreOperacionFrecuente,
        "NumeroCredito": numeroCredito
      };

      final response = await api.post(
        '/api/operaciones-frecuentes-cliente/agregar/pago-credito',
        form,
      );

      return AgregarOperacionFrecuenteResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al agregar operación fecuente.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<AgregarOperacionFrecuenteResponse> agregarPagoTarjetaCredito({
    required String? numeroCuentaOrigen,
    required String numeroCuentaCci,
    required String nombreOperacionFrecuente,
    required String nombreReceptor,
    required bool esTitular,
    required int? idTipoDocumentoCompensacion,
    required String numeroDocumento,
  }) async {
    try {
      Map<String, dynamic> form = {
        "NumeroCuenta": numeroCuentaOrigen,
        "NombreOperacionFrecuente": nombreOperacionFrecuente,
        "Detalle": {
          "CuentaDestinoCci": numeroCuentaCci,
          "NombreDestino": nombreReceptor,
          "MismoTitularEnDestino": esTitular,
          "TipoDocumento": idTipoDocumentoCompensacion,
          "NumeroDocumento": numeroDocumento
        },
      };

      final response = await api.post(
        '/api/operaciones-frecuentes-cliente/agregar/pago-tarjeta-credito',
        form,
      );

      return AgregarOperacionFrecuenteResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al agregar operación fecuente.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<AgregarOperacionFrecuenteResponse> agregarGiro({
    required String? numeroCuentaOrigen,
    required String nombreOperacionFrecuente,
    required int? tipoDocumento,
    required String numeroDocumento,
    required String nombreReceptor,
    required String direccion,
    required String? codigoPais,
    required int? idVinculo,
    required int? idMotivo,
    required String? codigoDepartamento,
    required String? codigoAgencia,
  }) async {
    try {
      Map<String, dynamic> form = {
        "NumeroCuenta": numeroCuentaOrigen,
        "NombreOperacionFrecuente": nombreOperacionFrecuente,
        "Detalle": {
          "TipoDocumento": tipoDocumento,
          "NumeroDocumento": numeroDocumento,
          "NombreApellido": nombreReceptor,
          "Direccion": direccion,
          "CodigoPais": codigoPais,
          "IdVinculo": idVinculo,
          "OtroVinculo": "",
          "IdMotivo": idMotivo,
          "OtroMotivo": "",
          "CodigoDepartamento": codigoDepartamento,
          "CodigoAgencia": codigoAgencia,
        }
      };

      final response = await api.post(
        '/api/operaciones-frecuentes-cliente/agregar/emision-giro',
        form,
      );

      return AgregarOperacionFrecuenteResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al agregar operación fecuente.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<AgregarOperacionFrecuenteResponse> agregarPagoServicios({
    required String? numeroCuentaOrigen,
    required String nombreOperacionFrecuente,
    required String? suministro,
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
        "NumeroCuenta": numeroCuentaOrigen,
        "NombreOperacionFrecuente": nombreOperacionFrecuente,
        "Detalle": {
          "Suministro": suministro,
          "CodigoEmpresa": codigoEmpresa,
          "CodigoServicio": codigoServicio,
          "CodigoGrupoEmpresa": codigoGrupoEmpresa,
          "CodigoCategoria": codigoCategoria,
          "NombreCategoria": nombreCategoria,
          "NombreEmpresa": nombreEmpresa,
          "NombreServicio": nombreServicio,
          "TipoPagoServicio": tipoPagoServicio
        },
      };

      final response = await api.post(
        '/api/operaciones-frecuentes-cliente/agregar/pago-servicio',
        form,
      );

      return AgregarOperacionFrecuenteResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al agregar operación fecuente.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<AgregarOperacionFrecuenteResponse> agregarRecargaVirtual({
    required String? numeroCuentaOrigen,
    required String? codigoOperador,
    required String nombreOperacionFrecuente,
    required String? numeroCelular,
  }) async {
    try {
      Map<String, dynamic> form = {
        "NumeroCuenta": numeroCuentaOrigen,
        "NombreOperacionFrecuente": nombreOperacionFrecuente,
        "Detalle": {
          "Operador": codigoOperador,
          "Telefono": numeroCelular,
        },
      };

      final response = await api.post(
        '/api/operaciones-frecuentes-cliente/agregar/recarga-virtual',
        form,
      );

      return AgregarOperacionFrecuenteResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al agregar operación fecuente.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<bool> agregarTransInterbancariaInmediata({
    required String? numeroCuentaOrigen,
    required String numeroCuentaReceptor,
    required String nombreOperacionFrecuente,
    required String nombreReceptor,
    required bool esTitular,
    required int? idTipoDocumentoCompensacion,
    required String numeroDocumento,
    required int tipoOperacionFrecuente
  }) async {
    try {
      Map<String, dynamic> form = {
        "NumeroCuenta": numeroCuentaOrigen,
        "NombreOperacionFrecuente": nombreOperacionFrecuente,
        "CodigoCuentaInterbancariaReceptor": numeroCuentaReceptor,
        "NombreDestino": nombreReceptor,
        "MismoTitularEnDestino": esTitular,
        "TipoDocumento": idTipoDocumentoCompensacion,
        "NumeroDocumento": numeroDocumento,
        "TipoOperacionFrecuente": tipoOperacionFrecuente
      };

      var response = await api.post('/api/transferencia-inmediata/agregar-operaciones-frecuentes',
        form,
      );

      return response.data;
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al agregar operación fecuente.', e);
      throw ServiceException(errorMessage);
    }
  }
}
