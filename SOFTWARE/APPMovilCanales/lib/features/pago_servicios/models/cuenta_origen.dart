import 'package:caja_tacna_app/features/shared/models/select_cuenta_option.dart';

class CuentaOrigenPagServ extends SelectCuentaOption {
  final String nombreMonedaProducto;
  final String codigoMonedaProducto;
  final String nombreTipoProducto;
  final String nombreTipoProductoCorto;
  final double montoCuota;
  final DateTime fechaCuotaVigente;
  final double montoItf;

  CuentaOrigenPagServ({
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
  }) : super(
          alias: alias,
          numeroProducto: numeroProducto,
          simboloMonedaProducto: simboloMonedaProducto,
          montoSaldo: montoSaldo,
        );

  factory CuentaOrigenPagServ.fromJson(Map<String, dynamic> json) =>
      CuentaOrigenPagServ(
        alias: json["Alias"],
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
      };
}
