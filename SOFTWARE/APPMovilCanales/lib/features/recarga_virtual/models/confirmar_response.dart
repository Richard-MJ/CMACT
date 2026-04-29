class ConfirmarResponse {
  final String temaMensaje;
  final String transaccionRealizada;
  final String cuentaOrigen;
  final String numeroCelular;
  final double monto;
  final String operador;
  final String simboloMoneda;
  final DateTime fechaPago;
  final int idOperacionTts;
  final double montoTipoCambio;

  ConfirmarResponse({
    required this.temaMensaje,
    required this.transaccionRealizada,
    required this.cuentaOrigen,
    required this.numeroCelular,
    required this.monto,
    required this.operador,
    required this.simboloMoneda,
    required this.fechaPago,
    required this.idOperacionTts,
    required this.montoTipoCambio,
  });

  factory ConfirmarResponse.fromJson(Map<String, dynamic> json) =>
      ConfirmarResponse(
        temaMensaje: json["TemaMensaje"],
        transaccionRealizada: json["TransaccionRealizada"],
        cuentaOrigen: json["CuentaOrigen"],
        numeroCelular: json["NumeroCelular"],
        monto: json["Monto"]?.toDouble(),
        operador: json["Operador"],
        simboloMoneda: json["SimboloMoneda"],
        fechaPago: DateTime.parse(json["FechaPago"]),
        idOperacionTts: json["IdOperacionTts"],
        montoTipoCambio: json["MontoTipoCambio"]?.toDouble(),
      );

  Map<String, dynamic> toJson() => {
        "TemaMensaje": temaMensaje,
        "TransaccionRealizada": transaccionRealizada,
        "CuentaOrigen": cuentaOrigen,
        "NumeroCelular": numeroCelular,
        "Monto": monto,
        "Operador": operador,
        "SimboloMoneda": simboloMoneda,
        "FechaPago": fechaPago.toIso8601String(),
        "IdOperacionTts": idOperacionTts,
        "MontoTipoCambio": monto,
      };
}
