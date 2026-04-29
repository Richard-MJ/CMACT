import 'package:caja_tacna_app/features/pago_tarjetas_credito/diferidas/models/entidad_financiera.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/diferidas/models/producto_debito.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/diferidas/models/tipo_documento.dart';

class DatosInicialesResponse {
  final List<ProductoDebito> productosDebito;
  final List<TipoDocumentoPagoTarjeta> tiposDocumento;
  final List<EntidadFinanciera> entidadesFinancieras;

  DatosInicialesResponse({
    required this.productosDebito,
    required this.tiposDocumento,
    required this.entidadesFinancieras,
  });

  factory DatosInicialesResponse.fromJson(Map<String, dynamic> json) =>
      DatosInicialesResponse(
        productosDebito: List<ProductoDebito>.from(
            json["ProductosDebito"].map((x) => ProductoDebito.fromJson(x))),
        tiposDocumento: List<TipoDocumentoPagoTarjeta>.from(
            json["TiposDocumento"]
                .map((x) => TipoDocumentoPagoTarjeta.fromJson(x))),
        entidadesFinancieras: List<EntidadFinanciera>.from(
            json["EntidadesFinancieras"]
                .map((x) => EntidadFinanciera.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "ProductosDebito":
            List<dynamic>.from(productosDebito.map((x) => x.toJson())),
        "TiposDocumento":
            List<dynamic>.from(tiposDocumento.map((x) => x.toJson())),
        "EntidadesFinancieras":
            List<dynamic>.from(entidadesFinancieras.map((x) => x.toJson())),
      };
}
