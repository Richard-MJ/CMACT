import 'package:caja_tacna_app/features/shared/models/monto_real_tipo_cambio.dart';

class PagarResponse {
    final int idVerificacion;
    final String codigoSolicitado;
    final DateTime fechaSistema;
    final DateTime fechaVencimiento;
    final MontoRealTipoCambio montoReal;

    PagarResponse({
        required this.idVerificacion,
        required this.codigoSolicitado,
        required this.fechaSistema,
        required this.fechaVencimiento,
        required this.montoReal,
    });

    factory PagarResponse.fromJson(Map<String, dynamic> json) => PagarResponse(
        idVerificacion: json["IdVerificacion"],
        codigoSolicitado: json["CodigoSolicitado"],
        fechaSistema: DateTime.parse(json["FechaSistema"]),
        fechaVencimiento: DateTime.parse(json["FechaVencimiento"]),
        montoReal: MontoRealTipoCambio.fromJson(json["MontoReal"] ?? {}),
    );

    Map<String, dynamic> toJson() => {
        "IdVerificacion": idVerificacion,
        "CodigoSolicitado": codigoSolicitado,
        "FechaSistema": fechaSistema.toIso8601String(),
        "FechaVencimiento": fechaVencimiento.toIso8601String(),
        "MontoReal": montoReal.toJson(),
    };
}