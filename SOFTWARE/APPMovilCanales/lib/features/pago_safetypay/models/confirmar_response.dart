class ConfirmarResponse {
  final String temaMensaje;
  final String transaccionRealizada;
  final String nombreTitular;
  final String numeroCuentaOrigen;
  final String lugarCompra;
  final String codigoCompra;
  final String simboloMonedaPago;
  final double montoPago;
  final DateTime fechaPago;
  final double tipoCambio;
  final int numeroOperacion;
  final String simboloMonedaDebitada;
  final double montoDebitado;
  final int idOperacionTts;
  final double montoItf;

  ConfirmarResponse({
    required this.temaMensaje,
    required this.transaccionRealizada,
    required this.nombreTitular,
    required this.numeroCuentaOrigen,
    required this.lugarCompra,
    required this.codigoCompra,
    required this.simboloMonedaPago,
    required this.montoPago,
    required this.fechaPago,
    required this.tipoCambio,
    required this.numeroOperacion,
    required this.simboloMonedaDebitada,
    required this.montoDebitado,
    required this.idOperacionTts,
    required this.montoItf,
  });

  factory ConfirmarResponse.fromJson(Map<String, dynamic> json) =>
      ConfirmarResponse(
        temaMensaje: json["TemaMensaje"],
        transaccionRealizada: json["TransaccionRealizada"],
        nombreTitular: json["NombreTitular"],
        numeroCuentaOrigen: json["NumeroCuentaOrigen"],
        lugarCompra: json["LugarCompra"],
        codigoCompra: json["CodigoCompra"],
        simboloMonedaPago: json["SimboloMonedaPago"],
        montoPago: json["MontoPago"]?.toDouble(),
        fechaPago: DateTime.parse(json["FechaPago"]),
        tipoCambio: json["TipoCambio"]?.toDouble(),
        numeroOperacion: json["NumeroOperacion"],
        simboloMonedaDebitada: json["SimboloMonedaDebitada"],
        montoDebitado: json["MontoDebitado"]?.toDouble(),
        idOperacionTts: json["IdOperacionTts"],
        montoItf: json["MontoItf"]?.toDouble(),
      );

  Map<String, dynamic> toJson() => {
        "TemaMensaje": temaMensaje,
        "TransaccionRealizada": transaccionRealizada,
        "NombreTitular": nombreTitular,
        "NumeroCuentaOrigen": numeroCuentaOrigen,
        "LugarCompra": lugarCompra,
        "CodigoCompra": codigoCompra,
        "SimboloMonedaPago": simboloMonedaPago,
        "MontoPago": montoPago,
        "FechaPago": fechaPago.toIso8601String(),
        "TipoCambio": tipoCambio,
        "NumeroOperacion": numeroOperacion,
        "SimboloMonedaDebitada": simboloMonedaDebitada,
        "MontoDebitado": montoDebitado,
        "IdOperacionTts": idOperacionTts,
        "MontoItf": montoItf,
      };
}
