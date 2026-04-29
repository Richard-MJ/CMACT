class ModificarOperacion {
  final String mensaje;

  ModificarOperacion({
    required this.mensaje,
  });

  factory ModificarOperacion.fromJson(Map<String, dynamic> json) =>
      ModificarOperacion(
        mensaje: json["Mensaje"],
      );

  Map<String, dynamic> toJson() => {
        "Mensaje": mensaje,
      };
}
