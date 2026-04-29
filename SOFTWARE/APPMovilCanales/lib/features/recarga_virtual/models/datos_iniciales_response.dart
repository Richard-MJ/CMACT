import 'package:caja_tacna_app/features/recarga_virtual/models/operador_movile.dart';
import 'package:caja_tacna_app/features/recarga_virtual/models/cuenta_origen.dart';
import 'package:caja_tacna_app/features/recarga_virtual/models/operador_movil_kasnet.dart';

class DatosInicialesResponse {
  final List<CuentaOrigenRV> productoParaSeleccionar;
  final List<OperadorMovil> operadorMovil;
  final List<OperadorMovilKasnet> operadorMovilKasnet;

  DatosInicialesResponse({
    required this.productoParaSeleccionar,
    required this.operadorMovil,
    required this.operadorMovilKasnet,   
  });

  factory DatosInicialesResponse.fromJson(Map<String, dynamic> json) =>
      DatosInicialesResponse(
        productoParaSeleccionar: List<CuentaOrigenRV>.from(
            json["ProductosParaSeleccionar"]
                .map((x) => CuentaOrigenRV.fromJson(x))),
        operadorMovil: List<OperadorMovil>.from(
            json["OperadoresMoviles"].map((x) => OperadorMovil.fromJson(x))),
        operadorMovilKasnet: List<OperadorMovilKasnet>.from(
            json["OperadoresMovilesKasnet"].map((x) => OperadorMovilKasnet.fromJson(x))),            
      );

  Map<String, dynamic> toJson() => {
        "ProductosParaSeleccionar":
            List<dynamic>.from(productoParaSeleccionar.map((x) => x.toJson())),
        "OperadoresMoviles":
            List<dynamic>.from(operadorMovil.map((x) => x.toJson())),
        "OperadoresMovilesKasnet":
            List<dynamic>.from(operadorMovilKasnet.map((x) => x.toJson())),
      };
}
