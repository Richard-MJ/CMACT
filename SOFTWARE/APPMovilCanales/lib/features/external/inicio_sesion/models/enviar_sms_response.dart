class EnviarSmsResponse {
  final int idVerificacion;
  final String numeroTelefonoSms;
  final DateTime fechaSistema;
  final DateTime fechaVencimiento;
  final String dni;

  EnviarSmsResponse({
    required this.idVerificacion,
    required this.numeroTelefonoSms,
    required this.fechaSistema,
    required this.fechaVencimiento,
    required this.dni,
  });

  factory EnviarSmsResponse.fromJson(Map<String, dynamic> json) =>
      EnviarSmsResponse(
        idVerificacion: json["IdVerificacion"],
        numeroTelefonoSms: json["NumeroTelefonoSMS"],
        fechaSistema: DateTime.parse(json["FechaSistema"]),
        fechaVencimiento: DateTime.parse(json["FechaVencimiento"]),
        dni: json["DNI"],
      );

  Map<String, dynamic> toJson() => {
        "IdVerificacion": idVerificacion,
        "NumeroTelefonoSMS": numeroTelefonoSms,
        "FechaSistema": fechaSistema.toIso8601String(),
        "FechaVencimiento": fechaVencimiento.toIso8601String(),
        "DNI": dni,
      };
}
