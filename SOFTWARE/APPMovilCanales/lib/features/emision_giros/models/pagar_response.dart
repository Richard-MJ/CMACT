class PagarResponse {
    final int idVerificacion;
    final String codigoSolicitado;
    final DateTime fechaSistema;
    final DateTime fechaVencimiento;

    PagarResponse({
        required this.idVerificacion,
        required this.codigoSolicitado,
        required this.fechaSistema,
        required this.fechaVencimiento,
    });

    factory PagarResponse.fromJson(Map<String, dynamic> json) => PagarResponse(
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