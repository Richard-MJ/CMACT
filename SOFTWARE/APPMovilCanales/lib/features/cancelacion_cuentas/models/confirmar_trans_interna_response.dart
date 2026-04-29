//para cancelaciones con trans propia o terceros

class ConfirmarTransInternaResponse {
  final int numeroOperacion;
  final String temaMensaje;
  final String tipoCuentaOrigen;
  final String numeroCuentaOrigen;
  final String nombreMoneda;
  final String nombreClienteBeneficiario;
  final String tipoCuentaDestino;
  final String numeroCuentaDestino;
  final double montoTransferencia;
  final double montoItf;
  final int idOperacionTts;
  final String codigoSistema;
  final DateTime fechaOperacion;
  final String simboloMoneda;
  final double montoTipoCambio;

  ConfirmarTransInternaResponse({
    required this.numeroOperacion,
    required this.temaMensaje,
    required this.tipoCuentaOrigen,
    required this.numeroCuentaOrigen,
    required this.nombreMoneda,
    required this.nombreClienteBeneficiario,
    required this.tipoCuentaDestino,
    required this.numeroCuentaDestino,
    required this.montoTransferencia,
    required this.montoItf,
    required this.idOperacionTts,
    required this.codigoSistema,
    required this.fechaOperacion,
    required this.simboloMoneda,
    required this.montoTipoCambio,
  });

  factory ConfirmarTransInternaResponse.fromJson(Map<String, dynamic> json) =>
      ConfirmarTransInternaResponse(
        numeroOperacion: json["NumeroOperacion"],
        temaMensaje: json["TemaMensaje"],
        tipoCuentaOrigen: json["TipoCuentaOrigen"],
        numeroCuentaOrigen: json["NumeroCuentaOrigen"],
        nombreMoneda: json["NombreMoneda"],
        nombreClienteBeneficiario: json["NombreClienteBeneficiario"],
        tipoCuentaDestino: json["TipoCuentaDestino"],
        numeroCuentaDestino: json["NumeroCuentaDestino"],
        montoTransferencia: json["MontoTransferencia"]?.toDouble(),
        montoItf: json["MontoItf"]?.toDouble(),
        idOperacionTts: json["IdOperacionTts"],
        codigoSistema: json["CodigoSistema"],
        fechaOperacion: DateTime.parse(json["FechaOperacion"]),
        simboloMoneda: json["SimboloMoneda"],
        montoTipoCambio: json["MontoTipoCambio"]?.toDouble(),
      );

  Map<String, dynamic> toJson() => {
        "NumeroOperacion": numeroOperacion,
        "TemaMensaje": temaMensaje,
        "TipoCuentaOrigen": tipoCuentaOrigen,
        "NumeroCuentaOrigen": numeroCuentaOrigen,
        "NombreMoneda": nombreMoneda,
        "NombreClienteBeneficiario": nombreClienteBeneficiario,
        "TipoCuentaDestino": tipoCuentaDestino,
        "NumeroCuentaDestino": numeroCuentaDestino,
        "MontoTransferencia": montoTransferencia,
        "MontoItf": montoItf,
        "IdOperacionTts": idOperacionTts,
        "CodigoSistema": codigoSistema,
        "FechaOperacion": fechaOperacion.toIso8601String(),
        "SimboloMoneda": simboloMoneda,
        "MontoTipoCambio": montoTipoCambio,
      };
}
