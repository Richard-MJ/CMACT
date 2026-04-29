import 'package:caja_tacna_app/features/shared/models/select_tipo_documento.dart';

class TipoDocumentoTransInter extends SelectTipoDocumento {
  final int codigoTipoDocumentoCamaraCompensacion;
  final bool esTipoPersonaJuridica;
  final int longitudTipoDocumento;

  TipoDocumentoTransInter({
    required super.idTipoDocumento,
    required this.codigoTipoDocumentoCamaraCompensacion,
    required super.descripcion,
    required this.esTipoPersonaJuridica,
    required this.longitudTipoDocumento,
  });

  factory TipoDocumentoTransInter.fromJson(Map<String, dynamic> json) =>
      TipoDocumentoTransInter(
        idTipoDocumento: json["CodigoTipoDocumento"],
        codigoTipoDocumentoCamaraCompensacion: json["CodigoTipoDocumentoCCE"],
        descripcion: json["DescripcionTipoDocumento"],
        esTipoPersonaJuridica: json["EsTipoPersonaJuridica"],
        longitudTipoDocumento: json["LongitudDocumentoCCE"],
      );

  Map<String, dynamic> toJson() => {
        "CodigoTipoDocumento": idTipoDocumento,
        "CodigoTipoDocumentoCCE": codigoTipoDocumentoCamaraCompensacion,
        "DescripcionTipoDocumento": descripcion,
        "EsTipoPersonaJuridica": esTipoPersonaJuridica,
        "LongitudDocumentoCCE": longitudTipoDocumento,
      };
}
