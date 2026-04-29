import 'package:formz/formz.dart';

enum NumeroCelularError { empty, length, format }

class NumeroCelular extends FormzInput<String, NumeroCelularError> {
  const NumeroCelular.pure(String value) : super.pure(value);
  const NumeroCelular.dirty(String value) : super.dirty(value);

  String? get errorMessage {
    if (isValid || isPure) return null;

    if (displayError == NumeroCelularError.empty) {
      return 'Debes completar este campo.';
    }
    if (displayError == NumeroCelularError.length) {
      return 'Debe tener 9 dígitos';
    }
    if (displayError == NumeroCelularError.format) {
      return 'Numero invalido';
    }
    return null;
  }

  @override
  NumeroCelularError? validator(String value) {
    final sanitizedValue = value.trim();
    const repeatedNumbers = {
      '111111111', '222222222', '333333333', '444444444',
      '555555555', '666666666', '777777777', '888888888',
      '999999999', '000000000'
    };

    if (sanitizedValue.isEmpty) return NumeroCelularError.empty;
    if (sanitizedValue.length != 9) return NumeroCelularError.length;
    if (repeatedNumbers.contains(sanitizedValue)) return NumeroCelularError.format;

    return null;
  }
}
