import 'package:caja_tacna_app/core/models/error_cuota.dart';
import 'package:caja_tacna_app/features/shared/models/error_manager.dart';
import 'package:caja_tacna_app/features/shared/models/error_manager_auth.dart';
import 'package:dio/dio.dart';

class ErrorService {
  static String verificarErrorBase(String errorMessage, Object e) {
    if (e is DioException) {
      if (e.response?.statusCode == 429) {
        try {
          final errorCuota = ErrorCuota.fromJson(e.response?.data);
          errorMessage = errorCuota.errorDescription;
        } catch (_) {}
      }
      if (e.response?.statusCode == 400) {
        try {
          final error = ErrorManager.fromJson(e.response?.data);
          errorMessage = error.mensaje;
        } catch (_) {}
      }
    }

    return errorMessage;
  }

  static String verificarErrorAuth(String errorMessage, Object e) {
    if (e is DioException) {
      if (e.response?.statusCode == 429) {
        try {
          final errorCuota = ErrorCuota.fromJson(e.response?.data);
          errorMessage = errorCuota.errorDescription;
        } catch (_) {}
      }
      if (e.response?.statusCode == 400) {
        try {
          final error = ErrorManagerAuth.fromJson(e.response?.data);
          errorMessage = error.errorDescription;
        } catch (_) {}
      }
    }

    return errorMessage;
  }
}
