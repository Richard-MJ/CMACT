import 'package:caja_tacna_app/features/shared/models/select_cuenta_option.dart';

class CuentaDestinoCancCuenta extends SelectCuentaOption {
  final String nombreTipoProducto;
  final String nombreTipoProductoCorto;
  final String codigoMonedaProducto;
  final String nombreMonedaProducto;
  final double montoCuota;
  final double montoItf;
  final DateTime fechaCuotaVigente;
  final String codigoTipo;

  CuentaDestinoCancCuenta({
    required String alias,
    required this.nombreTipoProducto,
    required this.nombreTipoProductoCorto,
    required String numeroProducto,
    required this.codigoMonedaProducto,
    required this.nombreMonedaProducto,
    required String simboloMonedaProducto,
    required double montoSaldo,
    required this.montoCuota,
    required this.montoItf,
    required this.fechaCuotaVigente,
    required this.codigoTipo,
  }) : super(
          alias: alias,
          numeroProducto: numeroProducto,
          simboloMonedaProducto: simboloMonedaProducto,
          montoSaldo: montoSaldo,
        );

  factory CuentaDestinoCancCuenta.fromJson(Map<String, dynamic> json) =>
      CuentaDestinoCancCuenta(
        alias: json["Alias"] ?? '',
        nombreTipoProducto: json["NombreTipoProducto"],
        nombreTipoProductoCorto: json["NombreTipoProductoCorto"],
        numeroProducto: json["NumeroProducto"],
        codigoMonedaProducto: json["CodigoMonedaProducto"],
        nombreMonedaProducto: json["NombreMonedaProducto"],
        simboloMonedaProducto: json["SimboloMonedaProducto"],
        montoSaldo: json["MontoSaldo"]?.toDouble(),
        montoCuota: json["MontoCuota"]?.toDouble(),
        montoItf: json["MontoItf"]?.toDouble(),
        fechaCuotaVigente: DateTime.parse(json["FechaCuotaVigente"]),
        codigoTipo: json["CodigoTipo"],
      );

  Map<String, dynamic> toJson() => {
        "Alias": alias,
        "NombreTipoProducto": nombreTipoProducto,
        "NombreTipoProductoCorto": nombreTipoProductoCorto,
        "NumeroProducto": numeroProducto,
        "CodigoMonedaProducto": codigoMonedaProducto,
        "NombreMonedaProducto": nombreMonedaProducto,
        "SimboloMonedaProducto": simboloMonedaProducto,
        "MontoSaldo": montoSaldo,
        "MontoCuota": montoCuota,
        "MontoItf": montoItf,
        "FechaCuotaVigente": fechaCuotaVigente.toIso8601String(),
        "CodigoTipo": codigoTipo,
      };
}
