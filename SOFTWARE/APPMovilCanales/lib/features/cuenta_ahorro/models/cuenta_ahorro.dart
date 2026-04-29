class CuentaAhorro {
  final String codigoSistema;
  final String alias;
  final String identificador;
  final String identificadorCci;
  final String codigoAgencia;
  final String descripcion;
  final String codigoMoneda;
  final String simboloMoneda;
  final double saldoDisponible;
  final double saldoContable;
  final String descripcionMonto;
  final String descripcionEstado;
  final String descripcionComercialCorta;
  final DateTime fechaApertura;
  final DateTime? fechaVencimiento;
  final double tea;
  final String nombreMoneda;
  final String descripcionAgencia;
  final String codigoProducto;
  final String? nombreCliente;
  final dynamic codigoTipo;
  final double montoDeposito;
  final double montoCuota;
  final int numeroTotalCuotas;
  final int numeroCuotasPagadas;
  final DateTime fechaCuotaVigente;
  final DateTime fechaUltimaRenovacion;

  CuentaAhorro({
    required this.codigoSistema,
    required this.alias,
    required this.identificador,
    required this.identificadorCci,
    required this.codigoAgencia,
    required this.descripcion,
    required this.codigoMoneda,
    required this.simboloMoneda,
    required this.saldoDisponible,
    required this.saldoContable,
    required this.descripcionMonto,
    required this.descripcionEstado,
    required this.descripcionComercialCorta,
    required this.fechaApertura,
    required this.fechaVencimiento,
    required this.tea,
    required this.nombreMoneda,
    required this.descripcionAgencia,
    required this.codigoProducto,
    required this.nombreCliente,
    required this.codigoTipo,
    required this.montoDeposito,
    required this.montoCuota,
    required this.numeroTotalCuotas,
    required this.numeroCuotasPagadas,
    required this.fechaCuotaVigente,
    required this.fechaUltimaRenovacion,
  });

  factory CuentaAhorro.fromJson(Map<String, dynamic> json) => CuentaAhorro(
        codigoSistema: json["CodigoSistema"],
        alias: json["Alias"],
        identificador: json["Identificador"],
        identificadorCci: json["IdentificadorCci"],
        codigoAgencia: json["CodigoAgencia"],
        descripcion: json["Descripcion"],
        codigoMoneda: json["CodigoMoneda"],
        simboloMoneda: json["SimboloMoneda"],
        saldoDisponible: json["SaldoDisponible"]?.toDouble(),
        saldoContable: json["SaldoContable"]?.toDouble(),
        descripcionMonto: json["DescripcionMonto"],
        descripcionEstado: json["DescripcionEstado"],
        descripcionComercialCorta: json["DescripcionComercialCorta"],
        fechaApertura: DateTime.parse(json["FechaApertura"]),
        fechaVencimiento: json["FechaVencimiento"] == null
            ? null
            : DateTime.parse(json["FechaVencimiento"]),
        tea: json["TEA"]?.toDouble(),
        nombreMoneda: json["NombreMoneda"],
        descripcionAgencia: json["DescripcionAgencia"],
        codigoProducto: json["CodigoProducto"],
        nombreCliente: json["NombreCliente"],
        codigoTipo: json["CodigoTipo"],
        montoDeposito: json["MontoDeposito"]?.toDouble(),
        montoCuota: json["MontoCuota"]?.toDouble(),
        numeroTotalCuotas: json["NumeroTotalCuotas"],
        numeroCuotasPagadas: json["NumeroCuotasPagadas"],
        fechaCuotaVigente: DateTime.parse(json["FechaCuotaVigente"]),
        fechaUltimaRenovacion: DateTime.parse(json["FechaUltimaRenovacion"]),
      );

  Map<String, dynamic> toJson() => {
        "CodigoSistema": codigoSistema,
        "Alias": alias,
        "Identificador": identificador,
        "IdentificadorCci": identificadorCci,
        "CodigoAgencia": codigoAgencia,
        "Descripcion": descripcion,
        "CodigoMoneda": codigoMoneda,
        "SimboloMoneda": simboloMoneda,
        "SaldoDisponible": saldoDisponible,
        "SaldoContable": saldoContable,
        "DescripcionMonto": descripcionMonto,
        "DescripcionEstado": descripcionEstado,
        "DescripcionComercialCorta": descripcionComercialCorta,
        "FechaApertura": fechaApertura.toIso8601String(),
        "FechaVencimiento": fechaVencimiento?.toIso8601String(),
        "TEA": tea,
        "NombreMoneda": nombreMoneda,
        "DescripcionAgencia": descripcionAgencia,
        "CodigoProducto": codigoProducto,
        "NombreCliente": nombreCliente,
        "CodigoTipo": codigoTipo,
        "MontoDeposito": montoDeposito,
        "MontoCuota": montoCuota,
        "NumeroTotalCuotas": numeroTotalCuotas,
        "NumeroCuotasPagadas": numeroCuotasPagadas,
        "FechaCuotaVigente": fechaCuotaVigente.toIso8601String(),
        "FechaUltimaRenovacion": fechaUltimaRenovacion.toIso8601String(),
      };
}
