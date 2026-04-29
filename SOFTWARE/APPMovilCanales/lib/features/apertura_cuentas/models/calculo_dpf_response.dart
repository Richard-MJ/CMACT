class CalculoDpfResponse {
  final Datos datos;

  CalculoDpfResponse({
    required this.datos,
  });

  factory CalculoDpfResponse.fromJson(Map<String, dynamic> json) =>
      CalculoDpfResponse(
        datos: Datos.fromJson(json["datos"]),
      );

  Map<String, dynamic> toJson() => {
        "datos": datos.toJson(),
      };
}

class Datos {
  final String tasaNominal;
  final String fechaApertura;
  final String fechaVencimiento;
  final String tasaEfectivaAnual;
  final String totalIntereses;
  final String simboloMoneda;
  final String montoApertura;
  final String montoItf;

  Datos({
    required this.tasaNominal,
    required this.fechaApertura,
    required this.fechaVencimiento,
    required this.tasaEfectivaAnual,
    required this.totalIntereses,
    required this.simboloMoneda,
    required this.montoApertura,
    required this.montoItf,
  });

  factory Datos.fromJson(Map<String, dynamic> json) => Datos(
        tasaNominal: json["TasaNominal"],
        fechaApertura: json["FechaApertura"],
        fechaVencimiento: json["FechaVencimiento"],
        tasaEfectivaAnual: json["TasaEfectivaAnual"],
        totalIntereses: json["TotalIntereses"],
        simboloMoneda: json["SimboloMoneda"],
        montoApertura: json["MontoApertura"],
        montoItf: json["MontoItf"],
      );

  Map<String, dynamic> toJson() => {
        "TasaNominal": tasaNominal,
        "FechaApertura": fechaApertura,
        "FechaVencimiento": fechaVencimiento,
        "TasaEfectivaAnual": tasaEfectivaAnual,
        "TotalIntereses": totalIntereses,
        "SimboloMoneda": simboloMoneda,
        "MontoApertura": montoApertura,
        "MontoItf": montoItf,
      };
}
