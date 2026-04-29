import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/constants/environment.dart';
import 'package:caja_tacna_app/constants/storage_keys.dart';
import 'package:caja_tacna_app/core/managers/dialog_manager.dart';
import 'package:caja_tacna_app/core/models/encrypt_response.dart';
import 'package:caja_tacna_app/core/services/encrypt_service.dart';
import 'package:caja_tacna_app/features/shared/events/auth_event.dart';
import 'package:caja_tacna_app/features/shared/events/dismiss_loader_event.dart';
import 'package:caja_tacna_app/features/shared/services/storage_service.dart';
import 'package:dio/dio.dart';
import 'dart:convert';

import 'package:http_certificate_pinning/http_certificate_pinning.dart';

class Api {
  final Dio _dioAuth = Dio(BaseOptions(baseUrl: Environment.urlAuth));
  final Dio _dioBase = Dio(BaseOptions(baseUrl: Environment.urlBase));

  final bool serviciosPuente = Environment.serviciosPuente;
  final bool encriptacion = Environment.encriptacion;
  final bool validarCertificado = Environment.validarCertificado;

  final storageService = StorageService();

  String tokenCajaTacna = '';
  String xsp = '';
  String xua = '';

  Api() {
    _dioAuth.interceptors.add(InterceptorsWrapper(
      onRequest: (options, handler) async {
        await _obtenerCredencialesGateway();

        //servicios puente
        if (serviciosPuente) {
          options.headers['X-Token'] = tokenCajaTacna;
          options.headers['X-UA'] = xua;
          options.headers['X-SP'] = xsp;
          options.headers['Authorization'] = tokenCajaTacna;
          options.headers['Content-Type'] = 'application/x-www-form-urlencoded';
        } else {
          //servicios directos a caja tacna

          options.headers['Authorization'] = 'Bearer $tokenCajaTacna';
          options.headers['Content-Type'] = 'application/x-www-form-urlencoded';
        }
        
        if (encriptacion) options.headers['X-Encryption'] = 'gcm';

        return handler.next(options);
      },
      onResponse: interceptorResponse,
      onError: interceptorError,
    ));

    if (validarCertificado) {
      _dioAuth.interceptors.add(CertificatePinningInterceptor(
          allowedSHAFingerprints: [Environment.shaFingerprint]));
    }

    //servicios directos
    _dioBase.interceptors.add(InterceptorsWrapper(
      onRequest: (options, handler) async {
        await _obtenerCredencialesGateway();

        // Verifica si la solicitud es POST o PUT
        if (encriptacion &&
            (options.method == 'POST' ||
                options.method == 'PUT' ||
                options.method == 'DELETE')) {
          // Intercepta los datos de la solicitud
          if (options.data is Map<String, dynamic>) {
            final dataEncriptada =
                await EncryptService.encrypt(json.encode(options.data));

            options.data = dataEncriptada;
          }
        }

        //servicios puente
        if (serviciosPuente) {
          options.headers['X-Token'] = tokenCajaTacna;
          options.headers['X-UA'] = xua;
          options.headers['X-SP'] = xsp;
          options.headers['Authorization'] = tokenCajaTacna;
          options.headers['Content-Type'] = 'application/json';
        } else {
          //servicios directos a caja tacna

          options.headers['Authorization'] = 'Bearer $tokenCajaTacna';
          options.headers['Content-Type'] = 'application/json';
        }

        if (encriptacion) options.headers['X-Encryption'] = 'gcm';

        return handler.next(options);
      },
      onResponse: interceptorResponse,
      onError: interceptorError,
    ));

    if (validarCertificado) {
      _dioBase.interceptors.add(CertificatePinningInterceptor(
          allowedSHAFingerprints: [Environment.shaFingerprint]));
    }
  }

  Future<Response> postAuth(String path, Object data) async {
    return _dioAuth.post(path, data: data);
  }

  Future<Response> get(
    String path, {
    Map<String, dynamic>? queryParameters,
  }) async {
    return _dioBase.get(path, queryParameters: queryParameters);
  }

  Future<Response> post(String path, Object data) async {
    return _dioBase.post(path, data: data);
  }

  Future<Response> put(String path, Object data) async {
    return _dioBase.put(path, data: data);
  }

  Future<Response> delete(String path, {Object? data}) async {
    return _dioBase.delete(path, data: data);
  }

  _obtenerCredencialesGateway() async {
    tokenCajaTacna = await storageService.get<String>(StorageKeys.token) ?? '';
  }

  esJsonEncriptado(Map<String, dynamic> json) {
    return json['ct'] != null && json['iv'] != null && json['s'] != null;
  }

  interceptorResponse(
    Response<dynamic> response,
    ResponseInterceptorHandler handler,
  ) async {
    // Desencriptar la respuesta si es necesario
    if (encriptacion) {
      // Obtener los datos de la respuesta
      var responseData = response.data;

      // Desencriptar los datos si es de tipo Map<String, dynamic>
      if (responseData is Map<String, dynamic>) {
        try {
          final EncrypResponse encrypResponse =
              EncrypResponse.fromJson(responseData);
          response.data = await EncryptService.decrypt(encrypResponse);
        } catch (e) {
          return handler.reject(
            DioException.badResponse(
              statusCode: 400,
              requestOptions: RequestOptions(),
              response: response,
            ),
          );
        }
      }
    }

    return handler.next(response);
  }

  interceptorError(DioException e, ErrorInterceptorHandler handler) async {
    if (e.response?.statusCode == 401) {
      DissmissLoaderEvents.notifyDismissLoader();

      AuthEvents.notifyLogout();
      await Future.delayed(const Duration(milliseconds: 300));

      DialogManager()
          .showSessionExpiredDialog(rootNavigatorKey.currentContext!);
    } else {
      // Obtener los datos de la respuesta
      var responseData = e.response?.data;

      // Desencriptar los datos si es de tipo Map<String, dynamic> encriptado
      if (responseData is Map<String, dynamic> &&
          esJsonEncriptado(responseData)) {
        try {
          final EncrypResponse encrypResponse =
              EncrypResponse.fromJson(responseData);
          e.response?.data = await EncryptService.decrypt(encrypResponse);
        } catch (error) {
          return handler.next(e);
        }
      }
    }

    return handler.next(e);
  }
}
