class IngresarSolicitudResponse {
  final int numeroSolicitud;
  final DateTime fechaRegistroSolicitud;
  final double montoPrestamo;
  final int numeroPlazo;

  IngresarSolicitudResponse({
    required this.numeroSolicitud,
    required this.fechaRegistroSolicitud,
    required this.montoPrestamo,
    required this.numeroPlazo,
  });

  factory IngresarSolicitudResponse.fromJson(Map<String, dynamic> json) =>
      IngresarSolicitudResponse(
        numeroSolicitud: json["NumeroSolicitud"],
        fechaRegistroSolicitud: DateTime.parse(json["FechaRegistroSolicitud"]),
        montoPrestamo: json["MontoPrestamo"],
        numeroPlazo: json["NumeroPlazo"],
      );

  Map<String, dynamic> toJson() => {
        "NumeroSolicitud": numeroSolicitud,
        "FechaRegistroSolicitud": fechaRegistroSolicitud.toIso8601String(),
        "MontoPrestamo": montoPrestamo,
        "NumeroPlazo": numeroPlazo,
      };
}
