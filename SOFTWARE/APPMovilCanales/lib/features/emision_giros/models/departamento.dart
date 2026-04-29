class Departamento {
  final String codigoDepartamento;
  final String descripcionDepartamento;

  Departamento({
    required this.codigoDepartamento,
    required this.descripcionDepartamento,
  });

  factory Departamento.fromJson(Map<String, dynamic> json) => Departamento(
        codigoDepartamento: json["CodigoDepartamento"],
        descripcionDepartamento: json["DescripcionDepartamento"],
      );

  Map<String, dynamic> toJson() => {
        "CodigoDepartamento": codigoDepartamento,
        "DescripcionDepartamento": descripcionDepartamento,
      };
}
