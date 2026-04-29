import 'package:caja_tacna_app/features/shared/models/select_cuenta_option.dart';

class ProductoDebito extends SelectCuentaOption {
  final String nombreCliente;
  final String codigoSistema;
  final String nombreMonedaProducto;
  final double saldoContable;
  final String codigoMonedaProducto;
  final String descripcionMonto;
  final String nombreTipoProducto;
  final String nombreTipoProductoCorto;
  final double montoCuota;
  final DateTime fechaCuotaVigente;
  final double tea;

  ProductoDebito({
    required this.nombreCliente,
    required this.codigoSistema,
    required String alias,
    required String numeroProducto,
    required String simboloMonedaProducto,
    required this.nombreMonedaProducto,
    required double montoSaldo,
    required this.saldoContable,
    required this.codigoMonedaProducto,
    required this.descripcionMonto,
    required this.nombreTipoProducto,
    required this.nombreTipoProductoCorto,
    required this.montoCuota,
    required this.fechaCuotaVigente,
    required this.tea,
  }) : super(
          alias: alias,
          numeroProducto: numeroProducto,
          simboloMonedaProducto: simboloMonedaProducto,
          montoSaldo: montoSaldo,
        );

  factory ProductoDebito.fromJson(Map<String, dynamic> json) => ProductoDebito(
        nombreCliente: json["NombreCliente"],
        codigoSistema: json["CodigoSistema"],
        alias: json["Alias"],
        numeroProducto: json["NumeroProducto"],
        simboloMonedaProducto: json["SimboloMonedaProducto"],
        nombreMonedaProducto: json["NombreMonedaProducto"],
        montoSaldo: json["MontoSaldo"]?.toDouble(),
        saldoContable: json["SaldoContable"]?.toDouble(),
        codigoMonedaProducto: json["CodigoMonedaProducto"],
        descripcionMonto: json["DescripcionMonto"],
        nombreTipoProducto: json["NombreTipoProducto"],
        nombreTipoProductoCorto: json["NombreTipoProductoCorto"],
        montoCuota: json["MontoCuota"]?.toDouble(),
        fechaCuotaVigente: DateTime.parse(json["FechaCuotaVigente"]),
        tea: json["TEA"]?.toDouble(),
      );

  Map<String, dynamic> toJson() => {
        "NombreCliente": nombreCliente,
        "CodigoSistema": codigoSistema,
        "Alias": alias,
        "NumeroProducto": numeroProducto,
        "SimboloMonedaProducto": simboloMonedaProducto,
        "NombreMonedaProducto": nombreMonedaProducto,
        "MontoSaldo": montoSaldo,
        "SaldoContable": saldoContable,
        "CodigoMonedaProducto": codigoMonedaProducto,
        "DescripcionMonto": descripcionMonto,
        "NombreTipoProducto": nombreTipoProducto,
        "NombreTipoProductoCorto": nombreTipoProductoCorto,
        "MontoCuota": montoCuota,
        "FechaCuotaVigente": fechaCuotaVigente.toIso8601String(),
        "TEA": tea,
      };
}
