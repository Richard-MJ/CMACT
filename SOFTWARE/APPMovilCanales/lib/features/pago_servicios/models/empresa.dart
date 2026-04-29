import 'package:caja_tacna_app/features/pago_servicios/models/pago_servicio.dart';

class Empresa {
  final int tipoPagoServicio;
  final String codigoEmpresa;
  final String nombreEmpresa;
  final int codigoGrupoEmpresa;
  final int codigoCategoria;
  final String nombreCategoria;

  Empresa({
    required this.tipoPagoServicio,
    required this.codigoEmpresa,
    required this.nombreEmpresa,
    required this.codigoGrupoEmpresa,
    required this.codigoCategoria,
    required this.nombreCategoria,
  });

  factory Empresa.fromJson(Map<String, dynamic> json) => Empresa(
        tipoPagoServicio: json["TipoPagoServicio"],
        codigoEmpresa: json["CodigoEmpresa"],
        nombreEmpresa: json["NombreEmpresa"],
        codigoGrupoEmpresa: json["CodigoGrupoEmpresa"],
        codigoCategoria: json["CodigoCategoria"],
        nombreCategoria: json["NombreCategoria"],
      );

  Map<String, dynamic> toJson() => {
        "TipoPagoServicio": tipoPagoServicio,
        "CodigoEmpresa": codigoEmpresa,
        "NombreEmpresa": nombreEmpresa,
        "CodigoGrupoEmpresa": codigoGrupoEmpresa,
        "CodigoCategoria": codigoCategoria,
        "NombreCategoria": nombreCategoria,
      };

  Empresa copyWith({
    List<PagoServicio>? servicios,
  }) {
    return Empresa(
      tipoPagoServicio: tipoPagoServicio,
      codigoEmpresa: codigoEmpresa,
      nombreEmpresa: nombreEmpresa,
      codigoGrupoEmpresa: codigoGrupoEmpresa,
      codigoCategoria: codigoCategoria,
      nombreCategoria: nombreCategoria,
    );
  }
}
