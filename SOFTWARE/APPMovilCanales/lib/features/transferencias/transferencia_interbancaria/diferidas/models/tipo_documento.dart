import 'package:caja_tacna_app/features/shared/models/select_tipo_documento.dart';

class TipoDocumentoTransInter extends SelectTipoDocumento {
  final int codigoTipoDocumentoCamaraCompensacion;
  final bool esTipoPersonaJuridica;
  final int longitudTipoDocumento;

  TipoDocumentoTransInter({
    required int idTipoDocumento,
    required this.codigoTipoDocumentoCamaraCompensacion,
    required String descripcion,
    required this.esTipoPersonaJuridica,
    required this.longitudTipoDocumento,
  }) : super(
          idTipoDocumento: idTipoDocumento,
          descripcion: descripcion,
        );

  factory TipoDocumentoTransInter.fromJson(Map<String, dynamic> json) =>
      TipoDocumentoTransInter(
        idTipoDocumento: json["IdTipoDocumento"],
        codigoTipoDocumentoCamaraCompensacion:
            json["CodigoTipoDocumentoCamaraCompensacion"],
        descripcion: json["Descripcion"],
        esTipoPersonaJuridica: json["EsTipoPersonaJuridica"],
        longitudTipoDocumento: json["LongitudTipoDocumento"],
      );

  Map<String, dynamic> toJson() => {
        "IdTipoDocumento": idTipoDocumento,
        "CodigoTipoDocumentoCamaraCompensacion":
            codigoTipoDocumentoCamaraCompensacion,
        "Descripcion": descripcion,
        "EsTipoPersonaJuridica": esTipoPersonaJuridica,
        "LongitudTipoDocumento": longitudTipoDocumento,
      };
}
