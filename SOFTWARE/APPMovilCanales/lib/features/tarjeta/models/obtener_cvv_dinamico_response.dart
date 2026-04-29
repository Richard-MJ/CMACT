class ObtenerCvvDinamicoResponse {
  final String numeroTarjeta;
  final DateTime fechaVencimientoTarjeta;
  final String cvvDinamico;
  final DateTime fechaGeneracionCvv;
  final DateTime fechaVencimientoCvv;

  ObtenerCvvDinamicoResponse({
    required this.numeroTarjeta,
    required this.fechaVencimientoTarjeta,
    required this.cvvDinamico,
    required this.fechaGeneracionCvv,
    required this.fechaVencimientoCvv,
  });

  factory ObtenerCvvDinamicoResponse.fromJson(Map<String, dynamic> json) =>
      ObtenerCvvDinamicoResponse(
        numeroTarjeta: json["NumeroTarjeta"],
        fechaVencimientoTarjeta:
            DateTime.parse(json["FechaVencimientoTarjeta"]),
        cvvDinamico: json["CvvDinamico"],
        fechaGeneracionCvv: DateTime.parse(json["FechaGeneracionCvv"]),
        fechaVencimientoCvv: DateTime.parse(json["FechaVencimientoCvv"]),
      );

  Map<String, dynamic> toJson() => {
        "NumeroTarjeta": numeroTarjeta,
        "FechaVencimientoTarjeta": fechaVencimientoTarjeta.toIso8601String(),
        "CvvDinamico": cvvDinamico,
        "FechaGeneracionCvv": fechaGeneracionCvv.toIso8601String(),
        "FechaVencimientoCvv": fechaVencimientoCvv.toIso8601String(),
      };
}
