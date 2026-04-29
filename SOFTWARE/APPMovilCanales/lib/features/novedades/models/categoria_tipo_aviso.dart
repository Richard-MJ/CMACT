class CategoriaTipoAviso {
  final int id;
  final String nombre;
  final int idTipoAviso;
  final String tipoAviso;
  final int activo;

  CategoriaTipoAviso({
    required this.id,
    required this.nombre,
    required this.idTipoAviso,
    required this.tipoAviso,
    required this.activo,
  });

  factory CategoriaTipoAviso.fromJson(Map<String, dynamic> json) =>
      CategoriaTipoAviso(
        id: json["id"],
        nombre: json["nombre"],
        idTipoAviso: json["idTipoAviso"],
        tipoAviso: json["tipoAviso"],
        activo: json["activo"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "nombre": nombre,
        "idTipoAviso": idTipoAviso,
        "tipoAviso": tipoAviso,
        "activo": activo,
      };
}
