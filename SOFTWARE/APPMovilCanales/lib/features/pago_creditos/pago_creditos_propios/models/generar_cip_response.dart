class GenerarCipResponse {
  final int codigoCip;
  final String moneda;
  final double montoOperacion;
  final String numeroCredito;
  final DateTime fechaExpiracion;
  final String ruta;

  GenerarCipResponse({
    required this.codigoCip,
    required this.moneda,
    required this.montoOperacion,
    required this.numeroCredito,
    required this.fechaExpiracion,
    required this.ruta,
  });

  factory GenerarCipResponse.fromJson(Map<String, dynamic> json) =>
      GenerarCipResponse(
        codigoCip: json["CodigoCip"],
        moneda: json["Moneda"],
        montoOperacion: json["MontoOperacion"]?.toDouble(),
        numeroCredito: json["NumeroCredito"],
        fechaExpiracion: DateTime.parse(json["FechaExpiracion"]),
        ruta: json["Ruta"],
      );

  Map<String, dynamic> toJson() => {
        "CodigoCip": codigoCip,
        "Moneda": moneda,
        "MontoOperacion": montoOperacion,
        "NumeroCredito": numeroCredito,
        "FechaExpiracion": fechaExpiracion.toIso8601String(),
        "Ruta": ruta,
      };
}
