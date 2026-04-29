class DatosRespuestaBarrido {
  double montoMaximoDia;
  double limiteMontoMaximo;
  double limiteMontoMinimo;
  final List<ResultadoBarrido> resultadosBarridos;

  DatosRespuestaBarrido({
    required this.montoMaximoDia,
    required this.limiteMontoMaximo,
    required this.limiteMontoMinimo,
    required this.resultadosBarridos,
  });

  factory DatosRespuestaBarrido.fromJson(Map<String, dynamic> json) =>
      DatosRespuestaBarrido(
        montoMaximoDia: json["MontoMaximoDia"]?.toDouble(),
        limiteMontoMaximo: json["LimiteMontoMaximo"]?.toDouble(),
        limiteMontoMinimo: json["LimiteMontoMinimo"]?.toDouble(),
        resultadosBarridos: List<ResultadoBarrido>.from(json["ResultadosBarrido"]
            .map((x) => ResultadoBarrido.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "MontoMaximoDia": montoMaximoDia,
        "LimiteMontoMaximo": limiteMontoMaximo,
        "LimiteMontoMinimo": limiteMontoMinimo,
        "ResultadosBarrido": List<dynamic>.from(resultadosBarridos.map((x) => x.toJson())),
      };
}

class ResultadoBarrido {
  final String? numeroCelular;
  final List<EntidadesReceptorAfiliado> entidadesFinancieras;

  ResultadoBarrido({
    this.numeroCelular,
    required this.entidadesFinancieras,
  });

  factory ResultadoBarrido.fromJson(Map<String, dynamic> json) =>
      ResultadoBarrido(
        numeroCelular: json["NumeroCelular"],
        entidadesFinancieras: List<EntidadesReceptorAfiliado>.from(json["EntidadesReceptor"]
            .map((x) => EntidadesReceptorAfiliado.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "NumeroCelular": numeroCelular,
        "EntidadesReceptor": List<dynamic>.from(entidadesFinancieras.map((x) => x.toJson())),
      };
}

class EntidadesReceptorAfiliado{
  final String? codigoEntidad;
  final String? nombreEntidad;

  EntidadesReceptorAfiliado({
    required this.codigoEntidad,
    required this.nombreEntidad,
  });

  factory EntidadesReceptorAfiliado.fromJson(Map<String, dynamic> json) => EntidadesReceptorAfiliado(
        codigoEntidad: json["CodigoEntidad"],
        nombreEntidad: json["NombreEntidad"],
      );

  Map<String, dynamic> toJson() => {
        "CodigoEntidad": codigoEntidad,
        "NombreEntidad": nombreEntidad,
      };
}


