import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/models/pagar_app_anticipo_response.dart';

class PagarAppResponse {
  final int idVerificacion;
  final String codigoSolicitado;
  final DateTime fechaSistema;
  final DateTime fechaVencimiento;
  final MontoRealCambio datosMontoReal;

  PagarAppResponse({
    required this.idVerificacion,
    required this.codigoSolicitado,
    required this.fechaSistema,
    required this.fechaVencimiento,
    required this.datosMontoReal,
  });

  factory PagarAppResponse.fromJson(Map<String, dynamic> json) =>
      PagarAppResponse(
        idVerificacion: json["IdVerificacion"],
        codigoSolicitado: json["CodigoSolicitado"],
        fechaSistema: DateTime.parse(json["FechaSistema"]),
        fechaVencimiento: DateTime.parse(json["FechaVencimiento"]),
        datosMontoReal: MontoRealCambio.fromJson(json["DatosMontoReal"]),
      );

  Map<String, dynamic> toJson() => {
        "IdVerificacion": idVerificacion,
        "CodigoSolicitado": codigoSolicitado,
        "FechaSistema": fechaSistema.toIso8601String(),
        "FechaVencimiento": fechaVencimiento.toIso8601String(),
        "DatosMontoReal": datosMontoReal.toJson(),
      };
}
