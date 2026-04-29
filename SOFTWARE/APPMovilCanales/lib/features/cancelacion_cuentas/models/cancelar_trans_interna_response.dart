//para cancelaciones con trans propia o terceros

class CancelarTransInternaResponse {
  final double montoInteres;
  final double montoCancelacion;
  final double montoItf;
  final double tipoCambio;
  final double montoDestino;
  final int idVerificacion;
  final double montoComision;
  final DateTime fechaVencimientoDeposito;
  final double montoPrincipalDeposito;
  final double interesPorPagar;
  final bool cancelacionAnticipada;
  final double interesCancelacion;
  final DatosAutorizacion datosAutorizacion;

  CancelarTransInternaResponse({
    required this.montoInteres,
    required this.montoCancelacion,
    required this.montoItf,
    required this.tipoCambio,
    required this.montoDestino,
    required this.idVerificacion,
    required this.montoComision,
    required this.fechaVencimientoDeposito,
    required this.montoPrincipalDeposito,
    required this.interesPorPagar,
    required this.cancelacionAnticipada,
    required this.interesCancelacion,
    required this.datosAutorizacion,
  });

  factory CancelarTransInternaResponse.fromJson(Map<String, dynamic> json) =>
      CancelarTransInternaResponse(
        montoInteres: json["MontoInteres"]?.toDouble(),
        montoCancelacion: json["MontoCancelacion"]?.toDouble(),
        montoItf: json["MontoItf"]?.toDouble(),
        tipoCambio: json["TipoCambio"]?.toDouble(),
        montoDestino: json["MontoDestino"]?.toDouble(),
        idVerificacion: json["IdVerificacion"],
        montoComision: json["MontoComision"]?.toDouble(),
        fechaVencimientoDeposito:
            DateTime.parse(json["FechaVencimientoDeposito"]),
        montoPrincipalDeposito: json["MontoPrincipalDeposito"]?.toDouble(),
        interesPorPagar: json["InteresPorPagar"]?.toDouble(),
        cancelacionAnticipada: json["CancelacionAnticipada"],
        interesCancelacion: json["InteresCancelacion"]?.toDouble(),
        datosAutorizacion:
            DatosAutorizacion.fromJson(json["DatosAutorizacion"]),
      );

  Map<String, dynamic> toJson() => {
        "MontoInteres": montoInteres,
        "MontoCancelacion": montoCancelacion,
        "MontoItf": montoItf,
        "TipoCambio": tipoCambio,
        "MontoDestino": montoDestino,
        "IdVerificacion": idVerificacion,
        "MontoComision": montoComision,
        "FechaVencimientoDeposito": fechaVencimientoDeposito.toIso8601String(),
        "MontoPrincipalDeposito": montoPrincipalDeposito,
        "InteresPorPagar": interesPorPagar,
        "CancelacionAnticipada": cancelacionAnticipada,
        "InteresCancelacion": interesCancelacion,
        "DatosAutorizacion": datosAutorizacion.toJson(),
      };
}

class DatosAutorizacion {
  final int idVerificacion;
  final String codigoSolicitado;
  final DateTime fechaSistema;
  final DateTime fechaVencimiento;

  DatosAutorizacion({
    required this.idVerificacion,
    required this.codigoSolicitado,
    required this.fechaSistema,
    required this.fechaVencimiento,
  });

  factory DatosAutorizacion.fromJson(Map<String, dynamic> json) =>
      DatosAutorizacion(
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
