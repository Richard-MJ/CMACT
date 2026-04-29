class OperadorMovil {
    final String codigoOperador;
    final String descripcionOperador;
    final double monMinRecarga;
    final double monMaxRecarga;
    final String nombreMoneda;
    final String codigoMoneda;
    final String simboloMoneda;

    OperadorMovil({
        required this.codigoOperador,
        required this.descripcionOperador,
        required this.monMinRecarga,
        required this.monMaxRecarga,
        required this.nombreMoneda,
        required this.codigoMoneda,
        required this.simboloMoneda,
    });

    factory OperadorMovil.fromJson(Map<String, dynamic> json) => OperadorMovil(
        codigoOperador: json["CodigoOperador"],
        descripcionOperador: json["DescripcionOperador"],
        monMinRecarga: json["MonMinRecarga"]?.toDouble(),
        monMaxRecarga: json["MonMaxRecarga"]?.toDouble(),
        nombreMoneda: json["NombreMoneda"],
        codigoMoneda: json["CodigoMoneda"],
        simboloMoneda: json["SimboloMoneda"],
    );

    Map<String, dynamic> toJson() => {
        "CodigoOperador": codigoOperador,
        "DescripcionOperador": descripcionOperador,
        "MonMinRecarga": monMinRecarga,
        "MonMaxRecarga": monMaxRecarga,
        "NombreMoneda": nombreMoneda,
        "CodigoMoneda": codigoMoneda,
        "SimboloMoneda": simboloMoneda,
    };
}
