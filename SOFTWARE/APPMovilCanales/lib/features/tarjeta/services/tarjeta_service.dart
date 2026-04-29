import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/compras_internet/models/afiliacion.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/tarjeta/models/confirmar_cvv_dinamico_response.dart';
import 'package:caja_tacna_app/features/tarjeta/models/obtener_cvv_dinamico_response.dart';
import 'package:caja_tacna_app/features/tarjeta/models/obtener_estado_servicio_cvv_dinamico_response.dart';

final api = Api();

class TarjetaService {
  static Future<ObtenerEstadoServicioCvvDinamicoResponse> obtenerEstadoServicio(
      {required String? numeroTarjeta}) async {
    try {
      Map<String, dynamic> form = {"numeroTarjeta": numeroTarjeta};

      final response =
          await api.post('/api/cvv-dinamico/estado-servicio', form);

      return ObtenerEstadoServicioCvvDinamicoResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió al obtener el estado del servicio cvv dinámico.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<Afiliacion> obtenerAfiliacion() async {
    try {
      final response = await api.get('/api/compras-por-internet');

      return Afiliacion.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener la afiliacion.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ConfirmarCvvDinamicoResponse> confirmarCvvDinamico(
      {required String? identificadorDispositivo}) async {
    try {
      Map<String, dynamic> form = {
        "identificadorDispositivo": identificadorDispositivo
      };

      final response = await api.post('/api/cvv-dinamico/confirmacion', form);
      return ConfirmarCvvDinamicoResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar cvv dinámico.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<ObtenerCvvDinamicoResponse> obtenerCvvDinamico(
      {required String? codigoAutorizacion,
      required String? numeroTarjeta}) async {
    try {
      Map<String, dynamic> form = {
        "codigoAutorizacion": codigoAutorizacion,
        "numeroTarjeta": numeroTarjeta
      };

      final response = await api.post('/api/cvv-dinamico', form);
      return ObtenerCvvDinamicoResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener cvv dinámico.', e);
      throw ServiceException(errorMessage);
    }
  }
}
