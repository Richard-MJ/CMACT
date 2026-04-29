import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/models/detalle_transferencia.dart';

class DatosConsultaCuentaPorQrResponse {
    String nombreEntidadReceptora;
    String? identificadorQR;
    double montoMaximoDia;
    double limiteMontoMaximo;
    double limiteMontoMinimo;
    DatosConsultaCuentaResponse datosConsultaCuenta;

    DatosConsultaCuentaPorQrResponse({
        required this.nombreEntidadReceptora,
        this.identificadorQR,
        required this.montoMaximoDia,
        required this.limiteMontoMaximo,
        required this.limiteMontoMinimo,
        required this.datosConsultaCuenta,
    });

  factory DatosConsultaCuentaPorQrResponse.fromJson(Map<String, dynamic> json) =>
      DatosConsultaCuentaPorQrResponse(
        montoMaximoDia: json["MontoMaximoDia"]?.toDouble(),
        limiteMontoMaximo: json["LimiteMontoMaximo"]?.toDouble(),
        limiteMontoMinimo: json["LimiteMontoMinimo"]?.toDouble(),
        nombreEntidadReceptora: json["NombreEntidadReceptora"],
        identificadorQR: json["IdentificadorQR"],
        datosConsultaCuenta: DatosConsultaCuentaResponse.fromJson(json["DatosConsulta"]),
      );

  Map<String, dynamic> toJson() => {
        "MontoMaximoDia": montoMaximoDia,
        "LimiteMontoMaximo": limiteMontoMaximo,
        "LimiteMontoMinimo": limiteMontoMinimo,
        "NombreEntidadReceptora": nombreEntidadReceptora,
        "IdentificadorQR": identificadorQR,
        "DatosConsulta": datosConsultaCuenta.toJson(),
      };
}


class DatosConsultaCuentaResponse {
    DetalleTransferencia detalleTransferencia;

    DatosConsultaCuentaResponse({
        required this.detalleTransferencia,
    });

  factory DatosConsultaCuentaResponse.fromJson(Map<String, dynamic> json) =>
      DatosConsultaCuentaResponse(
        detalleTransferencia: DetalleTransferencia.fromJson(json["ResultadoConsultaCuenta"]),
      );

  Map<String, dynamic> toJson() => {
        "ResultadoConsultaCuenta": detalleTransferencia.toJson(),
      };
}