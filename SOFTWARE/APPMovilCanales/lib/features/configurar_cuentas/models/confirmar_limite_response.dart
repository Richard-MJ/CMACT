class ConfirmarLimiteResponse {
  final String descripcionOperacion;
  final String numeroCuentaOrigen;
  final String nombreMoneda;
  final String simboloMoneda;
  final String nombreProducto;
  final DateTime fechaOperacion;
  final double valorLimite;

  ConfirmarLimiteResponse({
    required this.descripcionOperacion,
    required this.numeroCuentaOrigen,
    required this.nombreMoneda,
    required this.simboloMoneda,
    required this.nombreProducto,
    required this.fechaOperacion,
    required this.valorLimite,
  });

  factory ConfirmarLimiteResponse.fromJson(Map<String, dynamic> json) =>
      ConfirmarLimiteResponse(
        descripcionOperacion: json["DescripcionOperacion"],
        numeroCuentaOrigen: json["NumeroCuentaOrigen"],
        nombreMoneda: json["NombreMoneda"],
        simboloMoneda: json["SimboloMoneda"],
        nombreProducto: json["NombreProducto"],
        fechaOperacion: DateTime.parse(json["FechaOperacion"]),
        valorLimite: json["ValorLimite"]?.toDouble(),
      );

  Map<String, dynamic> toJson() => {
        "DescripcionOperacion": descripcionOperacion,
        "NumeroCuentaOrigen": numeroCuentaOrigen,
        "NombreMoneda": nombreMoneda,
        "SimboloMoneda": simboloMoneda,
        "NombreProducto": nombreProducto,
        "FechaOperacion": fechaOperacion.toIso8601String(),
        "ValorLimite": valorLimite,
      };
}
