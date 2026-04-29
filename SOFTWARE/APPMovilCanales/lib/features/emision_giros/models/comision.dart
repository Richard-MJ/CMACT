class Comision {
  final double montoGiro;
  final double montoComision;
  final double montoRecepcionado;
  final double montoComisionCmac;
  final double montoItf;
  final double montoEntregadoPorCajero;
  final double montoRedondeoAFavorDelCliente;
  final double montoTotalEntregadoPorCajero;

  Comision({
    required this.montoGiro,
    required this.montoComision,
    required this.montoRecepcionado,
    required this.montoComisionCmac,
    required this.montoItf,
    required this.montoEntregadoPorCajero,
    required this.montoRedondeoAFavorDelCliente,
    required this.montoTotalEntregadoPorCajero,
  });

  factory Comision.fromJson(Map<String, dynamic> json) => Comision(
        montoGiro: json["MontoGiro"]?.toDouble(),
        montoComision: json["MontoComision"]?.toDouble(),
        montoRecepcionado: json["MontoRecepcionado"]?.toDouble(),
        montoComisionCmac: json["MontoComisionCmac"]?.toDouble(),
        montoItf: json["MontoItf"]?.toDouble(),
        montoEntregadoPorCajero: json["MontoEntregadoPorCajero"]?.toDouble(),
        montoRedondeoAFavorDelCliente:
            json["MontoRedondeoAFavorDelCliente"]?.toDouble(),
        montoTotalEntregadoPorCajero:
            json["MontoTotalEntregadoPorCajero"]?.toDouble(),
      );

  Map<String, dynamic> toJson() => {
        "MontoGiro": montoGiro,
        "MontoComision": montoComision,
        "MontoRecepcionado": montoRecepcionado,
        "MontoComisionCmac": montoComisionCmac,
        "MontoItf": montoItf,
        "MontoEntregadoPorCajero": montoEntregadoPorCajero,
        "MontoRedondeoAFavorDelCliente": montoRedondeoAFavorDelCliente,
        "MontoTotalEntregadoPorCajero": montoTotalEntregadoPorCajero,
      };
}
