class DetalleCuentaAhorro {
  final String alias;
  final String codigoAgencia;
  final String codigoMoneda;
  final String descripcion;
  final String descripcionEstado;
  final String descripcionMonto;
  final String identificador;
  final String identificadorCci;
  final double saldoContable;
  final double saldoDisponible;
  final String simboloMoneda;
  final String descripcionComercialCorta;
  final DateTime fechaApertura;
  final double tea;
  final String nombreMoneda;
  final String descripcionAgencia;
  final dynamic fechaVencimiento;
  final String codigoProducto;
  final dynamic nombreCliente;
  final dynamic codigoCliente;
  final dynamic indicadorPlanAhorro;

  DetalleCuentaAhorro({
    required this.alias,
    required this.codigoAgencia,
    required this.codigoMoneda,
    required this.descripcion,
    required this.descripcionEstado,
    required this.descripcionMonto,
    required this.identificador,
    required this.identificadorCci,
    required this.saldoContable,
    required this.saldoDisponible,
    required this.simboloMoneda,
    required this.descripcionComercialCorta,
    required this.fechaApertura,
    required this.tea,
    required this.nombreMoneda,
    required this.descripcionAgencia,
    required this.fechaVencimiento,
    required this.codigoProducto,
    required this.nombreCliente,
    required this.codigoCliente,
    required this.indicadorPlanAhorro,
  });

  factory DetalleCuentaAhorro.fromJson(Map<String, dynamic> json) =>
      DetalleCuentaAhorro(
        alias: json["Alias"],
        codigoAgencia: json["CodigoAgencia"],
        codigoMoneda: json["CodigoMoneda"],
        descripcion: json["Descripcion"],
        descripcionEstado: json["DescripcionEstado"],
        descripcionMonto: json["DescripcionMonto"],
        identificador: json["Identificador"],
        identificadorCci: json["IdentificadorCci"],
        saldoContable: json["SaldoContable"]?.toDouble(),
        saldoDisponible: json["SaldoDisponible"]?.toDouble(),
        simboloMoneda: json["SimboloMoneda"],
        descripcionComercialCorta: json["DescripcionComercialCorta"],
        fechaApertura: DateTime.parse(json["FechaApertura"]),
        tea: json["TEA"]?.toDouble(),
        nombreMoneda: json["NombreMoneda"],
        descripcionAgencia: json["DescripcionAgencia"],
        fechaVencimiento: json["FechaVencimiento"],
        codigoProducto: json["CodigoProducto"],
        nombreCliente: json["NombreCliente"],
        codigoCliente: json["CodigoCliente"],
        indicadorPlanAhorro: json["IndicadorPlanAhorro"],
      );

  Map<String, dynamic> toJson() => {
        "Alias": alias,
        "CodigoAgencia": codigoAgencia,
        "CodigoMoneda": codigoMoneda,
        "Descripcion": descripcion,
        "DescripcionEstado": descripcionEstado,
        "DescripcionMonto": descripcionMonto,
        "Identificador": identificador,
        "IdentificadorCci": identificadorCci,
        "SaldoContable": saldoContable,
        "SaldoDisponible": saldoDisponible,
        "SimboloMoneda": simboloMoneda,
        "DescripcionComercialCorta": descripcionComercialCorta,
        "FechaApertura": fechaApertura.toIso8601String(),
        "TEA": tea,
        "NombreMoneda": nombreMoneda,
        "DescripcionAgencia": descripcionAgencia,
        "FechaVencimiento": fechaVencimiento,
        "CodigoProducto": codigoProducto,
        "NombreCliente": nombreCliente,
        "CodigoCliente": codigoCliente,
        "IndicadorPlanAhorro": indicadorPlanAhorro,
      };
}
