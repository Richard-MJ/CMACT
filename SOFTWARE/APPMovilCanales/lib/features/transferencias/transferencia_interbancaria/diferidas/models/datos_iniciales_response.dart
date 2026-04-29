import 'package:caja_tacna_app/features/transferencias/models/cuenta_transferencia.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/diferidas/models/tipo_documento.dart';

class DatosInicialesResponse {
  final List<CuentaTransferencia> productosDebito;
  final List<TipoDocumentoTransInter> tiposDocumento;
  final List<EntidadFinanciera> entidadesFinancieras;

  DatosInicialesResponse({
    required this.productosDebito,
    required this.tiposDocumento,
    required this.entidadesFinancieras,
  });

  factory DatosInicialesResponse.fromJson(Map<String, dynamic> json) =>
      DatosInicialesResponse(
        productosDebito: List<CuentaTransferencia>.from(json["ProductosDebito"]
            .map((x) => CuentaTransferencia.fromJson(x))),
        tiposDocumento: List<TipoDocumentoTransInter>.from(
            json["TiposDocumento"]
                .map((x) => TipoDocumentoTransInter.fromJson(x))),
        entidadesFinancieras: List<EntidadFinanciera>.from(
            json["EntidadesFinancieras"]
                .map((x) => EntidadFinanciera.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "ProductosDebito":
            List<dynamic>.from(productosDebito.map((x) => x.toJson())),
        "TiposDocumento":
            List<dynamic>.from(tiposDocumento.map((x) => x.toJson())),
        "EntidadesFinancieras":
            List<dynamic>.from(entidadesFinancieras.map((x) => x.toJson())),
      };
}

class EntidadFinanciera {
  final int idEntidadCce;
  final String nombreEntidadCce;
  final String codigoEntidad;
  final String? codigoComision;
  final double montoComision;

  EntidadFinanciera({
    required this.idEntidadCce,
    required this.nombreEntidadCce,
    required this.codigoEntidad,
    required this.codigoComision,
    required this.montoComision,
  });

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
