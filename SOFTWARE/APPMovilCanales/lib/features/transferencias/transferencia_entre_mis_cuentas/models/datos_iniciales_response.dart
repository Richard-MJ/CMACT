import 'package:caja_tacna_app/features/transferencias/models/cuenta_transferencia.dart';

class DatosInicialesResponse {
  final List<CuentaTransferencia> productosDebito;
  final List<CuentaTransferencia> productosCredito;

  DatosInicialesResponse({
    required this.productosDebito,
    required this.productosCredito,
  });

  factory DatosInicialesResponse.fromJson(Map<String, dynamic> json) =>
      DatosInicialesResponse(
        productosDebito: List<CuentaTransferencia>.from(
            json["ProductosDebito"].map((x) => CuentaTransferencia.fromJson(x))),
        productosCredito: List<CuentaTransferencia>.from(
            json["ProductosCredito"].map((x) => CuentaTransferencia.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "ProductosDebito":
            List<dynamic>.from(productosDebito.map((x) => x.toJson())),
        "ProductosCredito":
            List<dynamic>.from(productosCredito.map((x) => x.toJson())),
      };
}
