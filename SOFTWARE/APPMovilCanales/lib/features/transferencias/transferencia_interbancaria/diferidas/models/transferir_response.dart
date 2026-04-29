class TransferirResponse {
  final double montoItf;
  final double montoComision;
  final String codigoTarifarioComision;
  final String nombreEntidadCce;
  final int idEntidadFinancieraCce;
  final String nombreDestino;
  final double montoTransferir;
  final bool mismoTitularEnDestino;
  final bool esPersonaNatural;
  final DatosAutorizacion datosAutorizacion;

  TransferirResponse({
    required this.montoItf,
    required this.montoComision,
    required this.codigoTarifarioComision,
    required this.nombreEntidadCce,
    required this.idEntidadFinancieraCce,
    required this.nombreDestino,
    required this.montoTransferir,
    required this.mismoTitularEnDestino,
    required this.esPersonaNatural,
    required this.datosAutorizacion,
  });

  factory TransferirResponse.fromJson(Map<String, dynamic> json) =>
      TransferirResponse(
        montoItf: json["MontoItf"]?.toDouble(),
        montoComision: json["MontoComision"]?.toDouble(),
        codigoTarifarioComision: json["CodigoTarifarioComision"],
        nombreEntidadCce: json["NombreEntidadCce"],
        idEntidadFinancieraCce: json["IdEntidadFinancieraCce"],
        nombreDestino: json["NombreDestino"],
        montoTransferir: json["MontoTransferir"]?.toDouble(),
        mismoTitularEnDestino: json["MismoTitularEnDestino"],
        esPersonaNatural: json["EsPersonaNatural"],
        datosAutorizacion:
            DatosAutorizacion.fromJson(json["DatosAutorizacion"]),
      );

  Map<String, dynamic> toJson() => {
        "MontoItf": montoItf,
        "MontoComision": montoComision,
        "CodigoTarifarioComision": codigoTarifarioComision,
        "NombreEntidadCce": nombreEntidadCce,
        "IdEntidadFinancieraCce": idEntidadFinancieraCce,
        "NombreDestino": nombreDestino,
        "MontoTransferir": montoTransferir,
        "MismoTitularEnDestino": mismoTitularEnDestino,
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
