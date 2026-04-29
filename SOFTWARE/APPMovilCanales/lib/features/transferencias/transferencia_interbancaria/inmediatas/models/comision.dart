class Comision {
    int id;
    int idTipoTransferencia;
    String codigoComision;
    String codigoMoneda;
    String codigoAplicacionTarifa;
    double porcentaje;
    double minimo;
    double maximo;
    String indicadorPorcentaje;
    String indicadorFijo;
    double porcentajeCce;
    double minimoCce;
    double maximoCce;


    Comision({
        required this.id,
        required this.idTipoTransferencia,
        required this.codigoComision,
        required this.codigoMoneda,
        required this.codigoAplicacionTarifa,
        required this.porcentaje,
        required this.minimo,
        required this.maximo,
        required this.indicadorPorcentaje,
        required this.indicadorFijo,
        required this.porcentajeCce,
        required this.minimoCce,
        required this.maximoCce,
    });

    factory Comision.fromJson(Map<String, dynamic> json) =>
      Comision(
        id: json["Id"],
        idTipoTransferencia: json["IdTipoTransferencia"],
        codigoComision: json["CodigoComision"],
        codigoMoneda: json["CodigoMoneda"],
        codigoAplicacionTarifa: json["CodigoAplicacionTarifa"],
        porcentaje: json["Porcentaje"]?.toDouble(),
        minimo: json["Minimo"]?.toDouble(),
        maximo: json["Maximo"]?.toDouble(),
        indicadorPorcentaje: json["IndicadorPorcentaje"],
        indicadorFijo: json["IndicadorFijo"],
        porcentajeCce: json["PorcentajeCCE"]?.toDouble(),
        minimoCce: json["MinimoCCE"]?.toDouble(),
        maximoCce: json["MaximoCCE"]?.toDouble(),
      );

    Map<String, dynamic> toJson() => {
        "Id": id,
        "IdTipoTransferencia": idTipoTransferencia,
        "CodigoComision": codigoComision,
        "CodigoMoneda": codigoMoneda,
        "CodigoAplicacionTarifa": codigoAplicacionTarifa,
        "Porcentaje": porcentaje,
        "Minimo": minimo,
        "Maximo": maximo,
        "IndicadorPorcentaje": indicadorPorcentaje,
        "IndicadorFijo": indicadorFijo,
        "PorcentajeCCE": porcentajeCce,
        "MinimoCCE": minimoCce,
        "MaximoCCE": maximoCce,
      };
}
