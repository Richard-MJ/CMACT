import 'package:caja_tacna_app/features/configurar_cuentas/models/cuenta_destino.dart';

class DatosInicialesResponse {
  final List<CuentaDestinoCancCuenta> productosCredito;

  DatosInicialesResponse({
    required this.productosCredito,
  });

  factory DatosInicialesResponse.fromJson(Map<String, dynamic> json) =>
      DatosInicialesResponse(
        productosCredito: List<CuentaDestinoCancCuenta>.from(
            json["ProductosCredito"]
                .map((x) => CuentaDestinoCancCuenta.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "ProductosCredito":
            List<dynamic>.from(productosCredito.map((x) => x.toJson())),
      };
}
