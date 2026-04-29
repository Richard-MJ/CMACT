import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/models/index.dart';

class PagarAppAnticipoResponse {
  final PagarAppResponse datosTokenDigital;
  final CronogramaPlanPagos cronogramaPlanPagos;

  PagarAppAnticipoResponse(
      {required this.datosTokenDigital,
      required this.cronogramaPlanPagos});

  factory PagarAppAnticipoResponse.fromJson(Map<String, dynamic> json) {
    return PagarAppAnticipoResponse(
      datosTokenDigital: PagarAppResponse.fromJson(json['DatosTokenDigital']),
      cronogramaPlanPagos: CronogramaPlanPagos.fromJson(json['CronogramaPlanPagos']),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'DatosTokenDigital': datosTokenDigital.toJson(),
      'CronogramaPlanPagos': cronogramaPlanPagos.toJson(),
    };
  }
}

class CronogramaPlanPagos {
  final int respuesta;
  final String? datos;
  final String? nombreCartilla;

  CronogramaPlanPagos({
    required this.respuesta,
    required this.datos,
    required this.nombreCartilla,
  });

  factory CronogramaPlanPagos.fromJson(Map<String, dynamic> json) {
    return CronogramaPlanPagos(
      respuesta: json['Respuesta'],
      datos: json['Datos'] ?? "",
      nombreCartilla: json['NombreCartilla'] ?? "",
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'Respuesta': respuesta,
      'Datos': datos,
      'NombreCartilla': nombreCartilla,
    };
  }
}

class MontoRealCambio {
  final double tipoCambio;
  final double valorReal;
  final double itf;

  MontoRealCambio({
    required this.tipoCambio,
    required this.valorReal,
    required this.itf,
  });

  factory MontoRealCambio.fromJson(Map<String, dynamic> json) {
    return MontoRealCambio(
      tipoCambio: json['TipoCambio'],
      valorReal: json['ValorReal'],
      itf: json['Itf'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'TipoCambio': tipoCambio,
      'ValorReal': valorReal,
      'Itf': itf,
    };
  }
}
