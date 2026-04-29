class ConfirmarResponse {
  final int numeroOperacion;
  final String transaccionRealizada;
  final String cliente;
  final String cuentaSueldo;
  final String simboloMoneda;
  final double montoRecibido;
  final double montoComision;
  final double totalPago;
  final DateTime fechaOperacion;
  final String temaMensaje;

  ConfirmarResponse({
    required this.numeroOperacion,
    required this.transaccionRealizada,
    required this.cliente,
    required this.cuentaSueldo,
    required this.simboloMoneda,
    required this.montoRecibido,
    required this.montoComision,
    required this.totalPago,
    required this.fechaOperacion,
    required this.temaMensaje,
  });

  factory ConfirmarResponse.fromJson(Map<String, dynamic> json) =>
      ConfirmarResponse(
        numeroOperacion: json["NumeroOperacion"],
        transaccionRealizada: json["TransaccionRealizada"],
        cliente: json["Cliente"],
        cuentaSueldo: json["CuentaSueldo"],
        simboloMoneda: json["SimboloMoneda"],
        montoRecibido: json["MontoRecibido"]?.toDouble(),
        montoComision: json["MontoComision"]?.toDouble(),
        totalPago: json["TotalPago"]?.toDouble(),
        fechaOperacion: DateTime.parse(json["FechaOperacion"]),
        temaMensaje: json["TemaMensaje"],
      );

  Map<String, dynamic> toJson() => {
        "NumeroOperacion": numeroOperacion,
        "TransaccionRealizada": transaccionRealizada,
        "Cliente": cliente,
        "CuentaSueldo": cuentaSueldo,
        "SimboloMoneda": simboloMoneda,
        "MontoRecibido": montoRecibido,
        "MontoComision": montoComision,
        "TotalPago": totalPago,
        "FechaOperacion": fechaOperacion.toIso8601String(),
        "TemaMensaje": temaMensaje,
      };
}
