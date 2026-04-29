import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:formz/formz.dart';

enum NumeroDocumentoError { empty, format }

class NumeroDocumento extends FormzInput<String, NumeroDocumentoError> {
  const NumeroDocumento.pure(String value) : super.pure(value);
  const NumeroDocumento.dirty(String value) : super.dirty(value);

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == NumeroDocumentoError.empty) {
      return 'Debes completar este campo.';
    }

    if (displayError == NumeroDocumentoError.format) {
      return 'Número inválido';
    }

    return null;
  }

  @override
  NumeroDocumentoError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) {
      return NumeroDocumentoError.empty;
    }
    if (!_validarDigitosRepetidos(value)) {
      return NumeroDocumentoError.format;
    }
    if (!CtUtils.isSafeInput(value)) {
      return NumeroDocumentoError.format;
    }

    return null;
  }
}

bool _validarDigitosRepetidos(String value) {
  RegExp regex = RegExp(r'^(.)\1*$');

  return !regex.hasMatch(value);
}
