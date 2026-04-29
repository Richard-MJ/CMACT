import 'package:caja_tacna_app/features/emision_giros/models/departamento.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/models/tipo_ingreso.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/models/tipo_moneda.dart';

class ObtenerDatosInicialesSolicitudCrediticiaResponse {
  final List<TipoIngreso> tiposIngreso;
  final List<TipoMoneda> tiposMoneda;
  final List<Departamento> departamentos;

  ObtenerDatosInicialesSolicitudCrediticiaResponse({
    required this.tiposIngreso,
    required this.tiposMoneda,
    required this.departamentos,
  });

  factory ObtenerDatosInicialesSolicitudCrediticiaResponse.fromJson(
          Map<String, dynamic> json) =>
      ObtenerDatosInicialesSolicitudCrediticiaResponse(
        tiposIngreso: List<TipoIngreso>.from(
            json['TiposIngreso'].map((x) => TipoIngreso.fromJson(x))),
        tiposMoneda: List<TipoMoneda>.from(
            json['TiposMoneda'].map((x) => TipoMoneda.fromJson(x))),
        departamentos: List<Departamento>.from(
            json['Departamentos'].map((x) => Departamento.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        'TiposIngreso': List<dynamic>.from(tiposIngreso.map((x) => x.toJson())),
        'TiposMoneda': List<dynamic>.from(tiposMoneda.map((x) => x.toJson())),
        'Departamentos': List<dynamic>.from(departamentos.map((x) => x.toJson())),
      };
}
