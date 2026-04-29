import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:formz/formz.dart';

enum NombreBeneficiarioError { empty, length, format }

class NombreBeneficiario extends FormzInput<String, NombreBeneficiarioError> {
  const NombreBeneficiario.pure(super.value) : super.pure();
  const NombreBeneficiario.dirty(super.value) : super.dirty();

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == NombreBeneficiarioError.empty) {
      return 'Debes completar este campo.';
    }
    if (displayError == NombreBeneficiarioError.length) {
      return 'La cantidad maxima de caracteres es 100';
    }
    if (displayError == NombreBeneficiarioError.format) {
      return 'Nombre inválido.';
    }
    return null;
  }

  @override
  NombreBeneficiarioError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) {
      return NombreBeneficiarioError.empty;
    }
    if (value.length > 100) {
      return NombreBeneficiarioError.length;
    }
    if (!CtUtils.isSafeInput(value)) {
      return NombreBeneficiarioError.format;
    }
    return null;
  }
}
