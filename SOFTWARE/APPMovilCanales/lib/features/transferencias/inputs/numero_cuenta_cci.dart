import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:formz/formz.dart';

enum NumeroCuentaCciError { empty, invalid }

class NumeroCuentaCci extends FormzInput<String, NumeroCuentaCciError> {
  const NumeroCuentaCci.pure(super.value) : super.pure();
  const NumeroCuentaCci.dirty(super.value) : super.dirty();

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == NumeroCuentaCciError.empty) {
      return 'Debes completar este campo.';
    }
    if (displayError == NumeroCuentaCciError.invalid) {
      return 'La cuenta ingresada no es correcta';
    }

    return null;
  }

  @override
  NumeroCuentaCciError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) {
      return NumeroCuentaCciError.empty;
    }
    if (!CtUtils.validarEstructuraNumeroCci(value.trim())) {
      return NumeroCuentaCciError.invalid;
    }

    return null;
  }
}