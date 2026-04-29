class DestinoCredito {
  final String codigoDestino;
  final String codigoTipo;
  final String descripcionDestino;

  DestinoCredito({
    required this.codigoDestino,
    required this.codigoTipo,
    required this.descripcionDestino,
  });

  factory DestinoCredito.fromJson(Map<String, dynamic> json) => DestinoCredito(
        codigoDestino: json['CodigoDestino'],
        codigoTipo: json['CodigoTipo'],
        descripcionDestino: json['DescripcionDestino'],
      );

  Map<String, dynamic> toJson() => {
        'CodigoDestino': codigoDestino,
        'CodigoTipo': codigoTipo,
        'DescripcionDestino': descripcionDestino,
      };
}
