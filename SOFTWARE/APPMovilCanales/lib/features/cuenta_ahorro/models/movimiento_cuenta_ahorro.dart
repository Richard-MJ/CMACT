class MovimientoCuentaAhorro {
  final String descripcion;
  final String descripcionDetallada;
  final DateTime fecha;
  final double monto;
  final int numero;
  final dynamic ruc;
  final String signoMovimiento;
  final String simboloMoneda;
  final String canalOrigen;
  final String reversado;

  MovimientoCuentaAhorro({
    required this.descripcion,
    required this.descripcionDetallada,
    required this.fecha,
    required this.monto,
    required this.numero,
    required this.ruc,
    required this.signoMovimiento,
    required this.simboloMoneda,
    required this.canalOrigen,
    required this.reversado,
  });

  factory MovimientoCuentaAhorro.fromJson(Map<String, dynamic> json) =>
      MovimientoCuentaAhorro(
        descripcion: json["Descripcion"],
        descripcionDetallada:
            json["DescripcionDetallada"] ?? json["Descripcion"],
        fecha: DateTime.parse(json["Fecha"]),
        monto: json["Monto"]?.toDouble(),
        numero: json["Numero"],
        ruc: json["Ruc"],
        signoMovimiento: json["SignoMovimiento"],
        simboloMoneda: json["SimboloMoneda"],
        canalOrigen: json["CanalOrigen"],
        reversado: json["Reversado"],
      );

  Map<String, dynamic> toJson() => {
        "Descripcion": descripcion,
        "DescripcionDetallada": descripcionDetallada,
        "Fecha": fecha.toIso8601String(),
        "Monto": monto,
        "Numero": numero,
        "Ruc": ruc,
        "SignoMovimiento": signoMovimiento,
        "SimboloMoneda": simboloMoneda,
        "CanalOrigen": canalOrigen,
        "Reversado": reversado,
      };
}
