class ConfirmarResponse {
  final DateTime fechaOperacion;
  final String? cadenaHash;

  ConfirmarResponse({
    required this.fechaOperacion,
    this.cadenaHash,
  });

  factory ConfirmarResponse.fromJson(Map<String, dynamic> json) =>
      ConfirmarResponse(
        fechaOperacion: DateTime.parse(json["FechaOperacion"]),
        cadenaHash: json["CadenaHash"]
      );

  Map<String, dynamic> toJson() => {
        "FechaOperacion": fechaOperacion.toIso8601String(),
        "CadenaHash": cadenaHash,
      };
}
