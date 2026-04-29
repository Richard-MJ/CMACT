class VinculosMotivosResponse {
  final List<Vinculo> vinculos;
  final List<Motivo> motivos;

  VinculosMotivosResponse({
    required this.vinculos,
    required this.motivos,
  });

  factory VinculosMotivosResponse.fromJson(Map<String, dynamic> json) =>
      VinculosMotivosResponse(
        vinculos: List<Vinculo>.from(
            json["Vinculos"].map((x) => Vinculo.fromJson(x))),
        motivos:
            List<Motivo>.from(json["Motivos"].map((x) => Motivo.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "Vinculos": List<dynamic>.from(vinculos.map((x) => x.toJson())),
        "Motivos": List<dynamic>.from(motivos.map((x) => x.toJson())),
      };
}

class Vinculo {
  final int idVinculoMotivo;
  final String nombre;
  final bool especificar;
  final dynamic tipo;

  Vinculo({
    required this.idVinculoMotivo,
    required this.nombre,
    required this.especificar,
    required this.tipo,
  });

  factory Vinculo.fromJson(Map<String, dynamic> json) => Vinculo(
        idVinculoMotivo: json["IdVinculoMotivo"],
        nombre: json["Nombre"],
        especificar: json["Especificar"],
        tipo: json["Tipo"],
      );

  Map<String, dynamic> toJson() => {
        "IdVinculoMotivo": idVinculoMotivo,
        "Nombre": nombre,
        "Especificar": especificar,
        "Tipo": tipo,
      };
}

class Motivo {
  final int idVinculoMotivo;
  final String nombre;
  final bool especificar;
  final dynamic tipo;

  Motivo({
    required this.idVinculoMotivo,
    required this.nombre,
    required this.especificar,
    required this.tipo,
  });

  factory Motivo.fromJson(Map<String, dynamic> json) => Motivo(
        idVinculoMotivo: json["IdVinculoMotivo"],
        nombre: json["Nombre"],
        especificar: json["Especificar"],
        tipo: json["Tipo"],
      );

  Map<String, dynamic> toJson() => {
        "IdVinculoMotivo": idVinculoMotivo,
        "Nombre": nombre,
        "Especificar": especificar,
        "Tipo": tipo,
      };
}
