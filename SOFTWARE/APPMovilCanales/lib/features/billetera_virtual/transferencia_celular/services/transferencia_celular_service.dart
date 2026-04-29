import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/models/lista_entidad_financiera_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/models/datos_operacion_exitosa_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/models/datos_consulta_cuenta_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/models/datos_cliente_origen_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/models/montos_totales_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/models/datos_afiliacion_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/models/lista_contacto_barrido.dart';
import 'package:caja_tacna_app/features/billetera_virtual/models/token_response.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:caja_tacna_app/api/api.dart';

final api = Api();

class TransferenciaCelularService {
  static Future<DatosValidacionResponse> obtenerDatosAfiliacion() async {
    try {
      final response = await api.get('/api/interoperabilidad/validar-afiliacion');

      return DatosValidacionResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los datos.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<DatosClienteOrigenResponse> obtenerDatosCuentaOriginante(
    String numeroCuenta) async {
    try {      
      
      Map<String, dynamic> form = {
        "NumeroCuenta": numeroCuenta
      };

      final response =
          await api.post('/api/interoperabilidad/consulta-cuenta-originante', form);

      return DatosClienteOrigenResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al inicializar los datos del cliente originante.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<DatosConsultaCuentaResponse> consultaCuentaClienteReceptor(
    { required numeroCelular, 
      required codigoEntidad, 
      required cuentaEfectivo }
    ) async {
    try {      
      
      Map<String, dynamic> form = {
        "NumeroCelular": numeroCelular,
        "CodigoEntidad": codigoEntidad,
        "CuentaEfectivo": cuentaEfectivo,
      };

      final response =
          await api.post('/api/interoperabilidad/consulta-cuenta-receptor', form);

      return DatosConsultaCuentaResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al realizar la consulta cuenta del cliente receptor.', e);
      throw ServiceException(errorMessage);
    }
  }

    static Future<DatosConsultaCuentaPorQrResponse> obtenerDatosPorQR(
    {required tokenCodigoQr, required numeroCuenta}) async {
    try {      
      
      Map<String, dynamic> form = {
        "NumeroCuentaOriginante": numeroCuenta,
        "CadenaHash": tokenCodigoQr
      };

      final response =
          await api.post('/api/interoperabilidad/consulta-cuenta-qr', form);

      return DatosConsultaCuentaPorQrResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al realizar la consulta de Cuenta por Qr.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<MontosTotales> calcularMontosTotales(
    { 
      required numeroCuenta,
      required mismoTitular, 
      required saldoActual, 
      required montoOperacion,
      required montoMinimoCuenta,
      required esExoneradaItf,
      required esCuentaSueldo,
      required comision,
      required esExoneradoComision }
    ) async {
    try {      
      
      Map<String, dynamic> form = {
        "NumeroCuenta" : numeroCuenta,
        "MismoTitular" : mismoTitular,
        "SaldoActual" : saldoActual,
        "MontoOperacion" : montoOperacion,
        "MontoMinimoCuenta" : montoMinimoCuenta,
        "EsExoneradaITF" : esExoneradaItf,
        "EsCuentaSueldo" : esCuentaSueldo,
        "Comision": comision,
        "EsExoneradoComision" : esExoneradoComision,
      };

      final response =
          await api.post('/api/interoperabilidad/calcular-totales', form);

      return MontosTotales.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener los datos del cliente originante.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<TokenResponse> obtenerTokenDigital(
    {required codigoMonedaCuenta, required identificadorDispositivo}) async {
    try {

      Map<String, dynamic> form = {
        "CodigoMonedaCuenta": codigoMonedaCuenta,
        "IdentificadorDispositivo": identificadorDispositivo,
      };

      final response = await api.post('/api/interoperabilidad/confirmar', form);

      return TokenResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al obtener el token.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<DatosRespuestaBarrido> barridoContacto(
      {required numeroCelular, required nombreAlias, required codigoCCi, required numeroCelularOrigen}) async {
    try {

      Map<String, dynamic> form = {
        "CodigoCCI": codigoCCi,
        "NumeroCelularOrigen": numeroCelularOrigen,
        "ContactosBarrido":  [
          ContactosBarrido(
              numeroCelular: numeroCelular, 
              nombreAlias: nombreAlias)
          ],
      };

      final response =
          await api.post('/api/interoperabilidad/barrido', form);

      return DatosRespuestaBarrido.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al invocar el barrido de contacto.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<void> validarToken({required numeroVerificacion}) async {
    try {
      Map<String, dynamic> form = {
          "CodigoAutorizacion" : numeroVerificacion,
      };

      await api.post('/api/interoperabilidad/validar-token', form);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al validar el Token.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<DatosOperacionExitosaResponse> confirmarTransferencia(
    {required numeroCuenta, 
    required controlMonto, 
    required detalleTransferencia, 
    required identificadorQR,
    required numeroTarjeta,
    required entidadDestino,
    required celularOriginante,
    required celularReceptor}) async {
    try {

      Map<String, dynamic> form = {
        "NumeroTarjeta": numeroTarjeta,
        "EntidadDestino": entidadDestino,
        "IdentificadorQR": identificadorQR,
        "NumeroCuenta": numeroCuenta,
        "ControlMonto": controlMonto,
        "ResultadoConsultaCuenta": detalleTransferencia,
        "NumeroCelularOriginante": celularOriginante,
        "NumeroCelularReceptor": celularReceptor,
      };

      final response = await api.post('/api/interoperabilidad/realizar-orden-transferencia', form);

      return DatosOperacionExitosaResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al confirmar la operación.', e);
      throw ServiceException(errorMessage);
    }
  }
}
