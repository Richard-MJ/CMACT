class ConfirmarResponse {
  final int numeroOperacion;
  final String temaMensaje;
  final int idOperacionTts;
  final String tipoCuentaOrigen;
  final String numeroCuentaOrigen;
  final String nombreMonedaCuentaOrigen;
  final String nombreBeneficiario;
  final String simboloMonedaTransferencia;
  final double montoTransferencia;
  final DateTime fechaOperacion;
  final double montoComision;
  final double montoItf;
  final String numeroTarjeta;

  ConfirmarResponse({
    required this.numeroOperacion,
    required this.temaMensaje,
    required this.idOperacionTts,
    required this.tipoCuentaOrigen,
    required this.numeroCuentaOrigen,
    required this.nombreMonedaCuentaOrigen,
    required this.nombreBeneficiario,
    required this.simboloMonedaTransferencia,
    required this.montoTransferencia,
    required this.fechaOperacion,
    required this.montoComision,
    required this.montoItf,
    required this.numeroTarjeta,
  });

  factory ConfirmarResponse.fromJson(Map<String, dynamic> json) =>
      ConfirmarResponse(
        numeroOperacion: json["NumeroOperacion"],
        temaMensaje: json["TemaMensaje"],
        idOperacionTts: json["IdOperacionTts"],
        tipoCuentaOrigen: json["TipoCuentaOrigen"],
        numeroCuentaOrigen: json["NumeroCuentaOrigen"],
        nombreMonedaCuentaOrigen: json["NombreMonedaCuentaOrigen"],
        nombreBeneficiario: json["NombreBeneficiario"],
        simboloMonedaTransferencia: json["SimboloMonedaTransferencia"],
        montoTransferencia: json["MontoTransferencia"]?.toDouble(),
        fechaOperacion: DateTime.parse(json["FechaOperacion"]),
        montoComision: json["MontoComision"]?.toDouble(),
        montoItf: json["MontoItf"]?.toDouble(),
        numeroTarjeta: json["NumeroTarjeta"],
      );

  Map<String, dynamic> toJson() => {
        "NumeroOperacion": numeroOperacion,
        "TemaMensaje": temaMensaje,
        "IdOperacionTts": idOperacionTts,
        "TipoCuentaOrigen": tipoCuentaOrigen,
        "NumeroCuentaOrigen": numeroCuentaOrigen,
        "NombreMonedaCuentaOrigen": nombreMonedaCuentaOrigen,
        "NombreBeneficiario": nombreBeneficiario,
        "SimboloMonedaTransferencia": simboloMonedaTransferencia,
        "MontoTransferencia": montoTransferencia,
        "FechaOperacion": fechaOperacion.toIso8601String(),
        "MontoComision": montoComision,
        "MontoItf": montoItf,
        "NumeroTarjeta": numeroTarjeta,
      };
}
