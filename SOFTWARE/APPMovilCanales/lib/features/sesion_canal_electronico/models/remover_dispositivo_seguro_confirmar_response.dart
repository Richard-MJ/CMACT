class RemoverDispositivoSeguroConfirmacionResponse {
  final int idVerificacion;
  final String codigoSolicitado;
  final DateTime fechaSistema;
  final DateTime fechaVencimiento;

  RemoverDispositivoSeguroConfirmacionResponse({
    required this.idVerificacion,
    required this.codigoSolicitado,
    required this.fechaSistema,
    required this.fechaVencimiento,
  });

  factory RemoverDispositivoSeguroConfirmacionResponse.fromJson(
          Map<String, dynamic> json) =>
      RemoverDispositivoSeguroConfirmacionResponse(
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
