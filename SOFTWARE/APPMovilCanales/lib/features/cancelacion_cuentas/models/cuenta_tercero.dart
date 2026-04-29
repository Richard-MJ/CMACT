class CuentaTercero {
  final String codigoSistema;
  final String descripcion;
  final String codigoMoneda;
  final String simboloMoneda;
  final double saldoDisponible;
  final double saldoContable;
  final String descripcionMonto;
  final String descripcionEstado;
  final String descripcionComercialCorta;
  final double tea;
  final String nombreMoneda;
  final String descripcionAgencia;
  final String nombreCliente;
  final double montoDeposito;
  final double montoCuota;
  final int numeroTotalCuotas;
  final int numeroCuotasPagadas;
  final DateTime fechaCuotaVigente;
  final DateTime fechaUltimaRenovacion;

  CuentaTercero({
    required this.codigoSistema,
    required this.descripcion,
    required this.codigoMoneda,
    required this.simboloMoneda,
    required this.saldoDisponible,
    required this.saldoContable,
    required this.descripcionMonto,
    required this.descripcionEstado,
    required this.descripcionComercialCorta,
    required this.tea,
    required this.nombreMoneda,
    required this.descripcionAgencia,
    required this.nombreCliente,
    required this.montoDeposito,
    required this.montoCuota,
    required this.numeroTotalCuotas,
    required this.numeroCuotasPagadas,
    required this.fechaCuotaVigente,
    required this.fechaUltimaRenovacion,
  });

  factory CuentaTercero.fromJson(Map<String, dynamic> json) => CuentaTercero(
        codigoSistema: json["CodigoSistema"],
        descripcion: json["Descripcion"],
        codigoMoneda: json["CodigoMoneda"],
        simboloMoneda: json["SimboloMoneda"],
        saldoDisponible: json["SaldoDisponible"]?.toDouble(),
        saldoContable: json["SaldoContable"]?.toDouble(),
        descripcionMonto: json["DescripcionMonto"],
        descripcionEstado: json["DescripcionEstado"],
        descripcionComercialCorta: json["DescripcionComercialCorta"],
        tea: json["TEA"]?.toDouble(),
        nombreMoneda: json["NombreMoneda"],
        descripcionAgencia: json["DescripcionAgencia"],
        nombreCliente: json["NombreCliente"],
        montoDeposito: json["MontoDeposito"]?.toDouble(),
        montoCuota: json["MontoCuota"]?.toDouble(),
        numeroTotalCuotas: json["NumeroTotalCuotas"],
        numeroCuotasPagadas: json["NumeroCuotasPagadas"],
        fechaCuotaVigente: DateTime.parse(json["FechaCuotaVigente"]),
        fechaUltimaRenovacion: DateTime.parse(json["FechaUltimaRenovacion"]),
      );

  Map<String, dynamic> toJson() => {
        "CodigoSistema": codigoSistema,
        "Descripcion": descripcion,
        "CodigoMoneda": codigoMoneda,
        "SimboloMoneda": simboloMoneda,
        "SaldoDisponible": saldoDisponible,
        "SaldoContable": saldoContable,
        "DescripcionMonto": descripcionMonto,
        "DescripcionEstado": descripcionEstado,
        "DescripcionComercialCorta": descripcionComercialCorta,
        "TEA": tea,
        "NombreMoneda": nombreMoneda,
        "DescripcionAgencia": descripcionAgencia,
        "NombreCliente": nombreCliente,
        "MontoDeposito": montoDeposito,
        "MontoCuota": montoCuota,
        "NumeroTotalCuotas": numeroTotalCuotas,
        "NumeroCuotasPagadas": numeroCuotasPagadas,
        "FechaCuotaVigente": fechaCuotaVigente.toIso8601String(),
        "FechaUltimaRenovacion": fechaUltimaRenovacion.toIso8601String(),
      };
}
