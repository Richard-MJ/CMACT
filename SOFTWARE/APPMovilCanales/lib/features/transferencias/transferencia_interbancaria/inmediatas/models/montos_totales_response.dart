class MontosTotales {
    ControlMonto controlMonto;
    
    MontosTotales({
        required this.controlMonto,
    });

    factory MontosTotales.fromJson(Map<String, dynamic> json) =>
        MontosTotales(
          controlMonto: ControlMonto.fromJson(json["ControlMonto"]),
        );

    Map<String, dynamic> toJson() => {
          "ControlMonto": controlMonto.toJson(),
      };
}

class ControlMonto {
    String? codigoMoneda;
    String? codigoMonedaOrigen;
    double monto;
    double? montoComisionEntidad;
    double? montoComisionCce;
    double? itf;
    double? totalComision;
    double? total;

    ControlMonto({
        this.codigoMoneda,
        this.codigoMonedaOrigen,
        required this.monto,
        this.montoComisionEntidad,
        this.montoComisionCce,
        this.itf,
        this.totalComision,
        this.total,
    });

    factory ControlMonto.fromJson(Map<String, dynamic> json) =>
        ControlMonto(
          codigoMoneda: json["CodigoMoneda"],
          codigoMonedaOrigen: json["CodigoMonedaOrigen"],
          monto: json["Monto"]?.toDouble(),
          montoComisionEntidad: json["MontoComisionEntidad"]?.toDouble(),
          montoComisionCce: json["MontoComisionCce"]?.toDouble(),
          itf: json["Itf"]?.toDouble(),
          totalComision: json["TotalComision"]?.toDouble(),
          total: json["Total"]?.toDouble(),
        );

    Map<String, dynamic> toJson() => {
        "CodigoMoneda": codigoMoneda,
        "CodigoMonedaOrigen": codigoMonedaOrigen,
        "Monto": monto,
        "MontoComisionEntidad": montoComisionEntidad,
        "MontoComisionCce": montoComisionCce,
        "Itf": itf,
        "TotalComision": totalComision,
        "Total": total,
      };
}