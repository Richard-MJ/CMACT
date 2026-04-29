class AgregarOperacionFrecuenteResponse {
  final String mensaje;

  AgregarOperacionFrecuenteResponse({
    required this.mensaje,
  });

  factory AgregarOperacionFrecuenteResponse.fromJson(
          Map<String, dynamic> json) =>
      AgregarOperacionFrecuenteResponse(
        mensaje: json["Mensaje"],
      );

  Map<String, dynamic> toJson() => {
        "Mensaje": mensaje,
      };
}
