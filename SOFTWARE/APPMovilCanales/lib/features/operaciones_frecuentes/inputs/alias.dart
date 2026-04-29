import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:formz/formz.dart';

enum AliasOpFrecuenteError { empty, format }

class AliasOpFrecuente extends FormzInput<String, AliasOpFrecuenteError> {
  const AliasOpFrecuente.pure(String value) : super.pure(value);
  const AliasOpFrecuente.dirty(String value) : super.dirty(value);

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == AliasOpFrecuenteError.empty) {
      return 'Debes completar este campo.';
    }
    if (displayError == AliasOpFrecuenteError.format) {
      return 'Alias inválido';
    }
    return null;
  }

  @override
  AliasOpFrecuenteError? validator(String value) {
    if (value.isEmpty || value.trim().isEmpty) {
      return AliasOpFrecuenteError.empty;
    }
    if (!CtUtils.isSafeInput(value)) {
      return AliasOpFrecuenteError.format;
    }

    return null;
  }
}
