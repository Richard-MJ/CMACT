import 'package:caja_tacna_app/features/shared/models/select_tipo_documento.dart';

class TipoDocumentoGiro extends SelectTipoDocumento {
  final int codigoTipoDocumento;
  final int codigoTipoDocumentoCamaraCompensacion;
  final String descripcionTipoDocumento;
  final bool esTipoPersonaJuridica;
  final int longitudTipoDocumento;

  TipoDocumentoGiro({
    required int idTipoDocumento,
    required String descripcion,
    required this.codigoTipoDocumento,
    required this.codigoTipoDocumentoCamaraCompensacion,
    required this.descripcionTipoDocumento,
    required this.esTipoPersonaJuridica,
    required this.longitudTipoDocumento,
  }) : super(
          idTipoDocumento: idTipoDocumento,
          descripcion: descripcion,
        );

  factory TipoDocumentoGiro.fromJson(Map<String, dynamic> json) =>
      TipoDocumentoGiro(
        idTipoDocumento: json["CodigoTipoDocumento"],
        descripcion: json["DescripcionTipoDocumento"],
        codigoTipoDocumentoCamaraCompensacion:
            json["CodigoTipoDocumentoCamaraCompensacion"],
        codigoTipoDocumento: json["CodigoTipoDocumento"],
        descripcionTipoDocumento: json["DescripcionTipoDocumento"],
        esTipoPersonaJuridica: json["EsTipoPersonaJuridica"],
        longitudTipoDocumento: json["LongitudTipoDocumento"],
      );

  Map<String, dynamic> toJson() => {
        "CodigoTipoDocumento": codigoTipoDocumento,
        "CodigoTipoDocumentoCamaraCompensacion":
            codigoTipoDocumentoCamaraCompensacion,
        "DescripcionTipoDocumento": descripcionTipoDocumento,
        "EsTipoPersonaJuridica": esTipoPersonaJuridica,
        "LongitudTipoDocumento": longitudTipoDocumento,
      };
}
