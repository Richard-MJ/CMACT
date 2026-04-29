import 'package:caja_tacna_app/features/shared/models/select_cuenta_option.dart';

class CuentaOrigenApertura extends SelectCuentaOption {
  final String nombreTipoProducto;
  final String nombreTipoProductoCorto;
  final String codigoMonedaProducto;
  final String nombreMonedaProducto;
  final double montoCuota;
  final double montoItf;
  final DateTime fechaCuotaVigente;
  final String codigoTipo;

  CuentaOrigenApertura({
    required String alias,
    required String numeroProducto,
    required String simboloMonedaProducto,
    required this.nombreMonedaProducto,
    required double montoSaldo,
    required this.codigoMonedaProducto,
    required this.nombreTipoProducto,
    required this.nombreTipoProductoCorto,
    required this.montoCuota,
    required this.fechaCuotaVigente,
    required this.montoItf,
    required this.codigoTipo,
  }) : super(
          alias: alias,
          numeroProducto: numeroProducto,
          simboloMonedaProducto: simboloMonedaProducto,
          montoSaldo: montoSaldo,
        );

  factory CuentaOrigenApertura.fromJson(Map<String, dynamic> json) =>
      CuentaOrigenApertura(
        alias: json["Alias"] ?? '',
        numeroProducto: json["NumeroProducto"],
        simboloMonedaProducto: json["SimboloMonedaProducto"],
        nombreMonedaProducto: json["NombreMonedaProducto"],
        montoSaldo: json["MontoSaldo"]?.toDouble(),
        codigoMonedaProducto: json["CodigoMonedaProducto"],
        nombreTipoProducto: json["NombreTipoProducto"],
        nombreTipoProductoCorto: json["NombreTipoProductoCorto"],
        montoCuota: json["MontoCuota"]?.toDouble(),
        fechaCuotaVigente: DateTime.parse(json["FechaCuotaVigente"]),
        montoItf: json["MontoItf"]?.toDouble(),
        codigoTipo: json["CodigoTipo"],
      );

  Map<String, dynamic> toJson() => {
        "Alias": alias,
        "NumeroProducto": numeroProducto,
        "SimboloMonedaProducto": simboloMonedaProducto,
        "NombreMonedaProducto": nombreMonedaProducto,
        "MontoSaldo": montoSaldo,
        "CodigoMonedaProducto": codigoMonedaProducto,
        "NombreTipoProducto": nombreTipoProducto,
        "NombreTipoProductoCorto": nombreTipoProductoCorto,
        "MontoCuota": montoCuota,
        "FechaCuotaVigente": fechaCuotaVigente.toIso8601String(),
        "MontoItf": montoItf,
        "CodigoTipo": codigoTipo,
      };
}
