import 'package:caja_tacna_app/features/shared/models/reenvio_comprobante_operacion_response.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/core/services/error_service.dart';
import 'package:path_provider/path_provider.dart';
import 'package:caja_tacna_app/api/api.dart';
import 'package:open_file/open_file.dart';
import 'package:http/http.dart' as http;
import 'dart:typed_data';
import 'dart:async';
import 'dart:io';

final api = Api();

class SharedService {
  static Future<ReenvioComprobanteOperacionResponse> reenviarComprobante({
    required String? tipoOperacion,
    required String correoElectronicoDestinatario,
    required int? idOperacionTts,
    int? codigoTipoAnticipo = 0,
    int? codigoTipoSolicitante = 0,
    int? codigoTipoPago = 0,
  }) async {
    try {
      Map<String, dynamic> form = {
        "TipoOperacion": tipoOperacion,
        "CorreoElectronicoDestinatario": correoElectronicoDestinatario,
        "IdOperacionTts": idOperacionTts,
        "CodigoTipoAnticipo": codigoTipoAnticipo,
        "CodigoTipoSolicitante": codigoTipoSolicitante,
        "CodigoTipoPago": codigoTipoPago
      };

      final response = await api.post(
        '/api/ventanillas/enviocorreo',
        form,
      );

      return ReenvioComprobanteOperacionResponse.fromJson(response.data);
    } catch (e) {
      String errorMessage = ErrorService.verificarErrorBase(
          'Ocurrió un error al reenviar el comprobante.', e);
      throw ServiceException(errorMessage);
    }
  }

  static Future<void> mostrarArchivoDocumento({
    required String nombreDocumento,
    required Uint8List documento,
  }) async {
    try {
      final dir = await getApplicationDocumentsDirectory();
      final file = File("${dir.path}/$nombreDocumento");

      await file.writeAsBytes(documento, flush: true);

      await OpenFile.open(file.path);
    } catch (e) {
      throw Exception('Ocurrió un error al visualizar el documento: $e');
    }
  }

  static Future<Uint8List> convertirPdfAsBytes(String url, {Map<String, String>? headers}) async {
    final uri = Uri.parse(url);
    final response = await http.get(uri, headers: headers);
    if (response.statusCode == 200) {
      return response.bodyBytes;
    } else {
      throw Exception('Error al descargar PDF: código ${response.statusCode}');
    }
  }
}
