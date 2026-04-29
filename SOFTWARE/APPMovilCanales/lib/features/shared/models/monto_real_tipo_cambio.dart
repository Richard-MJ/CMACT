class MontoRealTipoCambio {
  final double tipoCambio;
  final double valorReal;
  final double itf;

  MontoRealTipoCambio({
    required this.tipoCambio,
    required this.valorReal,
    required this.itf,
  });

  factory MontoRealTipoCambio.fromJson(Map<String, dynamic> json) =>
      MontoRealTipoCambio(
        tipoCambio: json["TipoCambio"]?.toDouble() ?? 0.0,
        valorReal: json["ValorReal"]?.toDouble() ?? 0.0,
        itf: json["Itf"]?.toDouble() ?? 0.0,
      );

  Map<String, dynamic> toJson() => {
        "TipoCambio": tipoCambio,
        "ValorReal": valorReal,
        "Itf": itf,
      };
}
