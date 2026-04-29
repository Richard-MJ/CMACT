class Deuda {
  final String codigoPago;
  final DateTime fechaCreacion;
  final DateTime fechaVencimiento;
  final double monto;
  final String codigoMoneda;
  final String simboloMoneda;
  final String descripcionMoneda;
  final String lugar;
  final String codigoEstado;
  final String descripcionEstado;

  Deuda({
    required this.codigoPago,
    required this.fechaCreacion,
    required this.fechaVencimiento,
    required this.monto,
    required this.codigoMoneda,
    required this.simboloMoneda,
    required this.descripcionMoneda,
    required this.lugar,
    required this.codigoEstado,
    required this.descripcionEstado,
  });

  factory Deuda.fromJson(Map<String, dynamic> json) => Deuda(
        codigoPago: json["CodigoPago"],
        fechaCreacion: DateTime.parse(json["FechaCreacion"]),
        fechaVencimiento: DateTime.parse(json["FechaVencimiento"]),
        monto: json["Monto"]?.toDouble(),
        codigoMoneda: json["CodigoMoneda"],
        simboloMoneda: json["SimboloMoneda"],
        descripcionMoneda: json["DescripcionMoneda"],
        lugar: json["Lugar"],
        codigoEstado: json["CodigoEstado"],
        descripcionEstado: json["DescripcionEstado"],
      );

  Map<String, dynamic> toJson() => {
        "CodigoPago": codigoPago,
        "FechaCreacion": fechaCreacion.toIso8601String(),
        "FechaVencimiento": fechaVencimiento.toIso8601String(),
        "Monto": monto,
        "CodigoMoneda": codigoMoneda,
        "SimboloMoneda": simboloMoneda,
        "DescripcionMoneda": descripcionMoneda,
        "Lugar": lugar,
        "CodigoEstado": codigoEstado,
        "DescripcionEstado": descripcionEstado,
      };
}
