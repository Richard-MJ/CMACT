import 'package:caja_tacna_app/features/solicitud_crediticia/models/destino_credito.dart';

class TipoIngreso {
  final String codigoTipo;
  final String nombreTipo;
  final List<DestinoCredito> destinos;

  TipoIngreso({
    required this.codigoTipo,
    required this.nombreTipo,
    required this.destinos,
  });

  factory TipoIngreso.fromJson(Map<String, dynamic> json) => TipoIngreso(
        codigoTipo: json['CodigoTipo'],
        nombreTipo: json['NombreTipo'],
        destinos: List<DestinoCredito>.from(
            json['Destinos'].map((x) => DestinoCredito.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        'CodigoTipo': codigoTipo,
        'NombreTipo': nombreTipo,
        'Destinos': List<dynamic>.from(destinos.map((x) => x.toJson())),
      };
}
