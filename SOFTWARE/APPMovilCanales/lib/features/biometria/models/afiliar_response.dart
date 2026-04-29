class AfiliarResponse {
  final int idVerificacion;
  final String codigoSolicitado;
  final DateTime fechaSistema;
  final DateTime fechaVencimiento;

  AfiliarResponse({
    required this.idVerificacion,
    required this.codigoSolicitado,
    required this.fechaSistema,
    required this.fechaVencimiento,
  });

  factory AfiliarResponse.fromJson(Map<String, dynamic> json) =>
      AfiliarResponse(
        idVerificacion: json["IdVerificacion"],
        codigoSolicitado: json["CodigoSolicitado"],
        fechaSistema: DateTime.parse(json["FechaSistema"]),
        fechaVencimiento: DateTime.parse(json["FechaVencimiento"]),
      );

  Map<String, dynamic> toJson() => {
        "IdVerificacion": idVerificacion,
        "CodigoSolicitado": codigoSolicitado,
        "FechaSistema": fechaSistema.toIso8601String(),
        "FechaVencimiento": fechaVencimiento.toIso8601String(),
      };
}
