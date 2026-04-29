class DatosEstadoCuentaCorreo {
  final String mensaje;

  DatosEstadoCuentaCorreo({
    required this.mensaje,
  });

  factory DatosEstadoCuentaCorreo.fromJson(Map<String, dynamic> json) =>
      DatosEstadoCuentaCorreo(
        mensaje: json["Mensaje"],
      );

  Map<String, dynamic> toJson() => {
        "Mensaje": mensaje,
      };
}
