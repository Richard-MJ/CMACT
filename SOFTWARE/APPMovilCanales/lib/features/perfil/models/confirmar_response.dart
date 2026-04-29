class ConfirmarResponse {
  final String nombreCompleto;
  final String correoElectronico;
  final String numeroTelefonoCasa;
  final String dni;
  final DateTime fechaOperacion;

  ConfirmarResponse({
    required this.nombreCompleto,
    required this.correoElectronico,
    required this.numeroTelefonoCasa,
    required this.dni,
    required this.fechaOperacion,
  });

  factory ConfirmarResponse.fromJson(Map<String, dynamic> json) =>
      ConfirmarResponse(
        nombreCompleto: json["NombreCompleto"],
        correoElectronico: json["CorreoElectronico"],
        numeroTelefonoCasa: json["NumeroTelefonoCasa"],
        dni: json["DNI"],
        fechaOperacion: DateTime.parse(json["FechaOperacion"]),
      );

  Map<String, dynamic> toJson() => {
        "NombreCompleto": nombreCompleto,
        "CorreoElectronico": correoElectronico,
        "NumeroTelefonoCasa": numeroTelefonoCasa,
        "DNI": dni,
        "FechaOperacion": fechaOperacion.toIso8601String(),
      };
}
