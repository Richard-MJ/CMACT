class CancelarTransInterbancariaResponse {
  final int idVerificacion;
  final double montoItf;
  final double montoComision;
  final double montoInteres;
  final String nombreEntidadCce;
  final int idEntidadFinancieraCce;
  final String nombreDestino;
  final double montoCancelar;
  final bool esOperacionPropia;
  final bool esPersonaNatural;
  final DatosAutorizacion datosAutorizacion;

  CancelarTransInterbancariaResponse({
    required this.idVerificacion,
    required this.montoItf,
    required this.montoComision,
    required this.montoInteres,
    required this.nombreEntidadCce,
    required this.idEntidadFinancieraCce,
    required this.nombreDestino,
    required this.montoCancelar,
    required this.esOperacionPropia,
    required this.esPersonaNatural,
    required this.datosAutorizacion,
  });

  factory CancelarTransInterbancariaResponse.fromJson(
          Map<String, dynamic> json) =>
      CancelarTransInterbancariaResponse(
        idVerificacion: json["IdVerificacion"],
        montoItf: json["MontoItf"]?.toDouble(),
        montoComision: json["MontoComision"]?.toDouble(),
        montoInteres: json["MontoInteres"]?.toDouble(),
        nombreEntidadCce: json["NombreEntidadCce"],
        idEntidadFinancieraCce: json["IdEntidadFinancieraCce"],
        nombreDestino: json["NombreDestino"],
        montoCancelar: json["MontoCancelar"]?.toDouble(),
        esOperacionPropia: json["EsOperacionPropia"],
        esPersonaNatural: json["EsPersonaNatural"],
        datosAutorizacion:
            DatosAutorizacion.fromJson(json["DatosAutorizacion"]),
      );

  Map<String, dynamic> toJson() => {
        "IdVerificacion": idVerificacion,
        "MontoItf": montoItf,
        "MontoComision": montoComision,
        "MontoInteres": montoInteres,
        "NombreEntidadCce": nombreEntidadCce,
        "IdEntidadFinancieraCce": idEntidadFinancieraCce,
        "NombreDestino": nombreDestino,
        "MontoCancelar": montoCancelar,
        "EsOperacionPropia": esOperacionPropia,
        "EsPersonaNatural": esPersonaNatural,
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
