class ConfirmarAppResponse {
  final String temaMensaje;
  final int idOperacionTts;
  final String nombreClienteBeneficiado;
  final String tipoCuentaOrigen;
  final String numeroCuentaOrigen;
  final String tipoTransaccion;
  final int numeroCredito;
  final String nombreMonedaOperacion;
  final String simboloMonedaOperacion;
  final double montoOperacion;
  final DateTime fechaOperacion;
  final double montoAbonadoSinItf;
  final double montoTipoCambio;
  final String codigoAnalista;
  final double montoItfCuenta;
  final String numeroOperacion;

  ConfirmarAppResponse({
    required this.temaMensaje,
    required this.idOperacionTts,
    required this.nombreClienteBeneficiado,
    required this.tipoCuentaOrigen,
    required this.numeroCuentaOrigen,
    required this.tipoTransaccion,
    required this.numeroCredito,
    required this.nombreMonedaOperacion,
    required this.simboloMonedaOperacion,
    required this.montoOperacion,
    required this.fechaOperacion,
    required this.montoAbonadoSinItf,
    required this.montoTipoCambio,
    required this.codigoAnalista,
    required this.montoItfCuenta,
    required this.numeroOperacion,
  });

  factory ConfirmarAppResponse.fromJson(Map<String, dynamic> json) =>
      ConfirmarAppResponse(
        temaMensaje: json["TemaMensaje"],
        idOperacionTts: json["IdOperacionTts"],
        nombreClienteBeneficiado: json["NombreClienteBeneficiado"],
        tipoCuentaOrigen: json["TipoCuentaOrigen"],
        numeroCuentaOrigen: json["NumeroCuentaOrigen"],
        tipoTransaccion: json["TipoTransaccion"],
        numeroCredito: json["NumeroCredito"],
        nombreMonedaOperacion: json["NombreMonedaOperacion"],
        simboloMonedaOperacion: json["SimboloMonedaOperacion"],
        montoOperacion: json["MontoOperacion"]?.toDouble(),
        fechaOperacion: DateTime.parse(json["FechaOperacion"]),
        montoAbonadoSinItf: json["MontoAbonadoSinItf"]?.toDouble(),
        montoTipoCambio: json["MontoTipoCambio"]?.toDouble(),
        codigoAnalista: json["CodigoAnalista"],
        montoItfCuenta: json["MontoItfCuenta"]?.toDouble(),
        numeroOperacion: json["NumeroOperacion"],
      );

  Map<String, dynamic> toJson() => {
        "TemaMensaje": temaMensaje,
        "IdOperacionTts": idOperacionTts,
        "NombreClienteBeneficiado": nombreClienteBeneficiado,
        "TipoCuentaOrigen": tipoCuentaOrigen,
        "NumeroCuentaOrigen": numeroCuentaOrigen,
        "TipoTransaccion": tipoTransaccion,
        "NumeroCredito": numeroCredito,
        "NombreMonedaOperacion": nombreMonedaOperacion,
        "SimboloMonedaOperacion": simboloMonedaOperacion,
        "MontoOperacion": montoOperacion,
        "FechaOperacion": fechaOperacion.toIso8601String(),
        "MontoAbonadoSinItf": montoAbonadoSinItf,
        "MontoTipoCambio": montoTipoCambio,
        "CodigoAnalista": codigoAnalista,
        "MontoItfCuenta": montoItfCuenta,
        "NumeroOperacion": numeroOperacion,
      };
}
