class ObtenerDatosResponse {
  final String nombreCompleto;
  final String dni;
  final String correoElectronico;
  final String numeroTelefonoCasa;

  ObtenerDatosResponse({
    required this.nombreCompleto,
    required this.dni,
    required this.correoElectronico,
    required this.numeroTelefonoCasa,
  });

  factory ObtenerDatosResponse.fromJson(Map<String, dynamic> json) =>
      ObtenerDatosResponse(
        nombreCompleto: json["NombreCompleto"],
        dni: json["DNI"],
        correoElectronico: json["CorreoElectronico"] ?? '',
        numeroTelefonoCasa: json["NumeroTelefonoCasa"],
      );

  Map<String, dynamic> toJson() => {
        "NombreCompleto": nombreCompleto,
        "DNI": dni,
        "CorreoElectronico": correoElectronico,
        "NumeroTelefonoCasa": numeroTelefonoCasa,
      };
}
