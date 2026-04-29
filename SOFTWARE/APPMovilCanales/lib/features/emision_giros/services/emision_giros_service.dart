import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/emision_giros/models/agencia.dart';
import 'package:caja_tacna_app/features/emision_giros/models/comision.dart';
import 'package:caja_tacna_app/features/emision_giros/models/confirmar_giro_response.dart';
import 'package:caja_tacna_app/features/emision_giros/models/departamento.dart';
import 'package:caja_tacna_app/features/emision_giros/models/nacionalidad.dart';
import 'package:caja_tacna_app/features/emision_giros/models/pagar_response.dart';
import 'package:caja_tacna_app/features/emision_giros/models/tipo_documento.dart';
import 'package:caja_tacna_app/features/emision_giros/models/vinculos_motivos_response.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class EmisionGirosService {
  static Future<List<TipoDocumentoGiro>> obtenerTiposDocumentos() async {
    try {
      final response = await api.get('/api/giros/ObtenerTiposDocumentosGiros');

      return List<TipoDocumentoGiro>.from(
          response.data.map((x) => TipoDocumentoGiro.fromJson(x)));
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los tipos de documentos.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<List<Nacionalidad>> obtenerNacionalidades() async {
    try {
      final response = await api.get('/api/giros/Nacionalidades');

      return List<Nacionalidad>.from(
          response.data.map((x) => Nacionalidad.fromJson(x)));
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener las nacionalidades.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<List<Departamento>> obtenerDepartamentos() async {
    try {
      final response = await api.get('/api/giros/Departamentos');

      return List<Departamento>.from(
          response.data.map((x) => Departamento.fromJson(x)));
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los departamentos.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<VinculosMotivosResponse> obtenerVinculosMotivos() async {
    try {
      final response =
          await api.get('/api/giros/VinculoMotivo/?codigoSistema=CJ');

      return VinculosMotivosResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los vinculos y motivos.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<List<Agencia>> obtenerAgenciasPorDepartamento({
    required String? codigoDepartamento,
  }) async {
    try {
      final response =
          await api.get('/api/ventanillas/ObtenerAgencias/$codigoDepartamento');

      return List<Agencia>.from(response.data.map((x) => Agencia.fromJson(x)));
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener las agencias.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<Comision> obtenerComision({
    required String? codigoAgencia,
    required String? codigoMoneda,
    required String montoOperacion,
    required String? numeroCuenta,
  }) async {
    try {
      final response = await api.get(
          '/api/giros/ObtenerComisionGiro/$codigoAgencia/$codigoMoneda/$montoOperacion/$numeroCuenta');

      return Comision.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener la comision.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.7.1
  static Future<PagarResponse> pagarGiro({
    required String? numeroCuentaOrigen,
    required String? codigoMoneda,
    required String montoGiro,
    required String? identificadorDispositivo,
    required String numeroDocumentoBeneficiario,
  }) async {
    try {
      Map<String, dynamic> form = {
        "NumeroCuentaOrigen": numeroCuentaOrigen,
        "CodigoMonedaDestinoGiro": codigoMoneda,
        "MontoDestinoGiro": montoGiro,
        "NumeroDocumentoBenef": numeroDocumentoBeneficiario,
        "IdentificadorDispositivo": identificadorDispositivo,
      };

      final response = await api.post('/api/giros/confirmacion', form);
      return PagarResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al realizar el giro.', e);
      throw ServiceException(errorMessage);
    }
  }

  //servicio implementado siguiendo la documentacion v.7.1
  static Future<ConfirmarGiroResponse> confirmarGiro({
    required int? idVerificacion,
    required String tokenDigital,
    required String? numeroCuentaOrigen,
    required String? codigoMoneda,
    required String montoGiro,
    required int? tipoDocumentoBenef,
    required String numeroDocumentoBeneficiario,
    required String nombreBeneficiario,
    required String direccionBeneficiario,
    required String? codigoAgencia,
    required int? idVinculo,
    required int? idMotivo,
    required String? idNacionalidad,
    required double? montoTotal,
  }) async {
    try {
      Map<String, dynamic> form = {
        "idVerificacion": idVerificacion,
        "codigoAutorizacion": tokenDigital,
        "numeroCuentaOrigen": numeroCuentaOrigen,
        "codigoMonedaDestinoGiro": codigoMoneda,
        "montoDestinoGiro": montoGiro,
        "tipoDocumentoBenef": tipoDocumentoBenef,
        "numeroDocumentoBenef": numeroDocumentoBeneficiario,
        "nombresClienteDestino": nombreBeneficiario,
        "apellidoPaternoClienteDestino": "",
        "apellidoMaternoClienteDestino": "",
        "codigoAgenciaDestino": codigoAgencia,
        "direccionBeneficiario": direccionBeneficiario,
        "idVinculo": idVinculo,
        "otroVinculo": "",
        "idMotivo": idMotivo,
        "otroMotivo": "",
        "idNacionalidad": idNacionalidad,
        "montoTotal": montoTotal,
      };

      final response = await api.post('/api/giros/EmitirGiro', form);
      return ConfirmarGiroResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar el giro.', e);
      throw ServiceException(errorMessage);
    }
  }
}
