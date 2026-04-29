import 'package:caja_tacna_app/features/shared/models/select_cuenta_option.dart';

class CuentaDestinoAdelSuel extends SelectCuentaOption {
  final String codigoMoneda;
  final double montoComision;
  final double montoMinimo;
  final double montoMaximo;

  CuentaDestinoAdelSuel({
    required String alias,
    required String numeroProducto,
    required String simboloMonedaProducto,
    required double montoSaldo,
    required this.codigoMoneda,
    required this.montoComision,
    required this.montoMinimo,
    required this.montoMaximo,
  }) : super(
          alias: alias,
          numeroProducto: numeroProducto,
          simboloMonedaProducto: simboloMonedaProducto,
          montoSaldo: montoSaldo,
        );

  factory CuentaDestinoAdelSuel.fromJson(Map<String, dynamic> json) =>
      CuentaDestinoAdelSuel(
        alias: json["Alias"],
        numeroProducto: json["NumeroProducto"],
        codigoMoneda: json["CodigoMoneda"],
        simboloMonedaProducto: json["SimboloMoneda"],
        montoComision: json["MontoComision"]?.toDouble(),
        montoMinimo: json["MontoMinimo"]?.toDouble(),
        montoMaximo: json["MontoMaximo"]?.toDouble(),
        montoSaldo: json["MontoSaldo"]?.toDouble(),
      );

  Map<String, dynamic> toJson() => {
        "Alias": alias,
        "NumeroProducto": numeroProducto,
        "CodigoMoneda": codigoMoneda,
        "SimboloMoneda": simboloMonedaProducto,
        "MontoComision": montoComision,
        "MontoMinimo": montoMinimo,
        "MontoMaximo": montoMaximo,
        "MontoSaldo": montoSaldo,
      };
}
