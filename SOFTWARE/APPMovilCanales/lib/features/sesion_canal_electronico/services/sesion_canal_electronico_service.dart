import 'package:caja_tacna_app/api/api.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/models/dispositivo_seguro.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/models/remover_dispositivo_seguro_confirmar_response.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/models/remover_dispositivo_seguro_response.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/models/sesion_canal_electronico.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';

final api = Api();

class SesionCanalElectronicoService {
  // Obtener dispositivos seguros
  static Future<List<DispositivoSeguro>> obtenerDispositivosSeguros() async {
    try {
      final response = await api
          .get('/api/sesiones-canales-electronicos/dispositivos-seguros');

      return List<DispositivoSeguro>.from(
          response.data.map((x) => DispositivoSeguro.fromJson(x)));
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los dispositivos seguros.', e);
      throw ServiceException(errorMessage);
    }
  }

  // Obtener sesiones de canales electrónicos
  static Future<List<SesionCanalElectronico>>
      obtenerSesionesCanalesElectronicos() async {
    try {
      final response = await api.get('/api/sesiones-canales-electronicos');

      return List<SesionCanalElectronico>.from(
          response.data.map((x) => SesionCanalElectronico.fromJson(x)));
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener las sesiones de canales electrónicos.',
          e);
      throw ServiceException(errorMessage);
    }
  }

  // Generar confirmación para remover dispositivos seguros
  static Future<RemoverDispositivoSeguroConfirmacionResponse>
      removerDispositivosSegurosConfirmacion({
    required String identificadorDispositivo,
  }) async {
    try {
      Map<String, dynamic> form = {
        "IdentificadorDispositivo": identificadorDispositivo,
      };

      final response = await api.post(
          '/api/sesiones-canales-electronicos/confirmacion', form);

      return RemoverDispositivoSeguroConfirmacionResponse.fromJson(
          response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al generar la confirmación para remover dispositivos seguros.',
          e);
      throw ServiceException(errorMessage);
    }
  }

  // Remover dispositivos seguros
  static Future<RemoverDispositivoSeguroResponse> removerDispositivosSeguros({
    required String codigoAutorizacion,
    required List<String> dispositivos,
  }) async {
    try {
      Map<String, dynamic> form = {
        "CodigoAutorizacion": codigoAutorizacion,
        "Dispositivos": dispositivos,
      };

      final response =
          await api.put('/api/sesiones-canales-electronicos', form);

      return RemoverDispositivoSeguroResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al remover los dispositivos seguros.', e);
      throw ServiceException(errorMessage);
    }
  }
}
