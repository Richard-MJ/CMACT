class Novedad {
  final int id;
  final String titulo;
  final String resumen;
  final String descripcion;
  final String urlImagen;
  final int idCategoria;
  final String categoria;
  final bool activo;
  final DateTime fechaRegistro;
  final String fechaRegistroFormat;
  final DateTime fechaInicio;
  final String fechaInicioFormat;
  final DateTime fechaFinal;
  final String fechaFinalFormat;
  final int idTipoAviso;
  final String tipoAviso;

  Novedad({
    required this.id,
    required this.titulo,
    required this.resumen,
    required this.descripcion,
    required this.urlImagen,
    required this.idCategoria,
    required this.categoria,
    required this.activo,
    required this.fechaRegistro,
    required this.fechaRegistroFormat,
    required this.fechaInicio,
    required this.fechaInicioFormat,
    required this.fechaFinal,
    required this.fechaFinalFormat,
    required this.idTipoAviso,
    required this.tipoAviso,
  });

  factory Novedad.fromJson(Map<String, dynamic> json) => Novedad(
        id: json["id"],
        titulo: json["titulo"],
        resumen: json["resumen"],
        descripcion: json["descripcion"],
        urlImagen: json["urlImagen"],
        idCategoria: json["idCategoria"],
        categoria: json["categoria"],
        activo: json["activo"],
        fechaRegistro: DateTime.parse(json["fechaRegistro"]),
        fechaRegistroFormat: json["fechaRegistroFormat"],
        fechaInicio: DateTime.parse(json["fechaInicio"]),
        fechaInicioFormat: json["fechaInicioFormat"],
        fechaFinal: DateTime.parse(json["fechaFinal"]),
        fechaFinalFormat: json["fechaFinalFormat"],
        idTipoAviso: json["idTipoAviso"],
        tipoAviso: json["tipoAviso"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "titulo": titulo,
        "resumen": resumen,
        "descripcion": descripcion,
        "urlImagen": urlImagen,
        "idCategoria": idCategoria,
        "categoria": categoria,
        "activo": activo,
        "fechaRegistro": fechaRegistro.toIso8601String(),
        "fechaRegistroFormat": fechaRegistroFormat,
        "fechaInicio": fechaInicio.toIso8601String(),
        "fechaInicioFormat": fechaInicioFormat,
        "fechaFinal": fechaFinal.toIso8601String(),
        "fechaFinalFormat": fechaFinalFormat,
        "idTipoAviso": idTipoAviso,
        "tipoAviso": tipoAviso,
      };
}
