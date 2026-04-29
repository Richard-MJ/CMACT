import 'package:caja_tacna_app/features/apertura_cuentas/models/cuenta_origen.dart';

class DatosInicialesResponse {
  final List<ProductoApertura> productosApertura;
  final List<CuentaOrigenApertura> productosDebito;
  final GestionTdp gestionTdp;

  DatosInicialesResponse({
    required this.productosApertura,
    required this.productosDebito,
    required this.gestionTdp,
  });

  factory DatosInicialesResponse.fromJson(Map<String, dynamic> json) =>
      DatosInicialesResponse(
        productosApertura: List<ProductoApertura>.from(
            json["ProductosApertura"].map((x) => ProductoApertura.fromJson(x))),
        productosDebito: List<CuentaOrigenApertura>.from(json["ProductosDebito"]
            .map((x) => CuentaOrigenApertura.fromJson(x))),
        gestionTdp: GestionTdp.fromJson(json["GestionTdp"]),
      );

  Map<String, dynamic> toJson() => {
        "ProductosApertura":
            List<dynamic>.from(productosApertura.map((x) => x.toJson())),
        "ProductosDebito":
            List<dynamic>.from(productosDebito.map((x) => x.toJson())),
        "GestionTdp": gestionTdp.toJson(),
      };
}

class GestionTdp {
  final bool indicadorConocimientoDatosPersonales;
  final bool indicadorUsoDatosPersonales;

  GestionTdp({
    required this.indicadorConocimientoDatosPersonales,
    required this.indicadorUsoDatosPersonales,
  });

  factory GestionTdp.fromJson(Map<String, dynamic> json) => GestionTdp(
        indicadorConocimientoDatosPersonales:
            json["IndicadorConocimientoDatosPersonales"],
        indicadorUsoDatosPersonales: json["IndicadorUsoDatosPersonales"],
      );

  Map<String, dynamic> toJson() => {
        "IndicadorConocimientoDatosPersonales":
            indicadorConocimientoDatosPersonales,
        "IndicadorUsoDatosPersonales": indicadorUsoDatosPersonales,
      };
}

class ProductoApertura {
  final String codigoSistema;
  final String codigoProducto;
  final String codigoMoneda;
  final String nombreProducto;
  final String descripcionProducto;
  final String descripcionMoneda;
  final double montoMinimo;
  final String? tasaEfectivaNominal;
  final String? tasaEfectivaAnual;

  ProductoApertura({
    required this.codigoSistema,
    required this.codigoProducto,
    required this.codigoMoneda,
    required this.nombreProducto,
    required this.descripcionProducto,
    required this.descripcionMoneda,
    required this.montoMinimo,
    required this.tasaEfectivaNominal,
    required this.tasaEfectivaAnual,
  });

  factory ProductoApertura.fromJson(Map<String, dynamic> json) =>
      ProductoApertura(
        codigoSistema: json["CodigoSistema"],
        codigoProducto: json["CodigoProducto"],
        codigoMoneda: json["CodigoMoneda"],
        nombreProducto: json["NombreProducto"],
        descripcionProducto: json["DescripcionProducto"],
        descripcionMoneda: json["DescripcionMoneda"],
        montoMinimo: json["MontoMinimo"]?.toDouble(),
        tasaEfectivaNominal: json["TasaEfectivaNominal"],
        tasaEfectivaAnual: json["TasaEfectivaAnual"],
      );

  Map<String, dynamic> toJson() => {
        "CodigoSistema": codigoSistema,
        "CodigoProducto": codigoProducto,
        "CodigoMoneda": codigoMoneda,
        "NombreProducto": nombreProducto,
        "DescripcionProducto": descripcionProducto,
        "DescripcionMoneda": descripcionMoneda,
        "MontoMinimo": montoMinimo,
        "TasaEfectivaNominal": tasaEfectivaNominal,
        "TasaEfectivaAnual": tasaEfectivaAnual,
      };
}
