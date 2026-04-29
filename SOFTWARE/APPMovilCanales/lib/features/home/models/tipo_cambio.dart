class TipoCambio {
  final String descripcionMoneda;
  final DateTime fechaCotizacion;
  final double montoCompra;
  final double montoVenta;
  final String simboloMoneda;

  TipoCambio({
    required this.descripcionMoneda,
    required this.fechaCotizacion,
    required this.montoCompra,
    required this.montoVenta,
    required this.simboloMoneda,
  });

  factory TipoCambio.fromJson(Map<String, dynamic> json) => TipoCambio(
        descripcionMoneda: json["DescripcionMoneda"],
        fechaCotizacion: DateTime.parse(json["FechaCotizacion"]),
        montoCompra: json["MontoCompra"]?.toDouble(),
        montoVenta: json["MontoVenta"]?.toDouble(),
        simboloMoneda: json["SimboloMoneda"],
      );

  Map<String, dynamic> toJson() => {
        "DescripcionMoneda": descripcionMoneda,
        "FechaCotizacion": fechaCotizacion.toIso8601String(),
        "MontoCompra": montoCompra,
        "MontoVenta": montoVenta,
        "SimboloMoneda": simboloMoneda,
      };
}
