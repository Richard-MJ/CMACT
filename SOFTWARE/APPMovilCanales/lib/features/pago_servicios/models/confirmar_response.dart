class ConfirmarResponse {
  final String servicio;
  final String temaMensaje;
  final int idOperacionTts;
  final int numeroOperacion;
  final String numeroCuentaOrigen;
  final String nombreClienteOrigen;
  final String nombreDeudor;
  final DateTime fechaOperacion;
  final String simboloMonedaOrigen;
  final double montoDebitado;
  final double montoItf;
  final double montoTipoCambio;
  final String simboloMonedaDeuda;
  final double montoPagado;
  final int tipoOperacion;

  ConfirmarResponse({
    required this.servicio,
    required this.temaMensaje,
    required this.idOperacionTts,
    required this.numeroOperacion,
    required this.numeroCuentaOrigen,
    required this.nombreClienteOrigen,
    required this.nombreDeudor,
    required this.fechaOperacion,
    required this.simboloMonedaOrigen,
    required this.montoDebitado,
    required this.montoItf,
    required this.montoTipoCambio,
    required this.simboloMonedaDeuda,
    required this.montoPagado,
    required this.tipoOperacion,
  });

  factory ConfirmarResponse.fromJson(Map<String, dynamic> json) =>
      ConfirmarResponse(
        servicio: json["Servicio"],
        temaMensaje: json["TemaMensaje"],
        idOperacionTts: json["IdOperacionTts"],
        numeroOperacion: json["NumeroOperacion"],
        numeroCuentaOrigen: json["NumeroCuentaOrigen"],
        nombreClienteOrigen: json["NombreClienteOrigen"],
        nombreDeudor: json["NombreDeudor"],
        fechaOperacion: DateTime.parse(json["FechaOperacion"]),
        simboloMonedaOrigen: json["SimboloMonedaOrigen"],
        montoDebitado: json["MontoDebitado"]?.toDouble(),
        montoItf: json["MontoItf"]?.toDouble(),
        montoTipoCambio: json["MontoTipoCambio"]?.toDouble(),
        simboloMonedaDeuda: json["SimboloMonedaDeuda"],
        montoPagado: json["MontoPagado"]?.toDouble(),
        tipoOperacion: json["TipoOperacion"],
      );

  Map<String, dynamic> toJson() => {
        "Servicio": servicio,
        "TemaMensaje": temaMensaje,
        "IdOperacionTts": idOperacionTts,
        "NumeroOperacion": numeroOperacion,
        "NumeroCuentaOrigen": numeroCuentaOrigen,
        "NombreClienteOrigen": nombreClienteOrigen,
        "NombreDeudor": nombreDeudor,
        "FechaOperacion": fechaOperacion.toIso8601String(),
        "SimboloMonedaOrigen": simboloMonedaOrigen,
        "MontoDebitado": montoDebitado,
        "MontoItf": montoItf,
        "MontoTipoCambio": montoTipoCambio,
        "SimboloMonedaDeuda": simboloMonedaDeuda,
        "MontoPagado": montoPagado,
        "TipoOperacion": tipoOperacion,
      };
}
