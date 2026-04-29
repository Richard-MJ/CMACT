class Credito {
  final String alias;
  final String descripcionEstado;
  final double montoCuotaPactada;
  final double montoDesembolsado;
  final String nombreAnalista;
  final int numeroCredito;
  final double saldoPendiente;
  final String simboloMoneda;
  final String descripcionTipoCredito;
  final double porcentajeTasaInteres;
  final String? telefonoAnalista;
  final String nombreMoneda;
  final String modalidad;
  final DateTime fechaDesembolso;
  final DateTime fechaCuotaVigente;
  final int cuotasTotales;
  final int cuotaActual;
  final String descripcionSubProducto;

  Credito({
    required this.alias,
    required this.descripcionEstado,
    required this.montoCuotaPactada,
    required this.montoDesembolsado,
    required this.nombreAnalista,
    required this.numeroCredito,
    required this.saldoPendiente,
    required this.simboloMoneda,
    required this.descripcionTipoCredito,
    required this.porcentajeTasaInteres,
    required this.telefonoAnalista,
    required this.nombreMoneda,
    required this.modalidad,
    required this.fechaDesembolso,
    required this.fechaCuotaVigente,
    required this.cuotasTotales,
    required this.cuotaActual,
    required this.descripcionSubProducto,
  });

  factory Credito.fromJson(Map<String, dynamic> json) => Credito(
        alias: json["Alias"],
        descripcionEstado: json["DescripcionEstado"],
        montoCuotaPactada: json["MontoCuotaPactada"]?.toDouble(),
        montoDesembolsado: json["MontoDesembolsado"]?.toDouble(),
        nombreAnalista: json["NombreAnalista"],
        numeroCredito: json["NumeroCredito"],
        saldoPendiente: json["SaldoPendiente"]?.toDouble(),
        simboloMoneda: json["SimboloMoneda"],
        descripcionTipoCredito: json["DescripcionTipoCredito"],
        porcentajeTasaInteres: json["PorcentajeTasaInteres"]?.toDouble(),
        telefonoAnalista: json["TelefonoAnalista"],
        nombreMoneda: json["NombreMoneda"],
        modalidad: json["Modalidad"],
        fechaDesembolso: DateTime.parse(json["FechaDesembolso"]),
        fechaCuotaVigente: DateTime.parse(json["FechaCuotaVigente"]),
        cuotasTotales: json["CuotasTotales"],
        cuotaActual: json["CuotaActual"],
        descripcionSubProducto: json["DescripcionSubProducto"],
      );

  Map<String, dynamic> toJson() => {
        "Alias": alias,
        "DescripcionEstado": descripcionEstado,
        "MontoCuotaPactada": montoCuotaPactada,
        "MontoDesembolsado": montoDesembolsado,
        "NombreAnalista": nombreAnalista,
        "NumeroCredito": numeroCredito,
        "SaldoPendiente": saldoPendiente,
        "SimboloMoneda": simboloMoneda,
        "DescripcionTipoCredito": descripcionTipoCredito,
        "PorcentajeTasaInteres": porcentajeTasaInteres,
        "TelefonoAnalista": telefonoAnalista,
        "NombreMoneda": nombreMoneda,
        "Modalidad": modalidad,
        "FechaDesembolso": fechaDesembolso.toIso8601String(),
        "FechaCuotaVigente": fechaCuotaVigente.toIso8601String(),
        "CuotasTotales": cuotasTotales,
        "CuotaActual": cuotaActual,
        "DescripcionSubProducto": descripcionSubProducto,
      };
}
