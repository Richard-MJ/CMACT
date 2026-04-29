import 'package:caja_tacna_app/features/pago_safetypay/models/cuenta_origen.dart';

class DatosInicialesResponse {
  final List<CuentaOrigenSafety> productosDebito;
  final List<Moneda> monedas;

  DatosInicialesResponse({
    required this.productosDebito,
    required this.monedas,
  });

  factory DatosInicialesResponse.fromJson(Map<String, dynamic> json) =>
      DatosInicialesResponse(
        productosDebito: List<CuentaOrigenSafety>.from(
            json["ProductosDebito"].map((x) => CuentaOrigenSafety.fromJson(x))),
        monedas:
            List<Moneda>.from(json["Monedas"].map((x) => Moneda.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "ProductosDebito":
            List<dynamic>.from(productosDebito.map((x) => x.toJson())),
        "Monedas": List<dynamic>.from(monedas.map((x) => x.toJson())),
      };
}

class Moneda {
  final String codigoMoneda;
  final String nombreMoneda;
  final String monedaMl;

  Moneda({
    required this.codigoMoneda,
    required this.nombreMoneda,
    required this.monedaMl,
  });

  factory Moneda.fromJson(Map<String, dynamic> json) => Moneda(
        codigoMoneda: json["CodigoMoneda"],
        nombreMoneda: json["NombreMoneda"],
        monedaMl: json["Moneda_ML"],
      );

  Map<String, dynamic> toJson() => {
        "CodigoMoneda": codigoMoneda,
        "NombreMoneda": nombreMoneda,
        "Moneda_ML": monedaMl,
      };
}
