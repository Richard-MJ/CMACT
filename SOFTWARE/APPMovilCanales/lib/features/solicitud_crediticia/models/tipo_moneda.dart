class TipoMoneda {
  final String codigoMoneda;
  final String descripcion;
  final String operador;
  final String abreviacion;
  final String simbolo;
  final String codigoISO;
  final String codigoSunat;

  TipoMoneda({
    required this.codigoMoneda,
    required this.descripcion,
    required this.operador,
    required this.abreviacion,
    required this.simbolo,
    required this.codigoISO,
    required this.codigoSunat,
  });

  factory TipoMoneda.fromJson(Map<String, dynamic> json) => TipoMoneda(
        codigoMoneda: json['CodigoMoneda'],
        descripcion: json['Descripcion'],
        operador: json['Operador'],
        abreviacion: json['Abreviacion'],
        simbolo: json['Simbolo'],
        codigoISO: json['CodigoISO'],
        codigoSunat: json['CodigoSunat'],
      );

  Map<String, dynamic> toJson() => {
        'CodigoMoneda': codigoMoneda,
        'Descripcion': descripcion,
        'Operador': operador,
        'Abreviacion': abreviacion,
        'Simbolo': simbolo,
        'CodigoISO': codigoISO,
        'CodigoSunat': codigoSunat,
      };
}
