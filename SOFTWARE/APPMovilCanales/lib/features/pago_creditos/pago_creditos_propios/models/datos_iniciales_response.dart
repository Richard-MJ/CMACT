import 'package:caja_tacna_app/features/pago_creditos/models/credito_abonar.dart';
import 'package:caja_tacna_app/features/pago_creditos/models/producto_debito.dart';

class DatosInicialesResponse {
  final List<ProductoDebito> productosDebito;
  final List<CreditoAbonar> creditosAbonar;

  DatosInicialesResponse({
    required this.productosDebito,
    required this.creditosAbonar,
  });

  factory DatosInicialesResponse.fromJson(Map<String, dynamic> json) =>
      DatosInicialesResponse(
        productosDebito: List<ProductoDebito>.from(
            json["ProductosDebito"].map((x) => ProductoDebito.fromJson(x))),
        creditosAbonar: List<CreditoAbonar>.from(
            json["CreditosAbonar"].map((x) => CreditoAbonar.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "ProductosDebito":
            List<dynamic>.from(productosDebito.map((x) => x.toJson())),
        "CreditosAbonar":
            List<dynamic>.from(creditosAbonar.map((x) => x.toJson())),
      };
}
