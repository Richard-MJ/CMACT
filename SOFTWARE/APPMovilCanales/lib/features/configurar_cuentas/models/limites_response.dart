class LimitesResponse {
  final double numeroMaximoOperacionesDefecto;
  final double montoMaximoOperacionDefecto;
  final double montoMinimoOperacionDefecto;
  final double numeroMaximoOperacionesLimiteCliente;
  final double montoMaximoOperacionLimiteCliente;
  final double montoMaximoOperacionLimiteSemanalDefecto;
  final double montoMaximoOperacionLimiteSemanalCliente;

  LimitesResponse({
    required this.numeroMaximoOperacionesDefecto,
    required this.montoMaximoOperacionDefecto,
    required this.montoMinimoOperacionDefecto,
    required this.numeroMaximoOperacionesLimiteCliente,
    required this.montoMaximoOperacionLimiteCliente,
    required this.montoMaximoOperacionLimiteSemanalDefecto,
    required this.montoMaximoOperacionLimiteSemanalCliente,
  });

  factory LimitesResponse.fromJson(Map<String, dynamic> json) =>
      LimitesResponse(
        numeroMaximoOperacionesDefecto:
            json["NumeroMaximoOperacionesDefecto"]?.toDouble(),
        montoMaximoOperacionDefecto:
            json["MontoMaximoOperacionDefecto"]?.toDouble(),
        montoMinimoOperacionDefecto:
            json["MontoMinimoOperacionDefecto"]?.toDouble(),
        numeroMaximoOperacionesLimiteCliente:
            json["NumeroMaximoOperacionesLimiteCliente"]?.toDouble(),
        montoMaximoOperacionLimiteCliente:
            json["MontoMaximoOperacionLimiteCliente"]?.toDouble(),
        montoMaximoOperacionLimiteSemanalDefecto:
            json["MontoMaximoOperacionLimiteSemanalDefecto"]?.toDouble(),
        montoMaximoOperacionLimiteSemanalCliente:
            json["MontoMaximoOperacionLimiteSemanalCliente"]?.toDouble(),  
      );

  Map<String, dynamic> toJson() => {
        "NumeroMaximoOperacionesDefecto": numeroMaximoOperacionesDefecto,
        "MontoMaximoOperacionDefecto": montoMaximoOperacionDefecto,
        "MontoMinimoOperacionDefecto": montoMinimoOperacionDefecto,
        "NumeroMaximoOperacionesLimiteCliente":
            numeroMaximoOperacionesLimiteCliente,
        "MontoMaximoOperacionLimiteCliente": montoMaximoOperacionLimiteCliente,
        "MontoMaximoOperacionLimiteSemanalDefecto": montoMaximoOperacionLimiteSemanalDefecto,
        "MontoMaximoOperacionLimiteSemanalCliente": montoMaximoOperacionLimiteSemanalCliente,
      };
}
