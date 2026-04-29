import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:formz/formz.dart';

enum DireccionError { empty, format }

class Direccion extends FormzInput<String, DireccionError> {
  const Direccion.pure(String value) : super.pure(value);
  const Direccion.dirty(String value) : super.dirty(value);

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == DireccionError.empty) {
      return 'Debes completar este campo.';
    }
    if (displayError == DireccionError.format) {
      return 'Dirección inválida.';
    }
    return null;
  }

  @override
  DireccionError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) {
      return DireccionError.empty;
    }
    if (!CtUtils.isSafeInput(value)) {
      return DireccionError.format;
    }
    return null;
  }
}
