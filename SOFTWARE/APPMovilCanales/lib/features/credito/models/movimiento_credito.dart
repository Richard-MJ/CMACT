class MovimientoCredito {
  final double montoTotalPagar;
  final DateTime fechaRecibo;
  final String canal;
  final String descripcionTransaccion;

  MovimientoCredito({
    required this.montoTotalPagar,
    required this.fechaRecibo,
    required this.canal,
    required this.descripcionTransaccion,
  });

  factory MovimientoCredito.fromJson(Map<String, dynamic> json) =>
      MovimientoCredito(
        montoTotalPagar: json["MontoTotalPagar"]?.toDouble(),
        fechaRecibo: DateTime.parse(json["FechaRecibo"]),
        canal: json["Canal"],
        descripcionTransaccion: json["DescripcionTransaccion"],
      );

  Map<String, dynamic> toJson() => {
        "MontoTotalPagar": montoTotalPagar,
        "FechaRecibo": fechaRecibo.toIso8601String(),
        "Canal": canal,
        "DescripcionTransaccion": descripcionTransaccion,
      };
}
