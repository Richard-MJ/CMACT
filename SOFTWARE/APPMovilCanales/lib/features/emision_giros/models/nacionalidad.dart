class Nacionalidad {
  final String codigoPais;
  final String nombrePais;

  Nacionalidad({
    required this.codigoPais,
    required this.nombrePais,
  });

  factory Nacionalidad.fromJson(Map<String, dynamic> json) => Nacionalidad(
        codigoPais: json["CodigoPais"],
        nombrePais: json["NombrePais"],
      );

  Map<String, dynamic> toJson() => {
        "CodigoPais": codigoPais,
        "NombrePais": nombrePais,
      };
}
