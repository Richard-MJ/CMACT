class ConfirmarSaldoCero {
  final String nombreClienteOrigen;
  final String numeroCuentaOrigen;
  final String simboloMonedaOrigen;
  final double montoOrigen;
  final DateTime fechaOperacion;
  final int idOperacionTts;

  ConfirmarSaldoCero({
    required this.nombreClienteOrigen,
    required this.numeroCuentaOrigen,
    required this.simboloMonedaOrigen,
    required this.montoOrigen,
    required this.fechaOperacion,
    required this.idOperacionTts,
  });

  factory ConfirmarSaldoCero.fromJson(Map<String, dynamic> json) =>
      ConfirmarSaldoCero(
        nombreClienteOrigen: json["NombreClienteOrigen"],
        numeroCuentaOrigen: json["NumeroCuentaOrigen"],
        simboloMonedaOrigen: json["SimboloMonedaOrigen"],
        montoOrigen: json["MontoOrigen"]?.toDouble(),
        fechaOperacion: DateTime.parse(json["FechaOperacion"]),
        idOperacionTts: json["IdOperacionTts"],
      );

  Map<String, dynamic> toJson() => {
        "NombreClienteOrigen": nombreClienteOrigen,
        "NumeroCuentaOrigen": numeroCuentaOrigen,
        "SimboloMonedaOrigen": simboloMonedaOrigen,
        "MontoOrigen": montoOrigen,
        "FechaOperacion": fechaOperacion.toIso8601String(),
        "IdOperacionTts": idOperacionTts,
      };
}
