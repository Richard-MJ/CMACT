class ConfirmarResponse {
  final DateTime fechaOperacion;
  final String mensaje;

  ConfirmarResponse({
    required this.fechaOperacion,
    required this.mensaje,
  });

  factory ConfirmarResponse.fromJson(Map<String, dynamic> json) =>
      ConfirmarResponse(
        fechaOperacion: DateTime.parse(json["FechaOperacion"]),
        mensaje: json["Mensaje"],
      );

  Map<String, dynamic> toJson() => {
        "FechaOperacion": fechaOperacion.toIso8601String(),
        "Mensaje": mensaje,
      };
}
