class ObtenerCobroServicioResponse {
  final String nombreCliente;
  final String simboloMoneda;
  final String codigoMoneda;
  final String descripcionMoneda;
  final double montoDeuda;
  final double moraDeuda;
  final double comisionDeuda;
  final double montoTotalDeuda;
  final String suministro;
  final String numeroRecibo;
  final String codigoEmpresa;
  final String codigoServicio;
  final DateTime fechaVencimiento;
  final String tipoServicio;
  final double montoDeudaMinima;

  ObtenerCobroServicioResponse({
    required this.nombreCliente,
    required this.simboloMoneda,
    required this.codigoMoneda,
    required this.descripcionMoneda,
    required this.montoDeuda,
    required this.moraDeuda,
    required this.comisionDeuda,
    required this.montoTotalDeuda,
    required this.suministro,
    required this.numeroRecibo,
    required this.codigoEmpresa,
    required this.codigoServicio,
    required this.fechaVencimiento,
    required this.tipoServicio,
    required this.montoDeudaMinima,
  });

  factory ObtenerCobroServicioResponse.fromJson(Map<String, dynamic> json) =>
      ObtenerCobroServicioResponse(
        nombreCliente: json["NombreCliente"],
        simboloMoneda: json["SimboloMoneda"],
        codigoMoneda: json["CodigoMoneda"],
        descripcionMoneda: json["DescripcionMoneda"],
        montoDeuda: json["MontoDeuda"]?.toDouble(),
        moraDeuda: json["MoraDeuda"]?.toDouble(),
        comisionDeuda: json["ComisionDeuda"]?.toDouble(),
        montoTotalDeuda: json["MontoTotalDeuda"]?.toDouble(),
        suministro: json["Suministro"],
        numeroRecibo: json["NumeroRecibo"],
        codigoEmpresa: json["CodigoEmpresa"],
        codigoServicio: json["CodigoServicio"],
        fechaVencimiento: DateTime.parse(json["FechaVencimiento"]),
        tipoServicio: json["TipoServicio"],
        montoDeudaMinima: json["MontoDeudaMinima"],
      );

  Map<String, dynamic> toJson() => {
        "NombreCliente": nombreCliente,
        "SimboloMoneda": simboloMoneda,
        "CodigoMoneda": codigoMoneda,
        "DescripcionMoneda": descripcionMoneda,
        "MontoDeuda": montoDeuda,
        "MoraDeuda": moraDeuda,
        "ComisionDeuda": comisionDeuda,
        "MontoTotalDeuda": montoTotalDeuda,
        "Suministro": suministro,
        "NumeroRecibo": numeroRecibo,
        "CodigoEmpresa": codigoEmpresa,
        "CodigoServicio": codigoServicio,
        "FechaVencimiento": fechaVencimiento.toIso8601String(),
        "TipoServicio": tipoServicio,
        "MontoDeudaMinima": montoDeudaMinima,
      };
}
