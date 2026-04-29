import 'package:caja_tacna_app/features/shared/models/select_entidad_financiera_option.dart';

class EntidadFinanciera extends SelectEntidadFinancieraOption {
  final dynamic codigoEntidad;
  final dynamic codigoComision;
  final double montoComision;

  EntidadFinanciera({
    required int idEntidadCce,
    required String nombreEntidadCce,
    required this.codigoEntidad,
    required this.codigoComision,
    required this.montoComision,
  }) : super(
          idEntidadCce: idEntidadCce,
          nombreEntidadCce: nombreEntidadCce,
        );

  factory EntidadFinanciera.fromJson(Map<String, dynamic> json) =>
      EntidadFinanciera(
        idEntidadCce: json["IdEntidadCce"],
        nombreEntidadCce: json["NombreEntidadCce"],
        codigoEntidad: json["CodigoEntidad"],
        codigoComision: json["CodigoComision"],
        montoComision: json["MontoComision"]?.toDouble(),
      );

  Map<String, dynamic> toJson() => {
        "IdEntidadCce": idEntidadCce,
        "NombreEntidadCce": nombreEntidadCce,
        "CodigoEntidad": codigoEntidad,
        "CodigoComision": codigoComision,
        "MontoComision": montoComision,
      };
}
