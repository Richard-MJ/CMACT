import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/models/ingresar_solicitud_response.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/models/obtener_datos_iniciales_solicitud_crediticia_response.dart';

final api = Api();

class SolicitudCrediticiaService {
  // Obtener datos iniciales
  static Future<ObtenerDatosInicialesSolicitudCrediticiaResponse>
      obtenerDatosIniciales() async {
    try {
      final response =
          await api.get('/api/solicitud-crediticia/datos-iniciales');

      return ObtenerDatosInicialesSolicitudCrediticiaResponse.fromJson(
          response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los datos iniciales.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<IngresarSolicitudResponse> ingresarSolicitudCrediticia(
      {required String? codigoTipoIngreso,
      required String? ingresoMensual,
      required String? codigoTipoMoneda,
      required String? montoDeseado,
      required String? cuotas,
      required String? destinoCredito,
      required String? codigoAgenciaAtencion,
      required bool? indicadorPublicidad}) async {
    try {
      Map<String, dynamic> form = {
        "CodigoTipoIngreso": codigoTipoIngreso,
        "IngresoMensual": ingresoMensual,
        "CodigoTipoMoneda": codigoTipoMoneda,
        "MontoDeseado": montoDeseado,
        "Cuotas": cuotas,
        "DestinoCredito": destinoCredito,
        "CodigoAgenciaAtencion": codigoAgenciaAtencion,
        "IndicadorPublicidad": indicadorPublicidad,
      };

      final response = await api.post('/api/solicitud-crediticia', form);
      return IngresarSolicitudResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar el pago', e);
      throw ServiceException(errorMessage);
    }
  }
}
