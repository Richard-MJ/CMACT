class Publicidad {
    final int id;
    final String nombre;
    final String descripcion;
    final bool activo;
    final DateTime fechaRegistro;
    final String fechaRegistroFormat;
    final String nombreImagen;
    final String urlImagen;
    final String descripcionCategoria;
    final int idCategoria;

    Publicidad({
        required this.id,
        required this.nombre,
        required this.descripcion,
        required this.activo,
        required this.fechaRegistro,
        required this.fechaRegistroFormat,
        required this.nombreImagen,
        required this.urlImagen,
        required this.descripcionCategoria,
        required this.idCategoria,
    });

    factory Publicidad.fromJson(Map<String, dynamic> json) => Publicidad(
        id: json["id"],
        nombre: json["nombre"],
        descripcion: json["descripcion"],
        activo: json["activo"],
        fechaRegistro: DateTime.parse(json["fechaRegistro"]),
        fechaRegistroFormat: json["fechaRegistroFormat"],
        nombreImagen: json["nombreImagen"],
        urlImagen: json["urlImagen"],
        descripcionCategoria: json["descripcionCategoria"],
        idCategoria: json["idCategoria"],
    );

    Map<String, dynamic> toJson() => {
        "id": id,
        "nombre": nombre,
        "descripcion": descripcion,
        "activo": activo,
        "fechaRegistro": fechaRegistro.toIso8601String(),
        "fechaRegistroFormat": fechaRegistroFormat,
        "nombreImagen": nombreImagen,
        "urlImagen": urlImagen,
        "descripcionCategoria": descripcionCategoria,
        "idCategoria": idCategoria,
    };
}

