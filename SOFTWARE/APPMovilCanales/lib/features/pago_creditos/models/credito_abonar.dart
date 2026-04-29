class CreditoAbonar {
  final int numeroCredito;
  final DateTime fechaCuotaVigente;
  final String codigoMoneda;
  final String simboloMoneda;
  final double montoSaldoCapital;
  final double montoTotalPago;
  final int cuotasTotales;
  final int cuotaActual;
  final String nombreMoneda;
  final String descripcionTipoCredito;
  final double montoSaldoCancelacion;
  final DateTime fechaVencimiento;
  final String? alias;
  final String? nombreCliente;
  final double montoAnticipado;
  final double montoMinimoAnticipo;
  final double montoCuotaPactada;
  final String descripcionSubProducto;

  CreditoAbonar({
    required this.numeroCredito,
    required this.fechaCuotaVigente,
    required this.codigoMoneda,
    required this.simboloMoneda,
    required this.montoSaldoCapital,
    required this.montoTotalPago,
    required this.cuotasTotales,
    required this.cuotaActual,
    required this.nombreMoneda,
    required this.descripcionTipoCredito,
    required this.montoSaldoCancelacion,
    required this.fechaVencimiento,
    this.alias,
    this.nombreCliente,
    required this.montoAnticipado,
    required this.montoMinimoAnticipo,
    required this.montoCuotaPactada,
    required this.descripcionSubProducto,
  });

  factory CreditoAbonar.fromJson(Map<String, dynamic> json) => CreditoAbonar(
        numeroCredito: json["NumeroCredito"],
        fechaCuotaVigente: DateTime.parse(json["FechaCuotaVigente"]),
        codigoMoneda: json["CodigoMoneda"],
        simboloMoneda: json["SimboloMoneda"],
        montoSaldoCapital: json["MontoSaldoCapital"]?.toDouble(),
        montoTotalPago: json["MontoTotalPago"]?.toDouble(),
        cuotasTotales: json["CuotasTotales"],
        cuotaActual: json["CuotaActual"],
        nombreMoneda: json["NombreMoneda"],
        descripcionTipoCredito: json["DescripcionTipoCredito"],
        montoSaldoCancelacion: json["MontoSaldoCancelacion"]?.toDouble(),
        fechaVencimiento: DateTime.parse(json["FechaVencimiento"]),
        alias: json["Alias"],
        nombreCliente: json["NombreCliente"],
        montoAnticipado: json["MontoAnticipado"]?.toDouble(),
        montoMinimoAnticipo: json["MontoMinimoAnticipo"]?.toDouble(),
        montoCuotaPactada: json["MontoCuotaPactada"]?.toDouble(),
        descripcionSubProducto: json["DescripcionSubProducto"],
      );

  Map<String, dynamic> toJson() => {
        "NumeroCredito": numeroCredito,
        "FechaCuotaVigente": fechaCuotaVigente.toIso8601String(),
        "CodigoMoneda": codigoMoneda,
        "SimboloMoneda": simboloMoneda,
        "MontoSaldoCapital": montoSaldoCapital,
        "MontoTotalPago": montoTotalPago,
        "CuotasTotales": cuotasTotales,
        "CuotaActual": cuotaActual,
        "NombreMoneda": nombreMoneda,
        "DescripcionTipoCredito": descripcionTipoCredito,
        "MontoSaldoCancelacion": montoSaldoCancelacion,
        "FechaVencimiento": fechaVencimiento.toIso8601String(),
        "Alias": alias,
        "NombreCliente": nombreCliente,
        "MontoAnticipado": montoAnticipado,
        "MontoMinimoAnticipo": montoMinimoAnticipo,
        "MontoCuotaPactada": montoCuotaPactada,
        "DescripcionSubProducto": descripcionSubProducto,
      };
}
