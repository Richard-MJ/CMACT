import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:formz/formz.dart';

enum AliasCuentaAhorroError { empty, format }

class AliasCuentaAhorro extends FormzInput<String, AliasCuentaAhorroError> {
  const AliasCuentaAhorro.pure(String value) : super.pure(value);
  const AliasCuentaAhorro.dirty(String value) : super.dirty(value);

  String? get errorMessage {
    if (isValid || isPure) return null;
    if (displayError == AliasCuentaAhorroError.empty) {
      return 'Debes completar este campo.';
    }
    if (displayError == AliasCuentaAhorroError.format) {
      return 'Alias inválido';
    }
    return null;
  }

  @override
  AliasCuentaAhorroError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) {
      return AliasCuentaAhorroError.empty;
    }
    if (!CtUtils.isSafeInput(value)) {
      return AliasCuentaAhorroError.format;
    }

    return null;
  }
}
