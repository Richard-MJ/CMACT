class DatosOperacionExitosaResponse{
  final int numeroOperacion;
  final DateTime fechaOperacion;

  DatosOperacionExitosaResponse({
    required this.numeroOperacion,
    required this.fechaOperacion,
  });

  factory DatosOperacionExitosaResponse.fromJson(Map<String, dynamic> json) => DatosOperacionExitosaResponse(
        numeroOperacion: json["NumeroOperacion"],
        fechaOperacion: DateTime.parse(json["FechaOperacion"]),
      );

  Map<String, dynamic> toJson() => {
        "NumeroOperacion": numeroOperacion,
        "FechaOperacion": fechaOperacion.toIso8601String(),
      };
}
