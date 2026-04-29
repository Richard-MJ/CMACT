import 'package:caja_tacna_app/features/shared/models/select_tipo_documento.dart';

class TipoDocumentoLocal extends SelectTipoDocumento {
  TipoDocumentoLocal({
    required int idTipoDocumento,
    required String descripcion,
  }) : super(
          idTipoDocumento: idTipoDocumento,
          descripcion: descripcion,
        );
}
