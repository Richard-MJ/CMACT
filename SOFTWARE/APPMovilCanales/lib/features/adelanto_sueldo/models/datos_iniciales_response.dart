import 'package:caja_tacna_app/features/adelanto_sueldo/models/cuenta_destino.dart';

class DatosInicialesResponse {
  final int numeroAfiliacion;
  final String codigoCliente;
  final String nombreCliente;
  final String estadoAfiliacion;
  final List<CuentaDestinoAdelSuel> productos;
  final double montoRedondear;

  DatosInicialesResponse({
    required this.numeroAfiliacion,
    required this.codigoCliente,
    required this.nombreCliente,
    required this.estadoAfiliacion,
    required this.productos,
    required this.montoRedondear,
  });

  factory DatosInicialesResponse.fromJson(Map<String, dynamic> json) =>
      DatosInicialesResponse(
        numeroAfiliacion: json["NumeroAfiliacion"],
        codigoCliente: json["CodigoCliente"],
        nombreCliente: json["NombreCliente"],
        estadoAfiliacion: json["EstadoAfiliacion"],
        productos: List<CuentaDestinoAdelSuel>.from(
            json["Productos"].map((x) => CuentaDestinoAdelSuel.fromJson(x))),
        montoRedondear: json["MontoRedondear"]?.toDouble(),
      );

  Map<String, dynamic> toJson() => {
        "NumeroAfiliacion": numeroAfiliacion,
        "CodigoCliente": codigoCliente,
        "NombreCliente": nombreCliente,
        "EstadoAfiliacion": estadoAfiliacion,
        "Productos": List<dynamic>.from(productos.map((x) => x.toJson())),
        "MontoRedondear": montoRedondear,
      };
}
