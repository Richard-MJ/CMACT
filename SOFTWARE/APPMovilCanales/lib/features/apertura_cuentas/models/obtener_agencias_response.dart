import 'package:caja_tacna_app/features/emision_giros/models/agencia.dart';

class ObtenerAgenciasResponse {
  final List<Agencia> agencias;

  ObtenerAgenciasResponse({
    required this.agencias,
  });

  factory ObtenerAgenciasResponse.fromJson(Map<String, dynamic> json) =>
      ObtenerAgenciasResponse(
        agencias: List<Agencia>.from(
            json["agencias"].map((x) => Agencia.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "agencias": List<dynamic>.from(agencias.map((x) => x.toJson())),
      };
}
